using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class DfCalculator
    {
        public String componentType { set; get; }
        public double calculatorDate {set;get;}
        //ageTK: khoang thoi gian giua ngay kiem tra gan nhat
        public double ageTK { set; get; }
        public double Trd { set; get; }
        public double T { set; get; }
        public double Tmin { set; get; }
        public double InstallDate { set; get; }
        public double Date { set; get; }
        public double CrB { set; get; }
        public int InspEff { set; get; }
        public int numOfInsp { set; get; }
        public int Fps { set; get; }
        public int Fip { set; get; }
        public double CA { set; get; }
        public String levelInsp { set; get; }
        // select : co lop phu hoac k co lop phu
        public bool select { set; get; }
        public double Crcm { set; get; }
        public double Crbm { set; get; }
        public int Fom { set; get; }
        public int Fdl { set; get; }
        public int Fwd { set; get; }
        public int Fam { set; get; }
        public int Fsm { set; get; }

        
        public bool thinningDamage { set; get; }
        /// <summary>
        ///  set get
        /// </summary>
        
        private double min(double a, double b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }
        private double max(double a, double b)
        {
            if (a >= b)
                return a;
            else
                return b;
        }

        public double ageCoat()
        {
            double age = calculatorDate - Date;
            return max(age, 0.0);
        }
        private double age()
        {
            return min(ageCoat(), this.ageTK);
        }
        public double Cr()
        {
            double cr = CrB * max(Fps, Fip);
            return cr;
        }
        public double Artdex()
        {
            double art = (1 - (Trd - Cr() * age()) / (Tmin + CA));
            return max(art, 0.0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double ageRC()
        {
            double age = (Trd - T) / (Crcm);
            return max(age, 0.0);
        }
        public double Artdf()
        {
            double art = 0;
            if (select)
            {
                art = (1 - (Trd - Crcm * ageRC() - Crbm * (ageTK - ageRC())) / (Tmin + CA));
            }
            else
            {
                art = 1 - (Trd - Crbm * ageTK) / (Tmin + CA);
            }
            return max(art, 0.0);
        }
        public double getDfb()
        {
            RBICalculatorConn rbi = new RBICalculatorConn();
            double art = Artdf();
            String ART = "";
            if (art > 0)
            {
                ART = rbi.getmaxArt(art.ToString());
            }
            else
            {
                ART = "0.02";
            }
            int getDfb = rbi.getDfb(double.Parse(ART), numOfInsp, levelInsp, componentType);
            return getDfb;
            //return art.ToString();
        }

        ///<summary>
        ///</summary>
        public double Df()
        {
            double df = (getDfb() * Fip * Fdl * this.Fwd * this.Fam * this.Fsm) / (Fom);
            return df;
            //return 0; 
        }
        public string getDex()
        {
            RBICalculatorConn rbi = new RBICalculatorConn();
            double art = Artdex();
            String ART = rbi.getmaxArt(art.ToString());
            int getDfb = rbi.getDfb(double.Parse(ART), numOfInsp, levelInsp, componentType);
            string result = getDfb.ToString();
            return result;
        }
        public double getDF()
        {
            if (thinningDamage)
                return double.Parse(getDex()) + Df();
            else
                return max(double.Parse(getDex()), Df());
        }
        //dua vao table 4.1
        public String changeDF()
        {
            double df = getDF();
            if (df <= 2)
                return "A";
            else if (df > 2 && df <= 20)
                return "B";
            else if (df > 20 && df <= 100)
                return "C";
            else if (df > 100 && df <= 1000)
                return "D";
            else return "E";
        }
        
    }
}
