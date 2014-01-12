using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web;
using System.Data;

namespace web1.Admin
{
    public partial class NetworkAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["AdminName"] = "admin"; //
            com.adminLogin();

            if (!IsPostBack)
            {
                bindProvince();
                string NewsID = pg.request("id");
                if (NewsID != "")
                {

                    string sql = @"SELECT c.id as provinceId,b.id as cityid, c.Name as provinceName,b.Name as cityName, a.* FROM "+com.tablePrefix+@"News a
LEFT JOIN City b ON a.cityid=b.id
LEFT JOIN Province c ON c.id=b.Pid
 WHERE lbid=24 and NewsID=" + NewsID;
                        try
                        {
                            SQLHelper db = new SQLHelper();
                            db.sql = sql;
                            DataTable dt = db.Get_DataTable();
                            if (dt.Rows.Count > 0)
                            {

                                string title = dt.Rows[0]["title"].ToString();
                                string body = dt.Rows[0]["NewsBody"].ToString();
                                string desc = dt.Rows[0]["Description"].ToString();
                                string provinceId = dt.Rows[0]["provinceId"].ToString();
                                string cityid = dt.Rows[0]["cityid"].ToString();
                                hID.Value = NewsID;
                                txtTitle.Text = title;
                                tbxBody.Text = body;
                                tbxDesc.Text = desc;//联系方式
                                ddlProvince.SelectedValue = provinceId;
                                if (provinceId!="") bindCity(provinceId);
                                ddlCity.SelectedValue = cityid;
                                btnAdd.Text = "修改并保存";
                                lblOper.Text = "修改";
                                //tbxDesc.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
                            }

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message + "<br>" + sql);
                            Response.End();
                        }

                }
                else
                {
                    btnAdd.Text = "添加";
                    lblOper.Text = "添加";
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
            string cityid = ddlCity.SelectedValue;
            string newsBody = tbxBody.Text;
            string tel = tbxDesc.Text;
            //if (tel.Length < 8)
            //{
            //    alert.ShowAndBack(Page, "联系方式不能小于8位数字");
            //    return;
            //}
            string sql = "";
            if (cityid == "0" || cityid.Length==0)
            {
                alert.ShowAndBack(Page, "请选择省市");
                return;
            }
            if (id.Length > 0)
            {
                
                //修改
                sql = "UPDATE News SET Title='" + title + "',cityid='" + cityid + "',Description='" + tel + "',NewsBody='" + newsBody + "',EditTime=getdate() WHERE NewsID=" + id;
                SQLHelper db = new SQLHelper();
                db.sql = sql;
                if (db.ExecSql() == "1")
                {
                    alert.showAndGo("修改成功", "NetworkManage.aspx");
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
                lbid = "24";
                sql = "INSERT INTO News(NewsID,lbid,Title,cityid,Description,NewsBody,Creator) VALUES(" + newsid + "," + lbid + ",'" + title + "','" + cityid + "','" + tel + "','" + newsBody + "','" + Session["AdminName"].ToString() + "')";
                SQLHelper db = new SQLHelper();
                db.sql = sql;
                string result = db.ExecSql();
                if (result == "1")
                {
                    alert.showAndGo("添加成功", "NetworkManage.aspx");
                }
                else
                {
                    Response.Write(result+",sql="+sql);
                    Response.End();
                    //alert.ShowAndBack(Page, "添加失败" + result);
                }
            }
        }
        protected void bindProvince()
        {
            SQLHelper db = new SQLHelper();
            db.sql = "SELECT id,name FROM " + com.tablePrefix + "Province ORDER BY OrderID";
            DataTable dt = db.Get_DataTable();
            ddlProvince.DataSource = dt;
            ddlProvince.DataValueField = "id";
            ddlProvince.DataTextField = "name";
            ddlProvince.DataBind();
        }
        protected void bindCity(string pid)
        {
            if (pid != "0")
            {
                SQLHelper db = new SQLHelper();
                db.sql = "SELECT id,name FROM " + com.tablePrefix + "City WHERE pid=" + pid + " ORDER BY OrderID";
                DataTable dt = db.Get_DataTable();

                ddlCity.DataSource = dt;
                ddlCity.DataValueField = "id";
                ddlCity.DataTextField = "name";
                ddlCity.DataBind();
            }
            else
            {
                ddlCity.Items.Clear();
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindCity(ddlProvince.SelectedValue);
        }


    }
}