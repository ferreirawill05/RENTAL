using System;

namespace Senai.Rental.WebApi.Domains
{
    public class AluguelDomain
    {
        public int idAluguel { get; set; }
        public ClienteDomain Cliente { get; set; }
        public VeiculoDomain Veiculo { get; set; }
        public double preco { get; set; }
        public DateTime Adata { get; set; }
    }
}
