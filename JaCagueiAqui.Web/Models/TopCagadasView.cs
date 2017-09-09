using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JaCagueiAqui.Model;

namespace JaCagueiAqui.Models
{
    public class TopCagadasView
    {
        public SessaoTopCagadasView sessaoAtiva { get; set; }
        public string nomeSessao { get; set; }

        public TopCagadasView(SessaoTopCagadasView sessaoAtiva, string nomeSessao)
        {
            this.sessaoAtiva = sessaoAtiva;
            this.nomeSessao = nomeSessao;
        }
    }
}