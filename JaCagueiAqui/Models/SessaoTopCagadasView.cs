using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JaCagueiAqui.Model;

namespace JaCagueiAqui.Models
{
    public class SessaoTopCagadasView
    {
        public string idDiv { get; set; }
        public string nomeAction { get; set; }
        public List<Post> listaCagadas { get; set; }
        public int paginaAnterior { get; set; }
        public int paginaSeguinte { get; set; }
        public int paginaAtual { get; set; }
        public int totalPaginas { get; set; }

        public SessaoTopCagadasView(List<Post> listaCagadas, string idDiv, string nomeAction, int totalPaginas, int paginaAtual = 1, int paginaAnterior = 0, int paginaSeguinte = 0)
        {
            this.listaCagadas = listaCagadas;
            this.idDiv = idDiv;
            this.nomeAction = nomeAction;
            this.totalPaginas = totalPaginas;
            this.paginaAtual = paginaAtual;
            this.paginaAnterior = paginaAnterior;
            this.paginaSeguinte = paginaSeguinte;
        }
    }
}