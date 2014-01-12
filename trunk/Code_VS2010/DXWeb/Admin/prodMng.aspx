<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prodMng.aspx.cs" Inherits="DXWeb.Admin.prodMng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3>产品管理</h3>
    <a href="ProAdd.aspx">添加产品</a>
    <asp:Repeater ID="rpt" runat="server">
    <HeaderTemplate>
    <ul>
    
    </HeaderTemplate>
    <ItemTemplate>
      <li><%#Eval("Name") %></li>
    </ItemTemplate>
    <FooterTemplate>
    
    </ul>
    </FooterTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
