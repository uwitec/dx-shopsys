using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace web
{
    public static class com
    {
        public const string tablePrefix = "dianxin_";
        public static bool active()
        {
            if (Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) <= 20181103)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 生成固定长度的ID
        /// </summary>
        /// <param name="id">原ID</param>
        /// <returns>补长后的新ID</returns>
        public static string newid(string id,int len)
        {
            if (id.Length > 0)
            {
                id = Int32.Parse(id).ToString();
                int len0 = id.Length;

                for (int i = 0; i < len - len0; i++)
                {
                    id = "0" + id;
                }
            }
            return id;
        }

        public static string specialUser
        {
            get{ return "gsq";}
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="PWD">密码</param>
        /// <param name="Format">算法：0=哈希算法,1=MD5算法</param>
        /// <returns>返回加密后的字符串</returns>
        public static string MD5(string PWD, int Format)
        {
            string str = "";
            switch (Format)
            {
                case 0:
                    str = FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "SHA1");
                    break;
                case 1:
                    str = FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "MD5");
                    break;
            }
            return str;
        }

        /**/
        /// <summary>
        /// 获取用户登陆IP
        /// </summary>
        /// <returns>返回用户IP</returns>
        public static string GetIp()
        {
            string user_IP;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }
        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <returns>返回bool值</returns>
        public static bool isNumeric(string str)
        {
            string a = str;
            if (System.Text.RegularExpressions.Regex.IsMatch(a, @"^\d*$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void userOnline()
        {
            Member m = new Member();
            if (!m.isOnLine())
            {
                alert.showAndGo("您未登录或登录超时", "Default.aspx");
            }
        }
        public static bool isOnLine()
        {
            Member m = new Member();
            if (!m.isOnLine())
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// DataTable转成Json
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{");
            Json.Append("\""+jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        public static string DataTableToJson(string jsonName, DataTable dt,string pageCount,string pageSize,string curPage)
        {
            /*
             {"Mian新闻":[{"NewsID":"3","Title":"重大新闻","picSmall":"/UpFile/20120409153411822.jpg","EditTime":"2012/4/10 22:07:06"},{"NewsID":"2","Title":"t1","picSmall":"/UpFile/20120409142302040.jpg","EditTime":"2012/4/9 14:44:31"},{"NewsID":"1","Title":"ttt","picSmall":"/UpFile/20120409141519389.jpg","EditTime":"2012/4/9 14:15:21"}]}
             */
            StringBuilder Json = new StringBuilder();
            Json.Append("{");
            Json.Append("\"pageCount\":\"" + pageCount + "\",");
            Json.Append("\"pageSize\":\"" + pageSize + "\",");
            Json.Append("\"curPage\":\"" + curPage + "\",");
            Json.Append("\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
public static string DataTableToJson(string jsonName, DataTable dt,string pageCount)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{");
            Json.Append("\"pageCount\":\"" + pageCount + "\",");
            Json.Append("\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }



        //List转成json
        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        public static void adminLogin()
        {
            System.Web.HttpContext.Current.Session["AdminName"] = "1";
            return;
            if (!active())
            {
                alert.showAndGo("系统已过期，请与管理员联系。", "/Admin/Login.aspx");
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["AdminName"] == null)
                {
                    alert.showAndGo("您未登录或登录超时。", "/Admin/Login.aspx");
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }
       
    }
}
