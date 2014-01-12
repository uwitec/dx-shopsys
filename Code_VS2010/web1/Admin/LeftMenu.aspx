﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="web1.Admin.LeftMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <title></title>
    <script language="javascript">
        function expand(el) {
            childObj = document.getElementById("child" + el);

            if (childObj.style.display == 'none') {
                childObj.style.display = 'block';
            }
            else {
                childObj.style.display = 'none';
            }
            return;
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
    <table height="100%" cellspacing="0" cellpadding="0" width="170" background="images/menu_bg.jpg"
        border="0">
        <tr>
            <td valign="top" align="middle">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="150" border="0">
                    <tr height="22" onclick="expand(1)" class="menuParentTr">
                        <td style="padding-left: 40px" background="images/menu_bt.jpg" align="left">
                            <a class="menuParent" href="javascript:void(0);">新闻管理</a>
                        </td>
                    </tr>
                    <tr height="4">
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="child1" style="display: block" cellspacing="0" cellpadding="0" width="150"
                    border="0">
                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="news.aspx?lbid=2" target="main">公司新闻</a>
                        </td>
                    </tr>
                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="news.aspx?lbid=3" target="main">产品新闻</a>
                        </td>
                    </tr>
                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="news.aspx?lbid=22" target="main">活动新闻</a>
                        </td>
                    </tr>
                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="news.aspx?lbid=65" target="main">测评体验</a>
                        </td>
                    </tr>
                    <tr height="4">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="150" border="0">
                    <tr height="22" onclick="expand(2)" class="menuParentTr">
                        <td style="padding-left: 40px" background="images/menu_bt.jpg" align="left">
                            <a class="menuParent" href="javascript:void(0);">产品中心</a>
                        </td>
                    </tr>
                    <tr height="4">
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="child2" style="display: block" cellspacing="0" cellpadding="0" width="150"
                    border="0">
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="IconManage.aspx" target="main">功能图标管理</a>
                        </td>
                    </tr>



                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="ColorManage.aspx" target="main">颜色管理</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="proLb.aspx" target="main">类别管理</a>
                        </td>
                    </tr>
                        <asp:Repeater ID="rptProLbParent" runat="server">
                        <ItemTemplate>
                    <tr>
                        <td align="middle" width="30" height="20">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">

                        <a class="menuChild" href="prod.aspx?pid=<%#Eval("lbid") %>" target="main"><%#Eval("lbname") %></a>
                        &nbsp;<a class="menuChild" href="prod.aspx?pid=<%#Eval("lbid") %>" target="main">管理</a>|<a href="ProAdd.aspx?pid=<%#Eval("lbid") %>" target="main">添加</a>
                        </td>
                    </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    <tr height="4">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>

                <table cellspacing="0" cellpadding="0" width="150" border="0">
                    <tr height="22" onclick="expand(3)" class="menuParentTr">
                        <td style="padding-left: 40px" background="images/menu_bt.jpg" align="left">
                            <a class="menuParent" href="javascript:void(0);">销售网点</a>
                        </td>
                    </tr>
                    <tr height="4">
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="child3" style="display: block" cellspacing="0" cellpadding="0" width="150"
                    border="0">
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="ProvinceManage.aspx" target="main">省份城市管理</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="NetWorkManage.aspx" target="main">销售网点管理</a>
                        </td>
                    </tr>

                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="jiameng.aspx" target="main">我要加盟管理</a>
                        </td>
                    </tr>


                    <tr height="4">
                        <td colspan="2">
                        </td>
                    </tr>
                </table>

                <table cellspacing="0" cellpadding="0" width="150" border="0">
                    <tr height="22" onclick="expand(7)" class="menuParentTr">
                        <td style="padding-left: 40px" background="images/menu_bt.jpg" align="left">
                            <a class="menuParent" href="javascript:void(0);">系统管理</a>
                        </td>
                    </tr>
                    <tr height="4">
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="child7" style="display: block" cellspacing="0" cellpadding="0" width="150"
                    border="0">
<%--                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="FriendLink.aspx" target="main">友情链接</a>                        </td>
                    </tr>
                    <tr height="20">
                        <td align="middle" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="MemberManage.aspx" target="main">会员管理</a>                        </td>
                    </tr>   
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="execSql.aspx" target="main">执行SQL</a></td>
                    </tr>
                    --%>

                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="admin.aspx" target="main">管理员列表</a></td>
                    </tr>

                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" href="pwd.aspx" target="main">修改口令</a>                        </td>
                    </tr>
                    <tr>
                        <td align="middle" height="20" width="30">
                            <img height="9" src="images/menu_icon.gif" width="9">
                        </td>
                        <td align="left">
                            <a class="menuChild" onclick="if (!confirm('确定要退出吗？')) return false;"
                                href="Login.aspx?act=out" target="_top">退出系统</a>                        </td>
                    </tr>
              </table>
            </td>
            <td width="1" bgcolor="#d1e6f7">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
