using System;

namespace WebApplication1.Models
{
    public class Bolsista
    {
        // EXERCÍCIO POO:
        // Com base no formulário que vocês criaram, definam as propriedades abaixo.
        // Lembrem-se de usar 'public', o tipo de dado (string, int, etc) e o { get; set; }

        public string Nome { get; set; }

        // TODO: Criar a propriedade para o CPF

        // TODO: Criar a propriedade para a Matrícula

        // TODO: Criar a propriedade para a Data de Nascimento

        // TODO: Criar a propriedade para o Sexo

        // TODO: Criar método com o resumo das informações contendo nome e matrícula

        //TODO: Criar método que calcúla a idade do bolsista

        public string CPF { get; set; }

        public string Matricula { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string ObterResumo()
        {
            return $"Bolsista: {Nome} (Matrícula: {Matricula})";
        }

        public int CalcularIdade()
        {
            int idade = DateTime.Now.Year - DataNascimento.Year;
            return idade;
        }

    }
}