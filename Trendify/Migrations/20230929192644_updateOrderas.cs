using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendify.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminuserid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8126e449-05c1-4756-98d0-c35ec091e1bc", "AQAAAAIAAYagAAAAEMHPpLa/Lq5KRWKcvyhN8lG2zGuRohbDEnznM9phVEOG8o6A+qcN+xpkDHp99lgPdQ==", "3bb31e79-0581-4b59-a757-e48ad2692953" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminuserid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e582803-bcea-4f9a-b9f7-3965f14b823d", "AQAAAAIAAYagAAAAEDY7fObUjYPPltKGyxTkr809N+vCKvEbniu3L+vjO+AHwxXEFq0KPtHA0rYQYxfNSA==", "6b384898-f331-4e95-b70d-9f0529f9ad32" });
        }
    }
}
