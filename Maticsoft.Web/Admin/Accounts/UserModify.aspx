<%@ Page Title="<%$ Resources:Site, ptUserInfo%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="UserModify.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.UserModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
        <div class="newslist_title">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, ptUserInfo%>" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Sysmanage, lblEditUserinfo%>" />
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
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, fieldUserName%>" />:
                        </td>
                        <td height="25">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, fieldTrueName%>" />:
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtTrueName" TabIndex="4" runat="server" Width="249px" MaxLength="20"
                                ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Site,ErrorNotNull%>"
                                Display="Dynamic" ControlToValidate="txtTrueName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>                   
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Site, fieldEmail%>" />:
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>                   
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" ValidationGroup="B"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
