using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class AdditionalResidentFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfoJSON",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HighestEducationalAttainment",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Residents",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInfoJSON",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "HighestEducationalAttainment",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Residents");
        }
    }
}
