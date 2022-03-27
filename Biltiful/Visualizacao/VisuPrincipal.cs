using Biltiful.DataBase;
using ProjBiltiful.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biltiful.Visualizacao
{
    public static class VisuPrincipal
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
                Console.WriteLine("1. Cadastros Base");
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
                        VisuPrincipal.MenuCadastros();
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
            Console.ReadKey();
            do
            {
                Console.Clear();

                Console.WriteLine("=============== CADASTROS ===============");
                Console.WriteLine("1. Cliente");
                Console.WriteLine("2. Fornecedor");
                Console.WriteLine("3. Materia-Prima");
                Console.WriteLine("4. Produto");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        VisuCliente.MenuCliente();
                        break;

                    case "3":
                        VisuFornecedor.MenuFornecedor();
                        break;

                    case "4":
                        //VisuProduto.MenuProduto();
                        break;

                    case "5":
                        //VisuMateriaPrima.MenuMateriaPrima();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu");
                        break;
                }
            } while (escolha != "0");
        }

    }
}
