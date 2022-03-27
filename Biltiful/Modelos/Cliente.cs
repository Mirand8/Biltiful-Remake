using ProjBiltiful.DataBase;
using System;
using System.Collections.Generic;

namespace CadastrosBasicos
{
    public class Cliente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, char situacao)
        {
            CPF = FormataCPF(cpf);
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = char.ToUpper(sexo);
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = char.ToUpper(situacao);
        }

        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, DateTime ultimaCompra, DateTime dataCadastro, char situacao)
        {
            CPF = cpf.Length == 11 ? cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-") : cpf;
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaCompra = ultimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public void BloqueiaCadastro()
        {
            Cliente cliente;
            Console.WriteLine("Insira o CPF para bloqueio: ");
            string cpf = Console.ReadLine();
            cpf = cpf.Replace(".", "").Replace("-", "");

            bool flag = false;
            int opcao;
            Console.WriteLine("Cliente ja esta bloqueado");
            Console.WriteLine("Deseja desbloqueado ? [1 - Sim/ 2 - Nao]");

            do
            {
                flag = int.TryParse(Console.ReadLine(), out opcao);
            } while (flag != true);

            if (opcao == 1)
            {
                //write.DesbloqueiaCliente(cpf);
                Console.WriteLine("Cliente desbloqueado");
                Console.WriteLine("Pressione enter para continuar...");
                Console.ReadKey();
            }

            if (Validacoes.ValidarCpf(cpf))
            {
                //cliente = read.ProcuraCliente(cpf);
                //if (cliente != null)
                //{
                //write.BloqueiaCliente(cliente.CPF);
                // Console.WriteLine("CPF bloqueado!");
                // }
            }
            else
            {
                Console.WriteLine("CPF incorreto!");
            }
            //Console.WriteLine("Pressione enter para continuar...");
            //Console.ReadKey();
        }

        public void Localizar()
        {
            Console.WriteLine("Insira o cpf para localizar: ");
            string cpf = Console.ReadLine();

            //Cliente cliente = read.ProcuraCliente(cpf);

            // if (cliente != null)
            //{
            //    Console.WriteLine(cliente.ToString());

            //}
            //else
            Console.WriteLine("Nenhum cadastrado foi encontrado!");
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        public void ClientesBloqueados()
        {
            Console.WriteLine("Insira o CPF para pesquisa: ");
            string cpf = Console.ReadLine();
            bool flag = new Read().ProcurarCPFBloqueado(cpf);

            if (flag)
            {
                Cliente cliente = new Read().ProcuraCliente(cpf);
                Console.WriteLine(cliente.ToString());
            }
            else
            {
                Console.WriteLine("Cliente bloqueado nao encontrado");
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        public override string ToString()
        {
            return "\n====================================================" +
                    "\n\tCPF: " + CPF +
                   "\n\tNome: " + Nome +
                   "\n\tData de Nacimento: " + DataNascimento.ToString("dd/MM/yyyy") +
                   "\n\tSexo: " + Sexo +
                   "\n\tUltima compra: " + UltimaCompra.ToString("dd/MM/yyyy") +
                   "\n\tData cadastro: " + DataCadastro.ToString("dd/MM/yyyy") +
                   "\n\tSituacao: " + Situacao +
                   "\n====================================================";
        }

        public static string FormataCPF(string cpf) => cpf.Length < 11 ? cpf.Remove('.').Remove('-') : cpf;
    }
}
