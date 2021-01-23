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
                    "No. of Children - Still Living"
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
            });

            residents.ForEach(resident =>
            {
                var row = residents.IndexOf(resident) + 2;
                xlsSheet[$"D{row}"].Value = resident.FullName;

                xlsSheet[$"H{row}"].Value = resident.DateOfBirth.ToString("MM/dd/yyyy");
                xlsSheet[$"I{row}"].Value = resident.Age;
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
