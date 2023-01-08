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

            var model = new PaginatedItemsViewModel//we are creating an object here and return this obj to microservice by 
                                                   //return ok(model).during this process,microservice take this obj will convert them to json thats called serialization.
            
            {
                //this step will tell the user how many items in database and how many items perpage and display them in json.
                Pageindex = pageindex,
                Pagesize = item.Count,
                Data = item,
                Count = itemcount.Result//itemcount will get number of items in database and .Result will give you the result.
            };
                                    
            return Ok(model);


        }
        //this API is for getting results for selected brand and type from dropdown.

        [HttpGet("[action]/filter")]
        
        //we are adding filter in route here because in API we cannot have same method name with same route options for 
        //getting two differnt results. (but in c# we can have two methods with same name with differnt datatype for parameters.)
        public async Task<IActionResult> catalogitem([FromQuery] int? catalogtypeid, [FromQuery] int? catalogbrandid,
            [FromQuery] int pageindex = 0, [FromQuery] int pagesize = 4)
        {
            var query = (IQueryable<CatalogItem>)_context.catalog;//query means we are asking just to get the information without executing the table.
            if(catalogtypeid.HasValue)
            {
                query = query.Where(c => c.CatalogTypeID == catalogtypeid.Value);
                //here we are checking CatalogTypeId from CatalogItem table(database) and the catalogtypeid from parameter(user selected one passed 
                //through this method/action are same. we are getting it int?(that is nullable) because user may select it or maynot. so if there is a 
                //value in that parameter, then check it.
                //we are overwritting the query here.
                        }
            if(catalogbrandid.HasValue)
            {
                //this code does same like catalogtypeid.and we are not using if else because user can choose both type and brand from dropdown.
                
                query = query.Where(c => c.CatalogBrandID == catalogbrandid.Value);
            }


            var itemcount = query.LongCountAsync();
            var item = await query.OrderBy(i => i.Name)
                .Skip(pageindex * pagesize).Take(pagesize).ToListAsync();
            item = changepictureurl(item);

            var model = new PaginatedItemsViewModel

            {
                Pageindex = pageindex,
                Pagesize = item.Count,
                Data = item,
                Count = itemcount.Result
            };

            return Ok(model);


        }

        private  List<CatalogItem> changepictureurl(List<CatalogItem> item)
        {
            item.ForEach(items => items.PictureUrl = items.PictureUrl.Replace("https://needsreplacement", _config["ExternalBaseUrl"]));
            return item;
        }
    }
}
