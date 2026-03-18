using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMarket.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionPdfToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionPdfUrl",
                table: "Products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionPdfUrl",
                table: "Products");
        }
    }
}
