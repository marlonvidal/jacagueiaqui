using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JaCagueiAqui.Models
{
    public class PagingInfo
    {
        public PagingInfo(int paginaAtual, int total, int tamanhoPagina)
        {
            this.paginaAtual = paginaAtual;
            this.total = total;
            this.tamanhoPagina = tamanhoPagina;
        }

        public int paginaAtual { get; set; }
        public int total { get; set; }
        public int tamanhoPagina { get; set; }

        public int totalPaginas
        {
            get { return (int)Math.Ceiling((decimal)total / tamanhoPagina); }
        }
    }
}