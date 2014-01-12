using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace web1
{
    public static class Const
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ToString();
        public static string md5Key = "gsq";
    }
}