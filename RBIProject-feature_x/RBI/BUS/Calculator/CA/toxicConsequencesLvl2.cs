using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class toxicConsequencesLvl2
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();
        // mole fraction of release rate
        public double molefrac_tox { set; get; }
        // mass available of toxic
        public double mass_n { set; get; }
        // maximum leak duration 
        public double ld_maxn { set; get; }
        // W_n from step 3.4
        public double W_n { set; get; }
        // toxic component
        public String toxicComponent { set; get; }
        // criteria
        public String criteria { set; get; }
        // toxic cloud area
        public double ca_n_cloud { set; get; }
        // probability of safe dispersion from 8.10
        public double p_safen { set; get; }       
        // gff of hole number
        public double gff_n { set; get; }
        // gff total
        public double gff_total { set; get; }

        public double ld_n_tox()
        {
            double ld_n_tox = 0;
            ld_n_tox = Math.Min(Math.Min(3600, (mass_n)/(W_n)), 60 * ld_maxn);
            return ld_n_tox;
        }
        public double tox_lim()
        {
            double tox_lim = 0;
            tox_lim = rbi.getToxicImpactCriteria(toxicComponent, criteria);
            return tox_lim;
        }
        public double tox_lim_mod()
        {
            double tox_lim_mod = 0;
            double toxlim = tox_lim();
            tox_lim_mod = toxlim / molefrac_tox;
            return tox_lim_mod;
        }
        public double p_n_tox()
        {
            double p_n_tox = 0;
            p_n_tox = p_safen;
            return p_n_tox;
        }
        public double ca_n_tox()
        {
            double ca_n_tox = 0;
            double pntox = p_n_tox();
            ca_n_tox = pntox * ca_n_cloud;
            return ca_n_tox;
        }
        public double ca_tox()
        {
            double ca_tox = 0;
            double cantox = ca_n_tox();
            ca_tox = gff_n * cantox / gff_total;
            return ca_tox;
        }
    }
}
