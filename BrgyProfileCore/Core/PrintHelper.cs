using System;
using System.Collections.Generic;
using System.Text;
using IronXL;
using System.Linq;

namespace BrgyProfileCore.Core
{
    
    class PrintHelper
    {
        public static List<String> residentHeaders
        {
            get
            {
                return new List<String>(){
                    // A
                    "Household No.", 
                    "Family No.",
                    "Line No.",
                    "Name of Household Member",
                    "Relationship to HH Head",

                    //F
                    "Sex",
                    "Marital Status",
                    "Date of Birth (mm/dd/yyyy)",
                    "Age",
                    "Highest Educational Attainment",

                    // K
                    "Grade/Year Level of School Attendance", 
                    "Reason for Dropping Out of School",
                    "Religious Affiliation",
                    "Special Skills",
                    "Disability",

                    //P
                    "Indigenous People Membership",
                    "PHIC Membership Sponsor",
                    "Beneficiary - NHTS",
                    "Beneficiary - 4P's",
                    "Major Occupation of Earning HH Member",

                    //U
                    "Other Source of Income",
                    "Total Actual Income of Earning Member",
                    "House Ownership",
                    "Geohazard Location",
                    "Water Level",

                    //Z
                    "Lot Ownership where House is Located",
                    "Type of Fuel for Lighting",
                    "Type of Fuel for Cooking",
                    "Source of Water for Drinking",
                    "Distance of Water Source from House",

                    //AE
                    "Source of Water for General Use",
                    "Type of Toilet",
                    "Type of Garbage Disposal",
                    "No. of Children - Born Alive",
                    "No. of Children - Still Living",
                    
                    //AJ
                    "Family Planning Practice - Method Used",
                    "Family Planning Practice - Intention to Use",
                    "Family Planning Practice - Reason for not Using/Stopping/Shifting",
                    "Remarks"
                };
            }
        }
        public static void printResidents(List<Resident> residents, string filename = "Residents.xls")
        {

            //Create new Excel WorkBook document. 
            //The default file format is XLSX, but we can override that for legacy support
            WorkBook xlsWorkbook = WorkBook.Create(ExcelFileFormat.XLS);
            xlsWorkbook.Metadata.Author = "IronXL";

            //Add a blank WorkSheet
            WorkSheet xlsSheet = xlsWorkbook.CreateWorkSheet("new_sheet");

            //Add data and styles to the new worksheet
            residentHeaders.ForEach(header =>
            {
                var col = residentHeaders.IndexOf(header) + 1;
                var cellColumn = NumberToString(col) + "1";
                xlsSheet[cellColumn].Value = header;
                xlsSheet[cellColumn].Style.WrapText = true;
                xlsSheet[cellColumn].Style.BottomBorder.Type = IronXL.Styles.BorderType.Thick;
                xlsSheet[cellColumn].Style.ShrinkToFit = true;
            });

            residents.ForEach(resident =>
            {
                var row = residents.IndexOf(resident) + 2;
                xlsSheet[$"A{row}"].Value = resident.HouseholdId;
                xlsSheet[$"D{row}"].Value = resident.FullName;
                xlsSheet[$"E{row}"].Value = resident.RelationshiptoHHHead;

                xlsSheet[$"F{row}"].Value = resident.Gender;
                xlsSheet[$"G{row}"].Value = resident.MaritalStatus;
                xlsSheet[$"H{row}"].Value = resident.DateOfBirth.ToString("MM/dd/yyyy");
                xlsSheet[$"I{row}"].Value = resident.Age;
                xlsSheet[$"J{row}"].Value = resident.HighestEducationalAttainment;

                xlsSheet[$"K{row}"].Value = resident.Grade_YearLevelofSchoolAttendance;
                xlsSheet[$"L{row}"].Value = resident.ReasonforDroppingOutofSchool;
                xlsSheet[$"M{row}"].Value = resident.Religion;
                xlsSheet[$"N{row}"].Value = resident.SpecialSkills;
                xlsSheet[$"O{row}"].Value = resident.Disability;

                xlsSheet[$"P{row}"].Value = resident.IndigenousPeopleMembership;
                xlsSheet[$"Q{row}"].Value = resident.PHICMembershipSponsor;
                xlsSheet[$"R{row}"].Value = resident.NHTS;
                xlsSheet[$"S{row}"].Value = resident.Four_Ps;
                xlsSheet[$"T{row}"].Value = resident.MajorOccupationofEarningHHMember;

                xlsSheet[$"U{row}"].Value = resident.OtherSourceofIncome;
                xlsSheet[$"V{row}"].Value = resident.TotalActualIncomeofEarningMember;
                xlsSheet[$"W{row}"].Value = resident.HouseOwnership;
                xlsSheet[$"X{row}"].Value = resident.GeohazardLocation;
                xlsSheet[$"Y{row}"].Value = resident.WaterLevel;

                xlsSheet[$"Z{row}"].Value = resident.LotOwnershipwhereHouseisLocated;
                xlsSheet[$"AA{row}"].Value = resident.TypeofFuel_Lighting;
                xlsSheet[$"AB{row}"].Value = resident.TypeofFuel_Cooking;
                xlsSheet[$"AC{row}"].Value = resident.SourceofWaterforDrinking;
                xlsSheet[$"AD{row}"].Value = resident.DistanceofWaterSourcefromHouse;

                xlsSheet[$"AE{row}"].Value = resident.SourceofWaterforGeneralUse;
                xlsSheet[$"AF{row}"].Value = resident.TypeofToilet;
                xlsSheet[$"AG{row}"].Value = resident.TypeofGarbageDisposal;
                xlsSheet[$"AH{row}"].Value = resident.No_ofChildren_BornAlive;
                xlsSheet[$"AI{row}"].Value = resident.No_ofChildren_StillLiving;

                xlsSheet[$"AJ{row}"].Value = resident.FamilyPlanningPracticeMethodUsed;
                xlsSheet[$"AK{row}"].Value = resident.FamilyPlanningPracticeIntensiontoUse;
                xlsSheet[$"AL{row}"].Value = resident.FamilyPlanningPracticeReasonforNotUsing;
                xlsSheet[$"AM{row}"].Value = resident.Remarks;
            });
            //Save the excel file
            xlsWorkbook.SaveAs(filename);
        }

        public static void printRBI(List<Resident> residents, string filename = "RBI.xls")
        {
            //Create new Excel WorkBook document. 
            //The default file format is XLSX, but we can override that for legacy support
            WorkBook xlsWorkbook = WorkBook.Create(ExcelFileFormat.XLS);
            xlsWorkbook.Metadata.Author = "IronXL";

            //Add a blank WorkSheet
            WorkSheet xlsSheet = xlsWorkbook.CreateWorkSheet("new_sheet");

            // Headers

            /// NAME HEADER
            xlsSheet.Merge("A1:D1");
            xlsSheet["A1"].Value = "NAME";
            xlsSheet["A1"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            xlsSheet.Merge("A2:D2");
            xlsSheet["A2"].Value = "1";
            xlsSheet["A2"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            xlsSheet["A3"].Value = "LAST";
            xlsSheet["B3"].Value = "FIRST";
            xlsSheet["C3"].Value = "MIDDLE";
            xlsSheet["D3"].Value = "QUALIFIER";

            /// ADDRESS HEADER
            xlsSheet.Merge("E1:G1");
            xlsSheet["E1"].Value = "ADDRESS";
            xlsSheet["E1"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            xlsSheet.Merge("E2:G2");
            xlsSheet["E2"].Value = "(2)";
            xlsSheet["E2"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            xlsSheet["E3"].Value = "NUMBER";
            xlsSheet["F3"].Value = "STREET NAME";
            xlsSheet["G3"].Value = "NAME OF SITIO";

            //Add data and styles to the new worksheet
            residents.ForEach(resident =>
            {
                var row = residents.IndexOf(resident) + 5;
                xlsSheet[$"A{row}"].Value = resident.LastName;
                xlsSheet[$"B{row}"].Value = resident.FirstName;
                xlsSheet[$"C{row}"].Value = resident.MiddleName;
            });

            //Save the excel file
            xlsWorkbook.SaveAs(filename);
        }

        public static string NumberToString(int value)
        {
            StringBuilder sb = new StringBuilder();

            do
            {
                value--;
                int remainder = 0;
                value = Math.DivRem(value, 26, out remainder);
                sb.Insert(0, Convert.ToChar('A' + remainder));

            } while (value > 0);

            return sb.ToString();
        }
    }
}
