using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Biltiful.DataBase
{
    public class DBHelper
    {

        public static string GetDBConnectionString()
        {
            var datasource = @"DESKTOP-BBVNUBS";
            var database = "Builtiful";
            var username = "sa";
            var password = "112233abc@";

            //sua string de conexão 
            string connString = @"Data Source=" + datasource +
                                ";Initial Catalog=" + database +
                                ";Persist Security Info=True" +
                                ";User ID=" + username +
                                ";Password=" + password;

            return connString;
        }

        public static void TesteConexao()
        {
            using (var sc = new SqlConnection(GetDBConnectionString()))
            {
                sc.Open();
                var state = sc.State;
                Console.WriteLine(state);
                sc.Close();
            }
        }
    }
}