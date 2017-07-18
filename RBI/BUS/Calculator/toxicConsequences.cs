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

        /****************************Properties***********************/
        public double tox_mfrac { set; get; } //neu la chat doc 100% thi = 1
        public double Wn { set; get; }
        public string materialType { set; get; } //loai chat HF or H2S, Amoni or Chlorine
        public string timeDura { get; set; }
        public double mass_n { set; get; }
        public double ld_max { set; get; } // don vi: minutes, tra bang 5.7 sau nay xay dung ham tinh ld_max
        public String componentType { set; get; }
        public bool continuous_instantaneous { set; get; } //0 is continuos, 1 is instantaneous
        /*****************Function*******************/
        public double min(double a, double b)
        {
            return (a < b) ? a : b;
        }
        public double tox_ld_n()
        {
            return min(mass_n/Wn, ld_max * 60);
        }
        public double tox_rate_n()
        {
            return tox_mfrac * Wn;
        }
        public double tox_mass_n()
        {
            return tox_mfrac * mass_n;
        }
        public double tox_CA_inj() //chia lam 4 TH
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
                    log = cd[0] * Math.Log10(C4 * tox_rate_n()) + cd[1];
                else
                    log = cd[0] * Math.Log10(C4 * tox_mass_n()) + cd[1];
                return C8 * Math.Pow(10, log);
            }
            else
            {
                ef = rbical.get_cd_ef(materialType, timeDura); //e = ef[0], f = ef[1]
                if (!continuous_instantaneous)
                    return ef[0] * Math.Pow(tox_rate_n(), ef[1]);
                else
                    return ef[0] * Math.Pow(tox_mass_n(), ef[1]);
            }
        }
        public double tox_CA_inj_final() //có 4 trường hợp riêng biệt
        {
            double product = 0;
            for(int i = 1; i < 5; i++)
            {
                product += double.Parse(rbical.getGff(componentType, i)) * tox_CA_inj();
            }
            return product / double.Parse(rbical.getGff(componentType));
        }
    }
   
}
