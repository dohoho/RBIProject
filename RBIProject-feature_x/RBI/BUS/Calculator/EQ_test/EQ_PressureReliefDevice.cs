using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.EQ_test
{
    class EQ_PressureReliefDevice
    {
        RBICalculatorConn rbi = new RBICalculatorConn();

        /// <summary>
        ///  tinh toan xac suat loi khi mo van ap suat( PoF Open)
        /// </summary>

        // xac dinh khoang kiem tra tinsp ma xuat hien loi
        public int tinsp { set; get; }
        // xac dinh Fc
        public double Fc { set; get; }
        // xac dinh ap suat co the neu mo van k theo yeu cau( kPa)
        public double Po { set; get; }
        // xac dinh ap suat toi da cho phep de thiet bi duoc bao ve
        public double MAWP { set; get; }
        // xac dinh Fenv: yeu to dieu chinh moi truong tu bang 7.6
        public double Fenv { set; get; }
        // xac dinh kem kiem tra la pass hay fail
        public bool isPass { set; get; }

        // xac dinh Df(t) tai thoi gian t = tinsp
        public double Df { set; get; }
        //xac dinh Fms
        public double Fms { set; get; }
        //xac dinh xac suat loi cua he thong gff: dua tren tai lieu thiet ke he thong
        public double gff { set; get; }
        // xac dinh gfftotal
        public double gffTotal { set; get; }
        // xac dinh inspection efective
        public String inspEffective { set; get; }
        // xac dinh service( Fluid Severity) de tra bang 7.5
        public String service { set; get; }
        // xac dinh weibull demand
        public int weibull { set; get; }

        // xac dinh Dr_total
        public double DR { set; get; }


        //B: xac dinh CF
        private double get_CF(bool f)
        {
            return rbi.get_CF(inspEffective, f);
        }
        // B: xac dinh Fop();
        private double Fop()
        {
            return (1 / 3.375) * (Po / MAWP - 1.3);
        }
        // B: xac dinh cac he so n, b tu bang 7.4 va 7.5 example b = 1.8, n = 17.5
        public double n_mod()
        {
            double n = rbi.get_n(service, weibull);
            return n * Fc * Fenv * Fop();
        }
        // B: xac dinh Pf_prior: xac suat loi tai nam thu 6
        private double P_f_prior()
        {
            return 1 - Math.Exp(-Math.Pow(tinsp / n_mod(), 1.8));
        }
        // B: xac dinh xac suat van dap ung thoe yeu cau: P_p_prior
        private double P_p_prior()
        {
            return 1 - P_f_prior();
        }
        // B: tinh xac xuat loi trong dieu kien kiem tra P_f_cond
        private double P_f_cond()
        {
            if (isPass) return (1 - get_CF(true) * P_p_prior());
            else return get_CF(false) * P_f_prior() + (1 - get_CF(true) * P_p_prior());
        }
        // B: voi moi loai muc do danh gia ma co cong thuc tinh khac nhau( xem bang 7.9)
        public double P_f_wgt()
        {
            double p = 0;
            if (isPass)
            {
                p = P_f_prior() - 0.2 * P_f_prior() * (tinsp / n_mod()) + 0.2 * P_f_cond() * (tinsp / n_mod());
            }
            else
            {
                if (inspEffective.Equals("Highly Effective") || inspEffective.Equals("Usually Effective"))
                    p = P_f_cond();
                else
                    p = 0.5 * (P_f_prior() + P_f_cond());
            }
            return p;
        }
        // B: tu cong P_f_wgt() tinh ra n_upd
        public double n_upd()
        {
            return (tinsp / (Math.Pow(-Math.Log(1 - P_f_wgt()), 1 / 1.8)));
        }
        // B: tu n_upd xac dinh lai P_fod()
        public double P_fod()
        {
            return (1 - Math.Exp(-Math.Pow(tinsp / n_upd(), 1.8)));
        }
        // B: tinh toan Pf
        public double P_f()
        {
            return Df * gff * Fms + ((1 - gffTotal) / 3) * (Po / MAWP - 1);
        }

        // xac dinh P_prd_f: xac suat mo van bi loi
        public double P_prd_f()
        {
            return P_fod() * DR * P_f();
        }


        ///<summary>
        /// tinh toan xac suat ro ri( PoF Leakage)
        ///</summary>

        // xac dinh Fs: adjustment factor
        // Fs = 1.25 ( soft seated design) or 1.0( other cases)
        public double Fs { set; get; }
        // xac dinh co xuat hien su leakage hay k?
        public bool isLeakage { set; get; }
        // xac dinh muc do leakage( dua vao lich su)
        public String levelLeakage { set; get; }
        // xac dinh service Leakage
        public String service_leak { set; get; }
        // xac dinh kieu weibull_leak
        public String Weibull_leak { set; get; }


        // B1b: tinh toan nmod
        private double n_mod_l()
        {
            double n = rbi.get_n(service, weibull);
            return n * Fs * Fenv;
        }
        // B2b: tinh toan loi ro ri P_f_l() 
        // P_prd_f,prior cho truong hop leakeage
        public double P_f_l()
        {
            return 1 - Math.Exp(-Math.Pow(tinsp / n_mod_l(), 1.8));
        }

        // B3b: tinh toan P_p_l()
        // P_prd_p,prior cho truong hop leakage
        public double P_p_l()
        {
            return 1 - P_f_l();
        }

        // B4b: tinh toan P_fcond_l()
        // P_prd_f,cond cua su do ri.
        public double P_fcond_l()
        {
            if (isLeakage) return (get_CF(false) * P_f_l() + (1 - get_CF(true)) * P_p_l());
            else return (1 - get_CF(true)) * P_p_l();
        }
        // B5b: xac dinh P_l_wgt() dung bang 7.9
        public double P_l_wgt()
        {
            double p = 0;
            if (levelLeakage.Equals("Highly Effective Pass") || levelLeakage.Equals("Usually Effective Pass") || levelLeakage.Equals("Fairly Effective Pass"))
            {
                p = P_f_l() - 0.2 * P_f_l() * (tinsp / n_mod_l()) + 0.2 * P_fcond_l() * (tinsp / n_mod_l());
            }
            else
            {
                if (levelLeakage.Equals("Highly Effective Fail") || levelLeakage.Equals("Usually Effective Fail"))
                    p = P_fcond_l();
                else
                    p = 0.5 * P_f_l() + 0.5 * P_fcond_l();
            }
            return p;
        }

        // B6b: tinh toan n_upd_l()
        public double n_upd_l()
        {
            return (tinsp / (Math.Pow(-Math.Log(1 - P_l_wgt()), 1 / 1.8)));
        }
        // B7b: tinh PoF leakage P_prd_l()
        public double P_prd_l()
        {
            return (1 - Math.Exp(-Math.Pow(tinsp / n_upd_l(), 1.8)));
        }


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
        /// 
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
        private double D_mild()
        {
            return rbi.get_D_mild(inletSize, typePRDs);
        }
        //B3: xac dinh thoi gian ket mo van D_so
        public double D_so()
        {
            double so = (double)T_insolate / 1440;
            return so;
        }
        // B4: xac dinh Cost_mild
        private double Cost_mild()
        {
            return 24 * Fr * Cost_flu * D_mild() * lrate("Mild");
        }
        // B5: xac dinh Cost_so
        public double Cost_so()
        {
            return 24 * Fr * Cost_flu * D_so() * lrate("So");
        }
        // B6: tinh toan chi phai dung he thong de sua chua
        public double Cost_sd()
        {
            if (inletSize.Equals("Greater than 6"))
                return 2000;
            else
                return 1000;
        }
        // B7: xac dinh chi phi ton that dung san xuat
        private double Cost_mild_prod()
        {
            if (isTolerated)
                return 0;
            else
                return Unit_prod * D_sd;
        }
        public double Cost_so_prod()
        {
            return Unit_prod * D_sd;
        }

        // B8; xac dinh ton that
        public double Cost_mild_l()
        {
            return Cost_mild() + Cost_sd() + Cost_env + Cost_mild_prod();
        }
        public double Cost_so_l()
        {
            return Cost_so() + Cost_sd() + Cost_env + Cost_so_prod();
        }
        // B9: xac dinh final consequence
        public double Cost_l()
        {
            return 0.9 * Cost_mild_l() + 0.1 * Cost_so_l();
        }
    }
}
