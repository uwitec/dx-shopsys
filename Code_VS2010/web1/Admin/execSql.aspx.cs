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
    public partial class execSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {

            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            string sql = tbxSql.Text.Trim().ToUpper(); 
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            if (sql.Contains("SELECT "))
            {
                DataTable dt = db.Get_DataTable();
                gv.DataSource = dt;
                gv.DataBind();
                lblMsg.Text = "执行完成，查询结果如下：";
            }
            else
            {
                string result = db.ExecSql();
                if (result == "1")
                {
                    lblMsg.Text = "执行完成";
                }
                else
                {
                    lblMsg.Text = "执行失败，" + result;
                }
            }
            
        }
    }
}