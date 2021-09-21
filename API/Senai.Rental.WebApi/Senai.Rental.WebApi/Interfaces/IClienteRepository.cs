using Senai.Rental.WebApi.Domains;
using System.Collections.Generic;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IClienteRepository
    {
        List<ClienteDomain> ListarTodos();

        ClienteDomain BuscarPorId(int idCliente);

        void Cadastrar(ClienteDomain novoCliente);

        void AtualizarIdCorpo(ClienteDomain ClienteAtualizado);

        void AtualizarIdUrl(int idCliente, ClienteDomain ClienteAtualizado);

        void Deletar(int idCliente);
    }
}
