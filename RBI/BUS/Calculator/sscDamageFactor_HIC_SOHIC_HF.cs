using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class sscDamageFactor_HIC_SOHIC_HF
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public double age { set; get; } //[years]
        public int inspection { set; get; } // number of inspections
        public String inspectionCatalog { set; get; } // xac dinh he so inspectionCatalog table 9.2
        public bool crackPresent { set; get; } // xac dinh he thong co bi gay hay k?
        public bool IsPWHT {set; get;} //PWHT or As-Welded
        public bool HFpresent { set; get; }
        public double ppm { set; get; } // xac dinh chi so ppm

        private string getSusceptibility()
        {
            string _ppm = null;
            if (HFpresent)
            {
                if (ppm > 0.01) _ppm = ">0.01";
                else if (ppm >= 0.002 && ppm <= 0.01) _ppm = "0.002-0.01";
                else _ppm = "<0.002";
                return rbi.getSusHF(_ppm, IsPWHT);
            }
            else return "None"; 
        }
        private int Svi()
        {
            int svi = 0;
            if (crackPresent)
            {
                svi = 100;
            }
            else
            {
                switch (getSusceptibility())
                {
                    case "High": svi = 100; break;
                    case "Medium": svi = 10; break;
                    default: svi = 1; break;
                }
            }
            return svi;
        }
        public double D_HIC_HF_f()
        {
            string field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(Svi(), field) * Math.Pow(age, 1.1);
        }
    }
}
