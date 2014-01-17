using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using web;
using DBFramework.Entities;
using DBFramework;
namespace web1.Admin
{
    public partial class LbManage : System.Web.UI.Page
    {
        public string pid="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pid"] != null)
                {
                    pid = Request.QueryString["pid"].ToString();
                    List<DXLb> lbs = SQLHelper.GetEntities<DXLb>("");
                    if (lbs.Count > 0)
                    {
                        DXLb lb = lbs[0];
                        pidLink.NavigateUrl = "LbManage.aspx?pid="+pid;
                        pidLink.Text = lb.LbName;

                    }
                }
                else
                {
                    pidLink.NavigateUrl = "LbManage.aspx";
                    pidLink.Text = "顶级类";
                }
                hpid.Value = pid;
                RebindGridView(false);
            }
        }
        #region 重新绑定GridView
        private void RebindGridView(bool addNewRow)
        {

            List<DXLb> lblist = SQLHelper.GetEntities<DXLb>("isDeleted=0 AND ParentId=" + hpid.Value + " ORDER BY orderid");
            if(addNewRow)
                lblist.Add(new DXLb());
            GV.DataSource = lblist;
            GV.DataBind();
        }
        #endregion

        private void clearMsg() { lblMsg.Text = ""; }
        protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            clearMsg();
            string name = e.CommandName;
            if ("MoveUp".Equals(name) || "MoveDown".Equals(name))
            {
                System.Web.UI.WebControls.Button btn = e.CommandSource as System.Web.UI.WebControls.Button;
                if (btn == null) return;
                int index = ((System.Web.UI.WebControls.GridViewRow)btn.Parent.Parent).RowIndex;
                MoveRow(index, "MoveUp".Equals(name));
            }
        }

        protected void GV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (hasRowEditing())
            {
                e.Cancel = true;
            }
            else
            {
                // 设置EditIndex
                this.GV.EditIndex = e.NewEditIndex;
                // 绑定数据
                this.RebindGridView(false);
                // 设置最大长度
                TextBox codeCtrl = this.getLbNameCtrl(e.NewEditIndex);
                codeCtrl.MaxLength = 40;
            }
        }

        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            clearMsg();
            if (!hasRowEditing())
            {
                GridViewRow row = this.GV.Rows[e.RowIndex];
                string id = this.GV.DataKeys[e.RowIndex].Value.ToString();

                // 删除数据
                if (clsLB.HasChild(id))
                {
                    lblMsg.Text = "您要删除的类下面有子类别，请先删除子类。";
                    return;
                }
                else
                {
                    clsLB.del(id);
                }

                // 设置EditIndex
                this.GV.EditIndex = -1;
                // 绑定数据
                this.RebindGridView(false);
            }
        }

        protected void GV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // 设置EditIndex
            this.GV.EditIndex = -1;
            // 绑定数据
            this.RebindGridView(false);
        }

        #region 保存
        protected void GV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            clearMsg();
            string lbname= this.getLbNameCtrl(e.RowIndex).Text;
            // 保存数据
            if (!validation(e.RowIndex))
            {
                return;
            }
            else
            {
                string id = this.GV.DataKeys[e.RowIndex].Value.ToString();
                if (string.IsNullOrEmpty(id))
                {
                    if (clsLB.lbnameExists(lbname))
                    {
                        lblMsg.Text =lbname+ "已存在。";
                        return;
                    }
                    else
                    {
                        clsLB.AddLb(this.getLbNameCtrl(e.RowIndex).Text, "0");
                    }
                }
                else
                {
                    clsLB.Update(id,lbname);
                }
            }
            // 设置EditIndex
            this.GV.EditIndex = -1;
            // 绑定数据
            this.RebindGridView(false);
        }
        #endregion

        #region 取列表控件
        private TextBox getLbNameCtrl(int index)
        {
            TextBox t = null;
            try
            {
                t = this.GV.Rows[index].Cells[1].Controls[0] as TextBox;
            }
            catch
            {
            }
            return t;
        }
        
        #endregion
        #region 是否有行处于编辑状态
        private bool hasRowEditing()
        {
            foreach (GridViewRow row in this.GV.Rows)
            {
                if (row.RowState == DataControlRowState.Edit || this.getLbNameCtrl(row.RowIndex) != null)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        private void MoveRow(int index, bool isUp)
        {
            if (!hasRowEditing())
            {
                if (index == -1)
                {
                    //if (this.GV.Rows.Count != 0)
                    //    this.SetMessage("I0206", MsgLevel.Info);
                    return;
                }
                else
                {
                    string id = this.GV.DataKeys[index].Value.ToString();
                    clsLB.lbOrderUP(id, isUp);
                    RebindGridView(false);
                }
            }
        }

        #region 保存校验
        private bool validation(int rowIndex)
        {
            TextBox lbnameCtrl = this.getLbNameCtrl(rowIndex);
            string lbname = lbnameCtrl.Text.Trim();

            // 类型编码不能为空
            if (string.IsNullOrEmpty(lbname))
            {
                lblMsg.Text = "类别名称不能为空。";
                return false;
            }
            //if (clsLB.lbnameExists(lbname))
            //{
            //    lblMsg.Text = "类别名称已存在。";
            //    return false;
            //}
            return true;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            clearMsg();
            if (!hasRowEditing())
            {
                // 设置EditIndex
                this.GV.EditIndex = this.GV.Rows.Count;
                // 绑定数据
                this.RebindGridView(true);
            }
        }



    }
}