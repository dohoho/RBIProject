using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class noneFlamableNoneToxicConsequencesLvl2
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();
        //represent Fluid
        public String representFluid { set; get; }
        //
        public String eventTreeType { set; get; }
        //
        public String releasePhase { set; get; }
        // rate n from 7.1
        public double rate_n { set; get; }
        // mass n from 9.1
        public double mass_n { set; get; }
        //
        public double p_s { get; set; }
        //
        public double p_atm { set; get; }
        //
        public double t_s { set; get; }
        // equipment stored vapor volume
        public double v_s { set; get; }
        // moles flash from liquid to vapor
        public double n_v { set; get; }
        // release fluid ideal gas specific heat capacity ratio
        public double k { set; get; }
        //
        public double xs_cmdn_pexp { set; get; }
        //
        public double xs_injn_pexp { set; get; }
        //
        public double xs_cmdn_bleve { set; get; }
        //
        public double xs_injn_bleve { set; get; }
        // gff of hole number
        public double gff_n { set; get; }
        // gff total
        public double gff_total { set; get; }
        

        public double ca_injn_cont()
        {
            double ca_injn_cont = 0;
            double h = 0.31 - 0.0032 * Math.Pow(double.Parse(rbi.getC(11)) * (p_s - p_atm) - 40, 2);
            double g = 2696 -21.9 * double.Parse(rbi.getC(11)) * (p_s - p_atm) + 1.474 * Math.Pow(double.Parse(rbi.getC(11)) * (p_s - p_atm), 2);
            if (representFluid.Equals("Steam"))
                ca_injn_cont = double.Parse(rbi.getC(9)) * rate_n;
            else if (representFluid.Equals("Acids") || representFluid.Equals("Caustics"))
                ca_injn_cont = 0.2 * double.Parse(rbi.getC(8)) * g * Math.Pow(double.Parse(rbi.getC(4)) * rate_n, h);
            
            return ca_injn_cont;
        }
        public double ca_injn_inst()
        {
            double ca_injn_inst = 0;
            
            if (representFluid.Equals("Steam"))
                ca_injn_inst = double.Parse(rbi.getC(10)) * Math.Pow(mass_n, 0.6384);
            else if (representFluid.Equals("Acids") || representFluid.Equals("Caustics"))
                ca_injn_inst = 0;

            return ca_injn_inst;
        }
        public double fact_n_ic()
        {
            double fact_n_ic = 0;

            if (representFluid.Equals("Steam"))
                fact_n_ic = Math.Min(rate_n / double.Parse(rbi.getC(5)), 1);
            else if (representFluid.Equals("Acids") || representFluid.Equals("Caustics"))
                fact_n_ic = 0;

            return fact_n_ic;
        }
        public double ca_inj_leak()
        {
            double ca_injn_leak = 0;
            double cainjninst = ca_injn_inst();
            double factnic = fact_n_ic();
            double cainjncont = ca_injn_cont();
            ca_injn_leak = cainjninst * factnic + cainjncont * (1 - factnic);

            return ca_injn_leak;
        }
        public double W_tnt()
        {
            double W_tnt = 0;            
            W_tnt = double.Parse(rbi.getC(5)) * v_s * (p_s - p_atm)/(k - 1);

            return W_tnt;
        }
        public double Pso(double xs_cmd_pexp)
        {
            
            double Wtnt = W_tnt();
            double R_hsn = double.Parse(rbi.getC(27)) * xs_cmd_pexp * Math.Pow(Wtnt, 1 / 3);
            double Pso = double.Parse(rbi.getC(27)) * (-0.059965896 + (1.1288697) / (Math.Log(R_hsn)) - (7.9625216) / (Math.Pow(Math.Log(R_hsn), 2)) + (25.106738) / (Math.Pow(Math.Log(R_hsn), 3)) - (30.396707) / (Math.Pow(Math.Log(R_hsn), 4)) + (19.399862) / (Math.Pow(Math.Log(R_hsn), 5)) - (6.8853477) / (Math.Pow(Math.Log(R_hsn), 6)) + (1.2825511) / (Math.Pow(Math.Log(R_hsn), 7)) - (0.097705789) / (Math.Pow(Math.Log(R_hsn), 8)));
            
            return Pso;
        }
        
        public double ca_cmd_pexp()
        {
            double ca_cmd_pexp = 0;
            if (eventTreeType.Equals("Rupture"))
            {                
                ca_cmd_pexp = Math.PI * Math.Pow(xs_cmdn_pexp, 2);
            }            
            return ca_cmd_pexp;
        }
        public double Pr(double xs_inj_pleve)
        {
            double Pr = 0;
            double Wtnt = W_tnt();
            double R_hsn = double.Parse(rbi.getC(27)) * xs_inj_pleve * Math.Pow(Wtnt, 1 / 3);            
            double Pso = double.Parse(rbi.getC(27)) * (-0.059965896 + (1.1288697) / (Math.Log(R_hsn)) - (7.9625216) / (Math.Pow(Math.Log(R_hsn), 2)) + (25.106738) / (Math.Pow(Math.Log(R_hsn), 3)) - (30.396707) / (Math.Pow(Math.Log(R_hsn), 4)) + (19.399862) / (Math.Pow(Math.Log(R_hsn), 5)) - (6.8853477) / (Math.Pow(Math.Log(R_hsn), 6)) + (1.2825511) / (Math.Pow(Math.Log(R_hsn), 7)) - (0.097705789) / (Math.Pow(Math.Log(R_hsn), 8)));
            Pr = -23.8 + 2.92 * Math.Log(double.Parse(rbi.getC(27)) * Pso);

            return Pr;
        }
        public double ca_inj_pexp()
        {
            double ca_inj_pexp = 0;
            if (eventTreeType.Equals("Rupture"))
            {                
                ca_inj_pexp = Math.PI * Math.Pow(xs_injn_pexp, 2);
            }
            
            
            return ca_inj_pexp;
        }
        public double W_tnt_bleve()
        {
            double Wtntbleve = 0;
            Wtntbleve = double.Parse(rbi.getC(30)) * n_v * 8.314 * t_s * Math.Log(p_s / p_atm);
            double Wtnt = W_tnt();
            if(releasePhase.Equals("Two-phase"))                
                Wtntbleve = Wtntbleve + Wtnt;
            else
                return Wtntbleve;
            return Wtntbleve;
        }        
        public double ca_cmd_bleve()
        {

            double ca_cmd_bleve = 0;
            
            ca_cmd_bleve = Math.PI * Math.Pow(xs_cmdn_bleve, 2);
            return ca_cmd_bleve;
        }

        public double ca_inj_bleve()
        {
            double ca_inj_bleve = 0;
            if (eventTreeType.Equals("Rupture"))
            {                
                ca_inj_bleve = Math.PI * Math.Pow(xs_injn_bleve, 2);
            }


            return ca_inj_bleve;
        }
        public double ca_cmdn_nfnt()
        {
            double ca_cmdn_nfnt = 0;
            double cacmdpexp = ca_cmd_pexp();
            double cainjbleve = ca_cmd_bleve();

            ca_cmdn_nfnt = Math.Max(cacmdpexp, cainjbleve);
            return ca_cmdn_nfnt;
        }
        public double ca_injn_nfnt()
        {
            double ca_injn_nfnt = 0;
            double cacmdpexp = ca_inj_pexp();
            double cainjbleve = ca_inj_bleve();
            double cainjleak = ca_inj_leak();

            ca_injn_nfnt = Math.Max(cacmdpexp, cainjbleve) + cainjleak;
            return ca_injn_nfnt;
        }
        public double ca_cmd_nfnt()
        {
            double ca_cmd_nfnt = 0;
            double cacmdnnfnt = ca_cmdn_nfnt();
            ca_cmd_nfnt = gff_n * cacmdnnfnt / gff_total;
            return ca_cmd_nfnt;
        }
        public double ca_inj_nfnt()
        {
            double ca_inj_nfnt = 0;
            double cainjnnfnt = ca_injn_nfnt();
            ca_inj_nfnt = gff_n * cainjnnfnt / gff_total;
            return ca_inj_nfnt;
        }
    }
}
