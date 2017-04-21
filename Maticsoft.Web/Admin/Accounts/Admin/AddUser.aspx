<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, lblAddUser%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, lblAddNewUserOperate%>" />
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
                            <td height="25">
                                <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Width="200px" MaxLength="20"
                                    ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, fieldPassword%>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" Width="200px" MaxLength="20"
                                     TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, fieldConfirmationPassword%>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPassword1" TabIndex="3" runat="server" Width="200px" MaxLength="20"
                                     TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage='<%$ Resources:Site, ErrorPasswprd%>'
                                    Display="Dynamic" ControlToValidate="txtPassword1" ControlToCompare="txtPassword"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, fieldTrueName%>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTrueName" TabIndex="4" runat="server" Width="200px" MaxLength="20"
                                    ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtTrueName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--<tr>
												<td class="td_class">
												
												<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, fieldSex%>" />:
												</td>
												<td height="25"><asp:radiobutton id="RadioButton1" runat="server" GroupName="optSex" Checked="True" Text="<%$ Resources:Site, SexMale%>">
												</asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="RadioButton2" runat="server" GroupName="optSex" Text="<%$ Resources:Site, SexWoman%>"></asp:radiobutton>
												</td>
											</tr>--%>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Site, fieldTelphone%>" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Site, fieldEmail%>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Site, fieldUserType%>" />：
                            </td>
                            <td height="25">                                
                                <asp:RadioButtonList ID="radbtnlistUserType" runat="server" RepeatColumns="5">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <%--<tr>
												<td class="td_class">Style:
												</td>
												<td style="HEIGHT: 5px" height="5"><asp:dropdownlist id="dropStyle" runat="server" Width="200px">
														<asp:ListItem Value="1">DefaultBlue</asp:ListItem>
														<asp:ListItem Value="2">Olive</asp:ListItem>
														<asp:ListItem Value="3">Red</asp:ListItem>
														<asp:ListItem Value="4">Green</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>--%>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
