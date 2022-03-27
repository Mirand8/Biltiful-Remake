using Cadastros;
using CadastrosBasicos;
using ProjBiltiful.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biltiful.Visualizacao
{
    public static class VisuFornecedor
    {

        public static void MenuFornecedor()
        {
            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== FORNECEDORES ===============");
                Console.WriteLine("1. Cadastar");
                Console.WriteLine("2. Listar");
                Console.WriteLine("3. Editar registro");
                Console.WriteLine("4. Bloquear/Desbloqueia");
                Console.WriteLine("5. Localizar");
                Console.WriteLine("6. Localizar bloqueado");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        SubMenuRegistraFornecedor();
                        break;

                    case "2":
                        SubMenuNavegacaoFornecedores();
                        break;

                    case "3":
                        SubMenuEdicaoFornecedor();
                        break;

                    case "4":
                        SubMenuBloqueiaFornecedor();
                        break;

                    case "5":
                        SubMenuProcuraFornecedor();
                        break;

                    case "6":
                        SubMenuProcuraFornecedorBloqueado();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                        break;

                }
            } while (escolha != "0");
        }

        public static void SubMenuRegistraFornecedor()
        {
            Console.Clear();
            string cnpj;

            do
            {
                Console.Write("CNPJ: ");
                cnpj = Console.ReadLine();
                if (!Validacoes.ValidarCnpj(Fornecedor.FormataCNPJ(cnpj))) Console.WriteLine("CNPJ invalido! Digite novamente ou digite 0 para sair!");
                Console.Clear();
            } while (!Validacoes.ValidarCnpj(Fornecedor.FormataCNPJ(cnpj)) || cnpj == "0");

            if (new ControladorFornecedor().JaExiste(Fornecedor.FormataCNPJ(cnpj)))
            {
                Console.WriteLine("O fornecedor ja existe na base de dados! Digite 0 para tentar novamente ou 1 para sair!");
                Console.Write("=> ");
                string decisao = Console.ReadLine();
                if (decisao == "0") SubMenuRegistraFornecedor();
                else if (decisao == "1") MenuFornecedor();
            }
            else
            {
                Console.Write("Razao social: ");
                string razaoSocial = Console.ReadLine();
                Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                char situacao = char.Parse(Console.ReadLine().ToUpper());
                new ControladorFornecedor().InserirFornecedor(new Fornecedor(cnpj, razaoSocial, situacao));
                Console.WriteLine("Fornecedor de CNPJ {cnpj} foi cadastrado com sucesso!\n");
            }

            Console.WriteLine("Pressione enter para continuar");
            Console.ReadKey();
        }

        public static void SubMenuNavegacaoFornecedores()
        {
            Console.WriteLine("============== Fornecedor ==============");
            var controladorFornecedor = new ControladorFornecedor();
            List<Fornecedor> fornecedores = controladorFornecedor.GetFornecedores();
            int opcao = 0, posicao = 0;

            if (fornecedores.First().CNPJ != null)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("============== Fornecedor ==============");

                    if (opcao == 0)
                    {
                        Console.WriteLine(fornecedores.ElementAt(posicao).ToString());
                    }
                    else if (opcao == 1)
                    {
                        if (posicao == fornecedores.Count - 1) posicao = fornecedores.Count - 1;
                        else posicao++;
                        Console.WriteLine(fornecedores.ElementAt(posicao));
                    }
                    else if (opcao == 2)
                    {
                        if (posicao == 0) posicao = 0;
                        else posicao--;
                        Console.WriteLine(fornecedores.ElementAt(posicao));
                    }
                    else if (opcao == 3)
                    {
                        posicao = 0;
                        Console.WriteLine(fornecedores.First());
                    }
                    else if (opcao == 4)
                    {
                        posicao = fornecedores.Count - 1;
                        Console.WriteLine(fornecedores.Last());
                    }

                    Console.WriteLine(@"
                                    1. Proximo 
                                    2. Anterior
                                    3. Primeiro
                                    4. Ultimo
                                    0. Voltar para menu anterior.");
                    bool flag;
                    do flag = int.TryParse(Console.ReadLine(), out opcao); while (!flag);

                } while (opcao != 0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ainda nao tem nenhum fornecedor cadastrado");
                Console.WriteLine("Pressione enter para continuar...");
                Console.ReadKey();
            }
        }

        public static void SubMenuEdicaoFornecedor()
        {
            char continuar;
            do
            {
                Console.Clear();
                Console.WriteLine("Somente algumas informacoes podem ser alterada como (Nome/Data de Nascimento/sexo/Situacao)");
                Console.WriteLine("Digite o CPF do cliente que deseja editar: ");
                Console.Write("CPF: ");
                string cpf = Console.ReadLine();

                var controladorCliente = new ControladorCliente();
                if (controladorCliente.JaExiste(cpf))
                {
                    Console.WriteLine("Digite qual informaçao do cliente deseja alterar: ");
                    Console.Write("\t1.Nome\n\t2.Data de Nascimento\n\t2.Sexo\n\t3.Ativar/Desativar cliente\nEscolha:");
                    string nome;
                    DateTime dataNasc;

                    char escolha = Char.Parse(Console.ReadLine());
                    if (escolha == '1')
                    {
                        Console.WriteLine("Nome: ");
                        nome = Console.ReadLine();
                        controladorCliente.AtualizarCliente(nome, cpf);
                    }
                    else if (escolha == '2')
                    {
                        Console.WriteLine("Data de nascimento: ");
                        dataNasc = DateTime.Parse(Console.ReadLine());
                        controladorCliente.AtualizarCliente(dataNasc, cpf);
                    }
                    else if (escolha == '3')
                    {
                        Console.WriteLine("Deseja alterar o sexo?\n\t1.S => (Sim)\n\t2.N => (Nao)");
                        if (char.ToUpper(char.Parse(Console.ReadLine())) == 'S') controladorCliente.AtualizarCliente('1', cpf);
                    }
                    else if (escolha == '4')
                    {
                        Console.WriteLine("Desativar cliente?\n\t1.S => (Sim)\n\t2.N => (Nao)");
                        if (char.ToUpper(char.Parse(Console.ReadLine())) == 'S') controladorCliente.AtualizarCliente('2', cpf);
                    }
                    continuar = '0';
                }
                else
                {
                    Console.WriteLine($"O cliente de CPF {cpf} não existe!");
                    Console.WriteLine("Digite \n\t1. para tentar novamente\n\t0.Para Sair");
                    continuar = Char.Parse(Console.ReadLine());
                }
            } while (continuar != '0');

            Console.WriteLine("Retornando ao menu...");
            Console.ReadKey();
        }

        private static void SubMenuBloqueiaFornecedor()
        {
            
        }

        public static void SubMenuProcuraFornecedor()
        {
            Console.Clear();
            Console.WriteLine("- BLOQUEAR/DESBLOQUEAR CLIENTE -");
            var controladorCliente = new ControladorCliente();
            Console.Write("CPF do Cliente: ");
            string cpf = Console.ReadLine();

            while (!Validacoes.ValidarCpf(Cliente.FormataCPF(cpf)) && cpf != "0")
            {
                if (!Validacoes.ValidarCpf(Cliente.FormataCPF(cpf))) Console.WriteLine("CPF invalido! Digite novamente ou digite 0 para sair!");
                Console.Write("CPF do Cliente: ");
                cpf = Console.ReadLine();
            }

            if (controladorCliente.ProcurarBloqueado(cpf) == null)
            {
                Console.WriteLine($"O cliente de cpf {cpf} ja esta bloqueado!");
                Console.WriteLine("\t0. Voltar para o menu anterior\n\t1. Desbloquear cliente\n\t2. Procurar outro cliente");
                string decisao = Console.ReadLine();
                if (decisao == "0")
                {
                    Console.ReadKey();
                    Console.Clear();
                    MenuCliente();
                }
                else if (decisao == "2")
                {
                    Console.ReadKey();
                    SubMenuProcuraFornecedor();
                }
                else if (decisao == "1")
                {
                    controladorCliente.Desbloquear(cpf);
                    Console.WriteLine($"O cliente de cpf {cpf} foi desbloqueado e agora tem acesso as compras!");
                }
            }
            else
            {
                controladorCliente.Bloquear(cpf);
                Console.WriteLine($"Cliente de cpf ({cpf}) foi bloqueado e nao tem acesso mais as compras de cosmeticos!");
            }

            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        public static void SubMenuProcuraFornecedorBloqueado()
        {
            Console.Clear();
            var controladorCliente = new ControladorCliente();
            Console.WriteLine("- PROCURAR CLIENTE BLOQUEADO - ");
            Console.Write("CPF do Cliente: ");
            string cpf = Console.ReadLine();
            if (!Validacoes.ValidarCpf(cpf))
            {
                Console.WriteLine("CPF invalido! Digite novamente ou digite 0 para sair!");
                Console.Write("CPF do Cliente: ");
                cpf = Console.ReadLine();
                if (cpf == "0") MenuFornecedor();
                else
                {
                    Console.ReadKey();
                    SubMenuProcuraFornecedorBloqueado();
                }
            }

            var cliente = controladorCliente.ProcurarBloqueado(Cliente.FormataCPF(cpf));
            if (cliente != null) Console.WriteLine(cliente); 

            else
            {
                Console.WriteLine("Cliente bloqueado nao encontrado");
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();

            return null;
        }
    }
}
