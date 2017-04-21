<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Photo.Modify"
    Title="<%$Resources:CMSPhoto,ptAddPhotoInfo%>" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="uc1" TagName="PhotoClassDropList" Src="~/Controls/PhotoClassDropList.ascx" %>
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
            $("[id$='ThumbImage']").attr('ref', $("[id$='ThumbImage']").attr('src'));
            $("[id$='ThumbImage']").removeAttr('src');
            resizeImg('#tmp', 180, 200);
        });
    </script>
</head>
<body>
    <form runat="server">
    <div class="newslistabout" style="width: 700px; height: 400px">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:CMSPhoto,ptPrictureShow %>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="center" rowspan="12" id="tmp">
                                <asp:Image ID="ThumbImage" runat="server" ref="" />
                                <br />
                                <a id="ShowImage" target="_blank" runat="server">
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,lblViewOriginal %>" /></a>
                            </td>
                            <td colspan="2" align="center">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,PrictureName %>" />
                                ：
                            </td>
                            <td  width="*" align="left">
                                <asp:TextBox ID="txtPhotoName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMSPhoto,lblAlbum %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:DropDownList ID="ddlAlbum" runat="server" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblIntro %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,State %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                                    <asp:ListItem Text="<%$Resources:Site,Approved%>" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="<%$Resources:Site,Unaudited%>" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:CMSPhoto,lblPictureClass %>" />：
                            </td>
                            <td  width="*" align="left">
                                <uc1:PhotoClassDropList ID="ddlPhotoClass" runat="server" IsNull="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,lblOrder %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:TextBox ID="txtSequence" runat="server" Width="200px" onkeyup="value=value.replace(/[^\d]/g,'') "
                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMSPhoto,lblIsRecommend %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:CheckBox ID="chkIsRecomend" Text="<%$Resources:CMSPhoto,lblIsRecommend %>" runat="server"
                                    Checked="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:CMSPhoto,lblabel %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:TextBox ID="txtTags" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblCreater %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:Label ID="lblCreatedUserID" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblCreateDate %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:Label ID="lblCreatedDate" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMSPhoto,lblPageViews %>" />：
                            </td>
                            <td  width="*" align="left">
                                <asp:Label ID="lblPVCount" runat="server" Width="200px"></asp:Label>
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
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
