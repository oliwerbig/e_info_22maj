using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_info_22maj
{
    internal class Owner
    {
        public int TaxID;
        public List<Plot> Plots;

        public Owner(int taxID)
        {
            TaxID = taxID;     
            Plots = new List<Plot>();
        }
    }
}
