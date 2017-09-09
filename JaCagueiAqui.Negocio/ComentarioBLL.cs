using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaCagueiAqui.Model;
using JaCagueiAqui.Data;

namespace JaCagueiAqui.Negocio
{
    public class ComentarioBLL
    {
        public List<Comentario> BuscaComentarios()
        {
            using (var db = new DataBase())
            {
                var lista = (from c in db.Comentarios
                             orderby c.ID descending
                             select c).Take(20).ToList();

                return lista;
            }
        }

        public void Salvar(Comentario comentario)
        {
            using (var db = new DataBase())
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
            }
        }
    }
}
