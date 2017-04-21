<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsBatchUploadDropList.ascx.cs"
    Inherits="Maticsoft.Web.Controls.ProductsBatchUploadDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.selectProductnode.js" handle="/NodeProdCategory.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>
