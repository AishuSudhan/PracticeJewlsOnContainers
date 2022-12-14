using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Data
{
    //Anytime in this Catalog Microservice project,Anywhere if you want to talk to Database you need to create instance of this class 
    // CatalogContext.Because this class CatalogContext has everything that need for a Database.
    public class CatalogContext:DbContext
    {
        /*constructors here does the job of where to create the Table.since we have 
        inheritance here,we can just create the tables and pass it to the parent class.
        the parent/base class will take care of the "where" part.
        this constructor just pass the information to the base class.
        DbContextOptions is a type of parameter exists in EntityFrameWorkCore which is basically the options which specify 
        what database where the sql Server is and all those informations are stored in
        the parameter DbContextOptions.*/
        
        public CatalogContext(DbContextOptions options):base(options)
        { }
        

        //DbSet is Datatype to create table. Set means Table.
        //here we are creating a Table catalogTypes for the class CatalogType.

        public DbSet<CatalogType> catalogTypes { get; set; }
        public DbSet<CatalogBrand> catalogBrands { get; set; }
        public DbSet<CatalogItem> catalog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            //this is the method whic tells what are the elements we are going to have
            //in a table.

        {
            modelBuilder.Entity<CatalogType>(e =>
            {
                e.Property(t => t.Type).IsRequired().HasMaxLength(100);
                e.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CatalogBrand>(e =>
            {
                e.Property(b => b.Brand).IsRequired().HasMaxLength(100);
                e.Property(b => b.ID).IsRequired().ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CatalogItem>(e =>
            {
                e.Property(i => i.ID).IsRequired().ValueGeneratedOnAdd();
                e.Property(i => i.Name).IsRequired().HasMaxLength(100);
                e.Property(i => i.Price).IsRequired();

                e.HasOne(i => i.CatalogType)
                .WithMany().HasForeignKey(t => t.CatalogTypeID);
                /* we are creating the foreign key navigation from CatalogType table('s Id)
                 to CatalogItem table*/

                e.HasOne(i => i.CatalogBrand).WithMany()
                .HasForeignKey(t => t.CatalogBrandID);
            });
        }
    }
}
