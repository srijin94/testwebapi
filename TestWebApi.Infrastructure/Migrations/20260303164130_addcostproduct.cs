using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcostproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "products");
        }
    }
}
