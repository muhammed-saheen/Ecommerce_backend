using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class address_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_addresses_addressid",
                table: "orders");

            migrationBuilder.AddColumn<Guid>(
                name: "userid",
                table: "addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_addresses_userid",
                table: "addresses",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_users_userid",
                table: "addresses",
                column: "userid",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_addresses_addressid",
                table: "orders",
                column: "addressid",
                principalTable: "addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_users_userid",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_addresses_addressid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_addresses_userid",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_addresses_addressid",
                table: "orders",
                column: "addressid",
                principalTable: "addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
