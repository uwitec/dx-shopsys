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
using DBFramework;
using System.Collections.Generic;
using DBFramework.Entities;


namespace web.admin
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            
            if (pg.request("act") == "del")
            {
                try
                {
                    List<DXAdmin> ads = SQLHelper.GetEntities<DXAdmin>("WHERE id=" + pg.request("id"));
                    SQLHelper.DeleteEntity<DXAdmin>(ads[0]);
                    alert.showAndGo("删除成功","admin.aspx");
                }
                catch
                {
                    alert.ShowAndBack(Page, "删除失败");
                }
                return;

            }else{
                bindGV();
            }
            
        }
        protected void bindGV()
        {
            GV.DataSource = SQLHelper.GetEntities<DXAdmin>();
            GV.DataBind();
        }
    }
}
