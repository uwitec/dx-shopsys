<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="install.aspx.cs" Inherits="web1.install.install" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ul>
      <li>数据库IP：<asp:TextBox ID="txtIP" runat="server" Text="."></asp:TextBox>说明：“.”表示当前服务器。
        </li>
      <li>数据库用户名：<asp:TextBox ID="txtDbUsername" runat="server"></asp:TextBox>
        </li>
      <li>数据库密码：<asp:TextBox ID="txtDbPwd" runat="server"></asp:TextBox>
        </li>

        <li>数据库名称：<asp:TextBox ID="txtDbName" runat="server"></asp:TextBox></li>
<li>数据库实例名：<asp:TextBox ID="txtDbName2" runat="server"></asp:TextBox></li>
        <li>数据库版本：<asp:DropDownList ID="ddlVersion" runat="server">
        <asp:ListItem Text="SQL 2008" Value="MianDB_2008.sql"></asp:ListItem>
        <asp:ListItem Text="SQL 2005" Value="MianDB_2005.sql"></asp:ListItem>
        <asp:ListItem Text="SQL 2000" Value="MianDB_2000.sql"></asp:ListItem>
        </asp:DropDownList></li>
      <li><asp:Button ID="btnInstall" runat="server" Text="安装" 
              onclick="btnInstall_Click" /></li>
    </ul>
    </div>
    </form>
</body>
</html>
