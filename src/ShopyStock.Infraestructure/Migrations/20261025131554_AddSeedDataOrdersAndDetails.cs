using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopyStock.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataOrdersAndDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "Status", "TotalPrice" },
                values: new object[] { new Guid("51449123-eede-4982-aa0c-7a618346b174"), new DateTime(2026, 10, 25, 13, 15, 54, 183, DateTimeKind.Utc).AddTicks(6181), 0, 10000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { 4, "Mouse Inalambrico Logitech", 5000m, 13 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { new Guid("51449123-eede-4982-aa0c-7a618346b174"), 4, 2, 5000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("51449123-eede-4982-aa0c-7a618346b174"), 4 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("51449123-eede-4982-aa0c-7a618346b174"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
