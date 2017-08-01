using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class sccDamageFactor_pta_cracking
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // age: thoi gian trong he thong ke tu lan cuoi cung dat duoc level A,B,C,D
        public double age { set; get; }
        // number of inspections
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog table 9.2
        public String inspectionCatalog { set; get; }
        // HF present?
        public bool crackPresent { set; get; }
        // cacbonate steel?
        public bool cacbonateSteel { set; get; }
        // PWHT?
        public bool PWHT { set; get; }
        // material
        public String material { set; get; }
        // function of heat treatment
        public String fHT { set; get; }
        // operating temperature
        public double opTemp { set; get; }

        // B1: xac dinh muc do cracking HSC_HF su dung bang 14.3
        private String getsscp_pta()
        {
            String heatTreatment;
            if(opTemp < 427)
                heatTreatment = fHT + " (<427oC)";
            else
                heatTreatment = fHT + " (>=427oC)";
            
            return rbi.getSusceptibilityPTACracking(material, heatTreatment);
        }
        // B2: xac dinh SVI theo table 14.4
        private int SVI()
        {
            String sscp_pta = getsscp_pta();
            int svi;
            if (sscp_pta.Equals("High"))
                svi = 5000;
            else if (sscp_pta.Equals("Medium"))
                svi = 500;
            else if (sscp_pta.Equals("Low"))
                svi = 50;
            else
                svi = 1;
            return svi;
        }
        // B3: xac dinh D_fb_hsc tu bang 7.4
        private int D_fb_pta()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B4: tinh D_f_hsc theo cong thuc
        public double D_f_pta()
        {
            return D_fb_pta() * Math.Pow(age, 1.1);
        }
    }
    
}
