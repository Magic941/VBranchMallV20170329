<%@ Page Title="<%$ Resources:Site, ptRoleAssignment%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="RoleAssignment.aspx.cs" Inherits="Maticsoft.Web.Enterprise.RoleAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="admintitle">
            <div class="sj" style="margin-right: 20px;">
                <img src="/images/icon6.gif" width="21" height="28" /></div>
            <strong id="TitleText"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Site, ptRoleAssignment%>" /></strong>
        </div>
    </div>
<div class="newslistabout">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="5" align="left"  border="0" class="tdbg">
                    <tr>
                        <td align="left" height="25" colspan="2">
                             <h4><asp:Label ID="lblName" runat="server"></asp:Label></h4>
                             <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, TooltipRoleAssignForuser%>" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="ltbUser" Width="250px" Height="300px" runat="server"
                             SelectionMode="Single" AutoPostBack="True" OnSelectedIndexChanged="ltbUser_OnSelectedIndexChanged" ></asp:ListBox>
                        </td>
                        <td style="vertical-align:top" >
                            <asp:CheckBoxList ID="cblRole" runat="server" RepeatColumns="4">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-left: 190px">
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                class="adminsubmit" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
