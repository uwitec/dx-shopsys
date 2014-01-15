using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

namespace web
{
    public static class pg
    {
        public static string request(string name)
        {

            if (System.Web.HttpContext.Current.Request.QueryString[name] != null)
            {
                string val = GetSafeString(System.Web.HttpContext.Current.Request.QueryString[name].ToString());
                if (val.Length > 0)
                    return val;
                else
                    return "";
            }
            else if (System.Web.HttpContext.Current.Request.Form[name] != null)
            {
                string val = GetSafeString(System.Web.HttpContext.Current.Request.Form[name].ToString());
                if (val.Length > 0)
                    return val;
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// SQL注入过滤
        /// </summary>
        /// <param name="InText">要过滤的字符串</param>
        /// <returns>如果参数不存在不安全字符，则返回true</returns>
        public static bool SafeString(string InText)
        {
            //string word = "exec |insert |select |delete |update |chr |mid |master |or |truncate |char |declare |join ";
            string word = "exec |insert |select |delete |update |chr |mid |master |truncate |char |declare |join ";

            if (InText == null)
                return true;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return false;
                }
            }
            return true;
        }

        public static string ClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }


            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        public static string GetSafeString(string strInput)
        {
            string strTemp = strInput.Trim();
            if (strInput != null || strInput != "")
            {
                //strTemp = wipeScript(strTemp);
                //strTemp = strTemp.Replace("'", "''");
                //strTemp = strTemp.Replace("&", "");
                //strTemp = strTemp.Replace(@"""", "");
                //strTemp = strTemp.Replace("%", "");
                //strTemp = strTemp.Replace(";", "");
                //strTemp = strTemp.Replace("<",""); 
                //strTemp = strTemp.Replace(">",""); 
                //strTemp = strTemp.Replace("(", "");
                //strTemp = strTemp.Replace(")", "");
                //strTemp = strTemp.Replace("+", "");
                //strTemp = strTemp.Replace("--", "");
                //strTemp = strTemp.Replace("/*", "");
                //strTemp = strTemp.Replace("*/", "");
                strTemp = strTemp.Replace("delete ", "");
                strTemp = strTemp.Replace("drop ", "");
                strTemp = strTemp.Replace("insert ", "");
                strTemp = strTemp.Replace("select ", "");
                strTemp = strTemp.Replace("update ", "");
                strTemp = strTemp.Replace("grant ", "");
                strTemp = strTemp.Replace("alter ", "");
                //strTemp = strTemp.Replace("|", "");
                strTemp = strTemp.Replace("0x", "");
            }
            return strTemp;
        }


        /// <summary>
        /// 过滤脚本内容，防止js和iframe等木马
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string wipeScript(string html)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex.Replace(html, ""); //过滤<script></script>标记
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤href=javascript: (<A>) 属性

            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex2.Replace(html, " _disibledevent="); //过滤其它控件的on...事件

            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex3.Replace(html, ""); //过滤iframe
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex4.Replace(html, ""); //过滤frameset
            return html;
        }


        public static string dlbtn(string id)
        {
            string html = "";
            //<div id=download><a href='dl.aspx?id=1'>附件下载</a></div>
            //读取附件名称+.扩展名
            DataTable dt1 = db.Get_DataTable("SELECT lbid FROM " + com.tablePrefix + "News WHERE NewsID=" + id);
            if (dt1.Rows.Count > 0)
            {
                string lbid = dt1.Rows[0][0].ToString();
                if (lbid == "1")
                {
                    html = "";
                }
                else
                {
                    DataTable dt = db.Get_DataTable("SELECT title,pic,isnull(picName,'') AS picName FROM " + com.tablePrefix + "News WHERE NewsID=" + id);
                    if (dt.Rows.Count > 0)
                    {
                        string pic = dt.Rows[0]["pic"].ToString();
                        string ext = pic.Substring(pic.IndexOf('.'), pic.Length - pic.IndexOf('.'));
                        string picName = dt.Rows[0]["picName"].ToString();
                        if (picName.Length == 0)
                        {
                            picName = dt.Rows[0]["title"].ToString();
                        }

                        html = "\n<div id=\"download\">";
                        html += "<a href='dl.aspx?id=" + id + "'>" + picName + ext + "</a>";
                        html += "</div>\n";
                    }

                }
            }
            return html;

        }

        public static string session(string sessionName)
        {
            if (System.Web.HttpContext.Current.Session[sessionName] != null)
            {
                return System.Web.HttpContext.Current.Session[sessionName].ToString();
            }
            else
            {
                return "";
            }
            //System.Web.SessionState.HttpSessionState

        }


        #region 分页
        /*
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="datalistname"></param>
        /// <param name="pagesize"></param>
        /// <param name="PageTypeID"></param>
        /// <param name="Pageparent"></param>
        /// <returns></returns>
        private string GetPageNum(DataList datalistname, int pagesize, string PageTypeID, string Pageparent)
        {
            List<ProduceInfoTable> list = ProduceInfoTableManager.QueryProduceByProduceTypeID(Convert.ToInt32(PageTypeID));
            PagedDataSource objPds = new PagedDataSource();
            objPds.DataSource = list;
            objPds.AllowPaging = true;
            int total = list.Count;
            objPds.PageSize = pagesize;
            int page;
            if (HttpContext.Current.Request.QueryString["page"] != null)
                page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            else page = 1;
            objPds.CurrentPageIndex = page - 1; 
            datalistname.DataSource = objPds; 
            datalistname.DataBind(); 
            int allpage = 0; 
            int next = 0; 
            int pre = 0; 
            int startcount = 0; 
            int endcount = 0; 
            string pagestr = ""; 
            if (page < 1) { page = 1; }        
            //计算总页数      
            if (pagesize != 0)
            {
                allpage = (total / pagesize);
                allpage = ((total % pagesize) != 0 ? allpage + 1 : allpage);
                allpage = (allpage == 0 ? 1 : allpage);
            }
            next = page + 1;
            pre = page - 1;
            startcount = (page + 5) > allpage ? allpage - 9 : page - 4;//中间页起始序号        
            //中间页终止序号      
            endcount = page < 5 ? 10 : page + 5; if (startcount < 1) { startcount = 1; } //为了避免输出的时候产生负数，设置如果小于1就从序号1开始        
            if (allpage < endcount) { endcount = allpage; } //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内 ../Produce_Index/<%#DataBinder.Eval(Container.DataItem,"ProduceTypeID") %>_<%#DataBinder.Eval(Container.DataItem,"ProductParent")_page=1 %>.html     
            pagestr = "<a >" + "共" + allpage + "页</a>&nbsp;&nbsp;&nbsp;&nbsp;"; pagestr += page > 1 ? "<a href=\"../Produce/Produce_Index/page/" + PageTypeID + "_" + Pageparent + "_" + "1" + ".html\">首页</a>&nbsp;&nbsp;<a href=\"../Produce/Produce_Index/page/" + PageTypeID + "_" + Pageparent + "_" + pre + ".html\">上一页</a>" : "<a>首页</a>" + "&nbsp;&nbsp;" + "<a>上一页</a>";       //中间页处理，这个增加时间复杂度，减小空间复杂度      
            for (int i = startcount; i <= endcount; i++)
            {
                pagestr += page == i ? "&nbsp;&nbsp;" + "<a class=\"cpb\">" + i + "</a>" : "&nbsp;&nbsp;<a href=\"Produce_Index/page/" + PageTypeID + "_" + Pageparent + "_" + i + ".html\">" + i + "</a>";
            } 
            pagestr += page != allpage ? "&nbsp;&nbsp;<a href=\"../Produce/Produce_Index/page/" + PageTypeID + "_" + Pageparent + "_" + next + ".html\">下一页</a>&nbsp;&nbsp;<a href=\"../Produce/Produce_Index/page/" + PageTypeID + "_" + Pageparent + "_" + allpage + ".html\">末页</a>" : "&nbsp;&nbsp;" + "<a >下一页</a>" + "&nbsp;&nbsp;" + "<a >末页</a>";
            return pagestr;
        }
        */
        #endregion

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

        public static string MD5(string PWD)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "MD5");
        }

        /**/
        ///   <summary>   
        ///   去除HTML标记   
        ///   </summary>   
        ///   <param   name="NoHTML">包括HTML的源码   </param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /*
1	首页
2	1-3年级
3	4-6年级
4	初中
5	高中
6	成人
         */
        public static seo getSeo(string lbid)
        {
            seo s = new seo();
            SQLHelper_ db = new SQLHelper_();
            db.sql = "SELECT id,SeoTitle,SeoKey,SeoDesc FROM " + com.tablePrefix + "TB_SEO WHERE lbid=" + lbid;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                s.title = dt.Rows[0]["SeoTitle"].ToString();
                s.key = dt.Rows[0]["SeoKey"].ToString();
                s.desc = dt.Rows[0]["SeoDesc"].ToString();
            }
            else
            {
                s.title = s.key = s.desc = "";
            }
            return s;
        }

    }
}
