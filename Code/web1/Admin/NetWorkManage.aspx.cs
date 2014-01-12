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
    public partial class NetWorkManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                bindProvince();
                string id = pg.request("id");
                string ProvinceId = pg.request("ProvinceId");
                ddlProvince.SelectedValue = ProvinceId;
                string lbid = "24";
                string act = pg.request("act");
                if (act == "del")
                {
                    SQLHelper db = new SQLHelper();
                    db.sql = "DELETE FROM " + com.tablePrefix + "News WHERE NewsID=" + id;
                    db.ExecSql();
                    alert.showAndGo("删除成功", "NetworkManage.aspx?ProvinceId=" + ProvinceId);

                    return;
                }
                bindGv();
            }
        }

        public void bindGv()
        {
            string provid = ddlProvince.SelectedValue;

            string sql = @"SELECT c.id AS ProvinceId, c.Name as provinceName,b.Name as cityName, a.* FROM "+com.tablePrefix+@"News a
LEFT JOIN City b ON a.cityid=b.id
LEFT JOIN Province c ON c.id=b.Pid
 WHERE 1=1 AND lbid=24 ";
            if (provid.Length > 0)
            {
                sql += " AND c.id="+provid;
            }
            sql += " ORDER BY OrderId DESC";
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

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGv();
        }
        protected void bindProvince()
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT id,name FROM " + com.tablePrefix + "Province ORDER BY OrderID";
            DataTable dt = db.Get_DataTable();
            ddlProvince.DataSource = dt;
            ddlProvince.DataValueField = "id";
            ddlProvince.DataTextField = "name";
            ddlProvince.DataBind();
        }
    }
}