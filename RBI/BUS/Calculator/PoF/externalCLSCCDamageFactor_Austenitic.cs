using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class externalCLSCCDamageFactor_Austenitic
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public double CD { set; get; }
        // CD: Calculation date      
        public double CID { set; get; }
        // CID: component installation date     
        public int NoInspection { set; get; }
        // NoInspection: number of inspections 
        public String IECategory { set; get; }
        // IECategory : effectiveness category that has been performed on the component
        public bool crackPresent { set; get; }
        // Determine whether cracking is known
        public String driver { set; get; }
        // driver : driver for external CLSCC 
        public String CQ { set; get; }
        // CQ : coating quality
        public double opTemp { set; get; }
        // operating temperature
        // Step 2 : Determine age
        private double age()
        {
            double Date = 0;
            double age;
            if (CQ.Equals("No Coating")||CQ.Equals("Poor Coating Quality"))
                Date = CID;
            else if(CQ.Equals("Medium Coating Quality"))
                Date = CID + 5;
            else if(CQ.Equals("High Coating Quality"))
                Date = CID + 15;

            if ((CD - Date) > 0)
                age = CD - Date;
            else
                age = 0;
            return age;
        }
        // Step 3 : Determine susceptibility
        private String getsscp_external_clscc_austenitic()
        {
            String opTempString = null;
            if(opTemp < 49)
                opTempString = "<49";
            if((opTemp >= 49)&(opTemp < 93))
                opTempString = "49 to 93";
            if((opTemp >= 93)&(opTemp < 149))
                opTempString = "93 to 149";
            if(opTemp >= 149)
                opTempString = ">149";

            return rbi.getSusceptibilityExternalCLSCCAustenitic(opTempString, driver);
        }
        // Step 4 : Determine sererity index
        private int SVI()
        {
            String sscp_external_clscc = getsscp_external_clscc_austenitic();
            int svi = 0;
            if (sscp_external_clscc.Equals("High"))
                svi = 50;
            else if (sscp_external_clscc.Equals("Medium"))
                svi = 10;
            else if (sscp_external_clscc.Equals("Low"))
                svi = 1;
            
            return svi;
        }
        // Step 5 : Determine base damage factor from table 7.4
        private int D_fb_extclscc()
        {
            String field = NoInspection + IECategory; 
;
            return rbi.getD_f_scc(SVI(), field);
        }
        // Step 6 : Determine D_f_extclscc
        public double D_f_extclscc()
        {
            return D_fb_extclscc() * Math.Pow(age(), 1.1);
        }
    }
    
}
