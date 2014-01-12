using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using web1;
using DBFramework.Entities;
using DBFramework;
using System.Collections.Generic;


namespace web.admin
{
    public partial class AdminAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                com.adminLogin();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pwd = txtPwd.Text;
            if (pwd.Length > 0)
            {
                pwd = com.MD5(pwd + Const.md5Key, 1);
            }
            else
            {
                alert.ShowAndBack(Page, "请输入密码");
                return;
            }
            List<DXAdmin> ads = SQLHelper.GetEntities<DXAdmin>(" Name='"+username+"'");
            if (ads.Count > 0)
            {
                alert.ShowAndBack(Page, "用户" + username + "已存在");
            }
            else
            {
                try
                {
                    DXAdmin ad = new DXAdmin();
                    ad.Name = username;
                    ad.Pwd = pwd;
                    ad.IsDeleted = false;
                    SQLHelper.CreateEntity<DXAdmin>(ad);
                    alert.showAndGo("添加成功", "admin.aspx");
                }
                catch (Exception ex)
                {

                    alert.ShowAndBack(Page, "添加时出错，请与开发人员联系，错误信息：" + ex.Message);
                }
                
            }
            

        }
    }
}
