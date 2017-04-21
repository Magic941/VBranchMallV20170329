<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
         CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Themes.Add" Title="增加页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
          type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
            type="text/javascript"></script>

    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function() {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtExpireDate']").datepicker({ dateFormat: "yy-mm-dd", yearRange: "2012:2050" });

         
            $("#FileInfo").hide();
            $("[id$='lnkDelete']").click(function() {
                $.ajax({
                    url: "/UploadFile.aspx",
                    type: 'POST',
                    timeout: 10000,
                    async: false,
                    data: { Action: "Delete", FileUrl: $("[id$='hidFileUrl']").val() },
                    success: function(resultData) {
                        alert(resultData);
                        if (resultData == "Ok") {
                            $("[id$='hidFileUrl']").val("");
                            $("[id$='hfLogoUrl']").val("");
                            $("[id$='lnkDelete']").hide();
                            $("#FileInfo").hide();
                            clickautohide(4, "删除成功！", 2000);

                        } else {
                            clickautohide(4, "出现异常，请重试！", 2000);
                        }
                    }
                });

            });

            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': "/UploadFile.aspx",
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'width': 76,
                'height': 25,
                'auto': true,
                'multi': true,
                'fileExt': '*.zip',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024,
                'scriptData': { 'fName': $("[id$='txtFileName']").val() },
                'onInit': function() {
                },
                'onSelect': function(e, queueID, fileObj) {
                    $("#uploadify").uploadifySettings("scriptData", { "fName": $("[id$='txtFileName']").val() }); //动态更新配(执行此处时可获得值)
                },
                'onComplete': function(event, queueId, fileObj, response, data) {
                    if (response.split('|').length > 2) {
                        alert(response);
                        $("[id$='hidFileUrl']").val(response.split('|')[0]);
                        $("[id$='hidFileSize']").val(response.split('|')[1]);
                        $("[id$='hidfileEx']").val(response.split('|')[2]);
                        $("#fileName").text(response.split('|')[3]);
                        $("#FileInfo").show();
                        $("[id$='lnkDelete']").show();
                        alert("上传成功！");
                    } else {
                        alert("图片上传失败,请重试！");
                    }
                }
            });
        });

    </script>
    <!--SWF图片上传结束-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="上传文件" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="您可以给客户上传相应的文件" />
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
                                        名称：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtFileName" runat="server" Width="210px"></asp:TextBox>(用英文（如Default等）)
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFileName" ErrorMessage="必填字段"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td class="td_class">
                                        简介：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="210px" Height="51px" 
                                            TextMode="MultiLine"></asp:TextBox>
                                     </td>
                                </tr>
                                
                                   <tr>
                                    <td class="td_class">
                                        模版预览图：
                                    </td>
                                    <td height="25">
                                        <asp:FileUpload ID="FileImage" runat="server" Width="210" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileImage" runat="server" ErrorMessage="请选择正确的格式" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$"></asp:RegularExpressionValidator>
                      
                                     </td>
                                </tr>

                                <tr class="locaFile">
                                    <td class="td_class">
                                        上传模版：
                                    </td>
                                    <td height="25">
                                        <asp:HiddenField ID="hfLogoUrl" runat="server" />
                                            <asp:HiddenField ID="hidFileSize" runat="server" />
                                          <asp:HiddenField ID="hidFileUrl" runat="server" />
                                        <asp:HiddenField ID="hidfileEx" runat="server" />
                                        <div id="fileQueue">
                                        </div>
                                        <input type="file" name="uploadify" id="uploadify" />必须为Zip的压缩文件<br />
                                    </td>
                                </tr>
                                <tr id="FileInfo">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <span id="fileName"></span>
                                        <asp:HyperLink ID="lnkDelete" runat="server" Style="vertical-align: middle;">
                                            【<asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,btnDeleteText %>" />】</asp:HyperLink></td></tr><tr>
                                    <td class="td_class">
                                        作者： </td><td height="25">
                                        <asp:TextBox ID="txtAuthor" runat="server" 
                                            Width="210px"></asp:TextBox></td></tr><tr>
                                    <td class="td_class">
                                        语言： </td><td height="25">
                                        <asp:DropDownList ID="ddllanguage" runat="server"><asp:ListItem>中文</asp:ListItem><asp:ListItem>英文</asp:ListItem><asp:ListItem>法文</asp:ListItem><asp:ListItem></asp:ListItem></asp:DropDownList></td></tr></table></td></tr></table></div><table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
                                                                <tr>
                                                                    <td class="tdbg">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td style="height: 6px;">
                                                                                </td>
                                                                                <td height="6">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td_class">
                                                                                </td>
                                                                                <td height="25">
                                                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                                                                                class="adminsubmit_short" OnClientClick=" return PageIsValid(); " OnClick="btnSave_Click">
                                                                                    </asp:Button>
                                                                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                                                                                class="adminsubmit_short" CausesValidation="False" OnClick="btnCancle_Click"></asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
        </div>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>