using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai.Rental.WebApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private string stringConexao = @"Data Source=MARCAUM\SQLEXPRESS; initial catalog=M_RENTAL; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(ClienteDomain ClienteAtualizado)
        {
            if (ClienteAtualizado.nome != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE CLIENTE SET nome = @novoNomeCliente, sobrenome = @novoSobrenomeCliente, RG = @novoRGCliente WHERE idCliente = @idClienteAtualizado";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@NovoNomeCliente", ClienteAtualizado.nome);
                        cmd.Parameters.AddWithValue("@novoSobrenomeCliente", ClienteAtualizado.sobrenome);
                        cmd.Parameters.AddWithValue("@novoRGCliente", ClienteAtualizado.RG);
                        cmd.Parameters.AddWithValue("@idClienteAtualizado", ClienteAtualizado.idCliente);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AtualizarIdUrl(int idCliente, ClienteDomain ClienteAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE CLIENTE SET nome = @novoNomeCliente, sobrenome = @novoSobrenomeCliente, RG = @novoRGCliente WHERE idCliente = @idClienteAtualizado";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@novoNomeCliente", ClienteAtualizado.nome);
                    cmd.Parameters.AddWithValue("@novoSobrenomeCliente", ClienteAtualizado.sobrenome);
                    cmd.Parameters.AddWithValue("@novoRGCliente", ClienteAtualizado.RG);
                    cmd.Parameters.AddWithValue("@idClienteAtualizado", idCliente);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ClienteDomain BuscarPorId(int idCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idCliente, nome, sobrenome, RG FROM CLIENTE WHERE idCliente = @idCliente";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ClienteDomain clienteBuscado = new ClienteDomain
                        {
                            idCliente = Convert.ToInt32(reader["idCliente"]),
                            nome = reader["nome"].ToString(),
                            sobrenome = reader["sobrenome"].ToString(),
                            RG = reader["RG"].ToString()
                        };
                        return clienteBuscado;
                    }
                    return null;
                }
            };
        }

        public void Cadastrar(ClienteDomain novoCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO CLIENTE (nome, sobrenome, RG) VALUES (@nome, @sobrenome, @RG)";


                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@nome", novoCliente.nome);
                    cmd.Parameters.AddWithValue("@sobrenome", novoCliente.sobrenome);
                    cmd.Parameters.AddWithValue("@RG", novoCliente.RG);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM CLIENTE WHERE idCliente = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClienteDomain> ListarTodos()
        {
            List<ClienteDomain> ListaClientes = new List<ClienteDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idCliente, nome, sobrenome, RG FROM CLIENTE";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ClienteDomain Cliente = new ClienteDomain()
                        {
                            idCliente = Convert.ToInt32(rdr[0]),
                            nome = rdr[1].ToString(),
                            sobrenome = rdr[2].ToString(),
                            RG = rdr[3].ToString()
                        };
                        ListaClientes.Add(Cliente);
                    }
                }
            }
            return ListaClientes;
        }
    }
}
