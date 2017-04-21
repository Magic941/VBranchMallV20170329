<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.CMS.VideoAlbum.Show" Title="<%$ Resources:CMSVideo, ptVideoAlbumShow %>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       <asp:Literal ID="ltlShow" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumShow %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumShowTip %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow" valign="top">
                                <asp:Literal ID="ltlCoverVideo" runat="server" Text="<%$ Resources:CMSVideo, ltlCoverVideo %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Image ID="imgCoverVideo" runat="server" Width="120" Height="120" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlName" runat="server" Text="<%$ Resources:CMSVideo, Name %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblAlbumName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow" valign="top">
                                         <asp:Literal ID="ltlDescription" runat="server" Text="<%$ Resources:CMSVideo, Description %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlSequence" runat="server" Text="<%$ Resources:CMSVideo, ltlSequence %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblSequence" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlPrivacy" runat="server" Text="<%$ Resources:CMSVideo, ltlPrivacy %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPrivacy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlPvCount" runat="server" Text="<%$ Resources:CMSVideo, ltlPvCount %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPvCount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlCreatedUser" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedUser %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblCreatedUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="ltlCreatedDate" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedDate %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlLastUpdateUser" runat="server" Text="<%$ Resources:CMSVideo, ltlLastUpdateUser %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblLastUpdateUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                <asp:Literal ID="ltlLastUpdateDate" runat="server" Text="<%$ Resources:CMSVideo, ltlLastUpdateDate %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblLastUpdatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <br />
                                <asp:Button ID="btnEdit" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnEditText %>" class="adminsubmit_short"
                                    OnClick="btnEdit_Click" />
                                <asp:Button ID="btnClose" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCloseText %>" class="adminsubmit_short"
                                    OnClientClick="javascript:parent.$.colorbox.close();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="10" width="*" align="left">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
