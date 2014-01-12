using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using web;
using UcfarPagerControls;
namespace web1.Admin
{
    public partial class CityManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {
                string pid = pg.request("pid");
                if (pid == "") Response.Redirect("ProvinceManage.aspx");
                hpid.Value = pid;
                string pname = pg.request("p");
                lblProvinceName.Text = pname;
                bindGv();
            }
        }

        public DataTable getNewsDt()
        {
            SQLHelper db = new SQLHelper();
            try
            {
                string pagesize = Pager1.PageSize.ToString();
                string curpage = pg.request(Pager1.PagePara);
                string pageCount = "";//总页数
                int rowCount = 0;
                if (pagesize == "") pagesize = "10";
                if (curpage == "") curpage = "1";
                string sql = "SELECT id,name,OrderId FROM " + com.tablePrefix + "City WHERE pid= " + hpid.Value;

                sql += " ORDER BY OrderId";
                sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
                db.sql = sql;
                DataSet ds = db.Get_DataSet();
                DataTable dt = ds.Tables[2];
                pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
                rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
                Pager1.RecordCount = rowCount;
                //if (rowCount == 0) Pager1.Visible = false;
                return dt;
            }
            catch (NullReferenceException ex)
            {
                Response.Write("2. " + ex.Message + "。");
                //Response.End();
                return new DataTable();
            }

        }
        public void bindGv()
        {
            DataTable dt = getNewsDt();
            GV.DataSource = dt.DefaultView;
            GV.DataBind();
        }
        public void bindGv(bool addNewRow)
        {
            DataTable dt = getNewsDt();
            if (addNewRow)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            GV.DataSource = dt.DefaultView;
            GV.DataBind();
        }

        protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
            if (btn == null) return;
            int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;

            DataKey key = this.GV.DataKeys[index];
            string lbid = key.Value.ToString();

            GridViewRow gvr = GV.Rows[index];
            if (e.CommandName == "Save")
            {
                //查找 title控件
                TextBox txtTitle = null;
                try
                {
                    txtTitle = this.GV.Rows[index].Cells[1].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到城市名称文本框控件");
                    return;
                }

                TextBox tbxOrderId = null;
                try
                {
                    tbxOrderId = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到排序文本框控件");
                    return;
                }


                string title = pg.GetSafeString(txtTitle.Text.Trim());
                string orderid = pg.GetSafeString(tbxOrderId.Text.Trim());
                if (title == "")
                {
                    alert.ShowAndBack(Page, "请填写城市名称");
                    return;
                }
                try
                {
                    int o = Int32.Parse(orderid);
                }
                catch (Exception)
                {
                    orderid = "0";
                }
                SQLHelper db = new SQLHelper();
                if (lbid.Length > 0)
                {
                    //更新
                    string sql = "UPDATE City SET name='" + title + "',OrderId=" + orderid;
                    sql += " WHERE id=" + lbid;
                    db.sql = sql;
                    if (db.ExecSql() != "1")
                    {
                        alert.Show(Page, "保存失败");
                    }
                    else
                    {
                        GV.EditIndex = -1;
                        bindGv();
                    }
                }
                else
                {
                    //添加
                    db.sql = "INSERT INTO City(name,OrderId,pid) VALUES('" + title + "'," + orderid + ","+hpid.Value+")";
                    if (db.ExecSql() != "1")
                    {
                        alert.Show(Page, "添加失败");
                    }
                    else
                    {
                        GV.EditIndex = -1;
                        bindGv();
                    }
                }
            }

        }

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV.EditIndex = e.NewEditIndex;
            bindGv();
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string lbid = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            db.sql = "DELETE City WHERE id=" + lbid;
            db.ExecSql();
            bindGv();
            alert.Show(Page, "删除成功");
            return;
        }

        protected void GV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV.EditIndex = -1;
            bindGv();
        }

        protected void btnAddXiangCe_Click(object sender, EventArgs e)
        {
            GV.EditIndex = GV.Rows.Count;
            bindGv(true);
        }

        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV.PageIndex = e.NewPageIndex;
            bindGv();
        }

        protected void btnAddPic_Click(object sender, EventArgs e)
        {
            GV.EditIndex = GV.Rows.Count;
            bindGv(true);
        }

    }
}