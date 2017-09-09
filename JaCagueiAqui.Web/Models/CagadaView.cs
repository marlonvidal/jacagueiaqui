using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JaCagueiAqui.Model;

namespace JaCagueiAqui.Models
{
    public class CagadaView
    {
        public Post post { get; set; }
        public List<ComentarioCagada> comentariosCagada { get; set; }

        public CagadaView(Post post, List<ComentarioCagada> comentariosCagada)
        {
            this.post = post;
            this.comentariosCagada = comentariosCagada;
        }
    }
}