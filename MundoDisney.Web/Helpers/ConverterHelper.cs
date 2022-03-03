using MundoDisney.Web.Data;
using MundoDisney.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context,ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<Pelicula> ToPelicula(PeliculaViewModel model, Guid imageId, bool isNew)
        {
            return new Pelicula
            {
                Genero = await _context.Generos.FindAsync(model.GeneroId),
                PeliculaId = isNew ? 0 : model.PeliculaId,
                ImageId = imageId,
                Titulo = model.Titulo,
                Calificacions = model.Calificacions,
                Fecha = model.Fecha,
                PersonajePeliculas = model.PersonajePeliculas
            };
        }

        public PeliculaViewModel ToPeliculaViewModel(Pelicula pelicula)
        {
            return new PeliculaViewModel
            {
                Generos = _combosHelper.GetComboGenero(),
                PeliculaId = pelicula.PeliculaId,
                ImageId = pelicula.ImageId,
                Titulo = pelicula.Titulo,
                Fecha = pelicula.Fecha,
                Calificacions = pelicula.Calificacions,
                PersonajePeliculas = pelicula.PersonajePeliculas
            };
        }

        public  Personaje ToPersonaje(PersonajeViewModel model, Guid imageId, bool isNew)
        {
            return new Personaje
            {
                ImageId = model.ImageId,
                PersonajeId = model.PersonajeId,
                Nombre = model.Nombre,
                Edad = model.Edad,
                Historia = model.Historia,
                Peso =model.Peso,
               PersonajePeliculas = model.PersonajePeliculas
            };
        }

        public PersonajeViewModel ToPersonajeViewModel(Personaje personaje)
        {
            return new PersonajeViewModel
            {
                PersonajeId = personaje.PersonajeId,
                ImageId = personaje.ImageId,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Historia = personaje.Historia,
                PersonajePeliculas = personaje.PersonajePeliculas,
                Peso =personaje.Peso
                
            };
        }
    }
}
