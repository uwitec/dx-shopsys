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

namespace web1.SwfUploadDemo
{
    public partial class upfile : System.Web.UI.Page
    {
        //直接保存为文件
        protected void Page_Load(object sender, EventArgs e)
        {
            string uploadsFolder = Server.MapPath("~/UpFile");
            string fileurl = null;
            var httpfile = Request.Files["Filedata"];
            var filename = DateTime.Now.ToFileTime();
            if (httpfile != null)
            {

                httpfile.SaveAs(string.Format("{0}\\{1}.jpg", uploadsFolder, filename));
                //fileurl = "/UpFile/" + filename + ".jpg";
                fileurl = filename + ".jpg";

                Response.Write(fileurl);
                Response.End();
            }

        }
    }
}