using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class toxicConsequences
    {
        RBICalculatorConn rbical = new RBICalculatorConn();
        public double mfrac_tox { set; get; } //neu la chat doc 100% thi = 1
        public double Wn { set; get; }
        public string materialType { set; get; } //loai chat HF or H2S, Amoni or Chlorine
        public string timeDura { get; set; }
        public double mass_n { set; get; }
        public double ld_max { set; get; } // don vi: minutes, tra bang 5.7 sau nay xay dung ham tinh ld_max
        public String componentType { set; get; }
        public bool continuous_instantaneous { set; get; } //0 is continuos, 1 is instantaneous
        
        public double min(double a, double b)
        {
            return (a < b) ? a : b;
        }
        //Step 1: calculate the effective duration of the toxic release, ld_tox_n
        public double ld_tox_n()
        {
            return min(mass_n/Wn, ld_max * 60);
        }
        //Step 2: Determine the toxic percentage of the toxic component, mfrac_toxic
        //Step 3:  calculate the release rate, rate_tox_n, release mass, xmass_tox_n
        public double rate_tox_n()
        {
            return mfrac_tox * Wn;
        }
        public double xmass_tox_n()
        {
            return mfrac_tox * mass_n;
        }
        //Step 4: calculate the toxic consequence area
        public double CA_tox_inj() //chia lam 4 TH
        {
            double[] cd,ef;
            double C8 = double.Parse(rbical.getC(8));
            double C4 = double.Parse(rbical.getC(4));
            if (materialType == "HF" || materialType == "H2S")
            {
                //c = cd[0], d = cd[1]
                cd = rbical.get_cd_ef(materialType, timeDura);
                double log = 0;
                if (!continuous_instantaneous)
                    log = cd[0] * Math.Log10(C4 * rate_tox_n()) + cd[1];
                else
                    log = cd[0] * Math.Log10(C4 * xmass_tox_n()) + cd[1];
                return C8 * Math.Pow(10, log);
            }
            else
            {
                ef = rbical.get_cd_ef(materialType, timeDura); //e = ef[0], f = ef[1]
                if (!continuous_instantaneous)
                    return ef[0] * Math.Pow(rate_tox_n(), ef[1]);
                else
                    return ef[0] * Math.Pow(xmass_tox_n(), ef[1]);
            }
        }
        //Step 5: 
        //Step 6: Determine the final toxic consequence areas for personnel injury
        public double CA_tox_inj_final() 
        {
            double product = 0;
            for(int i = 1; i < 5; i++)
            {
                product += double.Parse(rbical.getGff(componentType, i)) * CA_tox_inj();
            }
            return product / double.Parse(rbical.getGff(componentType));
        }
    }
   
}
