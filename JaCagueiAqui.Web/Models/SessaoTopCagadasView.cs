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
        public PagingInfo pagingInfo { get; set; }

        public SessaoTopCagadasView(List<Post> listaCagadas, string idDiv, string nomeAction, PagingInfo pagingInfo)
        {
            this.listaCagadas = listaCagadas;
            this.idDiv = idDiv;
            this.nomeAction = nomeAction;
            this.pagingInfo = pagingInfo;
        }
    }
}