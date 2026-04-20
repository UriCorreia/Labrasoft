using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models; 

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
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Coordenador novo = new Coordenador();
                novo.Nome = txtNome.Text;
                novo.Titulo = txtTitulo.Text;
                novo.CPF = txtCPF.Text;
                novo.AreaAtuacao = txtAreaAtuacao.Text;
                novo.Email = txtEmail.Text;

                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (Repositorio.listaCoordenadores.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este Coordenador já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; 
                }

                Repositorio.listaCoordenadores.Add(novo);

                LimparCampos();

                lblMensagem.Text = "Coordenador cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

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
            txtTitulo.Text = "";
            txtCPF.Text = "";
            txtAreaAtuacao.Text = "";
            txtEmail.Text = "";
            txtNome.Focus(); 
        }

        private void AtualizarGrid()
        {
            if (Repositorio.listaCoordenadores.Count > 0)
            {
                // 1. Dizemos ao Grid qual é a fonte de dados (nossa lista)
                gridBolsistas.DataSource = Repositorio.listaCoordenadores;

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
                .Where(x => x.Nome != null && x.Nome.IndexOf(pesquisado, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            gridBolsistas.DataSource = encontrados;
            gridBolsistas.DataBind();

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