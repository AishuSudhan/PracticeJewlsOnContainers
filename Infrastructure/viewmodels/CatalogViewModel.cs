using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;

namespace WebMVC.viewmodels
{
    public class CatalogViewModel
    {
        public IEnumerable<SelectListItem> Brand { get; set; }
        public IEnumerable<SelectListItem>Types { get; set; }
        public IEnumerable<catalogitemclass> catalogitems { get; set; }
        public Paginationinfo paginationinfo { get; set; }
    }
}
