using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class sccDamageFactor_CacbonateCracking
    {
        public double age { set; get; }
        // number of inspections
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog table 9.2
        public String inspectionCatalog { set; get; }
        // do pH cua nuoc
        public double pH { set; get; }
        // xac dinh he thong co bi gay hay k?
        public bool crackPresent { set; get; }
        // xac dinh PWHT
        public double PWHT { set; get; }
        // xac dinh chi so ppm
        public double ppm { set; get; }
        public String Susceptibility { set; get; }
        public int D_cacbonate_fB { set; get; }
        public int svi()
        {
            if (crackPresent)
                return 1000;
            else
            {
                if (Susceptibility == "High") return 1000;
                if (Susceptibility == "Medium") return 100;
                if (Susceptibility == "Low") return 10;
                else return 1;
            }
        }
        public double result()
        {
            return D_cacbonate_fB * Math.Pow(age, 1.1);
        }
    }
}
