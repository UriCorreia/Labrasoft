using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Database;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Persistence;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarComponents();
                AtualizarGrid();
            }
        }
        private void carregarComponents()
        {
            using (SqlConnection conn = Conexao.ObterConexao())
            {
                if (conn != null)
                {
                    string sqlCoordenador = @"select id, nome from Coordenador
                                              where id not in (select idCoordenador from Projeto)";

                    SqlCommand cmdCoordenador = new SqlCommand(sqlCoordenador, conn);

                    DataTable dtCoord = new DataTable();
                    dtCoord.Load(cmdCoordenador.ExecuteReader()); // Lê os dados e já libera o reader para outra consulta

                    ddlCoordenadores.DataSource = dtCoord;
                    ddlCoordenadores.DataTextField = "nome";
                    ddlCoordenadores.DataValueField = "id";
                    ddlCoordenadores.DataBind();
                    ddlCoordenadores.Items.Insert(0, new ListItem("Selecione um Coordenador", "0"));

                    cmdCoordenador.Dispose();

                    string sqlBolsistas = @"select id, nome from Bolsista
                                            where id not in (select idBolsista from ProjetoBolsista)";

                    SqlCommand cmdBolsistas = new SqlCommand(sqlBolsistas, conn);

                    DataTable dtBols = new DataTable();
                    dtBols.Load(cmdBolsistas.ExecuteReader());

                    cblBolsistas.DataSource = dtBols;
                    cblBolsistas.DataTextField = "nome";
                    cblBolsistas.DataValueField = "id";
                    cblBolsistas.DataBind();
                }
            }
        }

        public void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
            string.IsNullOrWhiteSpace(txtAreaConhecimento.Text) ||
            string.IsNullOrWhiteSpace(txtVerbaAprovada.Text) ||
            string.IsNullOrWhiteSpace(txtBolsaIndividual.Text) ||
            ddlCoordenadores.SelectedValue == "0")
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {

                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.AreaConhecimento = txtAreaConhecimento.Text;
                novo.VerbaAprovada = decimal.Parse(txtVerbaAprovada.Text);
                novo.BolsaIndividual = decimal.Parse(txtBolsaIndividual.Text);
                novo.CoordenadorResponsavel = new Coordenador
                {
                    Id = int.Parse(ddlCoordenadores.SelectedValue)
                };

                List<int> idsBolsistas = new List<int>();
                foreach (ListItem item in cblBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        idsBolsistas.Add(int.Parse(item.Value));
                    }
                }

                ProjetoDAO dao = new ProjetoDAO();
                if (dao.Salvar(novo, idsBolsistas))
                {
                    lblMensagem.Text = "Projeto cadastrado com sucesso!";
                    lblMensagem.CssClass = "alert alert-success d-block";

                    LimparCampos();
                    carregarComponents();
                    AtualizarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "esconderMensagem();", true);
                }
                else
                {
                    lblMensagem.Text = "Erro ao salvar o projeto verifique os dados.";
                    lblMensagem.CssClass = "alert alert-danger d-block";
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "⚠️ Ocorreu um erro inesperado: " + ex.Message;
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        
        private void AtualizarGrid()
        {

            ProjetoDAO dao = new ProjetoDAO();
            DataTable dt = dao.Listar();

            if (dt != null)
            {
                gridProjetos.DataSource = dt;
                gridProjetos.DataBind();

                if (dt.Rows.Count == 0) lblAvisoGrid.Text = "Nenhum projeto cadastrado.";

                gridProjetos.Visible = true;
                lblAvisoGrid.Visible = (dt.Rows.Count == 0);
                pnlFilftros.Visible = true;
            }
        }

        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                int idProjeto = Convert.ToInt32(e.CommandArgument);
                ProjetoDAO dao = new ProjetoDAO();
                DataTable dt = dao.BuscarDetalhes(idProjeto);

                if(dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblDetTitulo.Text = row["Titulo"].ToString();
                    lblDetArea.Text = row["AreaConhecimento"].ToString();
                    lblDetCoordenador.Text = row["Coordenador"].ToString();
                    lblDetVerba.Text = Convert.ToDecimal(row["VerbaAprovada"]).ToString("C");
                    lblDetBolsa.Text = Convert.ToDecimal(row["BolsaIndividual"]).ToString("C");
                    
                    bltBolsistasDet.Items.Clear();
                    string listaBolsistas = row["Bolsistas"].ToString();

                    if (!String.IsNullOrEmpty(listaBolsistas))
                    {
                        foreach (var nome in listaBolsistas.Split(','))
                        {
                            bltBolsistasDet.Items.Add(new ListItem(nome.Trim()));
                        }
                    }
                    else
                    {
                        bltBolsistasDet.Items.Add(new ListItem("Nenhum bolsista associado"));
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDetalhes", "$('#modalDetalhes').modal('show');", true);
                }
            }
        }

        protected void lbtnOrdenar_Click(object sender, EventArgs e)
        {
            ProjetoDAO dao = new ProjetoDAO();
            DataTable dt = dao.Ordenar();

            if(dt != null)
            {
                gridProjetos.DataSource = dt;
                gridProjetos.DataBind();

                if(dt.Rows.Count == 0)
                {
                    lblAvisoGrid.Text = "Nenhum Coordenador encontrado!";
                    lblAvisoGrid.Visible = true;
                    lblMensagem.Visible = false;
                }
                else
                {
                    lblAvisoGrid.Visible = false;
                    lblMensagem.Visible = false;
                }
            }
        }

        protected void lbtnPesquisa_Click(object sender, EventArgs e)
        {
            var pesquisado = txtPesquisa.Text.Trim();

            if (string.IsNullOrEmpty(pesquisado))
            {
                AtualizarGrid();
                return;
            }

            ProjetoDAO dao = new ProjetoDAO();
            DataTable dt = dao.Pesquisa(pesquisado);

            if(dt != null)
            {
                gridProjetos.DataSource = dt;
                gridProjetos.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblAvisoGrid.Text = "Nenhum Projeto encontrado com esse título!";
                    lblAvisoGrid.Visible = true;
                    gridProjetos.Visible = false;
                }
                else
                {
                    lblAvisoGrid.Visible = false;
                    gridProjetos.Visible = true;
                }
            }
        }
        protected void lbtnLimparFiltro_Click(object sender, EventArgs e)
        {
            LimparCampos();

            AtualizarGrid();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        public void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerbaAprovada.Text = "";
            txtBolsaIndividual.Text = "";
            txtPesquisa.Text = "";

            ddlCoordenadores.ClearSelection();

            if (ddlCoordenadores.Items.Count > 0)
            {
                ddlCoordenadores.SelectedIndex = 0;
            }

            foreach (ListItem item in cblBolsistas.Items)
            {
                item.Selected = false;
            }
            txtTitulo.Focus();
        }
        
    }
}