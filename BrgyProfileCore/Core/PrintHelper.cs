using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpreadsheetLight;

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
            var sl = new SLDocument();
            sl.AddWorksheet("Residents");
            sl.SelectWorksheet("Residents");

            //Add data and styles to the new worksheet
            residentHeaders.ForEach(header =>
            {
                var col = residentHeaders.IndexOf(header) + 1;
                var cellColumn = NumberToString(col) + "1";
                sl.SetCellValue(cellColumn, header);

                SLStyle style = sl.CreateStyle();
                style.SetWrapText(true);
                style.SetBottomBorder(DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Medium, SLThemeColorIndexValues.Dark1Color);
                style.Alignment.Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                sl.SetCellStyle(cellColumn, style);
                sl.AutoFitColumn(cellColumn, 50);
            });

            sl.SetRowHeight(1, 40);
            residents.ForEach(resident =>
            {
                var row = residents.IndexOf(resident) + 2;
                //sl.SetCellValue($"A{row}", resident.HouseholdId);
                sl.SetCellValue($"D{row}", resident.FullName);
                sl.SetCellValue($"E{row}", resident.RelationshiptoHHHead);

                sl.SetCellValue($"F{row}", resident.Gender);
                sl.SetCellValue($"G{row}", resident.MaritalStatus);
                sl.SetCellValue($"H{row}", resident.DateOfBirth.ToString("MM/dd/yyyy"));
                sl.SetCellValue($"I{row}", resident.Age);
                sl.SetCellValue($"J{row}", resident.HighestEducationalAttainment);

                sl.SetCellValue($"K{row}", resident.Grade_YearLevelofSchoolAttendance);
                sl.SetCellValue($"L{row}", resident.ReasonforDroppingOutofSchool);
                sl.SetCellValue($"M{row}", resident.Religion);
                sl.SetCellValue($"N{row}", resident.SpecialSkills);
                sl.SetCellValue($"O{row}", resident.Disability);

                sl.SetCellValue($"P{row}", resident.IndigenousPeopleMembership);
                sl.SetCellValue($"Q{row}", resident.PHICMembershipSponsor);
                sl.SetCellValue($"R{row}", resident.NHTS);
                sl.SetCellValue($"S{row}", resident.Four_Ps);
                sl.SetCellValue($"T{row}", resident.MajorOccupationofEarningHHMember);

                sl.SetCellValue($"U{row}", resident.OtherSourceofIncome);
                sl.SetCellValue($"V{row}", resident.TotalActualIncomeofEarningMember);
                sl.SetCellValue($"W{row}", resident.HouseOwnership);
                sl.SetCellValue($"X{row}", resident.GeohazardLocation);
                sl.SetCellValue($"Y{row}", resident.WaterLevel);

                sl.SetCellValue($"Z{row}", resident.LotOwnershipwhereHouseisLocated);
                sl.SetCellValue($"AA{row}", resident.TypeofFuel_Lighting);
                sl.SetCellValue($"AB{row}", resident.TypeofFuel_Cooking);
                sl.SetCellValue($"AC{row}", resident.SourceofWaterforDrinking);
                sl.SetCellValue($"AD{row}", resident.DistanceofWaterSourcefromHouse);

                sl.SetCellValue($"AE{row}", resident.SourceofWaterforGeneralUse);
                sl.SetCellValue($"AF{row}", resident.TypeofToilet);
                sl.SetCellValue($"AG{row}", resident.TypeofGarbageDisposal);
                sl.SetCellValue($"AH{row}", resident.No_ofChildren_BornAlive);
                sl.SetCellValue($"AI{row}", resident.No_ofChildren_StillLiving);

                sl.SetCellValue($"AJ{row}", resident.FamilyPlanningPracticeMethodUsed);
                sl.SetCellValue($"AK{row}", resident.FamilyPlanningPracticeIntensiontoUse);
                sl.SetCellValue($"AL{row}", resident.FamilyPlanningPracticeReasonforNotUsing);
                sl.SetCellValue($"AM{row}", resident.Remarks);

                residentHeaders.ForEach(header =>
                {
                    var col = residentHeaders.IndexOf(header) + 1;
                    var cellColumn = NumberToString(col) + $"{row}";

                    SLStyle style = sl.CreateStyle();
                    style.SetWrapText(true);
                    sl.SetCellStyle(cellColumn, style);
                    sl.AutoFitColumn(cellColumn, 50);
                });

                sl.AutoFitRow(row);
            });
            //Save the excel file
            sl.SaveAs(filename);
        }

        public static void printRBI(List<Resident> residents, string filename = "RBI.xls")
        {
            ////Create new Excel WorkBook document. 
            ////The default file format is XLSX, but we can override that for legacy support
            //WorkBook xlsWorkbook = WorkBook.Create(ExcelFileFormat.XLS);
            //xlsWorkbook.Metadata.Author = "IronXL";

            ////Add a blank WorkSheet
            //WorkSheet xlsSheet = xlsWorkbook.CreateWorkSheet("new_sheet");

            //// Headers

            ///// NAME HEADER
            //xlsSheet.Merge("A1:D1");
            //xlsSheet["A1"].Value = "NAME";
            //xlsSheet["A1"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            //xlsSheet.Merge("A2:D2");
            //xlsSheet["A2"].Value = "1";
            //xlsSheet["A2"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            //xlsSheet["A3"].Value = "LAST";
            //xlsSheet["B3"].Value = "FIRST";
            //xlsSheet["C3"].Value = "MIDDLE";
            //xlsSheet["D3"].Value = "QUALIFIER";

            ///// ADDRESS HEADER
            //xlsSheet.Merge("E1:G1");
            //xlsSheet["E1"].Value = "ADDRESS";
            //xlsSheet["E1"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            //xlsSheet.Merge("E2:G2");
            //xlsSheet["E2"].Value = "(2)";
            //xlsSheet["E2"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;

            //xlsSheet["E3"].Value = "NUMBER";
            //xlsSheet["F3"].Value = "STREET NAME";
            //xlsSheet["G3"].Value = "NAME OF SITIO";

            ////Add data and styles to the new worksheet
            //residents.ForEach(resident =>
            //{
            //    var row = residents.IndexOf(resident) + 5;
            //    xlsSheet[$"A{row}"].Value = resident.LastName;
            //    xlsSheet[$"B{row}"].Value = resident.FirstName;
            //    xlsSheet[$"C{row}"].Value = resident.MiddleName;
            //});

            ////Save the excel file
            //xlsWorkbook.SaveAs(filename);
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
