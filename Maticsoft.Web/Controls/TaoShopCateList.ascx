<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaoShopCateList.ascx.cs" Inherits="Maticsoft.Web.Controls.TaoShopCateList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.SelectTaoShopCate.js" handle="/TaoCategory.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>

