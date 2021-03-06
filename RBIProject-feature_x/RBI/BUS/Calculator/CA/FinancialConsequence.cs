﻿using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace RBI.BUS.Calculator
{
    class FinancialConsequence
    {
        RBICalculatorConn rbiconn = new RBICalculatorConn();
        public String Materials { set; get; } //Material of Table 5.16
       
        public String componentType { set; get; }
        public double equipCost { set; get; }   //equipCost: chi phi $/m2
        public double prodCost { set; get; } //prodCost: production cost ($/day)
        public double CAinj { set; get; }
        public String Fluid { set; get; }
        public double propdens { set; get; } //popdens : population density( personnel/m2)
        public double injcost { set; get; }
        public double mass { set; get; }
        public double envcost { set; get; }
        public double CAcmd { set; get; } //tam thoi gan bang CAfalamemable
        private double getC(int a)
        {
            return double.Parse(rbiconn.getC(a));
        }
        //Step 1: Calculate the cost (consequence in $) to repair the specific piece of equipment, FCcmd
        public double FCcmd()
        {
            double product = 0;
            for (int i = 1; i < 5; i++)
            {
                product += double.Parse(rbiconn.getGff(componentType, i)) * rbiconn.getHolecost(componentType, i);
            }
            return (product * rbiconn.getMatcost(Materials)) / double.Parse(rbiconn.getGff(componentType));
        }
        //Step 2: Calculate the cost of damage to surrounding equipment in the affected area, FCaffa
        public double FCaffa()
        {
            double fc_affa = CAcmd * equipCost;
            return fc_affa;
        }
        //Step 3: calculate the cost of business interruption
        public double Outage_mult() //gan tam bang 1 gia tri
        {
            double a = 0;
            for (int i = 1; i < 5; i++)
            {
                a += rbiconn.getOutage(componentType, i);
            }
            return a;
        }
        public double Outage_cmd()
        {
            double product = 0;
            for (int i = 1; i < 5; i++)
            {
                product += double.Parse(rbiconn.getGff(componentType, i)) * rbiconn.getOutage(componentType, i);
            }
            return (product * Outage_mult()) / double.Parse(rbiconn.getGff(componentType));
        }
        private double Outage_affa()
        {
            double log = Math.Log10(Math.Abs(FCaffa() * Math.Pow(10, -6)));
            double outage_affa = Math.Pow(10, 1.242 + 0.585 * log);
            return outage_affa;
        }
        public double FCprod() // [$/day]
        {
            return (Outage_cmd() + Outage_affa()) * prodCost;
        }
        //Step 4: Calculate the costs associated with personnel injury 
        public double FCinj()
        {
            return CAinj * propdens * injcost;
        }
        //Step 5: Calculate the costs associated with environmental cleanup, FCenviron
        private double Frac_evap()
        {
            double NBP = rbiconn.getNBP(Fluid);
            double a = -7.1408 + 8.5823 * Math.Pow(10, -3) * getC(12) * NBP - 3.5594 * Math.Pow(10, -6) * Math.Pow(getC(12) * NBP, 2) + 2331.1 / (getC(12) * NBP) - 203545 / (Math.Pow(getC(12) * NBP, 2));
            return a;
        }
        public double Vol()    //tinh vol_n theo mass_n
        {
            double Pl = rbiconn.getPl(Fluid); //Liquid Density in table 5.12
            double a = getC(13) * mass * (1 - Frac_evap()) / Pl;
            return a;
        }
        public double FCenviron()
        {
            double product = 0;
            for (int i = 1; i < 5; i++)
            {
                product += double.Parse(rbiconn.getGff(componentType, i)) * Vol();
            }
            return (product * envcost) / double.Parse(rbiconn.getGff(componentType));
        }
        //Step 6: Calculate the total financial consequence 
        public double FC()
        {
            return FCcmd() + FCaffa() + FCenviron() + FCinj() + FCprod();
        }
        
    }
}
