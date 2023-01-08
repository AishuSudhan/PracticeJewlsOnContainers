namespace Infrastructure.Infrastructures
{
    public static class APIpaths
    {
        public static class Catalog
        {
            public static string Getalltypes(string BaseUrl)
            {
                return $"{BaseUrl}/catalogtypes";
            }
            public static string Getallbrands(string BaseUrl)
            {
                return $"{BaseUrl}/catalogbrands";
            }
            public static string GetallItems(string BaseUrl,int pagenumber,int pagesize,int? brand,int? type)
            {
                var preurl = string.Empty;
                var filterqs = string.Empty;
                if(brand.HasValue)
                {
                    filterqs = $"catalogBrandID={brand.Value}";
                }
                if(type.HasValue)
                {
                    filterqs = (filterqs == string.Empty) ? $"catalogTypeID={type.Value}" : $"{filterqs}&CatalogTypeID={type.Value}";
                }
                if(string.IsNullOrEmpty(filterqs))
                {
                    preurl= $"{BaseUrl}/catalogItem?pagenumber={pagenumber}&pagesize={pagesize}";
                }
                 else
                    {
                        preurl= $"{BaseUrl}/catalogItem/filter?pagenumber={pagenumber}&pagesize={pagesize}&{filterqs}";
                    }
                return preurl;
            }
        }
    }
}
