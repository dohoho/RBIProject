using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class sccDamageFactor_CausticCracking
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // khoang thoi gian trong he thong ke tu thoi diem kiem tra cuoi cung
        public double age { set; get; }
        // chi so inspection catalog( table 7.2)
        public String inspectionCatalog { set; get; }
        // so lan kiem tra( inspection)
        public int inspection { set; get; }
        // xac dinh muc do cracking: high, level, low, none
        public String levelCracking { set; get; }

        //B1: xac dinh SVI (table 7.3)
        private int SVI()
        {
            int svi;
            if (levelCracking.Equals("High"))
                svi = 5000;
            else if (levelCracking.Equals("Medium"))
                svi = 500;
            else if (levelCracking.Equals("Low"))
                svi = 50;
            else svi = 1;
            return svi;
        }
        // B2: xac dinh D_f_caustic tu bang 7.4
        private int D_f_caustic()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        //B3: xac dinh D_f tu D_f_caustic va age
        public double D_f()
        {
            return D_f_caustic() * Math.Pow(age, 1.1);
        }
    }
}
