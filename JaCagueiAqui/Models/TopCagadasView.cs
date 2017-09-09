using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JaCagueiAqui.Model;

namespace JaCagueiAqui.Models
{
    public class TopCagadasView
    {
        public SessaoTopCagadasView maisCurtidos { get; set; }
        public SessaoTopCagadasView maisComentados { get; set; }

        public TopCagadasView(SessaoTopCagadasView maisCurtidos, SessaoTopCagadasView maisComentados)
        {
            this.maisComentados = maisComentados;
            this.maisCurtidos = maisCurtidos;
        }
    }
}