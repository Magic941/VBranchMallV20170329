<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.CMS.VideoAlbum.Modify" Title="<%$ Resources:CMSVideo, ptVideoAlbumModify %>" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/Maticsoftv5.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript" src="/admin/js/Maticsoftv5.js"></script>
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //上传图片
            $("[id$='uploadifyCoverVideo']").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/Ajax_Handle/UploadPictureHandler.ashx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'folder': '/UploadFolder/',
                'queueID': 'fileQueueCoverVideo',
                'buttonImg': '/admin/images/uploadfile.png',
                'width': '92',
                'height': '24',
                'auto': true,
                'multi': false,
                'fileExt': '*.jpg;*.jpeg;*.png;*.gif;*.bmp',
                'fileDesc': 'Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {

                },

                'onComplete': function (event, queueId, fileObj, response, data) {

                    var filename = response.split('|')[1];

                    if (filename.length > 0) {

                        $("[id$=hfNewCoverVideo]").val(filename);

                        $("[id$=imgCoverVideo]").attr("src", "/UploadFolder/" + filename);

                        $("[id$=lblUploadSuccess]").text($("[id$=hfUploadSuccess]").val());
                        //clickautohide(4, "上传成功！", 3000);
                    }
                    else {

                        $("[id$=lblUploadFails]").text($("[id$=hfUploadFails]").val());
                        //clickautohide(5, "上传失败！", 3000);
                    }
                }
            });

        });  
    </script>
    <asp:HiddenField ID="hfUploadFails" runat="server" Value="<%$ Resources:CMSVideo, hfUploadFails %>" />
    <asp:HiddenField ID="hfUploadSuccess" runat="server" Value="<%$ Resources:CMSVideo, hfUploadSuccess %>" />
    <div class="newslistabout">
        <div class="newslistabout">
            <div class="newslist_title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitle">
                             <asp:Literal ID="ltlModify" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumModify %>"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitlebody">
                            <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumModifyTip %>"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                </td>
                                <td height="10" width="*" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" valign="top">
                                   <asp:Literal ID="ltlCoverVideo" runat="server" Text="<%$ Resources:CMSVideo, ltlCoverVideo %>"></asp:Literal>
                                </td>
                                <td height="25">
                                    <asp:HiddenField ID="hfCoverVideo" runat="server" />
                                    <asp:HiddenField ID="hfNewCoverVideo" runat="server" Value="" />
                                    <div id="fileQueueCoverVideo">
                                    </div>
                                    <input type="file" name="uploadifyCoverVideo" id="uploadifyCoverVideo" />
                                    &nbsp; &nbsp;
                                    <asp:Label ID="lblUploadFails" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblUploadSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Image ID="imgCoverVideo" runat="server" Width="120" Height="120" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                     <asp:Literal ID="ltlName" runat="server" Text="<%$ Resources:CMSVideo, Name %>"></asp:Literal> ：
                                </td>
                                <td height="25">
                                    <asp:TextBox ID="txtAlbumName" runat="server" Width="375px" class="addinput" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <div id="txtAlbumNameTip" runat="server">
                                    </div>
                                    <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="<%$ Resources:CMSVideo, IDS_Message_AlbumName_Description %>"
                                        ControlToValidate="txtAlbumName" ContainerId="ValidatorContainer">
                                        <Validators>
                                            <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:CMSVideo, IDS_Message_AlbumName_Description %>"
                                                LowerBound="1" UpperBound="100" />
                                        </Validators>
                                    </Maticsoft:ValidateTarget>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" valign="top">
                                     <asp:Literal ID="ltlDescription" runat="server" Text="<%$ Resources:CMSVideo, Description %>"></asp:Literal> ：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtDescription" runat="server" Width="374px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>"></asp:Literal> ：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, Normal %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%$ Resources:CMSVideo, NotAudit %>"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                     <asp:Literal ID="ltlPrivacy" runat="server" Text="<%$ Resources:CMSVideo, ltlPrivacy %>"></asp:Literal> ：
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
                                    <asp:Literal ID="ltlSequence" runat="server" Text="<%$ Resources:CMSVideo, ltlSequence %>"></asp:Literal> ：
                                </td>
                                <td height="25" width="*" align="left">
                                    <asp:TextBox ID="txtSequence" runat="server" Width="100px" MaxLength="6" onkeyup="value=value.replace(/[^\d]/g,'') "
                                        onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                        Style="text-align: right"></asp:TextBox>
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
                                         OnClientClick="javascript:parent.$.colorbox.close();"  class="adminsubmit_short" />
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
        <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
