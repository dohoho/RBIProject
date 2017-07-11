using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class sccDamageFactor_SulfideStressCracking
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
        // xac dinh PWHT
        public double PWHT { set; get; }
        // xac dinh chi so ppm
        public double ppm { set; get; }
        // B1: xac dinh do anh huong toi moi truong( table 9.3)
        private String EnvSeverity()
        {
            String ppmString = null;
            if (ppm < 50)
                ppmString = "<50";
            else if (ppm >= 50 && ppm < 1000)
                ppmString = "50-1000";
            else if (ppm >= 1000 && ppm <= 10000)
                ppmString = "1000-10000";
            else
                ppmString = ">10000";

            String pHString = null;
            if (pH < 5.5)
                pHString = "<5.5";
            else if (pH >= 5.5 && pH <= 7.5)
                pHString = "5.5-7.5";
            else if (pH >= 7.6 && pH <= 8.3)
                pHString = "7.6-8.3";
            else if (pH >= 8.4 && pH <= 8.9)
                pHString = "8.4-8.9";
            else
                pHString = ">9.0";
            return rbi.getEnvironmental(ppmString, pHString);
        }
        // B2: xac dinh scc tu bang 9.4
        private String SSC()
        {
            String pwhtString = null;
            if (PWHT < 200)
                pwhtString = "<200";
            else if (PWHT >= 200 && PWHT <= 237)
                pwhtString = "200-237";
            else
                pwhtString = ">237";
            return rbi.getSulfideStressCracking(EnvSeverity(), pwhtString);
        }
        // B3: tu B2- xac dinh SVI
        private int SVI()
        {
            String data = SSC();
            int svi;
            if (crackPresent == true)
            {
                svi = 100;
            }
            else
            {
                if (data.Equals("High"))
                    svi = 100;
                else if (data.Equals("Medium"))
                    svi = 10;
                else if (data.Equals("Low"))
                    svi = 1;
                else
                    svi = 1;
            }
            return svi;
        }
        //B4: tu buoc 3 tra bang 7.4 ra duoc he so D_fb_ssc
        private int D_fb_ssc()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B5: tinh ket qua cuoi cung D_f_ssc
        public double D_f_ssc()
        {
            return D_fb_ssc() * Math.Pow(age, 1.1);
        }
    }
}
