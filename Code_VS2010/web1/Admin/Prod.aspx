﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prod.aspx.cs" Inherits="web1.Admin.Prod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            &nbsp;&nbsp; 当前栏目：<%Response.Write(pname); %>
            &nbsp;&nbsp; <a href="ProAdd.aspx?pid=<%Response.Write(pid); %>">添加</a>
        </h4>
        <asp:HiddenField ID="hPid" runat="server" />
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" DataKeyNames="NewsID" AutoGenerateColumns="False" onrowcommand="GV_RowCommand"
            Width="100%" AllowPaging="True" PageSize="20" OnPageIndexChanging="GV_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="title" HeaderText="标题" SortExpression="title" />
                <asp:BoundField DataField="AddTime" HeaderText="添加时间" SortExpression="EditTime" />
                <asp:BoundField DataField="EditTime" HeaderText="修改时间" SortExpression="EditTime" />
                <asp:TemplateField HeaderText="置顶" Visible="false">
                    <ItemTemplate>
                        <%# setTopBtn(Eval("NewsID").ToString()) %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="首页显示" Visible="false">
                    <ItemTemplate>
                        <%# setIsIndex(Eval("NewsID").ToString())%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="图片管理">
                    <ItemTemplate>
                        <asp:Button ID="btnPicMng" runat="server" Text="管理" CommandName="PicMng" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="NewsID,pid" DataNavigateUrlFormatString="ProAdd.aspx?id={0}&pid={1}"
                    HeaderText="修改" Text="修改" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsID,pid" DataNavigateUrlFormatString="Prod.aspx?act=del&pid={1}&id={0}"
                    HeaderText="删除" Text="删除" />
            </Columns>
            <EmptyDataTemplate>
                暂无内容
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
