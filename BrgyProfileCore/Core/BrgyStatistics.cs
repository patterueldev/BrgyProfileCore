using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace BrgyProfileCore.Core
{
    struct MinMaxPair
    {
        public int min;
        public int max;
    }

    struct SitioResidentReport
    {
        public struct ResidentsRange
        {
            public string rangeTitle;
            public int residents;
        }

        public string sitio;
        public List<ResidentsRange> ranges;
    }
    class BrgyStatistics
    {
        private static BrgyContext db = new BrgyContext();
        private static List<Resident> residents = new List<Resident>();
        private static List<Sitio> sitio = new List<Sitio>();
        private static List<Household> household = new List<Household>();

        public static void RefreshStatistics()
        {
            residents = db.Residents.ToList();
            sitio = db.Sitio.Include(s => s.Residents).ToList();
            household = db.Households.Include(h => h.Residents).ToList();
        }
        public static int TotalResidentsByAge(int min, int max = 0)
        {
            return residents.Where(r => r.Age >= min && (max == 0 || r.Age <= max)).Count();
        }
        private static int TotalSitioResidentsByAge(Sitio s, int min, int max = 0)
        {
            if(s == null)
            {
                return residents.Where(r => r.Sitio == null).Where(r => r.Age >= min && (max == 0 || r.Age <= max)).Count();
            }
            return s.Residents.Where(r => r.Age >= min && (max == 0 || r.Age <= max)).Count();
        }
        public static List<SitioResidentReport> SitioResidentsByAgeReport()
        {
            var pairs = new List<MinMaxPair>()
            {
                new MinMaxPair{ min = 0, max = 3 },
                new MinMaxPair{ min = 4, max = 6 },
                new MinMaxPair{ min = 7, max = 11 },
                new MinMaxPair{ min = 12, max = 20 },
                new MinMaxPair{ min = 21, max = 35 },
                new MinMaxPair{ min = 36, max = 50 },
                new MinMaxPair{ min = 51, max = 80 },
                new MinMaxPair{ min = 81, max = 0 },
            };

            var reports = new List<SitioResidentReport>();

            sitio.ForEach(s =>
            {
                var ageRanges = new List<SitioResidentReport.ResidentsRange>();
                foreach (var p in pairs)
                {
                    ageRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = p.max > 0 ? $"{p.min} to {p.max} yrs" : $"{p.min} yrs and above",
                        residents = TotalSitioResidentsByAge(s, p.min, p.max)
                    });
                }

                reports.Add(
                    new SitioResidentReport
                    {
                        sitio = s.SitioName,
                        ranges = ageRanges
                    });
            });

            var unspecified = residents.Where(r => r.Sitio == null).Count();
            if (unspecified > 0)
            {
                var ageRanges = new List<SitioResidentReport.ResidentsRange>();
                pairs.ForEach(p =>
                {
                    ageRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = p.max > 0 ? $"{p.min} to {p.max} yrs" : $"{p.min} yrs and above",
                        residents = TotalSitioResidentsByAge(null, p.min, p.max)
                    });
                });
                reports.Add(
                    new SitioResidentReport
                    {
                        sitio = "Unspecified",
                        ranges = ageRanges
                    });
            }
            return reports;
        }
        public static List<SitioResidentReport> SitioResidentsByMaritalStatusReport()
        {
            var statuses = new List<string>()
            {
                "Single",
                "Married",
                "Divorced",
                "Widowed",
                "Separated",
            };

            var reports = new List<SitioResidentReport>();

            sitio.ForEach(s =>
            {
                var maritalRanges = new List<SitioResidentReport.ResidentsRange>();
                foreach (var status in statuses)
                {
                    maritalRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = status,
                        residents = s.Residents.Where(r => r.MaritalStatus == status).Count()
                    });
                }

                var unspecified = s.Residents.Where(r => r.MaritalStatus == "").Count();
                if (unspecified > 0)
                {
                    maritalRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = "Unspecified Status",
                        residents = unspecified
                    });
                }

                reports.Add(
                    new SitioResidentReport
                    {
                        sitio = s.SitioName,
                        ranges = maritalRanges
                    });
            });

            var unspecified = residents.Where(r => r.Sitio == null).Count();
            if (unspecified > 0)
            {
                var maritalRanges = new List<SitioResidentReport.ResidentsRange>();
                statuses.ForEach(status =>
                {
                    maritalRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = status,
                        residents = residents.Where(r => r.Sitio == null).Where(r => r.MaritalStatus == status).Count()
                    });


                });
                var unspecifiedStatus = residents.Where(r => r.Sitio == null).Where(r => r.MaritalStatus == "").Count();
                if (unspecifiedStatus > 0)
                {
                    maritalRanges.Add(new SitioResidentReport.ResidentsRange
                    {
                        rangeTitle = "Unspecified Status",
                        residents = unspecified
                    });
                }
                reports.Add(
                    new SitioResidentReport
                    {
                        sitio = "Unspecified",
                        ranges = maritalRanges
                    });
            }
            return reports;
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
