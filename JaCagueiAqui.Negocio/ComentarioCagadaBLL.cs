using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaCagueiAqui.Model;
using JaCagueiAqui.Data;
using System.Data.Entity.Validation;

namespace JaCagueiAqui.Negocio
{
    public class ComentarioCagadaBLL
    {
        public void Salvar(ComentarioCagada comentarioCagada)
        {
            using (var db = new DataBase())
            {
                db.ComentariosCagadas.Add(comentarioCagada);

                var post = db.Posts.SingleOrDefault(x => x.ID == comentarioCagada.PostId);
                post.NumeroComentarios++;

                db.Posts.Attach(post);
                var entry = db.Entry(post);
                entry.Property(x => x.NumeroComentarios).IsModified = true;

                db.SaveChanges();
            }
        }

        public List<ComentarioCagada> RetornaComentarios(int postId)
        {
            using (var db = new DataBase())
            {
                var itens = (from c in db.ComentariosCagadas
                             where c.PostId == postId
                             select c).ToList();

                return itens;
            }
        }
    }
}
