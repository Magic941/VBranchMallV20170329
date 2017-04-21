<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.CMS.Video.Show" Title="<%$ Resources:CMSVideo, ptVideoShow %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Tools/flowplayer/flowplayer-3.2.11/flowplayer-3.2.10.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlShow" runat="server" Text="<%$ Resources:CMSVideo, ptVideoShow %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoShowTip %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlTitle" runat="server" Text="<%$ Resources:CMSVideo, ltlTitle %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trLocalVideo" runat="server" visible="false">
                            <td class="td_classshow" valign="top">
                                <asp:Literal ID="ltlLocalVideo" runat="server" Text="<%$ Resources:CMSVideo, LocalVideo %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <a href="/UploadFolder/<%=localVideoUrl %>" style="display: block; width: 580px;
                                    height: 380px" id="player"></a>
                                <script type="text/javascript">
                                    flowplayer("player", "/Tools/flowplayer/flowplayer-3.2.11/flowplayer-3.2.11.swf");
                                </script>
                            </td>
                        </tr>
                        <tr id="trOnlineVideo" runat="server" visible="false">
                            <td class="td_classshow" valign="top">
                                 <asp:Literal ID="ltlOnlineVideo" runat="server" Text="<%$ Resources:CMSVideo, OnlineVideo %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                                    width="430" height="340">
                                    <param name="wmode" value="opaque" />
                                    <param name="movie" value="<%=onlineVideoUrl %>" />
                                    <param name="quality" value="high" />
                                    <embed src="<%=onlineVideoUrl %>" allowfullscreen="true" quality="high" width="430"
                                        height="340" align="middle" wmode="transparent" allowscriptaccess="always" type="application/x-shockwave-flash">
                                            </embed>
                                </object>
                                <div class="clear">
                                </div>
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
                                <asp:Literal ID="ltlAlbum" runat="server" Text="<%$ Resources:CMSVideo, ltlAlbum %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblAlbumID" runat="server"></asp:Label>
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
                                <asp:Literal ID="ltlCategory" runat="server" Text="<%$ Resources:CMSVideo, ltlCategory %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblVideoClassID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlIsRecommend" runat="server" Text="<%$ Resources:CMSVideo, IsRecommend %>"></asp:Literal>
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblIsRecomend" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow" valign="top">
                                 <asp:Literal ID="ltlImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlImageUrl %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Image ID="imgImageUrl" runat="server" Width="45" Height="45" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow" valign="top">
                                 <asp:Literal ID="ltlThumbImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlThumbImageUrl %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Image ID="imgThumbImageUrl" runat="server" Width="45" Height="45" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow" valign="top">
                                <asp:Literal ID="ltlNormalImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlNormalImageUrl %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Image ID="imgNormalImageUrl" runat="server" Width="45" Height="45" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="ltlDuration" runat="server" Text="<%$ Resources:CMSVideo, ltlDuration %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTotalTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlTotalComment" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalComment %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTotalComment" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlTotalFav" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalFav %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTotalFav" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlTotalUp" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalUp %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTotalUp" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlReference" runat="server" Text="<%$ Resources:CMSVideo, ltlReference %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblReference" runat="server"></asp:Label>
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
                                 <asp:Literal ID="ltlTags" runat="server" Text="<%$ Resources:CMSVideo, ltlTags %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTags" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlSite" runat="server" Text="<%$ Resources:CMSVideo, ltlSite %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblVideoUrl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlType" runat="server" Text="<%$ Resources:CMSVideo, ltlType %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblUrlType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trVideoFormat" runat="server" visible="false">
                            <td class="td_classshow">
                                <asp:Literal ID="ltlFormat" runat="server" Text="<%$ Resources:CMSVideo, ltlFormat %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblVideoFormat" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trDomain" runat="server" visible="false">
                            <td class="td_classshow">
                                <asp:Literal ID="ltlDomain" runat="server" Text="<%$ Resources:CMSVideo, ltlDomain %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblDomain" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlGrade" runat="server" Text="<%$ Resources:CMSVideo, ltlGrade %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblGrade" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlAttachment" runat="server" Text="<%$ Resources:CMSVideo, ltlAttachment %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:HyperLink ID="lnkAttachment" runat="server" Target="_blank">【<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CMSVideo, ltlDownload %>"></asp:Literal>】</asp:HyperLink>
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
                                <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlRemark" runat="server" Text="<%$ Resources:CMSVideo, ltlRemark %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                 <asp:Literal ID="ltlCreatedUser" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedUser %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
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
                                <asp:Label ID="lblLastUpdateUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                <asp:Literal ID="ltlLastUpdateDate" runat="server" Text="<%$ Resources:CMSVideo, ltlLastUpdateDate %>"></asp:Literal> ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblLastUpdateDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <br />
                                <asp:Button ID="btnEdit" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnEditText %>" class="adminsubmit_short"
                                    OnClick="btnEdit_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnBackText %>" class="adminsubmit_short"
                                    OnClick="btnCancle_Click"></asp:Button>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
