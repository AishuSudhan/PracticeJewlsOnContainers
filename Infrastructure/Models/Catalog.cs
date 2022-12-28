namespace WebMVC.Models
{
    public class Catalog
        //you can give anyname to the class. doesnt matter.we are doing deserialization here. when we get back the results,it will show the paginated items(we wrote)
        // thats why we are deserializing paginated itews here. properties should be exactly same as in viewmodels(api) folder.i just copy all the properties
        //from that folder  and paste it here. 
    {
        public int Pageindex { get; set; }
        public int Pagesize { get; set; }
        public long Count { get; set; }
        public IEnumerable<catalogitemclass> Data { get; set; }
    }
}
