using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using JaCagueiAqui.Model;

namespace JaCagueiAqui.Data
{
    //public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<DataBase>
    public class DataBaseInitializer : DropCreateDatabaseAlways<DataBase>
    {
        protected override void Seed(DataBase context)
        {
            base.Seed(context);
        }
    }

    public class DataBase : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<ComentarioCagada> ComentariosCagadas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new DataBaseInitializer());

            modelBuilder.Entity<Post>().Property(x => x.Lat).HasPrecision(11, 8);
            modelBuilder.Entity<Post>().Property(x => x.Long).HasPrecision(11, 8);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
