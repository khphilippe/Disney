using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MundoDisney.Commonn.Responses
{
  public  class PeliculaResponse
    {

        public Guid ImageId { get; set; }

      
        public string ImageFullPath => ImageId == Guid.Empty
             ? $"http://Pratique.somee.com/images/noimage.png"
            : $"https://onsalekevs.blob.core.windows.net/mundo/{ImageId}";
        
        public string Titulo { get; set; }


        public DateTime Fecha { get; set; }

  

       
    }
}

