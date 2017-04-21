<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiftCategoryDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.GiftCategoryDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.selectgiftnode.js" handle="/Shopmanage.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>