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
    public partial class upfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string newsid = pg.request("newsid");
            string guid = pg.request("guid");
            string type = pg.request("type");
            
            string fileurl = null;
            string folder = "/UpFile/";
            string uploadsFolder = Server.MapPath(folder);
            HttpPostedFile httpfile = Request.Files["Filedata"];
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (httpfile != null)
            {
                //保存到磁盘
                httpfile.SaveAs(string.Format("{0}\\{1}.jpg", uploadsFolder, filename));
                fileurl = folder + filename + ".jpg";
                Response.Write(fileurl);

                /*
                //保存到内存  ----  start
                System.Drawing.Image original_image = null;
                System.Drawing.Bitmap final_image = null;//最终图片
                System.Drawing.Graphics graphic = null;
                MemoryStream ms = null;
                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(httpfile.InputStream);
                try
                {
                    int width = original_image.Width; //原图 宽度
                    int height = original_image.Height; //原图 高度
                    final_image = new System.Drawing.Bitmap(original_image);
                    graphic = System.Drawing.Graphics.FromImage(final_image);
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(original_image, 0, 0, width, height);

                    // Store the thumbnail in the session (Note: this is bad, it will take a lot of memory, but this is just a demo)
                    ms = new MemoryStream();
                    final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Thumbnail thumb = new Thumbnail(filename, ms.GetBuffer());
                    // Put it all in the Session (initialize the session if necessary)			
                    List<Thumbnail> thumbnails = Session["file_info"] as List<Thumbnail>;
                    if (thumbnails == null)
                    {
                        thumbnails = new List<Thumbnail>();
                        if (type == "small")
                        {
                            Session["smallFile_info"] = thumbnails;
                        }
                        else
                        {
                            Session["file_info"] = thumbnails;
                        }

                    }
                    thumbnails.Add(thumb);
                    Response.StatusCode = 200;
                    Response.Write(filename);
                    //保存到内存  ----  end
                }
                catch(Exception ex)
                {
                    // If any kind of error occurs return a 500 Internal Server error
                    Response.StatusCode = 500;
                    Response.Write("上传失败");
                    Response.End();
                }
                finally
                {
                    // Clean up
                    if (original_image != null) original_image.Dispose();
                    if (ms != null) ms.Close();
                    Response.End();
                }
                 */
            }
            Response.End();
        }
    }
}