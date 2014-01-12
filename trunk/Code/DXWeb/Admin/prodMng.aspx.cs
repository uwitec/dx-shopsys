using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBFramework;
using DBFramework.Entities;
namespace DXWeb.Admin
{
    public partial class prodMng : System.Web.UI.Page
    {
        private List<dxProd> proList;
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLHelper.Setup(Const.connstr);
            proList = SQLHelper.GetEntities<dxProd>();
            rpt.DataSource = proList;
            rpt.DataBind();
        }
    }
}