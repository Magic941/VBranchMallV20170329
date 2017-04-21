<%@ Page Title="<%$ Resources:Site, ptRoleAssignment%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserRoleAssignment.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.UserRoleAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site, ptRoleAssignmentUserSet%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site, ptRoleAssignmentUserSet%>" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="5" align="left" border="0" class="tdbg">
                        <tr>
                            <td align="left" height="25" colspan="2">
                                <h4>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage,lblEnterpriseName %>" />
                                    <asp:Label ID="lblName" runat="server"></asp:Label></h4>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, TooltipUserRoleAssignForuser%>" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ltbUser" Width="250px" Height="300px" runat="server" SelectionMode="Single"
                                    AutoPostBack="True" OnSelectedIndexChanged="ltbUser_OnSelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td style="vertical-align: top">
                                <asp:CheckBoxList ID="cblRole" runat="server" RepeatColumns="4">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 190px">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnBackText %>"
                                    OnClick="btnBack_Click" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
