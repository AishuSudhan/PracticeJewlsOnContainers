using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.viewmodels;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> catalogTypes()
        {
            var types = await _context.catalogTypes.ToListAsync();
            return Ok(types);//we are adding OK in return so that it will show the status ok if it works successfully.
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> catalogBrands()
        {
            var brands = await _context.catalogBrands.ToListAsync();
            return Ok(brands); 

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> catalogitem([FromQuery]int pageindex=0,[FromQuery]int pagesize=4)
        {
            var itemcount = _context.catalog.LongCountAsync();//itemcount is to count total items in database
            
            //this code will give you the catalog data sort by name in alphabelical order(order by name)
            //and show the asking page with asking number of items(take pagesize will do that)
            //by skipping (skip)number of items before the asking page and return it in a list.
            //all these are doing by secondary thread.Meanwhile the main thread is waiting for user input or wait for secondary thread to finish its 
            //task and come back with required result.
           
            var item = await _context.catalog.OrderBy(i => i.Name)
                .Skip(pageindex * pagesize).Take(pagesize).ToListAsync();
            item = changepictureurl(item);//we are calling this method so that we can replace the dummy url we created in catalogseed class with real url we created in appsettings.json

            var model = new PaginatedItemsViewModel//this step will tell the user how many items in database and how many items perpage.
            {
                Pageindex = pageindex,
                Pagesize = item.Count,
                Data = item,
                Count = itemcount.Result//itemcount will get number of items in database and .Result will give you the result.
            };
                                    
            return Ok(model);


        }

        private List<CatalogItem> changepictureurl(List<CatalogItem> item)
        {
            item.ForEach(items => items.PictureUrl = items.PictureUrl.Replace("https://needsreplacement", _config["ExternalBaseUrl"]));
            return item;
        }
    }
}
