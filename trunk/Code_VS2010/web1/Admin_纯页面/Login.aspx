<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="web1.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理中心登陆</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body onload="document.form1.name.focus();" style="background-color:#002779;">
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
                                        <input style="border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid;
                                            border-bottom: #000000 1px solid" maxlength="30" size="24" value="管理员"
                                            name="name">
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
                                        <input style="border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid;
                                            border-bottom: #000000 1px solid" type="password" maxlength="30" size="24" value="www.865171.cn"
                                            name="pass">
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
                                        <input type="image" height="18" width="70" src="images/bt_login.gif" onclick="window.location.href='';">
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
