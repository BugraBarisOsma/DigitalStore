using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmailUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4902ed63-8360-42fb-9e3f-e64fa7306c58");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "029c0ed6-dd79-4c54-9c87-bfc651c009df", "b756e11a-1cc1-4b20-82ca-b839bdb40d9c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "029c0ed6-dd79-4c54-9c87-bfc651c009df");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b756e11a-1cc1-4b20-82ca-b839bdb40d9c");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72f17110-cc5c-4c96-8732-971d3d33fc4f", null, "Customer", "CUSTOMER" },
                    { "a5633d28-c86e-47f3-aba6-626ec1ac6fcb", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "Password", "Points", "Role", "SecurityStamp", "Surname", "Username", "WalletBalance", "isActive" },
                values: new object[] { "0dbd1cce-fb90-42ff-87cb-3adbb63ca8a9", "bcbb4a95-2347-4398-97d5-569c0936d2b1", "admin@example.com", "ADMIN@EXAMPLE.COM", "ADMIN", "admin123", 10, "Admin", "275c47ff-79bb-4d84-9076-fa5f91db2a60", "admin", "admin", 2000m, true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a5633d28-c86e-47f3-aba6-626ec1ac6fcb", "0dbd1cce-fb90-42ff-87cb-3adbb63ca8a9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72f17110-cc5c-4c96-8732-971d3d33fc4f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a5633d28-c86e-47f3-aba6-626ec1ac6fcb", "0dbd1cce-fb90-42ff-87cb-3adbb63ca8a9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5633d28-c86e-47f3-aba6-626ec1ac6fcb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0dbd1cce-fb90-42ff-87cb-3adbb63ca8a9");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "029c0ed6-dd79-4c54-9c87-bfc651c009df", null, "Admin", "ADMIN" },
                    { "4902ed63-8360-42fb-9e3f-e64fa7306c58", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "Password", "Points", "Role", "SecurityStamp", "Surname", "Username", "WalletBalance", "isActive" },
                values: new object[] { "b756e11a-1cc1-4b20-82ca-b839bdb40d9c", "1f63a2ff-4ec8-49f5-8b63-f32e2331dd32", "ADMIN@EXAMPLE.COM", "ADMIN", "admin123", 10, "Admin", "e69c7fe5-4338-495c-8125-8f966ad57bd8", "admin", "admin", 2000m, true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "029c0ed6-dd79-4c54-9c87-bfc651c009df", "b756e11a-1cc1-4b20-82ca-b839bdb40d9c" });
        }
    }
}
