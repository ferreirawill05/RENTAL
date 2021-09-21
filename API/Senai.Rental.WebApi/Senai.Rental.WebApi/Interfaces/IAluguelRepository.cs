using Senai.Rental.WebApi.Domains;
using System.Collections.Generic;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IAluguelRepository
    {
        List<AluguelDomain> ListarTodos();

        AluguelDomain BuscarPorId(int idAluguel);

        void Cadastrar(AluguelDomain novoAluguel);

        void AtualizarIdCorpo(AluguelDomain AluguelAtualizado);

        void AtualizarIdUrl(int idAluguel, AluguelDomain AluguelAtualizado);

        void Deletar(int idAluguel);
    }
}
