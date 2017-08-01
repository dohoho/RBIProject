using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class liningDamageFactor
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        public String liningType { set; get; }
        public int timeInService { set; get; }
        public int age { set; get; }
        public int Flc { set; get; }
        public double Fom { set; get; }
        // step 1: tra bang 6.4 ra 1 thanh phan cua Dfb
        private double D_fb_liner_inorganic()
        {
            double dfb = rbi.getDf_liner_64(age, liningType);
            return dfb;
        }
        // step 2: tra bang 6.5 ra duoc thanh phan con lai cua Dfb
        private double D_fb_liner_organic()
        {

            double dfb = rbi.getDf_liner_65(timeInService, age);
            return dfb;
        }
        // tinh tong 2 thanh phan lai
        private double D_fb_liner()
        {
            return D_fb_liner_inorganic() + D_fb_liner_organic();
        }
        // step 3: xac dinh Df
        public double D_f_liner()
        {
            return D_fb_liner() * Fom * Flc;
        }
    }
}
