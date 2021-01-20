using System;
using System.Collections.Generic;
using System.Text;

namespace BrgyProfileCore
{
    public class Sitio
    {
        public int SitioId { get; set; }
        public string SitioName { get; set; }

        public List<Resident> Residents { get; } = new List<Resident>();
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
