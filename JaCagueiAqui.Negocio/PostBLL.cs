using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaCagueiAqui.Model;
using JaCagueiAqui.Data;

namespace JaCagueiAqui.Negocio
{
    public class PostBLL
    {
        public Post RetornaPost(int id)
        {
            using (var db = new DataBase())
            {
                return db.Posts.Where(x => x.ID == id).FirstOrDefault();
            }
        }

        public object ListaItens()
        {
            using (var db = new DataBase())
            {
                var post = (from c in db.Posts
                            select c).ToList();

                return post;
            }
        }

        public void Salvar(Post post)
        {
            using (var db = new DataBase())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public object RegistrarNaoGostei(int ID)
        {
            using (var db = new DataBase())
            {
                try
                {
                    var post = db.Posts.FirstOrDefault(x => x.ID == ID);
                    post.NaoGostei++;

                    db.Posts.Attach(post);
                    var entry = db.Entry(post);
                    entry.Property(x => x.NaoGostei).IsModified = true;
                    db.SaveChanges();

                    return new { total = post.NaoGostei };
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public object RegistrarGostei(int ID)
        {
            using (var db = new DataBase())
            {
                try
                {

                    var post = db.Posts.FirstOrDefault(x => x.ID == ID);
                    post.Gostei++;

                    db.Posts.Attach(post);
                    var entry = db.Entry(post);
                    entry.Property(x => x.Gostei).IsModified = true;
                    db.SaveChanges();

                    return new { total = post.Gostei };
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int RetornaTotalRegistros()
        {
            using (var db = new DataBase())
            {
                return (from c in db.Posts select c.ID).Count();
            }
        }

        public List<Post> RetornaMaisCurtidas(int tamanho, int pagina = 1)
        {
            using (var db = new DataBase())
            {
                return (from c in db.Posts
                        orderby c.Gostei descending
                        select c).Skip((pagina - 1) * tamanho).Take(tamanho).ToList();
            }
        }

        public List<Post> RetornaMaisComentadas(int tamanho, int pagina = 1)
        {
            using (var db = new DataBase())
            {
                return (from c in db.Posts
                        orderby c.NumeroComentarios descending
                        select c).Skip((pagina - 1) * tamanho).Take(tamanho).ToList();
            }
        }
    }
}
