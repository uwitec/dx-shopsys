﻿using System;
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
    public partial class ProLb : System.Web.UI.Page
    {
        //public string lbid, lbname;

        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                bindDdlLbid();
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

        public void bindDdlLbid()
        {
            ddlLbid.Items.Clear();
            //ddlLbid.Items.Add(new ListItem("请选择类别", ""));
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE parentid=4 ORDER BY OrderId";
            DataTable dt = db.Get_DataTable();
            ddlLbid.DataSource = dt;
            ddlLbid.DataValueField = "lbid";
            ddlLbid.DataTextField = "lbname";
            ddlLbid.DataBind();
            ddlLbid.SelectedIndex = 0;
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
                string sql = "SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE 1=1 ";
                if (ddlLbid.SelectedValue.Length > 0)
                {
                    sql += " AND parentid=" + ddlLbid.SelectedValue;
                }

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
                    txtTitle = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到类别名称文本框控件");
                    return;
                }
                
                string title = pg.GetSafeString(txtTitle.Text.Trim());
                if (title == "")
                {
                    alert.ShowAndBack(Page, "请填写类别名称");
                    return;
                }

                SQLHelper db = new SQLHelper();
                if (lbid.Length > 0)
                {
                    //更新
                    string sql = "UPDATE lb SET lbname='" + title + "'";
                    sql += " WHERE lbid=" + lbid;
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
                    db.sql = "INSERT INTO lb(lbid,lbname,parentid) VALUES(" + clsLB.MaxLbid() + ",'" + title + "'," + ddlLbid.SelectedValue + ")";
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
            db.sql = "DELETE lb WHERE lbid=" + lbid;
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

        protected void ddlLbid_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGv();
        }

    }
}