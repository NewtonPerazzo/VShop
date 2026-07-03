using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                "Values('CellPhone Apple', 1200, 'Apple IPhone', 10, 'cellphone1.jpg', 1)");

            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                "Values('CellPhone Samsung', 900, 'Samsung cell', 10, 'cellphone2.jpg', 1)");

            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                "Values('Short', 69.99, 'Short for running', 15, 'short.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Products where Name in ('CellPhone Apple', 'CellPhone Samsung', 'Short')");
        }
    }
}
