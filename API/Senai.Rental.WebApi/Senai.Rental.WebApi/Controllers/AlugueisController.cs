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
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _AluguelRepository { get; set; }

        public AlugueisController()
        {
            _AluguelRepository = new AluguelRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<AluguelDomain> ListaAlugueis = _AluguelRepository.ListarTodos();

            return Ok(ListaAlugueis);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AluguelDomain AluguelBuscado = _AluguelRepository.BuscarPorId(id);

            if (AluguelBuscado == null)
            {
                return NotFound("Nenhum Aluguel encontrada!");
            }
            return Ok(AluguelBuscado);
        }

        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            _AluguelRepository.Cadastrar(novoAluguel);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, AluguelDomain AluguelAtualizado)
        {
            AluguelDomain AluguelBuscado = _AluguelRepository.BuscarPorId(id);

            {
                if (AluguelBuscado == null)
                {
                    return NotFound
                        (new
                        {
                            mensagem = "Aluguel não encontrado!"
                        });
                }
            }

            try
            {
                _AluguelRepository.AtualizarIdUrl(id, AluguelAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutBody(AluguelDomain AluguelAtualizado)
        {
            if ( AluguelAtualizado.idAluguel == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "id do Aluguel não foi informado!"
                    }
                );
            }

            AluguelDomain AluguelBuscado = _AluguelRepository.BuscarPorId(AluguelAtualizado.idAluguel);

            if (AluguelBuscado != null)
            {
                try
                {
                    _AluguelRepository.AtualizarIdCorpo(AluguelAtualizado);

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
                        mensagemErro = "Aluguel não encontrado!",
                        codErro = true
                    }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _AluguelRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
