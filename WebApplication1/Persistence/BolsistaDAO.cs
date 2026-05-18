using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Persistence
{
    public class BolsistaDAO
    {

        public bool Salvar(Bolsista bolsista)
        {
            try
            {
                using (SqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = @"insert into Bolsista(nome, cpf, matricula, dataNascimento, sexo)
                                   values (@nome, @cpf, @matricula, @dataNascimento, @sexo)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@nome", bolsista.Nome);
                    cmd.Parameters.AddWithValue("@cpf", bolsista.CPF);
                    cmd.Parameters.AddWithValue("@matricula", bolsista.Matricula);
                    cmd.Parameters.AddWithValue("@dataNascimento", bolsista.DataNascimento);
                    cmd.Parameters.AddWithValue("@sexo", bolsista.Sexo);

                    return cmd.ExecuteNonQuery() > 0;
                }
            } catch (Exception)
            {
                return false;
            }
        }

        public DataTable Listar()
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {
                string sql = "select id, nome, cpf, matricula, dataNascimento, sexo from Bolsista";
                SqlCommand cmd = new SqlCommand(sql, conn);

                DataTable dt = new DataTable();

                try
                {
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        public DataTable Ordenar()
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {
                string sql = "select id, nome, cpf, matricula, dataNascimento, sexo from Bolsista order by nome";
                SqlCommand cmd = new SqlCommand(sql, conn);

                DataTable dt = new DataTable();

                try
                {
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                } catch (Exception)
                {
                    return null;
                }
            }
        }

        public DataTable FMulheres()
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {
                string sql = "select id, nome, matricula, dataNascimento, Sexo from Bolsista where sexo like 'F' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                DataTable dt = new DataTable();

                try
                {
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public DataTable Pesquisa(string pesquisa)
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {

                if (conn == null) return null;

                string sql = "select id, nome, matricula, dataNascimento, sexo from Bolsista where nome like @pesquisa";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pesquisa", "%" + pesquisa + "%");
                    DataTable dt = new DataTable();

                    try
                    {
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
    }
}