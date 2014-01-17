using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using web;
using web1.Models;
namespace web1.Admin
{
    public partial class Prod : System.Web.UI.Page
    {
        Models.DbClassesDataContext dbContext = new Models.DbClassesDataContext();
        public string lbname,lbid;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            if (!IsPostBack)
            {
                string lbid = pg.request("lbid");
                if (lbid == "") lbid = "0";
                if (lbid != "")
                {
                    lbname = clsLB.getLbname(lbid);
                    hLbid.Value = lbid;
                }

                string act = pg.request("act");
                if (act == "top")
                {
                    //置顶
                    List<DXProd> prods = (from pro in dbContext.DXProd where pro.lbid == Int32.Parse(lbid) select pro).ToList<DXProd>();
                    foreach (var item in prods)
                    {
                        item.isTop = false;
                    }
                    dbContext.SubmitChanges();

                    DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(pg.request("id")) select pro).First<DXProd>();
                    pro_.isTop = true;
                    dbContext.SubmitChanges();
                    return;
                }

                if (act == "IsIndex")
                {
                    //置顶
                    DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(pg.request("id")) select pro).First<DXProd>();
                    pro_.isIndex = true;
                    dbContext.SubmitChanges();
                    alert.showAndGo("设置成功", "Prod.aspx?lbid=" + lbid);
                    return;
                }
                if (act == "del")
                {
                    string id = pg.request("id");
                    DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(pg.request("id")) select pro).First<DXProd>();
                    //删除图片
                    FileSys.delFile(pro_.pic);
                    FileSys.delFile(pro_.picSmall);
                    FileSys.delFile(pro_.pic.Replace(".jpg", "_s.jpg"));

                    //删除
                    dbContext.DXProd.DeleteOnSubmit(dbContext.DXProd.Single(c=>c.Id==Int32.Parse(id)));

                    alert.showAndGo("删除成功", "Prod.aspx?lbid=" + lbid);
                    return;
                }
                bindGv();
            }
        }

        public void bindGv()
        {
            lbid = hLbid.Value;
            
            List<DXProd> prods = (from pro in dbContext.DXProd
                                  join lb in dbContext.DXLb on pro.lbid equals lb.Id
                                  where lb.Id == Int32.Parse(lbid)
                                  orderby pro.OrderId
                                  select pro).ToList<DXProd>();
            GV.DataSource = prods;
            GV.DataBind();

        }
        public string setTopBtn(string newsid)
        {
            lbid = hLbid.Value;
            DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(newsid) select pro).First<DXProd>();
            string re = "";
            if (!pro_.isTop)
                re = "<a href='Prod.aspx?act=top&pid=" + lbid + "&id=" + newsid + "'>置顶</a>";
            else
                re = "已置顶";
            return re;
        }

        public string setIsIndex(string newsid)
        {
            DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(newsid) select pro).First<DXProd>();
            string re = "";
            if (pro_.isIndex)
            {
                re = "<a href='Prod.aspx?act=IsIndex&value=0&lbid=" + hLbid.Value + "&id=" + newsid + "' style='color:red;'>取消首页</a>";
            }
            else
            {
                re = "<a href='Prod.aspx?act=IsIndex&value=1&lbid=" + hLbid.Value + "&id=" + newsid + "' style='color:green;'>放到首页</a>";
            }
            return re;
        }

        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV.PageIndex = e.NewPageIndex;
            bindGv();
        }

        protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
            if (btn == null) return;
            int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;

            DataKey key = this.GV.DataKeys[index];
            string NewsID = key.Value.ToString();
            if (e.CommandName == "PicMng")
            {
                Server.Transfer("ProPicManage.aspx?productid=" + NewsID);
                return;
            }
        }


    }
}