using System;
using System.Collections.Generic;

using System.Web;
using System.Data;

namespace web
{
    public static class clsNews
    {
        public static int MaxNewsID()
        {
            DataTable dt = db.Get_DataTable("SELECT COUNT(0) FROM " + com.tablePrefix + "News");
            if (dt.Rows[0][0].ToString() == "0")
            {
                return 0;
            }
            else
            {
                DataTable dt1 = db.Get_DataTable("SELECT MAX(NewsID) AS NewsID FROM " + com.tablePrefix + "News");
                return Int32.Parse(dt1.Rows[0][0].ToString());
            }
        }



        public static string GetLbid(string NewsID)
        {
            string sql = "SELECT lbid FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
            SQLHelper db = new SQLHelper();
            db.sql = sql;
            DataTable dt = db.Get_DataTable();
            string result = "0";
            if (dt.Rows.Count > 0)
                result = dt.Rows[0][0].ToString();
            dt.Dispose();
            return result;
        }


        
        public static string MaxNewsid(){
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT ISNULL(Max(NewsID),0)+1 AS NewsID FROM " + com.tablePrefix + "News";
            DataTable dt = db.Get_DataTable();
            return dt.Rows[0][0].ToString();
        }


    }
}
