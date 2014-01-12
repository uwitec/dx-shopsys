using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using web;
namespace web1.Admin
{
    /// <summary>
    /// 添加内容页
//1.小图标上传。
//2.标题。
//3.内容添加（包含图片）
//4.日期
//内容页列表
//1.文章可编辑删除、推荐至首页（取消推荐）

    /// </summary>
    public partial class news : System.Web.UI.Page
    {
        public string lbid, lbname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string lbid = pg.request("lbid");
                if (lbid != "")
                {
                    lbname = clsLB.getLbname(lbid);
                    hlbid.Value = lbid;
                }

                string act = pg.request("act");
                if (act == "top")
                {
                    //置顶
                    db.ExecSql("UPDATE News SET isTop=0 WHERE lbid=" + lbid);
                    db.ExecSql("UPDATE News SET isTop=1 WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "news.aspx?lbid=" + lbid);
                    return;
                }

                if (act == "IsIndex")
                {
                    //置顶
                    db.ExecSql("UPDATE News SET IsIndex=" + pg.request("value")+" WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "news.aspx?lbid=" + lbid);
                    return;
                }



                if (act == "del")
                {
                    string id = pg.request("id");
                    SQLHelper db = new SQLHelper();
                    db.sql = "SELECT pic,picSmall FROM " + com.tablePrefix + "News WHERE NewsID=" + id;
                    DataTable dt = db.Get_DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        //string pic = dt.Rows[0]["pic"].ToString();
                        //if (pic.Length > 0)
                        //{
                        //    string[] pics = pic.Split('|');
                        //    foreach (string p in pics)
                        //    {
                        //        if (p.Length > 0)
                        //            File.Delete(Server.MapPath(p));
                        //    }
                        //}
                        string picSmall = dt.Rows[0]["picSmall"].ToString();
                        if (picSmall.Length > 0)
                            File.Delete(Server.MapPath(picSmall));
                        //删除大图
                        //删除相册中的照片
                        
                        db.sql = @"SELECT *
FROM " + com.tablePrefix + "News  WHERE ParentNewsID=" + id;
                        dt = new DataTable();
                        dt = db.Get_DataTable();
                        foreach (DataRow dr in dt.Rows)
                        {
                            string pic = dr["pic"].ToString().ToLower() ;
                            FileSys.delFile(pic);
                            FileSys.delFile(pic.Replace(".jpg","_s.jpg"));
                        }
                        db.sql = "DELETE News WHERE ParentNewsID=" + id;
                        db.ExecSql();
                    }

                    db.sql = "DELETE FROM " + com.tablePrefix + "News WHERE NewsID=" + id;
                    db.ExecSql();
                    alert.showAndGo("删除成功", "news.aspx?lbid=" + lbid);

                    return;
                }
                bindGv();
            }
        }

        public void bindGv()
        {
            lbid = hlbid.Value;
            string sql = @"SELECT *
FROM "+com.tablePrefix+@"News 
WHERE lbid=" + lbid;
            sql += " ORDER BY isTop DESC,IsIndex DESC,AddTime DESC";
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();

            GV.DataSource = dt.DefaultView;
            GV.DataBind();

        }
        public string setTopBtn(string newsid)
        {
            string re = "";
            string sql = "SELECT isNull(isTop,0) as isTop,lbid FROM " + com.tablePrefix + "News WHERE newsid=" + newsid;
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                string lbid = dt.Rows[0]["lbid"].ToString();
                if (dt.Rows[0][0].ToString() == "0")
                    re = "<a href='News.aspx?act=top&lbid=" + lbid + "&id=" + newsid + "'>置顶</a>";
                else
                    re = "已置顶";
            }
            return re;
        }

        public string setIsIndex(string newsid)
        {
            string re = "";
            string sql = "SELECT isNull(IsIndex,0) as IsIndex,lbid FROM " + com.tablePrefix + "News WHERE newsid=" + newsid;
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                string lbid = dt.Rows[0]["lbid"].ToString();
                if (dt.Rows[0][0].ToString() == "0")
                    re = "<a href='News.aspx?act=IsIndex&value=1&lbid=" + lbid + "&id=" + newsid + "' style='color:green;'>放到首页</a>";
                else
                    re = "<a href='News.aspx?act=IsIndex&value=0&lbid=" + lbid + "&id=" + newsid + "' style='color:red;'>取消首页</a>";
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
            lbid = hlbid.Value;
            System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
            if (btn == null) return;
            int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;

            DataKey key = this.GV.DataKeys[index];
            string NewsID = key.Value.ToString();
            if (e.CommandName == "PicMng")
            {
                Server.Transfer("NewsPicManage.aspx?lbid=" + NewsID);
                return;
            }
        }


    }
}