<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEdit.aspx.cs" Inherits="web.admin.AdminEdit" %>

<%@ Register src="../CheckAdminLogin.ascx" tagname="CheckAdminLogin" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<h4>&nbsp;&nbsp;修改用户信息</h4>
    <table width="500" border="0" cellpadding="0" cellspacing="0" class="tabelStyle">
      <tr>
        <td width="98">用户名：</td>
        <td width="401">
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:HiddenField ID="hid" runat="server" />
          </td>
      </tr>
      <tr>
        <td>密　码：</td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox> <span class=red>说明：如不修改密码，请留空。</span>
          </td>
      </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" Text="修改" onclick="Button1_Click" CssClass="tbutton" />
            &nbsp;
            <input type=button value=返回 onclick="window.history.back();" Class="tbutton" />
          </td>
      </tr>
    </table>
</form>
</body>
</html>
