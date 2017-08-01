using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class damageFactor_885Embrittlement
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public bool adminControls { set; get; }
        // adminControls : determine whether admin controls prevent pressurizing below some temperature 
        public double tmin { set; get; }
        // tmin : initiated minimum temperature
        public double tref { set; get; }
        // tref : original transition temperature
        public double designTemp { set; get; }
        // designTemp : design temperature
        public double opTemp { set; get; }
        // opTemp : operation temperature
        public bool brittleTempCheck { set; get; }
        // brittleTempCheck : determine if actual ductile to brittle transition temp is known 
        // Step 2: Determine T min
        private double t_min()
        {
            double minTemp;
            if (adminControls == true)
            {
                minTemp = tmin;
            }
            else
            {
                if (designTemp > opTemp)
                    minTemp = opTemp;
                else
                    minTemp = designTemp;
            }
            return minTemp;
        }
        private double t_ref()
        {
            double refTemp;
            if (brittleTempCheck == true)
            {
                refTemp = tref;
            }
            else
                refTemp = 80;
            return refTemp;
        }
        private double estimate()
        {
            double refTemp1 = t_ref();
            double minTemp1 = t_min();
            double estimateTemp = 0;
            if ((minTemp1 - refTemp1) >= 90)
                estimateTemp = 100;
            else if (((minTemp1 - refTemp1) >= 70) & ((minTemp1 - refTemp1) < 90))
                estimateTemp = 80;
            else if (((minTemp1 - refTemp1) >= 50) & ((minTemp1 - refTemp1) < 70))
                estimateTemp = 60;
            else if (((minTemp1 - refTemp1) >= 30) & ((minTemp1 - refTemp1) < 50))
                estimateTemp = 40;
            else if (((minTemp1 - refTemp1) >= 10) & ((minTemp1 - refTemp1) < 30))
                estimateTemp = 20;
            else if (((minTemp1 - refTemp1) >= -10) & ((minTemp1 - refTemp1) < 10))
                estimateTemp = 0;
            else if (((minTemp1 - refTemp1) >= -30) & ((minTemp1 - refTemp1) < -10))
                estimateTemp = -20;
            else if (((minTemp1 - refTemp1) >= -50) & ((minTemp1 - refTemp1) < -30))
                estimateTemp = -40;
            else if (((minTemp1 - refTemp1) >= -70) & ((minTemp1 - refTemp1) < 50))
                estimateTemp = -60;
            else if (((minTemp1 - refTemp1) >= -90) & ((minTemp1 - refTemp1) < 70))
                estimateTemp = -80;
            else if ((minTemp1 - refTemp1) < -90)
                estimateTemp = -100;
            return estimateTemp;

        }
        public double D_f_885()
        {
            return rbi.get885EmbrittlementDamageFactor(estimate());
        }
    }
}
