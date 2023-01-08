using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;
using Infrastructure.viewmodels;

namespace Infrastructure.controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalog _catalog;
        public CatalogController(ICatalog catalog)
        {
            _catalog = catalog;
        }
        public async Task<IActionResult> Index(int ? pagenumber,int? typesFilterApplied,int? brandFilterApplied)
        {
            var itemsperpage = 10;
            var catalog = await _catalog.GetAllItems(pagenumber ?? 0, itemsperpage, typesFilterApplied, brandFilterApplied); //calling items microservice,it will bring all items to display in views.
            var viewmodel = new CatalogViewModel
            {
                Brand = await _catalog.Getbrands(),//calling brand microservice,it will bring list of selectlist brands. 
                Types = await _catalog.Gettypes(),//calling types microservice,it will bring the list of selectlist types.
                catalogitems = catalog.Data,
                //we are doing paginated info seperately so that all the datas will come quickly to display in views.
                //instead of getting bigger chunk we are dividing into smaller pieces.

                paginationinfo = new Paginationinfo
                {
                    TotalItems = catalog.Count,
                    ActualPage = catalog.Pageindex,
                    ItemsPerPage = catalog.Pagesize,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsperpage)
                    //math ceiling will round up the divide results and then we are converting again to int to display total pagenumber
                    //first we are making it into decimal so that we will get exact pages like 1.4,so that we wont missout those remaining 4 pages
                },

                TypesFilterApplied= typesFilterApplied,
                BrandFilterApplied= brandFilterApplied
             //above 2 lines of code is saying whatever the user choose,the drop down will stay in that choice after the user chose.
            };
           return View(viewmodel);
        }
    }
}
