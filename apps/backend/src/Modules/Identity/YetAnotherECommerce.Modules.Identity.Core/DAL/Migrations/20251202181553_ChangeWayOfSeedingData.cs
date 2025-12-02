using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWayOfSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a29202b4-5f9f-4dd5-a049-dc88b94b4129"));

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f41f1b10-b385-41ba-83aa-b9342e80533a"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a29202b4-5f9f-4dd5-a049-dc88b94b4129"), null, "admin", "ADMIN" },
                    { new Guid("f41f1b10-b385-41ba-83aa-b9342e80533a"), null, "customer", "CUSTOMER" }
                });
        }
    }
}
