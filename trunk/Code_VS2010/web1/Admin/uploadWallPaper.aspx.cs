using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;
using web;

namespace web1.Admin
{
    public partial class uploadWallPaper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string newsid = pg.request("newsid"), sizeid="";
            string guid = pg.request("guid");
            string type = pg.request("type");
            string fileurl = null;
            string folder = "/UpFile/wallpaper/";
            string uploadsFolder = Server.MapPath(folder);
            HttpPostedFile httpfile = Request.Files["Filedata"];
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (httpfile != null)
            {
                //得到尺寸
                System.Drawing.Image original_image = null;//原图
                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(httpfile.InputStream);
                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;
                bool flag = false;
                //验证分辨率
                SQLHelper db = new SQLHelper();
                db.sql = "SELECT id,size FROM " + com.tablePrefix + "wallpaperSize";
                DataTable dt = db.Get_DataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!flag)
                    {
                        string[] wh = dr["Size"].ToString().Split('×');
                        if (wh.Length > 0)
                        {
                            if (width.ToString() == wh[0] && height.ToString() == wh[1])
                            {
                                flag = true;
                                sizeid = dr["id"].ToString();
                            }
                        }
                    }
                }
                if (!flag)
                {
                    Response.Write("分辨率不对");
                }
                else
                {
                    //保存到磁盘
                    httpfile.SaveAs(string.Format("{0}\\{1}.jpg", uploadsFolder, filename));
                    fileurl = folder + filename + ".jpg";
                    //将图片名保存到数据库 wallpaper.pic
                    if (newsid == "")
                    {
                        db.sql = "INSERT INTO wallpaper(sizeid,pic,NewsGUID) VALUES(" + sizeid + ",'" + fileurl + "','"+guid+"')";
                    }
                    else
                    {
                        db.sql = "INSERT INTO wallpaper(sizeid,pic,NewsGUID,NewsID) VALUES(" + sizeid + ",'" + fileurl + "','" + guid + "'," + newsid + ")";
                    }
                    db.ExecSql();
                    Response.Write(fileurl);
                }
            }
            Response.End();
        }
    }
}