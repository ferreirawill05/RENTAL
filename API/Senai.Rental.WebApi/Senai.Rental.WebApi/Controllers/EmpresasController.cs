using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private IEmpresaRepository _EmpresaRepository { get; set; }

        public EmpresasController()
        {
            _EmpresaRepository = new EmpresaRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EmpresaDomain> ListaEmpresas = _EmpresaRepository.ListarTodos();

            return Ok(ListaEmpresas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EmpresaDomain empresaBuscada = _EmpresaRepository.BuscarPorId(id);

            if (empresaBuscada == null)
            {
                return NotFound("Nenhuma empresa encontrada!");
            }
            return Ok(empresaBuscada);
        }

        [HttpPost]
        public IActionResult Post(EmpresaDomain novaEmpresa)
        {
            _EmpresaRepository.Cadastrar(novaEmpresa);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, EmpresaDomain empresaAtualizada)
        {
            EmpresaDomain empresaBuscada = _EmpresaRepository.BuscarPorId(id);

            {
            if (empresaBuscada == null)
                {
                    return NotFound
                        (new
                        {
                            mensagem = "Empresa não encontrada!"
                        });
                }
            }

            try
            {
                _EmpresaRepository.AtualizarIdUrl(id, empresaAtualizada);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutBody(EmpresaDomain EmpresaAtualizada)
        {
            if (EmpresaAtualizada.nomeEmpresa == null || EmpresaAtualizada.idEmpresa == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome da Empresa ou id da Empresa não foi informado!"
                    }
                );
            }

            EmpresaDomain EmpresaBuscada = _EmpresaRepository.BuscarPorId(EmpresaAtualizada.idEmpresa);

            if (EmpresaBuscada != null)
            {
                try
                {
                    _EmpresaRepository.AtualizarIdCorpo(EmpresaAtualizada);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

            return NotFound(
                    new
                    {
                        mensagemErro = "Empresa não encontrada!",
                        codErro = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _EmpresaRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
