<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.PhotoAlbum.Add"
    Title="<%$Resources:CMSPhoto,ptAddAlbum %>" %>

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
</head>
<body>
    <form id="Form1" runat="server">
    <div class="newslistabout" style="width: 700px;height:400px">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAddAlbum %>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblAddAlbum %>"/>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border" align="left">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblAlbumName%>"/>：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAlbumName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align:top;">
                               <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblIntro %>"/>：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="400px"
                                    Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblApprovalStatus %>"/>：
                            </td>
                            <td height="25" width="*">
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left"
                                    Width="340px">
                                    <asp:ListItem Selected="True" Value="2" Text="<%$Resources:Site,Normal %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,PendingReview %>"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%$Resources:Site,Unaudited %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMSPhoto,lblPrivacy %>"/>：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="radlPrivacy" runat="server" RepeatDirection="Horizontal"
                                    align="left" Width="340px">
                                    <asp:ListItem Selected="True" Value="0" Text="<%$Resources:Site,drpOpenToAll%>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,drpHimselfVisible %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$Resources:Site,drpFriendsWatch %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblOrder %>"/>：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSequence" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClientClick="JavaScript:parent.$.colorbox.close();" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <script src="/Scripts/calendar1.js" type="text/javascript"></script>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
