using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class consequenceAnalysisLvl1
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();

        // material
        public String material { set; get; }
        // fluid
        public String fluid { set; get; }
        // phase of stored fluid
        public String fluidPhase { set; get; }        
        // ideal gas constant
        public String idealGasConstant { set; get; }
        // component type
        public String componentType { set; get; }
        // fluid type
        public String fluidType { set; get; }
        // phase of fluid upon release
        public String releasePhase { set; get; }
        // detection type
        public double detectionType { set; get; }
        // isolation type
        public double isolationType { set; get; }
        // stored pressure
        public double p_s { set; get; }
        // atmospheric pressure kPa
        public double p_atm { set; get; }
        // stored temperature K
        public double t_s { set; get; }        
        // atmosphere temperature K
        public double t_atm { set; get; }        
        // reynold constant
        public double r_e { set; get; }
        // fluid mass
        public double mass_inv { set; get; }
        // component mass
        public double mass_comp { set; get; }
        // mitigation system 
        public double mitigationSystem { set; get; }
        // toxic percentage of toxic component
        public double mfrac_tox { set; get; } 
        // material type
        public String materialType { set; get; }
        // release duration
        public String releaseDuration { set; get; }
        // non flammable non toxic fluids result in ntft consequnece area
        public String nfntReleaseFluid { set; get; }
        // outage multiplier
        public double outage_mult { set; get; }
        // production cost ($)
        public double prodcost { set; get; }
        // injury cost that company would be incurred($)
        public double injcost { set; get; }
        // environment cost
        public double envcost { set; get; }
        // population density (person/m^2)
        public double popdens { set; get; }
        // equipcost
        public double equipcost { set; get; }
        // Step 1 representative fluid and properties
        // 1.1
        //public String representativeFluid()
        //{
        //    String representativeFluid = "";
        //    representativeFluid = rbi.getRepresentativeFluid(fluid);
        //    return representativeFluid;
        //}

        // 1.3
        public double p_l()
        {
            double p_l = 0;
            if (fluidPhase.Equals("Liquid"))
                p_l = rbi.getPl(fluid);            
            return p_l;
        }
        public double mw()
        {
            double mw = 0;
            if (fluidPhase.Equals("Vapor"))
                mw = double.Parse(rbi.getMw(fluid));
            return mw;
        }
        public double ait()
        {
            double AIT = 0;
            AIT = double.Parse(rbi.getAIT(fluid));
            return AIT;
        }
        public double k()
        {
            double k = 0;            
            if (fluidPhase.Equals("Vapor"))
            {
                double cp = rbi.getCp(fluid, idealGasConstant);
                double R = 8.314;
                k = cp / (cp - R);
            }                
            return k;
        }

        // Step 2 release hole size
        //2.1
        public double d_n(int i)
        {
            double dn = 0;
            if (i == 1)
                dn = 0.25;
            else if (i == 2)
                dn = 1;
            else if (i == 3)
                dn = 4;
            else if (i == 4)
                dn = 16;
            return dn;
        }
        // 2.2
        public double gff_n(int n)
        {
            double gff_n = 0;
            String size = null;
            if (n == 1)
            {
                size = "small";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (n == 2)
            {
                size = "medium";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (n == 3)
            {
                size = "large";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (n == 4)
            {
                size = "rupture";
                gff_n = rbi.getGffn(componentType, size);
            }
            return gff_n;

        }

        public double gff_total()
        {
            double gff_total = 0;
            gff_total = double.Parse(rbi.getGff(componentType));
            return gff_total;
        }
        

        // Step 3               
        // 3.3
        public double a_n(int n)
        {
            double a_n = 0;
            double dn = d_n(n);
            a_n = Math.PI * Math.Pow(dn, 2) / 4;
            return a_n;
        }
        // 3.4
        public double W_n(int n)
        {
            double W_n = 0;
            double an = a_n(n);            
            double k = 0;
            double k_vn = 0;
            double m_w = mw();
            double p_trans = 0;
            if (releasePhase.Equals("Liquid"))
            {
                k = 0.9935 + 2.878 / Math.Pow(r_e, 0.5) + 342.75 / Math.Pow(r_e, 1.5);
                k_vn = Math.Pow(k, -1);
                W_n = 0.61 * k_vn * rbi.getPl(fluid) * an * Math.Sqrt(2 * 6.67 * Math.Exp(-11) * Math.Abs(p_s - p_atm) / rbi.getPl(fluid)) / double.Parse(rbi.getC(1));
            }
            else if (releasePhase.Equals("Vapor") || releasePhase.Equals("Gas"))
            {
                double R = 8.314;
                k = (Cp() / (Cp() - R));
                p_trans = p_atm * Math.Pow((k + 1) / 2, k / (k - 1));
                if (p_s > p_trans)
                {
                    double x = (k * m_w * 6.67 * Math.Exp(-11) / (R * t_s)) * Math.Pow(2 / (k + 1), (k + 1) / (k - 1));
                    W_n = 0.9 * an * p_s * Math.Sqrt(x) / double.Parse(rbi.getC(2));
                }
                else
                {
                    double x = (m_w * 6.67 * Math.Exp(-11) / (8.314 * t_s)) * ((2 * k) / (k - 1)) * Math.Pow(p_atm / p_s, 2 / k) * (1 - Math.Pow(p_atm / p_s, (k - 1) / k));
                    W_n = 0.9 * an * p_s * Math.Sqrt(x) / double.Parse(rbi.getC(2));
                }
            }

            return W_n;
        }
        // cp
        public double Cp()
        {
            int idealCp = rbi.getCp_ideal(fluid);
            double cp = 0;
            double A = rbi.getCp(fluid, "A");
            double B = rbi.getCp(fluid, "B");
            double C = rbi.getCp(fluid, "C");
            double D = rbi.getCp(fluid, "D");
            double E = rbi.getCp(fluid, "E");
            double CP_C2 = (C / t_s) / (Math.Sinh(C / t_s));
            double CP_E2 = (E / t_s) / (Math.Cosh(E / t_s));
            if (idealCp == 1)
            {
                cp = A + B * t_s + C * t_s * t_s + D * t_s * t_s * t_s;
            }
            else if (idealCp == 2)
            {
                cp = A + B * CP_C2 * CP_C2 + D * CP_E2 * CP_E2;
            }
            else if (idealCp == 3)
            {
                cp = A + B * t_s + C * Math.Pow(t_s, 2) + D * Math.Pow(t_s, 3) + E * Math.Pow(t_s, 4);
            }
            else
            {
                cp = 0;
            }
            return cp;
        }


        // Step 4 : fluid inventory available for release
        // 4.5
        public double W_max8()
        {
            double W_max8 = 0;
            double an = 32450;            
            double k = 0;
            double k_vn = 0;
            double mw = double.Parse(rbi.getMw(fluid));
            double p_trans = 0;
            if (releasePhase.Equals("Liquid") || releasePhase.Equals("Two-phase"))
            {
                k = 0.9935 + 2.878 / Math.Pow(r_e, 0.5) + 342.75 / Math.Pow(r_e, 1.5);
                k_vn = Math.Pow(k, -1);
                W_max8 = 0.61 * k_vn * rbi.getPl(fluid) * an * Math.Sqrt(2 * 6.67 * Math.Exp(-11) * Math.Abs(p_s - p_atm) / rbi.getPl(fluid)) / double.Parse(rbi.getC(1));
            }
            else if (releasePhase.Equals("Vapor"))
            {
                double R = 8.314;
                k = (Cp() / (Cp() - R));
                p_trans = p_atm * Math.Pow((k + 1) / 2, k / (k - 1));
                if (p_s > p_trans)
                {
                    double x = (k * mw * 6.67 * Math.Exp(-11) / (R * t_s)) * Math.Pow(2 / (k + 1), (k + 1) / (k - 1));
                    W_max8 = 0.9 * an * p_s * Math.Sqrt(x) / double.Parse(rbi.getC(2));
                }
                else
                {
                    double x = (mw * 6.67 * Math.Exp(-11) / (8.314 * t_s)) * ((2 * k) / (k - 1)) * Math.Pow(p_atm / p_s, 2 / k) * (1 - Math.Pow(p_atm / p_s, (k - 1) / k));
                    W_max8 = 0.9 * an * p_s * Math.Sqrt(x) / double.Parse(rbi.getC(2));
                }
            }

            return W_max8;
        }
        // 4.6
        public double mass_addn(int n)
        {
            double mass_addn = 0;
            double Wmax8 = W_max8();
            double Wn = W_n(n);
            mass_addn = 180 * Math.Min(Wmax8, Wn);
            return mass_addn;
        }
        // 4.7
        public double mass_availn(int n)
        {
            double mass_availn = 0;
            double massaddn = mass_addn(n);
            mass_availn = Math.Min(mass_comp + massaddn, mass_inv);
            return mass_availn;
        }


        // Step 5 : releaseType
        // 5.1 
        public double t_n(int n)
        {
            double t_n = 0;
            double Wn = W_n(n);
            t_n = double.Parse(rbi.getC(3)) / Wn;
            return t_n;
        }
        // 5.2
        public String releaseType(int n)
        {
            String releaseType = "";
            double tn = t_n(n);
            double dn = d_n(n);
            double massavailn = mass_availn(n);
            if (dn <= 6.35)
                releaseType = "Continuous";
            else if ((tn <= 180) || (massavailn > 4536))
                releaseType = "Instantaneous";
            else
                releaseType = "Continuous";
            return releaseType;
        }


        

        // Step 6 : Impact of detection and isolation system on release magnitude
        // 6.4
        public double fact_di()
        {
            double fact_di = 0;
            if (detectionType == 1 && isolationType == 1)
            {
                fact_di = 0.25;
            }
            else if (detectionType == 1 && isolationType == 2)
            {
                fact_di = 0.2;
            }
            else if ((detectionType == 1 || detectionType == 2) && isolationType == 3)
            {
                fact_di = 0.1;
            }
            else if (detectionType == 2 && isolationType == 2)
            {
                fact_di = 0.15;
            }
            else fact_di = 0;
            return fact_di;
        }

        //6.5
        public double ld_n_max(int n)
        {
            double ld_max = 0;
            double dn = d_n(n);
            if (detectionType == 1 && isolationType == 1)
            {
                if (dn == 0.25)
                    ld_max = 20;
                else if (dn == 1)
                    ld_max = 10;
                else if (dn == 4)
                    ld_max = 5;
            }
            else if (detectionType == 1 && isolationType == 2)
            {
                if (dn == 0.25)
                    ld_max = 30;
                else if (dn == 1)
                    ld_max = 20;
                else if (dn == 4)
                    ld_max = 10;
            }
            else if (detectionType == 1 && isolationType == 3)
            {
                if (dn == 0.25)
                    ld_max = 40;
                else if (dn == 1)
                    ld_max = 30;
                else if (dn == 4)
                    ld_max = 20;
            }
            else if ((isolationType == 1 || isolationType == 2) && detectionType == 2)
            {
                if (dn == 0.25)
                    ld_max = 40;
                else if (dn == 1)
                    ld_max = 30;
                else if (dn == 4)
                    ld_max = 20;
            }
            else if (detectionType == 2 && isolationType == 3)
            {
                if (dn == 0.25)
                    ld_max = 60;
                else if (dn == 1)
                    ld_max = 30;
                else if (dn == 4)
                    ld_max = 20;
            }
            else if (detectionType == 3 && (isolationType == 1 || isolationType == 2 || isolationType == 3))
            {
                if (dn == 0.25)
                    ld_max = 60;
                else if (dn == 1)
                    ld_max = 40;
                else if (dn == 4)
                    ld_max = 20;
            }
            return ld_max;
        }


        // Step 7: Release rate and consequence for analysis
        // 7.1
        public double rate_n(int n)
        {
            double rate_n = 0;
            double Wn = W_n(n);
            double factdi = fact_di();

            rate_n = Wn * (1 - factdi);
            return rate_n;
        }
        // 7.2
        public double ld_n(int n)
        {
            double ld_n = 0;
            double raten = rate_n(n);
            double massavailn = mass_availn(n);
            double ldmax = ld_n_max(n);
            ld_n = Math.Min(massavailn / raten, 60 * ldmax);
            return ld_n;
        }
        // 7.3
        public double mass_n(int n)
        {
            double mass_n = 0;
            mass_n = Math.Min(rate_n(n) * ld_n(n), mass_availn(n));
            return mass_n;
        }


        // Step 8 : flammable and explosive consequence
        // 8.1
        public double fact_mit()
        {
            double fact_mit = 0;
            if (mitigationSystem == 1)
                fact_mit = 0.25;
            else if (mitigationSystem == 2)
                fact_mit = 0.2;
            else if (mitigationSystem == 3)
                fact_mit = 0.05;
            else if (mitigationSystem == 4)
                fact_mit = 0.15;
            return fact_mit;
        }
        // 8.2 
        public double eneff_n(int n)
        {
            double eneff_n;
            double massn = mass_n(n);
            eneff_n = 4 * Math.Log10(double.Parse(rbi.getC(4)) * massn) - 15;
            return eneff_n;
        }
        // 
        private double a_cont(int select)
        {
            String cmd = rbi.getcmd(fluid, select, fluidPhase, "a");
            double a_cont = 0;
            try
            {
                a_cont = double.Parse(cmd);
            }
            catch
            {
                a_cont = 0;
            }
            return a_cont;
        }
        private double a_inj(int select)
        {
            String inj = rbi.getinj(fluid, select, fluidPhase, "a");
            double a_inj = 0;
            try
            {
                a_inj = double.Parse(inj);
            }
            catch
            {
                a_inj = 0;
            }
            return a_inj;
        }
        private double b_cont(int select)
        {
            String cmd = rbi.getcmd(fluid, select, fluidPhase, "b");
            double b_cont;
            try
            {
                b_cont = double.Parse(cmd);
            }
            catch
            {
                b_cont = 0;
            }
            return b_cont;
        }
        private double b_inj(int select)
        {
            String inj = rbi.getinj(fluid, select, fluidPhase, "b");
            double b_inj = 0;
            try
            {
                b_inj = double.Parse(inj);
            }
            catch
            {
                b_inj = 0;
            }
            return b_inj;
        }

        // 8.4 8.5
        public double ca_cmdn_cont(int select, int n)
        {
            double ca_cmdn_cont;            
            if ((releasePhase.Equals("Liquid")) & (fluidType.Equals("TYPE 0")))
                ca_cmdn_cont = Math.Min(a_cont(select) * Math.Pow(rate_n(n), b_cont(select)), double.Parse(rbi.getC(7))) * (1 - fact_mit());
            else
                ca_cmdn_cont = a_cont(select) * Math.Pow(rate_n(n), b_cont(select)) * (1 - fact_mit());
            return ca_cmdn_cont;
        }
        private double effrate_n(int select, int n)
        {
            double effrate_n = 0;            
            if ((releasePhase.Equals("Liquid")) & (fluidType.Equals("TYPE 0")))
                effrate_n = (1 / double.Parse(rbi.getC(4)) * Math.Exp(Math.Log10(ca_cmdn_cont(select, n) / (a_cont(select) * double.Parse(rbi.getC(8)))) * Math.Pow(b_cont(select), -1)));            
            else            
                effrate_n = rate_n(n);
            return effrate_n;
        }

        //8.6 8.7
        private double ca_cmdn_inst(int select, int n)
        {
            double ca_cmdn_inst;
            if ((releasePhase.Equals("Liquid")) & (fluidType.Equals("TYPE 0")))
                ca_cmdn_inst = Math.Min(a_cont(select) * Math.Pow(mass_n(n), b_cont(select)), double.Parse(rbi.getC(7))) * ((1 - fact_mit()) / eneff_n(n));            
            else
                ca_cmdn_inst = a_cont(select) * Math.Pow(mass_n(n), b_cont(select)) * (1 - fact_mit());
            return ca_cmdn_inst;
        }
        public double effmass_n(int select, int n)
        {
            double effmass_n = 0;            
            if (releasePhase.Equals("Liquid"))
                effmass_n = (1 / double.Parse(rbi.getC(4))) * Math.Exp(Math.Log10(ca_cmdn_inst(select, n) / (double.Parse(rbi.getC(8)) * a_cont(select)) * (1 / b_cont(select))));            
            else
                effmass_n = mass_n(n);
            return effmass_n;
        }

        // 8.9 8.8
        public double ca_injn_cont(int select, int n)
        {
            double ca_injn_cont = 0;
            ca_injn_cont = a_inj(select) * Math.Pow(effrate_n(select, n), b_inj(select)) * (1 - fact_mit());
            return ca_injn_cont;
        }
       
       
        //8.10 8.11
        public double ca_injn_inst(int select, int n)
        {
            double ca_injn_inst = 0;
            ca_injn_inst = a_inj(select) * Math.Pow(effrate_n(select, n), b_inj(select)) * ((1 - fact_mit()) / eneff_n(n));
            return ca_injn_inst;
        }

        // 8.12
        public double fact_n_ic(int n)
        {
            String releasetype = releaseType(n);
            if (releasetype.Equals("Continuous"))
                return Math.Min(rate_n(n) / double.Parse(rbi.getC(5)), 1.0);
            else
                return 1.0;
        }

        // 8.13
        public double fact_ait()
        {
            double fact_ait = 0;
            double ai_t = ait();
            if ((t_s + double.Parse(rbi.getC(6))) <= ai_t)
                fact_ait = 0;
            else if ((t_s - double.Parse(rbi.getC(6))) >= ai_t)
                fact_ait = 1;
            else
                fact_ait = (t_s - ai_t + double.Parse(rbi.getC(6))) / (2 * double.Parse(rbi.getC(6)));
            return fact_ait;
        }

        // 8.15
        public double ca_cmdn_flame(int n)
        {
            double ca_cmdn_flame;
            double caailcmdn = ca_cmdn_cont(2, n) * fact_n_ic(n) + ca_cmdn_inst(4, n) * (1 - fact_n_ic(n));
            double caainlcmdn = ca_cmdn_cont(1, n) * fact_n_ic(n) + ca_cmdn_inst(3, n) * (1 - fact_n_ic(n));
            ca_cmdn_flame = caailcmdn * fact_ait() + caainlcmdn * (1 - fact_ait());
            return ca_cmdn_flame;
        }
        public double ca_injn_flame(int n)
        {
            double ca_injn_flame;
            double caailinjn = ca_injn_cont(2, n) * fact_n_ic(n) + ca_injn_inst(4, n) * (1 - fact_n_ic(n));
            double caainlinjn = ca_injn_cont(1, n) * fact_n_ic(n) + ca_injn_inst(3, n) * (1 - fact_n_ic(n));
            ca_injn_flame = caailinjn * fact_ait() + caainlinjn * (1 - fact_ait());
            return ca_injn_flame;
        }


        // 8.16
        // component damage consequence area       
        public double ca_cmd_flame()
        {
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff_n(i) * ca_cmdn_flame(i);                
            }
            double ca_cmd_flame = t / gff_total();
            return Math.Abs(ca_cmd_flame);
        }


        // personal injury consequence area
        public double ca_inj_flame()
        {
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff_n(i) * ca_injn_flame(i);
            }
            double ca_inj = t / gff_total();
            return Math.Abs(ca_inj);
        }

        public double ca()
        {
            return Math.Max(ca_inj(), ca_cmd());
        }
        // catalog
        public int convertCatalog()
        {
            double c_a = ca();
            if (c_a <= 100)
                return 1;
            else if (c_a > 100 && c_a <= 1000)
                return 2;
            else if (c_a > 1000 && c_a <= 3000)
                return 3;
            else if (c_a > 3000 && c_a <= 10000)
                return 4;
            else 
                return 5;
        }


        // Step 9 : toxic consequence
        // 9.1
        public double tox_ld_n(int n)
        {
            return Math.Min(mass_n(n) / W_n(n), ld_n_max(n) * 60);
        }

        // 9.3
        public double rate_tox_n(int n)
        {
            return mfrac_tox * W_n(n);
        }
       
        public double mass_tox_n(int n)
        {
            return mfrac_tox * mass_n(n);
        }
        // 9.4
        public double ca_injn_tox(int n) 
        {
            double[] cd, ef;
            double C8 = double.Parse(rbi.getC(8));
            double C4 = double.Parse(rbi.getC(4));
            String releasetype = releaseType(n);
            if (materialType == "HF" || materialType == "H2S")
            {
                //c = cd[0], d = cd[1]
                cd = rbi.get_cd_ef(materialType, releaseDuration);
                double log = 0;
                if (releasetype.Equals("Continuous"))
                    log = cd[0] * Math.Log10(C4 * rate_tox_n(n)) + cd[1];
                else
                    log = cd[0] * Math.Log10(C4 * mass_tox_n(n)) + cd[1];
                return C8 * Math.Pow(10, log);
            }
            else
            {
                ef = rbi.get_cd_ef(materialType, releaseDuration); //e = ef[0], f = ef[1]
                if (releasetype.Equals("Continuous"))
                    return ef[0] * Math.Pow(rate_tox_n(n), ef[1]);
                else
                    return ef[0] * Math.Pow(mass_tox_n(n), ef[1]);
            }
        }

        // 9.6
        public double ca_inj_tox() 
        {
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff_n(i) * ca_injn_tox(i);
            }
            double ca_inj_tox = t / gff_total();
            return Math.Abs(ca_inj_tox);
            
        }


        // Step 10 non flammable non toxic consequence
        // 10.1
        public double ca_injn_contnfnt(int n)
        {
            double ca_injn_cont = 0;
            double g = 2696 - 21.9 * double.Parse(rbi.getC(11)) * (p_s - p_atm) + 1.474 * Math.Pow((double.Parse(rbi.getC(11)) * (p_s - p_atm)), 2);
            double h = 0.31 - 0.00032 * Math.Pow((double.Parse(rbi.getC(11)) * (p_s - p_atm) - 40), 2);
            if (nfntReleaseFluid.Equals("Steam"))            
                ca_injn_cont = double.Parse(rbi.getC(9)) * rate_n(n);            
            else if (nfntReleaseFluid.Equals("Acids") || nfntReleaseFluid.Equals("Caustics"))            
                ca_injn_cont = 0.2 * double.Parse(rbi.getC(8)) * g * Math.Pow(double.Parse(rbi.getC(4)) * rate_n(n), h);            
            return ca_injn_cont;
        }
        
        public double ca_injn_instnfnt(int n)
        {
            double ca_injn_inst = 0;
            if (nfntReleaseFluid.Equals("Steam"))
                ca_injn_inst = double.Parse(rbi.getC(10)) * Math.Pow(mass_n(n), 0.6384);            
            else if (nfntReleaseFluid.Equals("Acids") || nfntReleaseFluid.Equals("Caustics"))
                ca_injn_inst = 0;
            return ca_injn_inst;
        }

        // 10.2
        public double fact_n_icnfnt(int n)
        {
            double fact_n_icnfnt = 0;
            if (nfntReleaseFluid.Equals("Steam"))            
                fact_n_icnfnt = Math.Min(rate_n(n) / double.Parse(rbi.getC(5)), 1);
            else if (nfntReleaseFluid.Equals("Acids") || nfntReleaseFluid.Equals("Caustics"))            
                fact_n_icnfnt = 0;            
            return fact_n_icnfnt;
        }

        // 10.3
        public double ca_injn_leaknfnt(int n)
        {
            return ca_injn_instnfnt(n) * fact_n_icnfnt(n) + ca_injn_contnfnt(n) * (1 - fact_n_icnfnt(n));
        }

        // 10.4
        public double ca_inj_nfnt()
        {
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff_n(i) * ca_injn_leaknfnt(i);
            }
            double ca_inj_nfnt = t / gff_total();
            return Math.Abs(ca_inj_nfnt);            
        }

        // Step 11 component and injury consequences
        // 11.1
        public double ca_cmd()
        {           
            return ca_cmd_flame();
        }

        // 11.2
        public double ca_inj()
        {
            double ca_inj = 0;
            double cainjflame = ca_inj_flame();
            double cainjtox = ca_inj_tox();
            double cainjnfnt = ca_inj_nfnt();
            ca_inj = Math.Max(Math.Max(cainjflame, cainjtox), cainjnfnt);
            return ca_inj;
        }
        // Step 12: financial consequence
        // 12.1
        public double fc_cmd()
        {
            double fc_cmd = 0;            
            double matcost = double.Parse(rbi.getMatCost(material));
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff_n(i) * double.Parse(rbi.getHoleSize(componentType, i)) * matcost;
            }
            fc_cmd = t / gff_total();
            return fc_cmd;
        }

        //12.2
        public double fc_affa()
        {
            double fc_affa = 0;
            double cacmd = ca_cmd();

            fc_affa = cacmd * equipcost;
            return fc_affa;
        }

        // 12.3
        public double outage_cmd()
        {
            double outage_cmd = 0;
            double t = 0;
            for (int i = 0; i < 5; i++)
            {
                t += gff_n(i) *rbi.getOutage(componentType, i) * outage_mult;
            }
            outage_cmd = t / gff_total();
            return outage_cmd;
        }
        public double outage_affa()
        {
            double outage_affa = 0;
            double fcaffa = fc_affa();

            outage_affa = Math.Pow(10, 1.242 + 0.585 * Math.Log10(fcaffa * Math.Pow(10, -6)));
            return outage_affa;
        }
        public double fc_prod()
        {
            double fc_prod = 0;
            double outagecmd = outage_cmd();
            double outageaffa = outage_affa();

            fc_prod = (outagecmd + outageaffa) * prodcost;
            return fc_prod;
        }

        // 12.4
        public double fc_inj()
        {
            double fc_inj = 0;
            double cainj = ca_inj();
            double outageaffa = outage_affa();

            fc_inj = cainj * popdens * injcost;
            return fc_inj;
        }

        // 12.5
        public double vol_n_env(int n)
        {
            double vol_n_env = 0;
            double massn = mass_n(n);
            double frac_evap = rbi.getfracEvap(fluid);

            vol_n_env = double.Parse(rbi.getC(13)) * massn * (1 - frac_evap) / p_l();
            return vol_n_env;
        }
        public double fc_environ()
        {
            double fc_environ = 0;

            double t = 0;
            for (int i = 0; i < 5; i++)
            {
                double volnenv = vol_n_env(i);
                t += gff_n(i) * volnenv * envcost;
            }

            fc_environ = t / gff_total();
            return fc_environ;
        }


        // 12.6
        public double fc()
        {
            double fc = 0;
            double fccmd = fc_cmd();
            double fcaffa = fc_affa();
            double fcprod = fc_prod();
            double fcinj = fc_inj();
            double fcenviron = fc_environ();
            fc = fccmd + fcaffa + fcprod + fcinj + fcenviron;
            return fc;
        }

        #region CA for EQ PRESSURE RELIEF DEVICES
        ///<summary>
        /// tinh toan cost open( Consequence of Failure to Open)
        /// tinh toan lai Level 1 voi ap suat cao P0 thay the cho ap suat lam viec Ps( cai nay tinh sau).
        /// B1: tinh toan he so hieu chinh ap suat cao
        ///  Fa = Math.Sqrt( Aprd/ Atotal)
        /// B2: xac dinh ap suat qua tai
        ///  Po = Fa*Po;
        /// B3: tinh toan lai cac consequence cho truong hop ap suat qua tai Po thay vi ap suat lam viec.
        ///</summary>


        ///<summary>
        /// Tinh toan Cost Leakage( thiet hai do su do ri gay ra)
        ///</summary>
        // xac dinh cong suat dong chay cua chat long ben trong PRD( lb/h)
        public double Wc { set; get; }
        // xac dinh thoi gian dung de sua chua( min)
        public int T_insolate { set; get; }
        // xac dinh gia luong chat long bi mat( $/lb)
        public double Cost_flu { set; get; }
        // xac dinh kich thuoc ong
        public String inletSize { set; get; }
        // xac dinh type PRDs
        public String typePRDs { set; get; }

        // Xac dinh Fr( dua vao trang thai:
        // 1: xa ra de dap lua va he thong phuc hoi hoa hoan duoc thiet lap: Fr = 0.5
        // 2: xa ra de dong he thong: Fr = 0.0
        // 3: cho cac truong hop khac : Fr = 1.0
        public double Fr { set; get; }

        // xac dinh PRDs size, so sanh voi kich thoi dau vao NPS 6
        //public bool isGreater { set; get; }
        //xac dinh so ngay phai tat he thong de sua, ngay
        public double D_sd { set; get; }
        //xac dinh chi phi don dep moi truong 
        public double Cost_env { set; get; }
        // xac dinh leakage co the duoc bo qua hay k?
        public bool isTolerated { set; get; }
        //xac dinh loi nhuan hang ngay( $/day)
        public double Unit_prod { set; get; }
        //B1: xac dinh lrate_mild va lrate_so
        public double lrate(String a)
        {
            if (a.Equals("mild") || a.Equals("Mild"))
                return 0.1 * Wc;
            else return 0.25 * Wc;
        }
        //B2: xac dinh thoi gian ro ri D_mild
        private double D_mild_pressure()
        {
            return rbi.get_D_mild(inletSize, typePRDs);
        }
        //B3: xac dinh thoi gian ket mo van D_so
        public double D_so_pressure()
        {
            double so = (double)T_insolate / 1440;
            return so;
        }
        // B4: xac dinh Cost_mild
        private double Cost_mild_pressure()
        {
            return 24 * Fr * Cost_flu * D_mild_pressure() * lrate("Mild");
        }
        // B5: xac dinh Cost_so
        public double Cost_so_pressure()
        {
            return 24 * Fr * Cost_flu * D_so_pressure() * lrate("So");
        }
        // B6: tinh toan chi phai dung he thong de sua chua
        public double Cost_sd_pressure()
        {
            if (inletSize.Equals("Greater than 6"))
                return 2000;
            else
                return 1000;
        }
        // B7: xac dinh chi phi ton that dung san xuat
        private double Cost_mild_prod_pressure()
        {
            if (isTolerated)
                return 0;
            else
                return Unit_prod * D_sd;
        }
        public double Cost_so_prod_pressure()
        {
            return Unit_prod * D_sd;
        }

        // B8; xac dinh ton that
        public double Cost_mild_l_pressure()
        {
            return Cost_mild_pressure() + Cost_sd_pressure() + Cost_env + Cost_mild_prod_pressure();
        }
        public double Cost_so_l_pressure()
        {
            return Cost_so_pressure() + Cost_sd_pressure() + Cost_env + Cost_so_prod_pressure();
        }
        // B9: xac dinh final consequence
        public double Cost_l_pressure()
        {
            return 0.9 * Cost_mild_l_pressure() + 0.1 * Cost_so_l_pressure();
        }
        #endregion
        #region CA for EQ Heat Exchanger Tube Bundles
        // xac dinh Unit_prod: gia tri san xuat( $/day)
        public double Unit_prod_heat { set; get; }
        // xac dinh ty le loi bo
        public double Rate_red { set; get; }
        // xac dinh so ngay phai dung he thong de sua( khong theo ke hoach: day)
        public double D_sd_heat { set; get; }
        // xac dinh duong kinh trong cua ong 
        public double D_shell { set; get; }
        // xac dinh chieu dai cua ong( ft)
        public double L_tube { set; get; }
        // xac dinh tube Material Cost Factor: Mf( bang 8.3)
        public double Mf { set; get; }
        // xac dinh maintenance Cost with bundle replacement: chi phi bao tri lien quan den thay the cac bo( $)
        public double Cost_maint_heat { set; get; }
        // B1:xac dinh Cost_prod
        private double Cost_prod_heat()
        {
            return Unit_prod_heat * (Rate_red / 100) * D_sd_heat;
        }
        // B2: xac dinh Cost_bundle
        private double Cost_bundle_heat()
        {
            return 22000 * (Math.PI * D_shell * D_shell / 4) * L_tube * Mf / double.Parse(rbi.getC(1));
        }
        // B3: xac dinh gia tri Cost_total
        public double Cost_total_heat()
        {
            return Cost_prod_heat() + Cost_env + Cost_maint_heat + Cost_bundle_heat();
        }
        #endregion
    }
}
