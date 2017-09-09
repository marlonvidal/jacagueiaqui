using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaCagueiAqui.Model;
using JaCagueiAqui.Data;

namespace JaCagueiAqui.Negocio
{
    public class ContatoBLL
    {
        public void Salvar(Contato contato)
        {
            using (var db = new DataBase())
            {
                db.Contatos.Add(contato);
                db.SaveChanges();
            }
        }
    }
}
