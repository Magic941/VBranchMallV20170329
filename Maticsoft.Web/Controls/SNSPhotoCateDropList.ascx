<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SNSPhotoCateDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.SNSPhotoCateDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.SelectSNSPhotoNode.js" handle="/SNSCategories.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>
