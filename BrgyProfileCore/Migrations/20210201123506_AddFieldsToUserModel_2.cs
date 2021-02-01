using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class AddFieldsToUserModel_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Name", "Password", "Role" },
                values: new object[] { "Admin", "F3D687BCE758C86D973492EAFF29F66A13C2C6B22B6A315EDDA35C7619A0F633ED9CB022D8D9EFCEE6DA0225E579CC08D8B8FDF55D4CA211304C6B2D483A3ACFURBFIDCNP6994KLE0VUW", "Administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Name", "Password", "Role" },
                values: new object[] { null, "886D4AEA1B6F72C45F944F9E9C0296031BE5912350E0EDE00FFE122D5E653161EF2071B7DD28F760AD2A7E8CEEB5C868D945BCE69B5884E5DFF4E44B99A60439W95I5H2N9TL2BN424XZU", null });
        }
    }
}
