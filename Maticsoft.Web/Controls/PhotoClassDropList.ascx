<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoClassDropList.ascx.cs"
    Inherits="Maticsoft.Web.Controls.PhotoClassDropList" %>
<script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
<div><asp:HiddenField ID="hfSelectedNode" runat="server" /></div>
<script src="/Scripts/jquery/maticsoft.selectPhotoClass.js" handle="/NodeHandle.aspx" isnull="<%= this.IsNull.ToString().ToLower() %>" type="text/javascript"></script>
