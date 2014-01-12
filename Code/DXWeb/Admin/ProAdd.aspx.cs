using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBFramework.Entities;
using DBFramework;
namespace DXWeb.Admin
{
    public partial class ProAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            dxProd prod = new dxProd();
            prod.Name = tbxTitle.Text.Trim();
            prod.Body = tbxBody.Text.Trim();
            SQLHelper.Setup(Const.connstr);
            SQLHelper.CreateEntity<dxProd>(prod);
            Response.Redirect("prodMng.aspx");
        }
    }
}