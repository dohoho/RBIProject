using RBI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS.Calculator
{
    class damageFactor_PipingMechanicalFatigue
    {
        RBICalculatorConn rbi = new RBICalculatorConn();

        public String noPreFatFailure { set; get; }
        // noPreFatFailure : number of previous Fatigue Failures
        public String vibrationSVI { set; get; }
        //vibrationSVI : severity of vibration 
        public double noWeeks { set; get; }
        // noWeeks : number of weeks pipe has been shaking
        public String cyclicType { set; get; }
        //cyclicType : type of cyclic loading connected directly or indirectly within approximately 50 feet
        public String correctiveActions { set; get; }
        //correctiveActions : corrective actions take
        public double totalpipeFitting { set; get; }
        //totalpipeFitting : total pipe fitting
        public String jointType { set; get; }
        //jointType : type of joint in this piping
        public String pipeCondition { set; get; }
        //pipeCondition : condition of pipe
        public double branchDiameter { set; get; }
        //branchDiameter : branch diameter
        // Step 1 : Determine base damage factor for previous failures
        private double D_fB_pf()
        {
            double D_fB_pf = 0;
            if (noPreFatFailure.Equals("None"))
                D_fB_pf = 1;
            else if (noPreFatFailure.Equals("One"))
                D_fB_pf = 50;
            else if (noPreFatFailure.Equals("Greater than one"))
                D_fB_pf = 500;
            return D_fB_pf;
        }
        // Step 2 : Determine base damage factor for visible/ audible shaking
        private double D_fB_as()
        {
            double D_fB_as = 0;
            if (vibrationSVI.Equals("Minor"))
                D_fB_as = 1;
            else if (vibrationSVI.Equals("Moderate"))
                D_fB_as = 50;
            else if (vibrationSVI.Equals("Severe"))
                D_fB_as = 500;
            return D_fB_as;
        }
        // Step 3 : Determine adjustment factor for visible/ audible shaking
        private double F_fB_as()
        {
            double F_fB_as = 0;
            if (noWeeks < 2)
                F_fB_as = 1;
            else if ((noWeeks >= 2) & (noWeeks < 13))
                F_fB_as = 0.2;
            else if ((noWeeks >= 13) & (noWeeks < 52))
                F_fB_as = 0.02;
            return F_fB_as;
        }
        // Step 4 : Determine base damage factor for each type of cyclic
        private double D_fB_cf()
        {
            double D_fB_cf = 0;
            if (cyclicType.Equals("Reciprocating Machinery"))
                D_fB_cf = 50;
            else if (cyclicType.Equals("PRV Chatter"))
                D_fB_cf = 25;
            else if (cyclicType.Equals("Valve with high pressure drop"))
                D_fB_cf = 10;
            else if (cyclicType.Equals("None"))
                D_fB_cf = 1;
            return D_fB_cf;
        }
        // Step 5 : Determine base damage factor - mechanical fatigue
        public double D_fB_mfat()
        {
            double D_fB_mfat = 0;
            double temp = D_fB_as() * F_fB_as();
            if (D_fB_pf() < temp)
            {
                if (temp < D_fB_cf())
                    D_fB_mfat = D_fB_cf();
                else
                    D_fB_mfat = temp;
            }
            else
            {
                if (D_fB_pf() < D_fB_cf())
                    D_fB_mfat = D_fB_cf();
                else
                    D_fB_mfat = D_fB_pf();
            }
            return D_fB_mfat;
        }
        // Step 6 : adjustment for corrective action 
        public double F_ca()
        {
            double F_ca = 0;
            if (correctiveActions.Equals("Modification based on complete engineering analysis"))
                F_ca = 0.002;
            else if (correctiveActions.Equals("Modification based on experience"))
                F_ca = 0.2;
            else if (correctiveActions.Equals("No modification"))
                F_ca = 2;
            return F_ca;
        }
        // Step 6 : adjustment for pipe complexity
        public double F_pc()
        {
            double F_pc = 0;
            if ((totalpipeFitting >= 0) & (totalpipeFitting <= 5))
                F_pc = 0.5;
            else if ((totalpipeFitting >= 6) & (totalpipeFitting <= 10))
                F_pc = 1;
            else if (totalpipeFitting > 10)
                F_pc = 2;
            return F_pc;
        }
        // Step 6 : adjustment for piping condition 
        public double F_cp()
        {
            double F_cp = 0;
            if (pipeCondition.Equals("Missing or damaged supports, improper support"))
                F_cp = 2;
            else if (pipeCondition.Equals("Broken gussets, gussets welded directly to the pipe"))
                F_cp = 2;
            else if (pipeCondition.Equals("Good Condition"))
                F_cp = 1;
            return F_cp;
        }
        // Step 6 : adjustment for joint type or brach design
        public double F_jb()
        {
            double F_jb = 0;
            if (jointType.Equals("Threaded") || jointType.Equals("Socketweld") || jointType.Equals("Saddle on"))
                F_jb = 2;
            else if (jointType.Equals("Saddle in fitting"))
                F_jb = 1;
            else if (jointType.Equals("Piping tee") || jointType.Equals("Weldolets"))
                F_jb = 0.2;
            else if (jointType.Equals("Sweepolets"))
                F_jb = 0.02;
            return F_jb;
        }
        // Step 6 : adjustment for brach diameter
        public double F_bd()
        {
            double F_bd = 0;
            if (branchDiameter <= 2)
                F_bd = 1;
            else if (branchDiameter > 2)
                F_bd = 0.02;
            return F_bd;
        }
        // Step 6 : Determine final value of damage factor 
        public double D_f_mfat()
        {
            return D_fB_mfat() * F_ca() * F_pc() * F_cp() * F_jb() * F_bd();
        }

    }
}
