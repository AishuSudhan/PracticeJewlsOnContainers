using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ICatalog
    {
        Task<Catalog> GetAllItems(int pagenumber, int pagesize);
        //Catalog here is the replica of paginateditem(src).
        Task<IEnumerable<SelectListItem>> Gettypes();
        //<IEnumerable<SelectListItem>> is like listbox in xamarin. this will give the selected item with id from the dropbox.
        //Task will return list of items.
        Task<IEnumerable<SelectListItem>> Getbrands();
    }
}
