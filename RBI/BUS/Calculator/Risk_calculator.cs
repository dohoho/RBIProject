using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class Risk_calculator
    {
        /// <summary>
        ///  xac dinh risk tu do suy ra inpection plan
        ///  ap dung co 1 so loai equipment: Pressure vessels
        ///                                  Heat exchangers
        ///                                  AirFin Heat Exchanger - Header Boxes
        ///                                  Pipes and Tubes
        /// </summary>
        // xac dinh loai equidment nao? moi loai eq co 1 cong thuc tinh plan rieng
        public String EquidmentType { set; get; }
        // xac dinh tan so loi cua thiet bi
        public double gff { set; get; }
        // xac dinh Df(t) : cai nay tinh tu cac cong thuc damage factor
        public double Df { set; get; }
        // xac dinh he so score: diem danh gia he thong quan ly
        public int Score { set; get; }
        // xac dinh FC: financial consequences
        public double FC { set; get; }
        // xac dinh CA: area consequences
        public double CA { set; get; }
        // xac dinh Risk target( CA(ft2) or FC($))
        public double RiskTarget { set; get; }

        /// <summary>
        /// tinh toan cac he so cua POF
        /// </summary>
        /// <returns></returns>
        // B1: tinh Fms
        private double getFms()
        {
            double pscore = Score * 100 / 1000;
            return Math.Pow(10, -0.02 * pscore + 1);
        }
        // B2: tinh POF( Failer/year)
        private double getPOF()
        {
            return gff * Df * getFms();
        }
        // B3: xac dinh Risk
        private double getRiskFC()
        {
            return getPOF() * FC;
        }
        private double getRiskCA()
        {
            return getPOF() * CA;
        }
        // B4 xac dinh inspection plan( gs Risk target: la so $ (FC))
        public int getInspection()
        {
            return (int)(RiskTarget / getRiskFC() + 0.5);
        }
    }
}
