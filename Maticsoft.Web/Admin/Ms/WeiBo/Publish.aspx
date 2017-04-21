<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="Publish.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.WeiBo.Publish" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#btnUploadImage').uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/UploadNormalImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.png',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'width': 75,
                'height': 25,
                'onSelect': function (e, queueID, fileObj) {
                    $("#ctl00_ContentPlaceHolder1_btnSave").hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("[id$='txtimgUrl']").attr("src", response.split('|')[1].format(''));
                        $("#ctl00_ContentPlaceHolder1_btnSave").show();
                    }
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    alert('上传图片大小不能超过2M，尺寸不能大于1280×1280');
                    alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
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
                        <asp:Literal ID="Literal1" runat="server" Text="微博消息管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行发布微博信息操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="微博消息" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDesc" runat="server" Width="360px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                           <tr>
                            <td class="td_class">
                                微博图片：
                            </td>
                            <td height="25">
                             <input id="btnUploadImage" type="button" value="上传图片" class="adminsubmit" />
                            </td>
                        </tr>
                            <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Image ID="txtimgUrl" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="发布"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
