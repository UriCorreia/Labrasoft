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
    public class CoordenadorDAO
    {
        public bool Salvar(Coordenador coordenador)
        {
            try
            {
                using (SqlConnection conn = Conexao.ObterConexao())
                {
                    string sql = @"insert into Coordenador(nome, titulo, cpf, areaAtuacao, email)
                               values(@nome, @titulo, @cpf, @areaAtuacao, @email)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@nome", coordenador.Nome);
                    cmd.Parameters.AddWithValue("@titulo", coordenador.Titulo);
                    cmd.Parameters.AddWithValue("@cpf", coordenador.CPF);
                    cmd.Parameters.AddWithValue("@areaAtuacao", coordenador.AreaAtuacao);
                    cmd.Parameters.AddWithValue("@email", coordenador.Email);

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
                if (conn == null) return null;

                string sql = "select id, nome, titulo, cpf, areaAtuacao, email from Coordenador";
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
                string sql = "select id, nome, titulo, cpf, areaAtuacao, email from Coodernador order by nome";
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

                string sql = "select id, nome, titulo, cpf, areaAtuacao, email from Coordenador where nome like @pesquisa";

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