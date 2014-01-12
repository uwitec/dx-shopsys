<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetworkAdd.aspx.cs" Inherits="web1.Admin.NetworkAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <h4>
        &nbsp;&nbsp;销售网点</h4>
    <asp:HiddenField ID="hLbid" runat="server" />
    <asp:HiddenField ID="hID" runat="server" />
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" class="tabelStyle">
        <tr>
            <th colspan="2">
                <asp:Label ID="lblOper" runat="server" Text="添加"></asp:Label>
            </th>
        </tr>
        <tr>
            <td width="21%">
                店名：
            </td>
            <td width="79%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px" />
            </td>
        </tr>
        <tr>
            <td width="21%">
                地区：
            </td>
            <td width="79%">
                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" 
            AppendDataBoundItems="true" 
                    onselectedindexchanged="ddlProvince_SelectedIndexChanged" >
                <asp:ListItem Value="0" Text="请选择省份"></asp:ListItem>
        </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server" 
            AppendDataBoundItems="false" >
                <asp:ListItem Value="0" Text="请选择城市"></asp:ListItem>
        </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                联系方式：
            </td>
            <td>
            <asp:TextBox ID="tbxDesc" runat="server" Width="200px" /> 不少于8位数字，请勿填写手机号码
            </td>
        </tr>

        <tr>
            <td>
                详细地址：
            </td>
            <td>
            <asp:TextBox ID="tbxBody" runat="server" Width="350px" />
            </td>
        </tr>

        <tr>
            <td colspan="2" align="center" valign="middle">
                <asp:Button ID="btnAdd" Text="修改并保存" runat="server" 
                    OnClick="btnAdd_Click" CssClass="tbutton" OnClientClick="return check();" />
                &nbsp;
                <input type="button" value="返回" onclick="window.history.back();" class="tbutton" />
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript">
        function check() {
            if (document.getElementById("txtTitle").value == "") {
                alert("请填写店名");
                document.getElementById("txtTitle").focus();
                return false;
            }
//            if (document.getElementById("tbxDesc").value == "") {
//                alert("请填写联系方式");
//                document.getElementById("tbxDesc").focus();
//                return false;
//            }
//            if (document.getElementById("tbxDesc").value.length <8 ) {
//                alert("联系方式不少于8位数字，请勿填写手机号码");
//                document.getElementById("tbxDesc").focus();
//                return false;
//            }
            if (document.getElementById("tbxBody").value == "") {
                alert("请填写详细地址");
                document.getElementById("tbxBody").focus();
                return false;
            }
        }
    </script>
</body>
</html>
