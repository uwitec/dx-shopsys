<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="web1.Admin.Header" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" background="images/header_bg.jpg"
        border="0">
        <tr>
            <td width="260"><img height="56" src="images/header_left.jpg" width="260"></td>
            <td style="font-weight: bold; color: #fff; padding-top: 20px" align="middle">
                当前用户：admin &nbsp;&nbsp; <a style="color: #fff" href="" target="main">修改口令</a> &nbsp;&nbsp;
                <a style="color: #fff" onclick="if (confirm('确定要退出吗？')) return true; else return false;"
                    href="" target="_top">退出系统</a>
            </td>
            <td align="right" width="268"><img height="56" src="images/header_right.jpg" width="268" /></td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr bgcolor="#1c5db6" height="4">
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
