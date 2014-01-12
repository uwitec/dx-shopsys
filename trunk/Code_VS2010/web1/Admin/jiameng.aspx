<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jiameng.aspx.cs" Inherits="web1.Admin.jiameng" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我要加盟管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            &nbsp;&nbsp; 当前栏目：我要加盟管理
        </h4>
        <asp:HiddenField ID="hlbid" runat="server" />
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" DataKeyNames="id" AutoGenerateColumns="False" 
            Width="100%" AllowPaging="True" PageSize="20" OnPageIndexChanging="GV_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="xingming" HeaderText="姓名" SortExpression="xingming" />
                <asp:BoundField DataField="xingbie" HeaderText="性别" SortExpression="xingbie" />
                <asp:BoundField DataField="cynx" HeaderText="从业年限" SortExpression="cynx" />
                <asp:BoundField DataField="tel" HeaderText="电话" SortExpression="tel" />
                <asp:BoundField DataField="email" HeaderText="邮箱" SortExpression="email" />
                <asp:BoundField DataField="sheng" HeaderText="省" SortExpression="sheng" />
                <asp:BoundField DataField="shi" HeaderText="市" SortExpression="shi" />
                <asp:BoundField DataField="message" HeaderText="留言" SortExpression="message" />
                <asp:BoundField DataField="AddTime" HeaderText="提交时间" SortExpression="AddTime" />

                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="jiameng.aspx?act=del&id={0}"
                    HeaderText="删除" Text="删除" />
            </Columns>
            <EmptyDataTemplate>
                暂无内容
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
