<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SelectFloorSort.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SelectFloorSort" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="sd" HeaderText="商品名称" />
            <asp:BoundField HeaderText="所在分类" />
            <asp:TemplateField HeaderText="显示楼层和排序"></asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
