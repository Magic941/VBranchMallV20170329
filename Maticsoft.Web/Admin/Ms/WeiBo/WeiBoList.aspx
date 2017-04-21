<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="WeiBoList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Ms.WeiBo.WeiBoList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <link href="/Scripts/jquery.upload/fineuploader-3.4.1.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.upload/jquery.fineuploader-3.4.1.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            multiple = false;
            uploadbutton = $("#UploadImage").html();
            templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#f7f7f7;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style="margin-top: 0px; text-align: center; "></ul>' +
            '</div>';
            var uploader = new qq.FineUploader({
                element: $('#UploadImage')[0],
                request: {
                    endpoint: '/UploadWeiboImg.aspx'
                },
                text: {
                    uploadButton: uploadbutton,
                    waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
                },
                template: templatehtml,
                multiple: multiple,
                validation: {
                    allowedExtensions: ['jpeg', 'jpg', 'gif', 'png']
                },
                callbacks: {
                    onComplete: function (id, fileName, responseJSON) {
                        $(".qq-upload-list").hide();
                        $(".btn-success").css("overflow", "");
                        $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                        if (responseJSON.success) {
                            $("#yulantu").show();
                            $("#tbiaoqing").hide();
                            $("#yulantuimage").attr("src", responseJSON.data.format(''));
                            $("[id$='hfImage']").val(responseJSON.data.format(''));
                            $("#ctl00_ContentPlaceHolder1_btnSave").show();
                        }
                    }
                }
            });

            var Check = $("#ctl00_ContentPlaceHolder1_chkSetTime").attr("checked");
            if (Check == "checked") {
                $("#txtTime").show();
            }
            
            //微博数量
            var count = $("[id$='hfWeiboCount']").val();
            $("#txtWeiboCount").text("(" + count + ")");

            $("#txtSelectWeibo").click(function () {
                $("#chkWeiboList").slideToggle("normal");
            });

            $("#biaoqingshow").click(function (e) {
                e.preventDefault(); $("#tbiaoqing").slideToggle(0);
                $("#yulantu").hide();
            });

            $("#delyulatu").click(function () {
                $("[id$='hfImage']").val("");
                $("#yulantu").hide();
            });
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                maxDate: +23
            });

            $(".imageUrl").each(function () {
                var src = $(this).attr("src");
                if (src != "") {
                    $(this).show();
                }
            });
            $("#ctl00_ContentPlaceHolder1_chkSetTime").click(function () {
                $("#txtTime").slideToggle("normal");
            });
        });
        function textCounter(fieldId, countfieId, maxlimit) {
            var fieldEle = fieldId;
            var countfieldEle = $("#" + countfieId + "");
            if (fieldEle == null || countfieldEle == null) {
                return false;
            }
            if (fieldEle.value.gblen() > maxlimit) // too long... trim it
            //fieldEle.value = fieldEle.value.substring(0, maxlimit);
            {
                ShowFailTip('亲，您输入的太多了');
                fieldEle.value = fieldEle.value.gbtrim(maxlimit, '');
            }
            else {
                countfieldEle.text(maxlimit - fieldEle.value.gblen());
            }

        }
        String.prototype.gblen = function () {
            var len = 0;
            for (var i = 0; i < this.length; i++) {
                if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
                    len += 1;
                } else {
                    len++;
                }
            }
            return len;
        };
        String.prototype.gbtrim = function (len, s) {
            var str = '';
            var sp = s || '';
            var len2 = 0;
            for (var i = 0; i < this.length; i++) {
                if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
                    len2 += 1;
                } else {
                    len2++;
                }
            }
            if (len2 <= len) {
                return this;
            }
            len2 = 0;
            len = (len > sp.length) ? len - sp.length : len;
            for (var i = 0; i < this.length; i++) {
                if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
                    len2 += 1;
                } else {
                    len2++;
                }
                if (len2 > len) {
                    str += sp;
                    break;
                }
                str += this.charAt(i);
            }
            return str;
        };

        function insertsmilie(smilieface) {
            $("[id$='txtDesc']").val($("[id$='txtDesc']").val() + smilieface);
            $("#tbiaoqing").hide();
        }

        function Verify() {
            var content = $("[id$='txtDesc']").val();
            if (content == "") {
                ShowFailTip('亲，请输入微博内容！');
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .lyz_tab_right
        {
            display: inline;
            background: url(/Admin/Images/user_12.jpg) no-repeat left top;
            float: left;
            width: 605px;
            min-height: 190px;
            padding: 5px 20px 5px 15px;
        }
        .lyz_tab_right_w
        {
            width: 603px;
            text-align: right;
            color: #666;
            padding-bottom: 10px;
            padding-top: 5px;
        }
        .lyz_tab_right_w span
        {
            font-size: 25px;
            font-family: "萝莉体 第二版";
            color: #538714;
        }
        .lyz_tab_right_a
        {
            width: 603px;
            height: 88px;
            border: #d2d2d2 1px solid;
            background: #fff;
        }
        .lyz_tab_right_a textarea
        {
            width: 603px;
            height: 88px;
            border: 0px;
            font-size: 14px;
            color: #666;
            line-height: 24px;
            background: none;
        }
        .lyz_tab_right_b
        {
            width: 605px;
            height: auto !important;
            margin-top: 8px;
        }
        .lyz_tab_right_b .xiao
        {
            width: 100px;
            height: 16px;
            float: left;
            position: relative;
        }
        .xiao p
        {
            float: left;
            margin-right: 5px;
        }
        .xiao p a
        {
            color: #e35a69;
        }
        .lyz_tab_right_b .issue
        {
            width: 62px;
            float: right;
        }
        .lyz_tab_right .picture
        {
            width: 450px;
            height: auto !important;
            margin-top: 40px;
            margin-left: 30px;
        }
        .picture_an
        {
            width: 110px;
            height: 47px;
            float: left;
        }
        .picture_wen
        {
            width: 300px;
            float: right;
            color: #666;
            line-height: 47px;
            font-size: 14px;
        }
        .lyz_tab_right .goods
        {
            width: 490px;
            height: 24px;
            margin-top: 50px;
            margin-left: 20px;
        }
        .goods_a
        {
            width: 400px;
            height: 24px;
            border: #abadb3 1px solid;
            float: left;
            background: #fff;
        }
        .goods_a input
        {
            border: 0px;
            background: none;
            line-height: 22px;
            color: #666;
            width: 400px;
            height: 24px;
        }
        .goods_b
        {
            width: 80px;
            height: 24px;
            float: right;
        }
        .yulan
        {
            width: 100px;
            height: auto !important;
            position: absolute;
            z-index: 1;
            top: 15px;
            z-index: 999;
            padding-left: 32px;
        }
        .yulan_a
        {
            width: 100px;
            height: 9px;
            background: url(/Admin/Images/yulan.png);
        }
        .yulan_b
        {
            width: 98px;
            height: auto !important;
            border: #ffaec3 1px solid;
            border-top: 0;
            background: #fff;
        }
        .yulan_b h1
        {
            font-size: 12px;
            color: #666;
            font-family: "微软雅黑";
            padding-left: 10px;
            padding-top: 10px;
            font-weight: normal;
        }
        .yulan_b_s
        {
            width: 78px;
            padding: 10px;
        }
        .yulan_b_y
        {
            width: 98px;
            height: 25px;
            background: #f5f5f5;
        }
        .yulan_b_y1
        {
            color: #0a8cd2;
            vertical-align: middle;
            padding-left: 10px;
            line-height: 25px;
        }
        .yulan_b_y1 a
        {
            color: #0a8cd2;
        }
        .biaoqingshow a:hover
        {
            color: red;
        }
        
        .tbiaoqing
        {
            width: 500px;
            height: auto !important;
            margin-top: 25px;
            position: absolute;
            z-index: 998;
        }
        .biaoqing_a
        {
            width: 500px;
            height: 9px;
            background: url(/Admin/Images/biao1.png);
        }
        .biaoqing_b
        {
            width: 498px;
            height: auto !important;
            border: #ffaec3 1px solid;
            border-top: 0;
            background: #fff;
        }
        .biaoqing_b1
        {
            width: 498px;
            height: 32px;
            border-bottom: #ccc 1px solid;
            background: #f8f8f8;
        }
        .biaoqing_b1_t
        {
            color: #666;
            line-height: 32px;
            padding-left: 20px;
            float: left;
        }
        .biaoqing_b1_y
        {
            width: 15px;
            height: 15px;
            float: right;
            margin-right: 10px;
            margin-top: 10px;
        }
        .biaoqing_b2
        {
            width: 486px;
            height: auto !important;
            padding: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfImage" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfWeiboCount" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微博群发" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="微博账号，统一管理，快速群发微博，实现网络营销。" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <div class="hover" id="con_one_1" style="padding-left: 28px">
                        <div class="lyz_tab_right_w">
                            <a>还可以输入</a><span maxlength="4" id="shengyu" size="3" style="color: #FF69A2; font-size: 17px;"
                                value="140">140</span><a> 字</a></div>
                        <div class="lyz_tab_right_a">
                            <textarea onkeyup="textCounter(this,'shengyu',140)" onkeydown="textCounter(this,'shengyu',140)"
                                id="txtDesc" runat="server"></textarea></div>
                        <div class="lyz_tab_right_b">
                            <div class="xiao" style="width: 300px">
                                <p id="biaoqingshow">
                                    <a href="javascript:;">
                                        <img src="/Admin/Images/detailed_14.jpg" /></a><a href="javascript:;" style="padding-left: 5px; color: grey" >表情</a>
                                </p>
                                <div id="UploadImage" style="width: 50px; float: left;">
                                    <a href="javascript:;">
                                        <img src="/Admin/Images/postpic.jpg" /></a><a href="javascript:;" style="padding-left: 5px;
                                            color:  grey">图片</a>
                                </div>
                                        <p style="padding-top: 4px; padding-left: 5px;">
                                    <a href="javascript:;" id="txtSelectWeibo" style="color: grey">微博帐号 </a><span id="txtWeiboCount">(0)</span>
                                </p>
                                <p style="padding-top: 4px; padding-left: 5px;">
                                    <asp:CheckBox ID="chkSetTime" runat="server" /><span style=" color: grey">定时发送</span>
                                </p>
                        
                                <div class="yulan" id="yulantu" style="display: none">
                                    <div class="yulan_a">
                                    </div>
                                    <div class="yulan_b">
                                        <h1 id="imagename">
                                            预览图</h1>
                                        <div class="yulan_b_s">
                                            <img id="yulantuimage" style="width: 80px; height: 80px" src="" /></div>
                                        <div class="yulan_b_y">
                                            <p class="yulan_b_y1">
                                                <a href="javascript:void(0)" id="delyulatu">删除</a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tbiaoqing" id="tbiaoqing" style="display: none">
                                <div class="biaoqing_a">
                                </div>
                                <div class="biaoqing_b">
                                    <div class="biaoqing_b1">
                                        <p class="biaoqing_b1_t">
                                            常用表情</p>
                                        <p class="biaoqing_b1_y" id="biaoqingclose" style="cursor: pointer; float: right">
                                            <img src="/Areas/SNS/Themes/Default/Content/images/biao2.jpg"></p>
                                    </div>
                                    <div class="biaoqing_b2">
                                        <ul>
                                            <li>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td id="biaoqing">
                                                                <img src="/Images/face/微笑.gif" title="" border="0" onclick="insertsmilie('[微笑]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/撇嘴.gif" title="" border="0" onclick="insertsmilie('[撇嘴]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/色.gif" title="" border="0" onclick="insertsmilie('[色]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/发呆.gif" title="" border="0" onclick="insertsmilie('[发呆]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/得意.gif" title="" border="0" onclick="insertsmilie('[得意]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/流泪.gif" title="" border="0" onclick="insertsmilie('[流泪]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/害羞.gif" title="" border="0" onclick="insertsmilie('[害羞]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/闭嘴.gif" title="" border="0" onclick="insertsmilie('[闭嘴]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/睡.gif" title="" border="0" onclick="insertsmilie('[睡]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/大哭.gif" title="" border="0" onclick="insertsmilie('[大哭]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/尴尬.gif" title="" border="0" onclick="insertsmilie('[尴尬]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/调皮.gif" title="" border="0" onclick="insertsmilie('[调皮]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/呲牙.gif" title="" border="0" onclick="insertsmilie('[呲牙]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/惊讶.gif" title="" border="0" onclick="insertsmilie('[惊讶]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/难过.gif" title="" border="0" onclick="insertsmilie('[难过]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/酷.gif" title="" border="0" onclick="insertsmilie('[酷]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/冷汗.gif" title="" border="0" onclick="insertsmilie('[冷汗]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/抓狂.gif" title="" border="0" onclick="insertsmilie('[抓狂]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/吐.gif" title="" border="0" onclick="insertsmilie('[吐]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/偷笑.gif" title="" border="0" onclick="insertsmilie('[偷笑]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/可爱.gif" title="" border="0" onclick="insertsmilie('[可爱]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/白眼.gif" title="" border="0" onclick="insertsmilie('[白眼]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/傲慢.gif" title="" border="0" onclick="insertsmilie('[傲慢]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/饥饿.gif" title="" border="0" onclick="insertsmilie('[饥饿]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/困.gif" title="" border="0" onclick="insertsmilie('[困]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/惊恐.gif" title="" border="0" onclick="insertsmilie('[惊恐]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/流汗.gif" title="" border="0" onclick="insertsmilie('[流汗]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/憨笑.gif" title="" border="0" onclick="insertsmilie('[憨笑]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/大兵.gif" title="" border="0" onclick="insertsmilie('[大兵]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/奋斗.gif" title="" border="0" onclick="insertsmilie('[奋斗]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/咒骂.gif" title="" border="0" onclick="insertsmilie('[咒骂]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/疑问.gif" title="" border="0" onclick="insertsmilie('[疑问]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/嘘.gif" title="" border="0" onclick="insertsmilie('[嘘]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/晕.gif" title="" border="0" onclick="insertsmilie('[晕]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/折磨.gif" title="" border="0" onclick="insertsmilie('[折磨]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/衰.gif" title="" border="0" onclick="insertsmilie('[衰]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/骷髅.gif" title="" border="0" onclick="insertsmilie('[骷髅]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/敲打.gif" title="" border="0" onclick="insertsmilie('[敲打]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/再见.gif" title="" border="0" onclick="insertsmilie('[再见]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/擦汗.gif" title="" border="0" onclick="insertsmilie('[擦汗]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/抠鼻.gif" title="" border="0" onclick="insertsmilie('[抠鼻]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/鼓掌.gif" title="" border="0" onclick="insertsmilie('[鼓掌]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/糗大了.gif" title="" border="0" onclick="insertsmilie('[糗大了]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/坏笑.gif" title="" border="0" onclick="insertsmilie('[坏笑]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/左哼哼.gif" title="" border="0" onclick="insertsmilie('[左哼哼]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/右哼哼.gif" title="" border="0" onclick="insertsmilie('[右哼哼]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/哈欠.gif" title="" border="0" onclick="insertsmilie('[哈欠]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/鄙视.gif" title="" border="0" onclick="insertsmilie('[鄙视]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/委屈.gif" title="" border="0" onclick="insertsmilie('[委屈]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/快哭了.gif" title="" border="0" onclick="insertsmilie('[快哭了]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/阴险.gif" title="" border="0" onclick="insertsmilie('[阴险]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/亲亲.gif" title="" border="0" onclick="insertsmilie('[亲亲]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/吓.gif" title="" border="0" onclick="insertsmilie('[吓]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/可怜.gif" title="" border="0" onclick="insertsmilie('[可怜]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/菜刀.gif" title="" border="0" onclick="insertsmilie('[菜刀]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/西瓜.gif" title="" border="0" onclick="insertsmilie('[西瓜]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/啤酒.gif" title="" border="0" onclick="insertsmilie('[啤酒]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/篮球.gif" title="" border="0" onclick="insertsmilie('[篮球]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/乒乓.gif" title="" border="0" onclick="insertsmilie('[乒乓]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/咖啡.gif" title="" border="0" onclick="insertsmilie('[咖啡]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/饭.gif" title="" border="0" onclick="insertsmilie('[饭]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/猪头.gif" title="" border="0" onclick="insertsmilie('[猪头]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/玫瑰.gif" title="" border="0" onclick="insertsmilie('[玫瑰]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/凋谢.gif" title="" border="0" onclick="insertsmilie('[凋谢]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/示爱.gif" title="" border="0" onclick="insertsmilie('[示爱]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/爱心.gif" title="" border="0" onclick="insertsmilie('[爱心]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/心碎.gif" title="" border="0" onclick="insertsmilie('[心碎]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/蛋糕.gif" title="" border="0" onclick="insertsmilie('[蛋糕]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/闪电.gif" title="" border="0" onclick="insertsmilie('[闪电]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/炸弹.gif" title="" border="0" onclick="insertsmilie('[炸弹]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/刀.gif" title="" border="0" onclick="insertsmilie('[刀]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/足球.gif" title="" border="0" onclick="insertsmilie('[足球]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/瓢虫.gif" title="" border="0" onclick="insertsmilie('[瓢虫]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/便便.gif" title="" border="0" onclick="insertsmilie('[便便]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/月亮.gif" title="" border="0" onclick="insertsmilie('[月亮]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/太阳.gif" title="" border="0" onclick="insertsmilie('[太阳]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/礼物.gif" title="" border="0" onclick="insertsmilie('[礼物]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/拥抱.gif" title="" border="0" onclick="insertsmilie('[拥抱]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/强.gif" title="" border="0" onclick="insertsmilie('[强]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/弱.gif" title="" border="0" onclick="insertsmilie('[弱]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/握手.gif" title="" border="0" onclick="insertsmilie('[握手]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/胜利.gif" title="" border="0" onclick="insertsmilie('[胜利]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/抱拳.gif" title="" border="0" onclick="insertsmilie('[抱拳]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/勾引.gif" title="" border="0" onclick="insertsmilie('[勾引]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/拳头.gif" title="" border="0" onclick="insertsmilie('[拳头]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/差劲.gif" title="" border="0" onclick="insertsmilie('[差劲]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/爱你.gif" title="" border="0" onclick="insertsmilie('[爱你]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/NO.gif" title="" border="0" onclick="insertsmilie('[NO]');"
                                                                    style="cursor: pointer;">
                                                                <img src="/Images/face/OK.gif" title="" border="0" onclick="insertsmilie('[OK]');"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="issue" id="postWeibo"  style="width: 80px">
                                <asp:Button ID="btnSave" runat="server" Text="发布" OnClick="btnSave_Click" class="adminsubmit_short" OnClientClick="return Verify();">
                                </asp:Button>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="chkWeiboList" style="display: none;padding-top: 8px">
                            <asp:CheckBoxList ID="ChkWeibo" runat="server" RepeatDirection="Horizontal" align="left">
                            </asp:CheckBoxList>
                        </div>
                             <div class="clear">
                            </div>
                        <div id="txtTime" style="display: none;padding-bottom: 12px;padding-top: 8px">
                            发送时间：
                            <asp:TextBox ID="txtDate" runat="server"  Width="88" ></asp:TextBox>
                            <asp:DropDownList ID="ddlHour" runat="server">
                            </asp:DropDownList>
                            时
                            <asp:DropDownList ID="ddlMins" runat="server">
                            </asp:DropDownList>
                            分
                            <span style="color: grey"> Tip ：定时只能选择24天之内</span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                  <%--  <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：--%>
                    &nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="False" DataKeyNames="WeiBoId" Style="float: left;" ShowGridLine="true"
            ShowHeaderStyle="true">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="微博消息" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Maticsoft.Common.StringPlus.SubString(Eval("WeiboMsg"), 100, "...")%>
                    &nbsp;&nbsp;
                            <img src="<%#Eval("ImageUrl")%>" alt="" width="80" height="80" class="imageUrl" style="display: none" />
                    </ItemTemplate>
                </asp:TemplateField>
          
                <asp:TemplateField ControlStyle-Width="50" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("CreateDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField ControlStyle-Width="50" HeaderText="发布时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("PublishDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderText="删除" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Publish"
                            CommandArgument='<%# Eval("WeiBoId")%>' Text="重新发布"> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;
            display: none">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:CheckBoxList ID="ChkWeibo2" runat="server" RepeatDirection="Horizontal" align="left">
                    </asp:CheckBoxList>
                    <asp:Button ID="btnSendWeibo" runat="server" Text="重新发布" class="adminsubmit" OnClick="btnSendWeibo_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
