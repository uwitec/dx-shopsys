using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using web;
namespace web1.Admin
{
    public partial class jiameng : System.Web.UI.Page
    {
        public string lbid, lbname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {
                string act = pg.request("act");
                if (act == "del")
                {
                    string id = pg.request("id");
                    SQLHelper db = new SQLHelper();
                    db.sql = "DELETE FROM " + com.tablePrefix + "jiameng WHERE id=" + id;
                    db.ExecSql();
                    alert.showAndGo("删除成功", "jiameng.aspx");
                    return;
                }
                bindGv();
            }
        }

        public void bindGv()
        {
            lbid = hlbid.Value;
            string sql = @"SELECT * FROM " + com.tablePrefix + "jiameng  ORDER BY AddTime DESC";
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();

            GV.DataSource = dt.DefaultView;
            GV.DataBind();

        }
        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV.PageIndex = e.NewPageIndex;
            bindGv();
        }


    }
}