using MundoDisney.Web.Data;
using MundoDisney.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Helpers
{
   public interface IConverterHelper
    {
       Task<Pelicula> ToPelicula(PeliculaViewModel model, Guid imageId, bool isNew);
        PeliculaViewModel ToPeliculaViewModel(Pelicula pelicula);

        Personaje ToPersonaje(PersonajeViewModel model, Guid imageId, bool isNew);

        PersonajeViewModel ToPersonajeViewModel(Personaje personaje);
    }
}
