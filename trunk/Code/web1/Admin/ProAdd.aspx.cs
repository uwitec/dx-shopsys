using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Data;
using web1.Models;
namespace web1.Admin
{
    public partial class ProAdd : System.Web.UI.Page
    {
        Models.DbClassesDataContext dbContext = new Models.DbClassesDataContext();
        public string lbid, lbname, NewsID, guid,pid,pname;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();

            if (!IsPostBack)
            {
                string id = pg.request("id");
                NewsID = id;
                pid = pg.request("pid");
                if (pid.Length > 0)
                {
                    pname = clsLB.getLbname(pid);
                    lblPname.Text = pname;
                    hPid.Value = pid;
                }
                

                string act = pg.request("act");
                if (act == "delpic")
                {
                    //由前台的jquery异步调用
                    string imgsrc = pg.request("imgsrc");
                    try
                    {
                        DXProd pd = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(id) select pro).First<DXProd>();
                        pd.pics = pd.pics.Replace(imgsrc, "");
                        pd.pics = pd.pics.Replace("||", "|");
                        dbContext.SubmitChanges();
                        FileSys.delFile(imgsrc);
                        Response.Write("ok");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("ERROR: "+ex.Message);
                    }
                    
                    Response.End();
                }

                if (id != "")
                {
                    pageInit();
                }
                else
                {
                    lbid = pg.request("lbid");
                    if (lbid == "") lbid = "0";
                    lbname = clsLB.getLbname(lbid);
                    pid = clsLB.getPid(lbid);
                    bindLbList(Int32.Parse(pid));

                    txtAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    btnAdd.Text = "添加";
                    lblOper.Text = "添加" + lbname;
                }
                //txtStar.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
                //txtStudentCount.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
                //txtStudentTotalCount.Attributes.Add("onchange", "if(/\\D/.test(this.value)){alert('只能输入数字');this.value=''}");
            }
        }

        protected void pageInit()
        {
            try
            {
                DXProd pro_ = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(NewsID) select pro).First<DXProd>();
                string title = pro_.Name;
                string newsBody = pro_.Body;
                string desc = pro_.Description;
                string pic = pro_.pic;
                DateTime? CreateTime = pro_.CreateTime;
                string picSmall = pro_.picSmall;
                string addTime = "";
                if (CreateTime != null)
                {
                    addTime = CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
                }
                lbid = pro_.lbid.ToString(); ;

                pid = clsLB.getPid(lbid);
                bindLbList(Int32.Parse(pid));
                lbList.SelectedValue = lbid;
                //lbname = clsLB.getLbname(lbid);
                hID.Value = NewsID;
                hPic.Value = pic;
                hSmallPic.Value = picSmall;
                txtTitle.Text = title;
                txtAddTime.Text = addTime;
                FCKeditor1.Value = newsBody;
                btnAdd.Text = "修改并保存";
                lblOper.Text = "修改" + lbname;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }

        void bindLbList(int parentLbid)
        {
            List<DXLb> lbs = (from lb in dbContext.DXLb where lb.ParentId == parentLbid orderby lb.OrderId select lb ).ToList<DXLb>();
            lbList.DataSource = lbs;
            lbList.DataTextField = "lbName";
            lbList.DataValueField = "Id";
            lbList.DataBind();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lbid = lbList.SelectedValue;
            string id = hID.Value;
            string title = pg.GetSafeString(txtTitle.Text);
            string oldPic = hPic.Value;
            string newPic = hPic_new.Value;
            string oldSmallPic = hSmallPic.Value;
            //string newsBody = pg.GetSafeString(tbxBody.Text).Replace("\"", "''");
            string newsBody = FCKeditor1.Value;
            string pic, picSmall = hSmallPic_new.Value;
            string addtime = pg.GetSafeString(txtAddTime.Text);
            string pid = hPid.Value;

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
                DXProd pd = (from pro in dbContext.DXProd where pro.Id == Int32.Parse(id) select pro).First<DXProd>();
                pd.lbid = Int32.Parse(lbid);
                pd.Name = title;
                pd.pic = pic;
                pd.picSmall = picSmall;
                pd.Body = newsBody;
                pd.EditorName = Session["AdminName"].ToString();
                pd.EditTime = DateTime.Now;
                dbContext.SubmitChanges();
                alert.showAndGo("修改成功", "Prod.aspx?lbid=" + lbid);
            }
            else
            {
                try
                {
                    //添加
                    DXProd pd = new DXProd();
                    pd.lbid = Int32.Parse(lbid);
                    pd.Name = title;
                    pd.pic = pic;
                    pd.picSmall = picSmall;
                    pd.Body = newsBody;
                    pd.CreateTime = DateTime.Now;
                    pd.EditTime = DateTime.Now;
                    pd.CreatorName = Session["AdminName"].ToString();
                    pd.EditorName = Session["AdminName"].ToString();

                    dbContext.DXProd.InsertOnSubmit(pd);
                    dbContext.SubmitChanges();
                    alert.showAndGo("添加成功", "Prod.aspx?lbid=" + lbid);
                }
                catch (Exception ex)
                {

                    alert.ShowAndBack(Page, "添加失败" + ex.Message);
                }
                
            }
        }

    }
}