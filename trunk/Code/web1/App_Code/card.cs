using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace web
{
    public static class card
    {
        /// <summary>
        /// 生成卡
        /// </summary>
        /// <param name="s1">前两位标识</param>
        /// <param name="s2">中间两位标识</param>
        /// <param name="count">要生成的数量</param>
        public static void create(string s1,string s2,string count)
        {
            try
            {
                string sql = "usp_CreateCard '" + s1 + "','" + s2 + "'," + count;
                db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
