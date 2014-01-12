using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Products
{
    public partial class ProList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lbid = Page.RouteData.Values["lbid"] as string;//接收路由参数
            lblLbid.Text = "lbid="+lbid;
        }
    }
}