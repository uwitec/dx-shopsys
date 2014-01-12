using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace DXWeb
{
    public static class Const
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["SQLConnString"].ToString();
    }
}