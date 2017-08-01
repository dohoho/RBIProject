using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.EQ_test
{
    /// <summary>
    ///  de don gian viec tinh toan ta gia thiet:
    ///  1. cac thiet bi duoc kiem tra se la lan dau
    ///  2. cac thiet bi deu dung du lieu chuan ham weibull
    ///  3. 
    /// </summary>
    class EQ_HeatExchangerTubeBundles
    {
        RBICalculatorConn rbi = new RBICalculatorConn();

        // xac dinh Unit_prod: gia tri san xuat( $/day)
        public double Unit_prod { set; get; }
        // xac dinh ty le loi bo
        public double Rate_red { set; get; }
        // xac dinh so ngay phai dung he thong de sua( khong theo ke hoach: day)
        public double D_sd { set; get; }
        // xac dinh duong kinh trong cua ong 
        public double D_shell { set; get; }
        // xac dinh chieu dai cua ong( ft)
        public double L_tube { set; get; }
        // xac dinh tube Material Cost Factor: Mf( bang 8.3)
        public double Mf { set; get; }
        // xac dinh environment cost:
        public double Cost_env { set; get; }
        // xac dinh maintenance Cost with bundle replacement: chi phi bao tri lien quan den thay the cac bo( $)
        public double Cost_maint { set; get; }
        // xac dinh thoi gian trong he thong( year)
        public double T { set; get; }
        // xac dinh Risk Target( tube)
        public double Risktarget_tube { set; get; }

        // B1:xac dinh Cost_prod
        private double Cost_prod()
        {
            return Unit_prod * (Rate_red / 100) * D_sd;
        }
        // B2: xac dinh Cost_bundle
        private double Cost_bundle()
        {
            return 22000 * (Math.PI * D_shell * D_shell / 4) * L_tube * Mf / double.Parse(rbi.getC(1));
        }
        // B3: xac dinh gia tri Cost_total
        public double Cost_total()
        {
            return Cost_prod() + Cost_env + Cost_maint + Cost_bundle();
        }
        // B4: xac dinh PoF tube
        /// <summary>
        ///  o day chung ta tinh toan cho truong hop lan dau kiem tra va gia su cac ong truyen nhiet nay duoc thay the
        ///  thay vi bao tri
        /// </summary>
        /// <returns></returns>
        public double P_f()
        {
            return (1 - Math.Exp(-Math.Pow(T / 20.45, 2.568)));
        }
        //B5: xac dinh current risk
        ///<summary>
        /// Risk_tube current = P_f()*Cost_total();
        ///</summary>
        public double Risk_tube_current()
        {
            return P_f() * Cost_total();
        }
        // B6: tinh toan thoi gian trong he thong.
        public double Tinsv()
        {
            double Pmax = Risktarget_tube / (Cost_total());
            return 20.45 * Math.Pow((-Math.Log(1 - Pmax)), 1 / 2.568);
        }
        // B7 xac dinh thoi gian thuc hien insp
        public double Tinsp()
        {
            return (int)(Tinsv() - T);
        }
    }
}
