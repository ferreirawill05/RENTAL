using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai.Rental.WebApi.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = @"Data Source=MARCAUM\SQLEXPRESS; initial catalog=M_RENTAL; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(VeiculoDomain VeiculoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE VEICULO SET cor = @novaCorVeiculo, placa = @novaPlacaVeiculo, idEmpresa = @novaEmpresaVeiculo WHERE idVeiculo = @idVeiculoAtualizado";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novaCorVeiculo", VeiculoAtualizado.cor);
                    cmd.Parameters.AddWithValue("@novaPlacaVeiculo", VeiculoAtualizado.placa);
                    cmd.Parameters.AddWithValue("@novaEmpresaVeiculo", VeiculoAtualizado.empresa.idEmpresa);
                    cmd.Parameters.AddWithValue("@idVeiculoAtualizado", VeiculoAtualizado.idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int idVeiculo, VeiculoDomain VeiculoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE VEICULO SET cor = @novaCorVeiculo, placa = @novaPlacaVeiculo, idEmpresa = @novaEmpresaVeiculo WHERE idVeiculo = @idVeiculoAtualizado";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novaCorVeiculo", VeiculoAtualizado.cor);
                    cmd.Parameters.AddWithValue("@novaPlacaVeiculo", VeiculoAtualizado.placa);
                    cmd.Parameters.AddWithValue("@novaEmpresaVeiculo", VeiculoAtualizado.empresa.idEmpresa);
                    cmd.Parameters.AddWithValue("@idVeiculoAtualizado", idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idVeiculo, EMPRESA.idEmpresa, nomeEmpresa, endereco, cor, placa FROM VEICULO INNER JOIN EMPRESA ON VEICULO.idEmpresa = EMPRESA.idEmpresa WHERE idVeiculo = @idVeiculoBuscado";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculoBuscado", idVeiculo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        VeiculoDomain VeiculoBuscado = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(reader["idVeiculo"]),
                            cor = reader["cor"].ToString(),
                            placa = reader["placa"].ToString(),
                            empresa = new EmpresaDomain
                            {
                                idEmpresa = Convert.ToInt32(reader["idEmpresa"]),
                                nomeEmpresa = reader["nomeEmpresa"].ToString(),
                                endereco = reader["endereco"].ToString()
                            }
                        };
                        return VeiculoBuscado;
                    }
                    return null;
                }
            };
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO VEICULO (idEmpresa, cor, placa) VALUES (@idEmpresa, @cor, @placa)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.empresa.idEmpresa);
                    cmd.Parameters.AddWithValue("@cor", novoVeiculo.cor);
                    cmd.Parameters.AddWithValue("@placa", novoVeiculo.placa);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM VEICULO WHERE idVeiculo = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> ListaVeiculos = new List<VeiculoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idVeiculo, EMPRESA.idEmpresa, nomeEmpresa, endereco, cor, placa FROM VEICULO INNER JOIN EMPRESA ON VEICULO.idEmpresa = EMPRESA.idEmpresa";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        VeiculoDomain Veiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(reader["idVeiculo"]),
                            cor = reader["cor"].ToString(),
                            placa = reader["placa"].ToString(),
                            empresa = new EmpresaDomain
                            {
                                idEmpresa = Convert.ToInt32(reader["idEmpresa"]),
                                nomeEmpresa = reader["nomeEmpresa"].ToString(),
                                endereco = reader["endereco"].ToString()
                            }
                        };
                        ListaVeiculos.Add(Veiculo);
                    }
                }
            }
            return ListaVeiculos;
        }
    }
}
