using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBI.DAL;
namespace RBI.BUS.Calculator
{
    class noneFlame_ToxicConsequences
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // componentType duoc su dung cho tra cuu gff
        public String componentType { set; get; }
        public double rate { set; get; }
        public double mass { set; get; }
        public double Ps { set; get; }
        public double Patm { set; get; }

        // neu la steam = true/ axit or cautics = false
        public bool steam { set; get; }
        // Chuan bi: xay dung cac ham chuc nang
        private double min(double a, double b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }
        private double getC(int a)
        {
            return double.Parse(rbi.getC(a));
        }
        private double getGff(int a)
        {
            return double.Parse(rbi.getGff(componentType, a));
        }
        private double getGffTotal()
        {
            return double.Parse(rbi.getGff(componentType));
        }
        // B1. tinh toan CA_cont_inj
        public double CA_cont_inj()
        {
            double ca = 0;
            double g = 2696 - 21.9 * getC(11) * (Ps - Patm) + 1.474 * Math.Pow((getC(11) * (Ps - Patm)), 2);
            double h = 0.31 - 0.00032 * Math.Pow((getC(11) * (Ps - Patm) - 40), 2);
            if (steam)
            {
                ca = getC(9) * rate;
            }
            else
            {
                ca = 0.2 * getC(8) * g * Math.Pow(getC(4) * rate, h);
            }
            return ca;
        }
        //B2. tinh toan CA_inst_inj
        public double CA_inst_inj()
        {
            double ca = 0;
            if (steam)
            {
                ca = getC(10) * Math.Pow(mass, 0.6384);
            }
            else
            {
                ca = 0;
            }
            return ca;
        }
        // B3: tinh toan Fact_ic
        public double Fact_ic()
        {
            double fact_ic = 0;
            if (steam)
            {
                fact_ic = min(rate / getC(5), 1);
            }
            else
            {
                fact_ic = 0;
            }
            return fact_ic;
        }
        // B4: tinh CA_leak_inj
        public double CA_leak()
        {
            return CA_inst_inj() * Fact_ic() + CA_cont_inj() * (1 - Fact_ic());
        }
        // B5: tinh toan CA_nfnt_inj
        public double CA_nfnt_inj()
        {
            double ca = 0;
            for (int i = 1; i < 5; i++)
            {
                ca += getGff(i) * CA_leak();
            }
            return ca / getGffTotal();
        }
    }
}
