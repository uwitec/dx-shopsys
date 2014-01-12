<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManage.aspx.cs" Inherits="web1.Admin.MemberManage" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h4>&nbsp;&nbsp;
    当前栏目：会员管理
    &nbsp;&nbsp;
        <asp:Button ID="btnAddMember" runat="server" Text="添加会员" 
            onclick="btnAddMember_Click"/>
    </h4>
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
            AutoGenerateColumns="False" Width="100%" 
            onpageindexchanging="GV_PageIndexChanging" DataKeyNames="userid" 
        onrowcommand="GV_RowCommand" onrowdeleting="GV_RowDeleting" 
        onrowediting="GV_RowEditing"  
        onrowcancelingedit="GV_RowCancelingEdit">
        <Columns>
            <asp:BoundField DataField="userid" HeaderText="id" SortExpression="userid" Visible="false" />
            <asp:BoundField DataField="email" HeaderText="邮箱" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="username" HeaderText="昵称" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="RegTime" HeaderText="注册时间" ItemStyle-HorizontalAlign="Left" ReadOnly="true"/>
            <asp:BoundField DataField="pwd" HeaderText="密码" ItemStyle-HorizontalAlign="Left" ReadOnly="true"/>

           <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
            <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Edit" /> 
            <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="Delete" OnClientClick="return confirm('确实要删除吗？')" />
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
    <cc2:UcfarPager ID="PagerMember" runat="Server"  PageSize="15"  PagePara="Page"  PageStyle="前后缩略">
</cc2:UcfarPager>
    <asp:HiddenField ID="hpwd" runat="server" Value="mian123456" />

    <br />说明：在后台新增的用户，默认密码为 mian123456，MD5值为：<%=md5("mian123456") %>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>



    </form>
</body>
</html>
