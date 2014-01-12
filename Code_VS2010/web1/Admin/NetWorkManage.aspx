<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NetWorkManage.aspx.cs" Inherits="web1.Admin.NetWorkManage" %>
<%@ Register Assembly="UcfarPager" Namespace="UcfarPagerControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售网点管理</title>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
    <link href="pagerstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            &nbsp;&nbsp; 当前栏目：销售网点管理
            &nbsp;&nbsp; <a href="NetworkAdd.aspx">添加</a>
        </h4>
        <asp:HiddenField ID="hpid" runat="server" />
        <div>
        筛选： 
        <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" 
            AppendDataBoundItems="true" 
                onselectedindexchanged="ddlProvince_SelectedIndexChanged">
                <asp:ListItem Text="全部省份" Value=""></asp:ListItem>
        </asp:DropDownList>
        </div>
        <asp:GridView ID="GV" runat="server" CssClass="tabelStyle" DataKeyNames="NewsID" AutoGenerateColumns="False"
            Width="100%" AllowPaging="True" PageSize="20" OnPageIndexChanging="GV_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="title" HeaderText="店名" SortExpression="title" />
                <asp:BoundField DataField="provinceName" HeaderText="省" SortExpression="provinceName" />
                <asp:BoundField DataField="cityName" HeaderText="市" SortExpression="cityName" />
                <asp:BoundField DataField="Description" HeaderText="联系方式" SortExpression="Description" />
                <asp:BoundField DataField="NewsBody" HeaderText="详细地址" SortExpression="NewsBody" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsID" DataNavigateUrlFormatString="NetworkAdd.aspx?id={0}"
                    HeaderText="修改" Text="修改" />
                <asp:HyperLinkField DataNavigateUrlFields="NewsID,ProvinceId" DataNavigateUrlFormatString="NetWorkManage.aspx?act=del&id={0}&ProvinceId={1}"
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
