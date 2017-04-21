<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Photo.Add" Title="<%$Resources:CMSPhoto,ptAddPhoto %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/jquery.uploadify/uploadify-v3.1/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/Admin/js/jquery.uploadify/uploadify-v3.1/jquery.uploadify-3.1.min.js"
        type="text/javascript"></script>
    <link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/maticsoft.jqueryui.dialog.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(document).ready(function () {
        var strId = "";
        //图片
        $("#uploadify").uploadify({
            formData: { userId:<%= strUserId %>, album: 0 },
            swf: '/Admin/js/jquery.uploadify/uploadify-v3.1/uploadify.swf',
            uploader: '/UploadPhotoHandler.aspx',
            method: 'post',
            queueID: "fileQueue",
            buttonImg: '/images/uploadfile.png',
            auto: true,
            multi: true,
            fileTypeExts: '*.jpg;*.gif;*.png;*.bmp',
            fileTypeDesc: 'Image Files (.JPG, .GIF, .PNG)',
            queueSizeLimit: 10,
            fileSizeLimit: 1024 * 1024 * 10,
            onDialogOpen: function (file) {
                $("#uploadify").uploadify("settings", "album", 10);
            },
            onUploadStart: function (file) {
                $("#uploadify").uploadify('settings', 'formData', {'folder': '/Upload/CMS/Photo/', 'userId':<%= strUserId %>, 'album': $("[id$='DropAlbum']").find(":selected").val() });
            },
            onUploadSuccess: function (file, data, response) {
                if (!data) {
                    return;
                }
                strId += data + ",";
            
            },
            onQueueComplete: function (file) {
                clickautohide(4, "<%= Resources.CMSPhoto.TooltipAddSucceed %>", 3000);
                window.location = "AddPhotoInfo.aspx?idlist=" + strId;
            }
        });
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAddPhoto %>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMSPhoto,lblAddPhoto %>"/>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblChooseAlbum %>"/>：
                    <asp:DropDownList ID="DropAlbum" runat="server" Width="206px">
                    </asp:DropDownList>
                </td>
                <td>
                    <input type="file" name="uploadify" id="uploadify" />
                </td>
                <td style="width: 70%">
                </td>
            </tr>
            <tr>
                <td class="tdbg" style="text-align: center" colspan="2">
                    <div id="fileQueue">
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
