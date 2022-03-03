using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MundoDisney.Web.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MundoDisney.Web.Models
{
    public class PeliculaViewModel : Pelicula
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public int GeneroId { get; set; }

        public IEnumerable<SelectListItem> Generos { get; set; }

    }
}
