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
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Guardian { get; set; }

        public int? HouseholdId { get; set; }
        public Household Household { get; set; }

        public int? SitioId { get; set; }
        public Sitio Sitio { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        public int Age
        {
            get
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
    }
}
