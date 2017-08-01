using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class cuiDamageFactor_Ferritic
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // xac dinh componentType( de lua chon xem tra cuu bang nao)
        public String componentType { set; get; }
        // xac dinh nam tinh toan calculatorDate
        public int calculatorDate { set; get; }
        // xac dinh number of inspection
        public int inspection { set; get; }
        // xac dinh inspection catalog
        public String inspectionCatalog { set; get; }
        // xac dinh thoi gian trong he thong ke tu lan kiem tra do day cuoi cung ageTK
        public int ageTK { set; get; }
        // xac dinh chat luong vo
        public String coatQuality { set; get; }
        // xac dinh thoi gian lam vo
        public int coatInstall { set; get; }
        // xac dinh do day vo trong lan kiem tra cuoi cung
        public double trd { set; get; }
        // xac dinh do mong toi da cho phep
        public double tmin { set; get; }
        // xac dinh nhiet do hoat dong( *F)
        public int temperature { set; get; }
        // xac dinh trinh dieu khien an mon
        public String driver { set; get; }
        // xac dinh chuyen gia co xac dinh he so an mon hay k?
        public bool expertCR { set; get; }
        // neu chuyen gia xac dinh he so thi he so do bang bao nhieu
        public double CR { set; get; }
        // xac dinh he so bu an mon CA
        public double CA { set; get; }
        // xac dinh he so phuc tap( 3 muc: trung binh(0), tren trungbinh(1), duoi trung binh(-1))
        public int complexity { set; get; }
        // xac dinh kha nang cach nhiet( 3 muc: trung binh(0), tren trungbinh(1), duoi trung binh(-1))
        public int insulation { set; get; }
        // xac dinh duong ong co cho phep bao tri hay khong? khong cho phep =2, nguoc lai =1
        public bool allowConfig { set; get; }
        // dat duong ong trong dat hoac trong nuoc hay k? co = 2, nguoc lai = 1
        public bool enterSoil { set; get; }
        // xac dinh insulationType
        public String isulationType { set; get; }

        private double min(double a, double b)
        {
            if (a <= b) return a;
            else return b;
        }
        private double max(double a, double b)
        {
            if (a >= b) return a;
            else return b;
        }
        //B1: xac dinh toc do an mon Crb dua vao bang 17.3 ket qua tra ve la mpy( mili inc per year) = 0.0254 mm/year
        private double Crb()
        {
            double crb = 0;
            if (expertCR)
            {
                crb = CR;
            }
            else
            {
                crb = rbi.getCrb_CUI(temperature, driver);
            }
            return crb;
        }
        // B2: xac dinh ageCoat
        private double ageCoat()
        {
            int date;
            if (coatQuality.Equals("Medium"))
                date = coatInstall + 5;
            else if (coatQuality.Equals("High"))
                date = coatInstall + 15;
            else
                date = coatInstall;
            return max(0, calculatorDate - date);
        }
        // B3: xac dinh age
        private double age()
        {
            return min(ageTK, ageCoat());
        }
        // B4: tinh Cr
        private double Cr()
        {
            double Fcm;
            double Fic;
            double Fps;
            double Fip;
            double Fins = rbi.getFins(isulationType);

            if (complexity == -1)
                Fcm = 0.75;
            else if (complexity == 0)
                Fcm = 1;
            else
                Fcm = 1.25;

            if (insulation == -1)
                Fic = 1.25;
            else if (insulation == 0)
                Fic = 1;
            else Fic = 0.75;

            if (allowConfig)
                Fps = 1;
            else Fps = 2;

            if (enterSoil)
                Fip = 2;
            else
                Fip = 1;

            return Crb() * Fcm * Fic * Fins * max(Fip, Fps);
        }

        // B5: tinh Art()
        private double Art()
        {
            double art = 1 - (trd - Cr() * age()) / (tmin + CA);
            return max(art, 0);
        }
        // B6: xac dinh D_f_cui tu bang 5.11 hoac 5.12
        public double D_f_cui()
        {
            if (componentType.Equals("TANKBOTTOM"))
                return rbi.D_fb_tank(Art() + "", inspection + "", inspectionCatalog);
            else return rbi.getDfb(Art() + "", inspection + "", inspectionCatalog);
        }
    }
}
