<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddPhotoInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Photo.AddPhotoInfo"
    Title="<%$Resources:CMSPhoto,ptAddPhotoInfo %>" %>

<%@ Register TagPrefix="uc1" TagName="PhotoClassDropList" Src="~/Controls/PhotoClassDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 15%;
        }
    </style>
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            resizeImg('.border', 180, 200);
        });
        function EditCover(controls, id) {
            $.ajax({
                url: "/EditPhotoHandle.aspx",
                type: 'post', dataType: 'text', timeout: 5000, async: false,
                data: { Action: "EditCover", PhotoId: id },
                success: function (resultData) {
                    if (resultData == "Success") {
                        $("[title='<%= Resources.CMSPhoto.lblFrontCover %>']").attr("title", "<%= Resources.CMSPhoto.lblSetToCover %>").text("<%= Resources.CMSPhoto.lblSetToCover %>");
                        $(controls).attr("title", "<%= Resources.CMSPhoto.lblFrontCover %>").text("<%= Resources.CMSPhoto.lblFrontCover %>");
                        clickautohide(4, "<%= Resources.CMSPhoto.TooltipSetToCoverSuccess %>", 2000);
                    }
                    else {
                        clickautohide(5, "<%= Resources.CMSPhoto.TooltipSetToCoverFail %>", 2000);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslist_title">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAddPhotoInfo %>" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblAddPhotoInfo%>" />
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <asp:Repeater ID="RepeaterPhoto" runat="server" OnItemDataBound="RepeaterPhoto_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td class="tdbg" style="text-align: center" colspan="2">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td width="15%" align="center" rowspan="5">
                                    <img ref='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T235x1280_") %>' style="width: <%#Unit.Parse(strThumbImageWidth) %>;
                                        height: <%#Unit.Parse(strThumbImageHeight) %>" />
                                </td>
                                <td class="td_class">
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,Name%>" />：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtPhotoName" runat="server" Text='<%#Eval("PhotoName") %>' Width="300px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSphoto,lblClassification%>" />：
                                </td>
                                <td height="25" width="*" align="left">
                                    <uc1:PhotoClassDropList ID="ddlPhotoClass" runat="server" IsNull="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblRecommend%>" />：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:CheckBox ID="chkIsRecomend" Text="<%$Resources:CMSPhoto,lblIsRecommend %>"
                                        runat="server" Checked="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMSPhoto,lblabel%>" />：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtTags" runat="server" Width="300px" MaxLength="250"></asp:TextBox>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMSPhoto,TooltipLabel%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblIntro%>" />：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="300px"
                                        Height="72px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a style="color: #0063dc; text-decoration: none;" phoneid='<%#Eval("PhotoID") %>'
                                        onclick='EditCover(this,$(this).attr("PhoneId"))' title='<%# GetPhotoCover(Eval("CoverPhoto"), Eval("PhotoID"))%>'>
                                        <%# GetPhotoCover(Eval("CoverPhoto"), Eval("PhotoID"))%></a>
                                </td>
                                <td class="td_class">
                                    &nbsp;
                                </td>
                                <td height="25" width="*" align="left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="tdbg" align="right" style="width: 15%" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
