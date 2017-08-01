using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace RBI.BUS.Calculator
{
    class flammableConsequences
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        BusEquipmentTemp busTemp = new BusEquipmentTemp();
        //get set

        public String Fluid { set; get; }
        public String componentType { set; get; }
        // type : Gas_Liquid
        public String type { set; get; }
        // Ps:
        public double Ps { get; set; }
        //Patm :is the atmospheric pressure, kPa [psia]
        public double Patm { set; get; }
        //Re:Reynold's Number
        public double re { set; get; }
        //Pl: p liquid
        public double pl { get; set; }
        // Ts : nhietdo hoat dong( *K) dung trong cong thuc ting Cp
        public double Ts { set; get; }
        //mass :khoi luong (Liquip,: kg )
        //public double mass { set; get; }
        //state:trang thai (hoi, long)
        public double mass { set; get; }
        public int state { set; get; }
        /// <summary>
        /// 
        /// </summary>
        //set get calculator factDi
        public int detection { set; get; }
        public int isolation { set; get; }
        
        public int holnumber { set; get; }

        // setup fact( fact mit table 5.10);
        public int mitigationSystem { set; get; }

        public double fact()
        {
            double ft = 0;
            if (mitigationSystem == 1) ft = 0.25;
            else if (mitigationSystem == 2) ft = 0.2;
            else if (mitigationSystem == 3) ft = 0.05;
            else ft = 0.15;
            return ft;
        }

        public double factDi()
        {
            double fDi = 0;
            if (detection == 1 && isolation == 1)
            {
                fDi = 0.25;
            }
            else if (detection == 1 && isolation == 2)
            {
                fDi = 0.2;
            }
            else if ((detection == 1 || detection == 2) && isolation == 3)
            {
                fDi = 0.1;
            }
            else if (detection == 2 && isolation == 2)
            {
                fDi = 0.15;
            }
            else fDi = 0;
            return fDi;
        }
        
        // rate using factdi
        public double rate()
        {
            double rt = Wn() * (1 - factDi());
            return rt;
        }
        //getCP : tra bang 5.2
        public double Cp()
        {
            int ideal = rbi.getCp_ideal(Fluid);
            double cp = 0;
            double A = rbi.getCp(Fluid, "A");
            double B = rbi.getCp(Fluid, "B");
            double C = rbi.getCp(Fluid, "C");
            double D = rbi.getCp(Fluid, "D");
            double E = rbi.getCp(Fluid, "E");
            double CP_C2 = (C / Ts) / (Math.Sinh(C / Ts));
            double CP_E2 = (E / Ts) / (Math.Cosh(E / Ts));
            if(ideal == 1)
            {
                cp = A + B * Ts + C * Ts * Ts + D * Ts * Ts * Ts;
            }
            else if(ideal == 2)
            {
                cp = A + B * CP_C2 * CP_C2 + D * CP_E2 * CP_E2;
            }
            else if(ideal == 3)
            {
                cp = A + B * Ts + C * Math.Pow(Ts, 2) + D * Math.Pow(Ts, 3) + E * Math.Pow(Ts, 4);
            }
            else
            {
                cp = 0;
            }
            return cp;
        }
        //get MW: tra bang
        public double MW()
        {
            double mw = double.Parse(rbi.getMw(Fluid));
            return mw;
        }

        // dn (in)
        public double Wn()
        {
            double W = 0;
            double dn = 0;
            if (holnumber == 1)
            {
                dn = 0.25;
            }
            else if (holnumber == 2) dn = 1;
            else if (holnumber == 3) dn = 4;
            else dn = 16;
            double A = 3.14 * dn * dn / 4;
            if (type=="Liquid" || type=="liquid")
            {
                double fractal = 0.9935 + 2.878 / Math.Pow(20, 0.5) + 342.75 / Math.Pow(20, 1.5);
                double Kvn = Math.Pow(fractal, -1);
                W = 0.61 * Kvn * pl * A * Math.Sqrt(2 * 6.67 * Math.Exp(-11) * Math.Abs(Ps - Patm) / pl) / getC(1);
                //Debug.WriteLine("W in Wn = " + W);
            }
            else
            {
                double mw = MW(); // tra bang 
                double k = (Cp() / (Cp() - 8.314)); // R = 8.314
                double Ptrans = Patm * Math.Pow((k + 1) / 2, k / (k - 1));
                if(Ps > Ptrans)
                {
                    double x = (k * mw * 6.67 * Math.Exp(-11) / (8.314 * Ts))*Math.Pow(2/(k+1),(k+1)/(k-1));
                    W = 0.9 * A * Ps * Math.Sqrt(x) / getC(2);
                }
                else
                {
                    double x = (mw * 6.67 * Math.Exp(-11) / (8.314 * Ts)) * ((2 * k) / (k - 1)) * Math.Pow(Patm / Ps, 2 / k) * (1 - Math.Pow(Patm / Ps, (k - 1) / k));
                    W = 0.9 * A * Ps * Math.Sqrt(x) / getC(2);
                }
            }
            return W;
        }
        public double getAIT()
        {
            double a = double.Parse(rbi.getAIT(Fluid));
            return a;
        }
        public double factIC()
        {
            if (state == 1)
                return min(rate() / getC(5), 1.0);
            else
                return 1.0;
        }
        public double factAIT( double AIT)
        {
            if ((Ts + getC(6) )<= AIT)
                return 0;
            else if ((Ts - getC(6)) >= AIT)
                return 1;
            else
                return (Ts - AIT + getC(6)) / (2 * getC(6));
        }
        public double gff(int a)
        {
            double gff = double.Parse(rbi.getGff(componentType, a));
            return gff;
        }
        public double getgffTotal()
        {
            double gffTotal = double.Parse(rbi.getGff(componentType));
            return gffTotal;
        }
        public double eneff()
        {
            double enf = 4 * Math.Log10(getC(4) * mass) - 15;
            return enf;
        }
        private double aCont(int select)
        {
            String cmd = rbi.getcmd(Fluid, select, type, "a");
            double a = 0;
            try
            {
                a = double.Parse(cmd);
            }
            catch
            {
                a = 0;
            }
            return a;
        }
        private double aInj(int select)
        {
            String inj = rbi.getinj(Fluid, select, type, "a");
            double a = 0;
            try
            {
                a = double.Parse(inj);
            }
            catch
            {
                a = 0;
            }
            return a;
        }
        private double bCont (int select)
        {
            String cmd = rbi.getcmd(Fluid, select, type, "b");
            double b;
            try
            {
                b = double.Parse(cmd);
            }
            catch
            {
                b = 0;
            }
            return b;
        }
        private double bInj(int select)
        {
            String inj = rbi.getinj(Fluid, select, type, "b");
            double b = 0;
            try
            {
                b = double.Parse(inj);
            }
            catch
            {
                b = 0;
            }
            return b;
        }
        private double min(double a, double b)
        {
            if (a <= b)
                return a;
            else
                return b;
        }
        
        // get C
        private double getC(int n)
        {
            double c = double.Parse(rbi.getC(n));
            return c;
        }

        // 8.4 8.5
        private double CACont (int select)
        {
            double ca;
            int a = 0;
            if (type == "liquid")
            {
                ca = min(aCont(select) * Math.Pow(rate(), bCont(select)), getC(7)) * (1 - fact());
            }
            else
            {
                ca = aCont(select) * Math.Pow(rate(), bCont(select)) * (1 - fact());
            }
            return ca;
        }
        private double effrate(int select)
        {
            double enf;
            if(type == "liquid")
            {
                enf = (1 / getC(4)) * Math.Exp(Math.Log10(CACont(select) / (aCont(1) * getC(8))) * Math.Pow(bCont(1), -1));
            }
            else
            {
                enf = rate();
            }
            return enf;
        }

        //8.6 8.7
        private double CAInst(int select)
        {
            double ca;
            int a = 0;
            double rate = 0;
            if (type == "liquid")
            {
                ca = min(aCont(select) * Math.Pow(mass, bCont(select)), getC(7)) * ((1 - fact())/eneff());
            }
            else
            {
                ca = aCont(select) * Math.Pow(rate, bCont(select)) * (1 - fact());
            }
            return ca;
        }
        private double effmass(int select)
        {
            double enf = 0;
            if (type == "liquid")
            {
                enf = (1 / getC(4)) * Math.Exp(Math.Log10(CAInst(select) / (getC(8) * aCont(select))*(1/bCont(select))));
            }
            else
            {
                enf = mass;
            }
            return enf;
        }

        // 8.9 8.8
        private double CAInjCont(int select)
        {
            int a = 0;
            double ca = 0;
            ca = aInj(select) * Math.Pow(effrate(select), bInj(select)) * (1 - fact());
            return ca;
        }
        //8.10 8.11
        private double CAInjInst(int select)
        {
            int a = 0;
            int b = 0;
            double ca = 0;
            ca = aInj(select) * Math.Pow(effrate(select), bInj(select)) * ((1 - fact()) / eneff());
            return ca;
        }
        // 8.14
        private double CACmd()
        {
            double CAAIL = CACont(2) * factIC() + CAInst(4) * (1 - factIC());
            double CAAINL = CACont(1) * factIC() + CAInst(3) * (1 - factIC()); 
            return CAAIL * factAIT(getAIT()) + CAAINL * (1 - factAIT(getAIT()));
        }
        private double CAInj()
        {
            double CAAINinj = CAInjCont(2) * factIC() + CAInjInst(4) * (1 - factIC());
            double CAAINLinj = CAInjCont(1) * factIC() + CAInjInst(3) * (1 - factIC());
            return CAAINinj * factAIT(getAIT()) + CAAINLinj * (1 - factAIT(getAIT()));
        }

        public double CA()
        {
            double t = 0;
            for(int i = 1; i< 5; i++)
            {
                t += gff(i) * CACmd();
                //t += gff(i);
            }
            double CAcmd = t / getgffTotal();
            double v = 0;
            for (int i = 1; i < 5; i++)
            {
               v += gff(i) * CAInj();
                //v += gff(i);
            }
            double CAinj = v * CAInj() / getgffTotal();
            return CAcmd + CAinj;
        }
        public double CA_inj()
        {
            double v = 0;
            for (int i = 1; i < 5; i++)
            {
                v += gff(i) * CAInj();
                //v += gff(i);
            }
            double CAinj = v * CAInj() / getgffTotal();
            return CAinj;

        }
       public double CA_cmd()
        {
            double t = 0;
            for (int i = 1; i < 5; i++)
            {
                t += gff(i) * CACmd();
                //t += gff(i);
            }
            double CAcmd = t / getgffTotal();
            return CAcmd;
        }
        public int convertCatalog()
        {
            double ca = CA();
            if (ca <= 100)
                return 1;
            else if (ca > 100 && ca <= 1000)
                return 2;
            else if (ca > 1000 && ca <= 3000)
                return 3;
            else if (ca > 3000 && ca <= 10000)
                return 4;
            else return 5;
        }
    }
}
