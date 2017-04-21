<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.PhotoAlbum.Modify"
    Title="<%$Resources:CMSPhoto,ptAlbumModify %>" %>

<html>
<head>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/MasterPage<%=Session["Style"]%>.css" type="text/css"
        rel="stylesheet" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" charset="utf-8">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/admin/js/msgbox/css/msgbox.css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript" src="/admin/js/msgbox/script/msgbox.js"></script>
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='imgCoverPhoto']").attr('ref', $("[id$='imgCoverPhoto']").attr('src'));
            resizeImg('.border', 150, 180);
            $("[id$='imgCoverPhoto']").removeAttr('src');
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="newslistabout" style="width: 700px; height: 400px">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAlbumModify %>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class" rowspan="9">
                                <div style="background-image: url(/Admin/Images/albumbg.gif); width:167px; height: 197px; margin-left: 5px; padding: 2px">
                                    <asp:Image ID="imgCoverPhoto" runat="server" width="150" height="180" style="margin-top: 5px;margin-right: 7px" />
                                </div>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:CMSPhoto,lblAlbumName %>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlbumName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblIntro %>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,lblApprovalStatus %>" />：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="2" Text="<%$Resources:Site,Normal %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,PendingReview %>"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%$Resources:Site,Unaudited %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Site,lblOrder %>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtSequence" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMSPhoto,lblPrivacy %>" />：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radlPrivacy" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="0" Text="<%$Resources:Site,drpOpenToAll %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,drpHimselfVisible %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$Resources:Site,drpFriendsWatch %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblCreater %>" />
                                ：
                            </td>
                            <td>
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblCreateData %>" />
                                ：
                            </td>
                            <td>
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMSPhoto,lblLastDate %>" />：
                            </td>
                            <td>
                                <asp:Label ID="lblLastUpdatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMSPhoto,lblPageViews %>" />：
                            </td>
                            <td>
                                <asp:Label ID="lblPVCount" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdbg" align="center" valign="bottom">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                        OnClientClick="JavaScript:parent.$.colorbox.close();" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
