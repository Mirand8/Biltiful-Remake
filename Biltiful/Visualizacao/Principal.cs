using Biltiful.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biltiful.Visualizacao
{
    public static class Principal
    {


        public static void MenuPrincipal()
        {

            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== MENU ===============");
                Console.WriteLine("1. Cadastros");
                Console.WriteLine("2. Produção");
                Console.WriteLine("3. Compras");
                Console.WriteLine("4. Vendas");
                Console.WriteLine("====================================");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        Environment.Exit(0);
                        break;

                    case "1":
                        //MenuCadastros.SubMenu();
                        break;

                    case "2":
                        //new Producao().SubMenu();
                        break;

                    case "3":
                        //Compra.SubMenu();
                        break;

                    case "4":

                        //MenuVendas.SubMenu();

                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                        break;
                }

            } while (escolha != "0");
        }

        public static void MenuCadastros()
        {
            string escolha;
            Console.WriteLine("Teste do banco = ");
            DBHelper.TesteConexao();
            Console.WriteLine("Esta tudo certo com o banco!");
            Console.ReadKey();
            do
            {
                Console.Clear();

                Console.WriteLine("=============== CADASTROS ===============");
                Console.WriteLine("1. Clientes / Fornecedores");
                Console.WriteLine("2. Produtos");
                Console.WriteLine("3. Matérias-Primas");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        //SubMenuClientesFornecedores();
                        break;

                    case "2":
                        //new Produto().Menu();
                        break;


                    case "3":
                        //new MPrima().Menu();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu");
                        break;
                }

            } while (escolha != "0");
        }

        //public static void MenuCompra()
        //{
        //    int option = -1;
        //    while (option != 0)
        //    {
        //        Console.Clear();

        //        Console.WriteLine("=============== COMPRAS ===============");
        //        Console.WriteLine("1. Nova Compra");
        //        Console.WriteLine("2. Consultar Compra");
        //        Console.WriteLine("3. Imprimir Registros de Compra");
        //        Console.WriteLine("--------------------------------------");
        //        Console.WriteLine("0. Voltar");
        //        Console.Write("\nEscolha: ");

        //        option = int.Parse(Console.ReadLine());
        //        switch (option)
        //        {
        //            // ---------- CADASTRAR COMPRA -----------
        //            case 1:
        //                if (new Read().VerificaListaFornecedor())
        //                    CadastraNovaCompra();
        //                else
        //                {
        //                    Console.WriteLine("Para realizar uma compra de materias primas devera ter o registro de ao menos um fornecedor.");
        //                    Console.ReadKey();
        //                }
        //                break;

        //            // ---------- LOCALIZAR COMPRA -----------
        //            case 2:
        //                Console.WriteLine("\nLocalizar Compra\n");
        //                int okl;
        //                int id;
        //                do
        //                {
        //                    Console.Write("Id da Compra: ");
        //                    id = int.Parse(Console.ReadLine());
        //                    okl = (id > 0 && id < 99999) && Compra.Localizar(id) != null ? 0 : 1;
        //                } while (okl != 0);
        //                if (okl == 2) break;
        //                Compra.Localizar(id).ImprimirCompra();
        //                break;

        //            // ---------- IMPRESSÃO POR REGISTRO -----------
        //            case 3:
        //                ImpressaoPorRegistro(new ManipulaArquivosCompraMP().PegarTodasAsCompras());
        //                break;
        //        }
        //        Console.ReadKey();
        //        Console.Clear();
        //    }
        //}

        public static void sep()
        {

        }

        //public static void MenuProducao()
        //{
        //    string escolha;

        //    do
        //    {

        //        Console.Clear();
        //        Console.WriteLine("\n=============== PRODUÇÃO ===============");
        //        Console.WriteLine("1. Cadastrar uma produção");
        //        Console.WriteLine("2. Localizar um registro");
        //        Console.WriteLine("3. Imprimir por registro");
        //        Console.WriteLine("---------------------------------------");
        //        Console.WriteLine("0. Voltar ao menu anterior");
        //        Console.Write("\nEscolha: ");

        //        switch (escolha = Console.ReadLine())
        //        {
        //            case "0":
        //                break;
        //            case "1":
        //                string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
        //                if (File.Exists(caminhoFinal + "\\Cosmetico.dat") && File.Exists(caminhoFinal + "\\Materia.dat"))
        //                {
        //                    Console.Clear();
        //                    Cadastrar();
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Não ha produtos ou materias primas cadastradas. Favor verificar!");
        //                    Console.ReadKey();
        //                }
        //                break;
        //            case "2":
        //                Console.Clear();
        //                Localizar();
        //                break;
        //            case "3":
        //                ImprimirPorRegistro();
        //                Console.Clear();
        //                break;
        //            default:
        //                Console.WriteLine("\n Opção inválida.");
        //                Console.WriteLine("\n Pressione ENTER para voltar ao menu.");
        //                Console.ReadKey();
        //                break;
        //        }

        //    } while (escolha != "0");
        //}
    }
}
