using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
using System.Diagnostics;
namespace RBI.BUS.Calculator
{
    class sccDamageFactor_CacbonateCracking
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public double age { set; get; }
        public int inspection { set; get; } // number of inspections
        public String inspectionCatalog { set; get; } // xac dinh he so inspectionCatalog table 9.2
        public double pH { set; get; } // do pH cua nuoc
        public bool crackPresent { set; get; }  // xac dinh he thong co bi gay hay k?
        public double ppm { set; get; } // nồng độ của CO3 trong nước
        //Step 1: Determine the number of inspections, and the corresponding inspection effectiveness category
        //Step 2: Determine the time in-service, age , since the last Level A, B, C or D inspection was performed
        //Step 3: Determine the susceptibility for cracking
        private String susceptibility()
        {
            String _pH = null;
            if (pH >= 7.6 && pH <= 8.3) _pH = "7.6-8.3";
            else if (pH > 8.3 && pH < 9.0) _pH = "8.3-9.0";
            else _pH = "=>9.0";
            String _ppm = null;
            if (ppm < 100) _ppm = "<100";
            else if (ppm >= 100 && ppm <= 500) _ppm = "100-500";
            else if (ppm > 500 && ppm <=1000) _ppm = "500-1000";
            else _ppm = ">1000";
            return rbi.getSusCarbonate(_pH, _ppm);
        }
        //Step 4: Determine the severity index, Svi
        private int Svi()
        {
            int svi = 0;
            if (crackPresent)
                svi = 1000;
            else
            {
                switch (susceptibility())
                {
                    case "High": svi = 1000; break;
                    case "Medium": svi = 100; break;
                    case "Low": svi = 10; break;
                    default: svi = 1; break;
                }
            }
            return svi;
        }
        //Step 5: Determine the base damage factor for carbonate cracking, D_carbonate_fB
        private int D_carbonate_fB()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(Svi(), field);
        }
        //Step 6: Calculate D_carbonate_f
        public double D_carbonate_f()
        {
            return D_carbonate_fB() * Math.Pow(age, 1.1);
        }
    }
}
