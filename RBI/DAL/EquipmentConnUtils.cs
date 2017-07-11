using MySql.Data.MySqlClient;
using RBI.Object;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBI.DAL
{
    class EquipmentConnUtils
    {
        Equipment eq = null;
        public void add(String itemNo, String name, String qty, String semi_Qualitative, String quanlitative, String processStream, String pressure, String pha, String business, String processStreamFluid, String operatingPressure, String phaRating, String businessLossValue, String group, String type, String equipmentDescription, String unitCode)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "INSERT INTO tbl_equipmentlist VALUES('" + itemNo + "','" + name + "','" + qty + "','" + semi_Qualitative + "','" + quanlitative + "','" + processStream + "','" + pressure + "','" + pha + "','" + business + "','" + processStreamFluid + "','" + operatingPressure + "','" + phaRating + "','" + businessLossValue + "','" + group + "','" + type + "','" + equipmentDescription + "','" + unitCode + "')";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Add data failed" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public void edit(String itemNo, String type, String description)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "UPDATE tbl_equipmentlist SET Type = '" + type + "', EquipmentDescription = '" + description + "' WHERE ItemNo ='" + itemNo + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Edit data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public void edit(String itemNo, String name, String qty, String semi_Qualitative, String quanlitative, String processStream, String pressure, String pha, String business, String processStreamFluid, String operatingPressure, String phaRating, String businessLossValue, String group, String type, String equipmentDescription, String unitCode, String olderitemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "UPDATE tbl_equipmentlist SET ItemNo = '" + itemNo + "',"
                                                        + " Name = '" + name + "',"
                                                        + " Qty = '" + qty + "',"
                                                        + " SemiQualitative = '" + semi_Qualitative + "',"
                                                        + " Quanlitative = '" + quanlitative + "',"
                                                        + " ProcessStream = '" + processStream + "',"
                                                        + " Pressure = '" + pressure + "',"
                                                        + " PHA = '" + pha + "',"
                                                        + " Business = '" + business + "',"
                                                        + " ProcessStreamFluid = '" + processStreamFluid + "'>,"
                                                        + " OperatingPressure = '" + operatingPressure + "',"
                                                        + " PHARating = '" + phaRating + "',"
                                                        + " BusinessLossValue = '" + businessLossValue + "',"
                                                        + " Group = '" + group + "',"
                                                        + " Type = '" + type + "',"
                                                        + " EquipmentDescription = '" + equipmentDescription + "',"
                                                        + " tbl_equipmentforrbi_UnitCode = '" + unitCode + "' "
                                                        + " WHERE ItemNo = '" + olderitemNo + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Edit data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public void delete(String itemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "DELETE FROM tbl_equipmentlist WHERE ItemNo = '" + itemNo + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Delete data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public List<Equipment> loads()
        {
            List<Equipment> listEq = new List<Equipment>();
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist";
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
                            eq = new Equipment();
                            eq.ItemNo = reader.GetString(0);
                            eq.Name = reader.GetString(1);
                            eq.Qty = reader.GetString(2);
                            eq.Semi_Quanty = reader.GetString(3);
                            eq.Quanlitative = reader.GetString(4);
                            eq.ProcessStream = reader.GetString(5);
                            eq.Pressure = reader.GetString(6);
                            eq.PHA = reader.GetString(7);
                            eq.Business = reader.GetString(8);
                            eq.ProcessStreamFluid = reader.GetString(9);
                            eq.OperatingPressure = reader.GetString(10);
                            eq.PHARating = reader.GetString(11);
                            eq.BusinessLossValue = reader.GetString(12);
                            eq.Group = reader.GetString(13);
                            eq.Type = reader.GetString(14);
                            eq.EquipmentDescription = reader.GetString(15);
                            eq.UnitCode = reader.GetString(16);
                            listEq.Add(eq);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Load data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return listEq;
        }
        public List<Equipment> loads(String data)
        {
            List<Equipment> listEq = new List<Equipment>();
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist WHERE tbl_equipmentforrbi_UnitCode ='" + data + "'";
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
                            eq = new Equipment();
                            eq.ItemNo = reader.GetString(0);
                            eq.Name = reader.GetString(1);
                            eq.Qty = reader.GetString(2);
                            eq.Semi_Quanty = reader.GetString(3);
                            eq.Quanlitative = reader.GetString(4);
                            eq.ProcessStream = reader.GetString(5);
                            eq.Pressure = reader.GetString(6);
                            eq.PHA = reader.GetString(7);
                            eq.Business = reader.GetString(8);
                            eq.ProcessStreamFluid = reader.GetString(9);
                            eq.OperatingPressure = reader.GetString(10);
                            eq.PHARating = reader.GetString(11);
                            eq.BusinessLossValue = reader.GetString(12);
                            eq.Group = reader.GetString(13);
                            eq.Type = reader.GetString(14);
                            eq.EquipmentDescription = reader.GetString(15);
                            eq.UnitCode = reader.GetString(16);
                            listEq.Add(eq);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Load data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return listEq;
        }
        public bool checkExist(String itemNo)
        {
            bool check = false;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist WHERE ItemNo = '" + itemNo + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        check = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return check;
        }
        public Equipment getEquipment(String itemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist WHERE ItemNo ='" + itemNo + "'";
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
                            eq = new Equipment();
                            eq.ItemNo = reader.GetString(0);
                            eq.Name = reader.GetString(1);
                            eq.Qty = reader.GetString(2);
                            eq.Semi_Quanty = reader.GetString(3);
                            eq.Quanlitative = reader.GetString(4);
                            eq.ProcessStream = reader.GetString(5);
                            eq.Pressure = reader.GetString(6);
                            eq.PHA = reader.GetString(7);
                            eq.Business = reader.GetString(8);
                            eq.ProcessStreamFluid = reader.GetString(9);
                            eq.OperatingPressure = reader.GetString(10);
                            eq.PHARating = reader.GetString(11);
                            eq.BusinessLossValue = reader.GetString(12);
                            eq.Group = reader.GetString(13);
                            eq.Type = reader.GetString(14);
                            eq.EquipmentDescription = reader.GetString(15);
                            eq.UnitCode = reader.GetString(16);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Load data failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return eq;
        }
        /// <summary>
        /// get total 
        /// </summary>

        /// qty 
        public int getTotalQty(String unitCode)
        {
            int total = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist WHERE tbl_equipmentforrbi_UnitCode = '" + unitCode + "'";
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
                            int k = 0;
                            String x = reader.GetString(2);
                            if (x.Equals(""))
                                k = 0;
                            else
                                k = int.Parse(x);
                            total += k;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return total;
        }

        // Semi-Quantitative
        public int getTotalSemi(String unitCode)
        {
            int total = 0;
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String sql = "SELECT * FROM tbl_equipmentlist WHERE tbl_equipmentforrbi_UnitCode = '" + unitCode + "'";
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
                            int k = 0;
                            String x = reader.GetString(3);
                            if (x.Equals(""))
                                k = 0;
                            else
                                k = int.Parse(x);
                            total += k;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return total;
        }
        //get unitcode
        public String getUnitcode(String itemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String data = null;
            String sql = "SELECT * FROM tbl_equipmentlist WHERE ItemNo = '" + itemNo + "'";
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
                            data = reader.GetString(16);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        //get des
        public String getDes(String itemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String data = null;
            String sql = "SELECT * FROM tbl_equipmentlist WHERE ItemNo = '" + itemNo + "'";
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
                            data = reader.GetString(15);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return data;
        }
        //get type
        public String getType(String itemNo)
        {
            MySqlConnection conn = DBUtils.getDBConnection();
            conn.Open();
            String data = null;
            String sql = "SELECT * FROM tbl_equipmentlist WHERE ItemNo = '" + itemNo + "'";
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
                            data = reader.GetString(14);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
