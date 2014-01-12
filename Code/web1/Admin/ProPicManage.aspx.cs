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
    public partial class ProPicManage : System.Web.UI.Page
    {
        public string lbid, lbname, productid,productName;

        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string productid = pg.request("productid");//.NewsID
                string realLbid = "";//lb.lbid
                if (productid == "")
                {
                    alert.ShowAndBack(Page, "productid未传递");
                    return;
                }
                else
                {
                    SQLHelper db = new SQLHelper();
                    db.sql = "SELECT TOP 1 lbid,NewsID,Title FROM " + com.tablePrefix + "News WHERE NewsID=" + productid;
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        productName = dt.Rows[0]["Title"].ToString();
                        realLbid = dt.Rows[0]["lbid"].ToString();
                    }
                }
                lblLbname.Text = productName;
                string pid = clsLB.getPid(realLbid);
                hlParentLbname.Text = clsLB.getLbname(pid);
                hlParentLbname.NavigateUrl = "Prod.aspx?pid=" + pid;
                hlbid.Value = productid;
                bindGv();
            }
        }
        public DataTable getNewsDt()
        {
            productid = hlbid.Value;
            SQLHelper db = new SQLHelper();
            string pagesize = Pager0.PageSize.ToString();
            string curpage = pg.request(Pager0.PagePara);
            string pageCount = "";//总页数
            int rowCount = 0;
            if (pagesize == "") pagesize = "20";
            if (curpage == "") curpage = "1";
            string sql = @"SELECT c.imgTypeName,b.ColorValue,b.ColorName
,SUBSTRING((''000''+CONVERT(varchar,b.OrderId)),LEN(''000''+CONVERT(varchar,b.OrderId))-2,3)+''-''+b.ColorName AS ColorText
,a.*
FROM "+com.tablePrefix+@"News a LEFT JOIN "+com.tablePrefix+@"Color b ON a.colorId=b.id
LEFT JOIN "+com.tablePrefix+@"ProductImgType c ON c.id =a.pro_imgTypeid
WHERE a.ParentNewsID=" + productid;
            sql += " ORDER BY a.OrderId";
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
            productid = hlbid.Value;
            System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
            if (btn == null) return;
            int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;

            DataKey key = this.GV.DataKeys[index];
            string NewsID = key.Value.ToString();

            GridViewRow gvr = GV.Rows[index];
            

            #region 单击上传图片按钮
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
                    //ThumNail.MakeThumNail(pic, picSmall, 127, 127, "HW");
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
            #endregion
            
            #region 保存
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
                string colorid="-1";
                
                try
                {
                    DropDownList ddlColor = (DropDownList)GV.Rows[index].FindControl("ddlColor");
                    colorid = ddlColor.SelectedValue;
                }
                catch (Exception)
                {
                    alert.ShowAndBack(Page, "未找到颜色控件");
                    return;
                }

                string imgTypeid = "";
                try
                {
                    DropDownList ddImglType = (DropDownList)GV.Rows[index].FindControl("ddlpro_imgTypeid");
                    imgTypeid = ddImglType.SelectedValue;
                }
                catch (Exception)
                {
                    alert.ShowAndBack(Page, "未找到图片类型控件");
                    return;
                }
                FileUpload fu = gvr.Cells[5].FindControl("FileUpload1") as FileUpload;

                string title = pg.GetSafeString(txtTitle.Text.Trim());
                if (title == "")
                {
                    alert.ShowAndBack(Page, "请填写标题");
                    return;
                }

                TextBox tbxOrder = null;
                try
                {
                    tbxOrder = this.GV.Rows[index].Cells[6].Controls[0] as TextBox;
                }
                catch
                {
                    alert.ShowAndBack(Page, "未找到排序文本框控件");
                    return;
                }
                string orderid = pg.GetSafeString(tbxOrder.Text.Trim());
                if (orderid == "")
                {
                    orderid = "0";
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
                    string sql = "UPDATE News SET title='" + title + "',ColorId=" + colorid + ",pro_imgTypeid=" + imgTypeid + ",editTime=getdate(),OrderId=" + orderid;
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

                    db.sql = "INSERT INTO News(ParentNewsID,NewsID,title,ColorId,pro_imgTypeid,pic,picSmall,EditTime,AddTime,OrderId) VALUES(" + productid + "," + NewsID + ",'" + title + "'," + colorid + "," + imgTypeid + ",'" + pic + "','" + picSmall + "',getdate(),getdate()," + orderid + ")";
                    if (db.ExecSql() != "1")
                    {
                        Response.Write("添加失败,sql="+db.sql);
                        //alert.Show(Page, "添加失败");
                        Response.End();
                    }
                    else
                    {
                        GV.EditIndex = -1;
                        bindGv();
                    }
                }
            }
            #endregion
            

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
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UpFile/" + path + "/") + filename + "_bak." + fileExt);
                        //将大图缩小，以免太大影响打开速度
                        ThumNail.MakeThumNail(Server.MapPath("~/UpFile/" + path + "/") + filename + "_bak." + fileExt, Server.MapPath("~/UpFile/" + path + "/") + filename + "." + fileExt, 600, 600, "HW2");
                        File.Delete(Server.MapPath("~/UpFile/" + path + "/") + filename + "_bak." + fileExt);
                        //生成缩略图
                        ThumNail.MakeThumNail(Server.MapPath("~/UpFile/" + path + "/") + filename + "." + fileExt, Server.MapPath("~/UpFile/" + path + "/") + filename + "_s." + fileExt, 300, 300, "HW2");

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

        protected void GV_RowCreated(object sender, GridViewRowEventArgs e)
        {
            productid = hlbid.Value;

            GridView g = (GridView)sender;
            if (g.Rows.Count == 0)
            {
            return;
            }
            int index = g.Rows.Count - 1;
            DataKey key = this.GV.DataKeys[index];
            string NewsID = key.Value.ToString();
            GridViewRow gvr = GV.Rows[index];
            #region ddlColor
            DropDownList ddl = (DropDownList)g.Rows[index].FindControl("ddlColor");
            if (ddl != null)
            {
                SQLHelper db = new SQLHelper();
                db.sql = "SELECT id,ColorName,ColorValue,OrderId,SUBSTRING(('000'+CONVERT(varchar,OrderId)),LEN('000'+CONVERT(varchar,OrderId))-3,4)+' '+ColorName AS ColorText FROM " + com.tablePrefix + "Color ORDER BY OrderId";
                DataTable dt = db.Get_DataTable();
                ddl.DataSource = dt;
                ddl.DataValueField = "id";
                ddl.DataTextField = "ColorText";
                ddl.DataBind();
                if (NewsID.Length > 0)
                {
                    db.sql = "SELECT ColorId FROM " + com.tablePrefix + "News WHERE NewsId=" + NewsID;
                    DataTable dtc = db.Get_DataTable();
                    if (dtc.Rows.Count > 0)
                    {
                        ddl.SelectedValue = dtc.Rows[0][0].ToString();
                    }
                }
            }
            #endregion
            
            DropDownList ddl_imgtid = (DropDownList)g.Rows[index].FindControl("ddlpro_imgTypeid");
            if (ddl_imgtid != null)
            {
                SQLHelper db = new SQLHelper();
                db.sql = "SELECT id,ImgTypeName FROM " + com.tablePrefix + "ProductImgType ORDER BY OrderId";
                DataTable dt = db.Get_DataTable();
                ddl_imgtid.DataSource = dt;
                ddl_imgtid.DataValueField = "id";
                ddl_imgtid.DataTextField = "ImgTypeName";
                ddl_imgtid.DataBind();
                if (NewsID.Length > 0)
                {
                    db.sql = "SELECT pro_imgTypeid FROM " + com.tablePrefix + "News WHERE NewsId=" + NewsID;
                    DataTable dti = db.Get_DataTable();
                    if (dti.Rows.Count > 0)
                    {
                        ddl_imgtid.SelectedValue = dti.Rows[0][0].ToString();
                    }
                }
            }
            
        }
    }
}