using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace web
{
    public static class member
    {
        public static bool exist(string userName)
        {
            DataTable dt = db.Get_DataTable("SELECT MemberID,UserName FROM " + com.tablePrefix + "Member WHERE UserName='" + userName + "'");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int maxid()
        {
            DataTable dt = db.Get_DataTable("SELECT COUNT(0) FROM " + com.tablePrefix + "Member");
            if (dt.Rows[0][0].ToString() == "0")
            {
                return 0;
            }
            else
            {
                DataTable dt1 = db.Get_DataTable("SELECT MAX(MemberID) AS id FROM " + com.tablePrefix + "Member");
                return Int32.Parse(dt1.Rows[0][0].ToString());
            }
        }
        public static bool login(string uid, string pwd)
        {
            DataTable dt = db.Get_DataTable("SELECT MemberID,Tel,Email FROM " + com.tablePrefix + "Member WHERE UserName='" + uid + "' AND pwd='" + pwd + "'");
            if (dt.Rows.Count > 0)
            {
                myMember user = new myMember();
                user.username = uid;
                user.tel = dt.Rows[0]["Tel"].ToString();
                user.email = dt.Rows[0]["Email"].ToString();
                HttpContext.Current.Session["user"] = user;
                //记录日志

                return true;
                
            }
            else
                return false;
        }

       

    }

    public class myMember
    {
        public string username,tel,email;
    }
}
