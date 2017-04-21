<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true" CodeBehind="AddUserType.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.AddUserType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, ptAddUserType%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, lblAddUserTypeOperate%>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, fieldUserType%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUserType" TabIndex="1" runat="server" Width="249px" MaxLength="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtUserType"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, fieldUserDescription%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" TabIndex="1" runat="server" Width="249px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" OnClientClick="javascript:parent.$.colorbox.close();" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
