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
    public partial class NewsPicManage : System.Web.UI.Page
    {
        public string lbid, lbname;

        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string lbid = pg.request("lbid");//新闻.NewsID
                string realLbid = "";//lb.lbid
                if (lbid == "")
                {
                    SQLHelper db = new SQLHelper();
                    db.sql = "SELECT TOP 1 lbid,NewsID,Title FROM " + com.tablePrefix + "News WHERE lbid=2";
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        lbid = dt.Rows[0]["NewsID"].ToString();//图片集管理
                        lbname = dt.Rows[0]["Title"].ToString();
                        realLbid = dt.Rows[0]["lbid"].ToString();

                    }
                }
                else
                {
                    SQLHelper db = new SQLHelper();
                    db.sql = "SELECT TOP 1 lbid,NewsID,Title FROM " + com.tablePrefix + "News WHERE NewsID=" + lbid;
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        lbname = dt.Rows[0]["Title"].ToString();
                        realLbid = dt.Rows[0]["lbid"].ToString();
                    }
                }
                lblLbname.Text = lbname;
                hlParentLbname.Text = clsLB.getLbname(realLbid);
                hlParentLbname.NavigateUrl = "News.aspx?lbid=" + realLbid;
                hlbid.Value = lbid;
                bindGv();
            }
        }
        public DataTable getNewsDt()
        {
            lbid = hlbid.Value;
            SQLHelper db = new SQLHelper();
            string pagesize = Pager0.PageSize.ToString();
            string curpage = pg.request(Pager0.PagePara);
            string pageCount = "";//总页数
            int rowCount = 0;
            if (pagesize == "") pagesize = "10";
            if (curpage == "") curpage = "1";
            string sql = @"SELECT *
FROM "+com.tablePrefix+@"News 
WHERE ParentNewsID=" + lbid;
            sql += " ORDER BY isTop DESC,IsIndex DESC,AddTime DESC";
            sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
            db.sql = sql;
            DataSet ds = db.Get_DataSet();
            DataTable dt = ds.Tables[2];
            pageCount = ds.Tables[1].Rows[0]["COUNTPAGE"].ToString();
            rowCount = Int32.Parse(ds.Tables[1].Rows[0]["ROWCOUNT"].ToString());
            Pager0.RecordCount = rowCount;
            if (rowCount == 0) Pager0.Visible = false;
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
                FileUpload fu = (FileUpload)gvr.FindControl("FileUpload1");//原图
                string pic = "";
                //上传图片
                pic = UpFile(fu);
                string picSmall = "";
                if (pic != "")
                {
                    picSmall = pic.ToLower().Replace(".jpg", "_s.jpg");
                    SQLHelper db = new SQLHelper();

                    //删除原图
                    db.sql = "SELECT pic,picSmall FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
                    DataTable dtp = db.Get_DataTable();
                    if (dtp.Rows.Count > 0)
                    {
                        string pic0 = dtp.Rows[0][0].ToString();
                        string pic1 = dtp.Rows[0][1].ToString();
                        if (pic0.Length > 0)
                            FileSys.delFile(pic0);
                        if (pic1.Length > 0)
                            FileSys.delFile(pic1);
                    }
                    //更新数据库
                    db.sql = "UPDATE News SET pic='" + pic + "',picSmall='" + picSmall + "' WHERE NewsID=" + NewsID;
                    db.ExecSql();

                    bindGv();
                    alert.Show(Page, "图片更新成功");

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
                    alert.ShowAndBack(Page, "未找到标题控件");
                    return;
                }
                
                FileUpload fu = gvr.Cells[3].FindControl("FileUpload1") as FileUpload;

                string title = pg.GetSafeString(txtTitle.Text.Trim());
                if (title == "")
                {
                    alert.ShowAndBack(Page, "请填写标题");
                    return;
                }
                string pic = UpFile(fu);
                string picSmall = "";
                if (pic != "") picSmall = pic.ToLower().Replace(".jpg", "_s.jpg");
                SQLHelper db = new SQLHelper();
                if (NewsID.Length > 0)
                {
                    //取出旧图
                    string oldPic = "", oldPicSmall = "";
                    db.sql = "SELECT pic,picSmall FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        oldPic = dt.Rows[0]["pic"].ToString();
                        oldPicSmall = dt.Rows[0]["picSmall"].ToString();
                    }
                    //更新
                    string sql = "UPDATE News SET title='" + title + "',editTime=getdate()";
                    if (pic.Length > 0)
                    {
                        sql += " ,pic='" + pic + "'";
                        FileSys.delFile(oldPic);
                    }
                    if (picSmall.Length > 0)
                    {
                        sql += " ,picSmall='" + picSmall + "'";
                        FileSys.delFile(oldPicSmall);
                    }
                    sql += " WHERE NewsID=" + NewsID;
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
                    NewsID = clsNews.MaxNewsid();

                    db.sql = "INSERT INTO News(ParentNewsID,NewsID,title,pic,picSmall,EditTime,AddTime) VALUES(" + lbid + "," + NewsID + ",'" + title + "','" + pic + "','" + picSmall + "',getdate(),getdate())";
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

                        //生成缩略图
                        ThumNail.MakeThumNail(Server.MapPath("~/UpFile/" + path + "/") + filename + "." + fileExt, Server.MapPath("~/UpFile/" + path + "/") + filename + "_s." + fileExt, 43, 30, "Cut");

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
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string newsID = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            //删除图片
            db.sql = "SELECT pic FROM " + com.tablePrefix + "News WHERE NewsID=" + newsID;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                string pic = dt.Rows[0]["pic"].ToString().ToLower();
                string picSmall = pic.Replace(".jpg", "_s.jpg");
                FileSys.delFile(pic);
                FileSys.delFile(picSmall);
            }
            db.sql = "DELETE News WHERE NewsID=" + newsID;
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