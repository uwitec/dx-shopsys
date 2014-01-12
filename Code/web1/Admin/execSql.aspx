<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="execSql.aspx.cs" Inherits="web1.Admin.execSql" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="tbxSql" runat="server" TextMode="MultiLine" Width="472px" 
            Height="200px"></asp:TextBox>

        <br />
        <asp:Button ID="btnRun" runat="server" Text="Run" onclick="btnRun_Click" />
        <div>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
    </div>
    <div>
        <asp:GridView ID="gv" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
