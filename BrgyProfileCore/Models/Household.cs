using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BrgyProfileCore
{
    public class Household
    {
        public int HouseholdId { get; set; }
        public string HouseholdName { get; set; }
        public int? HeadResidentId { get; set; }

        public List<Resident> Residents { get; } = new List<Resident>();

        public string HouseholdHead
        {
            get
            {
                if (Residents == null)
                {
                    return "N/A";
                }
                if (HeadResidentId != null && Residents.Count > 0)
                {
                    var resident = Residents.First(res =>
                    {
                        return res.ResidentId == this.HeadResidentId;
                    });
                    if (resident != null)
                    {
                        return resident.FullName;
                    }
                }
                return "N/A";
            }
        }
        public int ResidentsCount
        {
            get
            {
                if (Residents == null)
                {
                    return 0;
                }
                return Residents.Count;
            }
        }

        public bool HasResidents()
        {
            if (this.Residents != null)
            {
                return this.Residents.Count > 0;
            }
            return true;
        }
    }
}
