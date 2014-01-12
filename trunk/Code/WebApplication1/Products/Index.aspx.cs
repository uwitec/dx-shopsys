using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Products
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string lbid = "";//Request.QueryString["lbid"].ToString();
                string id = "";// Request.QueryString["id"].ToString();
                lblMsg.Text = "lbid=" + lbid + ",id=" + id;

            }
        }
    }
}