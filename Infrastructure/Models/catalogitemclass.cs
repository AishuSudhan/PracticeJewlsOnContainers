namespace Infrastructure.Models
{
    public class catalogitemclass
    {
        //i copy paste everything from catalogItem class from API. because we need paginated class in webmvc so we copy paste those properties 
        //from paginateditems(api) class to catalog class in webmvc. that class has relation with catalogitem class in api.so we copy paste that class too.
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public int CatalogTypeID { get; set; }
        public int CatalogBrandID { get; set; }

        //we dont need virtual relationship here. because there is no database in webclient. we need to get the results from api and display in webclient.
        //so we can change the virutal property to just string datatype.
        public string CatalogBrand { get; set; }
        public string CatalogType { get; set; }
    }
}
