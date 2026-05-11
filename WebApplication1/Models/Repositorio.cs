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
        public static List<Bolsista> listaBolsistas = new List<Bolsista>();

        public static List<Coordenador> listaCoordenadores = new List<Coordenador>();
        
        public static List<Projeto> listaProjetos = new List<Projeto>();
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