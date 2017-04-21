<%@ Page Title="<%$ Resources:Sysmanage,ptMailSendSettings%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="MailConfigInfoOld.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.MailConfigInfoOld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Sysmanage,ptMailSendSettings%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Sysmanage,lblConfiguringMailbox%>" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HiddenField_ID" runat="server" />

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Sysmanage,lblSMTPServer%>" />:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSMTPServer" runat="server" Width="400" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSMTPServer" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPServer"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Sysmanage,lblSMTPServerPort%>" />:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSMTPPort" runat="server" Width="400" Text="25" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSMTPPort" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPPort"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Sysmanage,lblSSl%>" />:
                            </td>
                            <td height="25">
                               <asp:CheckBox ID="chkSMTPSSL" runat="server" Text="<%$ Resources:SysManage,TooltipSMTPSSL %>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Sysmanage,lblSMTPMail%>" />:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMailaddress" runat="server" Width="400" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMailaddress" runat="server" ErrorMessage="*" ControlToValidate="txtMailaddress"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Sysmanage,lblSMTPUsername%>" />:
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtUsername" runat="server" Width="400" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="*" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Sysmanage,lblSMTPPassword%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPassword" runat="server" Width="400" TextMode="Password" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPServer"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
