<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Photo.Show" Title="<%$Resources:CMSPhoto,ptPrictureShow %>" %>

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
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptPrictureShow %>" /></span>
                        </td>
                        <td align="middle">
                            <table align="left" id="table3">
                                <tr valign="top" align="middle">
                                    <td width="100">
                                        <a href="modify.aspx?id=<%=strid%>">
                                            <img title="编辑" src="/admin/images/edit.gif" border="0" alt="" /></a>
                                    </td>
                                    <td width="100">
                                        <a href="delete.aspx?id=<%=strid%>">
                                            <img title="删除" src="/admin/images/delete.gif" border="0" alt="" /></a>
                                    </td>
                                    <td width="100">
                                        <a href="list.aspx">
                                            <img title="显示" src="/admin/images/view.gif" border="0" alt="" /></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            PhotoID ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPhotoID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,PrictureName %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPhotoName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblArtworkName %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblImageUrl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblIntro %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMSPhoto,lblAlbumID %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAlbumID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,State %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblState" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:CMSPhoto,lblCreaterID %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:CMSPhoto,lblCreateData %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:CMSPhoto,lblPageViews %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPVCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMSPhoto,lblPictureClass %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblClassID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMSPhoto,lblThumbnails %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblThumbImageUrl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:CMSPhoto,lblNormal %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblNormalImageUrl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Site,lblOrder %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSequence" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:CMSPhoto,lblIsRecommend %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblIsRecomend" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:CMSPhoto,lblComments %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCommentCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                           <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:CMSPhoto,lblabel %>" />：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblTags" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
