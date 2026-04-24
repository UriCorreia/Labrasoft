using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Repositorio
    {
        public static List<Bolsista> listaBolsistas = new List<Bolsista>()
        {
            new Bolsista { Nome = "Ana Beatriz Silva", CPF = "123.456.789-00", Matricula = "2023001", DataNascimento = new DateTime(2002, 5, 15), Sexo = "F" },
            new Bolsista { Nome = "Carlos Eduardo Souza", CPF = "234.567.890-11", Matricula = "2023002", DataNascimento = new DateTime(2001, 10, 20), Sexo = "M" },
            new Bolsista { Nome = "Mariana Oliveira", CPF = "345.678.901-22", Matricula = "2023003", DataNascimento = new DateTime(2003, 3, 12), Sexo = "F" },
            new Bolsista { Nome = "Ricardo Pereira", CPF = "456.789.012-33", Matricula = "2023004", DataNascimento = new DateTime(2000, 12, 05), Sexo = "M" },
            new Bolsista { Nome = "Fernanda Costa", CPF = "567.890.123-44", Matricula = "2023005", DataNascimento = new DateTime(2004, 07, 28), Sexo = "F" },
            new Bolsista { Nome = "Gabriel Martins", CPF = "678.901.234-55", Matricula = "2023006", DataNascimento = new DateTime(1999, 01, 30), Sexo = "M" },
            new Bolsista { Nome = "Juliana Almeida", CPF = "789.012.345-66", Matricula = "2023007", DataNascimento = new DateTime(2002, 09, 14), Sexo = "F" },
            new Bolsista { Nome = "Lucas Henrique Lima", CPF = "890.123.456-77", Matricula = "2023008", DataNascimento = new DateTime(2001, 04, 02), Sexo = "M" }
        };

        public static List<Coordenador> listaCoordenadores = new List<Coordenador>()
        {
            new Coordenador { Nome = "Ricardo Aris", Titulo = "Doutor", CPF = "111.222.333-44", AreaAtuacao = "Engenharia de Software", Email = "ricardo.aris@universidade.edu" },
            new Coordenador { Nome = "Maria Oliveira", Titulo = "Doutora", CPF = "555.666.777-88", AreaAtuacao = "Inteligência Artificial", Email = "maria.ai@universidade.edu" },
            new Coordenador { Nome = "Carlos Santos", Titulo = "Mestre", CPF = "999.888.777-66", AreaAtuacao = "Banco de Dados", Email = "carlos.santos@universidade.edu" },
            new Coordenador { Nome = "Fernanda Lima", Titulo = "Doutora", CPF = "123.456.789-00", AreaAtuacao = "Sistemas Distribuídos", Email = "fernanda.lima@universidade.edu" },
            new Coordenador { Nome = "Roberto Souza", Titulo = "Mestre", CPF = "444.333.222-11", AreaAtuacao = "Segurança da Informação", Email = "roberto.seg@universidade.edu" }
        };
        public static List<Projeto> listaProjetos = new List<Projeto>()
        {
            new Projeto {
                Titulo = "LabIA - Inovação em Redes Neurais",
                AreaConhecimento = "Inteligência Artificial",
                VerbaAprovada = 50000.00f,
                BolsaIndividual = 700.00f,
                CoordenadorResponsavel = listaCoordenadores[1], // Maria Oliveira
                BolsistasVinculados = new List<Bolsista> { listaBolsistas[0], listaBolsistas[1] }
            },
            new Projeto {
                Titulo = "Labrasoft - Engenharia Ágil",
                AreaConhecimento = "Engenharia de Software",
                VerbaAprovada = 35000.00f,
                BolsaIndividual = 600.00f,
                CoordenadorResponsavel = listaCoordenadores[0], // Ricardo Aris
                BolsistasVinculados = new List<Bolsista> { listaBolsistas[2], listaBolsistas[3] }
            },
            new Projeto {
                Titulo = "SecureNet - Defesa de Perímetro",
                AreaConhecimento = "Segurança da Informação",
                VerbaAprovada = 20000.00f,
                BolsaIndividual = 500.00f,
                CoordenadorResponsavel = listaCoordenadores[4], // Roberto Souza
                BolsistasVinculados = new List<Bolsista> { listaBolsistas[4] }
            }
        };

        public static bool validarDisponibilidade(String CPFCoordenador, List<string> MatriculasBolsistas)
        {
            if (Repositorio.listaProjetos.Any(x => x.CoordenadorResponsavel.CPF == CPFCoordenador)) return false;
            foreach(var matricula in MatriculasBolsistas)
            {
                if (Repositorio.listaProjetos.Any(x => x.BolsistasVinculados.Any(y => y.Matricula == matricula))) return false;
            }
            return true;
        }
    }
}