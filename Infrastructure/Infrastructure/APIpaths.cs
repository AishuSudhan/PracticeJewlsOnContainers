namespace WebMVC.Infrastructure
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
            public static string GetallItems(string BaseUrl,int pagenumber,int pagesize)
            {
                return $"{BaseUrl}/catalogItem?pagenumber={pagenumber}&pagesize={pagesize}";
            }
        }
    }
}
