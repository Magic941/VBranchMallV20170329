<%@ Page Title="<%$Resources:CMS,ContentptModify%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.CMS.Content.Modify" %>

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
    <!--Select2 Start-->
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <!--Select2 End-->
    <!--jQueryUploadify Start-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='dropClass']").select2();


            $("#delattach").hide();
            $("#attachpath").hide();

            if ($("[id$='hfs_Attachment']").val()) {
                $("#delattach").show();
                $("#attachpath").show();
                $("#attachpath").attr('href', $("[id$='hfs_Attachment']").val().format(''));
            }

            $("[id$='uploadify']").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSUploadImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
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
                        $("[id$='HiddenField_ISModifyImage']").val(1);
                    }
                    else {
                        ShowFailTip("图片上传失败！", 2000);
                    }
                }
            });

            $("[id$='FileAttachment']").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSUploadFile.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'width': 76,
                'height': 25,
                'queueID': 'div_Attachment',
                'auto': true,
                'multi': true,
                'fileExt': '*rar;*zip;*doc;*docx;*xls;*xlsx;*jpg;*gif;*png',
                'fileDesc': 'Video Files (.RAR,.ZIP,.DOC,.DOCX,.XLS,.XLSX,.JPG,.GIF,.PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("[id$='hfs_Attachment']").val(response.split('|')[1]);
                        $("[id$='HiddenField_IsModifyAttachment']").val(1);
                        $("#delattach").show();
                        $("#attachpath").show();
                        $("#attachpath").attr('href', response.split('|')[1].format(''));
                        ShowSuccessTip("<%= Resources.CMS.ContentTooltipUpload %>", 5000);
                    } else {
                        ShowFailTip("文件上传失败！", 2000);
                    }
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    if (errorObj.info == "1048576") {
                        ShowFailTip('上传文件过大', 2000);
                    } else {
                        ShowFailTip('上传文件发生错误, 状态码: [' + errorObj.info + ']', 2000);

                    }
                }
            });

            $("#delattach").click(function () {
                $("#delattach").hide();
                $("#attachpath").hide();
                $("#attachpath").attr('href', '');
                $("[id$='hfs_Attachment']").val("");
                $("[id$='HiddenField_IsDeleteAttachment']").val(1);
                ShowSuccessTip("<%= Resources.CMS.ContentTooltipDelete %>", 2000);
            });
        });
    </script>
    <!--jQueryUploadify End-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfClassID" runat="server" />
    <asp:HiddenField ID="HiddenField_ISModifyImage" runat="server" />
    <asp:HiddenField ID="HiddenField_IsModifyAttachment" runat="server" />
    <asp:HiddenField ID="HiddenField_IsDeleteAttachment" runat="server" />
    <asp:HiddenField ID="HiddenField_OldAttachPath" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ContentptModify%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,ContentlblModify%>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">扩展信息</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:;">SEO优化</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr style="display: none">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ContentlblContentID%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:Label ID="lblContentID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,ContentlblCreatedID%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMS,ContentlblClass%>" />
                                        ：
                                    </td>
                                    <td height="25">
                                        <asp:DropDownList ID="dropClass" runat="server" Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblTitle%>" />：
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
                                            OkMessage="输入正确" ControlToValidate="txtTitle" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="请输入标题名称，长度限制在40个字符以内！" LowerBound="1"
                                                    UpperBound="40" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal21" runat="server" Text="SeoURL地址" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSeoUrl" runat="server" Width="371px" MaxLength="25"></asp:TextBox>
                                        <%--  <asp:CheckBox ID="chkIndexChar" runat="server" Text="拼音" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="vertical-align: top; height: 50px;">
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,Content%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtContent" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtKeywords" runat="server" Width="500px" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div class="msgNormal">
                                            <asp:Literal ID="Literal13" runat="server" Text="请输入关键字以逗号分割，长度限制在25个字符以内！"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,State %>" />：
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
                                        <asp:Literal ID="Literal11" runat="server" Text="是否静态化" />：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkStatic" Text="是" runat="server" Checked="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal18" runat="server" Text="首页静态化" />：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkIndex" Text="是" runat="server" Checked="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr runat="server" id="TrSubTitle">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblSubtitled%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSubTitle" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="TrSummary">
                                    <td class="td_class" style="vertical-align: top; height: 50px;">
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblIntro%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSummary" runat="server" Width="500px" Height="100px" TextMode="MultiLine"
                                            Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr1">
                                    <td class="td_class">
                                        来源：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtBeFrom" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trImageUrl" runat="server">
                                    <td class="td_class" style="margin-top: 10px;">
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$Resources:Site,lblThumbnails%>" />：
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
                                <tr runat="server" id="TrLinkUrl">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:CMS,ContentlblLinkUrl%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtLinkUrl" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div class="msgNormal">
                                            <asp:Literal ID="lblHelpSecret" runat="server" Text="<%$Resources:CMS,ContenttoortipUrl %>"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,lblOrder %>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtOrders" runat="server" Text="1" Width="100px" MaxLength="6" onkeyup="value=value.replace(/[^\d]/g,'') "
                                            onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                                            Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:CMS,ContentlblContentProperty %>" />：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkIsHot" Text="<%$Resources:CMS,ContentlblHotSpot%>" runat="server"
                                            Checked="False" />
                                        <div style="margin-top: -20px; margin-left: 100px;">
                                            <asp:CheckBox ID="chkIsColor" Text="<%$Resources:CMS,ContentlblEyeCatching%>" runat="server"
                                                Checked="False" /></div>
                                        <div style="margin-top: -20px; margin-left: 190px;">
                                            <asp:CheckBox ID="chkIsTop" Text="<%$Resources:CMS,ContentlblTop%>" runat="server"
                                                Checked="False" /></div>
                                        <div style="margin-top: -20px; margin-left: 290px;">
                                            <asp:CheckBox ID="chkIsRecomend" Text="<%$Resources:CMS,ContentlblRecommend%>" runat="server"
                                                Checked="False" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$Resources:CMS,ContentlblAppendix%>" />：
                                    </td>
                                    <td height="25" style="padding-left: 5px">
                                        <asp:HiddenField ID="hfPvCount" runat="server" />
                                        <asp:HiddenField ID="hfs_Attachment" runat="server" />
                                        <div id="div_Attachment">
                                        </div>
                                        <input type="file" name="uploadify" id="FileAttachment" />
                                        <a id="delattach" href="javascript:void(0);">删除</a> <a id="attachpath" target="_blank">
                                            预览</a>
                                    </td>
                                </tr>
                                <tr runat="server" id="TrRemary">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$Resources:Site,remark%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtRemary" runat="server" Width="500px" MaxLength="100"></asp:TextBox><asp:HiddenField
                                            ID="hfCreatedDate" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">
                            页面标题 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Title" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            页面描述 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Description" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            页面关键词 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="372px">
                            </asp:TextBox>
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
                                        OnClick="btnSave_Click" class="adminsubmit_short" OnClientClick="return PageIsValid();">
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

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                             'superscript', 'subscript', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|', 'removeformat', 'formatmatch', 'autotypeset', '|', 'pasteplain', '|', 'customstyle',
                'paragraph', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', 'insertvideo', 'map', 'horizontal', '|',
                'link', 'unlink', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('<%=txtContent.ClientID %>'); //将编译器渲染到容器
    </script>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
