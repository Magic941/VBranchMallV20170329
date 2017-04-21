<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.Advertisement.Add"
    Title="增加页" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <link href="/admin/Service/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="/admin/Service/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            baseLoad();
            $("[id$='btnCancle']").click(function () {
                var Pid = $.getUrlParam("id");
                window.location = "SingleList.aspx?id=" + Pid;
                return false;
            });

            $("[id$='btnSave']").click(function () {
                var d1 = $("[id$=txtStartDate]").val();
                var d2 = $("[id$=txtEndDate]").val();
                if (d1 && CheckDate(d1)) {
                    ShowFailTip("请选择正确的开始时间！");
                    return false;
                }
                if (d2 && CheckDate(d2)) {
                    ShowFailTip("请选择正确的结束时间！");
                    return false;
                }
                if (new Date(Date.parse(d1.replace("-", "/")))> new Date(Date.parse(d2.replace("-", "/")))) {
                    ShowFailTip("开始时间不能大于结束时间，请重新选择日期！");
                    return false;
                }
            });

            $("[id$='rbTextContent']").click(function () {
                baseLoad();
            });
            $("[id$='rbImgContent']").click(function () {
                baseLoad();
            });
            $("[id$='rbFlashContent']").click(function () {
                baseLoad();
            });
            $("[id$='rbCodeContent']").click(function () {
                baseLoad();
            });

            $("#ctl00_ContentPlaceHolder1_txtEnterpriseID").autocompleteEnte("/Admin/Service/GetData.asmx/GetEnteName", {
                httpMethod: "POST",                                            //使用POST调用WebService
                dataType: 'json',                                                     //返回数据类型为XML
                minchar: 0,                                                           //最小响应字符数量
                selectFirst: false,                                                   //默认不选中第1条
                max: 12,                                                              //列表里的条数目
                minChars: 1,                                                        //自动完成激活之前填入的最小字符
                width: 265,                                                         //提示的宽度，溢出隐藏
                scrollHeight: 300,                                               //提示的高度，溢出显示滚动条
                matchContains: true,                                          //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false,                                                     //自动填充
                //格式化选项,由于WebService返回的数据是JSON格式,现在要转成HTML以TABLE形式显示
                formatItem: function (result, i, max) {
                    //这里返回的result为一个已封装好的JSON对象
                    //i为第几条数据，从1开始；max为共多少条数据
                    //返回参数里也可以只有一个参数result
                    var item = "<table id='auto" + i + "'><tr><td align='left'>" + result.name + "</td></tr></table>";
                    return item;
                }
            });

            //绑定日期控件
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();
            var day = today.getDate();
            $("[id$='txtStartDate']").prop("readonly", true).datepicker({
                minDate: new Date(),
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });
            $("[id$='txtEndDate']").prop("readonly", true).datepicker({
                minDate: new Date(year, month, day + 1), //结束日期为当天+1
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });

        });
        function baseLoad() {
            if ($("[id$='rbTextContent']").attr("checked")) {
                $(".filePath").hide();
                $(".SwffilePath").hide();
                $(".advHtml").hide();
                $(".NavUrl").show();
                $(".NavigateUrl").show();
                $(".AlternateText").show();
            }
            if ($("[id$='rbImgContent']").attr("checked")) {

                $(".filePath").show();
                $(".advHtml").hide();
                $(".SwffilePath").hide();
                $(".NavigateUrl").show();
                $(".AlternateText").show();
                $(".NavUrl").show();
                $(".fileUrl").text("图片地址：");
            }
            if ($("[id$='rbFlashContent']").attr("checked")) {
                $(".filePath").hide();
                $(".SwffilePath").show();
                $(".advHtml").hide();
                $(".NavigateUrl").hide();
                $(".AlternateText").hide();
                $(".NavUrl").hide();
                $(".fileUrl").text("Flash地址：");
            }
            if ($("[id$='rbCodeContent']").attr("checked")) {
                $(".filePath").hide();
                $(".SwffilePath").hide();
                $(".advHtml").show();
                $(".NavUrl").hide();
                $(".NavigateUrl").hide();
                $(".AlternateText").hide();
            }
        }
    </script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/UploadNormalImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'width': 76,
                'height': 25,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("[id$='hfFileUrl']").val(response.split('|')[1]);
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });
    </script>
    <!--SWF图片上传结束-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#swfUp").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSContent.aspx?action=uploadSwf',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.png',
                'folder': 'UploadFile',
                'queueID': 'swfQueue',
                'auto': true,
                'multi': true,
                'fileExt': '*.swf',
                'fileDesc': '文件类型 (.SWF)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    var jsonData = eval("(" + response.split('|')[1] + ")");
                    switch (jsonData.Status) {
                        case "OK":
                            $("[id$='hfSwfUrl']").val(jsonData.SavePath);
                            alert("上传成功！");
                            break;
                        case "Failed":
                            alert(jsonData.ErrorMessage);
                            break;
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
                        【<asp:Literal ID="Literal2" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="新增广告内容" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">扩展信息</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="myTab1_Content0">
                    <table style="width: 100%; border-bottom: none; border-top: none;" cellpadding="2"
                        cellspacing="1" class="border">
                        <tr>
                            <td class="tdbg">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="td_class">
                                            广告名称 ：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtAdvertisementName" runat="server" Width="371px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                        </td>
                                        <td height="25">
                                            <div id="txtAdvertisementNameTip" runat="server">
                                            </div>
                                            <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="广告名称不能为空，长度限制在20个字符以内！"
                                                OkMessage="输入正确" ControlToValidate="txtAdvertisementName" ContainerId="ValidatorContainer">
                                                <Validators>
                                                    <Maticsoft:InputStringClientValidator ErrorMessage="请输入广告名称，长度限制在20个字符以内！" LowerBound="1"
                                                        UpperBound="20" />
                                                </Validators>
                                            </Maticsoft:ValidateTarget>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            展现方式：
                                        </td>
                                        <td height="25">
                                            <asp:RadioButton ID="rbTextContent" runat="server" GroupName="AdType" Checked="true"
                                                Text="文字" />
                                            <asp:RadioButton ID="rbImgContent" runat="server" GroupName="AdType" Text="图片" />
                                            <asp:RadioButton ID="rbFlashContent" runat="server" GroupName="AdType" Text="Flash" />
                                            <asp:RadioButton ID="rbCodeContent" runat="server" GroupName="AdType" Text="代码" />
                                        </td>
                                    </tr>
                                    <tr class="filePath">
                                        <td class="td_class">
                                            <span class="fileUrl"></span>
                                        </td>
                                        <td height="25">
                                            <asp:HiddenField ID="hfFileUrl" runat="server" />
                                            <div id="fileQueue">
                                            </div>
                                            <input type="file" name="uploadify" id="uploadify" /><br />
                                        </td>
                                    </tr>
                                    <tr class="SwffilePath">
                                        <td class="td_class">
                                            <span class="fileUrl"></span>
                                        </td>
                                        <td height="25">
                                            <asp:HiddenField ID="hfSwfUrl" runat="server" />
                                            <div id="swfQueue">
                                            </div>
                                            <input type="file" name="uploadify" id="swfUp" /><br />
                                        </td>
                                    </tr>
                                    <tr class="AlternateText">
                                        <td class="td_class">
                                            广告语：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtAlternateText" runat="server" Width="371px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="NavigateUrl">
                                        <td class="td_class">
                                            链接地址：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtNavigateUrl" runat="server" Width="371px" Text="http://"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%-- <tr class="NavUrl" style="display:none;">
                                        <td class="td_class">
                                        </td>
                                        <td height="25">
                                            <div id="txtNavigateUrlTip" runat="server">
                                            </div>--%>
                                    <%-- <Maticsoft:ValidateTarget ID="validatetxtNavigateUrl" ContainerId="ValidatorContainer"
                                                runat="server" ControlToValidate="txtNavigateUrl" Description="请输入合法的链接地址，例如：http://www.maticsoft.com"  OkMessage="输入正确"
                                                Nullable="false">
                                                <Validators>
                                                    <Maticsoft:InputStringClientValidator ErrorMessage="请输入合法的链接地址，例如：http://www.maticsoft.com"
                                                        LowerBound="1" UpperBound="200" Regex="^(http)://.*" />
                                                </Validators>
                                            </Maticsoft:ValidateTarget>
                                        </td>
                                    </tr>--%>
                                    <tr class="advHtml">
                                        <td class="td_class" style="vertical-align: top;">
                                            广告HTML代码 ：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtAdvHtml" runat="server" Width="371px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                        </td>
                                        <td height="25">
                                            <asp:CheckBox ID="chkIsValid" Text="是否有效" runat="server" Checked="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="myTab1_Content1" class="none4">
                    <table style="width: 100%; border-bottom: none; border-top: none;" cellpadding="2"
                        cellspacing="1" class="border">
                        <tr>
                            <td class="tdbg">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="td_class">
                                            起始时间：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="371px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            结束时间：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtEndDate" runat="server" Width="371px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            显示频率：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtImpressions" runat="server" Width="70px" Text="10" onkeyup="value=value.replace(/[^\d]/g,'') "
                                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox>
                                            <%--<div style="margin-top: -23px; margin-left: 115px;" class="timeintervalClass">
                                                <asp:CheckBox ID="chkIsValid" Text="是否有效" runat="server" Checked="False" /></div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            最大浏览量：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtDayMaxPV" runat="server" Width="70px" Text="0" onkeyup="value=value.replace(/[^\d]/g,'') "
                                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox>
                                            <div style="margin-top: -25px; margin-left: 115px;" class="timeintervalClass">
                                                <span style="vertical-align: middle">最大IP访问：</span>
                                                <asp:TextBox ID="txtDayMaxIP" runat="server" Width="70px" Text="0" onkeyup="value=value.replace(/[^\d]/g,'') "
                                                    onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            每千次展示价格 ：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtCPMPrice" runat="server" Width="371px" Text="0.00"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            到期自动停止：
                                        </td>
                                        <td height="25">
                                            <asp:RadioButton ID="rbAutoStop" runat="server" GroupName="IsAutoStop" />是
                                            <asp:RadioButton ID="rbNoStup" runat="server" GroupName="IsAutoStop" />否
                                            <asp:RadioButton ID="rbNoLimit" runat="server" GroupName="IsAutoStop" Checked="true" />无限制
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                            广告主 ：
                                        </td>
                                        <td height="25">
                                            <asp:TextBox ID="txtEnterpriseID" runat="server" Width="371px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%; border-top: none;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                            class="adminsubmit_short" OnClientClick="return PageIsValid();" OnClick="btnSave_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                            class="adminsubmit_short"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <script type="text/javascript">
            var editor = new baidu.editor.ui.Editor({//实例化编辑器
                iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', '|', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                'justifyleft', 'justifycenter', 'justifyright', '|', 'removeformat', '|', 'pasteplain', '|', 'link', 'unlink', '|', 'insertimage']
                 ],
                initialContent: '', autoHeightEnabled: false,
                minFrameHeight: 70,
                pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: true, imagePath: "/Upload/RTF/", imageManagerPath: "/"
            });
            editor.render('ctl00_ContentPlaceHolder1_txtAdvHtml'); //将编译器渲染到容器
        </script>
        <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>