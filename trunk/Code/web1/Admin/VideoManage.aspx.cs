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
    public partial class VideoManage : System.Web.UI.Page
    {
        public string lbid, lbname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string lbid = pg.request("lbid");
                if (lbid == "")
                {
                    lbid = "11";//Mian影像
                }

                string act = pg.request("act");
                if (act == "top")
                {
                    //置顶
                    db.ExecSql("UPDATE News SET isTop=0 WHERE lbid=" + lbid);
                    db.ExecSql("UPDATE News SET isTop=1 WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "VideoManage.aspx?lbid=" + lbid);
                    return;
                }

                if (act == "IsIndex")
                {
                    //置顶
                    db.ExecSql("UPDATE News SET IsIndex=" + pg.request("value") + " WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "VideoManage.aspx?lbid=" + lbid);
                    return;
                }

                lbname = clsLB.getLbname(lbid);
                lblLbname.Text = lbname;
                hlbid.Value = lbid;
                bindGv();
            }
        }

        public void bindGv()
        {
            lbid = hlbid.Value;
            SQLHelper db = new SQLHelper();
            string pagesize = Pager2.PageSize.ToString();
            string curpage = pg.request(Pager2.PagePara);
            string pageCount = "";//总页数
            int rowCount = 0;
            if (pagesize == "") pagesize = "5";
            if (curpage == "") curpage = "1";
            string sql = @"SELECT *
FROM "+com.tablePrefix+@"News 
WHERE lbid=" + lbid;
            sql += " ORDER BY isTop DESC,IsIndex DESC,AddTime DESC";
            sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
            db.sql = sql;
            DataSet ds = db.Get_DataSet();
            DataTable dt = ds.Tables[2];
            pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
            rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
            Pager2.RecordCount = rowCount;
            if (rowCount == 0) Pager2.Visible = false;
            GV.DataSource = dt.DefaultView;
            GV.DataBind();

        }
        public void bindGv(bool addNewRow)
        {
            lbid = hlbid.Value;
            SQLHelper db = new SQLHelper();
            string pagesize = Pager2.PageSize.ToString();
            string curpage = pg.request(Pager2.PagePara);
            string pageCount = "";//总页数
            int rowCount = 0;
            if (pagesize == "") pagesize = "5";
            if (curpage == "") curpage = "1";
            string sql = @"SELECT *
FROM "+com.tablePrefix+@"News 
WHERE lbid=" + lbid;
            sql += " ORDER BY isTop DESC,IsIndex DESC,EditTime DESC";
            sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
            db.sql = sql;
            DataSet ds = db.Get_DataSet();
            DataTable dt = ds.Tables[2];
            pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
            rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
            Pager2.RecordCount = rowCount;
            if (rowCount == 0) Pager2.Visible = false;
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
            lbid = hlbid.Value;
            System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
            if (btn == null) return;
            int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;

            DataKey key = this.GV.DataKeys[index];
            string NewsID = key.Value.ToString();

            GridViewRow gvr = GV.Rows[index];
            if (e.CommandName == "upFile")
            {
                //寻找上传控件
                FileUpload fu = (FileUpload)gvr.FindControl("FileUpload1");
                if (fu != null)
                {
                    if (fu.HasFile)
                    {
                        SQLHelper db = new SQLHelper();
                        //上传图片
                        string pic = UpFile(fu);
                        //删除原图
                        db.sql = "SELECT pic FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
                        string pic0 = db.Get_DataTable().Rows[0][0].ToString();
                        if (pic0.Length > 0)
                            FileSys.delFile(pic0);

                        //更新数据库
                        db.sql = "UPDATE News SET pic='" + pic + "' WHERE NewsID=" + NewsID;
                        db.ExecSql();

                        bindGv();
                        alert.Show(Page, "图片更新成功");
                    }
                }
            }
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
                    alert.Show(Page, "未找到标题控件");
                    return;
                }
                TextBox txtEditTime = null;
                try
                {
                    txtEditTime = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到日期控件");
                    return;
                }
                TextBox txtVideoUrl = null;
                try
                {
                    txtVideoUrl = this.GV.Rows[index].Cells[5].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到视频路径控件");
                    return;
                }

                TextBox txtDescription = null;
                try
                {
                    txtDescription = this.GV.Rows[index].Cells[6].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到视频描述控件");
                    return;
                }


                FileUpload fu = gvr.Cells[4].FindControl("FileUpload1") as FileUpload;

                string title = pg.GetSafeString(txtTitle.Text.Trim());
                string EditTime = pg.GetSafeString(txtEditTime.Text.Trim());
                string VideoUrl = pg.GetSafeString(txtVideoUrl.Text.Trim());
                string Description = pg.GetSafeString(txtDescription.Text.Trim());
                if (title == "")
                {
                    //alert.ShowAndBack(Page, "请填写标题");
                    alert.Show(Page,"请填写标题");
                    return;
                }
                if (EditTime == "")
                {
                    EditTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                }
                if (VideoUrl == "")
                {
                    alert.Show(Page, "请填写视频路径");
                    return;
                }
                if (Description.Length > 500)
                {
                    alert.Show(Page, "视频简介不能超过500字");
                    return;
                }
                string pic = UpFile(fu);
                SQLHelper db = new SQLHelper();
                if (NewsID.Length > 0)
                {
                    //更新
                    if (pic.Length > 0)
                    {
                        db.sql = "UPDATE News SET title='" + title + "',pic='" + pic + "',editTime='" + EditTime + "',VideoUrl='" + VideoUrl + "',Description='" + Description + "' WHERE NewsID=" + NewsID;
                    }
                    else
                    {
                        db.sql = "UPDATE News SET title='" + title + "',editTime='" + EditTime + "',VideoUrl='" + VideoUrl + "',Description='" + Description + "' WHERE NewsID=" + NewsID;
                    }
                    
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
                    }
                }
                else
                {
                    //添加
                    NewsID = clsNews.MaxNewsid();
                    db.sql = "INSERT INTO News(lbid,NewsID,title,pic,EditTime,AddTime,VideoUrl,Description) VALUES(" + lbid + "," + NewsID + ",'" + title + "','" + pic + "','" + EditTime + "',getdate(),'" + VideoUrl + "','" + Description + "')";
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

        /// <summary>
        /// 返回上传成功后的文件名，如果上传失败则返回""
        /// </summary>
        /// <param name="fu">FileUpload控件ID</param>
        /// <returns></returns>
        protected string UpFile(FileUpload FileUpload1)
        {
            string fileFullName = "";
            if (FileUpload1.HasFile)
            {
                //判断文件是否小于2Mb   
                if (FileUpload1.PostedFile.ContentLength < 1024 * 1024 * 2)
                {
                    try
                    {
                        string filename = DateTime.Now.ToString("yyyyMMddhhmmssffff");
                        string path = DateTime.Now.ToString("yyyyMM");

                        if (!Directory.Exists(Server.MapPath("~/UpFile/" + path + "/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/UpFile/" + path + "/"));
                        }
                        string fileExt = FileUpload1.FileName.Split('.')[1].ToString().ToLower();

                        //上传文件并指定上传目录的路径   
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UpFile/" + path + "/") + filename + "." + fileExt);
                        /*注意->这里为什么不是:FileUpLoad1.PostedFile.FileName  
                        * 而是:FileUpLoad1.FileName?  
                        * 前者是获得客户端完整限定(客户端完整路径)名称  
                        * 后者FileUpLoad1.FileName只获得文件名.  
                        */

                        //当然上传语句也可以这样写(貌似废话):   
                        //FileUpLoad1.SaveAs(@"D:\"+FileUpLoad1.FileName);

                        filename = filename + "." + fileExt;
                        fileFullName = "/UpFile/" + path + "/" + filename;

                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = "出现异常,上传图片失败!" + ex.Message;
                        //lblMessage.Text += ex.Message;   
                    }

                }
                else
                {
                    //lblMessage.Text = "上传文件不能大于2MB!";

                }

            }
            return fileFullName;
        }

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV.EditIndex = e.NewEditIndex;
            bindGv();
            TextBox txtDescription = null;
            try
            {
                txtDescription = this.GV.Rows[GV.EditIndex].Cells[6].Controls[0] as TextBox;
                txtDescription.MaxLength = 500;
                txtDescription.TextMode = TextBoxMode.MultiLine;
                txtDescription.Width = 200;
                txtDescription.Height = 100;
            }
            catch
            {
                alert.ShowAndBack(Page, "未找到视频简介控件");
                return;
            }
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string newsID = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            db.sql = "DELETE News WHERE NewsID=" + newsID;
            db.ExecSql();
            bindGv();
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

        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV.PageIndex = e.NewPageIndex;
            bindGv();
        }

        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
           GV.EditIndex = GV.Rows.Count;
           bindGv(true);
           TextBox txtEditTime = null;
            try
            {
                txtEditTime = this.GV.Rows[GV.EditIndex].Cells[2].Controls[0] as TextBox;
                txtEditTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            catch
            {
                alert.ShowAndBack(Page, "未找到日期控件");
                return;
            }

            TextBox txtDescription = null;
            try
            {
                txtDescription = this.GV.Rows[GV.EditIndex].Cells[6].Controls[0] as TextBox;
                txtDescription.MaxLength = 500;
                txtDescription.TextMode = TextBoxMode.MultiLine;
                txtDescription.Width = 200;
                txtDescription.Height = 100;
            }
            catch
            {
                alert.ShowAndBack(Page, "未找到视频简介控件");
                return;
            }

        }
        public string setIsIndex(string newsid)
        {
            string re = "";
            string sql = "SELECT isNull(IsIndex,0) as IsIndex,lbid FROM " + com.tablePrefix + "News WHERE newsid=" + newsid;

            DataTable dt = db.Get_DataTable(sql);
            if (dt.Rows.Count > 0)
            {
                string lbid = dt.Rows[0]["lbid"].ToString();
                if (dt.Rows[0][0].ToString() == "0")
                    re = "<a href='VideoManage.aspx?act=IsIndex&value=1&lbid=" + lbid + "&id=" + newsid + "' style='color:green;'>放到首页</a>";
                else
                    re = "<a href='VideoManage.aspx?act=IsIndex&value=0&lbid=" + lbid + "&id=" + newsid + "' style='color:red;'>取消首页</a>";
            }
            return re;
        }

    }
}