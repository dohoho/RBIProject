﻿using System;
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
        // number of inspections
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog table 9.2
        public String inspectionCatalog { set; get; }
        // do pH cua nuoc
        public double pH { set; get; }
        // xac dinh he thong co bi gay hay k?
        public bool crackPresent { set; get; }
        // xac dinh chi so ppm
        public double ppm { set; get; }

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
        //calculate Svi
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
        private int D_carbonate_fB()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(Svi(), field);
        }
        /************** Result: D_carbonate_f **************/
        public double D_carbonate_f()
        {
            return D_carbonate_fB() * Math.Pow(age, 1.1);
        }
    }
}
