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
    public partial class AiMianXingDongMng : System.Web.UI.Page
    {
        public string lbid, lbname;
        protected void Page_Load(object sender, EventArgs e)
        {
            //com.adminLogin();

            if (!IsPostBack)
            {
                string lbid = pg.request("lbid");
                if (lbid == "")
                {
                    lbid = "8";//爱棉行动
                }
                lbname = clsLB.getLbname(lbid);
                lblLbname.Text = lbname;
                hlbid.Value = lbid;
                bindGv();
            }
        }

        public DataTable getNewsDt()
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
                    alert.Show(Page, "未找到标题控件");
                    return;
                }



                FileUpload fu = gvr.Cells[4].FindControl("FileUpload1") as FileUpload;
                TextBox txtHref = null;
                try
                {
                    txtHref = this.GV.Rows[index].Cells[5].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到链接地址控件");
                    return;
                }                


                string title = pg.GetSafeString(txtTitle.Text.Trim());
                string EditTime = pg.GetSafeString(txtEditTime.Text.Trim());
                string href = pg.GetSafeString(txtHref.Text.Trim());
                if (title == "")
                {
                    alert.Show(Page, "请填写标题");
                    return;
                }
                if (href == "")
                {
                    alert.Show(Page, "请填写链接地址");
                    return;
                }


                if (EditTime == "")
                {
                    EditTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                }
                string pic = UpFile(fu);
                SQLHelper db = new SQLHelper();
                if (NewsID.Length > 0)
                {
                    //更新
                    if (pic != "")
                    {
                        db.sql = "UPDATE News SET title='" + title + "',pic='" + pic + "',editTime='" + EditTime + "',href='" + href + "' WHERE NewsID=" + NewsID;
                    }
                    else
                    {
                        db.sql = "UPDATE News SET title='" + title + "',editTime='" + EditTime + "',href='" + href + "' WHERE NewsID=" + NewsID;
                    }


                    
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
                    db.sql = "INSERT INTO News(lbid,NewsID,title,pic,EditTime,AddTime,href) VALUES(" + lbid + "," + NewsID + ",'" + title + "','" + pic + "','" + EditTime + "',getdate(),'" + href + "')";
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
            TextBox txtEditTime = null;
            try
            {
                txtEditTime = this.GV.Rows[GV.EditIndex].Cells[2].Controls[0] as TextBox;
                if (txtEditTime.Text.Length == 0)
                {
                    txtEditTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                }
            }
            catch
            {
                alert.Show(Page, "未找到更新时间控件");
                return;
            }
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string newsID = GV.DataKeys[e.RowIndex].Value.ToString();
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT pic FROM " + com.tablePrefix + "News WHERE NewsID=" + newsID;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                FileSys.delFile(dt.Rows[0]["pic"].ToString());
            }
            db.sql = "DELETE News WHERE NewsID="+newsID;
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

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            GV.EditIndex = GV.Rows.Count;
            bindGv(true);
            TextBox txtEditTime = null;
            try
            {
                txtEditTime = this.GV.Rows[GV.EditIndex].Cells[2].Controls[0] as TextBox;
                if (txtEditTime.Text.Length == 0)
                {
                    txtEditTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                }
                //alert.Show(Page, txtEditTime.Text);
            }
            catch
            {
                alert.Show(Page, "未找到更新时间控件");
                return;
            }
        }

    }
}