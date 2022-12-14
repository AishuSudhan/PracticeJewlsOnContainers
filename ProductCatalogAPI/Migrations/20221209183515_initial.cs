using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalogBrands",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogBrands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "catalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "catalog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogTypeID = table.Column<int>(type: "int", nullable: false),
                    CatalogBrandID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_catalog_catalogBrands_CatalogBrandID",
                        column: x => x.CatalogBrandID,
                        principalTable: "catalogBrands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catalog_catalogTypes_CatalogTypeID",
                        column: x => x.CatalogTypeID,
                        principalTable: "catalogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalog_CatalogBrandID",
                table: "catalog",
                column: "CatalogBrandID");

            migrationBuilder.CreateIndex(
                name: "IX_catalog_CatalogTypeID",
                table: "catalog",
                column: "CatalogTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalog");

            migrationBuilder.DropTable(
                name: "catalogBrands");

            migrationBuilder.DropTable(
                name: "catalogTypes");
        }
    }
}
