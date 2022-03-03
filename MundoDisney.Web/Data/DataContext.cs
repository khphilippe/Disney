using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Web.Data.Entities;

namespace MundoDisney.Web.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Personaje> Personajes { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<PersonajePelicula> PersonajePeliculas { get; set; }

       
      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonajePelicula>().HasKey(pp => new

            { pp.PeliculaId, pp.PersonajeId });



            modelBuilder.Entity<PersonajePelicula>().HasKey(pp => new { pp.PeliculaId, pp.PersonajeId });

            modelBuilder.Entity<PersonajePelicula>()
                .HasOne<Pelicula>(pp => pp.Pelicula)
                .WithMany(p => p.PersonajePeliculas)
                .HasForeignKey(pp => pp.PeliculaId);


            modelBuilder.Entity<PersonajePelicula>()
                .HasOne<Personaje>(pp => pp.Personaje)
                .WithMany(p => p.PersonajePeliculas)
                .HasForeignKey(pp => pp.PersonajeId);



        } */

        }
}

