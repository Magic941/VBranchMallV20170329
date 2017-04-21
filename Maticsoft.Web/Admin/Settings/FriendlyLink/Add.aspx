<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.FriendlyLink.FLinks.Add" Title="<%$Resources:SiteSetting,ptFriendlyLink %>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SiteSetting,ptFriendlyLink %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SiteSetting,lblFriendlyLink %>" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:SiteSetting,lblLinkName %>" />£º
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Width="350px" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:SiteSetting,lblImgUrl %>" />£º
                            </td>
                            <td>
                                <asp:TextBox ID="txtImgUrl" runat="server" Width="350px" class="addinput"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:SiteSetting,lblLinkUrl%>" />£º
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtLinkUrl" runat="server" Width="350px" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtLinkUrl"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:SiteSetting,lblLinkDescribe%>" />£º
                            </td>
                            <td>
                                <asp:TextBox ID="txtLinkDesc" runat="server" TextMode="MultiLine" Width="350px" Height="50px"
                                    class="addtextarea"></asp:TextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:SiteSetting,lblLinkType%>" />£º
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:DropDownList ID="dropTypeID" runat="server">
                                    <asp:ListItem Value="0" Text="<%$Resources:SiteSetting,lblImgLink%>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:SiteSetting,lblTextLink%>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,State%>" />£º
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="dropState" runat="server">
                                    <asp:ListItem Value="0" Text="<%$Resources:Site,Unaudited%>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,btnApproveText%>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,lblOrder%>" />£º
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtOrderID" runat="server" Width="350px" Text="1" class="addinput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtOrderID" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOrderID"
                                    ErrorMessage="<%$Resources:SiteSetting,TooltipInputInt%>" ValidationExpression="^[+-]?\d+$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:SiteSetting,lblContacts%>" />£º
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContactPerson" runat="server" Width="350px" class="addinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,fieldEmail%>" />£º
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEmail" runat="server" Width="350px" class="addinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,fieldTelphone%>" />£º
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTelPhone" runat="server" Width="350px" class="addinput" onkeyup="value=value.replace(/[^\d]/g,'') "  onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"  ></asp:TextBox>
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
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
