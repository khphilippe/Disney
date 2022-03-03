using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MundoDisney.Commonn.DTOs
{
   public class PeliculaDto
    {
        public int PeliculaId { get; set; }

        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
             ? $"http://Pratique.somee.com/images/noimage.png"
            : $"https://onsalekevs.blob.core.windows.net/mundo/{ImageId}";

        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [Display(Name = "Date")]
        public DateTime DateLocal => Fecha.ToLocalTime();

        
       // public int Calification { get; set; }

    }
}
