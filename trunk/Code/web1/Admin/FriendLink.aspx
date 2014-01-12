<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLink.aspx.cs" Inherits="web1.Admin.FriendLink" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>友情链接管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h4>&nbsp;&nbsp;
    当前栏目：<asp:HyperLink ID="hlParentLbname" runat="server">友情链接</asp:HyperLink> > &nbsp;&nbsp;
        <asp:Button ID="btnAddPic" runat="server" Text="添加" 
            onclick="btnAddPic_Click" />
    </h4>
    <asp:HiddenField ID="hlbid" runat="server" />
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
            AutoGenerateColumns="False" Width="100%" 
            onpageindexchanging="GV_PageIndexChanging" DataKeyNames="NewsID" 
        onrowcommand="GV_RowCommand" onrowdeleting="GV_RowDeleting" 
        onrowediting="GV_RowEditing" onrowupdating="GV_RowUpdating" 
        onrowcancelingedit="GV_RowCancelingEdit" onrowdatabound="GV_RowDataBound">
        <Columns>
            <asp:BoundField DataField="newsid" HeaderText="id" SortExpression="newsid" Visible="false" />
            <asp:BoundField DataField="title" HeaderText="友情链接名称" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="href" HeaderText="链接地址" ItemStyle-HorizontalAlign="Left" />


            <asp:BoundField DataField="EditTime" HeaderText="更新时间" ItemStyle-HorizontalAlign="Left" />

            <asp:TemplateField HeaderText="图片" ItemStyle-Width="350">
            <ItemTemplate>
            <a href="<%# Eval("pic") %>" target="_blank"><img width="100" src="<%# Eval("pic") %>" alt="单击查看原图" style="display:<%# (Eval("pic").ToString()=="")?"none":"block" %>"/></a>
            
            </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="更新图片">
            <ItemTemplate>
                <asp:FileUpload ID="FileUpload1" runat="server"/>
                <asp:Button ID="btnUpload" runat="server" Text="上传" CommandName="upFile" />
            </ItemTemplate>
            <EditItemTemplate>
            <asp:FileUpload ID="FileUpload1" runat="server"/>
            </EditItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
            <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Edit" />
            <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="Delete" />
            </ItemTemplate>
            <EditItemTemplate>
            <asp:Button ID="btnSave" runat="server" Text="保存" CommandName="Save" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
            </EditItemTemplate>
            </asp:TemplateField>

        </Columns>
        <EmptyDataTemplate>
        暂无内容
        </EmptyDataTemplate>
    </asp:GridView>
    <cc2:UcfarPager ID="Pager1" runat="Server"  PageSize="5"  PagePara="Page"  PageStyle="前后缩略">
</cc2:UcfarPager>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
