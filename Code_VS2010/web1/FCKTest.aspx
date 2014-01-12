<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FCKTest.aspx.cs" Inherits="web1.FCKTest" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<%--    <FCKeditorV2:FCKeditor ID="FCKeditor1" FormatSource="true" EnableSourceXHTML="true" runat="server" Height="50" ToolbarSet="VideoWindow"></FCKeditorV2:FCKeditor>
    <FCKeditorV2:FCKeditor ID="FCKeditor2" FormatSource="true" EnableSourceXHTML="true" runat="server" Height="100" ToolbarSet="Basic"></FCKeditorV2:FCKeditor>--%>
    <FCKeditorV2:FCKeditor ID="FCKeditor3" FormatSource="true" EnableSourceXHTML="true" runat="server" Height="150" ToolbarSet="Default"></FCKeditorV2:FCKeditor>
    </div>

    </form>
</body>
</html>
