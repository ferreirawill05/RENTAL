using Senai.Rental.WebApi.Domains;
using System.Collections.Generic;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IEmpresaRepository
    {
        List<EmpresaDomain> ListarTodos();

        EmpresaDomain BuscarPorId(int idEmpresa);

        void Cadastrar(EmpresaDomain novaEmpresa);

        void AtualizarIdCorpo(EmpresaDomain EmpresaAtualizada);

        void AtualizarIdUrl(int idEmpresa, EmpresaDomain EmpresaAtualizada);

        void Deletar(int idEmpresa);
    }
}
