using System;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciar a classe
                Bolsista aluno = new Bolsista();

                // 2. Mapear a TELA para o OBJETO
                aluno.Nome = txtNome.Text;
                aluno.Matricula = txtMatricula.Text;
                aluno.CPF = txtCPF.Text;
                aluno.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                // 3. Executar a lógica que você já criou na Semana 1
                string resumo = aluno.ObterResumo();
                int idade = aluno.CalcularIdade();

                // 4. Mostrar o resultado na tela
                lblMensagem.Text = $"Sucesso! {resumo}. Idade: {idade} anos.";
                lblMensagem.ForeColor = System.Drawing.Color.DarkGreen;
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro: Verifique se a data de nascimento foi preenchida.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
