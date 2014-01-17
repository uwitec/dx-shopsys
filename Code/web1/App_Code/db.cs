using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web;

namespace web1
{
    public static class db
    {
        public static string realFileName = HttpContext.Current.Server.MapPath("/DB/conn.txt");

        //public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        public static string ConnectionString;
        private static SqlConnection sqlcon = new SqlConnection();
        
        private static SqlCommand myCmd = new SqlCommand();
        private static DataTable myDt = new DataTable();
        private static SqlDataAdapter myAdapter = new SqlDataAdapter();

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns></returns>
        public static bool OpenDB()
        {
            try
            {
                StreamReader sr = File.OpenText(realFileName);
                ConnectionString = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                if (sqlcon != null && sqlcon.State != ConnectionState.Open)
                {
                    sqlcon = new SqlConnection();
                    sqlcon.ConnectionString = ConnectionString;
                    sqlcon.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void CloseDB()
        {
            try
            {
                if (sqlcon != null && sqlcon.State != ConnectionState.Closed)
                    sqlcon.Close();
                sqlcon.Dispose();
            }
            catch
            { }
            GC.Collect();
        }

        /// <summary>
        /// 用与执行Transact-SQL语句并执行
        /// </summary>
        /// <returns>返回BOOL值</returns>
        public static bool ExecuteNonQuery(string sql)
        {
            try
            {
                if (sqlcon != null && sqlcon.State == ConnectionState.Closed)
                    OpenDB();
                //SqlCommand cmd = new SqlCommand(sql, sqlcon);
                using (myCmd = new SqlCommand(sql, sqlcon))
                {
                    myCmd.ExecuteNonQuery();
                    myCmd.Dispose();
                }
                CloseDB();
                return true;
            }
            catch (Exception e)
            {
                CloseDB();
                return false;
            }
        }
        /// <summary>
        /// 执行Transact-SQL语句，并返回记录集
        /// </summary>
        /// <returns></returns>
        public static DataSet Get_DataSet(string _sql)
        {
            DataSet ds = new DataSet();
            if (sqlcon != null && sqlcon.State != ConnectionState.Open)
                OpenDB();
            using (myCmd = new SqlCommand(_sql, sqlcon))
            {
                using (myAdapter = new SqlDataAdapter(myCmd))
                {
                    
                    myAdapter.Fill(ds);
                    myAdapter.Dispose();
                }
                
            }
            
            //SqlDataAdapter sda = new SqlDataAdapter(_sql, sqlcon);
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //sda.Dispose();
            CloseDB();
            return ds;
        }

        /// <summary>
        /// 执行Transact-SQL语句，并返回记录集
        /// </summary>
        /// <returns></returns>
        public static DataTable Get_DataTable(string _sql)
        {
            //SqlCommand myCmd = new SqlCommand();
            //DataTable dt = new DataTable();
            DataTable myDt = new DataTable();
            if (sqlcon != null && sqlcon.State != ConnectionState.Open)
                OpenDB();
            try
            {
                using (myCmd = new SqlCommand(_sql, sqlcon))
                {
                    //SqlDataAdapter sda = new SqlDataAdapter(myCmd);
                    //myDt.Clear();
                    //sda.Fill(myDt);
                    SqlDataAdapter sda = new SqlDataAdapter(_sql, sqlcon);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    myDt = ds.Tables[0];
                    sda.Dispose();
                }
                CloseDB();
            }
            catch
            {
                CloseDB();
            }
            return myDt;
        }


        public static string ExecSql(string _sql)
        {
            try
            {
                if (sqlcon != null && sqlcon.State != ConnectionState.Open)
                    OpenDB();
                SqlCommand scd = new SqlCommand(_sql, sqlcon);
                scd.ExecuteNonQuery();
                scd.Dispose();
                CloseDB();
                return "1";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="SPName">存储过程名称</param>
        /// <param name="HtParas">参数</param>
        /// <returns>int</returns>
        /// 调用实例：
        /// System.Collections.Hashtable Ht = new System.Collections.Hashtable();
        /// Ht.Add("@parg1", StrBcID);
        /// Ht.Add("@parg2", StrSecond);
        /// SqlHelper sqlh = new SqlHelper()
        /// if (sqlh.ExcuParametersSt("procedureName", Ht) == 1)
        ///       return true;
        ///    else
        ///        return false;
        public static int ExcuParametersSt(string SPName, System.Collections.Hashtable HtParas)
        {
            int IntRETURN_VALUE = 0;
            if (sqlcon != null && sqlcon.State != ConnectionState.Open)
                OpenDB();
            SqlCommand SqlCmd = new SqlCommand(SPName, sqlcon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(SqlCmd);
            try
            {

                for (int i = 0; i < SqlCmd.Parameters.Count; i++)
                {
                    string dd = SqlCmd.Parameters[i].ParameterName;
                    foreach (string text in HtParas.Keys)
                    {
                        if (text.ToUpper() != SqlCmd.Parameters[i].ParameterName.ToUpper())
                        {
                            continue;
                        }
                        if (HtParas[text] == null)
                        {
                            SqlCmd.Parameters[i].IsNullable = true;
                            SqlCmd.Parameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            SqlCmd.Parameters[i].Value = HtParas[text];
                        }
                        break;
                    }
                }

                //执行存储过程
                SqlCmd.ExecuteNonQuery();
                IntRETURN_VALUE = int.Parse(SqlCmd.Parameters["@RETURN_VALUE"].Value.ToString());

            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }
            return IntRETURN_VALUE;
        }
    }
}
