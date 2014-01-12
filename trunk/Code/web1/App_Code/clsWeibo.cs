using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetDimension.Weibo;
using System.Configuration;
namespace web
{
    public static class clsWeibo
    {
        public static OAuth Authorize(string CallbackUrl)
        {
            string AppKey = ConfigurationManager.AppSettings["app_key"].ToString();
            string AppSecrect = ConfigurationManager.AppSettings["app_secret"].ToString();
            OAuth o = new OAuth(AppKey, AppSecrect,CallbackUrl);
            //string authorizeUrl = o.GetAuthorizeURL();
            while (!ClientLogin(o))	//使用模拟方法
            {
                //Console.WriteLine("授权登录失败，请重试。");
                HttpContext.Current.Response.Write("授权登录失败，请重试。");
            }

            return o;
        }

        private static bool ClientLogin(OAuth o)
        {
            //Console.Write("微博账号:");
            string passport = "cotton_cbyi@yahoo.cn";
            //Console.Write("登录密码:");

            //ConsoleColor originColor = Console.ForegroundColor;
            //Console.ForegroundColor = Console.BackgroundColor; //知道这里是在干啥不？其实是为了不让你们看到我的密码^_^

            string password = "edelmangc";

            //Console.ForegroundColor = originColor; //恢复前景颜色。

            return o.ClientLogin(passport, password);
            
        }
    }
}