using Microsoft.AspNetCore.Http;
using MundoDisney.Web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Models
{
    public class PersonajeViewModel : Personaje
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
