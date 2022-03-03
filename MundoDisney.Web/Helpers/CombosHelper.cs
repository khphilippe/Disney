using Microsoft.AspNetCore.Mvc.Rendering;
using MundoDisney.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetComboGenero()
        {
            List<SelectListItem> list = _context.Generos.Select(g => new SelectListItem
            {
                Text = g.Nombre,
                Value = $"{g.GeneroId}"
            })

            .OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select your genero...]",
                Value = "0"
            });

            return list;
        }

    }
}
