<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="web1.admin.admin" %>
<%@ Register src="../CheckAdminLogin.ascx" tagname="CheckAdminLogin" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<uc1:CheckAdminLogin ID="CheckAdminLogin1" runat="server" />
<h4>&nbsp;&nbsp;后台用户管理   <a href="AdminAdd.aspx">添加管理员</a></h4>
<asp:GridView ID="GV" runat="server" CssClass="tabelStyle" Width="100%" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="用户名" 
                    SortExpression="Name" />
                <asp:BoundField DataField="LoginTime" HeaderText="登录时间" 
                    SortExpression="LoginTime" />
                <asp:HyperLinkField DataNavigateUrlFields="id" 
                    DataNavigateUrlFormatString="AdminEdit.aspx?id={0}" HeaderText="修改" Text="修改" />
                <asp:HyperLinkField DataNavigateUrlFields="id" 
                    DataNavigateUrlFormatString="admin.aspx?act=del&id={0}" HeaderText="删除" Text="删除" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
</form>
</body>
</html>
