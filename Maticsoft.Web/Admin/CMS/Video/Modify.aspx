<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.CMS.Video.Modify" Title="<%$ Resources:CMSVideo, ptVideoModify %>" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/Maticsoftv5.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript" src="/admin/js/Maticsoftv5.js"></script>
    <link href="/Scripts/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery.uploadify/uploadify-v2.1.0/swfobject.js"></script>
    <script src="/Scripts/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //上传本地
            $("[id$='uploadifyLocalVideo']").uploadify({
                'uploader': '/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/Ajax_Handle/UploadLocalVideoHandler.ashx',
                'cancelImg': '/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'folder': '/UploadFolder/',
                'queueID': 'fileQueueLocalVideo',
                'buttonImg': '/admin/images/uploadfile.png',
                'width': '92',
                'height': '24',
                'auto': false,
                'multi': false,
                'fileExt': '*.flv;*.mp4',
                'fileDesc': 'Video Files (*.flv,*.mp4)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 30,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },

                'onComplete': function (event, queueId, fileObj, response, data) {

                    $("[id$='hfNewLocalVideo']").val(response.split('|')[1]);

                    $("[id$='lblMsg']").text($("[id$='hfUploadSuccess']").val());
                    //clickautohide(4, "上传成功！", 3000);
                }
            });

            //上传附件
            $("[id$='uploadifyAttachment']").uploadify({
                'uploader': '/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/Ajax_Handle/UploadAttachmentHandler.ashx',
                'cancelImg': '/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'folder': '/UploadFolder/',
                'queueID': 'fileQueueAttachment',
                'buttonImg': '/admin/images/uploadfile.png',
                'width': '92',
                'height': '24',
                'auto': true,
                'multi': false,
                'fileExt': '*.rar;*.zip;*.doc;*.docx,*.xls;*.xlsx;*.jpg;*.jpeg;*.gif;*.png',
                'fileDesc': ' Attachment Files (.rar, .zip,.doc,.docx,.xls,.xlsx,.jpg,*.jpeg,.gif,.png)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 30,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },

                'onComplete': function (event, queueId, fileObj, response, data) {

                    var attachment = response.split('|')[1];
                    $("[id$='hfNewAttachment']").val(attachment);
                    $("[id$='divAttachment']").show();
                    $("[id$='lnkAttachment']").attr("href", "/UploadFolder/" + attachment);

                    $("[id$='lblStatus']").text($("[id$='hfUploadSuccess']").val());
                    //clickautohide(4, "上传附件成功！", 3000);
                }
            });

        });

        function PageIsValid() {


            if ($("[id$='txtTitle']").val() == "") {

                return false;
            }

            if ($("[id$='hfUrlType']").val() == 1) {

                if ($("[id$='txtOnlineVideo']").val() == "") {

                    clickautohide(1, "<%= Resources.CMSVideo.TooltipFillWebsite %>", 3000);

                    return false;

                }
                else {

                    $.ajax({
                        url: "/Ajax_Handle/CheckNetworkVideo.aspx",
                        type: 'post',
                        dataType: 'text',
                        async: true, // 同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
                        timeout: 10000,
                        data: {
                            videoUrl: $("[id$='txtOnlineVideo']").val()
                        },
                        success: function (data) {

                            if (data == "false") {
                                //clickautohide(1, "您输入的视频播放页地址有误！", 3000);
                                //alert('<%= Resources.CMSVideo.TooltipVideoUrl %>');
                                return false;

                            }

                        }
                    });

                }

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfUploadSuccess" runat="server" Value="<%$ Resources:CMSVideo, hfUploadSuccess %>" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlAdd" runat="server" Text="<%$ Resources:CMSVideo, ptVideoModify %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoModifyTip %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="25" width="*" align="left">
                        <Maticsoft:StatusMessage ID="statusMessage" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlType" runat="server" Text="<%$ Resources:CMSVideo, ltlType %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:HiddenField ID="hfUrlType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlTitle" runat="server" Text="<%$ Resources:CMSVideo, ltlTitle %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTitle" runat="server" Width="375px" class="addinput" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25" width="*" align="left">
                                <div id="txtTitleTip" runat="server">
                                </div>
                                <Maticsoft:ValidateTarget ID="ValidateTargetTitle" runat="server" Description="<%$ Resources:CMSVideo, IDS_Message_Title_Description %>"
                                    ControlToValidate="txtTitle" ContainerId="ValidatorContainer">
                                    <Validators>
                                        <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:CMSVideo, IDS_Message_Title_Description %>"
                                            LowerBound="1" UpperBound="200" />
                                    </Validators>
                                </Maticsoft:ValidateTarget>
                                <br />
                            </td>
                        </tr>
                        <tr id="trLocalVideo" runat="server" visible="false">
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlUploadVideo" runat="server" Text="<%$ Resources:CMSVideo, ltlUploadVideo %>"></asp:Literal>：<asp:HiddenField
                                    ID="hfLocalVideo" runat="server" Value="" />
                                <asp:HiddenField ID="hfNewLocalVideo" runat="server" Value="" />
                            </td>
                            <td height="25" width="*" align="left">
                                <div id="fileQueueLocalVideo">
                                </div>
                                <input type="file" name="uploadifyLocalVideo" id="uploadifyLocalVideo" />
                                <br />
                                <a href="javascript:$('#uploadifyLocalVideo').uploadifyUpload()">【<asp:Literal ID="ltlUpload"
                                    runat="server" Text="<%$ Resources:CMSVideo, ltlUpload %>"></asp:Literal>】</a>
                                &nbsp;&nbsp; <a href="javascript:$('#uploadifyLocalVideo').uploadifyClearQueue()">【<asp:Literal
                                    ID="ltlCancelUpload" runat="server" Text="<%$ Resources:CMSVideo, ltlCancelUpload %>"></asp:Literal>】</a>
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr id="trOnlineVideo" runat="server" visible="false">
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlSite" runat="server" Text="<%$ Resources:CMSVideo, ltlSite %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtOnlineVideo" runat="server" Width="375px" class="addinput" MaxLength="200"></asp:TextBox>
                                *
                                <asp:Literal ID="ltlSiteTip" runat="server" Text="<%$ Resources:CMSVideo, ltlSiteTip %>"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlDescription" runat="server" Text="<%$ Resources:CMSVideo, Description %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="374px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlTags" runat="server" Text="<%$ Resources:CMSVideo, ltlTags %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTags" runat="server" Width="372px" class="addinput" MaxLength="100"></asp:TextBox>
                                <asp:Literal ID="ltlTagsTip" runat="server" Text="<%$ Resources:CMSVideo, ltlTagsTip %>"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlAlbum" runat="server" Text="<%$ Resources:CMSVideo, ltlAlbum %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:DropDownList ID="dropAlbumID" runat="server" Width="375px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlDuration" runat="server" Text="<%$ Resources:CMSVideo, ltlDuration %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTotalHours" runat="server" Width="40px" MaxLength="2" onkeyup="value=value.replace(/[^\d]/g,'') "
                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right;"></asp:TextBox><b>:</b>
                                <asp:TextBox ID="txtMinutes" runat="server" Width="40px" MaxLength="2" onkeyup="value=value.replace(/[^\d]/g,'') "
                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right;"></asp:TextBox><b>:</b>
                                <asp:TextBox ID="txtSeconds" runat="server" Width="40px" MaxLength="2" onkeyup="value=value.replace(/[^\d]/g,'') "
                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right;"></asp:TextBox>&nbsp;&nbsp; (<asp:Literal ID="ltlHours"
                                        runat="server" Text="<%$ Resources:CMSVideo, ltlHours %>"></asp:Literal>：<asp:Literal
                                            ID="ltlMinutes" runat="server" Text="<%$ Resources:CMSVideo, ltlMinutes %>"></asp:Literal>：<asp:Literal
                                                ID="ltlSeconds" runat="server" Text="<%$ Resources:CMSVideo, ltlSeconds %>"></asp:Literal>)
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlCreatedUser" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedUser %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtCreatedUserID" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlCreatedDate" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedDate %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtCreatedDate" runat="server" Width="70px" onfocus="setday(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlCategory" runat="server" Text="<%$ Resources:CMSVideo, ltlCategory %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlVideoClassID" runat="server" RepeatDirection="Horizontal"
                                    align="left" RepeatColumns="6">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlImageUrl %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtImageUrl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlThumbImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlThumbImageUrl %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtThumbImageUrl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlNormalImageUrl" runat="server" Text="<%$ Resources:CMSVideo, ltlNormalImageUrl %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtNormalImageUrl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlDomain" runat="server" Text="<%$ Resources:CMSVideo, ltlDomain %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtDomain" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="ltlFormat" runat="server" Text="<%$ Resources:CMSVideo, ltlFormat %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtVideoFormat" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlPrivacy" runat="server" Text="<%$ Resources:CMSVideo, ltlPrivacy %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlPrivacy" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="0" Text="<%$ Resources:CMSVideo, Open %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$ Resources:CMSVideo, Private %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, SemiOpen %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="<%$ Resources:CMSVideo, NotYetReleased %>"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="<%$ Resources:CMSVideo, Screen %>"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="<%$ Resources:CMSVideo, Publish %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlIsRecommend" runat="server" Text="<%$ Resources:CMSVideo, IsRecommend %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:CheckBox ID="chkIsRecomend" Text="<%$ Resources:CMSVideo, Recommend %>" runat="server"
                                    Checked="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlSequence" runat="server" Text="<%$ Resources:CMSVideo, ltlSequence %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSequence" runat="server" Width="100px" MaxLength="6" onkeyup="value=value.replace(/[^\d]/g,'') "
                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                    Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlRemark" runat="server" Text="<%$ Resources:CMSVideo, ltlRemark %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Width="372px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="5" width="*" align="left">
                                <asp:HiddenField ID="hfAttachment" runat="server" Value="" />
                                <asp:HiddenField ID="hfNewAttachment" runat="server" Value="" />
                            </td>
                        </tr>
                        <tr id="trAttachment" runat="server">
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlAttachment" runat="server" Text="<%$ Resources:CMSVideo, ltlAttachment %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <div id="fileQueueAttachment">
                                </div>
                                <p>
                                    <input type="file" name="uploadifyAttachment" id="uploadifyAttachment" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="5">
                                <div id="divAttachment" style="<%=strStyle%>">
                                    <asp:HyperLink ID="lnkAttachment" runat="server" Target="_blank">
                                        【<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CMSVideo, ltlDownload %>"></asp:Literal>】</asp:HyperLink>
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Green"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClientClick="return PageIsValid();" OnClick="btnSave_Click" class="adminsubmit_short" />
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" />
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
        <br />
    </div>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
