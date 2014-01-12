<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProAdd.aspx.cs" Inherits="DXWeb.Admin.ProAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加产品</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
	<script type="text/javascript" src="../swfupload/swfupload.js"></script>
	<script type="text/javascript" src="../swfupload/handlers.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <h4>&nbsp;&nbsp;<asp:Label ID="lblPname" runat="server"></asp:Label></h4>
    <div>
    标题：<asp:TextBox ID="tbxTitle" runat="server"></asp:TextBox><br />
    内容：<asp:TextBox ID="tbxBody" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnAdd" runat="server" Text="添加" onclick="btnAdd_Click" />
    </div>
    </form>
</body>
</html>
