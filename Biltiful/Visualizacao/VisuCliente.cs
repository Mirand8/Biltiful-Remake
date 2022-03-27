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
    public static class VisuCliente
    {

        public static void MenuCliente()
        {
            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== CLIENTES ===============");
                Console.WriteLine("1. Cadastrar");
                Console.WriteLine("2. Listar todos");
                Console.WriteLine("3. Editar registro");
                Console.WriteLine("4. Bloqueio OU Desbloqueio");
                Console.WriteLine("5. Localizar");
                Console.WriteLine("6. Localizar bloqueado");
                Console.WriteLine("=======================================================");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        SubMenuRegistrarCliente();
                        break;

                    case "2":
                        SubMenuNavegacaoClientes();
                        break;

                    case "3":
                        SubMenuEdicaoCliente();
                        break;

                    case "4":
                        SubMenuBloqueioCliente();
                        break;

                    case "5":
                        SubMenuProcuraCliente();
                        break;

                    case "6":
                        SubMenuProcurarClienteBloqueado();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                        break;
                }
            } while (escolha != "0");
        }

        public static void SubMenuRegistrarCliente()
        {
            Console.Clear();
            var controladorCliente = new ControladorCliente();
            bool flag;

            DateTime dataNasc;

            do
            {
                Console.Write("Data de nascimento: ");
                flag = DateTime.TryParse(Console.ReadLine(), out dataNasc);
            } while (flag != true);

            if (!Validacoes.CalculaMaioridade(dataNasc))
            {
                Console.WriteLine("Menor de 18 anos nao pode ser cadastrado!!");
                Console.ReadKey();
            }
            else
            {
                string cpf, nome;
                char situacao, sexo;
                bool isCPFValid;
                do
                {
                    Console.Write("CPF: ");
                    cpf = Console.ReadLine();
                    isCPFValid = Validacoes.ValidarCpf(Cliente.FormataCPF(cpf));
                    if (!isCPFValid) Console.WriteLine("Digite um CPF valido!");
                } while (!isCPFValid);

                if (!controladorCliente.JaExiste(cpf))
                {
                    Console.Write("Nome: ");
                    nome = Console.ReadLine();
                    Console.Write("Genero (M - Masculino/ F - Feminino): ");
                    sexo = char.Parse(Console.ReadLine());
                    Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                    situacao = char.Parse(Console.ReadLine());
                    controladorCliente.InserirCliente(new Cliente(cpf, nome, dataNasc, sexo, situacao));
                    Console.WriteLine("Cliente cadastrado com sucesso");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Cliente ja cadastrado!!");
                    Console.ReadKey();
                }
            }
        }

        public static void SubMenuEdicaoCliente()
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

        public static void SubMenuNavegacaoClientes()
        {
            Console.WriteLine("============== Cliente ==============");
            var controladorCliente = new ControladorCliente();
            List<Cliente> clientes = controladorCliente.GetClientes();
            int opcao = 0, posicao = 0;
            bool flag = false;
            Console.WriteLine(clientes.First().CPF == null);
            if (clientes.First().CPF != null)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("============== Cliente ==============");

                    if (opcao == 0)
                    {
                        Console.WriteLine(clientes.ElementAt(posicao).ToString());
                    }
                    else if (opcao == 1)
                    {
                        if (posicao == clientes.Count - 1) posicao = clientes.Count - 1;
                        else posicao++;
                        Console.WriteLine(clientes.ElementAt(posicao));
                    }
                    else if (opcao == 2)
                    {
                        if (posicao == 0) posicao = 0;
                        else posicao--;
                        Console.WriteLine(clientes.ElementAt(posicao));
                    }
                    else if (opcao == 3)
                    {
                        posicao = 0;
                        Console.WriteLine(clientes.First());
                    }
                    else if (opcao == 4)
                    {
                        posicao = clientes.Count - 1;
                        Console.WriteLine(clientes.Last());
                    }

                    Console.WriteLine(@"
                                        1. Proximo 
                                        2. Anterior
                                        3. Primeiro
                                        4. Ultimo
                                        0. Voltar para menu anterior.");
                    do
                    {
                        flag = int.TryParse(Console.ReadLine(), out opcao);
                    } while (!flag);

                } while (opcao != 0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ainda nao tem nenhum cliente cadastrado");
                Console.WriteLine("Pressione enter para continuar...");
                Console.ReadKey();
            }
        }

        public static void SubMenuBloqueioCliente()
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
                    SubMenuBloqueioCliente();
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

        public static void SubMenuProcuraCliente()
        {
            Console.Clear();
            Console.WriteLine("- PROCURAR CLIENTE -");
            Console.Write("CPF do Cliente: ");
            string cpf = Console.ReadLine();

            while (!Validacoes.ValidarCpf(Cliente.FormataCPF(cpf)) && cpf != "0")
            {
                if (!Validacoes.ValidarCpf(Cliente.FormataCPF(cpf))) Console.WriteLine("CPF invalido! Digite novamente ou digite 0 para sair!");
                Console.Write("CPF do Cliente: ");
                cpf = Console.ReadLine();
            }

            var cliente = new ControladorCliente().GetCliente(cpf);
            if (cliente != null) Console.WriteLine(cliente);

            else Console.WriteLine("Nenhum cadastrado foi encontrado!");

            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }

        public static void SubMenuProcurarClienteBloqueado()
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
                if (cpf == "0") MenuCliente();
                else
                {
                    Console.ReadKey();
                    SubMenuProcurarClienteBloqueado();
                }
            }

            var cliente = controladorCliente.ProcurarBloqueado(Cliente.FormataCPF(cpf));
            if (cliente != null) Console.WriteLine(cliente); 

            else Console.WriteLine("Cliente bloqueado nao encontrado");
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
    }
}
