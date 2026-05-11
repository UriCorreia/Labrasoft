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
    public class ProjetoDAO
    {

        public bool Salvar(Projeto projeto, List<int> idsBolsistas)
        {
            using(SqlConnection conn = Conexao.ObterConexao())
            {
                if (conn == null) return false;
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string sql = @"insert into Projeto(titulo, areaConhecimento, verbaAprovada, bolsaIndividual, idCoordenador)
                                   values(@titulo, @areaConhecimento, @verbaAprovada, @bolsaIndividual, @idCoordenador)
                                   select scope_identity();";

                    SqlCommand cmd = new SqlCommand(sql, conn, trans);
                    cmd.Parameters.AddWithValue("@titulo", projeto.Titulo);
                    cmd.Parameters.AddWithValue("@areaConhecimento", projeto.AreaConhecimento);
                    cmd.Parameters.AddWithValue("@verbaAprovada", projeto.VerbaAprovada);
                    cmd.Parameters.AddWithValue("@bolsaIndividual", projeto.BolsaIndividual);
                    cmd.Parameters.AddWithValue("@idCoordenador", projeto.CoordenadorResponsavel.Id);

                    int idNovo = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach(int idBolsista in idsBolsistas)
                    {
                        string sqlVinculo = @"insert into ProjetoBolsista(idProjeto, idBolsista)
                                              values(@idProjeto, @idBolsista)";
                        SqlCommand cmdVinculo = new SqlCommand(sqlVinculo, conn, trans);
                        cmdVinculo.Parameters.AddWithValue("@idProjeto", idNovo);
                        cmdVinculo.Parameters.AddWithValue("@idBolsista", idBolsista);
                        cmdVinculo.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        public DataTable Listar()
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {
                if(conn == null) return null;

                string sql = @"select p.id, p.titulo, p.areaConhecimento, p.verbaAprovada, c.nome as nomeCoordenador
                               from Projeto p
                               inner join Coordenador c on p.idCoordenador = c.id";
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
    }
}