using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Data.Entities
{
    public class PersonajePelicula
    {
        [Key]
        public int PersonajePeliculaId { get; set; }
        public Personaje Personaje { get; set; }
        public Pelicula Pelicula { get; set; }
        public int PersonajeId { get; set; }
        public int PeliculaId { get; set; }
        
    }
}
