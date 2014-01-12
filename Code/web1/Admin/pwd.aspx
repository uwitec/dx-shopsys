<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwd.aspx.cs" Inherits="web.admin.pwd" %>
<%@ Register src="../CheckAdminLogin.ascx" tagname="CheckAdminLogin" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改密码</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
<table width="500" border="0" cellpadding="0" cellspacing="1" class="tabelStyle">
      <tr>
        <th colspan="2">修改密码</th>
        </tr>
      <tr>
        <td align="left" class="style1">用户名：</td>
        <td align="left">
        <%Response.Write(adminName); %>
        </td>
      </tr>
      
      <tr>
        <td align="left" class="style1">原密码：</td>
        <td align="left">
  <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                runat="server" ControlToValidate="txtPwd" ErrorMessage="请填写原密码"></asp:RequiredFieldValidator>
                        </td>
      </tr>
      <tr>
        <td align="left" class="style1">新密码：</td>
        <td align="left">
            <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                runat="server" ControlToValidate="txtNewPwd" ErrorMessage="请填写新密码"></asp:RequiredFieldValidator>
                        </td>
      </tr>
      <tr>
        <td align="left" class="style1">确认新密码：</td>
        <td align="left">
            <asp:TextBox ID="txtNewPwd2" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtNewPwd2" ErrorMessage="请填写确认新密码"></asp:RequiredFieldValidator>
          </td>
      </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />
          </td>
        </tr>
    </table>
    </form>
</body>
</html>
