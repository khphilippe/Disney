using Microsoft.AspNetCore.Http;
using MundoDisney.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MundoDisney.Web.Data
{
    public class Pelicula
    {
        public int PeliculaId { get; set; }

         public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
             ? $"http://Pratique.somee.com/images/noimage.png"
            : $"https://onsalekevs.blob.core.windows.net/mundo/{ImageId}";

        [Required]
        public string Titulo { get; set; }

        //[Required]
        public DateTime Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date")]
        public DateTime DateLocal => Fecha.ToLocalTime();
        
        
        [Range(0,5,ErrorMessage ="La calificacion debe estar entre 1 al 5")]
        public int Calificacions { get; set; }

        public ICollection<PersonajePelicula> PersonajePeliculas {get;set;}
        public Genero Genero { get; set; }

    }
}
