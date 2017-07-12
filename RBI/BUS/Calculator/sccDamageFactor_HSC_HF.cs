using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class sccDamageFactor_HSC_HF
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // age: thoi gian trong he thong ke tu lan cuoi cung dat duoc level A,B,C,D
        public double age { set; get; }
        // number of inspections
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog table 9.2
        public String inspectionCatalog { set; get; }
        // HF present?
        public bool hfPresent { set; get; }
        // xac dinh he thong co bi gay hay k?
        public bool crackPresent { set; get; }
        // cacbonate steel?
        public bool cacbonateSteel { set; get; }
        // PWHT?
        public bool PWHT { set; get; }
        // Brinell Hardness
        public double BrinellHardness { set; get; }

        // B1: xac dinh muc do cracking HSC_HF su dung bang 14.3
        private String getHSC_HF()
        {
            String hsc_hf= null;
            String field = null;

            if (BrinellHardness < 200)
                field = "<200";
            else if (BrinellHardness >= 200 && BrinellHardness <= 237)
                field = "200-237";
            else
                field = ">237";


            if (crackPresent)
                hsc_hf = "High";
            else
            {
                if (hfPresent)
                {
                    if (cacbonateSteel)
                        hsc_hf=rbi.getHSC_HF(PWHT, field);
                    else
                        hsc_hf = "None";
                }
                else
                    hsc_hf = "None";
            }
            return hsc_hf;
        }
        // B2: xac dinh SVI theo table 14.4
        private int SVI()
        {
            String hsc_hf = getHSC_HF();
            int svi;
            if (hsc_hf.Equals("High"))
                svi = 100;
            else if (hsc_hf.Equals("Medium"))
                svi = 10;
            else
                svi = 1;
            return svi;
        }
        // B3: xac dinh D_fb_hsc tu bang 7.4
        private int D_fb_hsc()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B4: tinh D_f_hsc theo cong thuc
        public double D_f_hsc()
        {
            return D_fb_hsc() * Math.Pow(age, 1.1);
        }
    }
}
