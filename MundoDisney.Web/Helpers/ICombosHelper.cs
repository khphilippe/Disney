using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MundoDisney.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboGenero();
    }
}
