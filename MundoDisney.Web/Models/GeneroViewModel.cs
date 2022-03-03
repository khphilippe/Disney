using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MundoDisney.Web.Data;
using System.ComponentModel.DataAnnotations;


namespace MundoDisney.Web.Models
{
    public class GeneroViewModel : Genero
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

       

    }
}
