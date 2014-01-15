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
    public partial class Prod : System.Web.UI.Page
    {
        public string lbid, lbname,pid,pname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string pid = pg.request("pid");
                if (pid == "") pid = "6";
                if (pid != "")
                {
                    pname = clsLB.getLbname(pid);
                    hPid.Value = pid;
                }

                string act = pg.request("act");
                if (act == "top")
                {
                    //置顶
                    db.ExecSql("UPDATE " + com.tablePrefix + "News SET isTop=0 WHERE lbid IN (SELECT lbid FROM " + com.tablePrefix + "lb WHERE parentid=" + pid + ")");
                    db.ExecSql("UPDATE " + com.tablePrefix + "News SET isTop=1 WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "Prod.aspx?pid=" + pid);
                    return;
                }

                if (act == "IsIndex")
                {
                    //置顶
                    db.ExecSql("UPDATE News SET IsIndex=" + pg.request("value") + " WHERE NewsID=" + pg.request("id"));
                    alert.showAndGo("设置成功", "Prod.aspx?lbid=" + lbid);
                    return;
                }



                if (act == "del")
                {
                    string id = pg.request("id");
                    SQLHelper_ db = new SQLHelper_();
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
                        //string picSmall = dt.Rows[0]["picSmall"].ToString();
                        //if (picSmall.Length > 0)
                        //    File.Delete(Server.MapPath(picSmall));
                        //删除大图
                        //删除相册中的照片

                        db.sql = @"SELECT *
FROM " + com.tablePrefix + "News  WHERE ParentNewsID=" + id;
                        dt = new DataTable();
                        dt = db.Get_DataTable();
                        foreach (DataRow dr in dt.Rows)
                        {
                            string pic = dr["pic"].ToString().ToLower();
                            FileSys.delFile(pic);
                            FileSys.delFile(pic.Replace(".jpg", "_s.jpg"));
                        }
                        db.sql = "DELETE News WHERE ParentNewsID=" + id;
                        db.ExecSql();
                    }

                    db.sql = "DELETE FROM " + com.tablePrefix + "News WHERE NewsID=" + id;
                    db.ExecSql();
                    alert.showAndGo("删除成功", "Prod.aspx?pid=" + pid);

                    return;
                }
                bindGv();
            }
        }

        public void bindGv()
        {
            pid = hPid.Value;
            string sql = @"SELECT b.lbname,c.lbid AS Pid,c.lbname AS Pname, a.*
FROM "+com.tablePrefix+@"News  a 
INNER JOIN "+com.tablePrefix+@"lb b ON a.lbid=b.lbid
INNER JOIN "+com.tablePrefix+@"lb c ON b.parentid=c.lbid
WHERE c.lbid =" + pid;
            sql += " ORDER BY a.isTop DESC,a.IsIndex DESC,a.AddTime DESC";
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();

            GV.DataSource = dt.DefaultView;
            GV.DataBind();

        }
        public string setTopBtn(string newsid)
        {
            pid = hPid.Value;
            string re = "";
            string sql = "SELECT isNull(isTop,0) as isTop,lbid FROM " + com.tablePrefix + "News WHERE newsid=" + newsid;
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                string lbid = dt.Rows[0]["lbid"].ToString();
                if (dt.Rows[0][0].ToString() == "0")
                    re = "<a href='Prod.aspx?act=top&pid=" + pid + "&id=" + newsid + "'>置顶</a>";
                else
                    re = "已置顶";
            }
            return re;
        }

        public string setIsIndex(string newsid)
        {
            string re = "";
            string sql = "SELECT isNull(IsIndex,0) as IsIndex,lbid FROM " + com.tablePrefix + "News WHERE newsid=" + newsid;
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                    re = "<a href='Prod.aspx?act=IsIndex&value=1&pid=" + pid + "&id=" + newsid + "' style='color:green;'>放到首页</a>";
                else
                    re = "<a href='Prod.aspx?act=IsIndex&value=0&pid=" + pid + "&id=" + newsid + "' style='color:red;'>取消首页</a>";
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
            pid = hPid.Value;
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