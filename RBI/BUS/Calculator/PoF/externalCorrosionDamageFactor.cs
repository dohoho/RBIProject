using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
using System.Diagnostics;
namespace RBI.BUS.Calculator
{
    class externalCorrosionDamageFactor
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public String componentType { set; get; } //tên thiết bị
        public int inspection { set; get; } //số lần kiểm tra
        public String inspectionCatalog { set; get; } 
        public String CoatingQuality { set; get; }
        public double ComponentInstallationDate { set; get; }
        public double age_tk { set; get; } //since the last inspection thickness reading
        public double t_rd { set; get; } //thickness reading
        public double t_min { set; get; } 
        public double Date { set; get; }
        public double CalculationDate { set; get; }
        public int F_ps { set; get; }   //có 2 trường hợp để tìm:
        public int F_ip { set; get; }   //F_ps và F_ip
        public double CA { set; get; }
       
        public double Temperature { set; get; }
        
        public String Driver { set; get; }
        private double min_max(double x, double y, bool IsMin) //IsMin = true find min, false find max
        {
            if (IsMin) return x < y ? x : y;
            else return x > y ? x : y;
        }
        //Step 1: Determine number of inspection and inspectionCatalog
        //Step 2: Determine age_tk, t_rd
        //Step 3: Determine age_coat
        public double calculationDate()
        {
            if (CoatingQuality == "Poor" || CoatingQuality == "No")
                Date = ComponentInstallationDate;
            else if (CoatingQuality == "Medium")
                Date = ComponentInstallationDate + 5;
            else Date = ComponentInstallationDate + 15;
            return CalculationDate - Date;
        }
        public double age_coat()
        {
            return min_max(0, calculationDate(), false);
        }
        //Step 4: Determine time in-service, age
        public double age()
        {
            return min_max(age_tk, age_coat(), true);
        }
        //Step 5: Determine C_rB
        public double getTemperature()
        {
            if (Temperature <= 14) return 10;
            else if (Temperature <= 30.5) return 18;
            else if (Temperature <= 66.5) return 43;
            else if (Temperature <= 125) return 90;
            else if (Temperature <= 192.5) return 160;
            else if (Temperature <= 237.5) return 225;
            else return 250;
        }
        public int C_rB()
        {
            return rbi.getCorrosionRate(getTemperature(), Driver);
        }
        //Step 6: Compute final corrosion rate C_r
        public double C_r()
        {
            return C_rB() * min_max(F_ip, F_ps, false);
        }
        //Step 7: Determine the minimum required wall thickness, t_min
        //Step 8: Determine A_rt
        public double A_rt()
        {
            double temp = 1 - (t_rd - C_r() * age()) / (t_min + CA);
            return min_max(temp, 0.0, false);
        }
        //Step 9: Determine D_extcor_f
        public int D_extcor_f()
        {
            return rbi.getDfb(A_rt(), inspection, inspectionCatalog, componentType);
        }
    }
}
