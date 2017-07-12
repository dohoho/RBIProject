using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class sccDamageFactor_SLSCC
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // age: thoi gian trong he thong ke tu lan cuoi cung dat duoc level A,B,C,D
        public double age { set; get; }
        // number of inspections
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog table 9.2
        public String inspectionCatalog { set; get; }
        // do pH cua nuoc
        public double pH { set; get; }
        // xac dinh he thong co bi gay hay k?
        public bool crackPresent { set; get; }
        // xac dinh Temperature( *F)
        public double Temperature { set; get; }
        // xac dinh chi so nong do Clorua (ppm)
        public double ppm { set; get; }

        //B1: Xac dinh muc do bang bang 13.3
        private String SLSCC()
        {
            String temString = null;
            String phString = null;
            String field = null;

            if (ppm >= 1 && ppm <= 10)
                field = "1-10";
            else if (ppm >= 11 && ppm <= 100)
                field = "11-100";
            else if (ppm >= 101 && ppm <= 1000)
                field = "101-1000";
            else
                field = ">1000";


            if(pH <= 10)
            {
                phString = "<=10";
                if (Temperature >= 100 && Temperature <= 150)
                    temString = "100-150";
                else if (Temperature > 150 && Temperature <= 200)
                    temString = ">150-200";
                else
                    temString = ">200-300";
            }
            else
            {
                phString = ">10";
                if(Temperature < 200)
                {
                    temString = "<200";
                }
                else
                {
                    temString = "200-300";
                }
            }

            return rbi.getCLSCC(temString, phString, field);
        }

        // B2: xac dinh SVI
        private int SVI()
        {
            int svi;
            String level = SLSCC();

            if (crackPresent == true)
                svi = 5000;
            else
            {
                if (level.Equals("High"))
                    svi = 5000;
                else if (level.Equals("Medium"))
                    svi = 500;
                else if (level.Equals("Low"))
                    svi = 50;
                else
                    svi = 1;
            }
            return svi;
        }

        //B3: xac dinh D_fb_slscc bang table 7.4
        private int D_fb_slscc()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B4: tinh toan D_f_slscc
        public double D_f_slscc()
        {
            return D_fb_slscc() * Math.Pow(age, 1.1);
        }
    }
}
