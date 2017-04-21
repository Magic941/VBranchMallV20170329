<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.CategoriesDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.selectnode.js" handle="/Shopmanage.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>