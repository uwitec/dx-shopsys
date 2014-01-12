<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoManage.aspx.cs" Inherits="web1.Admin.PhotoManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图片管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
<style>

ul#picList
{
    padding:0; margin:0;
}
ul#picList li
{
    padding:0; margin:0; width:100px; height:100px; float:left; border:1px solid #ccc; margin:2px; text-align:center; line-height:20px; font:12px;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <h4>&nbsp;&nbsp;
    当前栏目：<asp:HyperLink ID="hlParentLbname" runat="server">精彩瞬间</asp:HyperLink> > 
        <asp:Label ID="lblLbname" runat="server" Text=""></asp:Label> > 图片管理 
    &nbsp;&nbsp;
        <asp:Button ID="btnAddPic" runat="server" Text="添加图片" 
            onclick="btnAddPic_Click" />
    </h4>
    <asp:HiddenField ID="hlbid" runat="server" />
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
            AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="20" 
            onpageindexchanging="GV_PageIndexChanging" DataKeyNames="NewsID" 
        onrowcommand="GV_RowCommand" onrowdeleting="GV_RowDeleting" 
        onrowediting="GV_RowEditing" onrowupdating="GV_RowUpdating" 
        onrowcancelingedit="GV_RowCancelingEdit" onrowdatabound="GV_RowDataBound">
        <Columns>
            <asp:BoundField DataField="newsid" HeaderText="id" SortExpression="newsid" Visible="false" />
            <asp:BoundField DataField="title" HeaderText="图片标题" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="EditTime" HeaderText="更新时间" ItemStyle-HorizontalAlign="Left" />

            <asp:TemplateField HeaderText="图片" ItemStyle-Width="350">
            <ItemTemplate>
            <ul id="picList">
            <li><a href="<%# Eval("pic") %>" target="_blank"><img src="<%# Eval("picSmall") %>" alt="单击查看原图" style="display:<%# (Eval("picSmall").ToString()=="")?"none":"block" %>"/></a>缩略图</li>
            
            <%--<li><a href="<%# Eval("pic") %>" target="_blank"><img src="<%# Eval("pic") %>" alt="单击放大查看" style="display:<%# (Eval("pic").ToString()=="")?"none":"block" %>"/></a>原图</li>--%>
            </ul>
            
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
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
