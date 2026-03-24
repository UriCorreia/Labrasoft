using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class TesteBolsista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bolsista alunoTeste = new Bolsista();

            // 3. ATRIBUIR VALORES (SIMULANDO O PREENCHIMENTO DE UM FORMULÁRIO)
            alunoTeste.Nome = "José da Silva";
            alunoTeste.Matricula = "2024-001X";
            alunoTeste.CPF = "123.456.789-00";
            alunoTeste.Sexo = "M";
            alunoTeste.DataNascimento = new DateTime(2005, 05, 20); // 20 de Maio de 2005

            // 4. MOSTRAR OS RESULTADOS NA TELA DO NAVEGADOR
            Response.Write("<h2>--- Teste de Funcionamento (Semana 1) ---</h2>");

            // Chama o método que resume as informações
            Response.Write("<p><b>Resumo:</b> " + alunoTeste.ObterResumo() + "</p>");

            // Chama o método que calcula a idade
            Response.Write("<p><b>Idade Calculada:</b> " + alunoTeste.CalcularIdade() + " anos</p>");
        }
    }
}