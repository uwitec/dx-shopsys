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
    public partial class IconManage : System.Web.UI.Page
    {
        public string lbid, lbname;

        protected void Page_Load(object sender, EventArgs e)
        {
            //com.adminLogin();

            if (!IsPostBack)
            {

                try
                {
                    string lbid;
                    lbid = pg.request("lbid");
                    if (lbid == "") lbid = "64";
                    hlParentLbname.Text = clsLB.getLbname(lbid);
                    hlParentLbname.NavigateUrl = "IconManage.aspx?lbid=" + lbid;
                    hlbid.Value = lbid;
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
            lbid = hlbid.Value;
            SQLHelper db = new SQLHelper();
            try
            {
                if (Pager1 == null)
                {
                    Response.Write("2.1  Pager1 == null。");
                }
                if (Pager1.PageSize == null)
                {
                    Response.Write("2.2  Pager1.PageSize == null。");
                }



                string pagesize = Pager1.PageSize.ToString();
                string curpage = pg.request(Pager1.PagePara);
                string pageCount = "";//总页数
                int rowCount = 0;
                if (pagesize == "") pagesize = "20";
                if (curpage == "") curpage = "1";
                string sql = @"SELECT NewsID,Title,Pic,Description,OrderId
FROM "+com.tablePrefix+@"News 
WHERE lbid=" + lbid;
                sql += " ORDER BY title";
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
                if (pic != "")
                {
                    SQLHelper db = new SQLHelper();
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
                    alert.Show(Page, "未找到图标名称控件");
                    return;
                }

                TextBox txtDesc = null;
                try
                {
                    txtDesc = this.GV.Rows[index].Cells[2].Controls[0] as TextBox;
                }
                catch
                {
                    alert.Show(Page, "未找到图标描述控件");
                    return;
                }

                //FileUpload fu_small = gvr.Cells[4].FindControl("FileUpload2") as FileUpload;//缩略图
                FileUpload fu = gvr.Cells[3].FindControl("FileUpload1") as FileUpload;
                string title = pg.GetSafeString(txtTitle.Text.Trim());
                string desc = pg.GetSafeString(txtDesc.Text.Trim());
                //if (title == "")
                //{
                //    alert.ShowAndBack(Page, "请填写图标名称");
                //    return;
                //}
                if (desc == "")
                {
                    alert.ShowAndBack(Page, "请填写图标描述");
                    return;
                }


                string pic = UpFile(fu);


                SQLHelper db = new SQLHelper();
                if (NewsID.Length > 0)
                {
                    //取出旧图
                    string oldPic = "";
                    db.sql = "SELECT pic FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        oldPic = dt.Rows[0]["pic"].ToString();
                    }
                    //更新
                    string sql = "UPDATE News SET title='" + title + "',Description='" + desc + "'";
                    if (pic.Length > 0)
                    {
                        sql += " ,pic='" + pic + "'";
                        FileSys.delFile(oldPic);
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
                    db.sql = "INSERT INTO News(lbid,NewsID,title,AddTime,pic,Description) VALUES(" + lbid + "," + NewsID + ",'" + title + "',getdate(),'" + pic + "','" + desc + "')";
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
                        string path = "~/images/function_icon/";

                        if (!Directory.Exists(Server.MapPath(path)))
                        {
                            Directory.CreateDirectory(Server.MapPath(path));
                        }
                        string fileExt = FileUpload1.FileName.Split('.')[1].ToString().ToLower();

                        //上传文件并指定上传目录的路径   
                        FileUpload1.PostedFile.SaveAs(Server.MapPath(path) + filename + "." + fileExt);
                        //生成缩略图
                        //ThumNail.MakeThumNail(Server.MapPath("~/UpFile/" + path + "/") + filename + "." + fileExt, Server.MapPath("~/UpFile/" + path + "/") + filename + "_s." + fileExt, 60, 60, "Cut");

                        //当然上传语句也可以这样写(貌似废话):   
                        //FileUpLoad1.SaveAs(@"D:\"+FileUpLoad1.FileName);

                        filename = filename + "." + fileExt;
                        fileFullName = path + filename;

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
            return fileFullName.Replace("~/", "/");
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
                string pic = dt.Rows[0]["pic"].ToString();
                FileSys.delFile(pic);
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