using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Persistence;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // Na primeira vez que a página carrega, podemos querer exibir a lista 
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtTitulo.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtAreaAtuacao.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {
                Coordenador novo = new Coordenador();
                novo.Nome = txtNome.Text;
                novo.Titulo = txtTitulo.Text;
                novo.CPF = txtCPF.Text;
                novo.AreaAtuacao = txtAreaAtuacao.Text;
                novo.Email = txtEmail.Text;

                CoordenadorDAO dao = new CoordenadorDAO();

                if (dao.Salvar(novo))
                {
                    lblMensagem.Text = "Coordenador cadastrado com Sucesso!";

                    LimparCampos();
                    AtualizarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "esconderMensagem();", true);
                }
                else
                {
                    lblMensagem.Text = "Erro ao cadastrar. Verifique os dados.";
                    lblMensagem.CssClass = "alert alert-danger d-block";
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "⚠️ Ocorreu um erro inesperado: " + ex.Message;
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtTitulo.Text = "";
            txtCPF.Text = "";
            txtAreaAtuacao.Text = "";
            txtEmail.Text = "";
            txtNome.Focus(); 
        }

        private void AtualizarGrid()
        {
            CoordenadorDAO dao = new CoordenadorDAO();
            DataTable dt = dao.Listar();

            if(dt != null)
            {
                gridCoordenadores.DataSource = dt;
                gridCoordenadores.DataBind();
            }
        }

        protected void gridCoordenadores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-nowrap";
                }
            }
        }

        protected void lbtnOrdenar_Click(object sender, EventArgs e)
        {
            Repositorio.listaCoordenadores = Repositorio.listaCoordenadores.OrderBy(x => x.Nome).ToList();
            AtualizarGrid();
        }
            
        protected void lbtnLimparFiltros_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";

            AtualizarGrid();
        }
        protected void lbtnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisado = txtPesquisa.Text;

            if (string.IsNullOrEmpty(pesquisado))
            {
                AtualizarGrid();
                return;
            }

            var encontrados = Repositorio.listaCoordenadores
                .Where(x => (x.Nome != null && x.Nome.IndexOf(pesquisado, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (x.Titulo != null && x.Titulo.IndexOf(pesquisado, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            gridCoordenadores.DataSource = encontrados;
            gridCoordenadores.DataBind();

            if (encontrados.Count == 0)
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
}