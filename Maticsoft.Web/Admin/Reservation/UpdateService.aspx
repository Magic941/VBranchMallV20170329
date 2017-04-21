<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="UpdateService.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Reservation.UpdateService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script src="/admin/js/jquery/ProductImage.helper.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function SubForm() {
            var hfProductImages;
            hfProductImages = $('[id$=hfProductImages]');
            hfProductImages.val('');
            $('.ImgUpload').each(function () {
                var img = $(this).find("input[type=hidden]").val();
                if (img) {
                    hfProductImages.val(hfProductImages.val() + '|' + img);
                }
            });
        }
    </script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/jqueryui/css/jquery-ui-timepicker-addon.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfProductImages" runat="server" />
    <asp:HiddenField ID="hfProductImagesThumbSize" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        修改服务
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以修改新的服务信息
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
                                服务类型 ：
                            </td>
                            <td height="25">
                                <select id="ddlServiceType" name="Rank" cssclass="select2" style="width: 205px" runat="server">
                                    <option value="-1">--请选择--</option>
                                    <option value="1">KTV</option>
                                    <option value="2">酒店</option>
                                    <option value="3">看房</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                服务名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtServiceName" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtServiceName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                是否需要支付 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rblPay" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="true" Text="是">
                                    </asp:ListItem>
                                    <asp:ListItem Value="false" Text="否">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                预约限定量 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rblRuleType" name="ruleType" runat="server" RepeatDirection="Horizontal">
                                    <%-- <asp:ListItem Value="0" Text="时间限定" Selected="True">
                                    </asp:ListItem>--%>
                                    <asp:ListItem Value="1" Text="每日量限定" Selected="True">
                                    </asp:ListItem>
                                    <asp:ListItem Value="2" Text="总量限定">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                开始时间 ：
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                结束时间：<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                最大限定 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtMaxCount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align: top;">
                                内容 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" Width="600px" TextMode="MultiLine"
                                    Height="150px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                备注 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="600px" TextMode="MultiLine" Height="150px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align: top;">
                                简介 ：
                            </td>
                            <td height="25">
                                <div>
                                    <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Width="594px"></asp:TextBox>
                                    <%-- <div id="progressbar1" class="progress" style="float: left;">
                                    </div>--%>
                                </div>
                                (字数限制为300个)
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                商品图片 ：
                            </td>
                            <td height="25">
                                <ul class="product_upload_img_ul" style="display: block">
                                    <li>
                                        <div class="ImgUpload ">
                                            <asp:HiddenField ID="hfImage0" runat="server" />
                                            <span id="a1" class="cancel" style="display: none; z-index: 999999; width: 127px;"><a
                                                class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden;">
                                                    <input type="file" class="file_upload" id="file_upload0" />
                                                </span>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ImgUpload">
                                            <asp:HiddenField ID="hfImage1" runat="server" />
                                            <span id="Span1" class="cancel" style="display: none; z-index: 999999; width: 127px;">
                                                <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden;">
                                                    <input type="file" class="file_upload" id="file_upload1" />
                                                </span>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ImgUpload">
                                            <asp:HiddenField ID="hfImage2" runat="server" />
                                            <span id="Span3" class="cancel" style="display: none; z-index: 999999; width: 127px;">
                                                <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden;">
                                                    <input type="file" class="file_upload" id="file_upload2" />
                                                </span>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ImgUpload">
                                            <asp:HiddenField ID="hfImage3" runat="server" />
                                            <span id="Span5" class="cancel" style="display: none; z-index: 999999; width: 127px;">
                                                <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden;">
                                                    <input type="file" class="file_upload" id="file_upload3" />
                                                </span>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ImgUpload">
                                            <asp:HiddenField ID="hfImage4" runat="server" />
                                            <span id="Span7" class="cancel" style="display: none; z-index: 999999; width: 127px;">
                                                <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden;">
                                                    <input type="file" class="file_upload" id="file_upload4" />
                                                </span>
                                        </div>
                                    </li>
                                    <li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal">
                                    <asp:Literal ID="Literal32" runat="server" Text="请选择有效的图片文件，第一张图片为产品主图，建议将图片文件的大小限制在200KB以内。" /></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnSave_Click"
                                    OnClientClick="return SubForm();"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="取消" class="adminsubmit_short" OnClick="btnCancle_Click"
                                    CausesValidation="false"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({
            //实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css',
            toolbars: [

               ['fullscreen',
                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
                'insertimage', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'link', 'unlink', '|']
                 ],
            initialContent: '', autoHeightEnabled: false,
            minFrameHeight: 200,
            pasteplain: false,
            wordCount: false,
            elementPathEnabled: false,
            autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render($('[id$=txtSummary]').get(0)); //将编译器渲染到容器
    </script>
    <%--<script type="text/javascript">
        $(function () {

            $('input[type="radio"][name$="rblRuleType"]').click(function () {
                var rule = $(this).val();
                if (rule == 0) {
                    $("#time").attr("style", "display:''");
                    $("#count").attr("style", "display:none");
                } else {
                    $("#count").attr("style", "display:''");
                    $("#time").attr("style", "display:none");
                }
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$=txtStartDate]").prop("readonly", true).datetimepicker({
                showSecond: true, //显示秒
                timeFormat: "HH:mm:ss", //格式化时间
                stepHour: 2, //设置步长
                stepMinute: 10,
                stepSecond: 10,
                defaultDate: "+1w",
                changeMonth: true,
                // dateFormat: "yy-mm-dd",
                minDate: new Date()
                //                onClose: function (selectedDate) {
                //                    $("#txtEndDate").datepicker("option", "minDate", selectedDate);
                //                }
            });
            $("[id$=txtEndDate]").prop("readonly", true).datetimepicker({
                showSecond: true, //显示秒
                timeFormat: "HH:mm:ss", //格式化时间
                stepHour: 2, //设置步长
                stepMinute: 10,
                stepSecond: 10,
                defaultDate: "+1w",
                changeMonth: true,
                // dateFormat: "yy-mm-dd",
                minDate: new Date(),
                onClose: function (selectedDate) {
                    $("[id$=txtStartDate]").datepicker("option", "maxDate", selectedDate);
                    $("[id$=txtEndDate]").val($(this).val());
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
