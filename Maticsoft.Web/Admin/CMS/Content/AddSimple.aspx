<%@ Page Title="<%$Resources:CMS,ContentptAdd%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="AddSimple.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Content.AddSimple" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/Admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/Admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <!--jQueryUploadify Start-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#delattach").hide();
            $("#attachpath").hide();

            if ($("[id$='hfs_Attachment']").val()) {
                $("#delattach").show();
                $("#attachpath").show();
                $("#attachpath").attr('href', $("[id$='hfs_Attachment']").val().format(''));
            }

            $("[id$='txtTitle']").blur(function () {
                var str = $(this).val().replace(/^\s+|\s+$/g, "");
                $(this).val(str);
            });

            $("[id$='uploadify']").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSUploadImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'queueID': 'fileQueue',
                'width': 76,
                'height': 25,
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {

                    var responseJSON = $.parseJSON(response);
                    if (responseJSON.success) {
                        $("[id$='imgUrl']").attr("src", responseJSON.data.format(''));
                        $("[id$='HiddenField_ICOPath']").val(responseJSON.data);
                    }
                    else {
                        ShowFailTip("图片上传失败！", 2000);
                    }
                },

                'onError': function (event, ID, fileObj, errorObj) {
                    ShowFailTip('上传文件发生错误, 状态码: [' + errorObj.info + ']', 2000);
                }
            });


            $("#delattach").click(function () {
                $("[id$='hfs_Attachment']").val("");
                $("#delattach").hide();
                $("#attachpath").hide();
                $("#attachpath").attr('href', '');
                ShowSuccessTip("<%=Resources.CMS.ContentTooltipDelete %>", 2000);
            });

        });
    </script>
    <!--jQueryUploadify End-->
    <!--Select2 Start-->
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[id$='ddlType']").select2(); });
    </script>
    <!--Select2 End-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfClassID" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ContentptAdd%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,ContentlblAdd%>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ContentlblClass%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:DropDownList ID="ddlType" runat="server" Width="381px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtTitle" runat="server" Width="500px" MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div id="txtTitleTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="标题不能为空，长度限制在40个字符以内！"
                                            OkMessage="输入标题正确" ControlToValidate="txtTitle" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="请输入标题名称，长度限制在40个字符以内！" LowerBound="1"
                                                    UpperBound="40" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                        <br />
                                    </td>
                                </tr>
                                <tr runat="server" id="TrSummary">
                                    <td class="td_class" style="vertical-align: top; height: 50px;">
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblIntro%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSummary" runat="server" Width="500px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="vertical-align: top; height: 50px;">
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,Content%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtContent" runat="server" Width="80%" TextMode="MultiLine" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trImageUrl" runat="server">
                                    <td class="td_class" style="margin-top: 10px;">
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblThumbnails%>" />:
                                    </td>
                                    <td height="25" style="padding-left: 5px">
                                        <asp:HiddenField ID="HiddenField_ICOPath" runat="server" />
                                        <div id="fileQueue">
                                        </div>
                                        <input type="file" name="uploadify" id="uploadify" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:Image ID="imgUrl" runat="server" Width="120" Height="120" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,State%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                          <asp:ListItem Value="0" Text="发布" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="待审核"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="草稿"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal20" runat="server" Text="同步到微博" />：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkSina" Text="新浪微博" runat="server" Checked="False" />
                                        <asp:CheckBox ID="chkQQ" Text="腾讯微博" runat="server" Checked="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
                class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                        OnClick="btnSave_Click" OnClientClick="return PageIsValid();" class="adminsubmit_short">
                                    </asp:Button>
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        OnClick="btnCancle_Click" class="adminsubmit_short" ValidationGroup="Group1">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

             ['fullscreen',
                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
                'insertimage', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|']
                 ],
            initialContent: '', autoHeightEnabled: false,
            minFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: true, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('<%=txtContent.ClientID %>'); //将编译器渲染到容器
    </script>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
