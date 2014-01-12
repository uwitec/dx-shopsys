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

namespace web1.SwfUploadDemo
{
    public partial class upload2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // System.Drawing.Image thumbnail_image = null;//缩略图
            
            System.Drawing.Image original_image = null;//原图
            System.Drawing.Bitmap final_image = null;//最终图片
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;
            try
            {
                // Get the data
                HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                int width = original_image.Width;
                int height = original_image.Height;
                final_image = new System.Drawing.Bitmap(original_image);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(original_image, 0, 0, width, height);
                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());
                List<Thumbnail> thumbnails = Session["file_info"] as List<Thumbnail>;
                if (thumbnails == null)
                {
                    thumbnails = new List<Thumbnail>();
                    Session["file_info"] = thumbnails;
                }
                thumbnails.Add(thumb);

                Response.StatusCode = 200;
                Response.Write(thumbnail_id);


            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write("An error occured");
                Response.End();
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (ms != null) ms.Close();
                Response.End();
            }
        }
    }
}