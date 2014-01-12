<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProPicManage.aspx.cs" Inherits="web1.Admin.ProPicManage" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品图片管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
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
    当前栏目：<asp:HyperLink ID="hlParentLbname" runat="server">产品管理</asp:HyperLink> > 
        <asp:Label ID="lblLbname" runat="server" Text=""></asp:Label> > 图片管理 
    &nbsp;&nbsp;
        <asp:Button ID="btnAddPic" runat="server" Text="添加图片" 
            onclick="btnAddPic_Click" />
    </h4>
    <asp:HiddenField ID="hlbid" runat="server" />
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
            AutoGenerateColumns="False" Width="100%" 
            onpageindexchanging="GV_PageIndexChanging" DataKeyNames="NewsID" 
        onrowcommand="GV_RowCommand" onrowdeleting="GV_RowDeleting" 
        onrowediting="GV_RowEditing" onrowupdating="GV_RowUpdating" 
        onrowcancelingedit="GV_RowCancelingEdit"
        onrowcreated="GV_RowCreated">
        <Columns>
            <asp:BoundField DataField="newsid" HeaderText="id" SortExpression="newsid" Visible="false" />
            <asp:BoundField DataField="title" HeaderText="图片标题" ItemStyle-HorizontalAlign="Left" />
            <%--<asp:BoundField DataField="EditTime" HeaderText="更新时间" ItemStyle-HorizontalAlign="Left" />--%>

            <asp:TemplateField HeaderText="图片">
            <ItemTemplate><a href="<%# Eval("pic") %>" target="_blank"><img src="<%# Eval("picSmall") %>" height="60" alt="单击放大查看" style="display:<%# (Eval("picSmall").ToString()=="")?"none":"block" %>"/></a>
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="颜色">
                <ItemTemplate>
                    <div>
                      <div style="background-color:<%#Eval("colorValue") %>;"><%#Eval("ColorText")%><div>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlColor" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="-1" Text="不限颜色"></asp:ListItem>
                    </asp:DropDownList>
                    
                </EditItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="图片类型">
                <ItemTemplate>
                <%#Eval("imgTypeName")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlpro_imgTypeid" runat="server" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </EditItemTemplate>
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
            <asp:BoundField DataField="OrderId" HeaderText="排序" ItemStyle-HorizontalAlign="Left" />
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
    <cc2:UcfarPager ID="Pager0" runat="Server"  PageSize="20"  PagePara="Page"  PageStyle="前后缩略">
</cc2:UcfarPager>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <div style="padding:20px; padding-top:4px;">
    说明：<br />
    1. 每个颜色上传一张缩略图，在产品列表中调用。<br />
    2. 每个颜色上传四张导航图片，在产品展示页中的多图展示部分显示。<br />
    3. 实图欣赏是产品展示页中主体内容显示的图片，数量不限。<br />
    4. 图片大小请限制在200KB以内。<br />
    5. 每个产品最多支持五种颜色，超出的将不显示。
    </div>


    </form>
</body>
</html>
