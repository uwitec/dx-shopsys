using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace web1
{
    public class CookieHelper
    {

        #region 将ShopMobileView写入Cookie
        /// <summary>
        /// 将ShopMobileView写入Cookie
        /// </summary>
        /// <param name="sv">商品模型实体</param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void RecentViewToCookie(RecentView smv, string cookieName)
        {
            if (HasCookie(cookieName))
                cookieName = SetRecentViewCookie();

            IFormatter fm = new BinaryFormatter();
            string StrCartNew = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                fm.Serialize(ms, smv);
                byte[] byt = new byte[ms.Length];
                byt = ms.ToArray();
                StrCartNew = Convert.ToBase64String(byt);
                ms.Flush();
            }
            WriteCookie(cookieName, StrCartNew, 30);
        }
        #endregion

        #region 将Cookie反序列化为ShopMobileView
        /// <summary>
        /// 将Cookie反序列化为ShopMobileView
        /// </summary>
        /// <param name="CookieName">CookieName</param>
        public static RecentView CookieToShopMobileView(string cookieName)
        {
            if (HasCookie(cookieName))
                cookieName = SetRecentViewCookie();

            string StrCart = GetCookie(cookieName);
            if (StrCart == "" || StrCart == string.Empty)
                return null;

            string StrViewNew = GetCookie(cookieName);

            byte[] byt = Convert.FromBase64String(StrViewNew);

            RecentView SvNew = null;
            using (Stream smNew = new MemoryStream(byt, 0, byt.Length))
            {
                IFormatter fmNew = new BinaryFormatter();
                SvNew = (RecentView)fmNew.Deserialize(smNew);

            }
            if (SvNew == null)
                return null;
            else
                return SvNew;
        }
        #endregion

        #region 判断是否存在Cookie表
        /// <summary>   
        /// 判断是否存在Cookie表   
        /// </summary>   
        /// <param name="CookieName">Cookie名称</param>   
        /// <returns></returns>   
        public static bool HasCookie(string cookieName)
        {
            bool BoolReturnValue = false;
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                BoolReturnValue = true;

            return BoolReturnValue;
        }
        #endregion

        #region 为Cookie赋值方法
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(天)</param>
        public static void WriteCookie(string cookieName, string cookieValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
                cookie = new HttpCookie(cookieName);

            cookie.Value = cookieValue;
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        #endregion

        #region 读cookie值
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null)
                return HttpContext.Current.Request.Cookies[cookieName].Value.ToString();

            return "";
        }
        #endregion

        #region 删除Cookies
        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="cookieName">主键</param>
        /// <returns></returns>
        public static bool DelCookie(string cookieName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(cookieName);
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 设置Cookie的名称
        public static string SetRecentViewCookie()
        {
            return string.Format("prod_recentview");
        }

        public static string SetAffixViewCookie()
        {
            return string.Format("alpinepro_Affixview");
        }

        #endregion
    }

    /// <summary>
    /// 最近浏览过的手机商品
    /// </summary>
    [Serializable]
    public class RecentView
    {
        public Hashtable _RecentViewItems = new Hashtable();

        #region 返回已浏览过的所有商品(接口类型)
        /// <summary>
        /// 返回已浏览过的所有商品(接口类型)
        /// </summary>
        public ICollection RecentViewItems
        {
            get { return _RecentViewItems.Values; }
        }
        #endregion

        #region 向已浏览过实体中添加某商品
        /// <summary>
        /// 向已浏览过实体中添加某商品
        /// </summary>
        /// <param name="productID">产品编号</param>
        /// <param name="productName">产品名称</param>
        /// <param name="pro_bianhao">编号</param>
        /// <param name="picUrl">图片地址</param>
        public void AddViewItem(string pro_bianhao, int productID, string productName, string picUrl)
        {
            RecentViewItem item = (RecentViewItem)_RecentViewItems[productID];
            if (item == null)
                _RecentViewItems.Add(productID, new RecentViewItem(pro_bianhao, productID, productName, picUrl));
        }
        public void AddViewItem(RecentViewItem item)
        {
            _RecentViewItems.Add(item.ProductID, item);
        }

        #endregion

        #region 从已浏览过实体中移除某商品
        /// <summary>
        /// 从已浏览过实体中移除某商品
        /// </summary>
        /// <param name="productID"></param>
        public void RemoveViewItem(int productID)
        {
            RecentViewItem item = (RecentViewItem)_RecentViewItems[productID];
            if (item == null)
                return;
            else
                _RecentViewItems.Remove(productID);
        }
        #endregion

        #region 清空已浏览过实体
        /// <summary>
        /// 清空已浏览过实体
        /// </summary>
        public void ClearView()
        {
            _RecentViewItems.Clear();
        }
        #endregion

    }

    /// <summary>
    /// 最近浏览过的手机商品的实体类
    /// </summary>
    [Serializable]
    public class RecentViewItem
    {
        private int _ProductID;         //产品ID；
        private string _ProductName;    //产品名称
        private string _PicUrl;         //图片地址
        private string _pro_bianhao;    //编号
             
        #region 构造函数
        public RecentViewItem(string pro_bianhao, int productID, string productName, string picUrl)
        {
            _pro_bianhao = pro_bianhao;
            _ProductID = productID;
            _ProductName = productName;
            _PicUrl = picUrl;
        }

        public RecentViewItem(RecentViewItem item)
        {
            _pro_bianhao = item.Pro_bianhao;
            _ProductID = item.ProductID;
            _ProductName = item.ProductName;
            _PicUrl = item.PicUrl;
        }
        #endregion

        /// <summary>
        /// 编号
        /// </summary>
        public string Pro_bianhao
        {
            get { return _pro_bianhao; }
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID
        {
            get { return _ProductID; }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get { return _ProductName; }
        }


        /// <summary>
        /// 产品图片地址
        /// </summary>
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
    }

}