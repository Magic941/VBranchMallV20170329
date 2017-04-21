<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.ShopCategories.Modify"
     %>

<%@ Register TagPrefix="uc1" TagName="GoodTypeDropList" Src="/Controls/GoodTypeDropList.ascx" %>
<html>
<head>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/MasterPage<%=Session["Style"]%>.css" type="text/css"
        rel="stylesheet" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" charset="utf-8">
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.css" rel="stylesheet"  type="text/css" />
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/admin/js/msgbox/css/msgbox.css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript" src="/admin/js/msgbox/script/msgbox.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.colorpicker.js"></script>
    <script type="text/javascript">
        function uploadPic(sourceId, fileQueueId, hfPathId, queueSizeLimit, multiSelected, accumulate, hideUploadControl, showTargetId) {
            $("#" + sourceId).uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',
                'script': '/WebLogo.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'queueID': fileQueueId,
                'width': 76,
                'height': 25,
                'auto': true,
                'multi': multiSelected,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': queueSizeLimit,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },
                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        //$("#imgUrl").attr("src", response.split('|')[1].format(''));
                        var resPicPath = response.split('|')[1];
                        if (hideUploadControl == true) $("#" + sourceId).fadeOut();
                        var tempPicId = queueId;
                        var tempPicPath = resPicPath.replace("{0}", "");
                        appendPic(hfPathId, showTargetId, tempPicId, tempPicPath, accumulate);
                    } else {
                        ShowFailTip("图片上传失败！", 2000);
                    }
                    return false;
                },
                'onCancel': function (event, ID, fileObj, data) {
                    removePic(hfPathId, showTargetId, ID);
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    ShowFailTip('上传文件发生错误, 状态码: [' + errorObj.info + ']', 2000);
                }
            });
        }

        function appendPic(valueFieldId, targetId, imgId, imgSrc, accumulate) {
            addValue(valueFieldId, imgSrc, accumulate);
            var htmlPicList = "";
            htmlPicList += "<div id='div-" + imgId + "' style='float:left; margin:2px 2px;'>";
            htmlPicList += "<img id='" + imgId + "' src='" + imgSrc + "' style='width:100px;' />";
            htmlPicList += "</div>";
            $("#" + targetId).append(htmlPicList);
        }

        function removePic(valueFieldId, targetId, imgId) {
            removeValue(valueFieldId, $("#" + targetId).find("#" + imgId).attr("src"));
            $("#" + targetId).find("#div-" + imgId).remove();
        }

        // 向隐藏域中添加值
        function addValue(valueFieldId, value, accumulate) {
            var el = $("#" + valueFieldId);
            if (accumulate == true) {
                if ($.trim(el.val()).length > 0) {
                    el.val(el.val() + "|" + value);
                }
                else {
                    el.val(value);
                }
            }
            else {
                el.val(value);
            }
        }
        // 从隐藏域中移除值
        function removeValue(valueFieldId, value) {
            var el = $("#" + valueFieldId);
            if (el.val().indexOf(value) >= 0) el.val(el.val().replace(value, "").replace("||", "|"));
            if (el.val().indexOf("|") == 0) el.val(el.val().substr(1));
            if (el.val().lastIndexOf("|") == el.val().length - 1) el.val(el.val().substr(0, el.val().length - 1));
        }
        // 从隐藏域中解析图片地址,将图片显示出来
        function resolvePics(valueFieldId, showTargetId) {
            var picPaths = $.trim($("#" + valueFieldId).val());
            if (picPaths.length > 0)
            {
                $.each(picPaths.split("|"), function (i, n) {
                    var tempPicName = n.substr(n.lastIndexOf("/") + 1, n.lastIndexOf(".") - n.lastIndexOf("/") - 1);
                    var htmlPicList = "";
                    htmlPicList += "<div id='div-" + tempPicName + "' style='float:left; margin:2px 2px;'><ul>";
                    htmlPicList += "<li><img id='" + tempPicName + "' src='" + n + "' style='width:100px;' /></li>";
                    htmlPicList += "<li style='text-align:center;'><a href=\"javascript:removePic('" + valueFieldId + "','" + showTargetId + "','" + tempPicName + "');\">删除</a></li>";
                    htmlPicList += "</ul></div>";
                    $("#" + showTargetId).append(htmlPicList);
                });
            }
        }

        $(document).ready(function () {
            uploadPic('entryPicUploadify', 'entryPicFileQueue', 'hfEntryPicPath', 1, false, false, true, 'entryPicList');
            uploadPic('bannerPicUploadify', 'bannerPicFileQueue', 'hfBannerPicPath', 999, true, true, false, 'bannerPicList');

            resolvePics('hfEntryPicPathOriginal', 'entryPicListOriginal');
            resolvePics('hfBannerPicPathOriginal', 'bannerPicListOriginal');

            $("#txtbgcolor").colorpicker({
                fillcolor: true,
                success: function (o, color) {
                    //  $(o).css("color", color);
                }
            });
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="newslistabout" style="width: 500px">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptClassModify %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblClassModify %>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,fieldUserDescription %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtGoodtypeName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblParentsClass %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <uc1:GoodTypeDropList ID="ddlPhotoClass" runat="server" IsNull="True" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblOrder %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSequence" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="分类图片" />：
                            </td>
                             <td height="25">
                                <asp:HiddenField ID="hfEntryPicPath" runat="server" />
                                <asp:HiddenField ID="hfEntryPicPathOriginal" runat="server" />
                                <div id="entryPicFileQueue"></div>
                                <input type="file" name="uploadify" id="entryPicUploadify" />
                                 <div id="entryPicListOriginal" style="display:block;"></div>
                                <div id="entryPicList" style="display:block;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="背景颜色" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtbgcolor" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="横幅图片" />：
                            </td>
                            <td height="25">
                                <asp:HiddenField ID="hfBannerPicPath" runat="server" />
                                <asp:HiddenField ID="hfBannerPicPathOriginal" runat="server" />
                                <div id="bannerPicFileQueue"></div>
                                <input type="file" name="uploadify" id="bannerPicUploadify" />
                                <div id="bannerPicListOriginal"></div>
                                <div id="bannerPicList"></div>
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
