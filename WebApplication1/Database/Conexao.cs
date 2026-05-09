using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication1.Database
{
    public class Conexao
    {
        private static string stringConexao = ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;

        public static SqlConnection ObterConexao()
        {
            SqlConnection conn = new SqlConnection(stringConexao);
            try
            {
                conn.Open();
                return conn;
            } catch(Exception e)
            {
                Console.WriteLine("Erro ao conectar com o banco de dados: " + e.Message);
                return null;
            }
        }
    }
}