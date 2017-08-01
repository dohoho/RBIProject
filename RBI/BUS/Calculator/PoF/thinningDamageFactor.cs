using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class thinningDamageFactor
    {
        RBICalculatorConn rbi = new RBICalculatorConn();
        // xac dinh number of inspection
        public int inspection { set; get; }
        // xac dinh he so inspectionCatalog( table 5.5, 5.6)
        public String inspectionCatalog { set; get; }
        // xac dinh loai Component nao
        public String componentType { set; get; }
        // xac dinh thoi gian trong he thong ke tu lan doc thickness
        public int age { set; get; }
        // xac dinh do day doc duoc
        public double Trd { set; get; }
        // xac dinh xem thanh phan co lop vo hay khong
        public bool haveCladding { set; get; }
        // neu la loai tank thi co protection barrier khong?
        public bool protectionBarrier { set; get; }

        ///<summary>
        /// neu co lop vo thi xac dinh cac thanh phan cua lop vo
        ///</summary>
        // xac dinh toc do an mon cua lop vo
        public double Crcm { set; get; }
        // xac dinh do day cua lop kim loai 
        public double T { set; get; }
        
        ///<summary>
        /// neu khong co lop vo thi xac dinh cac thong so 
        ///</summary>
        // xac dinh toc do an mon cua lop kim loai
        public double Crbm { set; get; }
        // xac dinh do day toi thieu can thiet( dua tren ma thi cong)
        public double Tmin { set; get; }
        public double CA { set; get; }

        // xac dinh cac he so cuoi cung Fom, Fip, Fdl,Fwd,Fam,Fsm
        // Fom dua vao table 5.13 : Online Monitoring
        public int Fom { set; get; }
        // xac dinh he so Fip: 1 or 3
        public int Fip { set; get; }
        // xac dinh he so Fdl: 1 or 3
        public int Fdl { set; get; }
        // xac dinh he so Fwd: 1 or 10
        public int Fwd { set; get; }
        // xac dinh he so Fam: 1 or 5
        public int Fam { set; get; }
        // xac dinh he so Fsm: chi ton tai cho tank bottoms
        public int Fsm { set; get; }


        private double max(double a, double b)
        {
            if (a <= b) return b;
            else return a;
        }
        //B1: xac dinh lai he so Tmin
        private double getTmin()
        {
            double t;
            if (componentType.Equals("TANKBOTTOM"))
            {
                if (protectionBarrier)
                {
                    // inc
                    t = 0.125;
                }
                else
                {
                    // inc
                    t = 0.25;
                }
            }
            else
            {
                t = Tmin;
            }
            return t;
        }

        //B2: neu co lop vo thi tinh toan agerc
        private double agerc()
        {
            return max((Trd - T) / Crcm, 0);
        }
        //B3: tinh toan Art
        private double Art()
        {
            if (!haveCladding)
            {
                return max(1 - (Trd - Crbm * age) / (getTmin() + CA), 0);
            }
            else
            {
                double x = 1 - (Trd - Crcm * agerc() - Crbm * (age - agerc())) / (getTmin() + CA);
                return max(x, 0);
            }
        }
        // B4: chuan hoa art
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
                return rbi.D_fb_tank(getArt(), inspectionCatalog);
            }
            else
                return rbi.getDfb(getArt(), inspection, inspectionCatalog);
        }

        // B6: xac dinh ket qua cuoi cung tu B5 va cac gia tri
        public double D_f_thin()
        {
            if (componentType.Equals("TANKBOTOM"))
                return getD_fb_thin() * Fip * Fdl * Fwd * Fam * Fsm / Fom;
            else
                return getD_fb_thin() * Fip * Fdl * Fwd * Fam / Fom;
        } 
    }
}
