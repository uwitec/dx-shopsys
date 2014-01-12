using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using web;
using System.Collections.Generic;
using System.Drawing;

namespace web1.Admin
{
    public partial class NewsAdd : System.Web.UI.Page
    {
        public string lbid, lbname,NewsID,guid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["AdminName"] = "admin"; //
            
            if (!IsPostBack)
            {
                hGuid.Value = Guid.NewGuid().ToString();//上传图片时作为临时标志
                string NewsID = pg.request("id");
                lbid = pg.request("lbid");
                if (lbid == "") lbid = "2";//Mian新闻
                hLbid.Value = lbid;
                if (NewsID != "")
                {
                    string act = pg.request("act");
                    if (act == "delpic")
                    {
                        //由前台的jquery异步调用
                        string imgsrc = pg.request("imgsrc");
                        SQLHelper db = new SQLHelper();
                        db.sql = "UPDATE News SET pic = replace(pic,'" + imgsrc + "','') WHERE NewsID=" + NewsID;
                        db.ExecSql();
                        db.sql = "UPDATE News SET pic = replace(pic,'||','|') WHERE NewsID=" + NewsID;
                        db.ExecSql();
                        FileSys.delFile(imgsrc);
                        Response.Write("ok");
                        Response.End();
                    }
                    else
                    {
                        string sql = "SELECT * FROM News WHERE NewsID=" + NewsID;
                        try
                        {
                            DataTable dt = db.Get_DataTable(sql);
                            if (dt.Rows.Count > 0)
                            {

                                string title = dt.Rows[0]["title"].ToString();
                                string newsBody = dt.Rows[0]["NewsBody"].ToString();
                                string pic = dt.Rows[0]["pic"].ToString();
                                string addTime = dt.Rows[0]["addTime"].ToString();
                                string picSmall = dt.Rows[0]["picSmall"].ToString();
                                if (addTime.Length > 0)
                                {
                                    addTime = Convert.ToDateTime(addTime).ToString("yyyy-MM-dd hh:mm:ss");
                                }
                                lbid = dt.Rows[0]["lbid"].ToString();
                                lbname = clsLB.getLbname(lbid);
                                hID.Value = NewsID;
                                hPic.Value = pic;
                                hSmallPic.Value = picSmall;
                                txtTitle.Text = title;
                                txtAddTime.Text = addTime;
                                FCKeditor1.Value = newsBody;
                                btnAdd.Text = "修改并保存";
                                lblOper.Text = "修改"+lbname;
                            }

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message + "<br>" + sql);
                            Response.End();
                        }
                    }

                }
                else
                {
                    lbname = clsLB.getLbname(lbid);
                    txtAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    btnAdd.Text = "添加";
                    lblOper.Text = "添加" + lbname;
                }
                //txtStar.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
                //txtStudentCount.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
                //txtStudentTotalCount.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string lbid = hLbid.Value;
            string id = hID.Value;
            string title = pg.GetSafeString(txtTitle.Text);
            string oldPic = hPic.Value;
            string newPic = hPic_new.Value;
            string oldSmallPic = hSmallPic.Value;
            string newsBody = pg.GetSafeString(FCKeditor1.Value);
            string pic, picSmall = hSmallPic_new.Value ;
            string addtime = pg.GetSafeString(txtAddTime.Text);

            pic = oldPic + newPic;
            pic = pic.Replace("||", "|");
            string sql = "";

            if (id.Length > 0)
            {
                if (picSmall == "")
                {
                    //未上传小图
                    picSmall = oldSmallPic;
                }
                else
                {
                    //已上传小图并且原来有图
                    if (picSmall != "" && oldSmallPic != "")
                    {
                        //删除旧图
                        FileSys.delFile(oldSmallPic);
                    }
                }
                //修改
                sql = "UPDATE News SET Title='" + title + "',AddTime='" + addtime + "',pic='" + pic + "',picSmall='" + picSmall + "',NewsBody='" + newsBody + "',Editor='" + Session["AdminName"].ToString() + "',EditTime=getdate() WHERE NewsID=" + id;
                if (db.ExecSql(sql) == "1")
                {
                    //删除旧文件
                    if (picSmall != oldPic)
                    {
                        FileSys.delFile(oldPic);
                    }

                    alert.showAndGo("修改成功", "News.aspx?lbid=" + lbid);
                }
                else
                {
                    //alert.ShowAndBack(Page, "修改失败");
                    Response.Write(sql);
                    Response.End();
                }
            }
            else
            {
                //添加
                string newsid = clsNews.MaxNewsid();
                sql = "INSERT INTO News(NewsID,lbid,Title,AddTime,pic,picSmall,NewsBody,Creator) VALUES(" + newsid + "," + lbid + ",'" + title + "','" + addtime + "','" + pic + "','"+picSmall+"','" + newsBody + "','" + Session["AdminName"].ToString() + "')";

                string result = db.ExecSql(sql);
                if (result == "1")
                {
                    alert.showAndGo("添加成功", "News.aspx?lbid=" + lbid);
                }
                else
                {
                    //Response.Write(sql);
                    alert.ShowAndBack(Page, "添加失败" + result);
                }
            }
        }


    }
}