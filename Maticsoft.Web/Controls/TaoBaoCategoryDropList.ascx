<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaoBaoCategoryDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.TaoBaoCategoryDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.SelectTaoBaoNode.js" handle="/SNSCategories.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>