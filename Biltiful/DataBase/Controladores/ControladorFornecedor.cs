using CadastrosBasicos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biltiful.DataBase;
using Biltiful.Cadastros;

namespace ProjBiltiful.DataBase
{
    public class ControladorFornecedor
    {
        public void InserirFornecedor(Fornecedor fornecedor)
        {
            Console.WriteLine("Inserindo fornecedor: " + fornecedor);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("InsereCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CNPJ", SqlDbType.NVarChar).Value = fornecedor.CNPJ;
                cmd.Parameters.AddWithValue("@RazaoSocial", SqlDbType.NVarChar).Value = fornecedor.RazaoSocial;
                cmd.Parameters.AddWithValue("@DataAbertura", SqlDbType.Date).Value = fornecedor.DataAbertura;
                cmd.Parameters.AddWithValue("@Ultima_Compra", SqlDbType.Date).Value = fornecedor.UltimaCompra;
                cmd.Parameters.AddWithValue("@Data_Cadastro", SqlDbType.Date).Value = fornecedor.DataCadastro;
                cmd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = fornecedor.Situacao;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool JaExiste(string cnpj)
        {
            bool existe;
            cnpj = FormataCnpj(cnpj);
            var sql = $"SELECT COUNT(1) FROM Fornecedor WHERE CNPJ = '{cnpj}'";

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

        private static string FormataCnpj(string cnpj) => cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");

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
    }
}