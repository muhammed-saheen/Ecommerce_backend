using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class F : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "Role", "Status", "username" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174000"), "sn@gmail.com", "saheen", "$2a$11$lmv2OnI.Et1.fYhzHE9nTu/dRoIcut44TOuEMjeRC3wnvJeee4pyS", "8606524321", "admin", true, "muhd_sn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"));
        }
    }
}
