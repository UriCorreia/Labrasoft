using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {

        private static List<Bolsista> listaBolsistas = new List<Bolsista>();
        //{
        //    new Bolsista { Nome = "Ana Beatriz Silva", CPF = "123.456.789-00", Matricula = "2023001", DataNascimento = new DateTime(2002, 5, 15), Sexo = "F" },
        //    new Bolsista { Nome = "Carlos Eduardo Souza", CPF = "234.567.890-11", Matricula = "2023002", DataNascimento = new DateTime(2001, 10, 20), Sexo = "M" },
        //    new Bolsista { Nome = "Mariana Oliveira", CPF = "345.678.901-22", Matricula = "2023003", DataNascimento = new DateTime(2003, 3, 12), Sexo = "F" },
        //    new Bolsista { Nome = "Ricardo Pereira", CPF = "456.789.012-33", Matricula = "2023004", DataNascimento = new DateTime(2000, 12, 05), Sexo = "M" },
        //    new Bolsista { Nome = "Fernanda Costa", CPF = "567.890.123-44", Matricula = "2023005", DataNascimento = new DateTime(2004, 07, 28), Sexo = "F" },
        //    new Bolsista { Nome = "Gabriel Martins", CPF = "678.901.234-55", Matricula = "2023006", DataNascimento = new DateTime(1999, 01, 30), Sexo = "M" },
        //    new Bolsista { Nome = "Juliana Almeida", CPF = "789.012.345-66", Matricula = "2023007", DataNascimento = new DateTime(2002, 09, 14), Sexo = "F" },
        //    new Bolsista { Nome = "Lucas Henrique Lima", CPF = "890.123.456-77", Matricula = "2023008", DataNascimento = new DateTime(2001, 04, 02), Sexo = "M" }
        //};
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
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Bolsista novo = new Bolsista();
                novo.Nome = txtNome.Text;
                novo.Matricula = txtMatricula.Text;
                novo.CPF = txtCPF.Text;
                novo.Sexo = ddlSexo.SelectedValue;
                novo.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (listaBolsistas.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este bolsista já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; // Para a execução aqui
                }

                // 2. ADICIONAR NA LISTA ESTÁTICA
                listaBolsistas.Add(novo);

                // 3. Limpar os campos para o próximo cadastro
                LimparCampos();

                // 4. Mensagem de sucesso e atualizar visualização
                lblMensagem.Text = "Bolsista cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                // Chamar o método que atualiza o GridView (veremos abaixo)
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
            if (listaBolsistas.Count > 0)
            {
                // 1. Dizemos ao Grid qual é a fonte de dados (nossa lista)
                gridBolsistas.DataSource = listaBolsistas;

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

        protected void btnOrdenar_Click(object sender, EventArgs e)
        {
            listaBolsistas = listaBolsistas.OrderBy(x => x.Nome).ToList();
            AtualizarGrid();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            var filtrados = listaBolsistas.Where(x => x.Sexo == "F").ToList();
            gridBolsistas.DataSource = filtrados;
            gridBolsistas.DataBind();
        }

        protected void btnLimparFiltros_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }
    }
}
