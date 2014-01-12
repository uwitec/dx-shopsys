<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorManage.aspx.cs" Inherits="web1.Admin.ColorManage" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>颜色管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h4>&nbsp;&nbsp;
    当前栏目：<asp:HyperLink ID="hlParentLbname" runat="server">商品类别管理</asp:HyperLink> > &nbsp;&nbsp;
        <asp:Button ID="btnAddPic" runat="server" Text="添加" 
            onclick="btnAddPic_Click" />
    </h4>
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
            AutoGenerateColumns="False" Width="100%" 
            onpageindexchanging="GV_PageIndexChanging" DataKeyNames="id" 
        onrowcommand="GV_RowCommand" onrowdeleting="GV_RowDeleting" 
        onrowediting="GV_RowEditing" 
        onrowcancelingedit="GV_RowCancelingEdit">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="false" />
            <asp:BoundField DataField="ColorName" HeaderText="颜色名称" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="ColorValue" HeaderText="色值" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="orderid" HeaderText="排序" ItemStyle-HorizontalAlign="Left" />
           <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
            <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Edit" />
            <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="Delete" />
            </ItemTemplate>
            <EditItemTemplate>
            <asp:Button ID="btnSave" runat="server" Text="保存" CommandName="Save" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
            </EditItemTemplate>
            </asp:TemplateField>

        </Columns>
        <EmptyDataTemplate>
        暂无内容
        </EmptyDataTemplate>
    </asp:GridView>
    <cc2:UcfarPager ID="Pager1" runat="Server"  PageSize="15"  PagePara="Page"  PageStyle="前后缩略">
</cc2:UcfarPager>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <div style="padding:20px; padding-top:4px;">
    说明：<br />
    1. 排序只能填写数字，显示为四位数。比如填写103，显示结果为 0103；输入 3 则显示为 0003。<br />
    </div>

    </form>
</body>
</html>
