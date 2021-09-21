using Senai.Rental.WebApi.Domains;
using System.Collections.Generic;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IVeiculoRepository
    {
        List<VeiculoDomain> ListarTodos();

        VeiculoDomain BuscarPorId(int idVeiculo);

        void Cadastrar(VeiculoDomain novoVeiculo);

        void AtualizarIdCorpo(VeiculoDomain VeiculoAtualizado);

        void AtualizarIdUrl(int idVeiculo, VeiculoDomain VeiculoAtualizado);

        void Deletar(int idVeiculo);
    }
}
