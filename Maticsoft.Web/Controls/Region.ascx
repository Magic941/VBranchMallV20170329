<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Region.ascx.cs" Inherits="Maticsoft.Web.Controls.Region" %>
<asp:DropDownList ID="ddlProvince" runat="server" 
    onselectedindexchanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
<asp:DropDownList ID="ddlCity" runat="server" 
    onselectedindexchanged="ddlCity_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
<asp:DropDownList ID="ddlArea" runat="server" 
    onselectedindexchanged="ddlArea_SelectedIndexChanged" AutoPostBack="false">
</asp:DropDownList>
