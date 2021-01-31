using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class CreateUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "837EE2E09592A5AE44D06A44D8E5EA6957A40B81F3578A67F2C569C102607ACBE8478A20B61C1D66F613C8FB9AEFDE9226F0CE28F612F2B2E4F6B4093E298ED48WTSCJHWFI6ZH0X38MAO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "^?H??(qQ??o??)'s`=\rj???*?rB?");
        }
    }
}
