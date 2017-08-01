using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class HTHA_DamageFactor
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public int inspection { set; get; }
        public String inspectionCatalog { set; get; }
        public double T { set; get; } //Exposure temperature oF
        public double age { set; get; } //Exposure time
        public double P_H2 { set; get; } //Exposure hydrogen partial pressure [MPa]
        public String Materials { set; get; }
        //Step 1: Check temperature T and Hydrogen partial pressure P_H2, if T <= 400F or P_H2 <= 0.552 -> step 6
        //Step 2: Determine inspection and inspectionCatalog
        //Step 3: Determine the material of construction, exposure time
        //Step 4: Compute the Pv
        public double Pv()
        {
            double t = (T - 32) / 1.8; //doi sang do C
            double log1 = Math.Log10(P_H2 / 0.0979);
            double log2 = 3.09 * Math.Pow(10, -4) * (t + 273) * (Math.Log10(age) + 14);
            return log1 + log2;
        }
        //Step 5: Determine the susceptibility base on Pv
        public String getSusceptibility()
        {
            double pv = Pv();
            String susceptibility = null;
            if (P_H2 > 8.274) Materials = "1.25Cr-0.5Mo";
            switch (Materials)
            {
                case "Carbon Steel":
                    {
                        if (pv > 4.7) susceptibility = "High";
                        else if (pv > 4.61 && pv <= 4.7) susceptibility = "Medium";
                        else if (pv > 4.53 && pv <= 4.61) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                case "C-0.5Mo(Annealed)":
                    {
                        if (pv > 4.95) susceptibility = "High";
                        else if (pv > 4.87 && pv <= 4.95) susceptibility = "Medium";
                        else if (pv > 4.78 && pv <= 4.87) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                case "C-0.5Mo(Normalized)":
                    {
                        if (pv > 5.6) susceptibility = "High";
                        else if (pv > 5.51 && pv <= 5.6) susceptibility = "Medium";
                        else if (pv > 5.43 && pv <= 5.51) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                case "1Cr-0.5Mo":
                    {
                        if (pv > 5.8) susceptibility = "High";
                        else if (pv > 5.71 && pv <= 5.8) susceptibility = "Medium";
                        else if (pv > 5.63 && pv <= 5.71) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                case "1.25Cr-0.5Mo":
                    {
                        if (pv > 6.0) susceptibility = "High";
                        else if (pv > 5.92 && pv <= 6.0) susceptibility = "Medium";
                        else if (pv > 5.83 && pv <= 5.92) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                case "2.25Cr-1Mo":
                    {
                        if (pv > 6.53) susceptibility = "High";
                        else if (pv > 6.45 && pv <= 6.53) susceptibility = "Medium";
                        else if (pv > 6.36 && pv <= 6.45) susceptibility = "Low";
                        else susceptibility = "Not";
                        break;
                    }
                default: susceptibility = "NULL"; break;
            }
            return susceptibility;
        }
        //Step 6: Determine D_HTHA_f
        public int D_HTHA_f()
        {
            if (T <= 400 && P_H2 <= 0.552) return 1;
            return rbi.getHTHA(inspection, inspectionCatalog, getSusceptibility());
        }
    }
}
