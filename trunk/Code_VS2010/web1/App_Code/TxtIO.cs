using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web;
namespace web
{
    public static class TxtIO
    {
        public static string readFile(string fileName)
        {
            string realFileName = HttpContext.Current.Server.MapPath(fileName);
            StreamReader sr = File.OpenText(realFileName);
            string val = sr.ReadToEnd() ;
            sr.Close();
            sr.Dispose();
            return val;
        }

        public static bool writeFile(string fileName, string val)
        {
            try
            {
                string realFileName = HttpContext.Current.Server.MapPath(fileName);
                if (!File.Exists(realFileName))
                    File.CreateText(realFileName);
                FileStream fs = new FileStream(realFileName, FileMode.Create);
                //获得字节数组
                byte[] data = new UTF8Encoding().GetBytes(val);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }


    }　　

}
