using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class consequenceAnalysisLvl2
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();
        flamable_explosiveConsequencesLvl2 flamable_explosiveConsequences = new flamable_explosiveConsequencesLvl2();       
        toxicConsequencesLvl2 toxicConsequences = new toxicConsequencesLvl2();
        noneFlamableNoneToxicConsequencesLvl2 noneFlamableNoneToxicConsequences = new noneFlamableNoneToxicConsequencesLvl2();


        // material
        public String material { set; get; }   
        // fluid
        public String fluid { set; get; }
        // condition
        public String condition { set; get; }
        // phase of flashed fluid
        public String flashedFluidPhase { set; get; }
        // equipment type
        public String equiqmentType { set; get; }
        // component type
        public String componentType { set; get; }
        // inventory fluid
        public String inventoryFluid { set; get; }
        // type of fluid
        public String typeofFluid { set; get; }
        // phase of inventory fluid
        public String fluidPhase { set; get; }
        // phase of fluid upon release
        public String releasePhase { set; get; }
        // poolfire type
        public String poolfireType { set; get; }
        // event Tree type
        public String eventTreeType { set; get; }
        // equipcost
        public double equipcost { set; get; }  
        // mass fraction liquid
        public double frac_l { set; get; }
        // mass fraction vapor
        public double frac_v { set; get; }
        // vapor density
        public double p_v { set; get; }
        // flash temperature K
        public double t_f { set; get; }
        // fraction of fluid flashed
        public double frac_fsh { set; get; }
        // liquid density
        public double p_l { set; get; }
        // stored pressure kPa
        public double p_s { set; get; }
        // atmospheric pressure kPa
        public double p_atm { set; get; }
        // stored fluid's saturation pressure kPa
        public double psat_s { set; get; }
        // stored temperature K
        public double t_s { set; get; }
        // bubble point temperature for flashed liquid K
        public double t_b { set; get; }
        // dew point temperature for flashed vapor K
        public double t_d { set; get; }
        // atmosphere temperature K
        public double t_atm { set; get; }
        // time for steady release of fluid
        public double t_pn { set; get; }
        // specific heat of pool liquid J/kg-K
        public double c_pl { set; get; }
        // reynold constant
        public double r_e { set; get; }
        // hole number
        public double holeNumber { set; get; }
        // fluid mass
        public double mass_inv { set; get; }
        // component mass
        public double mass_comp { set; get; }
        // mass of flammable material in vapor cloud
        public double mass_vce { set; get; }
        // detection type
        public double detectionType { set; get; }
        // isolation type
        public double isolationType { set; get; }
        // lower flammability limit
        public double LFL { set; get; }
        // upper flammability limit
        public double UFL { set; get; }
        // mass fraction of release rate
        public double mfrac_flame { set; get; }
        // volume of liquid to establish fire pool m3
        public double V_pn { set; get; }
        // bubble-point pressure, corresponding to the ground temperature kPa
        public double P_bg { set; get; }
        // wind speed m/s
        public double u_w { set; get; }
        // area surface type
        public String surface { set; get; }
        // ground temperature K
        public double t_g { set; get; }
        // ambient condition
        public String ambientCondition { set; get; }
        // humidity %
        public double RH { set; get; }
        // m
        public double xs_cmdn_pool { set; get; }
        //
        public double xs_injn_pool { set; get; }
        //
        public double xs_cmdn_jet { set; get; }
        //
        public double xs_injn_jet { set; get; }
        //
        public double xs_cmdn_fb { set; get; }
        //
        public double xs_injn_fb { set; get; }
        //
        public double xs_cmdn_vce { set; get; }
        //
        public double xs_injn_vce { set; get; }
        // grade cloud area
        public double gradecloudArea { set; get; }
        // mole fraction of release rate
        public double molefrac_tox { set; get; }
        // leak duration
        public double ld_maxn { set; get; }
        // toxic Component
        public String toxicComponent { set; get; }
        // criteria
        public String criteria { set; get; }
        // grade level cloud
        public double ca_n_cloud { set; get; }
        //represent Fluid
        public String representFluid { set; get; }
        // equipment stored vapor volume
        public double v_s { set; get; }
        // moles flash from liquid to vapor
        public double n_v { set; get; }
        // release fluid ideal gas specific heat capacity ratio
        public double k { set; get; }
        // 
        public double xs_cmdn_pexp { set; get; }
        // 
        public double xs_cmdn_bleve { set; get; }
        // 
        public double xs_injn_pexp { set; get; }
        // 
        public double xs_injn_bleve { set; get; }
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
        


        // Step 2 release rate calculation
        // 2.1
        public double d_n()
        {
            double dn = 0;
            if (holeNumber == 1)
                dn = 0.25;
            else if (holeNumber == 2)
                dn = 1;
            else if (holeNumber == 3)
                dn = 4;
            else if (holeNumber == 4)
                dn = 16;
            return dn;
        }
        // 2.2
        public double gff_n()
        {
            double gff_n = 0;
            String size = null;
            if (holeNumber == 1)
            {
                size = "small";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (holeNumber == 2)
            {
                size = "medium";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (holeNumber == 3)
            {
                size = "large";
                gff_n = rbi.getGffn(componentType, size);
            }
            else if (holeNumber == 4)
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

        // Step 3 release hole size selection
        // 3.2
        public String release_phase()
        {
            String release_phase = "";
            if ((p_s >= p_atm) & (p_s <= psat_s))
                release_phase = "Vapor";
            else if ((psat_s > p_atm) & (psat_s <= p_s))
                release_phase = "Two-phase";
            else if ((p_atm > psat_s) & (p_atm <= p_s))
                release_phase = "Liquid";
            return release_phase;
        }
 
        // 3.3
        public double a_n()
        {
            double a_n = 0;
            double dn = d_n();
            a_n = Math.PI * Math.Pow(dn, 2) / 4;
            return a_n;
        }
        // 3.4
        public double W_n()
        {
            double W_n = 0;
            double an = a_n();
            String releasephase = release_phase();
            double k = 0;
            double k_vn = 0;
            double mw = double.Parse(rbi.getMw(fluid));            
            double p_trans = 0;
            if (releasephase.Equals("Liquid") || releasephase.Equals("Two-phase"))
            {
                k = 0.9935 + 2.878 / Math.Pow(r_e, 0.5) + 342.75 / Math.Pow(r_e, 1.5);
                k_vn = Math.Pow(k, -1);
                W_n = 0.61 * k_vn * rbi.getPl(fluid) * an * Math.Sqrt(2 * 6.67 * Math.Exp(-11) * Math.Abs(p_s - p_atm) / rbi.getPl(fluid)) / double.Parse(rbi.getC(1));
            }
            else if (releasephase.Equals("Vapor"))
            {
                double R = 8.314;
                k = (Cp() / (Cp() - R)); 
                p_trans = p_atm * Math.Pow((k + 1) / 2, k / (k - 1));
                if (p_s > p_trans)
                {
                    double x = (k * mw * 6.67 * Math.Exp(-11) / (R * t_s)) * Math.Pow(2 / (k + 1), (k + 1) / (k - 1));
                    W_n = 0.9 * an * p_s * Math.Sqrt(x) / double.Parse(rbi.getC(2));
                }
                else
                {
                    double x = (mw * 6.67 * Math.Exp(-11) / (8.314 * t_s)) * ((2 * k) / (k - 1)) * Math.Pow(p_atm / p_s, 2 / k) * (1 - Math.Pow(p_atm / p_s, (k - 1) / k));
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
            String releasephase = release_phase();
            double k = 0;
            double k_vn = 0;
            double mw = double.Parse(rbi.getMw(fluid));
            double p_trans = 0;
            if (releasephase.Equals("Liquid") || releasephase.Equals("Two-phase"))
            {
                k = 0.9935 + 2.878 / Math.Pow(r_e, 0.5) + 342.75 / Math.Pow(r_e, 1.5);
                k_vn = Math.Pow(k, -1);
                W_max8 = 0.61 * k_vn * rbi.getPl(fluid) * an * Math.Sqrt(2 * 6.67 * Math.Exp(-11) * Math.Abs(p_s - p_atm) / rbi.getPl(fluid)) / double.Parse(rbi.getC(1));
            }
            else if (releasephase.Equals("Vapor"))
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
        public double mass_addn()
        {
            double mass_addn = 0;
            double Wmax8 = W_max8();
            double Wn = W_n();
            mass_addn = 180 * Math.Min(Wmax8, Wn);
            return mass_addn;
        }
        // 4.7
        public double mass_availn()
        {
            double mass_availn = 0;
            double massaddn = mass_addn();            
            mass_availn = Math.Min(mass_comp + massaddn, mass_inv);
            return mass_availn;
        }

        // Step 5 : releaseType
        // 5.1 
        public double t_n()
        {
            double t_n = 0;
            double Wn = W_n();
            t_n = double.Parse(rbi.getC(3))/Wn;
            return t_n;
        }
        // 5.2
        public String releaseType()
        {
            String releaseType = "";
            double tn = t_n();
            double dn = d_n();
            double massavailn = mass_availn();
            if (dn <= 6.35)
                releaseType = "Continuous";
            else if((tn <= 180) || (massavailn > 4536))
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
        public double ld_max()
        {
            double ld_max = 0;
            double dn = d_n();
            if (detectionType == 1 && isolationType == 1)
            {
                if(dn == 0.25)
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

        // Step 7 : Release rate and consequence for analysis
        // 7.1
        public double rate_n()
        {
            double rate_n = 0;
            double Wn = W_n();
            double factdi = fact_di();

            rate_n = Wn * (1 - factdi);
            return rate_n;
        }
        // 7.2
        public double ld_n()
        {
            double ld_n = 0;
            double raten = rate_n();
            double massavailn = mass_availn();
            double ldmax = ld_max();
            ld_n = Math.Min(massavailn / raten, 60 * ldmax);
            return ld_n;
        }
        // 7.3
        public double frac_ro()
        {
            double frac_ro = 0;
            if (frac_fsh < 0.5)
                frac_ro = 1 - 2 * frac_fsh;
            else
                frac_ro = 0;           
            return frac_ro;
        }
        // 7.4
        public double W_n_pool()
        {
            double W_n_pool = 0;
            double raten = rate_n();
            double fracro = frac_ro();            
            W_n_pool = raten * fracro ;
            return W_n_pool;
        }
        // 7.5
        public double W_n_jet()
        {
            double W_n_jet = 0;
            double raten = rate_n();
            double fracro = frac_ro();
            W_n_jet = raten * (1 - fracro);
            return W_n_jet;
        }
        // 7.6
        public double frac_entl()
        {
            double frac_entl = 0;
            double fracro = frac_ro();
            frac_entl = (frac_l * frac_fsh) / (1 - fracro);
            return frac_entl;
        }
        // 7.7
        public double erate_n()
        {
            double erate_n = 0;
            double r_pn = Math.Sqrt(2 / 3) * Math.Pow((8 * 9.81 * V_pn) / (Math.PI), 0.25) * Math.Pow(t_pn, 0.75);
            double ksurf = rbi.getksurf(surface);
            double xsurf = rbi.getxsurf(surface);
            double alphasurf = rbi.getalphasurf(surface);

            if (releasePhase.Equals("Vapor"))
                erate_n = W_n_jet();
            else if (releasePhase.Equals("Liquid"))
                if (poolfireType.Equals("Non-boiling"))
                    erate_n = double.Parse(rbi.getC(15)) * P_bg * double.Parse(rbi.getMw(fluid)) * Math.Pow(r_pn, 1.89) * Math.Pow(u_w, 0.78);
                else if (poolfireType.Equals("Boiling"))
                    erate_n = Math.Pow(Math.PI, 1.5) * xsurf * ksurf * (t_g - t_b) * Math.Pow(2 * 9.81 * V_pn, 0.5) * t_pn / (double.Parse(rbi.getC(14)) * 90 *Math.Sqrt(Math.PI * alphasurf));
            
            return erate_n;
        }
         
        // Step 8 flamable explosive Consequences area
        public double ca_cmdflame()
        {
            double ca_cmdflame = 0;
            flamable_explosiveConsequences.UFL = UFL;
            flamable_explosiveConsequences.LFL = UFL;
            flamable_explosiveConsequences.W_n = W_n();
            flamable_explosiveConsequences.mass_vce = mass_vce;
            flamable_explosiveConsequences.mass_availn = mass_availn();
            flamable_explosiveConsequences.frac_ro = frac_ro();
            flamable_explosiveConsequences.mfrac_flame = mfrac_flame;
            flamable_explosiveConsequences.rate_n = rate_n();
            flamable_explosiveConsequences.frac_fsh = frac_fsh;
            flamable_explosiveConsequences.fluid = fluid;
            flamable_explosiveConsequences.releaseType = releaseType();
            flamable_explosiveConsequences.fluidPhase = fluidPhase;
            flamable_explosiveConsequences.releasePhase = releasePhase;
            flamable_explosiveConsequences.poolType = poolfireType;
            flamable_explosiveConsequences.holeNumber = holeNumber;
            flamable_explosiveConsequences.r_e = r_e;
            flamable_explosiveConsequences.gff_n = gff_n();
            flamable_explosiveConsequences.gff_total = gff_total();
            flamable_explosiveConsequences.p_l = rbi.getPl(fluid);
            flamable_explosiveConsequences.p_v = p_v;
            flamable_explosiveConsequences.p_s = p_s;
            flamable_explosiveConsequences.p_atm = p_atm;
            flamable_explosiveConsequences.t_s = t_s;
            flamable_explosiveConsequences.t_b = t_b;
            flamable_explosiveConsequences.t_atm = t_atm;
            flamable_explosiveConsequences.c_pl = c_pl;
            flamable_explosiveConsequences.W_n_pool = W_n_pool();
            flamable_explosiveConsequences.W_n_jet = W_n_jet();
            flamable_explosiveConsequences.ambientCondition = ambientCondition;
            flamable_explosiveConsequences.u_w = u_w;
            flamable_explosiveConsequences.RH = RH;
            flamable_explosiveConsequences.xs_cmdn_pool = xs_cmdn_pool;
            flamable_explosiveConsequences.xs_injn_pool = xs_injn_pool;
            flamable_explosiveConsequences.xs_cmdn_jet = xs_cmdn_jet;
            flamable_explosiveConsequences.xs_injn_jet = xs_injn_jet;
            flamable_explosiveConsequences.xs_cmdn_fb = xs_cmdn_fb;
            flamable_explosiveConsequences.xs_injn_fb = xs_injn_fb;
            flamable_explosiveConsequences.xs_cmdn_vce = xs_cmdn_vce;
            flamable_explosiveConsequences.xs_injn_vce = xs_injn_jet;
            flamable_explosiveConsequences.ca_injn_flash = gradecloudArea;

            ca_cmdflame = flamable_explosiveConsequences.ca_cmd_flame();
            return ca_cmdflame;
        }
        public double ca_injflame()
        {
            double ca_injflame = 0;
            flamable_explosiveConsequences.UFL = UFL;
            flamable_explosiveConsequences.LFL = UFL;
            flamable_explosiveConsequences.W_n = W_n();
            flamable_explosiveConsequences.mass_vce = mass_vce;
            flamable_explosiveConsequences.mass_availn = mass_availn();
            flamable_explosiveConsequences.frac_ro = frac_ro();
            flamable_explosiveConsequences.mfrac_flame = mfrac_flame;
            flamable_explosiveConsequences.rate_n = rate_n();
            flamable_explosiveConsequences.frac_fsh = frac_fsh;
            flamable_explosiveConsequences.fluid = fluid;
            flamable_explosiveConsequences.releaseType = releaseType();
            flamable_explosiveConsequences.fluidPhase = fluidPhase;
            flamable_explosiveConsequences.releasePhase = releasePhase;
            flamable_explosiveConsequences.poolType = poolfireType;
            flamable_explosiveConsequences.holeNumber = holeNumber;
            flamable_explosiveConsequences.r_e = r_e;
            flamable_explosiveConsequences.gff_n = gff_n();
            flamable_explosiveConsequences.gff_total = gff_total();
            flamable_explosiveConsequences.p_l = rbi.getPl(fluid);
            flamable_explosiveConsequences.p_v = p_v;
            flamable_explosiveConsequences.p_s = p_s;
            flamable_explosiveConsequences.p_atm = p_atm;
            flamable_explosiveConsequences.t_s = t_s;
            flamable_explosiveConsequences.t_b = t_b;
            flamable_explosiveConsequences.t_atm = t_atm;
            flamable_explosiveConsequences.c_pl = c_pl;
            flamable_explosiveConsequences.W_n_pool = W_n_pool();
            flamable_explosiveConsequences.W_n_jet = W_n_jet();
            flamable_explosiveConsequences.ambientCondition = ambientCondition;
            flamable_explosiveConsequences.u_w = u_w;
            flamable_explosiveConsequences.RH = RH;
            flamable_explosiveConsequences.xs_cmdn_pool = xs_cmdn_pool;
            flamable_explosiveConsequences.xs_injn_pool = xs_injn_pool;
            flamable_explosiveConsequences.xs_cmdn_jet = xs_cmdn_jet;
            flamable_explosiveConsequences.xs_injn_jet = xs_injn_jet;
            flamable_explosiveConsequences.xs_cmdn_fb = xs_cmdn_fb;
            flamable_explosiveConsequences.xs_injn_fb = xs_injn_fb;
            flamable_explosiveConsequences.xs_cmdn_vce = xs_cmdn_vce;
            flamable_explosiveConsequences.xs_injn_vce = xs_injn_jet;
            flamable_explosiveConsequences.ca_injn_flash = gradecloudArea;

            ca_injflame = flamable_explosiveConsequences.ca_inj_flame();
            return ca_injflame;
        }


        // Step 9 toxic consequences area
        public double mass_n()
        {
            double mass_n = 0;
            mass_n = Math.Min(rate_n() * ld_n(), mass_availn()); 

            return mass_n;
        }
        public double ca_cmdtox()
        {
            double ca_cmdtox = 0;
            return ca_cmdtox;
        }
        public double ca_injtox()
        {
            double ca_injtox = 0;
            toxicConsequences.molefrac_tox = molefrac_tox;
            toxicConsequences.mass_n = mass_n();
            toxicConsequences.ld_maxn = ld_max();
            toxicConsequences.W_n = W_n();
            toxicConsequences.toxicComponent = toxicComponent; //"Hydro Sulfide";
            toxicConsequences.criteria = criteria; //"AEGL3-10";
            toxicConsequences.ca_n_cloud = ca_n_cloud;
            ca_injtox = toxicConsequences.ca_tox();
            return ca_injtox;
        }


        // Step 10 non flammable non toxic consequence area
        public double ca_cmdnfnt()
        {
            double ca_cmdnfnt = 0;
            noneFlamableNoneToxicConsequences.representFluid = representFluid;
            noneFlamableNoneToxicConsequences.eventTreeType = eventTreeType;
            noneFlamableNoneToxicConsequences.releasePhase = releasePhase;
            noneFlamableNoneToxicConsequences.rate_n = rate_n();
            noneFlamableNoneToxicConsequences.mass_n = mass_n();
            noneFlamableNoneToxicConsequences.p_s = p_s;
            noneFlamableNoneToxicConsequences.p_atm = p_atm;
            noneFlamableNoneToxicConsequences.t_s = t_s;
            noneFlamableNoneToxicConsequences.v_s = v_s;
            noneFlamableNoneToxicConsequences.n_v = n_v;
            noneFlamableNoneToxicConsequences.k = k;
            noneFlamableNoneToxicConsequences.xs_cmdn_pexp = xs_cmdn_pexp;
            noneFlamableNoneToxicConsequences.xs_cmdn_bleve = xs_cmdn_bleve;
            noneFlamableNoneToxicConsequences.xs_injn_pexp = xs_injn_pexp;
            noneFlamableNoneToxicConsequences.xs_injn_bleve = xs_injn_bleve;
            ca_cmdnfnt = noneFlamableNoneToxicConsequences.ca_cmd_nfnt();
            return ca_cmdnfnt;
        }
        public double ca_injnfnt()
        {
            double ca_injnfnt = 0;
            noneFlamableNoneToxicConsequences.representFluid = representFluid;
            noneFlamableNoneToxicConsequences.eventTreeType = eventTreeType;
            noneFlamableNoneToxicConsequences.releasePhase = releasePhase;
            noneFlamableNoneToxicConsequences.rate_n = rate_n();
            noneFlamableNoneToxicConsequences.mass_n = mass_n();
            noneFlamableNoneToxicConsequences.p_s = p_s;
            noneFlamableNoneToxicConsequences.p_atm = p_atm;
            noneFlamableNoneToxicConsequences.t_s = t_s;
            noneFlamableNoneToxicConsequences.v_s = v_s;
            noneFlamableNoneToxicConsequences.n_v = n_v;
            noneFlamableNoneToxicConsequences.k = k;
            noneFlamableNoneToxicConsequences.xs_cmdn_pexp = xs_cmdn_pexp;
            noneFlamableNoneToxicConsequences.xs_cmdn_bleve = xs_cmdn_bleve;
            noneFlamableNoneToxicConsequences.xs_injn_pexp = xs_injn_pexp;
            noneFlamableNoneToxicConsequences.xs_injn_bleve = xs_injn_bleve;
            ca_injnfnt = noneFlamableNoneToxicConsequences.ca_inj_nfnt();
            return ca_injnfnt;
        }


        // Step 11 component and injury consequences
        public double ca_cmd()
        {
            double ca_cmd = 0;
            double cacmdflame = ca_cmdflame();
            double cacmdntft = ca_cmdnfnt();
            ca_cmd = Math.Max(cacmdflame, cacmdntft);
            return ca_cmd;
        }
        public double ca_inj()
        {
            double ca_inj = 0;
            double cainjflame = ca_injflame();
            double cainjnfnt = ca_injnfnt();
            double cainjtox = ca_injtox();
            ca_inj = Math.Max(Math.Max(cainjflame, cainjtox), cainjnfnt);
            return ca_inj;
        }

        // Step 12: financial consequence
        // 12.1
        public double fc_cmd()
        {
            double fc_cmd = 0;            
            double gffn = gff_n();
            double gfftotal = gff_total();
            double holecost = rbi.getHolecost(componentType, (int) holeNumber);
            double matcost = rbi.getMatcost(material);

            fc_cmd = gffn * holecost * matcost / gfftotal;
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
            double gffn = gff_n();
            double gfftotal = gff_total();
            double outagen = rbi.getOutage(componentType, (int)holeNumber);

            outage_cmd = gffn * outagen * outage_mult / gfftotal;
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
        public double vol_n_env()
        {
            double vol_n_env = 0;
            double massn = mass_n();
            double frac_evap = rbi.getfracEvap(fluid);

            vol_n_env = double.Parse(rbi.getC(13)) * massn * (1 - frac_evap) / p_l;
            return vol_n_env;
        }
        public double fc_environ()
        {
            double fc_environ = 0;
            double volnenv = vol_n_env();
            double gffn = gff_n();
            double gfftotal = gff_total();

            fc_environ = gffn * volnenv * envcost / gfftotal;
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
    }
}
