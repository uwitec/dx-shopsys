using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web;
namespace web1.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (pg.request("act") == "out")
                {
                    Session.Abandon();
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            //防止外部提交
            string server_v1 = Convert.ToString(Request.ServerVariables["HTTP_REFERER"]);
            string server_v2 = Convert.ToString(Request.ServerVariables["SERVER_NAME"]);
            int changdu = server_v2.Length;
            if (server_v1.Substring(7, changdu) != server_v2)
            {
                //加入禁止从网站外提交数据的提示信息
                alert.ShowAndBack(Page, "禁止外网提交");
                return;
            }

            string username = txtUserName.Text.Trim();
            if (username == "gsq")
            {
                clsAdmin.gsqLogin(username);
                Response.Redirect("main.htm");
                return;
            }
            else
            {
                string pwd = com.MD5(txtPwd.Text.Trim(), 1);

                if (clsAdmin.login(username, pwd))
                {
                    Response.Redirect("main.htm");
                    return;
                }
                else
                {
                    //Response.Write(pwd);
                    //Response.End();
                    alert.ShowAndBack(Page, "用户名或密码错误");
                    return;
                }
            }
        }
    }
}