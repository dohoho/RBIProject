using DevComponents.DotNetBar;
using RBI.BUS;
using RBI.Object;
using RBI.BUS.BUSExcel;
using RBI.PRE.subForm;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RBI.PRE.TAB;
using RBI.BUS.Calculator;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using System.IO;
using RBI.DAL;
using System.Diagnostics;
using RBI.BUS.Calculator;
namespace RBI
{
    public partial class Form1 : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void addNewTab(string strTabName, UserControl ucContent)
        {
            //-----------If exist tabpage then exit---------------
            foreach (TabItem tabPage in tabControl1.Tabs)
                if (tabPage.Text == strTabName)
                {
                    tabControl1.SelectedTab = tabPage;
                    return;
                }
            TabControlPanel newTabPanel = new DevComponents.DotNetBar.TabControlPanel();
            TabItem newTabPage = new TabItem(this.components);
            newTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            newTabPanel.Location = new System.Drawing.Point(0, 26);
            newTabPanel.Name = "panel" + strTabName;
            newTabPanel.Padding = new System.Windows.Forms.Padding(1);
            newTabPanel.Size = new System.Drawing.Size(1230, 384);
            newTabPanel.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            newTabPanel.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            newTabPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            newTabPanel.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            newTabPanel.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            newTabPanel.Style.GradientAngle = 90;
            newTabPanel.TabIndex = 2;
            newTabPanel.TabItem = newTabPage;
            //-------------New  tab page---------------------
            Random ran = new Random();
            newTabPage.Name = strTabName + ran.Next(100000) + ran.Next(22342);
            newTabPage.AttachedControl = newTabPanel;
            newTabPage.Text = strTabName;
            ucContent.Dock = DockStyle.Fill;
            newTabPanel.Controls.Add(ucContent);

            //------------add Tab page to Tab control-------------
            tabControl1.Controls.Add(newTabPanel);
            tabControl1.Tabs.Add(newTabPage);
            tabControl1.SelectedTab = newTabPage;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PRE.ITEM.jtree jtree = new PRE.ITEM.jtree();
            jtree.fillTree(treeView1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void explorerBar1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            newEquipment eq = new newEquipment();
            eq.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newEquipment eq = new newEquipment();
            eq.ShowDialog(this);
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            newComponent nc = new newComponent();
            nc.ShowDialog(this);
        }

        private void buttonItem19_Click(object sender, EventArgs e)
        {
            UCEquipmentInfomation ucEq = new UCEquipmentInfomation();
            addNewTab("Equipment Infomation", ucEq);
        }

        private void tabControl1_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            // lay ra Tab da chon
            TabItem chooseTab = tabControl1.SelectedTab;
            // xoa bo tab
            tabControl1.Tabs.Remove(chooseTab);
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {
            UCDamage ucDm = new UCDamage();
            addNewTab("Damage Mechanism", ucDm);
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            UCInspHistory ucHis = new UCInspHistory();
            addNewTab("Inspection History", ucHis);
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            UCRevision ucRe = new UCRevision();
            addNewTab("Equipment Revision", ucRe);
        }

        private void buttonItem23_Click(object sender, EventArgs e)
        {
            UCRisksummery ucRisk = new UCRisksummery();
            addNewTab("Risk Summary", ucRisk);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewEqRBI_Click(object sender, EventArgs e)
        {
            NewEquipmentForRBI eqRBI = new NewEquipmentForRBI();
            eqRBI.ShowDialog(this);
        }

        private void btnNewCom_Click(object sender, EventArgs e)
        {
            newComponent nCom = new newComponent();
            nCom.ShowDialog(this);
        }

        private void btnNewEq_Click(object sender, EventArgs e)
        {
            newEquipment nEq = new newEquipment();
            nEq.ShowDialog(this);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            UCEquipmentTemp uceqTem = new UCEquipmentTemp();
            addNewTab("Plant List", uceqTem);
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            UCComponent ucCom = new UCComponent();
            addNewTab("Component List", ucCom);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            newEquipment eq = new newEquipment();
            eq.ShowDialog(this);
        }

        private void btnComponent_Click(object sender, EventArgs e)
        {
            newComponent com = new newComponent();
            com.ShowDialog(this);
        }

        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            NewEquipmentForRBI eq = new NewEquipmentForRBI();
            eq.ShowDialog(this);
        }

        private void btnNewPlant_Click(object sender, EventArgs e)
        {
            NewPlant plant = new NewPlant();
            plant.ShowDialog(this);
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            subHomeUsr home = new subHomeUsr();
            addNewTab("Home", home);
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            UCEquipmentsList eq = new UCEquipmentsList();
            addNewTab("Equipments List", eq);
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            UCEquipmentForRBI eq = new UCEquipmentForRBI();
            addNewTab("Equipments For RBI", eq);
        }

        private void btnImportEq_Click(object sender, EventArgs e)
        {
            // khai bao cac business
            BusEquipmentListExcel eqExc = new BusEquipmentListExcel();
            BusEquipmentForRBIExcel eqRBIExc = new BusEquipmentForRBIExcel();
            BusEquipForRBI eqRBI = new BusEquipForRBI();
            BusEquipments eq = new BusEquipments();

            // khai bao cac list
            List<EquipmentForRbi> listEqRBI = new List<EquipmentForRbi>();
            List<Equipment> listEq = new List<Equipment>();

            /* OpenFileDialog op = new OpenFileDialog();
             op.Filter = "Excel 97-2003 File (*.xls)|*.xls";
             String path;
             if (op.ShowDialog() == DialogResult.OK)
             {
                 path = op.FileName;

                 listEqRBI = eqRBIExc.getListEQForRBI(path);
                 listEq = eqExc.getListAllEquipment(path);
             }
             */

            String path = @"D:\RBI\Equipment_List sample.xls";
            //String path = @"C:\Users\VuNA\Downloads\RBI\Equipment_List sample.xls";
            if (File.Exists(path))
            {
                listEqRBI = eqRBIExc.getListEQForRBI(path);
                listEq = eqExc.getListAllEquipment(path);
            }
            else
            {
                MessageBox.Show("File chưa tồn tại hoặc đặt tên sai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (listEqRBI.Count > 0 || listEq.Count > 0)
            {
                for (int i = 0; i < listEqRBI.Count; i++)
                {
                    if (!eqRBI.checkExist(listEqRBI[i]))
                    {
                        eqRBI.add(listEqRBI[i]);
                    }
                }

                WatingForm wt = new WatingForm();
                wt.ShowDialog(this);

                for (int i = 0; i < listEq.Count; i++)
                {
                    if (!eq.checkExist(listEq[i]))
                    {
                        eq.add(listEq[i]);
                    }
                }
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            RBIQuestion rbiQues = new RBIQuestion();
            rbiQues.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // khai bao business
            BusEquipmentTempExcel eqEx = new BusEquipmentTempExcel();
            BusComponents busCo = new BusComponents();
            BusComponentExcel busCoex = new BusComponentExcel();
            BusEquipmentTemp busEQ = new BusEquipmentTemp();
            BusEquipments eq = new BusEquipments();

            // khai bao list
            List<EquipmentTemp> list = new List<EquipmentTemp>();
            List<Object.Component> liscom = new List<Object.Component>();
            List<Equipment> listeq = new List<Equipment>();

            /*OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel 97-2003 File (*.xls)|*.xls";
            String path;
            if (op.ShowDialog() == DialogResult.OK)
            {
                path = op.FileName;
                liscom = busCoex.getListCom(path);
                list = eqEx.getListEQTemp(path);
            }
            */

            String path = @"D:\RBI\Equipment template.xls";
            //String path = @"C:\Users\VuNA\Downloads\RBI\Equipment template.xls";
            if (File.Exists(path))
            {
                liscom = busCoex.getListCom(path);
                list = eqEx.getListEQTemp(path);
                listeq = eqEx.getListEquipment(path);
            }
            else
            {
                MessageBox.Show("File chưa tồn tại hoặc đặt tên sai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (list.Count > 0)
            {
                for (int i = 0; i < liscom.Count; i++)
                {
                    if ((!busCo.checkExist(liscom[i])) && (!liscom[i].Name.Equals("")) && (!liscom[i].Name.Equals("Sub Component")))
                    {
                        busCo.add(liscom[i]);
                    }
                }

                for (int i = 0; i < listeq.Count; i++)
                {
                    eq.edit(listeq[i].ItemNo,listeq[i].Type,listeq[i].EquipmentDescription);
                }
                WatingForm wt = new WatingForm();
                wt.ShowDialog(this);
                for (int i = 0; i < list.Count; i++)
                {
                    if ((!list[i].ComName.Equals("Sub Component")) && (!list[i].ComName.Equals("")))
                    {
                        if (!busEQ.checkExist(list[i].Plant, list[i].ItemNo, list[i].ComName))
                        {
                            busEQ.add(list[i]);
                            // Console.WriteLine(list[i].ComName);
                        }

                    }

                }
            }
        }

        private List<RiskSummary> Calculator()
        {
            DfCalculator dfClass = new DfCalculator();
            flammableConsequences flameClass = new flammableConsequences();
            BusRiskSummary bus = new BusRiskSummary();
            BusEquipmentTemp busTemp = new BusEquipmentTemp();
            List<EquipmentTemp> list = busTemp.loads();
            
            List<RiskSummary> listRisk = new List<RiskSummary>();
            RiskSummary risk = new RiskSummary();
            BusComponents busCOm = new BusComponents();
            Component com;
            RBICalculatorConn rbicon = new RBICalculatorConn();

            assetConsequence assetExample = new assetConsequence();
            toxicConsequences toxic = new toxicConsequences();
            noneFlame_ToxicConsequences nonFlam_Toxic = new noneFlame_ToxicConsequences();
            sccDamageFactor_HIC_SOHIC sscHIC_SOHIC = new sccDamageFactor_HIC_SOHIC();
            sccDamageFactor_CausticCracking sscCaustic = new BUS.Calculator.sccDamageFactor_CausticCracking();
            sccDamageFactor_CacbonateCracking sscCacbonate = new BUS.Calculator.sccDamageFactor_CacbonateCracking();
            sscDamageFactor_HIC_SOHIC_HF sscHF = new BUS.Calculator.sscDamageFactor_HIC_SOHIC_HF();
            for (int i = 0; i < list.Count; i++)
            {

                com = busCOm.getcom(list[i].ComName);
                // ngay tinh toan = ngay hien tai
                //dfClass.calculatorDate = double.Parse(DateTime.Now.Year.ToString());
                //dfClass.ageTK = 0;
                //dfClass.Trd = 30.5;
                //String normalThic = busCOm.getcom(list[i].ComName).NorminalThickness;
                //if (normalThic.Equals(""))
                //{
                //    dfClass.T = 0;
                //}
                //else
                //{
                //    dfClass.T = double.Parse(normalThic);
                //}
                
                //dfClass.Tmin = 20;
                //dfClass.InstallDate = 2016;
                //dfClass.Date = 2016;
                //dfClass.CrB = 2;
                //dfClass.InspEff = 2;
                //dfClass.numOfInsp = 1;
                //dfClass.Fps = 1;
                //dfClass.Fip = 1;
                //String ca = busCOm.getcom(list[i].ComName).CA;
                //if (ca.Equals(""))
                //{
                //    dfClass.CA = 0;
                //}
                //else
                //{
                //    dfClass.CA = double.Parse(ca);
                //}
                //dfClass.levelInsp = "A";
                //dfClass.Crcm = 0;
                //dfClass.Crbm = 2;
                //dfClass.Fom = 10;
                //dfClass.Fdl = 1;
                //dfClass.Fwd = 1;
                //dfClass.Fam = 1;
                //dfClass.Fsm = 1;
                //dfClass.select = false;
                //dfClass.thinningDamage = true;

                // flammable consequence
                //flameClass.Fluid = "C5";
                //flameClass.componentType = "COMPC";
                //flameClass.type = "liquid";
                //flameClass.Ps = double.Parse(list[i].OperingPressInlet);
                //flameClass.Patm = 101;
                //flameClass.re = 20;
                //flameClass.pl = rbicon.getPl(flameClass.Fluid);
                //flameClass.Ts = 63.8;
                //flameClass.mass = busTemp.getMass(list[i].ItemNo, list[i].ComName);
                //flameClass.state = 1;
                //flameClass.detection = 1;
                //flameClass.isolation = 1;
                //flameClass.holnumber = 2;
                //flameClass.mitigationSystem = 2;
                
                /******Asset Consequence Calculate Example*/
                //assetExample.Materials = "1.25Cr-0.5Mo";
                //assetExample.componentType = "COMPC";
                //assetExample.prodCost = 1;
                //assetExample.equipCost = 2;
                //assetExample.CAinj = 83.12;
                //assetExample.CAcmd = flameClass.CA_cmd();
                //assetExample.propdens = 2;
                //assetExample.injcost = 2;
                //assetExample.Fluid = flameClass.Fluid;
                //assetExample.mass = flameClass.mass;
                //assetExample.envcost = 1;
                
                /*****************toxic consequence example****************/
                //toxic.componentType = "COMPC";
                //toxic.materialType = "H2S";
                //toxic.timeDura = "5";
                //toxic.ld_max = 20; //for 1/4 inch
                //toxic.mass_n = flameClass.mass;
                //toxic.tox_mfrac = 1;
                //toxic.continuous_instantaneous = false;
                //toxic.Wn = flameClass.Wn();

                //nonflam test
                //nonFlam_Toxic.componentType = "COMPC";
                //nonFlam_Toxic.mass = flameClass.mass;
                //nonFlam_Toxic.Patm = flameClass.Patm;
                //nonFlam_Toxic.Ps = flameClass.Ps;
                //nonFlam_Toxic.rate = flameClass.rate();
                //nonFlam_Toxic.steam = true;
                /*SSC Damage Factor HIC/SOHIC-H2S Example*/
                //sscHIC_SOHIC.age = 2;
                //sscHIC_SOHIC.inspectionCatalog = "D";
                //sscHIC_SOHIC.pH = 6;
                //sscHIC_SOHIC.crackPresent = false;
                //sscHIC_SOHIC.ppm = 60;
                //sscHIC_SOHIC.PWHT = 0.02;
                //sscHIC_SOHIC.inspection = 2;
                /*SSC Damage Factor Cacbonate Example*/
                //sscCacbonate.age = 2;
                //sscCacbonate.inspection = 2;
                //sscCacbonate.inspectionCatalog = "D";
                //sscCacbonate.pH = 8;
                //sscCacbonate.ppm = 200;
                //sscCacbonate.crackPresent = false;
                /*SSC Damage Factor HF*/
                sscHF.age = 4;
                sscHF.inspection = 3;
                sscHF.inspectionCatalog = "B";
                sscHF.crackPresent = false;
                sscHF.HFpresent = true;
                sscHF.ppm = 300;
                /*print to Output Window*/
                Debug.WriteLine("HF Damage Factor");
                Debug.WriteLine("D_HF = " + sscHF.D_HIC_HF_f());

                MessageBox.Show("Thời gian kể từ ngày kiểm tra cuối cùng = " + sscHF.age + " năm\n" +
                                "Inspection Catalog = " + sscHF.inspectionCatalog + "\n" +
                                "Số lần kiểm tra = " + sscHF.inspection + " lần\n" +
                                "Crack present = " + sscHF.crackPresent + "\n" +
                                "Nồng độ HF " + sscHF.ppm + " ppm\n" +
                                "D_HIC_SOHIC-HF_f = " + sscHF.D_HIC_HF_f(), "SSC Damage Factor - HIC/SOHIC-HF");

                //risk.ItemNo = list[i].ItemNo;
                //risk.ComName = list[i].ComName;
                //risk.RepresentFluid = flameClass.Fluid;
                //risk.FluidPhase = flameClass.type;
                //risk.CotcatFlammable = flameClass.convertCatalog() +"";
               /* Console.WriteLine(flameClass.CA());
                Console.WriteLine(risk.CotcatFlammable);
                Console.WriteLine(flameClass.MW());*/
                //risk.InitThinningCategory = dfClass.changeDF();

                //int checkRisk =0;
                //String prof = risk.InitThinningCategory +""+ risk.CotcatFlammable;
                //if (prof == "A1" || prof == "A2" || prof == "B1" || prof == "B2" || prof == "C1" || prof == "C2")
                //{
                //    checkRisk = 1;
                //}
                //else if (prof == "D1" || prof == "D2" || prof == "C3" || prof == "B3" || prof == "B4" || prof == "A3" || prof == "A4")
                //{
                //    checkRisk = 2;
                //}
                //else if (prof == "E1" || prof == "E2" || prof == "E3" || prof == "D3" || prof == "D4" || prof == "C4" || prof == "B5" || prof == "A5")
                //{
                //    checkRisk = 3;
                //}
                //else
                //{
                //    checkRisk = 4;
                //}
                //Console.WriteLine(prof);
                //Console.WriteLine(checkRisk);

                //if (checkRisk == 1)
                //{
                //    risk.CurrentRiskCal = "Low";
                //}
                //else if (checkRisk == 2)
                //{
                //    risk.CurrentRiskCal = "Medium";
                //}
                //else if (checkRisk == 3)
                //{
                //    risk.CurrentRiskCal = "Medium High";
                //}
                //else
                //{
                //    risk.CurrentRiskCal = "High";
                //}

                //if (bus.checkExist(risk))
                //{
                //    bus.delete(risk.ComName, risk.ItemNo);
                //}
                //bus.add(risk);

                //listRisk.Add(risk);
            }
            return listRisk;
        }
        public void exportToExcel( String duongdan, String tentap)
        {
            BusRiskSummary bus = new BusRiskSummary();
            List<RiskSummary> list = bus.loads();
            app obj = new app();
            obj.Application.Workbooks.Add(Type.Missing);
            obj.Columns.ColumnWidth = 20;

            obj.Cells[1, "A"] = "Equipment";
            obj.Cells[1, "B"] = "Equipment Description";
            obj.Cells[1, "C"] = "Equipment Type";
            obj.Cells[1, "D"] = "Components";
            obj.Cells[1, "E"] = "Represent.fluid";
            obj.Cells[1, "F"] = "Fluid phase";
            obj.Cells[1, "G"] = "Cofcat.Flammable";
            obj.Cells[1, "H"] = "Current Risk";
            obj.Cells[1, "I"] = "Cofcat.People";
            obj.Cells[1, "J"] = "Cofcat.Asset";
            obj.Cells[1, "K"] = "Cofcat.Env";
            obj.Cells[1, "L"] = "Cofcat.Reputation";
            obj.Cells[1, "M"] = "Cofcat.Combined";
            obj.Cells[1, "N"] = "Component Material Glade";
            obj.Cells[1, "O"] = "InitThinningPOFCatalog";
            obj.Cells[1, "P"] = "InitEnv.Cracking";
            obj.Cells[1, "Q"] = "InitOtherPOFCatalog";
            obj.Cells[1, "R"] = "InitPOFCatelog";
            obj.Cells[1, "S"] = "ExtThinningPOF";
            obj.Cells[1, "T"] = "ExtEnvCrackingProbabilityCatelog";
            obj.Cells[1, "U"] = "ExtOtherPOFCatelog";
            obj.Cells[1, "V"] = "ExtPOFCatelog";
            obj.Cells[1, "W"] = "POFCatelog";
            obj.Cells[1, "X"] = "Current Risk";
            obj.Cells[1, "Y"] = "Future Risk";

            //int row = 2; //start row
            for (int i = 0; i < list.Count; i++)
            {
                BusEquipments buseq = new BusEquipments();
                String Equipment = list[i].ItemNo.ToString();
                String Description = null;
                if (buseq.getdes(Equipment) == null || buseq.getdes(Equipment) == "")
                {
                    Description = "N/A";
                }
                else
                {
                    Description = buseq.getdes(Equipment);
                }
                String Equipment_Type = null;
                if (buseq.gettype(Equipment) == null || buseq.gettype(Equipment) == "")
                {
                    Equipment_Type = "N/A";
                }
                else
                {
                    Equipment_Type = buseq.gettype(Equipment).ToString();
                }
                String Components = list[i].ComName;
                String Represent_fluid = null;
                if (list[i].RepresentFluid == null || list[i].RepresentFluid == "")
                {
                    Represent_fluid = "N/A";
                }
                else
                {
                    Represent_fluid = list[i].RepresentFluid;
                }
                String Fluid_phase = null;
                if (list[i].FluidPhase == null || list[i].FluidPhase == "")
                {
                    Fluid_phase = "N/A";
                }
                else
                {
                    Fluid_phase = list[i].FluidPhase;
                }
                String Cofcat_Flammable = null;
                if (list[i].CotcatFlammable == null || list[i].CotcatFlammable == "")
                {
                    Cofcat_Flammable = "N/A";
                }
                else
                {
                    Cofcat_Flammable = list[i].CotcatFlammable;
                }
                String Current_Risk = null;
                if (list[i].CurrentRisk == null || list[i].CurrentRisk == "")
                {
                    Current_Risk = "N/A";
                }
                else
                {
                    Current_Risk = list[i].CurrentRisk;
                }
                String Cofcat_People = null;
                if (list[i].CotcatPeople == null || list[i].CotcatPeople == "")
                {
                    Cofcat_People = "N/A";
                }
                else
                {
                    Cofcat_People = list[i].CotcatPeople;
                }
                String Cofcat_Asset = null;
                if (list[i].CotcatAsset == null || list[i].CotcatAsset == "")
                {
                    Cofcat_Asset = "N/A";
                }
                else
                {
                    Cofcat_Asset = list[i].CotcatAsset;
                }
                String Cofcat_Env = null;
                if (list[i].CotcatEnv == null || list[i].CotcatEnv == "")
                {
                    Cofcat_Env = "N/A";
                }
                else
                {
                    Cofcat_Env = list[i].CotcatEnv;
                }
                String Cofcat_Reputation = null;
                if (list[i].CotcatReputation == null || list[i].CotcatReputation == "")
                {
                    Cofcat_Reputation = "N/A";
                }
                else
                {
                    Cofcat_Reputation = list[i].CotcatReputation;
                }
                String Cofcat_Combined = null;
                if (list[i].CotcatCombinled == null || list[i].CotcatCombinled == "")
                {
                    Cofcat_Combined = "N/A";
                }
                else
                {
                    Cofcat_Combined = list[i].CotcatCombinled;
                }
                String Component_Material_Glade = null;
                if (list[i].ComponentMaterialGrade == null || list[i].ComponentMaterialGrade == "")
                {
                    Component_Material_Glade = "N/A";
                }
                else
                {
                    Component_Material_Glade = list[i].ComponentMaterialGrade;
                }
                String InitThinningPOFCatalog = null;
                if (list[i].InitThinningCategory == null || list[i].InitThinningCategory == "")
                {
                    InitThinningPOFCatalog = "N/A";
                }
                else
                {
                    InitThinningPOFCatalog = list[i].InitThinningCategory;
                }
                String InitEnv_Cracking = null;
                if (list[i].InitEnvCracking == null || list[i].InitEnvCracking == "")
                {
                    InitEnv_Cracking = "N/A";
                }
                else
                {
                    InitEnv_Cracking = list[i].InitEnvCracking;
                }
                String InitOtherPOFCatalog = null;
                if (list[i].InitOtherCategory == null || list[i].InitOtherCategory == "")
                {
                    InitOtherPOFCatalog = "N/A";
                }
                else
                {
                    InitOtherPOFCatalog = list[i].InitOtherCategory;
                }
                String InitPOFCatelog = null;
                if (list[i].InitCategory == null || list[i].InitCategory == "")
                {
                    InitPOFCatelog = "N/A";
                }
                else
                {
                    InitPOFCatelog = list[i].InitCategory;
                }
                String ExtThinningPOF = null;
                if (list[i].ExtThinningCategory == null || list[i].ExtThinningCategory == "")
                {
                    ExtThinningPOF = "N/A";
                }
                else
                {
                    ExtThinningPOF = list[i].ExtThinningCategory;
                }
                String ExtEnvCrackingProbabilityCatelog = null;
                if (list[i].ExtEnvCracking == null || list[i].ExtEnvCracking == "")
                {
                    ExtEnvCrackingProbabilityCatelog = "N/A";
                }
                else
                {
                    ExtEnvCrackingProbabilityCatelog = list[i].ExtEnvCracking;
                }
                String ExtOtherPOFCatelog = null;
                if (list[i].ExtOtherCategory == null || list[i].ExtOtherCategory == "")
                {
                    ExtOtherPOFCatelog = "N/A";
                }
                else
                {
                    ExtOtherPOFCatelog = list[i].ExtOtherCategory;
                }
                String ExtPOFCatelog = null;
                if (list[i].ExtCategory == null || list[i].ExtCategory == "")
                {
                    ExtPOFCatelog = "N/A";
                }
                else
                {
                    ExtPOFCatelog = list[i].ExtCategory;
                }
                String POFCatelog = null;
                if (list[i].POFCategory == null || list[i].POFCategory == "")
                {
                    POFCatelog = "N/A";
                }
                else
                {
                    POFCatelog = list[i].POFCategory;
                }
                String Current_Risk_Cal = list[i].CurrentRiskCal.ToString();
                Console.WriteLine(list[i].CurrentRiskCal);
               /* if (list[i].CurrentRiskCal.Equals(""))
                {
                    Current_Risk_Cal = "N/A";
                }
                else
                {
                    Current_Risk_Cal = list[i].CurrentRiskCal;
                }
                */
                String Future_Risk = null;
                if (list[i].FutureRisk == null || list[i].FutureRisk == "")
                {
                    Future_Risk = "N/A";
                }
                else
                {
                    Future_Risk = list[i].FutureRisk;
                }
                //obj.Cells[i + 2, 1] = list[i].ItemNo.ToString();
                obj.Cells[i + 2, "A"] = Equipment;
                obj.Cells[i + 2, "B"] = Description;
                obj.Cells[i + 2, "C"] = Equipment_Type;
                obj.Cells[i + 2, "D"] = Components;
                obj.Cells[i + 2, "E"] = Represent_fluid;
                obj.Cells[i + 2, "F"] = Fluid_phase;
                obj.Cells[i + 2, "G"] = Cofcat_Flammable;
                obj.Cells[i + 2, "H"] = Current_Risk;
                obj.Cells[i + 2, "I"] = Cofcat_People;
                obj.Cells[i + 2, "J"] = Cofcat_Asset;
                obj.Cells[i + 2, "K"] = Cofcat_Env;
                obj.Cells[i + 2, "L"] = Cofcat_Reputation;
                obj.Cells[i + 2, "M"] = Cofcat_Combined;
                obj.Cells[i + 2, "N"] = Component_Material_Glade;
                obj.Cells[i + 2, "O"] = InitThinningPOFCatalog;
                obj.Cells[i + 2, "P"] = InitEnv_Cracking;
                obj.Cells[i + 2, "Q"] = InitOtherPOFCatalog;
                obj.Cells[i + 2, "R"] = InitPOFCatelog;
                obj.Cells[i + 2, "S"] = ExtThinningPOF;
                obj.Cells[i + 2, "T"] = ExtEnvCrackingProbabilityCatelog;
                obj.Cells[i + 2, "U"] = ExtOtherPOFCatelog;
                obj.Cells[i + 2, "V"] = ExtPOFCatelog;
                obj.Cells[i + 2, "W"] = POFCatelog;
                obj.Cells[i + 2, "X"] = Current_Risk_Cal;
                obj.Cells[i + 2, "Y"] = Future_Risk;

            }

            obj.ActiveWorkbook.SaveCopyAs(duongdan + tentap + ".xls");
            obj.ActiveWorkbook.Saved = true;


        }
        
        private void btnRisk_Click(object sender, EventArgs e)
        {
            Calculator();
            //exportToExcel(@"C:\Users\VuNA\Dropbox\Lab_Associates Team Folder\RBI\Output\", "RiskSummary");
            //exportToExcel(@"E:\", "RiskSummary");
            //sendEmail email = new sendEmail();
            //email.senEmail();
            //WatingForm wait = new WatingForm();
            //wait.ShowDialog(this);
            //UCrisk risk = new UCrisk();
            //addNewTab("Risk Summary", risk);
        }
    }
}
