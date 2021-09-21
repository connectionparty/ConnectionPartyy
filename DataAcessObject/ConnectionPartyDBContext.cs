using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace DataAcessObject
{
    public class ConnectionPartyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C: \Users\Caio Fabeni\Desktop\ConnectionParty - d333d814a75248c92f13eae19401980be2e88c8f\ConnectionParty2.mdf;Integrated Security=True;Connect Timeout=30");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
