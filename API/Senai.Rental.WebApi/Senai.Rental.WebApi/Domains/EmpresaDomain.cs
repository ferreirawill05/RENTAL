using System.ComponentModel.DataAnnotations;

namespace Senai.Rental.WebApi.Domains
{
    public class EmpresaDomain
    {
        public int idEmpresa { get; set; }

        public string nomeEmpresa { get; set; }

        public string endereco { get; set; }
    }
}
