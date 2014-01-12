using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace web
{
    public sealed class Constants
    {

        /// <summary>
        /// 网站名称
        /// </summary>
        public static string SITE_NAME
        {
            get
            {
                return ConfigurationManager.AppSettings["SITE_NAME"].ToString();
            }
        }

        /// <summary>
        /// 域名
        /// </summary>
        public static string DOMAIN
        {
            get
            {
                return ConfigurationManager.AppSettings["DOMAIN"].ToString();
            }
        }

        /// <summary>
        /// 文章来源
        /// </summary>
        public static string NEWS_FROM
        {
            get
            {
                return ConfigurationManager.AppSettings["NEWS_FROM"].ToString();
            }
        }


        /// <summary>
        /// 列表页默认显示记录条数
        /// </summary>
        public static string DEFAULT_PAGE_SIZE
        {
            get
            {
                return ConfigurationManager.AppSettings["DEFAULT_PAGE_SIZE"].ToString();
            }
        }


    }
}
