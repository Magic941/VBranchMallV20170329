<%@ Page Title="<%$ Resources:Sysmanage, ptConfigurationAdd %>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="MailConfigadd.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.MailConfigadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="user_border" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0" id="table1">
        <tr>
            <td valign="top">
                <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                    <tr>
                        <td align="left">
                            <span style="font-size: 12pt; font-weight: bold; color: #3666AA">
                                <img src="/admin/images/icon.gif" align="absmiddle" style="border-width: 0px;" />
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Sysmanage, ptConfigurationAdd %>" /></span>
                        </td>
                        <td align="middle">
                            <table align="left" id="table3">
                                <tr valign="top" align="left">
                                    <td width="80">
                                        <a href="MailConfiglist.aspx">
                                            <img title="" src="/admin/images/view.gif" border="0" alt="" /></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            <div class="newslistabout" >
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="3" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Sysmanage, lblSMTPMail%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtMailaddress" runat="server" Width="400"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMailaddress" runat="server" ErrorMessage="*" ControlToValidate="txtMailaddress"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Sysmanage, lblSMTPUsername%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtUsername" runat="server" Width="400"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="*" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                             <asp:Literal ID="Literal4" runat="server" Text="<%$  Resources:Sysmanage, lblSMTPPassword%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtPassword" runat="server" Width="400" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPServer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="3" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$  Resources:Sysmanage, lblSMTPServer%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtSMTPServer" runat="server" Width="400"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSMTPServer" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPServer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$  Resources:Sysmanage, lblSMTPServerPort%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtSMTPPort" runat="server" Width="400" Text="25"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSMTPPort" runat="server" ErrorMessage="*" ControlToValidate="txtSMTPPort"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$  Resources:Sysmanage, lblSSl%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:CheckBox ID="chkSMTPSSL" runat="server" Text="<%$  Resources:Sysmanage, TooltipSMTPSSL%>" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="3" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$  Resources:Sysmanage, lblPOPServer%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtPOPServer" runat="server" Width="400"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$  Resources:Sysmanage, lblPOPServerPort%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:TextBox ID="txtPOPPort" runat="server" Width="400" Text="110"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right">
                            <asp:Literal ID="Literal10" runat="server" Text="<%$  Resources:Sysmanage, lblSSl%>"/>:
                        </td>
                        <td height="25" align="left">
                            <asp:CheckBox ID="chkPOPSSL" runat="server" Text="<%$  Resources:Sysmanage, TooltipPOPSSL%>" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center">
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                    OnClick="btnSave_Click" class="adminsubmit" ></asp:Button>
                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                    OnClientClick="javascript:history.go(-1);return false;" class="adminsubmit"  ValidationGroup="B"></asp:Button>
            </td>
        </tr>
    </table>
    <br /></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
