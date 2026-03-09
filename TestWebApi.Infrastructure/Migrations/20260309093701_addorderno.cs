using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addorderno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order_no",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_no",
                table: "orders");
        }
    }
}
