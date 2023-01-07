using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Models;

namespace Infrastructure.viewmodels
{
    public class CatalogViewModel
    {
        public IEnumerable<SelectListItem> Brand { get; set; }
        public IEnumerable<SelectListItem>Types { get; set; }
        public IEnumerable<catalogitemclass> catalogitems { get; set; }
        public Paginationinfo paginationinfo { get; set; }
        public int? BrandFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }
}
