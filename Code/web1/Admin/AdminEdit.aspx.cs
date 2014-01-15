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
using DBFramework.Entities;
using DBFramework;
using System.Collections.Generic;


namespace web.admin
{
    public partial class AdminEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                init();
            }
        }

        public void init()
        {
            string id = pg.request("id");
            if (id == "")
            {
                alert.ShowAndBack(this.Page, "参数错误\n您未选择要修改的用户。");
                Response.End();
            }
            List<DXAdmin> adms = SQLHelper.GetEntities<DXAdmin>(" Name='" + Session["AdminName"].ToString() + "'");
            string roles = "";
            if (adms.Count > 0)
            {
                DXAdmin adm = adms[0];
                txtUsername.Text = adm.Name;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write(""+RoleList.SelectedValue);
            string userRole = "";
            string username = txtUsername.Text;
            string pwd = txtPwd.Text;
            if (pwd.Length > 0)
            {
                pwd = com.MD5(pwd, 1);
            }
            string result = clsAdmin.adminEdit(hid.Value, pwd, userRole);
            if (result == "1")
            {
                alert.showAndGo("修改成功", "admin.aspx");
            }
            else
            {
                alert.ShowAndBack(Page, "修改出错，请与开发人员联系，错误信息：" + result);
            }
        }
    }
}
