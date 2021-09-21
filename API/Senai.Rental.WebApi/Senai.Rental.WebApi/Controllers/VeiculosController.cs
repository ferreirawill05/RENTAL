using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;

namespace Senai.Rental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _VeiculoRepository { get; set; }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> ListaVeiculos = _VeiculoRepository.ListarTodos();

            return Ok(ListaVeiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            VeiculoDomain VeiculoBuscada = _VeiculoRepository.BuscarPorId(id);

            if (VeiculoBuscada == null)
            {
                return NotFound("Nenhuma Veiculo encontrada!");
            }
            return Ok(VeiculoBuscada);
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain novaVeiculo)
        {
            _VeiculoRepository.Cadastrar(novaVeiculo);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, VeiculoDomain VeiculoAtualizada)
        {
            VeiculoDomain VeiculoBuscada = _VeiculoRepository.BuscarPorId(id);

            {
                if (VeiculoBuscada == null)
                {
                    return NotFound
                        (new
                        {
                            mensagem = "Veiculo não encontrado!"
                        });
                }
            }

            try
            {
                _VeiculoRepository.AtualizarIdUrl(id, VeiculoAtualizada);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutBody(VeiculoDomain VeiculoAtualizada)
        {
            if (VeiculoAtualizada.cor == null || VeiculoAtualizada.idVeiculo == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Nome do Veiculo ou id do Veiculo não foi informado!"
                    }
                );
            }

            VeiculoDomain VeiculoBuscada = _VeiculoRepository.BuscarPorId(VeiculoAtualizada.idVeiculo);

            if (VeiculoBuscada != null)
            {
                try
                {
                    _VeiculoRepository.AtualizarIdCorpo(VeiculoAtualizada);

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
                        mensagemErro = "Veiculo não encontrado!",
                        codErro = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _VeiculoRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
