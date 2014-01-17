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
    public class SQLHelper_
    {
        //public static string realFileName = HttpContext.Current.Server.MapPath("/DB/conn.txt");

        #region 变量
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        //public static string ConnectionString;

        private string _sql = "";
        private SqlConnection sqlcon;

        #endregion


        public string sql { set { _sql = value; } get { return _sql; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLHelper_()
        {
            //StreamReader sr = File.OpenText(realFileName);
            //ConnectionString = sr.ReadToEnd();
            //sr.Close();
            //sr.Dispose();
            sqlcon = new SqlConnection(ConnectionString);
            sqlcon.Open();

        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns></returns>
        public bool OpenDB()
        {
            try
            {
                if (sqlcon.State != ConnectionState.Open)
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
        public void CloseDB()
        {
            try
            {
                if (sqlcon.State != ConnectionState.Closed)
                    sqlcon.Close();
                sqlcon.Dispose();
            }
            catch
            { }
            //GC.Collect();
        }

        /// <summary>
        /// 用与执行Transact-SQL语句并执行
        /// </summary>
        /// <returns>返回BOOL值</returns>
        public bool ExecuteNonQuery(out string Result)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    OpenDB();
                SqlCommand cmd = new SqlCommand(_sql, sqlcon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Result = "";
                CloseDB();
                return true;
            }
            catch (Exception e)
            {
                CloseDB();
                Result = e.Message;
                return false;
            }
        }


        /// <summary>
        /// 执行Transact-SQL语句，并返回记录集
        /// </summary>
        /// <returns></returns>
        public DataSet Get_DataSet()
        {
            if (sqlcon.State != ConnectionState.Open)
                OpenDB();
            SqlDataAdapter sda = new SqlDataAdapter(_sql, sqlcon);
            DataSet ds = new DataSet();
            try
            {
                sda.Fill(ds);
                sda.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ",sql=" + _sql);
            }
            finally
            {
                CloseDB();
            }
            return ds;
        }

        /// <summary>
        /// 执行Transact-SQL语句，并返回记录集
        /// </summary>
        /// <returns></returns>
        public DataTable Get_DataTable()
        {
            if (sqlcon.State != ConnectionState.Open)
                OpenDB();
            SqlDataAdapter sda = new SqlDataAdapter(_sql, sqlcon);
            try
            {
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.Dispose();
                CloseDB();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "sql: " + _sql);
            }
            
        }

        /// <summary>
        /// 执行Transact-SQL语句，并返回执行结果
        /// </summary>
        /// <returns></returns>
        public bool ExcSql()
        {
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                    OpenDB();
                SqlCommand scd = new SqlCommand(_sql, sqlcon);
                scd.ExecuteNonQuery();
                scd.Dispose();
                CloseDB();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public string ExecSql()
        {
            try
            {
                if (sqlcon.State != ConnectionState.Open)
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
        /// 执行Transact-SQL语句，并返回DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ExcSqlResult(string sql)
        {
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                    OpenDB();
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlcon);
                sda.SelectCommand.CommandTimeout = 0;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                sda.Dispose();
                CloseDB();
                return ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                return null;
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
        public int ExcuParametersSt(string SPName, System.Collections.Hashtable HtParas)
        {
            int IntRETURN_VALUE = 0;
            if (sqlcon.State != ConnectionState.Open)
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
