using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Projeto
    {
        public string Nome { get; set;  }
        public float VerbaAprovada { get; set; }
        public float BolsaIndividual { get; set; }
        public Coordenador CoordenadorResponsavel { get; set; }
        public List<Bolsista> BolsistasVinculados { get; set; }

        public Projeto()
        {
            BolsistasVinculados = new List<Bolsista>();
        }
    }
}