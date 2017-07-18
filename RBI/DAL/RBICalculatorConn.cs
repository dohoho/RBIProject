using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBI.DAL
{
    class RBICalculatorConn
    {
        public String getmaxArt(String art)
        {
            List<String> list = new List<string>();
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select art from tbl_dfb_thin where art <= '" + art + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            list.Add(read.GetString(0));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return list[list.Count -1];
        }

        public int getDfb(double A_rt, int insp, String level, String componentType)
        {
            int data = 0;
            String sql = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            if(componentType == "TANKBOTTOM")
                sql = "SELECT " + level + " FROM tbl_dfb_thin_tank_bottom WHERE art ='" + A_rt + "'";
            else
                sql = "SELECT " + level + " FROM tbl_dfb_thin WHERE art ='" + A_rt + "' AND insp = '"+ insp +"'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using(DbDataReader read = cmd.ExecuteReader())
                {
                    if (read.HasRows)
                    {
                        while(read.Read())
                        {
                            data = read.GetInt32(0);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }

        public String getGff(String comStyle)
        {
            String gff = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT gffTotal FROM tbl_gff WHERE componentType='" + comStyle + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using(DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            gff = reader.GetString(0);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return gff;
        }
        public double getNBP(String Fluid)
        {
            double data = 0;
            String sql = "SELECT NBP FROM tbl_fluid_leak WHERE Fluid='" + Fluid + "'";
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            data = reader.GetDouble(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;

        }
        public double getOutage(String componentType, int index)
        {
            double data = 0;
            String sql = null;
            if (index == 1) sql = "SELECT small FROM tbl_equipment_outage WHERE componentType='" + componentType + "'";
            else if (index == 2) sql = "SELECT medium FROM tbl_equipment_outage WHERE componentType='" + componentType + "'";
            else if (index == 3) sql = "SELECT large FROM tbl_equipment_outage WHERE componentType='" + componentType + "'";
            else sql = "SELECT rupture FROM tbl_equipment_outage WHERE componentType='" + componentType + "'";
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            data = double.Parse(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is: " + ex.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getGff(String comStyle, int a)
        {
            String data = null;
            String sql = null;
            if (a == 1) sql = "SELECT small FROM tbl_gff WHERE componentType='" + comStyle + "'";
            else if (a == 2) sql = "SELECT medium FROM tbl_gff WHERE componentType='" + comStyle + "'";
            else if (a == 3) sql = "SELECT large FROM tbl_gff WHERE componentType='" + comStyle + "'";
            else sql = "SELECT rupture FROM tbl_gff WHERE componentType='" + comStyle + "'";
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getMw(String fluid)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT MW FROM tbl_properties_level1 WHERE Fluid = '" + fluid + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Function Error"+ e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public int getHolecost(String componentType, int index)
        {
            int data = 0;
            String sql = null;
            if (index == 1) sql = "SELECT Small FROM tbl_component_damage_cost WHERE componentType='" + componentType + "'";
            else if (index == 2) sql = "SELECT Medium FROM tbl_component_damage_cost WHERE componentType='" + componentType + "'";
            else if (index == 3) sql = "SELECT Large FROM tbl_component_damage_cost WHERE componentType='" + componentType + "'";
            else sql = "SELECT Rupture FROM tbl_component_damage_cost WHERE componentType='" + componentType + "'";
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = int.Parse(reader.GetString(0));
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double getMatcost(String material)
        {
            double data = 0;
            String sql = null;
            sql = "SELECT CostFactor FROM tbl_material_cost_factor WHERE Materials='" + material + "'";
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = double.Parse(reader.GetString(0));
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getC(int a)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT SIUnits FROM tbl_si_conversion WHERE conversionFactory = '" + a + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getAIT(String fluid)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT Auto FROM tbl_properties_level1 WHERE Fluid = '" + fluid + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String buff = reader.GetString(0);
                            if (!buff.Equals(""))
                            {
                                data = buff;
                            }
                            else
                            {
                                data = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getcmd(String fluid, int a, String type, String getX) 
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = null;
            if (type.Equals("Liquid"))
                type = "liquid";
            else
                type = "gas";

            if (a == 1)
            {
                sql = "SELECT CAINL_" + type + "_" + getX + " FROM tbl_component_damage WHERE Fluid = '" + fluid + "'";
            }
            else if (a == 2)
            {
                sql = "SELECT CAIL_" + type + "_" + getX + " FROM tbl_component_damage WHERE Fluid = '" + fluid + "'";
            }
            else if(a == 3)
            {
                sql = "SELECT IAINL_" + type + "_" + getX + " FROM tbl_component_damage WHERE Fluid = '" + fluid + "'";
            }
            else
            {
                sql = "SELECT IAIL_" + type + "_" + getX + " FROM tbl_component_damage WHERE Fluid = '" + fluid + "'";
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String buff = reader.GetString(0);
                            if (!buff.Equals("") || buff!=null)
                            {
                                data = buff;
                            }
                            else
                            {
                                data = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public String getinj(String fluid, int a, String type, String getX)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();

            if (type.Equals("Liquid"))
                type = "liquid";
            else
                type = "gas";
            String sql = null;
            if (a == 1)
            {
                sql = "SELECT CAINL_" + type + "_" + getX + " FROM tbl_component_damage_personel WHERE Fluid = '" + fluid + "'";
            }
            else if (a == 2)
            {
                sql = "SELECT CAIL_" + type + "_" + getX + " FROM tbl_component_damage_personel WHERE Fluid = '" + fluid + "'";
            }
            else if (a == 3)
            {
                sql = "SELECT IAINL_" + type + "_" + getX + " FROM tbl_component_damage_personel WHERE Fluid = '" + fluid + "'";
            }
            else
            {
                sql = "SELECT IAIL_" + type + "_" + getX + " FROM tbl_component_damage_personel WHERE Fluid = '" + fluid + "'";
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String buff = reader.GetString(0);
                            if (!buff.Equals(""))
                            {
                                data = buff;
                            }
                            else
                            {
                                data = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="get cac tham so cua cp"></param>
        /// <returns></returns>
        public int getCp_ideal(String fluid)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT ideal FROM tbl_properties_level1 WHERE Fluid = '" + fluid + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.GetString(0).Equals(""))
                            {
                                data = int.Parse(reader.GetString(0));
                            }
                            else data = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double getCp(String fluid, String getX)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + getX + " FROM tbl_properties_level1 WHERE Fluid = '" + fluid + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.GetString(0).Equals(""))
                            {
                                data = double.Parse(reader.GetString(0));
                            }
                            else data = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error"+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double getPl(String Fluid)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            double data = 0;
            conn.Open();
            String sql = "SELECT Density FROM tbl_properties_level1 WHERE Fluid ='" + Fluid + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.GetString(0).Equals(""))
                            {
                                data = double.Parse(reader.GetString(0));
                            }
                            else data = 0;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Function Error" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double[] get_cd_ef(string materialType, string timeDuration)
        {
            double[] e_f = {0,0};
            MySqlConnection conn = DBUtils.getDBConnection();
            //double data = 0;
            conn.Open();
            String sql = "SELECT a,b FROM rbi.tbl_gas_toxic where Toxic = '"+materialType+"' and ContinuousReleasesDuration='"+timeDuration+"'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            e_f[0] = double.Parse(reader.GetString(0));
                            e_f[1] = double.Parse(reader.GetString(1));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Function Error" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return e_f;
        }
        public double getDf_liner_65(int yearInService, int age)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String column = null;
            if (age <= 3)
            {
                column = "WithinLast3Years";
            }
            else if ((age > 3) && (age <= 6))
            {
                column = "WithinLast6Years";
            }
            else
            {
                column = "MoreThan6Years";
            }
            String sql = "select " + column + " from tbl_lining_factor_organic where YearInService ='" + yearInService + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetDouble(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double getDf_liner_64(int yearInService, String liningType)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "Select " + liningType + " from tbl_lining_factor_inorganic where YearsSinceLastInspection ='" + yearInService + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetDouble(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        /// get D_f_scc table 7.4
        ///</summary>
        public int getD_f_scc(int svi, String field)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + field + " FROM rbi.tbl_scc_damage_factor WHERE SVI = '" + svi + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        /// tra cuu do anh tuong toi moi truong dua vao pH( table 9.3)
        ///</summary>
        public String getEnvironmental(String ppm, String pH)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            String sql = "select `" + ppm + "` from tbl_evironmental_severity where PH = '" + pH + "'";
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        /// tra cuu Sulfide Stress cracking table 9.4
        ///</summary>
        public String getSulfideStressCracking(String environment, String pwht)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `PWHT(" + pwht + ")` FROM tbl_sulfide_stress_cracking WHERE Environmental ='" + environment + "' ";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        ///  get hic/sohic cracking table 10.4
        ///</summary>
        public String getHIC(String evironmental, String Suscep)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + Suscep + "_PWHT` FROM rbi.tbl_hic_sohic_cracking WHERE Environmental = '" + evironmental + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        ///get Susceptibility to Cracking – Carbonate Cracking (table 11.3)
        ///</summary>
        public String getSusCarbonate(String pH, String ppm)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + ppm + "` FROM rbi.tbl_susceptibility_carbonate WHERE pH = '" + pH + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }

        public string getSusHF(string SulfurSteel, bool IsPWHT)
        {
            String data = null;
            String sql = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            if (IsPWHT) sql = "SELECT PWHT FROM rbi.tbl_susceptibility_hic_sohic_hf WHERE SulfurSteel = '" + SulfurSteel + "'";
            else sql = "SELECT AsWelded FROM rbi.tbl_susceptibility_hic_sohic_hf WHERE SulfurSteel = '" + SulfurSteel + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        /// get data table HSC_HF( table 14.3)
        ///</summary>
        public String getHSC_HF(bool select, String field)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            String sql = null;
            if (select)
                sql = "SELECT `" + field + "` FROM tbl_hsc_hf WHERE Field ='PWHT' ";
            else
                sql = "SELECT `" + field + "` FROM tbl_hsc_hf WHERE Field ='As-Welded' ";
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// tra cuu bang 13.3
        /// </summary>
        public String getCLSCC(String temp, String pH, String field)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + field + "` FROM rbi.tbl_clscc WHERE PH = '" + pH + "' AND Temperature ='" + temp + "' ";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public int getCorrosionRate(double temperature, String driver)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + driver + " FROM rbi.tbl_corrosion_rate WHERE Temperature = '" + temperature + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        //public object GetDataBase(object obj1, object obj2, string table, string where)
        //{
        //    object data = null;
        //    string sql = null;
        //    MySqlConnection conn = new DBUtils.getDBConnection();
        //    conn.Open();
        //    sql = "SELECT '" + obj1 + "' FROM '" + table + "' WHERE '" + where + " = ' " + obj2 + "'";
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = sql;
        //        using (DbDataReader reader = cmd.ExecuteReader())
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    data = reader.GetValue(0);
        //                }
        //            }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return data;
        //}
        
    }
}
