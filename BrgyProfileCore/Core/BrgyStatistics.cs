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
        public static int AverageResident1_18perSitio
        {
            get
            {
                var Sitio = db.Sitio.Include(h => h.Residents).ToList();
                var totalResidents = Sitio.Select<Sitio, int>(h =>
                {
                    var residents = h.Residents.Where(r => {
                        return r.Age >= 0 && r.Age <= 18;
                    }).ToList();
                    return residents.Count;
                }).Sum();
                var totalSitio = Sitio.Count;
                if (totalSitio == 0)
                {
                    return 0;
                }
                return totalResidents / totalSitio;
            }
        }
        public static int AverageResident19_50perSitio
        {
            get
            {
                var Sitio = db.Sitio.Include(h => h.Residents).ToList();
                var totalResidents = Sitio.Select<Sitio, int>(h =>
                {
                    var residents = h.Residents.Where(r => {
                        return r.Age >= 19 && r.Age <= 50;
                    }).ToList();
                    return residents.Count;
                }).Sum();
                var totalSitio = Sitio.Count;
                if (totalSitio == 0)
                {
                    return 0;
                }
                return totalResidents / totalSitio;
            }
        }
        public static int AverageResident51_AboveperSitio
        {
            get
            {
                var Sitio = db.Sitio.Include(h => h.Residents).ToList();
                var totalResidents = Sitio.Select<Sitio, int>(h =>
                {
                    var residents = h.Residents.Where(r => {
                        return r.Age >= 51;
                    }).ToList();
                    return residents.Count;
                }).Sum();
                var totalSitio = Sitio.Count;
                if (totalSitio == 0)
                {
                    return 0;
                }
                return totalResidents / totalSitio;
            }
        }
    }

    class Helper
    {

    }
}
