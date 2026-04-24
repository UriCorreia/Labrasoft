using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarComponents();
            }
        }
        private void carregarComponents()
        {
            var coordenadoresOcupados = Repositorio.listaProjetos
                                                        .Select(x => x.CoordenadorResponsavel.CPF).ToList();

            ddlCoordenadores.DataSource = Repositorio.listaCoordenadores.Where(x => !coordenadoresOcupados.Contains(x.CPF))
                                                                        .ToList();
            ddlCoordenadores.DataTextField = "Nome";
            ddlCoordenadores.DataValueField = "CPF";
            ddlCoordenadores.DataBind();
            ddlCoordenadores.Items.Insert(0, new ListItem("Selecione um Coordenador", "0"));

            var bolsistasOcupados = Repositorio.listaProjetos.SelectMany(x => x.BolsistasVinculados)
                                                             .Select(x => x.Matricula)
                                                             .ToList();

            cblBolsistas.DataSource = Repositorio.listaBolsistas.Where(x => !bolsistasOcupados.Contains(x.Matricula))
                                                                .ToList();
            cblBolsistas.DataTextField = "Nome";
            cblBolsistas.DataValueField = "Matricula";
            cblBolsistas.DataBind();

            AtualizarGrid();
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
                string cpfValidar = ddlCoordenadores.SelectedValue;
                List<string> mValidar = new List<string>();
                foreach(ListItem item in cblBolsistas.Items)
                {
                    if (item.Selected) mValidar.Add(item.Value);
                }

                bool disponivel = Repositorio.validarDisponibilidade(cpfValidar, mValidar);

                if (!disponivel)
                {
                    lblMensagem.Text = "⚠️ O Coordenador ou algum dos Bolsistas selecionados já estão alocados em algum Projeto!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    return;
                }

                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.AreaConhecimento = txtAreaConhecimento.Text;
                novo.VerbaAprovada = float.Parse(txtVerbaAprovada.Text);
                novo.BolsaIndividual = float.Parse(txtBolsaIndividual.Text);
                novo.CoordenadorResponsavel = Repositorio.listaCoordenadores.FirstOrDefault(c => c.CPF == ddlCoordenadores.SelectedValue);
                foreach (ListItem item in cblBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        Bolsista bolsistaSelecionado = Repositorio.listaBolsistas.FirstOrDefault(b => b.Matricula == item.Value);
                        if (bolsistaSelecionado != null)
                        {
                            novo.BolsistasVinculados.Add(bolsistaSelecionado);
                        }
                    }
                }

                Repositorio.listaProjetos.Add(novo);

                LimparCampos();

                carregarComponents();
                
                lblMensagem.Text = "Projeto cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao cadastrar. Verifique os dados!";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerbaAprovada.Text = "";
            txtBolsaIndividual.Text = "";
            txtPesquisa.Text = "";
            ddlCoordenadores.SelectedIndex = 0;
            foreach (ListItem item in cblBolsistas.Items)
            {
                item.Selected = false;
            }
            txtTitulo.Focus();
        }
        public void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        private void AtualizarGrid()
        {
            if(Repositorio.listaProjetos.Count > 0)
            {
                gridProjetos.DataSource = Repositorio.listaProjetos;
                gridProjetos.DataBind();

                lblAvisoGrid.Visible = false;
                pnlFilftros.Visible = true;
                gridProjetos.Visible = true;
            }
            else
            {
                lblAvisoGrid.Visible = true;
                pnlFilftros.Visible = false; 
                gridProjetos.Visible = false;
            }
        }
        protected void lbtnOrdenar_Click(object sender, EventArgs e)
        {
            Repositorio.listaProjetos = Repositorio.listaProjetos.OrderBy(x => x.Titulo).ToList();
            AtualizarGrid();
        }
        protected void lbtnLimparFiltro_Click(object sender, EventArgs e)
        {
            LimparCampos();

            AtualizarGrid();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }
        protected void lbtnPesquisa_Click(object sender, EventArgs e)
        {
            var pesquisado = txtPesquisa.Text.Trim();

            if (string.IsNullOrEmpty(pesquisado))
            {
                AtualizarGrid();
                return;
            }

            var encontrados = Repositorio.listaProjetos
                .Where(p => (p.Titulo.ToLower().Contains(pesquisado.ToLower())) ||
                            (p.CoordenadorResponsavel.Nome.ToLower().Contains(pesquisado.ToLower())))
                .ToList();

            gridProjetos.DataSource = encontrados;
            gridProjetos.DataBind();

            if (encontrados.Count == 0)
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
}