﻿using MySql.Data.MySqlClient;
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
            List<String> list = new List<String>();
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
            String select = "";
            switch (index)
            {
                case 1: select = "Small"; break;
                case 2: select = "Medium"; break;
                case 3: select = "Large"; break;
                default: select = "Rupture"; break;
            }
            sql = "SELECT "+select+" FROM tbl_component_damage_cost WHERE componentType='" + componentType + "'";
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
                            if (!buff.Equals("") || !reader.IsDBNull(0))
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
                            if (!buff.Equals("") || !reader.IsDBNull(0))
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
                            if (!buff.Equals("") || !reader.IsDBNull(0))
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
                            if (!reader.GetString(0).Equals("") || !reader.IsDBNull(0))
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
                            if (!reader.GetString(0).Equals("") || !reader.IsDBNull(0))
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
                            if (!reader.GetString(0).Equals("") || !reader.IsDBNull(0))
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
            String sql = "select " + column + " from tbl_65_lining_factor_organic where YearInService ='" + yearInService + "'";
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
            String sql = "Select " + liningType + " from tbl_64_lining_factor_inorganic where YearsSinceLastInspection ='" + yearInService + "'";
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
            String sql = "SELECT " + field + " FROM rbi.tbl_74_scc_damage_factor WHERE SVI = '" + svi + "'";
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
            String sql = "select `" + ppm + "` from tbl_93_evironmental_severity where PH = '" + pH + "'";
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
            String sql = "SELECT `PWHT(" + pwht + ")` FROM tbl_94_sulfide_stress_cracking WHERE Environmental ='" + environment + "' ";
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
            String sql = "SELECT `" + ppm + "` FROM rbi.tbl_113_susceptibility_carbonate WHERE pH = '" + pH + "'";
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
                sql = "SELECT `" + field + "` FROM tbl_143_hsc_hf WHERE Field ='PWHT' ";
            else
                sql = "SELECT `" + field + "` FROM tbl_143_hsc_hf WHERE Field ='As-Welded' ";
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
            String sql = "SELECT `" + field + "` FROM rbi.tbl_133_clscc WHERE PH = '" + pH + "' AND Temperature ='" + temp + "' ";
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
            String sql = "SELECT " + driver + " FROM rbi.tbl_163_external_corrosion_rate WHERE OperatingTemp = '" + temperature + "'";
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
        public int getHTHA(int inspection, String Catalog, String susceptibility)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String select = inspection + Catalog;
            String sql = "SELECT " + select + " FROM rbi.tbl_htha_damage WHERE Susceptibility = '" + susceptibility + "'";
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
        public double[] getK_h_water(String Soil) //table 7.2
        {
            double[] data = { 0, 0, 0 };
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT WaterLower,WaterUpper,SoilPorosity FROM rbi.tbl_soil_type_and_properties WHERE SoilType = '" + Soil + "'";
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
                            data[0] = reader.GetDouble(0);
                            data[1] = reader.GetDouble(1);
                            data[2] = reader.GetDouble(2);
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
        public double[] get_pl_ul(String fluid) //table 7.1
        {
            double[] data = { 0, 0};
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT LiquidDensity,LiquidViscosity FROM rbi.tbl_fluid_properties WHERE Fluid = '" + fluid + "'";
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
                            data[0] = reader.GetDouble(0);
                            data[1] = reader.GetDouble(1);
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
        public int[] getCost(String Envi_Sensi) //table 7.6 page 3-150
        {
            int[] data = { 0, 0, 0, 0 , 0, 0};
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + Envi_Sensi + " FROM rbi.tbl_cost_parameter_environmental_sensitivity";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            data[i++] = reader.GetInt32(0);
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
        public double getHoleDiameter(String holeSize) //table 7.3
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT ReleaseHoleDiameter FROM rbi.tbl_713_release_hole_size_tank_shell_course WHERE ReleaseHoleSize = '"+holeSize+"'";
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
        public int getN_rh(double diameter, String releaseHole)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + releaseHole + " FROM rbi.tbl_75_function_tank_diameter WHERE TankDiameter = '" + diameter + "'";
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
        public double getToxicImpactCriteria(String toxicComponent, String criteria)
        {
            double toxicImpactCriteria = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + criteria + "` FROM tbl_toxic_impact_criteria WHERE `Toxic Component`='" + toxicComponent + "'";
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
                            toxicImpactCriteria = reader.GetDouble(0);
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
            return toxicImpactCriteria;
        }
        public double getGffn(String componentType, String holesize)
        {
            double gff = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + holesize + "` FROM tbl_gff WHERE componentType='" + componentType + "'";
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
                            gff = reader.GetDouble(0);
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
            return gff;
        }
        public double getksurf(String surface)
        {
            double getksurf = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `Thermal Conductivity` FROM tbl_surface_interaction_param WHERE `Surface`='" + surface + "'";
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
                            getksurf = reader.GetDouble(0);
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
            return getksurf;
        }
        public double getxsurf(String surface)
        {
            double getxsurf = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `Surface Roughness` FROM tbl_surface_interaction_param WHERE `Surface`='" + surface + "'";
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
                            getxsurf = reader.GetDouble(0);
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
            return getxsurf;
        }
        public double getalphasurf(String surface)
        {
            double getalphasurf = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `Thermal Diffusivity` FROM tbl_surface_interaction_param WHERE `Surface`='" + surface + "'";
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
                            getalphasurf = reader.GetDouble(0);
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
            return getalphasurf;
        }
        public double getfracEvap(String Fluid)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select Fract from tbl_fluid_leak where Fluid ='" + Fluid + "'";
            double data = 0;
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
        public double getpoii_n_amb_ambientTemperature(String releaseType, String fluidPhase)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `PII, Given Ignition At Ambient Temperature` from tbl_event_propability where `Release Type` = '" + releaseType + "' and `Fluid Phase` = '" + fluidPhase + "'";
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
        public double getpoii_AIT(String releaseType, String fluidPhase)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `PII, Given Ignition At AIT` from tbl_event_propability where `Release Type` = '" + releaseType + "' and `Fluid Phase` = '" + fluidPhase + "'";
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
        public double getpvcedi(String releaseType, String fluidPhase)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `PVCEorFF, Given Delayed Ignition VCE` from tbl_event_propability where `Release Type` = '" + releaseType + "' and `Fluid Phase` = '" + fluidPhase + "'";
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
        ///<summary>
        /// tra cuu D_fb tu bang 5.12
        ///</summary>
        public double D_fb_tank(double art, String level)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + level + " FROM tbl_512_dfb_thin_tank_bottom WHERE art ='" + art + "'";
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
                            data = read.GetInt32(0);
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
        ///<summary>
        /// xac dinh D_fb_brittle table(21.4 and 21.5)
        /// dua vao component thickness va Tmin-Tref
        ///</summary>
        public double get_D_fb_brittle(bool select, double deltaT, double componentThickness)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = null;
            if (select)
                sql = "SELECT `" + componentThickness + "` FROM tbl_215_damage_factor_pwht where `Tmin-Tref`='" + deltaT + "'";
            else
                sql = "SELECT `" + componentThickness + "` FROM tbl_214_damage_factor_not_pwht where `Tmin-Tref`='" + deltaT + "'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        /// Susceptibility to cracking _ External CLSCC Austenitic
        ///</summary>
        public String getSusceptibilityExternalCLSCCAustenitic(String opTempString, String driver)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `" + driver + "` from tbl_183_external_clscc_austenitic_sscp where OpTemp = '" + opTempString + "'";
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
        /// Susceptibility to cracking _ External CUI CLSCC Austenitic
        ///</summary>
        public String getSusceptibilityExternalCUICLSCCAustenitic(String opTempString, String driver)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `" + driver + "` from tbl_193_external_cui_clscc_austenitic_sscp where OpTemp = '" + opTempString + "'";
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
        /// Damage Factor - 885 Embrittlement
        ///</summary>
        public double get885EmbrittlementDamageFactor(double substraction)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `Damage Factor` from tbl_885_embrittlement_damage_factor where `Tmin-Tref` = '" + substraction + "'";
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
        ///<summary>
        /// get Tref ( table 21.3): impact test exemption temperature
        ///</summary>
        public double getTref(double componentThickness, String curve)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `Curve" + curve + "` FROM tbl_213_impact_test_exemption where ComponentThickness ='" + componentThickness + "'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        // get data table 24.3j
        public double getDf_spe_243(double lmhSigma, double Tmin)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String column = null;
            if ((1 < lmhSigma) && (lmhSigma < 5))
                column = "LowSigma";
            if ((5 <= lmhSigma) && (lmhSigma < 10))
                column = "MediumSigma";
            if (10 <= lmhSigma)
                column = "HighSigma";
            String sql = "select " + column + " from tbl_spe_damages_factor where EvaluationTemperature ='" + Tmin + "'";
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
        ///<summary>
        /// xac dinh toc do an mon bang table 17.3
        ///</summary>
        public int getCrb_CUI(double temp, String driver)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + driver + "` FROM rbi.tbl_corrosion_rate_cui WHERE Temperature = '" + temp + "'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        ///<summary>
        /// xac dinh he so Fins tu bang 17.4
        ///</summary>
        public double getFins(String insulation)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `Fins` FROM tbl_corrosion_rate_insulation_type WHERE IsulationType ='" + insulation + "'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// Tra cuu DFB
        /// </summary>
        /// <returns></returns>
        public int getDfb(double art, int insp, String level)
        {
            int data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT " + level + " FROM tbl_511_dfb_thin WHERE insp='" + insp + "' AND art ='" + art + "'";
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
                            data = read.GetInt32(0);
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
        /// Susceptibility to cracking _ PTA Cracking
        ///</summary>
        public String getSusceptibilityPTACracking(String material, String heatTreatment)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "select `" + heatTreatment + "` from tbl_123_pta_cracking_sscp where Material = '" + material + "'";
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
        /////<summary>
        ///// get data table HSC_HF( table 14.3)
        /////</summary>
        //public String getHSC_HF(bool select, String field)
        //{
        //    String data = null;
        //    MySqlConnection conn = DBUtils.getDBConnection();
        //    String sql = null;
        //    if (select)
        //        sql = "SELECT `" + field + "` FROM tbl_hsc_hf WHERE Field ='PWHT' ";
        //    else
        //        sql = "SELECT `" + field + "` FROM tbl_hsc_hf WHERE Field ='As-Welded' ";
        //    conn.Open();
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = sql;
        //        using (DbDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    data = reader.GetString(0);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return data;
        //}
        ///<summary>
        ///  get hic/sohic cracking table 10.4
        ///</summary>
        public String getHIC(String evironmental, String Suscep, bool pwht)
        {
            String data = null;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = null;
            if (pwht)
                sql = "SELECT `" + Suscep + "_PWHT` FROM tbl_hic_sohic_cracking WHERE Environmental = '" + evironmental + "'";
            else
                sql = "SELECT `" + Suscep + "_As` FROM tbl_hic_sohic_cracking WHERE Environmental ='" + evironmental + "'";
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
        /// tao tra cuu cho 2 truong hop dac biet
        ///</summary>

        // tra bang 7.8 de tim ra CF_pass va CF_fail
        public double get_CF(String effective, bool isPass)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = null;
            if (isPass)
                sql = "SELECT `" + effective + "` FROM rbi.tbl_78_level_inspection_confidence_factor where `Inspection Result`='Pass'";
            else
                sql = "SELECT `" + effective + "` FROM rbi.tbl_78_level_inspection_confidence_factor where `Inspection Result`='Fail'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        public double get_n(String Severity, int location)
        {
            double data = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT `" + location + "N` FROM rbi.tbl_75_default_weibull WHERE `Fluid Severity` = '" + Severity + "'";
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
                MessageBox.Show("Error: " + e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
    }
}
