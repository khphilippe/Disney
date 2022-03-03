using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MundoDisney.Commonn.DTOs
{
   public class PersonajeDto
    {
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

    }
}
