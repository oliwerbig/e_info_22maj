using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_info_22maj
{
    internal class Plot
    {
        public string Street;
        public string Address;
        public Classification Classification;
        public int BuildingArea;

        public Plot(string street, string address, Classification classification, int buildingArea)
        {
            Street = street;
            Address = address;
            Classification = classification;
            BuildingArea = buildingArea;
        }
    }
}
