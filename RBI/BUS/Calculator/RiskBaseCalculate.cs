using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class RiskBaseCalculate
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        ///<summary>
        /// Tinh toan PoF
        ///</summary>

        ///<summary>
        /// khai bao tham so truyen vao
        ///</summary>
        
        public String equipmentType { set; get; }
        public String componentType { set; get; }
        public bool crackPresent { set; get; }
        // DF_thinning
        public int noInsp { set; get; } // num of inspection
        public String catalog_thin { set; get; } // effective catalog
        public int age { set; get; } // thoi gian trong he thong ke tu lan inspec cuoi cung
        public double Trd { set; get; } // do day doc duoc( inc)
        public bool haveCladding { set; get; }// thanh phan co lop vo hay k?
        public bool protectionBarrier { set; get; }// neu la tank thi co protection barrier k?
        public double Crcm { set; get; } // toc do an mon lop vo
        public double Crbm { set; get; } // toc do an mon kim loai
        public double T { set; get; }// do day kim loai
        public double Tmin { set; get; }// do day toi thieu can thiet
        public double CA { set; get; } // tro cap an mon( inc)
        public int Fom_thin { set; get; }// Fom: table 5.13
        public int Fip { set; get; }// Fip: 1 or 3
        public int Fdl { set; get; }// Fdl: 1 or 3
        public int Fwd { set; get; }// Fwd: 1 or 10
        public int Fam { set; get; }// Fam: 1 or 5
        public int Fsm { set; get; }// Fsm(tank bottom only): 1,1.5,2
        // DF_linning
        public String linningType { set; get; }// xac dinh loai linning
        public int yearInService { set; get; }// dung de tra bang 6.5
        public double Fom_lin { set; get; }// Fom: 0.1 or 1.0
        public double Flc { set; get; }// Flc: poor-10, average-2, good-1
        // DF_caustic
        public bool haveCrack { set; get; } // co bi crack hay k?
        public String catalog_caustic { set; get; }// effect caustic
        public String level_caust { set; get; }// muc do crack: high, medium, low, none
        // DF_amine
        public String level_amine { set; get; }// muc do crack
        public String catalog_amine { set; get; }// effective amine
        // DF_sulfide
        public String catalog_sulf { set; get; } // effective sulfide
        public double pH { set; get; }// do PH cua nuoc, dung chung cho ca hic (dung chung toan bo)

        public bool isPWHT { set; get; } // he thong co su dung PWHT isPWHT ko dung?????
        public double ppm_sulf { set; get; } // chi so ppm - dung chung cho hic: nong do H2S trong nuoc
        public double PWHT { set; get; } //nhap vao gia tri cua PWHT
        public bool isCrackPresent { set; get; }
        // DF_HIC/SOHIC-H2S
        public String catalog_hicH2S { set; get; }// effective hic
        public double ppm_H2S { set; get; }
        // DF_cacbonate
        public String catalog_cacbon { set; get; } // effective cacbonate
        public double ppm_cacbon { set; get; } // nong do cacbonat trong nuoc
        // DF_pta
        public String catalog_pta { set; get; } // effective pta
        public String material { set; get; }// material
        public String fHT { set; get; }// function of heat treatment
        // DF_clscc
        public String catalog_clscc { set; get; } // effective clscc
        public double Temperature { set; get; }// xac dinh nhiet do( *F)
        public double ppm_clo { set; get; }// xac dinh nong do cua clorua
        // DF_hf
        public String catalog_hf { set; get; }// effective hf
        public bool hfPresent { set; get; }// hf present?
        public bool carbonateSteel { set; get; }// cacbonate steel?
        public double BrinellHardness { set; get; }// do cung 
        // DF_hic_hf
        public String catalog_hic_hf { set; get; } // effective hic-hf
        public double ppm_S { set; get; } // nong do Sulfur
        // DF_extd_corrosion
        public String catalog_extd { set; get; }// effective extd
        public int Fps_ext { set; get; }// Fps_ext: 2 or 1
        public int Fip_ext { set; get; }// Fip_ext: 2 or 1
        public int comInstallDate { set; get; }// component install date
        public double opTemp { set; get; }// operating temperate
        public String driver_extd { set; get; }// driver extend
        public String coatQuality { set; get; }// coating quality
        // DF_cui
        public String catalog_cui { set; get; }// effective cui
        public String driver_cui { set; get; }// trinh dieu khien an mon
        public bool expCR { set; get; }// chuyen gia co xac dinh he so an mon k
        public double CR { set; get; }// he so an mon co ban
        public String complexity { set; get; }// he so phuc tap
        public String insulation { set; get; }// he so cach nhiet
        public bool allowConfig { set; get; }// cho phep bao tri hay k
        public bool enterSoil { set; get; }// dat trong dat hay nuoc hay k
        public String isulationtype { set; get; }// insulation Type
        // DF_extd_clscc
        public String catalog_ext_clscc { set; get; }// effective extend clscc
        public String driver_ext_clscc { set; get; }// dieu khien ext_clscc
        // DF_extd_cui
        public String pipingComp { set; get; }// piping complexity
        public String insCondition { set; get; }// Insulation condition
        // DF_htha
        public int age_htha { set; get; }// thoi gian hoat dong, hours
        public double T_htha { set; get; }// temperature (*F)
        public double P_h2 { set; get; } // Pressure (MPa)
        public String materials { set; get; } // Material
        // DF_brittle
        public double tempMin { set; get; }// nhiet do thap nhat
        public double tempDesign { set; get; }// nhiet do thiet ke
        public double tempUpset { set; get; }
        public double tempBoiling { set; get; }
        public double MDMT { set; get; }// nhiet do kim loai thiet ke toi thieu
        public double tImpact { set; get; }// nhiet do va cham
        public String curve { set; get; }// material curve
        public bool lowTemp { set; get; }// he thong tx nhieu nam voi nhiet do thap
        // DF_temp_embrittle
        public double SCE { set; get; }// he so SCE
        // DF_ 885
        public double tRef { set; get; }// nhiet do chuyen tiep ban dau
        public double tMin_885 { set; get; }// nhiet do ban dau thap nhat
        public bool adminControl { set; get; }// admin co ngan ngua ap suat duoi 1 so nhiet do
        public bool brittleCheck { set; get; }// nhiet do deo -> gion co biet hay k
        // DF_sigma
        public double tShutdown { set; get; }// nhiet do tat may
        public double tUpset { set; get; }// nhiet do upset
        public double pSigma { set; get; }// % sigma
        // DF_piping_mechan
        public String noPreFatFailure { set; get; }// number of previous Fatigue Failures
        public String vibrationSVI { set; get; }// severity of vibration 
        public double noWeeks { set; get; }// number of weeks pipe has been shaking
        public String cyclicType { set; get; }// type of cyclic loading connected directly or indirectly within approximately 50 feet
        public String correctiveActions { set; get; }// corrective actions take
        public double totalpipeFitting { set; get; }// total pipe fitting
        public String jointType { set; get; }// type of joint in this piping
        public String pipeCondition { set; get; }// condition of pipe
        public double branchDiameter { set; get; }// branch diameter

        //Find min_max
        private double min_max(double a, double b, bool IsMin)
        {
            if (IsMin) return a < b ? a : b; //IsMin = true find min
            else return a > b ? a : b;       //IsMin = false find max
        }
        #region Thinning Damage Factor
        //Step 1: xác định lại hệ số Tmin
        private double getTmin()
        {
            double t;
            if (componentType.Equals("TANKBOTTOM"))
            {
                t = protectionBarrier ? 0.125 : 0.25; //inc
            }
            else
            {
                t = Tmin;
            }
            return t;
        }
        //Step 2: nếu có lớp vỏ thì tính toán agerc
        private double agerc()
        {
            return min_max((Trd - T) / Crcm, 0, false);
        }
        //Step 3: Tính toán Art
        private double Art()
        {
            if (!haveCladding)
            {
                return min_max(1 - (Trd - Crbm * age) / (getTmin() + CA), 0, false);
            }
            else
            {
                double x = 1 - (Trd - Crcm * agerc() - Crbm * (age - agerc())) / (getTmin() + CA);
                return min_max(x, 0, false);
            }
        }
        // B4: chuẩn hóa art
        private double getArt()
        {
            double art = 0;
            if (!componentType.Equals("TANKBOTTOM"))
            {
                double[] data = { 0.02, 0.04, 0.06, 0.08, 0.1, 0.12, 0.14, 0.16, 0.18, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5, 0.55, 0.6, 0.65 };
                if (Art() < data[0])
                    art = data[0];
                else if (Art() > data[data.Length - 1])
                    art = data[data.Length - 1];
                else
                {
                    for (int i = 1; i < data.Length - 1; i++)
                    {
                        if (Art() > (data[i - 1] + data[i]) / 2 && Art() < (data[i] + data[i + 1]) / 2)
                        {
                            art = data[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                double[] data = { 0.05, 0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5, 0.55, 0.6, 0.65, 0.7, 0.75, 0.8, 0.85, 0.9, 0.95, 1 };
                if (Art() < data[0])
                    art = data[0];
                else if (Art() > data[data.Length - 1])
                    art = data[data.Length - 1];
                else
                {
                    for (int i = 1; i < data.Length - 1; i++)
                    {
                        if (Art() > (data[i - 1] + data[i]) / 2 && Art() < (data[i] + data[i + 1]) / 2)
                        {
                            art = data[i];
                            break;
                        }
                    }
                }
            }
            return art;
        }
        // B5: get data from table 5.11 5.12 D_fb_thin
        private double getD_fb_thin()
        {
            if (componentType.Equals("TANKBOTOM"))
            {
                return rbi.D_fb_tank(getArt(), catalog_thin);
            }
            else
                return rbi.getDfb(getArt(), noInsp, catalog_thin);
        }
        // B6: xác định kết quả cuối cùng từ bước 5 và các giá trị
        public double D_f_thin()
        {
            if (componentType.Equals("TANKBOTOM"))
                return getD_fb_thin() * Fip * Fdl * Fwd * Fam * Fsm / Fom_thin;
            else
                return getD_fb_thin() * Fip * Fdl * Fwd * Fam / Fom_thin;
        } 
        #endregion

        #region Linning Damage Factor
        //Step 1: tra bảng 6.4 ra 1 thành phần của Dfb
        private double D_fb_liner_inorganic()
        {
            double dfb = rbi.getDf_liner_64(age, linningType);
            return dfb;
        }
        //Step 2: tra bảng 6.5 ra được thành phần còn lại Dfb
        private double D_fb_liner_organic()
        {

            double dfb = rbi.getDf_liner_65(yearInService, age);
            return dfb;
        }
        // tính tổng 2 thành phần lại
        private double D_fb_liner()
        {
            return D_fb_liner_inorganic() + D_fb_liner_organic();
        }
        //Step 3: xác định Df
        public double D_f_liner()
        {
            return D_fb_liner() * Fom_thin * Flc;
        }
        #endregion

        //SSC Damage Factor Caustic Cracking cần xem xét tính toán lại
        #region SSC Damage Factor Caustic Cracking 
        //Step 1: Determine SVI (table 7.3)
        private int SVI_Caustic()
        {
            int svi;
            if (level_caust.Equals("High"))
                svi = 5000;
            else if (level_caust.Equals("Medium"))
                svi = 500;
            else if (level_caust.Equals("Low"))
                svi = 50;
            else svi = 1;
            return svi;
        }
        // B2: xác định D_f_caustic từ bảng 7.4
        private int D_f_caustic()
        {
            String field = noInsp + catalog_caustic;
            return rbi.getD_f_scc(SVI_Caustic(), field);
        }
        //B3: xác định D_f từ D_f_caustic và age
        public double D_f()
        {
            return D_f_caustic() * Math.Pow(age, 1.1);
        }
        #endregion 

        #region SSC Damage Factor Amine Cracking
        //Step 1: xác định SVI( table 8.3)
        private int SVI_Amine()
        {
            int svi;
            if (level_amine.Equals("High"))
                svi = 1000;
            else if (level_amine.Equals("Medium"))
                svi = 100;
            else if (level_amine.Equals("Low"))
                svi = 10;
            else
                svi = 1;
            return svi;
        }
        //Step 2: xác định D_f_amine( table 7.4)
        private int D_fb_amine()
        {
            String field = noInsp + catalog_amine;
            return rbi.getD_f_scc(SVI_Amine(), field);
        }
        //Step 3: xác định D_f theo công thức
        public double D_f_amine()
        {
            return D_fb_amine() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SSC Damage Factor Sulfide Stress Cracking
        //Step 1: xac dinh do anh huong toi moi truong( table 9.3)
        private String EnvSeveritySulfide()
        {
            String ppmString = null;
            if (ppm_sulf < 50)
                ppmString = "<50";
            else if (ppm_sulf >= 50 && ppm_sulf < 1000)
                ppmString = "50-1000";
            else if (ppm_sulf >= 1000 && ppm_sulf <= 10000)
                ppmString = "1000-10000";
            else
                ppmString = ">10000";

            String pHString = null;
            if (pH < 5.5)
                pHString = "<5.5";
            else if (pH >= 5.5 && pH <= 7.5)
                pHString = "5.5-7.5";
            else if (pH >= 7.6 && pH <= 8.3)
                pHString = "7.6-8.3";
            else if (pH >= 8.4 && pH <= 8.9)
                pHString = "8.4-8.9";
            else
                pHString = ">9.0";
            return rbi.getEnvironmental(ppmString, pHString);
        }
        //Step 2: xac dinh scc tu bang 9.4
        private String SSC()
        {
            String pwhtString = null;
            if (PWHT < 200)
                pwhtString = "<200";
            else if (PWHT >= 200 && PWHT <= 237)
                pwhtString = "200-237";
            else
                pwhtString = ">237";
            return rbi.getSulfideStressCracking(EnvSeveritySulfide(), pwhtString);
        }
        // Step 3: tu B2- xac dinh SVI
        private int SVI_sulfide()
        {
            String data = SSC();
            int svi;
            if (isCrackPresent == true)
            {
                svi = 100;
            }
            else
            {
                if (data.Equals("High"))
                    svi = 100;
                else if (data.Equals("Medium"))
                    svi = 10;
                else if (data.Equals("Low"))
                    svi = 1;
                else
                    svi = 1;
            }
            return svi;
        }
        //Step 4: tu buoc 3 tra bang 7.4 ra duoc he so D_fb_ssc
        private int D_fb_ssc()
        {
            String field = noInsp + catalog_sulf;
            return rbi.getD_f_scc(SVI_sulfide(), field);
        }
        // B5: tinh ket qua cuoi cung D_f_ssc
        public double D_f_ssc()
        {
            return D_fb_ssc() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SSC Damage Factor HIC/SOHIC-H2S
        //B1: xac dinh muc do anh huong toi moi truong( table 10.3)
        private String EnvSeverityH2S()
        {
            String ppmString = null;
            if (ppm_H2S < 50)
                ppmString = "<50";
            else if (ppm_H2S >= 50 && ppm_H2S < 1000)
                ppmString = "50-1000";
            else if (ppm_H2S >= 1000 && ppm_H2S <= 10000)
                ppmString = "1000-10000";
            else
                ppmString = ">10000";

            String pHString = null;
            if (pH < 5.5)
                pHString = "<5.5";
            else if (pH >= 5.5 && pH <= 7.5)
                pHString = "5.5-7.5";
            else if (pH >= 7.6 && pH <= 8.3)
                pHString = "7.6-8.3";
            else if (pH >= 8.4 && pH <= 8.9)
                pHString = "8.4-8.9";
            else
                pHString = ">9.0";
            //Debug.WriteLine("Environ = " + rbi.getEnvironmental(ppmString, pHString));
            return rbi.getEnvironmental(ppmString, pHString);
        }
        // B2: xac dinh scc tu bang 10.4
        private String SCC()
        {
            String pwhtString = null;
            if (PWHT > 0.01)
                pwhtString = "High";
            else if (PWHT >= 0.002 && PWHT <= 0.01)
                pwhtString = "Low";
            else
                pwhtString = "Ultra";
            return rbi.getHIC(EnvSeverityH2S(), pwhtString);
        }
        // B3: xac dinh svi
        private int SVI_H2S()
        {
            String data = SCC();
            int svi;
            if (crackPresent == true)
            {
                svi = 100;
            }
            else
            {
                if (data.Equals("High"))
                    svi = 100;
                else if (data.Equals("Medium"))
                    svi = 10;
                else
                    svi = 1;
            }
            return svi;
        }
        // B4: xac dinh D_fb_hic tu bang 7.4
        private int D_fb_hic()
        {
            String field = noInsp + catalog_hicH2S;
            return rbi.getD_f_scc(SVI_H2S(), field);
        }
        // B5: tinh D_f_hic bang cong thuc
        public double D_f_hic()
        {
            return D_fb_hic() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SSC Damage Factor Carbonate Cracking
        //Step 1: Determine the number of inspections, and the corresponding inspection effectiveness category
        //Step 2: Determine the time in-service, age , since the last Level A, B, C or D inspection was performed
        //Step 3: Determine the susceptibility for cracking
        private String susceptibility()
        {
            String _pH = null;
            if (pH >= 7.6 && pH <= 8.3) _pH = "7.6-8.3";
            else if (pH > 8.3 && pH < 9.0) _pH = "8.3-9.0";
            else _pH = "=>9.0";
            String _ppm = null;
            if (ppm_cacbon < 100) _ppm = "<100";
            else if (ppm_cacbon >= 100 && ppm_cacbon <= 500) _ppm = "100-500";
            else if (ppm_cacbon > 500 && ppm_cacbon <= 1000) _ppm = "500-1000";
            else _ppm = ">1000";
            return rbi.getSusCarbonate(_pH, _ppm);
        }
        //Step 4: Determine the severity index, Svi
        private int SVI_Carbonate()
        {
            int svi = 0;
            if (crackPresent)
                svi = 1000;
            else
            {
                switch (susceptibility())
                {
                    case "High": svi = 1000; break;
                    case "Medium": svi = 100; break;
                    case "Low": svi = 10; break;
                    default: svi = 1; break;
                }
            }
            return svi;
        }
        //Step 5: Determine the base damage factor for carbonate cracking, D_carbonate_fB
        private int D_carbonate_fB()
        {
            String field = noInsp + catalog_cacbon;
            return rbi.getD_f_scc(SVI_Carbonate(), field);
        }
        //Step 6: Calculate D_carbonate_f
        public double D_carbonate_f()
        {
            return D_carbonate_fB() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SSC Damage Factor PTA Cracking
        // B1: xac dinh muc do cracking HSC_HF su dung bang 14.3
        private String getsscp_pta()
        {
            String heatTreatment;
            if (opTemp < 427)
                heatTreatment = fHT + " (<427oC)";
            else
                heatTreatment = fHT + " (>=427oC)";

            return rbi.getSusceptibilityPTACracking(material, heatTreatment);
        }
        // B2: xac dinh SVI theo table 14.4
        private int SVI()
        {
            String sscp_pta = getsscp_pta();
            int svi;
            if (sscp_pta.Equals("High"))
                svi = 5000;
            else if (sscp_pta.Equals("Medium"))
                svi = 500;
            else if (sscp_pta.Equals("Low"))
                svi = 50;
            else
                svi = 1;
            return svi;
        }
        // B3: xac dinh D_fb_hsc tu bang 7.4
        private int D_fb_pta()
        {
            String field = noInsp + catalog_pta;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B4: tinh D_f_hsc theo cong thuc
        public double D_f_pta()
        {
            return D_fb_pta() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SSC Damage Factor CLSCC
        //B1: Xac dinh muc do bang bang 13.3
        private String CLSCC()
        {
            String temString = null;
            String phString = null;
            String field = null;

            if (ppm_clo >= 1 && ppm_clo <= 10)
                field = "1-10";
            else if (ppm_clo >= 11 && ppm_clo <= 100)
                field = "11-100";
            else if (ppm_clo >= 101 && ppm_clo <= 1000)
                field = "101-1000";
            else
                field = ">1000";


            if (pH <= 10)
            {
                phString = "<=10";
                if (Temperature >= 100 && Temperature <= 150)
                    temString = "100-150";
                else if (Temperature > 150 && Temperature <= 200)
                    temString = ">150-200";
                else
                    temString = ">200-300";
            }
            else
            {
                phString = ">10";
                if (Temperature < 200)
                {
                    temString = "<200";
                }
                else
                {
                    temString = "200-300";
                }
            }

            return rbi.getCLSCC(temString, phString, field);
        }

        // B2: xac dinh SVI
        private int SVI_CLSCC()
        {
            int svi;
            String level = CLSCC();

            if (crackPresent == true)
                svi = 5000;
            else
            {
                if (level.Equals("High"))
                    svi = 5000;
                else if (level.Equals("Medium"))
                    svi = 500;
                else if (level.Equals("Low"))
                    svi = 50;
                else
                    svi = 1;
            }
            return svi;
        }

        //B3: xac dinh D_fb_clscc bang table 7.4
        private int D_fb_clscc()
        {
            String field = noInsp + catalog_clscc;
            return rbi.getD_f_scc(SVI_CLSCC(), field);
        }
        // B4: tinh toan D_f_clscc
        public double D_f_clscc()
        {
            return D_fb_clscc() * Math.Pow(age, 1.1);
        }
        #endregion

        #region SCC DAMAGE FACTOR – HSC-HF
        // B1: xac dinh muc do cracking HSC_HF su dung bang 14.3
        private String getHSC_HF()
        {
            String hsc_hf = null;
            String field = null;

            if (BrinellHardness < 200)
                field = "<200";
            else if (BrinellHardness >= 200 && BrinellHardness <= 237)
                field = "200-237";
            else
                field = ">237";


            if (crackPresent)
                hsc_hf = "High";
            else
            {
                if (hfPresent)
                {
                    if (carbonateSteel)
                        hsc_hf = rbi.getHSC_HF(PWHT, field);
                    else
                        hsc_hf = "None";
                }
                else
                    hsc_hf = "None";
            }
            return hsc_hf;
        }
        // B2: xac dinh SVI theo table 14.4
        private int SVI()
        {
            String hsc_hf = getHSC_HF();
            int svi;
            if (hsc_hf.Equals("High"))
                svi = 100;
            else if (hsc_hf.Equals("Medium"))
                svi = 10;
            else
                svi = 1;
            return svi;
        }
        // B3: xac dinh D_fb_hsc tu bang 7.4
        private int D_fb_hsc()
        {
            String field = inspection + inspectionCatalog;
            return rbi.getD_f_scc(SVI(), field);
        }
        // B4: tinh D_f_hsc theo cong thuc
        public double D_f_hsc()
        {
            return D_fb_hsc() * Math.Pow(age, 1.1);
        }
        #endregion

    }
}
