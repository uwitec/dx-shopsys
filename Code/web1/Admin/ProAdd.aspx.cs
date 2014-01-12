using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using web;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
namespace web1.Admin
{
    public partial class ProAdd : System.Web.UI.Page
    {
        public string lbid, lbname, NewsID, guid,pid,pname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                
                string NewsID = pg.request("id");
                pid = pg.request("pid");
                pname = clsLB.getLbname(pid);
                lblPname.Text = pname;
                hPid.Value = pid;
                bindLbList();
                bindFunctionList();
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
                        string sql = "SELECT * FROM " + com.tablePrefix + "News WHERE NewsID=" + NewsID;
                        try
                        {
                            SQLHelper db = new SQLHelper();
                            db.sql = sql;
                            DataTable dt = db.Get_DataTable();
                            if (dt.Rows.Count > 0)
                            {

                                string title = dt.Rows[0]["title"].ToString();
                                string bianhao = dt.Rows[0]["pro_bianhao"].ToString();
                                string mianliao = dt.Rows[0]["pro_mianliao"].ToString();
                                string newsBody = dt.Rows[0]["NewsBody"].ToString();
                                string desc =  dt.Rows[0]["Description"].ToString();
                                string pic = dt.Rows[0]["pic"].ToString();
                                string addTime = dt.Rows[0]["addTime"].ToString();
                                string picSmall = dt.Rows[0]["picSmall"].ToString();
                                if (addTime.Length > 0)
                                {
                                    addTime = Convert.ToDateTime(addTime).ToString("yyyy-MM-dd hh:mm:ss");
                                }
                                lbid = dt.Rows[0]["lbid"].ToString();
                                
                                lbList.SelectedValue = lbid;
                                //lbname = clsLB.getLbname(lbid);
                                hID.Value = NewsID;
                                hPic.Value = pic;
                                hSmallPic.Value = picSmall;
                                txtTitle.Text = title;
                                tbxBianhao.Text = bianhao;
                                tbxMianliao.Text = mianliao;
                                txtAddTime.Text = addTime;
                                tbxBody.Text = newsBody;
                                tbxSize.Text = desc;
                                string func = dt.Rows[0]["pro_function"].ToString();
                                string[] funcArr = func.Split(',');
                                for (int i = 0; i < functionList.Items.Count; i++)
                                {
                                    foreach (string fun in funcArr)
                                    {
                                        if(functionList.Items[i].Value==fun)
                                            functionList.Items[i].Selected = true;
                                    }
                                }

                                btnAdd.Text = "修改并保存";
                                lblOper.Text = "修改" + lbname;
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
            lbid = lbList.SelectedValue;
            string id = hID.Value;
            string title = pg.GetSafeString(txtTitle.Text);
            string bianhao = tbxBianhao.Text.Trim();
            string mianliao = tbxMianliao.Text.Trim();
            string oldPic = hPic.Value;
            string newPic = hPic_new.Value;
            string oldSmallPic = hSmallPic.Value;
            string newsBody = pg.GetSafeString(tbxBody.Text).Replace("\"", "''");
            string size = pg.GetSafeString(tbxSize.Text).Replace("\"", "''");
            string pic, picSmall = hSmallPic_new.Value;
            string addtime = pg.GetSafeString(txtAddTime.Text);
            string pid = hPid.Value;
            string func = ",";
            foreach (ListItem li in functionList.Items)
            {
                if (li.Selected == true)
                {
                    func += "," + li.Value;
                }
            }
            if (func != ",")
            {
                func = func.Replace(",,", "");
            }
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
                sql = "UPDATE News SET lbid=" + lbid + ", Title='" + title + "',pro_mianliao='" + mianliao + "',pro_bianhao='" + bianhao + "',picSmall='" + picSmall + "',AddTime='" + addtime + "',Description='" + size + "',NewsBody='" + newsBody + "',Editor='" + Session["AdminName"].ToString() + "',EditTime=getdate(),pro_function='" + func + "' WHERE NewsID=" + id;
                SQLHelper db = new SQLHelper();
                db.sql = sql;
                if (db.ExecSql() == "1")
                {
                    //删除旧文件
                    //if (picSmall != oldPic)
                    //{
                    //    FileSys.delFile(oldPic);
                    //}

                    alert.showAndGo("修改成功", "Prod.aspx?pid=" + pid);
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
                sql = "INSERT INTO News(NewsID,lbid,Description,Title,picSmall,pro_bianhao,pro_mianliao,AddTime,pro_function,NewsBody,Creator) VALUES(" + newsid + "," + lbid + ",'" + size + "','" + title + "','" + picSmall + "','" + bianhao + "','" + mianliao + "','" + addtime + "','" + func + "','" + newsBody + "','" + Session["AdminName"].ToString() + "')";
                SQLHelper db = new SQLHelper();
                db.sql = sql;
                string result = db.ExecSql();
                if (result == "1")
                {
                    alert.showAndGo("添加成功", "Prod.aspx?pid=" + pid);
                }
                else
                {
                    //Response.Write(sql);
                    alert.ShowAndBack(Page, "添加失败" + result);
                }
            }
        }

        protected void bindLbList()
        {
            pid = hPid.Value;
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT lbid,lbname FROM " + com.tablePrefix + "lb WHERE parentid=" + pid + " Order by orderid";
            DataTable dt = db.Get_DataTable();
            lbList.DataSource = dt.DefaultView;
            lbList.DataTextField = "lbname";
            lbList.DataValueField = "lbid";
            lbList.DataBind();
        }

        protected void bindFunctionList()
        {
            functionList.Items.Clear();
            //for (int i = 1; i <= 35; i++)
            //{
            //    ListItem li = new ListItem();
            //    string img = "0" + i.ToString() + ".jpg";
            //    int len = img.Length;
            //    img = img.Substring(len - 6, 6);
            //    li.Value = img;
            //    li.Text = "<img src='../images/function_icon/" + img + "' />";
            //    functionList.Items.Add(li);
            //}

            SQLHelper db = new SQLHelper();
            db.sql = "SELECT NewsId,Title,Description,pic FROM " + com.tablePrefix + "News WHERE lbid=64";
            DataTable dt = db.Get_DataTable();
            functionList.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr["NewsId"].ToString();
                li.Text = "<img src='" + dr["pic"].ToString() + "' title='" + dr["Description"].ToString() + "' />";
                functionList.Items.Add(li);
            }
        }
    }
}