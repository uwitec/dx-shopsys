<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="web1.Admin.MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr height="28">
            <td background="images/title_bg1.jpg">
                当前位置:
            </td>
        </tr>
        <tr>
            <td bgcolor="#b1ceef" height="1">
            </td>
        </tr>
        <tr height="20">
            <td background="images/shadow_bg.jpg">
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr height="100">
            <td align="middle" width="100">
                <img height="100" src="images/admin_p.gif" width="90">
            </td>
            <td width="60">
                &nbsp;
            </td>
            <td>
                <table height="100" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            当前时间：<%=DateTime.Now.ToString("YY-MM-DD HH:mm") %>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; font-size: 16px">
                            <%=Session["AdminName"].ToString() %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            欢迎进入网站管理中心！
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="10">
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
        <tr height="20">
            <td>
            </td>
        </tr>
        <tr height="22">
            <td style="padding-left: 20px; font-weight: bold; color: #ffffff" align="middle"
                background="images/title_bg2.jpg">
                您的相关信息
            </td>
        </tr>
        <tr bgcolor="#ecf4fc" height="12">
            <td>
            </td>
        </tr>
        <tr height="20">
            <td>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="2" width="95%" align="center" border="0">
        <tr>
            <td align="right" width="100">
                登陆帐号：
            </td>
            <td style="color: #880000">
                <%=Session["AdminName"].ToString() %>
            </td>
        </tr>
        <tr>
            <td align="right">
                网站开发QQ：
            </td>
            <td style="color: #880000">
                396068801
            </td>
        </tr>
            <td align="right">
                电话：
            </td>
            <td style="color: #880000">
                13520751796
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
