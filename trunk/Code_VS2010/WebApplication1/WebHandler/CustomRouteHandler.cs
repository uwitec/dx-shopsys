using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;
using System.Web.UI;

namespace WebApplication1.WebHandler
{
    public class CustomRouteHandler : IRouteHandler
    {
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VirtualPath { get; private set; }

        public CustomRouteHandler(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        /// <summary>
        /// 返回实际请求页
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            foreach (var urlParm in requestContext.RouteData.Values)
            {
                requestContext.HttpContext.Items[urlParm.Key] = urlParm.Value;
            }
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;

            return page;
        }
    }
}