using Microsoft.AspNetCore.Http;
using MundoDisney.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Data
{
    public class Personaje
    {
        [Key]
        public int PersonajeId { get; set; }

        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
           ? $"http://Pratique.somee.com/images/noimage.png"
            : $"https://onsalekevs.blob.core.windows.net/mundo/{ImageId}";
        
        [Required]
       public string Nombre { get; set; }
        [Required]

        public int Edad { get; set; }
        [Required]
        public int Peso { get; set; }
        public string Historia { get; set; }
        public ICollection<PersonajePelicula> PersonajePeliculas {get;set;}

    }
}