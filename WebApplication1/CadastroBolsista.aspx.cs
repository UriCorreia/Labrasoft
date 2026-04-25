using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
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
            // VERIFICAÇÃO DE SEGURANÇA: Se campos básicos estiverem vazios, para aqui.
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

                if (Repositorio.listaBolsistas.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este bolsista já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; 
                }

                Repositorio.listaBolsistas.Add(novo);

                LimparCampos();

                lblMensagem.Text = "Bolsista cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "esconderMensagem();", true);

                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao cadastrar. Verifique os dados.";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            // Aproveite para limpar a mensagem de erro/sucesso também
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
            txtNome.Focus(); // Coloca o cursor de volta no Nome
        }

        private void AtualizarGrid()
        {
            if (Repositorio.listaBolsistas.Count > 0)
            {
                // 1. Dizemos ao Grid qual é a fonte de dados (nossa lista)
                gridBolsistas.DataSource = Repositorio.listaBolsistas;

                // 2. O DataBind() "desenha" as linhas da tabela no HTML
                gridBolsistas.DataBind();

                lblAvisoGrid.Visible = false;
                pnlFilftros.Visible = true;
                gridBolsistas.Visible = true;
            }
            else
            {
                lblAvisoGrid.Visible = true;
                pnlFilftros.Visible = false;
                gridBolsistas.Visible = false;
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
            Repositorio.listaBolsistas = Repositorio.listaBolsistas.OrderBy(x => x.Nome).ToList();
            AtualizarGrid();
        }

        protected void lbtnFiltrar_Click(object sender, EventArgs e)
        {
            var filtrados = Repositorio.listaBolsistas.Where(x => x.Sexo == "F").ToList();

            gridBolsistas.DataSource = filtrados;
            gridBolsistas.DataBind();

            if (filtrados.Count == 0)
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

            var encontrados = Repositorio.listaBolsistas
                .Where(x => (x.Nome != null && x.Nome.IndexOf(pesquisado, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (x.Matricula != null && x.Matricula.IndexOf(pesquisado, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            gridBolsistas.DataSource = encontrados;
            gridBolsistas.DataBind();

            if(encontrados.Count == 0)
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
