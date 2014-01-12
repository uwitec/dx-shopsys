using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
namespace web
{
    public class Member
    {
//        string sql = @"INSERT INTO [Members]
//           ([username]
//           ,[pwd]
//           ,[email]
//           ,sex
//           ,[photo])
//     VALUES
//           (<username, varchar(50),>
//           ,<pwd, varchar(100),>
//           ,<email, varchar(100),>
//           ,<sex, varchar(2),>
//           ,<photo, varchar(500),>)";

        public Member getMemberInfo(string email)
        {
            Member m = new Member();
            string sql = "SELECT userid,username,email,sex FROM " + com.tablePrefix + "Members WHERE email='" + email + "'";
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                m.userid = dt.Rows[0]["userid"].ToString();
                m.username = dt.Rows[0]["username"].ToString();
                m.email = dt.Rows[0]["email"].ToString();
                m.sex = dt.Rows[0]["sex"].ToString();
                return m;
            }
            else
            {
                return null;
            }

        }
        public string username
        {
            get;
            set;
        }

        public string userid
        {
            get;
            set;
        }
        public string email
        {
            get;
            set;
        }
        public string sex
        {
            get;
            set;
        }



        public string photo
        {
            get;
            set;
        }


        public bool isOnLine()
        {
            bool re = false;
            //Session["member"]
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return true;
            }
            return re;
        }

        public bool exists(string uname)
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT * FROM " + com.tablePrefix + "Members WHERE username='" + uname + "'";
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool emailExists(string email)
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT * FROM " + com.tablePrefix + "Members WHERE email='" + email + "'";
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool login(string email, string pwd)
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT * FROM " + com.tablePrefix + "Members WHERE email='" + email + "' AND pwd='" + pwd + "'";
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                System.Web.HttpContext.Current.Session["username"] = dt.Rows[0]["username"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkLogin()
        {
            if (System.Web.HttpContext.Current.Session["username"] == null)
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
