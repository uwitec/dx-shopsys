using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using web1;
using DBFramework;
using DBFramework.Entities;
using System.Collections.Generic;

namespace web1
{
    public static class clsAdmin
    {

        public static bool adminExists(string username)
        {
            List<DXAdmin> admList = SQLHelper.GetEntities<DXAdmin>(" Name='" + username + "' AND IsDeleted=0 ");
            if (admList.Count==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string adminEdit(string id, string pwd, string role)
        {
            string sql = "";
            if (pwd.Length > 0)
                sql = "UPDATE Admin SET pwd='" + pwd + "',role='" + role + "' WHERE id=" + id;
            else
                sql = "UPDATE Admin SET role='" + role + "' WHERE id=" + id;
            try
            {
                SQLHelper_ db = new SQLHelper_();
                db.sql = sql;
                db.ExecSql();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;// +"SQL:" + sql;
            }
        }
        public static bool AdminDel(string id)
        {
            string sql = "DELETE Admin WHERE id=" + id;
            try
            {
                SQLHelper_ db = new SQLHelper_();
                db.sql = sql;
                db.ExecSql();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable AdminInfo(string username)
        {
            string sql = "SELECT * FROM " + com.tablePrefix + "Admin WHERE username='" + username + "'";
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            return db.Get_DataTable();
        }

        public static DataTable AdminInfo(int id)
        {
            string sql = "SELECT * FROM " + com.tablePrefix + "Admin WHERE id=" + id.ToString() + "";
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            return db.Get_DataTable();
        }

        public static bool gsqLogin(string uname)
        {
            SQLHelper.Setup();
            List<DXAdmin> list = SQLHelper.GetEntities<DXAdmin>();
            if (list.Count > 0)
            {
                System.Web.HttpContext.Current.Session["AdminName"] = list[0].Name;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool login(string uname, string pwd)
        {
            DBFramework.SQLHelper.ConnectionString = Const.connstr;
            List<DXAdmin> users = DBFramework.SQLHelper.GetEntitiesByQuery<DXAdmin>("SELECT * FROM DXAdmin WHERE name='" + uname + "' AND pwd='" + pwd + "'");
            if (users.Count > 0)
            {
                System.Web.HttpContext.Current.Session["AdminName"] = uname;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool checkLogin()
        {
            if (System.Web.HttpContext.Current.Session["AdminName"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
