<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="TestCallApi.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.TestCallApi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="我的收入带分面" />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="我的收入" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="我的粉丝带分面" />
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click1" Text="我的粉丝" />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="自动注册" />
        <br />
    <asp:Label ID="pagelb" runat="server" Text="页数"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="pageSize" runat="server" Text="显示行数20"></asp:Label>
    <br />
    <asp:Label ID="sumPage" runat="server" Text="总页数"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
