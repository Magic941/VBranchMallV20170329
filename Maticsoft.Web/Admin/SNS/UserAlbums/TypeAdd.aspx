<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="TypeAdd.aspx.cs" Inherits="Maticsoft.Web.SNS.AlbumType.Add" Title="增加专辑类型" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="增加专辑类型" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以增加专辑类型" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                专辑类型名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTypeName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                菜单显示的顺序 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtMenuSequence" runat="server" Text="1" Width="100px" MaxLength="6"
                                    onkeyup="value=value.replace(/[^\d]/g,'') " onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                专辑的数量 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAlbumsCount" runat="server" Text="1" Width="100px" MaxLength="6"
                                    onkeyup="value=value.replace(/[^\d]/g,'') " onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">可用</asp:ListItem>
                                    <asp:ListItem Value="0">不可用</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkIsMenu"  runat="server" Checked="False" Text="是否是菜单" />
                                <asp:CheckBox ID="chkMenuIsShow" runat="server" Checked="False" Text="是否显示" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                备注 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
