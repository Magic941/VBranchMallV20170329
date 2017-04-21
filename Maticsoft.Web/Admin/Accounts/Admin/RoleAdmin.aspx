<%@ Page Title="<%$ Resources:Site, ptRoleAdmin%>" Language="C#" MasterPageFile="~/Admin/BasicAddSearch.Master"
    AutoEventWireup="true" CodeBehind="RoleAdmin.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.RoleAdmin"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("[id$='divAdd']").length <= 0) {
                $("#HFISSHOW").parent().parent().parent().parent().hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Site, ptRoleAdmin%>" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder_TitleButton"
    runat="server">
    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage, lblRoleManageOperate%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_ADD" runat="server">
<input  type="hidden" id="HFISSHOW"/>
    <div class="newslistabout" id="divAdd" runat="server" >
        <b>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblAddRole%>" />：</b>
        <asp:TextBox ID="txtRoleName" runat="server" class="inputtext" meta:resourcekey="txtRoleNameResource1"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
            class="adminsubmit_short" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
        <asp:Label ID="lblToolTip" runat="server" meta:resourcekey="lblToolTipResource1"></asp:Label></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Search" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
        <tr>
            <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
               
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <asp:DataList ID="RoleList" runat="server" RepeatColumns="3" CellPadding="4" Width="98%"
                    ItemStyle-HorizontalAlign="Left" meta:resourcekey="RoleListResource1">
                    <ItemTemplate>
                        [<a href='EditRoleC.aspx?RoleID=<%# DataBinder.Eval(Container.DataItem, "RoleID") %>'><%# DataBinder.Eval(Container.DataItem, "Description") %></a>]<br />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:DataList></center>
            </td>
        </tr>
    </table>
    <center>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_Gridview" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
