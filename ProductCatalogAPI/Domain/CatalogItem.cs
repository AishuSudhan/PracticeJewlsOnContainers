namespace ProductCatalogAPI.Domain
{
    public class CatalogItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public int CatalogTypeID { get; set; }
        public int CatalogBrandID { get; set; }

        public virtual CatalogBrand CatalogBrand { get; set; }
        public virtual CatalogType CatalogType { get; set; }
    }
    
}
