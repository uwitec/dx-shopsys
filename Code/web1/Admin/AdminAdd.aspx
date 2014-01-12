<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAdd.aspx.cs" Inherits="web.admin.AdminAdd" %>
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
<h4>&nbsp;&nbsp;用户信息管理</h4>
<h4>&nbsp;&nbsp;添加新用户</h4>
    <table width="500" border="0" cellpadding="0" cellspacing="0" class="tabelStyle">
      <tr>
        <td width="98">用户名：</td>
        <td width="401">
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        <asp:HiddenField ID="hideParentid" runat="server" />
          </td>
      </tr>
      <tr>
        <td>密　码：</td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
          </td>
      </tr>
      <tr style="display:none;">
        <td>权　限：</td>
        <td>
            <asp:CheckBoxList ID="RoleList" runat="server" CssClass="noBorder">
            </asp:CheckBoxList>
          </td>
      </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" Text="添加" onclick="Button1_Click" />
          </td>
      </tr>
    </table>

</form>
</body>
</html>
