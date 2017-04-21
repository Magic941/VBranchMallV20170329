<%@ Page Title="<%$ Resources:Site, ptRoleAssignment%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="roleassignment.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.roleassignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, ptRoleAssignment%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Sysmanage, lblRoleAssignmentOperate%>" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="5" width="100%" border="0" class="tdbg">
                        <tr>
                            <td align="left" height="25">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, TooltipRoleAssignForuser%>" />
                                <asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="22">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="3" Width="100%">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClientClick="javascript:history.go(-1);return false;" class="adminsubmit_short">
                                </asp:Button>
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
