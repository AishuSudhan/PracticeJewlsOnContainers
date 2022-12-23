using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Data
{
    //this class is going to place the sample data in the catalog. we dont need this class for cart and Order microservices.
    //only catalog need data. we dont want anything to be in cart and order unless the user adds them. 
    //this class is going to be a utility class.
    public class CatalogSeed
    {
        public static void Seed (CatalogContext context)
        {
            context.Database.Migrate();//this code will run all the Migrations and create the tables. this code will make c# come to reality 
            //by having tables created. If this code is not called tables won't be created. we are writting this code first in seed because 
            //before you seed that is put data in database we want database and tables to be created and apply all the changes.
            //this one has to be done even before the application use the database.
            if (!context.catalogTypes.Any())
            {
                context.catalogTypes.AddRange(getcatalogtypes());
                context.SaveChanges();
            }

            if(!context.catalogBrands.Any())
            {
                context.catalogBrands.AddRange(getcatalogbrands());
                context.SaveChanges();

             }
            if(!context.catalog.Any())
            {
                context.catalog.AddRange(getcatalog());
                context.SaveChanges();
            }
        }

        private static IEnumerable<CatalogItem> getcatalog()
        {
            return new List<CatalogItem>
            {
                new CatalogItem{Name="red Fire",CatalogBrandID=1,CatalogTypeID=2,Price=100M,Description="perfect for  celebration",PictureUrl="https://needsreplacement/api/pic/1" },
                new CatalogItem{Name="blue Ocean",CatalogBrandID=2,CatalogTypeID=1,Price=21.67M,Description="perfect for september",PictureUrl="https://needsreplacement/api/pic/2"},
                new CatalogItem{Name="green Lake",CatalogBrandID=3,CatalogTypeID=2,Price=675.343M,Description="ring for Nature lovers",PictureUrl="https://needsreplacement/api/pic/3" }, 
                new CatalogItem{Name="snow white",CatalogBrandID=3,CatalogTypeID=1,Price=500M,Description="for people who loves sparkle",PictureUrl="https://needsreplacement/api/pic/4"},
                new CatalogItem{Name="morning breeze",CatalogBrandID=2,CatalogTypeID=3,Price=200M,Description="all days are sunday",PictureUrl="https://needsreplacement/api/pic/5" },
                new CatalogItem{Name="beach walking",CatalogBrandID=1,CatalogTypeID=3,Price=218.67M,Description="perfect for summer",PictureUrl="https://needsreplacement/api/pic/6"},
                new CatalogItem{Name="Lake deep blue",CatalogBrandID=3,CatalogTypeID=3,Price=875.343M,Description="deep blue likes deep thoughts",PictureUrl="https://needsreplacement/api/pic/7" },
                new CatalogItem{Name="purple leaf",CatalogBrandID=2,CatalogTypeID=2,Price=369M,Description="for people who loves fall",PictureUrl="https://needsreplacement/api/pic/8"},
                new CatalogItem{Name="holiday spirit",CatalogBrandID=1,CatalogTypeID=1,Price=1000M,Description="perfect for Holiday celebration",PictureUrl="https://needsreplacement/api/pic/9" }
                

            };

        }  
            


        private static IEnumerable<CatalogBrand> getcatalogbrands()
        {
            return new List<CatalogBrand>
            {
                new CatalogBrand{Brand="Tiffany"},
                new CatalogBrand{Brand="Bony Levy"},
                new CatalogBrand{Brand="Kate Spade"}
            };
        }

        private static IEnumerable<CatalogType> getcatalogtypes()
        {
            return new List<CatalogType>
            {
                new CatalogType{Type="Wedding Rings"},
                new CatalogType { Type="Engagement Rings"},
                new CatalogType{Type="Anniversary Rings"}
            };
        }
    }
}
