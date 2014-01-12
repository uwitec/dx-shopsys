using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using web;
using UcfarPagerControls;
namespace web1.Admin
{
    public partial class ColorManage : System.Web.UI.Page
    {
        //public string lbid, lbname;

        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {
                try
                {
                    bindGv();
                }
                catch (Exception ex)
                {
                    Response.Write("1. " + ex.Message);
                    Response.End();
                }

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
                if (pagesize == "") pagesize = "15";
                if (curpage == "") curpage = "1";
                string sql = @"SELECT id,ColorName,ColorValue,SUBSTRING((''000''+CONVERT(varchar,OrderId)),LEN(''000''+CONVERT(varchar,OrderId))-3,4) AS OrderId FROM " + com.tablePrefix + "Color ORDER BY OrderId";
                sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
                db.sql = sql;
                DataSet ds = db.Get_DataSet();
                DataTable dt = ds.Tables[2];
                pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
                rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
                Pager1.RecordCount = rowCount;
                if (rowCount == 0) Pager1.Visible = false;
                return dt;
            }
            catch (Exception ex)
            {
                Response.Write("2. " + ex.Message + "。");
                //Response.End();
                return null;
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
            string id = key.Value.ToString();

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
                    alert.Show(Page, "未找到颜色名称文本框");
                    return;
                }

                TextBox txtColorValue = null;
                try
                {
                    txtColorValue = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到色值文本框");
                    return;
                }
                //排序

                TextBox txtOrderId = null;
                try
                {
                    txtOrderId = this.GV.Rows[index].Cells[3].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到排序文本框");
                    return;
                }

                string ColorName = pg.GetSafeString(txtTitle.Text.Trim());
                string colorValue = pg.GetSafeString(txtColorValue.Text.Trim());
                string orderid = pg.GetSafeString(txtOrderId.Text.Trim());
                if (ColorName == "")
                {
                    alert.ShowAndBack(Page, "请填写类别名称");
                    return;
                }



                SQLHelper db = new SQLHelper();
                if (id.Length > 0)
                {

                    //更新
                    string sql = "UPDATE Color SET ColorName='" + ColorName + "',ColorValue='" + colorValue + "',OrderId="+orderid;

                    sql += " WHERE id=" + id;
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
                    db.sql = "INSERT INTO Color(ColorName,ColorValue,OrderId) VALUES('" + ColorName + "','" + colorValue + "'," + orderid + ")";
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
            string id = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            db.sql = "DELETE Color WHERE id=" + id;
            db.ExecSql();
            bindGv();
            alert.Show(Page, "删除成功");
            return;
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