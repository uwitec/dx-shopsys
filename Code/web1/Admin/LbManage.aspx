<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LbManage.aspx.cs" Inherits="web1.Admin.LbManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品类别管理</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<div>
    <h4>&nbsp;&nbsp;类别管理  上级类：<asp:HyperLink ID="pidLink" runat="server">顶级类</asp:HyperLink><asp:Button ID="btnAdd" runat="server" Text="添加大类" 
            onclick="btnAdd_Click" />
            <asp:HiddenField ID="hpid" runat="server" Value="0" />
            </h4>
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" 
        DataKeyNames="id" AutoGenerateColumns="False" 
            Width="100%" AllowPaging="True" PageSize="5" 
        onrowcancelingedit="GV_RowCancelingEdit" onrowcommand="GV_RowCommand" 
        onrowdeleting="GV_RowDeleting" onrowediting="GV_RowEditing" onrowupdating="GV_RowUpdating"
            
            >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="true" ItemStyle-HorizontalAlign="Left"/>
                <%--<asp:BoundField DataField="lbname" HeaderText="类别名称" ItemStyle-HorizontalAlign="Left"/>--%>
                <asp:HyperLinkField HeaderText="类别名称" ItemStyle-HorizontalAlign="Left" DataNavigateUrlFormatString="LbManage.aspx?pid={0}" DataNavigateUrlFields="id" DataTextField="lbname" />


                <asp:TemplateField HeaderText=" 操作 " ShowHeader="False" ItemStyle-Width="180px">
                    <EditItemTemplate>
                        <asp:Button Text="保存" ID="Button_Update" runat="server"  CommandName="Update" OperateType="Edit"/>
                        <asp:Button CssClass="edit" ID="Button_Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="撤销" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button CssClass="edit" ID="Button_Edit" runat="server" OperateType="Edit" CausesValidation="False" CommandName="Edit"
                            Text="编辑" />
                        <asp:Button CssClass="edit" ID="Button_Delete" runat="server" OperateType="Delete" CausesValidation="False" CommandName="Delete"
                            Text="删除" OnClientClick="return confirm('确定要删除吗？');" />
                                                            
                            <asp:Button CssClass="edit" ID="Button_Up" runat="server" OperateType="Edit" CausesValidation="False" CommandName="MoveUp"
                            Text="上移" />
                            <asp:Button CssClass="edit" ID="Button_Down" runat="server" OperateType="Edit" CausesValidation="False" CommandName="MoveDown"
                            Text="下移" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                暂无类别
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    
    </form>
</body>
</html>
