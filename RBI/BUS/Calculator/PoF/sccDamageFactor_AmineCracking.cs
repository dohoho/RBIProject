using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class sccDamageFactor_AmineCracking
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // khoang thoi gian trong he thong ke tu lan cuoi dat level A,B,C,D
        public double age { set; get; }
        // numer of inspections
        public int inspection { set; get; }
        // chi so inspectionCatalog
        public String inspectionCatalog { set; get; }
        // chi so levelCracking
        public String levelCracking { set; get; }

        //B1: xac dinh SVI( table 8.3)
        private int SVI()
        {
            int svi;
            if (levelCracking.Equals("High"))
                svi = 1000;
            else if (levelCracking.Equals("Medium"))
                svi = 100;
            else if (levelCracking.Equals("Low"))
                svi = 10;
            else
                svi = 1;
            return svi;
        }
        // B2: xac dinh D_f_amine( table 7.4)
        private int D_fb_amine()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        //B3: Xac dinh D_f theo cong thuc
        public double D_f_amine()
        {
            return D_fb_amine() * Math.Pow(age, 1.1);
        }
    }
}
