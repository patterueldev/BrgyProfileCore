using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpreadsheetLight;
using System.Windows.Controls;
using System.IO;
using System.Reflection;
using System.Printing;
using System.Diagnostics;
using PDFtoPrinter;

namespace BrgyProfileCore.Core
{
    
    static class PrintHelper
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
        public static void ExportResidentsSheet(List<Resident> residents, string filename = "Residents.xls")
        {
            //Create new Excel WorkBook document. 
            //The default file format is XLSX, but we can override that for legacy support
            var sl = new SLDocument();
            sl.RenameWorksheet("Sheet1", "Residents");
            //sl.AddWorksheet("Residents");
            //sl.SelectWorksheet("Residents");

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
                var householdNo = "";
                if (resident.HouseholdId != null)
                {
                    householdNo = $"{householdNo}";
                }
                sl.SetCellValue($"A{row}", householdNo);
                sl.SetCellValue($"D{row}", resident.FullName);
                sl.SetCellValue($"E{row}", resident.RelationshiptoHHHead);

                sl.SetCellValue($"F{row}", resident.Gender);
                sl.SetCellValue($"G{row}", resident.MaritalStatus);
                sl.SetCellValue($"H{row}", resident.DateOfBirth.ToString("MM/dd/yyyy"));
                sl.SetCellValue($"I{row}", resident.GetAge());
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

        public static void ExportRBISheet(List<Resident> residents, string filename = "RBI.xls")
        {
            var sl = new SLDocument();
            sl.RenameWorksheet("Sheet1", "Household");

            SLStyle style = sl.CreateStyle();
            style.SetWrapText(true);
            style.Alignment.Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
            style.Alignment.Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center;

            //// Headers

            ///// NAME HEADER
            sl.MergeWorksheetCells("A1", "D1");
            sl.SetCellValue("A1", "NAME");
            sl.SetCellStyle("A1", style);

            sl.MergeWorksheetCells("A2", "D2");
            sl.SetCellValue("A2", "1");
            sl.SetCellStyle("A2", style);

            sl.SetCellValue("A3", "LAST");
            sl.SetCellValue("B3", "FIRST");
            sl.SetCellValue("C3", "MIDDLE");
            sl.SetCellValue("D3", "QUALIFIER");
            sl.SetCellStyle("A3", "D3", style);
            sl.AutoFitColumn("A3", "D3", 50);

            ///// ADDRESS HEADER
            sl.MergeWorksheetCells("E1", "G1");
            sl.SetCellValue("E1", "ADDRESS");
            sl.SetCellStyle("E1", style);

            sl.MergeWorksheetCells("E2", "G2");
            sl.SetCellValue("E2", "2");
            sl.SetCellStyle("E2", style);

            sl.SetCellValue("E3", "NUMBER");
            sl.SetCellValue("F3", "STREET NAME");
            sl.SetCellValue("G3", "NAME OF SITIO");
            sl.SetCellStyle("E3", "G3", style);
            sl.AutoFitColumn("E3", "G3", 50);
            sl.SetCellValue("E1", "ADDRESS");
            sl.SetCellStyle("E1", style);

            // Place of Birth // H
            sl.SetCellValue("H1", "PLACE OF BIRTH\n\n(3)");
            sl.SetCellStyle("H1", style);
            sl.SetCellStyle("H1", style);
            sl.MergeWorksheetCells("H1", "H3");

            // Date of Birth // I
            sl.SetCellValue("I1", "DATE OF BIRTH\n\n(4)");
            sl.SetCellStyle("I1", style);
            sl.AutoFitColumn("I1", 50);
            sl.MergeWorksheetCells("I1", "I3");

            // Sex // J
            sl.SetCellValue("J1", "SEX\n\n(5)");
            sl.SetCellStyle("J1", style);
            sl.AutoFitColumn("J1", 50);
            sl.MergeWorksheetCells("J1", "J3");

            // Civil // K
            sl.SetCellValue("K1", "CIVIL\n\n(6)");
            sl.SetCellStyle("K1", style);
            sl.AutoFitColumn("K1", 50);
            sl.MergeWorksheetCells("K1", "K3");

            // Citizenship // L
            sl.SetCellValue("L1", "CITIZENSHIP\n\n(7)");
            sl.SetCellStyle("L1", style);
            sl.AutoFitColumn("L1", 50);
            sl.MergeWorksheetCells("L1", "L3");

            // Occupation // M
            sl.SetCellValue("M1", "OCCUPATION\n\n(7)");
            sl.SetCellStyle("M1", style);
            sl.AutoFitColumn("M1", 50);
            sl.MergeWorksheetCells("M1", "M3");

            // Relationship with the Head // N
            sl.SetCellValue("N1", "RELATIONSHIP WITH THE HEAD OF THE FAMILY");
            sl.SetCellStyle("N1", style);
            sl.AutoFitColumn("N1", 30);
            sl.MergeWorksheetCells("N1", "N4");

            ////Add data and styles to the new worksheet
            residents.ForEach(resident =>
            {
                var row = residents.IndexOf(resident) + 5;
                sl.SetCellValue($"A{row}", resident.LastName);
                sl.SetCellValue($"B{row}", resident.FirstName);
                sl.SetCellValue($"C{row}", resident.MiddleName);

                sl.SetCellValue($"E{row}", resident.AddressNumber);
                sl.SetCellValue($"F{row}", resident.AddressStreet);
                sl.SetCellValue($"G{row}", resident.AddressSubdivision);

                sl.SetCellValue($"H{row}", resident.PlaceOfBirth);
                sl.SetCellValue($"I{row}", resident.DateOfBirth.ToString("MM/dd/yyyy"));

                if (resident.Gender != null && resident.Gender.Trim() != "")
                {
                    sl.SetCellValue($"J{row}", resident.Gender[0].ToString());
                }

                sl.SetCellValue($"K{row}", resident.MaritalStatus);
                sl.SetCellValue($"L{row}", resident.Citizenship);
                sl.SetCellValue($"M{row}", resident.Occupation);
                sl.SetCellValue($"N{row}", resident.RelationshiptoHHHead);
            });
            //Save the excel file
            sl.SaveAs(filename);
        }
        public static void PrintResidentsToPDF(List<Resident> residents, string filename, bool willprint, string printername = null)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.PaperOrientation = IronPdf.PdfPrintOptions.PdfPaperOrientation.Landscape;

            string curDir = Directory.GetCurrentDirectory();
            string myFile = Path.Combine(curDir, "BHI-Template.html");

            string html = System.IO.File.ReadAllText(myFile);

            var tableRowsBuilder = new StringBuilder();
            residents.ForEach(r =>
            {
                var row = @$"
                <tr>
                    <td>{r.FullName}</td>
                    <td>{r.Gender}</td>
                    <td>{r.MaritalStatus}</td>
                    <td>{r.DateOfBirth.ToString("MM/dd/yyyy")}</td>

                    <td>{r.HighestEducationalAttainment}</td>
                    <td>{r.Religion}</td>
                </tr>";
                tableRowsBuilder.Append(row);
            });

            var processed = Helpers.ReplaceString(html,
                "<!-- sample -->",
                "<!-- end sample-->",
                tableRowsBuilder.ToString());

            var PDF = Renderer.RenderHtmlAsPdf(processed);

            if (!willprint)
            {
                PDF.SaveAs(filename);
            }
            else
            {
                string Fname = "brgypdftemp.pdf";
                string folderPath = System.IO.Path.GetTempPath();
                string pdfPath = Path.Combine(folderPath, Fname);
                PDF.SaveAs(pdfPath);

                var printer = new PDFtoPrinterPrinter();
                var options = new PrintingOptions(printername, pdfPath);
                printer.Print(options);
            }
        }

        public static void PrintHouseholdToPDF(List<Resident> residents, string filename, bool willprint, string printername = null)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.PaperOrientation = IronPdf.PdfPrintOptions.PdfPaperOrientation.Landscape;

            string curDir = Directory.GetCurrentDirectory();
            string myFile = Path.Combine(curDir, "RBI-Template.html");

            string html = System.IO.File.ReadAllText(myFile);

            var tableRowsBuilder = new StringBuilder();
            residents.ForEach(r =>
            {
                var row = @$"
                <tr>
                    <td>{r.LastName}</td>
                    <td>{r.FirstName}</td>
                    <td>{r.MiddleName}</td>
                    <td></td>
                    
                    <td>{r.AddressNumber}</td>
                    <td>{r.AddressStreet}</td>
                    <td>{r.AddressSubdivision}</td>
                    
                    <td>{r.PlaceOfBirth}</td>
                    <td>{r.DateOfBirth.ToString("MM/dd/yyyy")}</td>
                    <td>{r.Gender}</td>
                    <td>{r.MaritalStatus}</td>
                    <td>{r.Citizenship}</td>
                    <td>{r.Occupation}</td>
                    
                    <td>{r.RelationshiptoHHHead}</td>
                </tr>";
                tableRowsBuilder.Append(row);
            });

            var settings = Properties.Settings.Default;

            var processed = Helpers.ReplaceString(html, 
                "<!-- sample -->", 
                "<!-- end sample-->", 
                tableRowsBuilder.ToString());

            processed = Helpers.ReplaceString(processed, 
                "<!-- Province Name -->", 
                "<!-- End Province Name -->", 
                settings.Province.ToUpper());
            processed = Helpers.ReplaceString(processed,
                "<!-- Municipality Name -->",
                "<!-- End Municipality Name -->",
                settings.Municipality.ToUpper());
            processed = Helpers.ReplaceString(processed,
                "<!-- Brgy Name -->",
                "<!-- End Brgy Name -->",
                settings.BrgyName.ToUpper());

            processed = Helpers.ReplaceString(processed,
                "<!-- PreparedBy -->",
                "<!-- EndPreparedBy -->",
                settings.RBI_PreparedBy.ToUpper());
            processed = Helpers.ReplaceString(processed,
                "<!-- CertifiedCorrect -->",
                "<!-- EndCertifiedCorrect -->",
                settings.RBI_CertifiedCorrected.ToUpper());
            processed = Helpers.ReplaceString(processed,
                "<!-- ValidatedBy -->",
                "<!-- EndValidatedBy -->",
                settings.RBI_ValidatedBy.ToUpper());

            processed = Helpers.ReplaceString(processed,
                "<!-- PreparedByTitle -->",
                "<!-- EndPreparedByTitle -->",
                settings.RBI_PreparedByTitle);
            processed = Helpers.ReplaceString(processed,
                "<!-- CertifiedCorrectTitle -->",
                "<!-- EndCertifiedCorrectTitle -->",
                settings.RBI_CertifiedCorrectedTitle);
            processed = Helpers.ReplaceString(processed,
                "<!-- ValidatedByTitle -->",
                "<!-- EndValidatedByTitle -->",
                settings.RBI_ValidatedByTitle);

            var PDF = Renderer.RenderHtmlAsPdf(processed);

            if (!willprint)
            {
                PDF.SaveAs(filename);
            }
            else
            {
                string Fname = "brgypdftemp.pdf";
                string folderPath = System.IO.Path.GetTempPath();
                string pdfPath = Path.Combine(folderPath, Fname);
                PDF.SaveAs(pdfPath);

                var printer = new PDFtoPrinterPrinter();
                var options = new PrintingOptions(printername, pdfPath);
                printer.Print(options);
            }
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
