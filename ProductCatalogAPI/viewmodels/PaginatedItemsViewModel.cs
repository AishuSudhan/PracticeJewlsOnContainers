using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.viewmodels
{
    public class PaginatedItemsViewModel
    {
        public int Pageindex { get; set; }
        public int Pagesize { get; set; }
        public long Count { get; set; }
        public IEnumerable<CatalogItem> Data { get; set; }
       
    }
}
