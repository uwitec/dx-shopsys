using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using web;

namespace web1
{
    public partial class getAiMianList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
lbid	lbname	parentid
1	Mian质新闻	0
2	Mian新闻	1
3	Mian百科	1
4	Mian故事	1
5	Mian尚前沿	1
6	Mian最行动	0
7	Mian事预告	6
8	爱Mian行动	6
9	Mian享瞬间	0
10	精采瞬间	9
11	Mian影像	9
12	Mian教堂	9
13	Mian在身边	0
14	微Mian互动	13
15	Mian下载	13
16	乐Mian游戏	13
17	图片集管理	10 
             */
            string lbid = pg.request("lbid");
            if (lbid == "") lbid = "8";

            string pagesize = pg.request("pagesize");
            string curpage = pg.request("curpage");
            string pageCount = "";//总页数
            if (pagesize == "") pagesize = "5";
            if (curpage == "") curpage = "1";


            string lbname = clsLB.getLbname(lbid);
            SQLHelper_ db = new SQLHelper_();

            string sql = "SELECT NewsID AS ID,Title,pic,href AS LinkUrl FROM News WHERE lbid=" + lbid + " ORDER BY EditTime DESC";
            sql = "exec sp_GetPageData '" + sql + "'," + curpage + "," + pagesize;
            db.sql = sql;
            DataSet ds = db.Get_DataSet();
            DataTable dt = ds.Tables[2];
            pageCount = ds.Tables[1].Rows[0][2].ToString();

            string strJson = string.Empty;
            strJson = com.DataTableToJson(lbname, dt, pageCount);
            Response.Write(strJson);
            Response.End();
        }
    }
}