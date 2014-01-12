using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web;
namespace web1.Admin
{
    public partial class Header : System.Web.UI.Page
    {
        public string uname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            //if (Session["AdminName"] != null)
            //{
            //    uname = Session["AdminName"].ToString();
            //}
        }
    }
}