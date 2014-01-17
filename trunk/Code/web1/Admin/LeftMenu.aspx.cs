using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web;
using System.Data;
using System.IO;
using web;
using DBFramework;
using DBFramework.Entities;
namespace web1.Admin
{
    public partial class LeftMenu : System.Web.UI.Page
    {
        private int parentid_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            com.adminLogin();
            //if (!clsAdmin.checkLogin())
            //{
            //    //alert.showOnly("您未登录，或登录超时。");
            //    //alert.ResponseScript(Page, "alert('您未登录，或登录超时。');parent.location.href='Login.aspx';");
            //    //Response.Write("您未登录，或登录超时。");
            //    //Response.End();
            //    return;
            //}
            //bindProPlb();
            //bindMenuProLb(MenuProLb);
            addTree(proLbTree, (TreeNode)null, 0);
        }
        protected void bindProPlb()
        {
            SQLHelper.Setup();
            List<DXLb> lbs = SQLHelper.GetEntities<DXLb>(" IsDeleted=0 AND parentid = 0 Order By OrderId");
            rptProLbParent.DataSource = lbs;
            rptProLbParent.DataBind();

            //MenuProLb

        }

        protected void bindMenuProLb(Menu MenuProLb)
        {
            SQLHelper.Setup();
            List<DXLb> lbs = SQLHelper.GetEntities<DXLb>(" IsDeleted=0 AND parentid = 0 Order By OrderId");
            foreach (DXLb lb in lbs)
            {
                MenuItem menuNode = new MenuItem();
                menuNode.Text = lb.LbName;
                menuNode.Value = lb.Id.ToString();
                menuNode.NavigateUrl = "Prod.aspx?lbid=" + lb.Id;
                menuNode.Enabled = true; 
                MenuProLb.Items.Add(menuNode);
                parentid_ = lb.Id;
                addChildMenu(menuNode);
            }
        }
        protected void addChildMenu(MenuItem pNode)
        {
            List<DXLb> lbs = SQLHelper.GetEntities<DXLb>(" IsDeleted=0 AND parentid = " + parentid_.ToString() + " Order By OrderId");
            foreach (DXLb lb in lbs)
            {
                MenuItem menuNode = new MenuItem(); 
                menuNode.Text = lb.LbName;
                menuNode.Value = lb.Id.ToString();
                menuNode.NavigateUrl = "Prod.aspx?lbid=" + lb.Id;
                menuNode.Enabled = true;
                pNode.ChildItems.Add(menuNode);
                parentid_ = lb.Id;
                addChildMenu(menuNode);
            }
        }

        void addTree(TreeView tree, TreeNode pNode, int pid)
        {
            if (pNode == null)
            {
                tree.Nodes.Clear();
            }
            SQLHelper.Setup();
            List<DXLb> lbs = SQLHelper.GetEntities<DXLb>(" IsDeleted=0 AND parentid = " + pid.ToString() + " Order By OrderId");
            foreach (DXLb lb in lbs)
            {
                TreeNode Node = new TreeNode();
                Node.Text = lb.LbName;
                Node.Value = lb.Id.ToString();
                
                Node.Expanded =false; //默认不展开
                if (pNode==null)
                {
                    tree.Nodes.Add(Node);
                    Node.NavigateUrl = null;
                    
                }
                else
                {
                    pNode.ChildNodes.Add(Node);
                    Node.NavigateUrl = "Prod.aspx?lbid=" + lb.Id;
                    Node.Target = "main";
                }
                addTree(tree, Node, lb.Id);
            }
        }

    }
}