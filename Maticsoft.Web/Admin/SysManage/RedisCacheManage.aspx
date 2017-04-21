<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="RedisCacheManage.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.RedisCacheManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table style="width: 100%;">
        <tr>
            <td>
       <asp:Button ID="DeleteAllCache" runat="server" Text="删除所有缓存" OnClick="DeleteAllCache_Click" />
            </td>
            <td>    
    <asp:Button ID="LoadAllCache" runat="server" Text="加载所有缓存" OnClick="LoadAllCache_Click" /> 
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><asp:Button ID="DeleteAllProduct" runat="server" Text="删除所有商品缓存" OnClick="DeleteAllProduct_Click" /></td>
            <td>
    <asp:Button ID="LoadAllProduct" runat="server" Text="加载所有商品缓存" OnClick="LoadAllProduct_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:Button ID="DeleteAllGroupBuy" runat="server" Text="删除所有团购缓存" OnClick="DeleteAllGroupBuy_Click" />
            </td>
            <td>
    <asp:Button ID="LoadAllGroupBuy" runat="server" Text="加载所有团购缓存" OnClick="LoadAllGroupBuy_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:Button ID="DeleteAllCategory" runat="server" Text="删除所有分类缓存" OnClick="DeleteAllCategory_Click" />
            </td>
            <td>
    <asp:Button ID="LoadAllCategory" runat="server" Text="加载所有分类缓存" OnClick="LoadAllCategory_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:Button ID="DeleteAllProductImage" runat="server" Text="删除所有商品图片信息缓存" OnClick="DeleteAllProductImage_Click" />
            </td>
            <td>
    <asp:Button ID="LoadAllProductImage" runat="server" Text="加载所有商品图片信息缓存" OnClick="LoadAllProductImage_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:Button ID="DeleteAllProductCategories" runat="server" Text="删除所有分类关联缓存" OnClick="DeleteAllProductCategories_Click" />
            </td>
            <td>
    <asp:Button ID="LoadAllProductCategories" runat="server" Text="加载所有分类关联缓存" OnClick="LoadAllProductCategories_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:Label ID="ShowText" runat="server" Text="Label"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
