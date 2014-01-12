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
    public partial class MemberManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {
                //lblLbname.Text = "会员管理";
                bindGv();
            }
        }
        public DataTable getNewsDt()
        {
            SQLHelper db = new SQLHelper();
            string pagesize = PagerMember.PageSize.ToString();
            string curpage = pg.request(PagerMember.PagePara);
            string pageCount = "";//总页数
            int rowCount = 0;
            if (pagesize == "") pagesize = "10";
            if (curpage == "") curpage = "1";
            string sql = @"SELECT * FROM " + com.tablePrefix + "Members ORDER BY RegTime DESC";
            sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
            db.sql = sql;
            DataSet ds = db.Get_DataSet();
            DataTable dt = ds.Tables[2];
            pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
            rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
            PagerMember.RecordCount = rowCount;
            if (rowCount == 0) PagerMember.Visible = false;
            return dt;
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
            string NewsID = key.Value.ToString();

            GridViewRow gvr = GV.Rows[index];
            
            if (e.CommandName == "Save")
            {
                TextBox txtemail = null;
                try
                {
                    txtemail = this.GV.Rows[index].Cells[1].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到邮箱控件");
                    return;
                }
                TextBox txtusername = null;
                try
                {
                    txtusername = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到昵称控件");
                    return;
                }



                string username = pg.GetSafeString(txtusername.Text.Trim());
                string email = pg.GetSafeString(txtemail.Text.Trim());
                if (email == "")
                {
                    //alert.ShowAndBack(Page, "请填写标题");
                    alert.Show(Page, "请填写邮箱");
                    return;
                }
                if (username == "")
                {
                    alert.Show(Page, "请填写昵称");
                    return;
                }
                SQLHelper db = new SQLHelper();
                if (NewsID.Length > 0)
                {
                    //更新
                    db.sql = "UPDATE Members SET email='" + email + "',username='" + username + "' WHERE userid=" + NewsID;

                    string result = db.ExecSql();
                    if (result != "1")
                    {
                        Response.Write(result + db.sql);
                        Response.End();
                        //alert.Show(Page, "保存失败");
                    }
                    else
                    {
                        GV.EditIndex = -1;
                        bindGv();
                        alert.Show(Page, "保存成功");
                    }
                }
                else
                {
                    //添加
                    NewsID = clsNews.MaxNewsid();
                    db.sql = "INSERT INTO Members(email,username,pwd,regtime) VALUES('" + email + "','" + username + "','" + com.MD5(hpwd.Value,1) + "',getdate())";
                    if (db.ExecSql() != "1")
                    {
                        alert.Show(Page, "添加失败");
                    }
                    else
                    {
                        GV.EditIndex = -1;
                        bindGv();
                        alert.Show(Page, "添加成功");
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
            string newsID = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            db.sql = "DELETE Members WHERE userid=" + newsID;
            db.ExecSql();
            bindGv();
            alert.Show(Page, "删除成功");
        }


        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV.EditIndex = -1;
            bindGv();
        }

        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV.PageIndex = e.NewPageIndex;
            bindGv();
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            GV.EditIndex = GV.Rows.Count;
            bindGv(true);
        }
        public string md5(string pwd)
        {
            return com.MD5(pwd, 1);
        }
    }
}