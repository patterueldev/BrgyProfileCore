using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace BrgyProfileCore.Core
{
    class BrgyStatistics
    {
        private static BrgyContext db = new BrgyContext();
        private static List<Resident> residents = new List<Resident>();
        
        public static void RefreshStatistics()
        {
            residents = db.Residents.ToList();
        }
        public static int TotalResidentsByAge(int min, int max = 0)
        {
            return residents.Where(r => r.GetAge() >= min && (max == 0 || r.GetAge() <= max)).Count();
        }
        public static int AverageResidentPerHousehold {
            get {
                var Households = db.Households.Include(h => h.Residents).ToList();
                var totalResidents = Households.Select(h => h.Residents.Count).Sum();
                var totalHouseholds = Households.Count;
                if(totalHouseholds == 0)
                {
                    return 0;
                }
                return totalResidents/totalHouseholds;
            }
        }
        public static int AveragePopulationperSitio
        {
            get
            {
                var Sitio = db.Sitio.Include(h => h.Residents).ToList();
                var totalResidents = Sitio.Select(h => h.Residents.Count).Sum();
                var totalSitio = Sitio.Count;
                if (totalSitio == 0)
                {
                    return 0;
                }
                return totalResidents / totalSitio;
            }
        }
    }
}
