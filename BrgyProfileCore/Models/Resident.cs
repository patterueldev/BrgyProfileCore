using System;
using System.Collections.Generic;
using System.Text;
namespace BrgyProfileCore
{
    public class Resident  
    {
        public int ResidentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressStreet { get; set; }
        public string AddressSubdivision { get; set; }
        public string ContactNumber { get; set; }

        public int? HouseholdId { get; set; }
        public Household Household { get; set; }

        public int? SitioId { get; set; }
        public Sitio Sitio { get; set; }

        // MARK: Additional Info
        public string Gender { get; set; }
        public string HighestEducationalAttainment { get; set; }
        public string Religion { get; set; }
        public string Citizenship { get; set; }
        public string Occupation { get; set; }
        
        public int FamilyNo { get; set; }
        public int LineNo { get; set; }
        public string RelationshiptoHHHead { get; set; }

        //F
        public string MaritalStatus { get; set; }

        // K
        public string Grade_YearLevelofSchoolAttendance { get; set; }
        public string ReasonforDroppingOutofSchool { get; set; }
        public string SpecialSkills { get; set; }
        public string Disability { get; set; }

        //P
        public string IndigenousPeopleMembership { get; set; }
        public string PHICMembershipSponsor { get; set; }
        public string NHTS { get; set; }
        public string Four_Ps { get; set; }
        public string MajorOccupationofEarningHHMember { get; set; }

        //U
        public string OtherSourceofIncome { get; set; }
        public string TotalActualIncomeofEarningMember { get; set; }
        public string HouseOwnership { get; set; }
        public string GeohazardLocation { get; set; }
        public string WaterLevel { get; set; }

        //Z
        public string LotOwnershipwhereHouseisLocated { get; set; }
        public string TypeofFuel_Lighting { get; set; }
        public string TypeofFuel_Cooking { get; set; }
        public string SourceofWaterforDrinking { get; set; }
        public string DistanceofWaterSourcefromHouse { get; set; }

        //AE
        public string SourceofWaterforGeneralUse { get; set; }
        public string TypeofToilet { get; set; }
        public string TypeofGarbageDisposal { get; set; }
        public string No_ofChildren_BornAlive { get; set; }
        public string No_ofChildren_StillLiving { get; set; }
        
        // More Fields
        public string FamilyPlanningPracticeMethodUsed { get; set; }
        public string FamilyPlanningPracticeIntensiontoUse { get; set; }
        public string FamilyPlanningPracticeReasonforNotUsing { get; set; }
        public string Remarks { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        public int GetAge()
        {
            if (DateOfBirth != null)
            {
                var now = DateTime.Now;
                int years = DateTime.Now.Year - DateOfBirth.Year;
                if (now.Month < DateOfBirth.Month || now.Day < DateOfBirth.Day)
                {
                    years -= 1;
                }
                return years;
            }
            return 0;
        }

        public string HouseholdName
        {
            get
            {
                if(Household == null)
                {
                    return "N/A";
                }
                return Household.HouseholdName;
            }
        }

        public string SitioName
        {
            get
            {
                if (Sitio == null)
                {
                    return "N/A";
                }
                return Sitio.SitioName;
            }
        }
    }
}
