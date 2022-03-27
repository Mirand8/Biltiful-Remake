using CadastrosBasicos;
using System;
using System.Collections.Generic;
using System.Data;
using Biltiful.DataBase;
using Cadastros;
using System.Data.SqlClient;

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
                cmd.Parameters.AddWithValue("@Ultima_Compra", SqlDbType.Date).Value = fornecedor.UltimaVenda;
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
            cnpj = Fornecedor.FormataCNPJ(cnpj);
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

        public List<Fornecedor> GetFornecedores()
        {
            var fornecedores = new List<Fornecedor>();
            var sql = "SELECT * FROM Fornecedor WHERE Situacao = 'A'";

            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cnpj = reader["CNPJ"].ToString();
                            var razaoSocial = reader["Razao_Social"].ToString();
                            var dataAbertura = Convert.ToDateTime(reader["Data_Abertura"]);
                            var ultimaVenda = Convert.ToDateTime(reader["Ultima_Venda"]);
                            var dataCadastro = Convert.ToDateTime(reader["Data_Cadastro"]);
                            var situacao = Convert.ToChar(reader["Situacao"]);
                            fornecedores.Add(new Fornecedor(cnpj, razaoSocial, dataAbertura, ultimaVenda, dataCadastro, situacao));
                        }
                    }
                }
            }
            return fornecedores;
        }

        public Fornecedor GetFornecedor(string cnpj)
        {
            Fornecedor fornecedor;

            var sql = $"SELECT * FROM Fornecedor WHERE CPF = '{cnpj}'";

            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        cnpj = reader["CNPJ"].ToString();
                        var razaoSocial = reader["Razao_Social"].ToString();
                        var dataAbertura = Convert.ToDateTime(reader["Data_Abertura"]);
                        var ultimaVenda = Convert.ToDateTime(reader["Ultima_Venda"]);
                        var dataCadastro = Convert.ToDateTime(reader["Data_Cadastro"]);
                        var situacao = Convert.ToChar(reader["Situacao"]);
                        fornecedor = new Fornecedor(cnpj, razaoSocial, dataAbertura, ultimaVenda, dataCadastro, situacao);
                    }
                    connection.Close();
                }
            }
            return fornecedor;
        }

        public void AtualizarFornecedor(string razaoSocial, string cnpj)
        {
            Console.WriteLine("Atualizando fornecedor: " + cnpj);
            Console.ReadKey();
            using (var connection = new SqlConnection(DBHelper.GetDBConnectionString()))
            {
                var cmd = new SqlCommand("AlteraNomeFornecedor", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cnpj;
                cmd.Parameters.AddWithValue("@Razao_Social", SqlDbType.NVarChar).Value = razaoSocial;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}