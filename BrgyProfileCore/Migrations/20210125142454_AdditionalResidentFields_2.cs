using Microsoft.EntityFrameworkCore.Migrations;

namespace BrgyProfileCore.Migrations
{
    public partial class AdditionalResidentFields_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guardian",
                table: "Residents",
                newName: "WaterLevel");

            migrationBuilder.RenameColumn(
                name: "AdditionalInfoJSON",
                table: "Residents",
                newName: "TypeofToilet");

            migrationBuilder.AddColumn<string>(
                name: "Disability",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistanceofWaterSourcefromHouse",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyNo",
                table: "Residents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Four_Ps",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeohazardLocation",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade_YearLevelofSchoolAttendance",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseOwnership",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndigenousPeopleMembership",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineNo",
                table: "Residents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LotOwnershipwhereHouseisLocated",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MajorOccupationofEarningHHMember",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NHTS",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "No_ofChildren_BornAlive",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "No_ofChildren_StillLiving",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceofIncome",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PHICMembershipSponsor",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonforDroppingOutofSchool",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelationshiptoHHHead",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceofWaterforDrinking",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceofWaterforGeneralUse",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialSkills",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalActualIncomeofEarningMember",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofFuel_Cooking",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofFuel_Lighting",
                table: "Residents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofGarbageDisposal",
                table: "Residents",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disability",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "DistanceofWaterSourcefromHouse",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "FamilyNo",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Four_Ps",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "GeohazardLocation",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Grade_YearLevelofSchoolAttendance",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "HouseOwnership",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "IndigenousPeopleMembership",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "LineNo",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "LotOwnershipwhereHouseisLocated",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "MajorOccupationofEarningHHMember",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "NHTS",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "No_ofChildren_BornAlive",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "No_ofChildren_StillLiving",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "OtherSourceofIncome",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "PHICMembershipSponsor",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "ReasonforDroppingOutofSchool",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "RelationshiptoHHHead",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "SourceofWaterforDrinking",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "SourceofWaterforGeneralUse",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "SpecialSkills",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "TotalActualIncomeofEarningMember",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "TypeofFuel_Cooking",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "TypeofFuel_Lighting",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "TypeofGarbageDisposal",
                table: "Residents");

            migrationBuilder.RenameColumn(
                name: "WaterLevel",
                table: "Residents",
                newName: "Guardian");

            migrationBuilder.RenameColumn(
                name: "TypeofToilet",
                table: "Residents",
                newName: "AdditionalInfoJSON");
        }
    }
}
