using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class AdditionalResidentFields_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Citizenship",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyPlanningPracticeIntensiontoUse",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyPlanningPracticeMethodUsed",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyPlanningPracticeReasonforNotUsing",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Residents",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Citizenship",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "FamilyPlanningPracticeIntensiontoUse",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "FamilyPlanningPracticeMethodUsed",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "FamilyPlanningPracticeReasonforNotUsing",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Residents");
        }
    }
}
