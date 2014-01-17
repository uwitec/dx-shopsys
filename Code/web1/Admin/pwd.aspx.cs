using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using web1;
namespace web.admin
{
    public partial class pwd : System.Web.UI.Page
    {
        public string adminName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            adminName = Session["AdminName"].ToString(); ;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string pwd = txtPwd.Text;
            string newPwd = txtNewPwd.Text;
            string newPwd2 = txtNewPwd2.Text;
            if (pwd.Length == 0)
            {
                alert.Show(Page, "请填写原密码");
                return;
            }
            if (newPwd.Length == 0)
            {
                alert.Show(Page, "请填写新密码");
                return;
            }
            if (newPwd != newPwd2)
            {
                alert.Show(Page, "新密码与确认新密码不一致");
                return;
            }


            string memberName = Session["AdminName"].ToString();
            SQLHelper_ db = new SQLHelper_();
            db.sql = "SELECT pwd FROM " + com.tablePrefix + "admin WHERE username='" + memberName + "'";
            DataTable dt = db.Get_DataTable();

            pwd = com.MD5(pwd, 1);
            newPwd = com.MD5(newPwd, 1);

            if (dt.Rows.Count > 0)
            {
                if (pwd != dt.Rows[0]["pwd"].ToString())
                {
                    alert.Show(Page, "原密码错误");
                    return;
                }
                else
                {
                    db.sql = "UPDATE admin SET Pwd='" + newPwd + "' WHERE username='" + memberName + "'";
                    db.ExecSql();
                    alert.Show(Page,"修改成功");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
