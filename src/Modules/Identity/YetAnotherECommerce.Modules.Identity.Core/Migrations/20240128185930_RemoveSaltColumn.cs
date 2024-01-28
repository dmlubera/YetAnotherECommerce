using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YetAnotherECommerce.Modules.Identity.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSaltColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password_Salt",
                schema: "identity",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password_Salt",
                schema: "identity",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
