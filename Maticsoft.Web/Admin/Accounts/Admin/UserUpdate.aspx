<%@ Page Title="<%$ Resources:Site, ptUserUpdate%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserUpdate.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.UserUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblModifUser%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage,lblModifUserOperate%>"/>
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
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, fieldUserName%>" />：
                        </td>
                        <td >
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, fieldPassword%>" />：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" Width="249px" MaxLength="20"
                                TextMode="Password" class="addinput"></asp:TextBox><asp:Literal ID="Literal11" runat="server"
                                    Text="<%$ Resources:Sysmanage, lblNoChangePasswpord%>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, fieldConfirmationPassword%>" />：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtPassword1" TabIndex="3" runat="server" Width="249px" MaxLength="20"
                                 TextMode="Password"  class="addinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, fieldTrueName%>" />：
                        </td>
                        <td>
                            <asp:TextBox ID="txtTrueName" TabIndex="4" runat="server" Width="249px" MaxLength="20"
                                  class="addinput"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                Display="Dynamic" ControlToValidate="txtTrueName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Site, fieldTelphone%>" />：
                        </td>
                        <td style="height: 3px" height="3">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200px"     class="addinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Site, fieldEmail%>" />：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtEmail" runat="server" Width="200px"     class="addinput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:Sysmanage,fieldEmployeeID %>" />：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtEmployeeID" runat="server" Width="200px"      class="addinput"></asp:TextBox>
                            <asp:RangeValidator ControlToValidate="txtEmployeeID" ID="RangeValidator2" runat="server"  Display="Dynamic" 
 Type="Integer" MinimumValue="1" MaximumValue="999999999" ErrorMessage="<%$ Resources:Site, TooltipFormatErr %>"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Site, fieldUserType%>" />：
                        </td>
                        <td height="25">
                            <asp:DropDownList ID="dropUserType" runat="server" Width="200px" class="dropSelect">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:Sysmanage,lblState%>"/>
                        </td>
                        <td height="25">
                            <asp:CheckBox ID="chkActive" runat="server" Text="<%$ Resources:Sysmanage,lblFrozenAccount %>" Checked="false" />
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
                <asp:Button ID="btnCancle" runat="server" CausesValidation="false"  Text="<%$ Resources:Site, btnCancleText %>"
                    OnClientClick="javascript:history.go(-1);return false;" class="adminsubmit_short"></asp:Button>
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
