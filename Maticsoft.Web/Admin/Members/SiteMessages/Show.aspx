<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.SiteMessages.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看系统消息" />
                    </td>
                </tr>
            <%--    <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SiteSetting,lblFriendlyLinkShow %>" />
                    </td>
                </tr>--%>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                    <tr>
                                        <td height="25" width="30%" align="right">
                                            ID ：
                                        </td>
                                        <td height="25" width="*" align="left">
                                            <asp:Label ID="lblID" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" width="30%" align="right">
                                            发送者 ：
                                        </td>
                                        <td height="25" width="*" align="left">
                                            <asp:Label ID="lblSenderID" Text="Admin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" width="30%" align="right">
                                            接受者 ：
                                        </td>
                                        <td height="25" width="*" align="left">
                                            <asp:Label ID="lblReceiverID" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" width="30%" align="right">
                                            内容 ：
                                        </td>
                                        <td height="25" width="*" align="left">
                                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                          <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnBackText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
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
