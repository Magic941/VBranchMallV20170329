<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SNSCategoryDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.SNSCategoryDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.SelectSNSnode.js" handle="/SNSCategories.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>