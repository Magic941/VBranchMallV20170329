<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoClassDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.VideoClassDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.selectVideoClass.js" handle="/CMSContent.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>