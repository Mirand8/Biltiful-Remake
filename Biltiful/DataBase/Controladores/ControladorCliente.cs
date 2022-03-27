using CadastrosBasicos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Biltiful.DataBase;

namespace ProjBiltiful.DataBase
{
    public class ControladorCliente
    {
        public void InserirCliente(Cliente cliente)
        {
            Console.WriteLine("Inserindo cliente: " + cliente);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("InsereCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cliente.CPF;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                cmd.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = cliente.DataNascimento;
                cmd.Parameters.AddWithValue("@Sexo", SqlDbType.Char).Value = cliente.Sexo;
                cmd.Parameters.AddWithValue("@Ultima_Compra", SqlDbType.Date).Value = cliente.UltimaCompra;
                cmd.Parameters.AddWithValue("@Data_Cadastro", SqlDbType.Date).Value = cliente.DataCadastro;
                cmd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = cliente.Situacao;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool JaExiste(string cpf)
        {
            bool existe;
            var sql = $"SELECT COUNT(1) FROM Cliente WHERE CPF = '{cpf}'";

            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    existe = (int)cmd.ExecuteScalar() == 1;
                    connection.Close();
                }
            }
            return existe;
        }

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();
            var sql = "SELECT * FROM Cliente WHERE Situacao = 'A'";

            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cpf = reader["CPF"].ToString();
                            var nome = reader["Nome"].ToString();
                            var dataNasc = Convert.ToDateTime(reader["DataNasc"]);
                            var sexo = Convert.ToChar(reader["Sexo"]);
                            var ultimaCompra = Convert.ToDateTime(reader["Ultima_Compra"]);
                            var dataCadastro = Convert.ToDateTime(reader["Data_Cadastro"]);
                            var situacao = Convert.ToChar(reader["Situacao"]);
                            clientes.Add(new Cliente(cpf, nome, dataNasc, sexo, ultimaCompra, dataCadastro, situacao));
                        }
                    }
                }
            }
            return clientes;
        }

        public Cliente GetCliente(string cpf)
        {
            Cliente cliente;

            var sql = $"SELECT * FROM Cliente WHERE CPF = '{cpf}'";

            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        var nCpf = reader["CPF"].ToString();
                        var nome = reader["Nome"].ToString();
                        var dataNasc = Convert.ToDateTime(reader["DataNasc"]);
                        var sexo = Convert.ToChar(reader["Sexo"]);
                        var ultimaCompra = Convert.ToDateTime(reader["Ultima_Compra"]);
                        var dataCadastro = Convert.ToDateTime(reader["Data_Cadastro"]);
                        var situacao = Convert.ToChar(reader["Situacao"]);
                        cliente = new Cliente(cpf, nome, dataNasc, sexo, ultimaCompra, dataCadastro, situacao);
                    }
                    connection.Close();
                }
            }
            return cliente;
        }

        public void AtualizarCliente(string nome, string cpf)
        {
            Console.WriteLine("Atualizando cliente: " + cpf);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("AlteraNomeCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                cmd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = nome;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AtualizarCliente(DateTime dataNasc, string cpf)
        {
            Console.WriteLine("Alterando cliente: " + cpf);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("AtualizaNascCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                cmd.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = dataNasc;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AtualizarCliente(char tipo, string cpf)
        {
            Console.WriteLine("Alterando cliente: " + cpf);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("AlteraSexoOuSituacaoCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                cmd.Parameters.AddWithValue("@Tipo", SqlDbType.Char).Value = tipo;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Cliente ProcurarBloqueado(string cpf)
        {
            return null;
        }

        public void Bloquear(string cpf)
        {
            
        }

        public void Desbloquear(string cpf)
        {

        }
    }
}