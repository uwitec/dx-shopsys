using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web;
using System.Data;
using System.IO;
using web;
namespace web1.Admin
{
    public partial class LeftMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            //if (!clsAdmin.checkLogin())
            //{
            //    //alert.showOnly("您未登录，或登录超时。");
            //    //alert.ResponseScript(Page, "alert('您未登录，或登录超时。');parent.location.href='Login.aspx';");
            //    //Response.Write("您未登录，或登录超时。");
            //    //Response.End();
            //    return;
            //}
            bindProPlb();
        }
        protected void bindProPlb()
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE parentid=4 Order By OrderId";
            DataTable dt = db.Get_DataTable();
            rptProLbParent.DataSource = dt;
            rptProLbParent.DataBind();
        }
    }
}