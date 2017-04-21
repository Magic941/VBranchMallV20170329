<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.CMS.Video.Add" Title="<%$ Resources:CMSVideo, ptVideoAdd %>" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("[id$='radLocalVideo']").attr("checked") == "checked") {
                $("[id$='trLocalVideo']").show();
                $("[id$='trOnlineVideo']").hide();
            }
            if ($("[id$='radOnlineVideo']").attr("checked") == "checked") {
                $("[id$='trOnlineVideo']").show();
                $("[id$='trLocalVideo']").hide();

            }

        });

        $(function () {
            //选择模式
            //本地
            if ($("[id$='radLocalVideo']").click(function () {
                $("[id$='trLocalVideo']").show();
                $("[id$='trOnlineVideo']").hide();
                $("[id$='radOnlineVideo']").attr("checked", false);
            }));

            //网络
            if ($("[id$='radOnlineVideo']").click(function () {
                $("[id$='trOnlineVideo']").show();
                $("[id$='trLocalVideo']").hide();
                $("[id$='radLocalVideo']").attr("checked", false);
            }));

        });
    </script>
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/Maticsoftv5.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript" src="/admin/js/Maticsoftv5.js"></script>
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
 <link href="/Admin/js/jquery.uploadify/uploadify-v3.1/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/Admin/js/jquery.uploadify/uploadify-v3.1/jquery.uploadify-3.1.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //上传本地
            $("#uploadifyLocalVideo").uploadify({
                swf: '/Admin/js/jquery.uploadify/uploadify-v3.1/uploadify.swf',
                uploader: '/UploadVideoHandler.aspx',
                method: 'post',
                queueID: "fileQueueLocalVideo",
                buttonImg: '/images/uploadfile.png',
                auto: true,
                multi: false,
                fileExt: '*.flv;*.mp4',
                fileDesc: 'Video Files (*.flv,*.mp4)',
                queueSizeLimit: 1,
                sizeLimit: 1024 * 1024 * 30,
                onInit: function () {
                },
                onSelect: function (e, queueID, fileObj) {
                },
                onUploadSuccess: function (file, data, response) {
                    $("[id$=hfLocalVideo]").val(data.split('|')[0]);
                    $("#txtVideoImage").attr("src", data.split('|')[1]);
                    $("#txtVideoImage").show();
                    $("[id$=lblMsg]").text($("[id$=hfUploadSuccess]").val());
                    //clickautohide(4, "上传成功！", 3000);
                },
            });

        });

        function PageIsValid() {
            if ($("[id$='radLocalVideo']").attr("checked") != "checked" && $("[id$='radOnlineVideo']").attr("checked") != "checked") {

                clickautohide(1, "<%= Resources.CMSVideo.TooltipSwitchType %>", 3000);

                return false;
            }
            if ($("[id$='radLocalVideo']").attr("checked") == "checked") {

                $("[id$='radOnlineVideo']").attr("checked", false);

                if (!$("[id$='hfLocalVideo']").val()) {

                    clickautohide(1, "<%= Resources.CMSVideo.TooltipSwitchVideo %>", 3000);

                    return false;
                }

            }

            if ($("[id$='radOnlineVideo']").attr("checked") == "checked") {

                
                $("[id$='radLocalVideo']").attr("checked", false);
                if ($("[id$='txtOnlineVideo']").val()=="") {

                    clickautohide(1, "<%= Resources.CMSVideo.TooltipFillWebsite %>", 3000);

                    return false;

                }
                else {

                    $.ajax({
                        url: "/CheckNetworkVideo.aspx",
                        type: 'post',
                        dataType: 'text',
                        data: {
                            videoUrl: $("[id$='txtOnlineVideo']").val()
                        },
                        success: function (data) {

                            if (data == "false") {
                                //clickautohide(1, "您输入的视频播放页地址有误！",3000);
                                alert('<%= Resources.CMSVideo.TooltipVideoUrl %>');
                                return false;

                            }

                        }
                    });

                }

            }

            if ($("[id$='txtTitle']").val()=="") {
                $("[id$='txtTitle']").focus();
                return false;
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
                        <asp:Literal ID="ltlAdd" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAdd %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAddTip %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlType" runat="server" Text="<%$ Resources:CMSVideo,  ltlType %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButton ID="radLocalVideo" runat="server" Text="<%$ Resources:CMSVideo, LocalVideo %>" />
                                <asp:RadioButton ID="radOnlineVideo" runat="server" Text="<%$ Resources:CMSVideo, OnlineVideo %>" />
                            </td>
                        </tr>
                        <tr id="trLocalVideo" style="display: none">
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlUploadVideo" runat="server" Text="<%$ Resources:CMSVideo, ltlUploadVideo %>"></asp:Literal>：<asp:HiddenField
                                    ID="hfLocalVideo" runat="server" Value="" />
                            </td>
                            <td height="25" width="*" align="left">
                                <div id="fileQueueLocalVideo">
                                </div>
                                <input type="file" name="uploadifyLocalVideo" id="uploadifyLocalVideo" />
                                <br />
                                <a href="javascript:$('#uploadifyLocalVideo').uploadifyUpload()">【
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CMSVideo, ltlUpload %>"></asp:Literal>】</a>
                                &nbsp;&nbsp; <a href="javascript:$('#uploadifyLocalVideo').uploadifyClearQueue()">【
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:CMSVideo, ltlCancelUpload %>"></asp:Literal>】</a>
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                <br />
                                <img alt="" src="" id="txtVideoImage"  style="display: none;width: 80px;height: 80px"/>
                                <br />
                            </td>
                        </tr>
                        <tr id="trOnlineVideo" style="display: none">
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlSite" runat="server" Text="<%$ Resources:CMSVideo, ltlSite %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtOnlineVideo" runat="server" Width="372px" class="addinput" MaxLength="100"></asp:TextBox>
                                &nbsp;&nbsp; *
                                <asp:Literal ID="ltlSiteTip" runat="server" Text="<%$ Resources:CMSVideo, ltlSiteTip %>"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="ltlTitle" runat="server" Text="<%$ Resources:CMSVideo, ltlTitle %>"></asp:Literal>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTitle" runat="server" Width="372px" class="addinput" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25" width="*" align="left">
                                <div id="txtTitleTip" runat="server">
                                </div>
                                <Maticsoft:ValidateTarget ID="ValidateTargetTitle" runat="server" Description="<%$ Resources:CMSVideo, IDS_Message_Title_Description %>" OkMessage="输入正确"
                                    ControlToValidate="txtTitle" ContainerId="ValidatorContainer">
                                    <Validators>
                                        <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:CMSVideo, IDS_Message_Title_Description %>"
                                            LowerBound="1" UpperBound="200" />
                                    </Validators>
                                </Maticsoft:ValidateTarget>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="ltlDescription" runat="server" Text="<%$ Resources:CMSVideo, Description %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="372px" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:DropDownList ID="dropAlbumID" runat="server" Width="377px">
                                </asp:DropDownList>
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
                                <asp:Literal ID="ltlPrivacy" runat="server" Text="<%$ Resources:CMSVideo, ltlPrivacy %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlPrivacy" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Selected="True" Value="0" Text="<%$ Resources:CMSVideo, Open %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$ Resources:CMSVideo, Private %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, SemiOpen %>"></asp:ListItem>
                                </asp:RadioButtonList>
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
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkAddContinue" runat="server" Text="<%$ Resources:SysManage, TooltipAddContinue%>">
                                </asp:CheckBox>
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
