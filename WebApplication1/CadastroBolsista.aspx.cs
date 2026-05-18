using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Persistence;

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtMatricula.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtDataNasc.Text) ||
            ddlSexo.SelectedIndex <= 0)
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {
                Bolsista novo = new Bolsista();
                novo.Nome = txtNome.Text;
                novo.Matricula = txtMatricula.Text;
                novo.CPF = txtCPF.Text;
                novo.Sexo = ddlSexo.SelectedValue;
                novo.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                BolsistaDAO dao = new BolsistaDAO();

                if (dao.Salvar(novo))
                {

                    lblMensagem.Text = "Bolsista cadastrado com sucesso!";
                    lblMensagem.CssClass = "alert alert-success d-block";

                    LimparCampos();
                    AtualizarGrid();

                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "esconderMensagem();", true);

                }
                else
                {
                    lblMensagem.Text = "Erro ao cadastrar. Por favor, verifique os dados!";
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
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtDataNasc.Text = "";
            ddlSexo.SelectedIndex = 0;
            txtNome.Focus(); 
        }

        private void AtualizarGrid()
        {
            BolsistaDAO dao = new BolsistaDAO();
            DataTable dt = dao.Listar();

            if(dt != null)
            {
                gridBolsistas.DataSource = dt;
                gridBolsistas.DataBind();

                pnlFilftros.Visible = dt.Rows.Count > 0;
            }
        }

        protected void gridBolsistas_RowDataBound(object sender, GridViewRowEventArgs e)
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
            BolsistaDAO dao = new BolsistaDAO();
            DataTable dt = dao.Ordenar();

            if (dt != null)
            {
                gridBolsistas.DataSource = dt;
                gridBolsistas.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblAvisoGrid.Text = "Nenhum Bolsista encontrado!";
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

        protected void lbtnFiltrar_Click(object sender, EventArgs e)
        {
            BolsistaDAO dao = new BolsistaDAO();
            DataTable dt = dao.FMulheres();

            if(dt != null)
            {
                gridBolsistas.DataSource = dt;
                gridBolsistas.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblAvisoGrid.Text = "Nenhuma Bolsista encontrada!";
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

        protected void lbtnLimparFiltros_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";

            AtualizarGrid();
        }
        protected void lbtnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisado = txtPesquisa.Text.Trim();

            if (string.IsNullOrEmpty(pesquisado))
            {
                AtualizarGrid();
                return;
            }

            BolsistaDAO dao = new BolsistaDAO();

            DataTable dt = dao.Pesquisa(pesquisado);

            if (dt != null)
            {
                gridBolsistas.DataSource = dt;
                gridBolsistas.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblAvisoGrid.Text = "Nenhum Bolsista encontrado!";
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
}
