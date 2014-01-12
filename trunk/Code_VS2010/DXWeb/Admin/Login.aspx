<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="web1.Admin.Login"  EnableViewStateMac="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理中心登陆</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        if (self != top) { top.location = self.location; }

        window.onload = function () {
            //document.form1.txtUserName.focus();
            $("#txtUserName").focus();
        }

</script>
</head>
<body style="background-color:#002779;">
    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="#002779"
        border="0">
        <tr>
            <td align="middle">
                <table cellspacing="0" cellpadding="0" width="468" border="0">
                    <tr>
                        <td><img height="23" src="images/login_1.jpg" width="468"></td>
                    </tr>
                    <tr>
                        <td><img height="147" src="images/login_2.jpg" width="468"></td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="468" bgcolor="#ffffff" border="0">
                    <tr>
                        <td width="16"><img height="122" src="images/login_3.jpg" width="16"></td>
                        <td align="middle">
                            <table cellspacing="0" cellpadding="0" width="230" border="0">
                                
                                <tr height="5">
                                    <td width="5">
                                    </td>
                                    <td width="56">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr height="36">
                                    <td>
                                    </td>
                                    <td>
                                        用户名
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="LoginInput" MaxLength="30"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr height="36">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        口 令
                                    </td>
                                    <td>
<asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="LoginInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr height="5">
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnLogin" ImageUrl="images/bt_login.gif" runat="server" 
                                            onclick="btnLogin_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16"><img height="122" src="images/login_4.jpg" width="16"></td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="468" border="0">
                    <tr>
                        <td><img height="16" src="images/login_5.jpg" width="468"></td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="468" border="0">
                    <tr>
                        <td align="right">
                            <a href="http://www.865171.cn/" target="_blank">
                                <img height="26" src="images/login_6.gif" width="165" border="0"></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
