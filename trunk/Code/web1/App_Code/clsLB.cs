using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using DBFramework;
//using DBFramework.Entities;
using web1.Models;
using System.Linq;
namespace web1
{
    public static class clsLB
    {
        
        public static DataSet GetLblist()
        {
            string sql = string.Format(@"SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE parentid=0 ORDER BY lbid");
            DataSet ds = db.Get_DataSet(sql);
            return ds;
        }

        public static DataSet GetLblist(string pid)
        {
            string sql = "SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE parentid=" + pid;
            DataSet ds = db.Get_DataSet(sql);
            return ds;
        }


        public static DataSet GetXLblist()
        {
            string sql = string.Format(@"SELECT a.lbid AS lb1id, a.lbname,b.lbid AS lb2id,b.lbname,c.lbid AS lb3id, c.lbname,d.lbid AS lb4id,d.lbname FROM "+com.tablePrefix+@"lb a
LEFT JOIN "+com.tablePrefix+@"lb b ON b.parentid=a.lbid
LEFT JOIN "+com.tablePrefix+@"lb c ON c.parentid=b.lbid
LEFT JOIN "+com.tablePrefix+@"lb d ON d.parentid=c.lbid
WHERE a.parentid=0
ORDER BY a.lbid,b.lbid,c.lbid,d.lbid");
            DataSet ds = db.Get_DataSet(sql);
            return ds;
        }

        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="lbname">栏目名称</param>
        /// <param name="parentid">父栏目ID</param>
        /// <returns>int,1=成功，0=已存在</returns>
        public static string AddLb(string lbname, string parentid)
        {
            string maxlbid = MaxLbid();
            string sql = "INSERT INTO " + com.tablePrefix + "lb (lbid,lbname,parentid,OrderId) VALUES(" + maxlbid + ",'" + lbname + "'," + parentid + "," + maxlbid + ")";
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            return db.ExecSql();
        }

        public static void Update(string id, string lbname)
        {
            string sql = "UPDATE " + com.tablePrefix + "lb SET lbname='" + lbname + "' WHERE id=" + id;
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            db.ExcSql();
        }
        public static void Update(string id, string lbname,string orderid)
        {
            string sql = "UPDATE " + com.tablePrefix + "lb SET lbname='" + lbname + "',orderid="+orderid+" WHERE id=" + id;
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            db.ExcSql();
        }

        /// <summary>
        /// 获取栏目名称
        /// </summary>
        /// <param name="lbid">栏目ID</param>
        /// <returns>栏目名称</returns>
        public static string getLbname(string lbid)
        {
            if (lbid != "0")
            {
                string sql = "SELECT lbname from DXLb WHERE Id=" + lbid + "";
                SQLHelper_ db = new SQLHelper_();
                db.sql = sql;
                DataTable dt = db.Get_DataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "顶级栏目";
            }
        }
        //public static DXLb getParent(DXLb thislb)
        //{
        //    Models.DbClassesDataContext dbc = new Models.DbClassesDataContext();
        //    var qry = from lb in dbc.DXLb where lb.Id == thislb.ParentId select lb;
        //    DXLb parent = dbc.DXLb.First(c => c.ParentId == thislb.ParentId);
        //    return parent;
        //}

        public static string getPid(string lbid)
        {
            string sql = "SELECT parentid from " + com.tablePrefix + "lb WHERE Id=" + lbid + "";
            SQLHelper_ db = new SQLHelper_();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        public static bool lbidHasChild(string lbid)
        {
            string sql = "SELECT COUNT(0) FROM " + com.tablePrefix + "lb WHERE parentid=" + lbid;
            DataTable dt = db.Get_DataTable(sql);
            if (Int32.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DataTable getParentInfo(string lbid)
        {
            string sql = "SELECT * FROM " + com.tablePrefix + "lb WHERE lbid=(SELECT parentid FROM " + com.tablePrefix + "lb WHERE lbid=" + lbid + ")";
            return db.Get_DataTable(sql);
        }
        public static DataTable getLbInfo(string lbid)
        {
            string sql = "SELECT * FROM " + com.tablePrefix + "lb WHERE lbid=" + lbid;
            return db.Get_DataTable(sql);
        }
        public static string getTopID(string lbid)
        {
            string topid = getPid(lbid)=="0"? lbid: getPid(lbid);
            topid = getPid(topid) == "0" ? topid : getPid(topid);
            return topid;
        }
        public static string MaxLbid()
        {
            SQLHelper_ db = new SQLHelper_();
            db.sql = "SELECT ISNULL(Max(lbid),0)+1 AS lbid FROM " + com.tablePrefix + "lb";
            DataTable dt = db.Get_DataTable();
            return dt.Rows[0][0].ToString();
        }
        /// <summary>
        /// 在同类中排序
        /// </summary>
        /// <param name="id">lb.id</param>
        /// <param name="isUp">1=升序，0=降序</param>
        /// <returns>排序后的dataTable</returns>
        public static void lbOrderUP(string id,bool isUp)
        {
            SQLHelper_ db = new SQLHelper_();
            db.sql = "SELECT id,lbid,parentid,orderid FROM "+com.tablePrefix+"lb WHERE id="+id;
            DataTable dt = db.Get_DataTable();
            string parentid = dt.Rows[0]["parentid"].ToString();
            string curOrder = dt.Rows[0]["orderid"].ToString();

            if (isUp)
            {
                db.sql = "SELECT TOP 1 id,lbid,parentid,orderid FROM " + com.tablePrefix + "lb WHERE parentid=" + parentid + " AND orderid<" + curOrder + " ORDER BY orderid DESC";
            }
            else
            {
                db.sql = "SELECT TOP 1 id,lbid,parentid,orderid FROM " + com.tablePrefix + "lb WHERE parentid=" + parentid + " AND orderid>" + curOrder + " ORDER BY orderid";
            }

            DataTable dt1 = db.Get_DataTable();
            if (dt1.Rows.Count > 0)
            {
                string newOrder = dt1.Rows[0]["orderid"].ToString();
                string otherId = dt1.Rows[0]["id"].ToString();
                db.sql = "UPDATE "+com.tablePrefix+"lb SET orderid=" + newOrder + " WHERE id=" + id;
                db.ExecSql();
                db.sql = "UPDATE "+com.tablePrefix+"lb SET orderid=" + curOrder + " WHERE id=" + otherId;
                db.ExecSql();
            }
        }
        public static bool HasChild(string id)
        {
            string sql = "SELECT COUNT(0) FROM " + com.tablePrefix + "lb WHERE parentid=" + id;
            DataTable dt = db.Get_DataTable(sql);
            if (Int32.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static void del(string id)
        {
            SQLHelper_ db = new SQLHelper_();
            db.sql = "UPDATE dxlb SET isDeleted=1 WHERE id="+id;
            db.ExecSql();
        }
        public static bool lbnameExists(string lbname)
        {
            SQLHelper_ db = new SQLHelper_();
            db.sql = "SELECT 1 FROM " + com.tablePrefix + "lb WHERE lbname='" + lbname+"'";
            if (db.Get_DataTable().Rows.Count > 0)
                return true;
            else
                return false;

        }
    }
}
