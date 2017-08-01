using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class flamable_explosiveConsequencesLvl2
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();

        // lower flammability limit
        public double LFL { set; get; }
        // upper flammability limit
        public double UFL { set; get; }
        // W_n from 3.4
        public double W_n { set; get; }
        // mass of flammable material in vapor cloud
        public double mass_vce { set; get; }
        //available mass from 4.7
        public double mass_availn { set; get; }
        //mass_n mass from 9.1
        public double mass_n { set; get; }        
        //  fraction of fluid rain out from 7.3
        public double frac_ro { set; get; }
        // mass fraction of release rate
        public double mfrac_flame { set; get; }
        //release rate from 7.1
        public double rate_n { set; get; }
        // mass fraction of the stored fluid that flashes to vapor upon release
        public double frac_fsh { set; get; }        
        // fluid
        public String fluid { set; get; }
        // release type of fluid
        public String releaseType { set; get; }
        // stored phase of fluid
        public String fluidPhase { set; get; }
        // phase of fluid upon release
        public String releasePhase { set; get; }
        // pool fire type
        public String poolType { set; get; }
        //represent Fluid
        public String representFluid { set; get; }
        // hole number
        public double holeNumber { set; get; }
        // reynold number
        public double r_e { set; get; }
        // gff of hole number
        public double gff_n { set; get; }
        // gff total
        public double gff_total { set; get; }
        // liquid density kg/m3
        public double p_l { set; get; }
        // vapor density kg/m3
        public double p_v { set; get; }
       // stored pressure kPa
        public double p_s { get; set; }
        // atmosphere pressure kPa
        public double p_atm { set; get; }
        // storage temperature kPa
        public double t_s { set; get; }
        // bubble-point temperature K
        public double t_b { set; get; }
        // atmosphere temperature K
        public double t_atm { set; get; }
        // specific heat of pool liquid J/kg-K
        public double c_pl { set; get; }
        // portion of release rate forms a pool from step 7.4 kg/s
        public double W_n_pool { set; get; }
        // portion of release rate forms a jet from step 7.5 kg/s
        public double W_n_jet { set; get; }
        // ambient condition
        public String ambientCondition { set; get; }
        // wind speed m/s
        public double u_w { set; get; }
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
        // ca_injn_flash = grade cloud area
        public double ca_injn_flash { set; get; }
   

        // 8.2 flammable release rate
        public double rate_n_flame()
        {
            return rate_n * mfrac_flame;
        }
        public double rate_ln_flame()
        {
            return rate_n_flame() * (1 - frac_fsh);
        }
        public double rate_vn_flame()
        {
            return rate_n_flame() * frac_fsh;
        }


        // 8.4 probability of ignition of the release
        public double poi_ln_amb()
        {
            return 1.00982 - 0.70372 * Math.Log(double.Parse(rbi.getMw(fluid))) - 0.013045 * Math.Log(double.Parse(rbi.getC(4)) * rate_ln_flame()) + 0.18554 * Math.Log(Math.Pow(double.Parse(rbi.getMw(fluid)),2)) - 0.0014619 * Math.Log(Math.Pow(double.Parse(rbi.getC(4)) * rate_ln_flame(),2)) - 0.022131 * Math.Log(double.Parse(rbi.getMw(fluid))) * Math.Log(double.Parse(rbi.getC(4)) * rate_ln_flame()) - 0.016572 * Math.Log(Math.Pow(double.Parse(rbi.getMw(fluid)),3)) + 0.00011281 * Math.Log(Math.Pow(double.Parse(rbi.getC(4)) * rate_ln_flame(),3)) + 0.00050697 * Math.Log(double.Parse(rbi.getMw(fluid))) * Math.Log(Math.Pow(double.Parse(rbi.getC(4)) * rate_ln_flame(),2)) - 0.0035535 * Math.Log(Math.Pow(double.Parse(rbi.getMw(fluid)),2)) * Math.Log(double.Parse(rbi.getC(4)) * rate_ln_flame());
            
        }
        public double poi_vn_amb()
        {
            return (1.16928 - 0.39309 * Math.Log(double.Parse(rbi.getMw(fluid))) - 0.053213 * Math.Log(double.Parse(rbi.getC(4)) * rate_vn_flame()) + 0.033904 * Math.Log(Math.Pow(double.Parse(rbi.getMw(fluid)), 2)) - 0.0028936 * Math.Log(Math.Pow(double.Parse(rbi.getC(4)) * rate_ln_flame(), 2)) - 0.0067701 * Math.Log(double.Parse(rbi.getMw(fluid))) * Math.Log(double.Parse(rbi.getC(4)) * rate_ln_flame()) / (1 - 0.00110843 * Math.Log(double.Parse(rbi.getMw(fluid))) - 0.094276 * Math.Log(double.Parse(rbi.getC(4)) * rate_vn_flame()) + 0.029813 * Math.Log(Math.Pow(double.Parse(rbi.getMw(fluid)), 2)) + 0.0031951 * Math.Log(Math.Pow(double.Parse(rbi.getC(4)) * rate_ln_flame(), 2)) - 0.058105 * Math.Log(double.Parse(rbi.getMw(fluid))) * Math.Log(double.Parse(rbi.getC(4)) * rate_vn_flame())));

        }
        public double poi_l_ait()
        {
            return 1.0;
        }
        public double poi_v_ait()
        {
            double max;
            double temp = (170 - double.Parse(rbi.getMw(fluid)))/(170 - 2);
            if (temp > 0)
                max = 0.7 + temp;
            else
                max = 0.7;
            return max;
        }
        public double poi_ln()
        {
            return poi_ln_amb() + (poi_l_ait() - poi_ln_amb()) * (t_s - double.Parse(rbi.getC(16))) / (double.Parse(rbi.getAIT(fluid)) - double.Parse(rbi.getC(16)));
        }
        public double poi_vn()
        {
            return poi_vn_amb() + (poi_v_ait() - poi_vn_amb()) * (t_s - double.Parse(rbi.getC(16))) / (double.Parse(rbi.getAIT(fluid)) - double.Parse(rbi.getC(16)));
        }
        public double poi_2n()
        {
            return poi_ln() * frac_fsh + poi_vn() * (1 - frac_fsh);
        }


        // 8.5 probability of a immidiate ignition given ignition given immediate release 
        public double poii(String fp)
        {
            double poii = 0;
            if (releaseType.Equals("Continuous"))
            {
                if (fp.Equals("Liquid"))
                {
                    if (ambientCondition.Equals("Ambient Temperature"))
                        poii = rbi.getpoii_n_amb_ambientTemperature(releaseType, fp);
                    else if (ambientCondition.Equals("AIT"))
                        poii = rbi.getpoii_AIT(releaseType, fp);
                }
                else if (fp.Equals("Vapor"))
                {
                    if (ambientCondition.Equals("Ambient Temperature"))
                        poii = rbi.getpoii_n_amb_ambientTemperature(releaseType, fp);
                    else if (ambientCondition.Equals("AIT"))
                        poii = rbi.getpoii_AIT(releaseType, fp);
                }
            }
            else if (releaseType.Equals("Instantaneous"))
            {
                if (fp.Equals("Liquid"))
                {
                    if (ambientCondition.Equals("Ambient Temperature"))
                        poii = rbi.getpoii_n_amb_ambientTemperature(releaseType, fp);
                    else if (ambientCondition.Equals("AIT"))
                        poii = rbi.getpoii_AIT(releaseType, fp);
                }
                else if (fp.Equals("Vapor"))
                {
                    if (ambientCondition.Equals("Ambient Temperature"))
                        poii = rbi.getpoii_n_amb_ambientTemperature(releaseType, fp);
                    else if (ambientCondition.Equals("AIT"))
                        poii = rbi.getpoii_AIT(releaseType, fp);
                }
            }
            return poii;
        }
        public double poii_ln()
        {
            double poii_ln_amb = poii("Liquid");
            return poii_ln_amb + (1.0 - poii_ln_amb) * (t_s - double.Parse(rbi.getC(16))) / (double.Parse(rbi.getAIT(fluid)) - double.Parse(rbi.getC(16)));
        }
        public double poii_vn()
        {
            double poii_vn_amb = poii("Vapor");
            return poii_vn_amb + (1.0 - poii_vn_amb) * (t_s - double.Parse(rbi.getC(16))) / (double.Parse(rbi.getAIT(fluid)) - double.Parse(rbi.getC(16)));
        }
        public double poii_2n()
        {
            return frac_fsh * poii_ln() + (1 - frac_fsh) * poii_vn();
        }


        // 8.6 probability of a vce given delayed ignition
        public double pvce(String fp)
        {
            double pvce = rbi.getpvcedi(releaseType, fp);
            return pvce;
        }
        public double pvcedi_ln()
        {
            return pvce("Liquid");
        }
        public double pvcedi_vn()
        {
            return pvce("Vapor");
        }
        public double pvcedi_2n()
        {
            return frac_fsh * pvcedi_ln() + (1 - frac_fsh) * pvcedi_vn();
        }


        // 8.7 probability of a flasf fire given delayed ignition 
        public double pffdi_ln()
        {
            double pffdi_ln = 1 - pvcedi_ln();
            return pffdi_ln;
        }
        public double pffdi_vn()
        {
            double pffdi_vn = 1 - pvcedi_vn();
            return pffdi_vn;
        }
        public double pffdi_2n(String fp)
        {
            double pffdi_2n = frac_fsh * pffdi_ln() + (1 - frac_fsh) * pffdi_vn();
            return pffdi_2n;
        }


        // 8.8 probability of a fireball given immediate release 
        public double pfbii()
        {
            double pfbii = 0;
            if ((releaseType.Equals("Instantaneous") || fluidPhase.Equals("Vapor")) & releaseType.Equals("Instantaneous") || fluidPhase.Equals("Two-phase"))
                pfbii = 1;
            else
                pfbii = 0;
            return pfbii;
        }


        // 8.9 select appropriate event tree type
        public String eventTreeType()
        {
            String eventTreeType = "";
            if ((holeNumber == 1) || (holeNumber == 2) || (holeNumber == 3))
                eventTreeType = "Leakage";
            else if (holeNumber == 4)
                eventTreeType = "Rupture";
            return eventTreeType;
        }


        // 8.10 probability of all possible event outcomes
        public double p_poolfire()
        {
            double p_poolFire = 0;
            String eTT = eventTreeType();
            double poiln = poi_ln();
            double poiiln = poii_ln();
            double poi2n = poi_2n();
            double poii2n = poii_2n();
            double pvcedivn = pvcedi_vn();
            double pfbii2r = pfbii();
            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_poolFire = 0;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_poolFire = poiln * poiiln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    if (releaseType.Equals("Instantaneous"))
                        p_poolFire = poi2n * poii2n;
                    else
                        p_poolFire = 0;
                    
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_poolFire = 0;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_poolFire = poiln * poiiln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_poolFire = poi2n * poii2n * (1 - pfbii2r);
                }
            }
            
            return p_poolFire;
        }
        public double p_jetfire()
        {
            double p_jetfire = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();
            double poiivn = poii_vn();
            double poi2n = poi_2n();
            double poii2n = poii_2n();
           
            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    if(releaseType.Equals("Continuous"))
                        p_jetfire = poivn * poiivn;
                    else
                        p_jetfire = 0;
                    
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_jetfire = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    if (releaseType.Equals("Continuous"))
                        p_jetfire = poi2n * poii2n;
                    else
                        p_jetfire = 0;                 
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_jetfire = 0;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_jetfire = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_jetfire = 0;
                }
            }

            return p_jetfire;
        }
        public double p_fireball()
        {
            double p_fireball = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();
            double poiivn = poii_vn();
            double poi2n = poi_2n();
            double poii2n = poii_2n();
            double pfbii2r = pfbii();
            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    if (releaseType.Equals("Instantaneous"))
                        p_fireball = poivn * poiivn;
                    else
                        p_fireball = 0; 
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_fireball = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    if (releaseType.Equals("Instantaneous"))
                        p_fireball = poi2n * poii2n;
                    else
                        p_fireball = 0; 
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_fireball = poivn * poiivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_fireball = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_fireball = poi2n * poii2n * pfbii2r;
                }
            }

            return p_fireball;
        }
        public double p_vce()
        {
            double p_vce = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();
            double poiivn = poii_vn();
            double poiln = poi_ln();
            double poiiln = poii_ln();
            double poi2n = poi_2n();
            double poii2n = poii_2n();
            double pfbii2r = pfbii();
            double pvcedivn = pvcedi_vn();
            double pvcediln = pvcedi_ln();
            double pvcedi2n = pvcedi_2n();
            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_vce = poivn * (1 - poiivn) * pvcedivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_vce = poiln * (1 - poiiln) * pvcediln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_vce = poi2n * (1 - poii2n) * pvcedi2n;
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_vce = poivn * (1 - poiivn) * pvcedivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_vce = poiln * (1 - poiiln) * pvcediln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_vce = poi2n * (1 - poii2n) * pvcedi2n;
                }
            }

            return p_vce;
        }
        public double p_flashfire()
        {
            double p_flashfire = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();
            double poiivn = poii_vn();
            double poiln = poi_ln();
            double poiiln = poii_ln();
            double poi2n = poi_2n();
            double poii2n = poii_2n();
            double pfbii2r = pfbii();
            double pvcedivn = pvcedi_vn();
            double pvcediln = pvcedi_ln();
            double pvcedi2n = pvcedi_2n();
            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_flashfire = poivn * (1 - poiivn) * (1 - pvcedivn);
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_flashfire = poiln * (1 - poiiln) * (1 - pvcediln);
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_flashfire = poi2n * (1 - poii2n) * (1 - pvcedi2n);
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_flashfire = poivn * (1 - poiivn) * (1 - pvcedivn);
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_flashfire = poiln * (1 - poiiln) * (1 - pvcediln);
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_flashfire = poi2n * (1 - poii2n) * (1 - pvcedi2n);
                }
            }

            return p_flashfire;
        }
        public double p_safedispersion()
        {
            double p_safedispersion = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();
            double poiln = poi_vn();
            double poi2n = poi_2n();

            if (eTT.Equals("Leakage"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_safedispersion = 1 - poivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_safedispersion = 1 - poiln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_safedispersion = 1 - poi2n;
                }
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_safedispersion = poivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_safedispersion = 1 - poiln;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_safedispersion = 0;
                }
            }

            return p_safedispersion;
        }
        public double p_pe()
        {
            double p_pe = 0;
            String eTT = eventTreeType();
            double poivn = poi_vn();

            if (eTT.Equals("Leakage"))
            {
                p_pe = 0;
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_pe = 1 - poivn;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_pe = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_pe = 0;
                }
            }
            return p_pe;
        }
        public double p_bleve()
        {
            double p_bleve = 0;
            String eTT = eventTreeType();
            double poi2n = poi_2n();

            if (eTT.Equals("Leakage"))
            {
                p_bleve = 0;
            }
            else if (eTT.Equals("Rupture"))
            {
                if (releasePhase.Equals("Vapor"))
                {
                    p_bleve = 0;
                }
                else if (releasePhase.Equals("Liquid"))
                {
                    p_bleve = 0;
                }
                else if (releasePhase.Equals("Two-phase"))
                {
                    p_bleve = 1 - poi2n;
                }
            }
            return p_bleve;
        }
        // 8.11 component damage and personal injury of fire pool
        public double m_b()
        {
            double m_b = 0;
            if (poolType.Equals("Non-boiling"))
                m_b = (double.Parse(rbi.getC(17)) * 45 * Math.Pow(10, 6)) / (c_pl * (t_b - t_atm) + 20 * Math.Pow(10, 5));
            else if (poolType.Equals("Boiling"))
                m_b = (double.Parse(rbi.getC(17)) * 45 * Math.Pow(10, 6)) / (20 * Math.Pow(10, 5));           
            return m_b;
        }
        public double aburn_pfn()
        {
            double aburn_pfn = 0;
            double mb = m_b();
            aburn_pfn = W_n_pool / mb;
            return aburn_pfn;
        }
        public double a_max_pfn()
        {
            double a_max_pfn = 0;
            a_max_pfn = mass_availn / (double.Parse(rbi.getC(18)) * frac_ro * p_l);
            return a_max_pfn;
        } 
        public double a_pfn()
        {
            double a_pfn = 0;
            double amaxpfn = a_max_pfn();
            double aburnpfn = aburn_pfn();
            a_pfn = Math.Min(Math.Min(amaxpfn, aburnpfn), double.Parse(rbi.getC(7)) * 929.1);
            return a_pfn;
        }
        public double r_pfn()
        {
            double r_pfn = 0;
            double apfn = a_pfn(); 
           
            r_pfn = Math.Sqrt(apfn/Math.PI);
            return r_pfn;
        }
        public double u_sn()
        {
            double u_sn = 0;
            double mb = m_b();
            double rpfn = r_pfn();
            u_sn = Math.Max(1.0, u_w * Math.Pow(p_v/(2 * 9.81 * mb * rpfn), 0.333));
            return u_sn;
        }
        public double l_pfn()
        {
            double l_pfn = 0;
            double mb = m_b();
            double rpfn = r_pfn();
            double usn = u_sn();
            l_pfn = 110 * rpfn * Math.Pow(mb / (p_atm * Math.Sqrt(2 * 9.81 * rpfn)), 0.67) * Math.Pow(usn, -0.21);
            return l_pfn;
        }
        public double O_pfn()
        {
            double O_pfn = 0;            
            double usn = u_sn();
            O_pfn = Math.Acos(1 / usn);
            return O_pfn;
        }
        public double qrad_n_pool()
        {
            double qrad_n_pool = 0;
            double lpfn = l_pfn();
            double rpfn = r_pfn();
            double mb = m_b();
            qrad_n_pool = (double.Parse(rbi.getC(14)) * 0.35 * mb * 45 * Math.Pow(10, 6) * Math.PI * Math.Pow(rpfn, 2)) / (2 * Math.PI * rpfn * lpfn + Math.PI * Math.Pow(rpfn, 2));
            return qrad_n_pool;
        }
        public double t_aimn(double xs_n)
        {
            double t_aimn = 0;
            double pw = double.Parse(rbi.getC(20)) * RH * Math.Exp(14.4114 - double.Parse(rbi.getC(19))/t_atm);

            t_aimn = 0.819 * Math.Pow(pw * xs_n, -0.09);
            return t_aimn;
        }
        public double f_cyln(double xs_n_pool)
        {
            double f_cyln = 0;
            double lpfn = l_pfn();
            double rpfn = r_pfn();
            double opfn = O_pfn();
            double X = lpfn / rpfn;
            double Y = xs_n_pool / rpfn;
            double A = Math.Pow(X, 2) + Math.Pow(Y + 1, 2) - 2 * X * (Y + 1) * Math.Sin(opfn);
            double B = Math.Pow(X, 2) + Math.Pow(Y - 1, 2) - 2 * X * (Y - 1) * Math.Sin(opfn);
            double C = 1 + (Math.Pow(Y, 2) -1) * Math.Pow(Math.Cos(opfn), 2);
            double fvn = (X * Math.Cos(opfn)) / (Y - X * Math.Sin(opfn)) * (Math.Pow(X, 2) + Math.Pow(Y + 1, 2) - 2 * Y * (1 + Math.Sin(opfn))) / (Math.PI * Math.Sqrt(A * B)) * Math.Atan((A * (Y - 1)) / (B * (Y + 1))) + (Math.Cos(opfn)) / (Math.PI * Math.Sqrt(C)) * (Math.Atan((X * Y - (Math.Pow(Y, 2) * Math.Sin(opfn))) / (Math.Sqrt(Math.Pow(Y, 2) - 1) * Math.Sqrt(C))) + Math.Atan((Math.Sin(opfn) * Math.Sqrt(Math.Pow(Y, 2) - 1)) / (Math.Sqrt(C)))) - (X * Math.Cos(opfn)) / (Math.PI * (Y - X * Math.Sin(opfn))) * Math.Atan(Math.Sqrt((Y - 1) / (Y + 1)));
            double fhn = 1 / Math.PI * Math.Atan(Math.Sqrt((Y + 1) / (Y - 1))) - (Math.Pow(X, 2) + Math.Pow(Y + 1, 2) - 2 * (Y + 1 + X * Y * Math.Sin(opfn))) / (Math.PI * Math.Sqrt(A * B)) * Math.Atan(Math.Sqrt((A * (Y - 1)) / (B * (Y + 1)))) + (Math.Sin(opfn)) / (Math.PI * Math.Sqrt(C)) * (Math.Atan((X * Y - (Math.Pow(Y, 2) - 1) * Math.Sin(opfn)) / (Math.Sqrt(Math.Pow(Y, 2) - 1) * Math.Sqrt(C)) + Math.Atan((Math.Sin(opfn) * Math.Sqrt(Math.Pow(Y, 2) - 1))/(Math.Sqrt(C)))));

            f_cyln = Math.Sqrt(Math.Pow(fvn, 2) + Math.Pow(fhn, 2));
            return f_cyln;
        }
        public double ith_n_pool(double xs_n_pool)
        {
            double ith_n_pool = 0;
            double tatmn = t_aimn(xs_n_pool);
            double qradnpool = qrad_n_pool();
            double fcyln = f_cyln(xs_n_pool);           

            ith_n_pool = double.Parse(rbi.getC(19)) * qradnpool * tatmn * fcyln;

            return ith_n_pool;
        }
        public double ca_pool(double xs_n_pool)
        {
            double ca_pool = 0;
            ca_pool = Math.PI * Math.Pow(xs_n_pool, 2);
            return ca_pool;
        }

        // 8.12 component damage and personal injury of jet fire
        public double qrad_n_jet()
        {
            double qrad_n_jet = 0;            
            double mb = m_b();
            qrad_n_jet = double.Parse(rbi.getC(14)) * 0.35 * W_n_jet * 55.50 * Math.Pow(10, 6);
            return qrad_n_jet;
        }
        public double fp_n(double xs_n)
        {
            double fp_n = 0;
            fp_n = 1 / (4 * Math.PI * Math.Pow(xs_n, 2));
            return fp_n;
        }
        public double ith_n_jet(double xs_n_jet)
        {
            
            double ith_n_jet = 0;
            double tatmn = t_aimn(xs_n_jet);
            double qradnjet = qrad_n_jet();
            double fpn = fp_n(xs_n_jet);
            

            ith_n_jet = tatmn * qradnjet * fpn;

            return ith_n_jet;
        }
        public double ca_jet(double xs_n_jet)
        {
            double ca_jet = 0;
            ca_jet = Math.PI * Math.Pow(xs_n_jet, 2);
            return ca_jet;
        }

        // 8.13 component damage and personal injury of fireball
        public double mass_fb()
        {
            double mass_fb = 0;
            mass_fb = mfrac_flame * mass_availn;
            return mass_fb;
        }
        public double d_max_fb()
        {
            double d_max_fb = 0;
            double massfb = mass_fb();
            d_max_fb = double.Parse(rbi.getC(22)) * Math.Pow(massfb, 0.333);
            return d_max_fb;
        }
        public double h_fb()
        {
            double h_fb = 0;
            double dmaxfb = d_max_fb();
            h_fb = 0.75 * dmaxfb;
            return h_fb;
        }
        public double t_fb()
        {
            double t_fb = 0;
            double massfb = mass_fb();
            if (massfb < 29.937)
                t_fb = double.Parse(rbi.getC(23)) * Math.Pow(massfb, 0.333);
            else
                t_fb = double.Parse(rbi.getC(24)) * Math.Pow(massfb, 0.167);
            return t_fb;
        }
        public double qrad_fball()
        {
            double qrad_fball = 0;
            double massfb = mass_fb();
            double dmax = d_max_fb();
            double tfb = t_fb();
            double Bfb = double.Parse(rbi.getC(25)) * Math.Pow(p_s, 0.32);
            qrad_fball = (double.Parse(rbi.getC(14)) * Bfb * massfb * 45 * Math.Pow(10, 6)) / (Math.PI * Math.Pow(dmax, 2) * tfb);
            return qrad_fball;
        }
        public double fsph(double xs_fball)
        {
            double fsph = 0;
            double dmax = d_max_fb();
            double cfb = Math.Sqrt(Math.Pow(dmax / 2, 2) + Math.Pow(xs_fball / 2, 2));
            fsph = Math.Pow(dmax, 2) / (4 * Math.Pow(cfb, 2));
            return fsph;
        }
        public double ith_n_fb(double xs_n_fb)
        {

            double ith_n_fb = 0;
            double tatmn = t_aimn(xs_n_fb);
            double qradnjet = qrad_fball();
            double fsph1 = fsph(xs_n_fb);
            ith_n_fb = tatmn * qradnjet * fsph1;

            return ith_n_fb;
        }
        public double ca_fb(double xs_fball)
        {
            double ca_fb = 0;
            ca_fb = Math.PI * Math.Pow(xs_fball, 2);
            return ca_fb;
        }

        // 8.14 component damage and personal injury of vce
        public double W_tnt()
        {
            double W_tnt = 0;
            W_tnt = (0.09 * mass_vce * 90 * Math.Pow(10, 6)) / (4648);
            return W_tnt;
        }
        public double R_hsn(double xs_n_vce)
        {
            double R_hsn = 0;
            double wtnt = W_tnt();
            R_hsn = (double.Parse(rbi.getC(27)) * xs_n_vce) / (Math.Pow(wtnt, 0.3333333333));
            return R_hsn;
        }
        public double P_son(double xs_n_vce)
        {
            double P_son = 0;
            double rhsn = R_hsn(xs_n_vce);
            P_son = Math.Abs(-0.059965896 + (1.1288697) / (Math.Log(rhsn)) - (7.9625216) / (Math.Pow(Math.Log(rhsn), 2)) + (25.106738) / (Math.Pow(Math.Log(rhsn), 3)) - (30.396707) / (Math.Pow(Math.Log(rhsn), 4)) + (19.399862) / (Math.Pow(Math.Log(rhsn), 5)) - (6.8853477) / (Math.Pow(Math.Log(rhsn), 6)) + (1.2825511) / (Math.Pow(Math.Log(rhsn), 7)) - (0.097705789) / (Math.Pow(Math.Log(rhsn), 8)));
            return P_son;
        }
        //public double xs_cmdn_vce()
        //{
        //    double xs_cmdn_vce = 0;
        //    double wtnt = W_tnt();
        //    do{
               
        //    } while ()
        //    return xs_cmdn_vce;
        //}

        public double ca_vce(double xs_n_vce)
        {
            double ca_vce = 0;
            ca_vce = Math.PI * Math.Pow(xs_n_vce, 2);
            return ca_vce;
        }
        public double p_r(double xs_n_vce)
        {
            double p_r = 0;
            double pson = P_son(xs_n_vce);
            p_r = Math.Abs(-23.8 + 2.92 * Math.Log(double.Parse(rbi.getC(28)) * pson));
            return p_r;
        }

        // 8.15 component damage and personal injury of flash fire
        public double ca_cmdn_flash()
        {
            double ca_cmdn_flash = 0;
            ca_cmdn_flash = 0.25 * ca_injn_flash;
            return ca_cmdn_flash;
        }

        // 8.16
        public double ca_cmdn_flame()
        {
            double ca_cmdn_flame = 0;
            ca_cmdn_flame = p_poolfire() * ca_pool(xs_cmdn_pool) + p_jetfire() * ca_jet(xs_cmdn_jet) + p_fireball() * ca_fb(xs_cmdn_fb) + p_vce() * ca_vce(xs_cmdn_vce) + p_flashfire() * ca_cmdn_flash();
            return ca_cmdn_flame;
        }
        public double ca_injn_flame()
        {
            double ca_injn_flame = 0;
            ca_injn_flame = p_poolfire() * ca_pool(xs_injn_pool) + p_jetfire() * ca_jet(xs_injn_jet) + p_fireball() * ca_fb(xs_injn_fb) + p_vce() * ca_vce(xs_injn_vce) + p_flashfire() * ca_injn_flash;
            return ca_injn_flame;
        }

        //8.17
        public double ca_cmd_flame()
        {
            double ca_cmd_flame = 0;
            ca_cmd_flame = gff_n * ca_cmdn_flame() / gff_total;
            return ca_cmd_flame;
        }
        public double ca_inj_flame()
        {
            double ca_inj_flame = 0;
            ca_inj_flame = gff_n * ca_injn_flame() / gff_total;
            return ca_inj_flame;
        }

    }
}
