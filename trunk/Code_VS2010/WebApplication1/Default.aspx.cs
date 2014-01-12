using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _id = Page.RouteData.Values["id"] as string;//接收路由参数
            lblMsg.Text = "id=" + _id;
            string _t = "";
            foreach (var item in Request.QueryString) //如果是用 /default/123?name=xxx之类的传过来的，测试一下能不能收到其它参数
            {
                _t += item + "=" + Request.QueryString[item.ToString()] + "，";
            }
            lblMsg.Text +="<br>URL参数："+ _t.Trim(',');
        }
    }
}
