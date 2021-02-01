using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class AddFieldsToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "886D4AEA1B6F72C45F944F9E9C0296031BE5912350E0EDE00FFE122D5E653161EF2071B7DD28F760AD2A7E8CEEB5C868D945BCE69B5884E5DFF4E44B99A60439W95I5H2N9TL2BN424XZU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "7CA30CD76802442041CB4C3E51AD13FC9110711A67F498FEB752A636489A456B1AF83B93E7A883DCE27BAA167C95146C996B7DDD75383E73ACF59185ABBA4F23XDH8V2IYNZX5TNLGL6D4");
        }
    }
}
