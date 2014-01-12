<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎使用 ASP.NET!
    </h2>
    <p>
        <ul>
          <li><a href="Products/index.html">产品中心</a></li>
          <li>·<a href="Products/list-1">产品分类1</a></li>
          <li>·<a href="Products/show-1">产品1</a></li>
        </ul>
    </p>
    <p>
        <ul>
          <li><a href="News/index.html">新闻中心</a></li>
          <li>·<a href="News/1.html">新闻分类1</a></li>
          <li>·<a href="News/2.html">新闻分类2</a></li>
        </ul>
    </p>
    <p>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </p>
</asp:Content>
