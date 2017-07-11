using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using RBI.Object;
using RBI.BUS;

namespace RBI.PRE.subForm
{
    public partial class UCrisk : UserControl
    {
        public UCrisk()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            BusEquipments buseq = new BusEquipments();
            BusRiskSummary bus = new BusRiskSummary();
            List<RiskSummary> list = bus.loads();
            DataGridViewRow row;
            for (int i = 0;i <list.Count; i++)
            {
                row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = list[i].ItemNo;
                row.Cells[1].Value = buseq.getdes(list[i].ItemNo);
                row.Cells[2].Value = buseq.gettype(list[i].ItemNo);
                row.Cells[3].Value = list[i].ComName;
                row.Cells[4].Value = list[i].RepresentFluid;
                row.Cells[5].Value = list[i].FluidPhase;
                row.Cells[6].Value = list[i].CotcatFlammable;
                row.Cells[7].Value = list[i].CurrentRisk;
                row.Cells[8].Value = list[i].CotcatPeople;
                row.Cells[9].Value = list[i].CotcatAsset;
                row.Cells[10].Value = list[i].CotcatEnv;
                row.Cells[11].Value = list[i].CotcatReputation;
                row.Cells[12].Value = list[i].CotcatCombinled;
                row.Cells[13].Value = list[i].ComponentMaterialGrade;
                row.Cells[14].Value = list[i].InitThinningCategory;
                row.Cells[15].Value = list[i].InitEnvCracking;
                row.Cells[16].Value = list[i].InitOtherCategory;
                row.Cells[17].Value = list[i].InitCategory;
                row.Cells[18].Value = list[i].ExtThinningCategory;
                row.Cells[19].Value = list[i].ExtEnvCracking;
                row.Cells[20].Value = list[i].ExtOtherCategory;
                row.Cells[21].Value = list[i].ExtCategory;
                row.Cells[22].Value = list[i].POFCategory;
                row.Cells[23].Value = list[i].CurrentRiskCal;
                row.Cells[24].Value = list[i].FutureRisk;

                dataGridView1.Rows.Add(row);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
         /*   SaveFileDialog saveF = new SaveFileDialog();
            saveF.Filter = "Excel 97-2003 File (*.xls)|*.xls";
            saveF.ShowDialog();
            List<RiskSummary> list = new List<RiskSummary>();
            // xu ly
            if(saveF.FileName != "")
            {
                //tao excel application
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                //tao sheet
                Excel.Worksheet sheet = null;
                try
                {
                    // doc du lieu tu listview -> wooksheet
                    sheet = wb.ActiveSheet;
                    sheet.Name = "Risk Summary";
                    sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, list.Count]].Merge();
                    sheet.Cells[1, 1].Value = "Risk Summary";
                    sheet.Cells[1, 1].Borders.Weigth = Excel.XlBorderWeight.xlThin;
                    for(int i = 0; i < list.Count; i++)
                    {

                    }
                }catch (Exception ex)
                {
                    MessageBox.Show("error" + ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                */ 
            //}
        }
    }
}
