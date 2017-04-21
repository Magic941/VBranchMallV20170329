
<%@ Page Title="发送客服消息" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
  CodeBehind="AddMsg.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Push.AddMsg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
     <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
        <link href="/Scripts/jqueryui/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            var backgtoundImgBase = "url('{0}') no-repeat center center";
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtPublishDate").prop("readonly", true).datetimepicker({
                timeText: '时间',
                hourText: '小时',
                minuteText: '分钟',
                secondText: '秒',
                currentText: '现在',
                closeText: '完成',
                showSecond: true, //显示秒  
                dateFormat: "yy-mm-dd",
                timeFormat: ' HH:mm:ss'//格式化时间  
            });
            $("#txtNewsDate").prop("readonly", true).datetimepicker({
                timeText: '时间',
                hourText: '小时',
                minuteText: '分钟',
                secondText: '秒',
                currentText: '现在',
                closeText: '完成',
                showSecond: true, //显示秒  
                dateFormat: "yy-mm-dd",
                timeFormat: ' HH:mm:ss'//格式化时间  
            });

            $("#txtVoiceDate").prop("readonly", true).datetimepicker({
                timeText: '时间',
                hourText: '小时',
                minuteText: '分钟',
                secondText: '秒',
                currentText: '现在',
                closeText: '完成',
                showSecond: true, //显示秒  
                dateFormat: "yy-mm-dd",
                timeFormat: 'HH:mm:ss'//格式化时间  
            });



            $("[id$='ddCategory']").hide();
            //删除消息
            $(".delMsg").die("click").live("click", function () {
                var itemId = $(this).attr("itemId");
                var self = $(this);
                $.ajax({
                    url: ("AddMsg.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "DeleteMsg", Callback: "true", ItemId: itemId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.parent().parent().parent().hide();
                            ShowSuccessTip("删除消息成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });


            $("#ddlUrl").change(function () {
                var value = $(this).val();
                $("#txtUrl").hide();
                $("[id$='ddCategory']").hide();
                if (value == 0) {
                    $("#txtUrl").show();
                }
                if (value == 5) {
                    $("[id$='ddCategory']").show();
                }
            })


            //添加消息
            $("#btnSave").die("click").live("click", function () {
                var title = $("#txtTitle").val();
                if (title == "") {
                    ShowFailTip("请填写标题");
                    return;
                }
                var path = $("#txtPath").val();
                if (path == "") {
                    ShowFailTip("请上传图片");
                    return;
                }
                var urltype = $("#ddlUrl").val();
                var category = $("[id$='ddCategory']").val();
                var desc = $("#txtDesc").val();
                var txturl = $("#txtUrl").val();
                $.ajax({
                    url: ("AddMsg.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "AddItemMsg", Callback: "true", Title: title, ImageUrl: path, Desc: desc, UrlType: urltype, Url: txturl, Category: category },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#msgBox").find("ul").append("<li><div class='content'> <div><img src='" + resultData.picurl.format('T_') + "' alt=''   /></div><div class='msgInfo'><span class='msgtitle'>" + title + "</span><br/> " + desc +
                                    "  <a class='delMsg' href='javascript:;' itemId='" + resultData.Data + "'>删除</a></div></div> </li>");
                            $(".file_uploadUploader").css("background", backgtoundImgBase.format("/admin/images/AddImage.gif"))
                            $("#txtPath").val("");
                            $("#txtTitle").val("");
                            $("#ddlUrl").val("");
                            $("#txtUrl").val("");
                            $("#txtDesc").val("");
                            ShowSuccessTip("添加消息成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        // 通常 textStatus 和 errorThrown 之中
                        // 只有一个会包含信息
                        alert(XMLHttpRequest);
                        alert(errorThrown)
                        this; // 调用本次AJAX请求时传递的options参数
                    }
                });
            });

            $("#btnPublish").click(function () {
                var itemArr = new Array();
                var itemIds = "";
                $(".delMsg").each(function () {
                    var itemid = $(this).attr("itemId");
                    if (itemid) {
                        itemArr.push(itemid)
                    }
                })
                if (itemArr.length == 0) {
                    ShowFailTip("请添加消息");
                    return;
                }
                var date = $("#txtNewsDate").val();
                if (date == "") {
                    ShowFailTip("请选择发布时间");
                    return;
                }
                itemIds = itemArr.join(",");
                $.ajax({
                    url: ("AddMsg.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "Publish", Callback: "true", ItemIds: itemIds, Date: date },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            ShowSuccessTip("发送消息成功");
                            $("#msgBox").find("ul").empty();
                            window.location.href = "MsgList.aspx";
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });


            $("#btnPublishVoice").click(function () {
                var voicePath = $("#txtVoicePath").val();
                var date = $("#txtVoiceDate").val();
                if (voicePath == "") {
                    ShowFailTip("请上传音频文件");
                    return;
                }
                if (date == "") {
                    ShowFailTip("请选择发布时间");
                    return;
                }
                $.ajax({
                    url: ("AddMsg.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "PublishVoice", Callback: "true", Path: voicePath, Date: date },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#txtFileName").text("")
                            $("#txtVoicePath").val("");
                            ShowSuccessTip("发送消息成功");
                            window.location.href = "MsgList.aspx";
                        }
                        if (resultData.STATUS == "NoToken") {
                            ShowFailTip("获取微信授权失败！请检查您的微信API设置和对应的权限");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });

            $("#uploadify").uploadify({
                'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
                'script': '/WeChatImg.aspx',
                'cancelImg': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',   //指定取消上传的图片，默认‘cancel.png’ 
                'wmode': 'transparent',
                'hideButton': true,
                'width': 128,
                'height': 127,
                'auto': true,               //选定文件后是否自动上传，默认false 
                'multi': false,               //是否允许同时上传多文件，默认false 
                'fileDesc': '图片文件', //出现在上传对话框中的文件类型描述 
                'fileExt': '*.jpg;*.bmp;*.png;*.gif',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc 
                'sizeLimit': 2097152,          //控制上传文件的大小，单位byte 
                'onComplete': function (event, queueID, fileObj, response) {
                    var responseJSON = $.parseJSON(response);
                    if (responseJSON.success) {
                        $(event.target).parent().css("background", backgtoundImgBase.format(responseJSON.data.format('T_')));
                        $("#txtPath").val(responseJSON.data);
                    }
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
                }
            });
            $("#UploadVoice").uploadify({
                'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
                'script': '/WeChatFile.aspx',
                'cancelImg': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',   //指定取消上传的图片，默认‘cancel.png’ 
                'wmode': 'transparent',
                'auto': true,               //选定文件后是否自动上传，默认false 
                'multi': false,               //是否允许同时上传多文件，默认false 
                'fileDesc': '音频文件', //出现在上传对话框中的文件类型描述 
                'fileExt': '*.mp3;*.wma;*.wav;*.amr',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc 
                'sizeLimit': 2097152,          //控制上传文件的大小，单位byte 
                'onComplete': function (event, queueID, fileObj, response) {
                    var responseJSON = $.parseJSON(response);
                    if (responseJSON.success) {
                        $("#txtFileName").text(responseJSON.name)
                        $("#txtVoicePath").val(responseJSON.data);
                    }
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
                }
            });


        });
      
    </script>
    <style type="text/css">
        .msgBox
        {
            background: #fff;
            border-radius: 5px;
            padding-top: 10px;
        }
        .msgBox .delMsg
        {
            float: right;
            color: #889db6;
        }
        .msgtitle
        {
            font-size: 16px;
            font-family: "Microsoft YaHei" !important;
        }
        .msgBox img
        {
            width: 60px;
            height: 60px;
            float: left;
            border-radius: 3px;
        }
        .msgBox .list ul
        {
            overflow: hidden;
            zoom: 1;
            width: 520px;
            border: 1px solid #ccc;
        }
        .msgBox .list ul li
        {
            float: left;
            clear: both;
            width: 100%;
            border-bottom: 1px dashed #d8d8d8;
            padding: 2px 0;
            background: #fff;
            overflow: hidden;
        }
        .msgBox .list ul li.hover
        {
            background: #f5f5f5;
        }
        .msgBox .list .content
        {
            float: left;
            width: 500px;
            font-size: 14px;
            font-family: arial;
            word-wrap: break-word;
        }
        .msgBox .list .msgInfo
        {
            display: inline;
            word-wrap: break-word;
            float: left;
            width: 420px;
            padding-left: 8px;
        }
        .msgBox .list .times
        {
            color: #889db6;
            font: 12px/18px arial;
            margin-top: 5px;
            overflow: hidden;
            zoom: 1;
        }
        .msgBox .list .times span
        {
            float: left;
        }
        .msgBox .list .times a
        {
            float: right;
            color: #889db6;
        }
        .content
        {
            background: #fff;
        }
        #txtPostMsg
        {
            width: 668px;
            resize: none;
            height: 65px;
            overflow: auto;
        }
        #userName, #conBox
        {
            color: #777;
            border: 1px solid #d0d0d0;
            border-radius: 6px;
            padding: 3px 5px;
            font: 14px/1.5 arial;
        }
        .msgBox .list .userPic
        {
            float: left;
            height: 50px;
            display: inline;
            margin-left: 10px;
            border-radius: 3px;
            padding-top: 4px;
        }
        #userName.active, #conBox.active
        {
            border: 1px solid #7abb2c;
        }
        #userName
        {
            height: 20px;
        }
        #btnAddMsg
        {
            border: 0;
            height: 30px;
            cursor: pointer;
            margin-left: 10px;
        }
        #btnAddMsg.hover
        {
            background-position: 0 -30px;
        }
        .tr
        {
            overflow: hidden;
            zoom: 1;
            width: 668px;
        }
        .tr p
        {
            float: right;
            line-height: 30px;
        }
        .tr *
        {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信客服消息推送" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="微信客服消息推送" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);" id="tab0"><a href="javascript:;">文本消息</a></li>
                    <li class="normal" onclick="nTabs(this,1);" id="tab1"><a href="javascript:;">图文消息</a></li>
                    <li class="normal" onclick="nTabs(this,2);" id="tab2"><a href="javascript:;">语音消息</a></li>
                </ul>
            </div>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td class="td_class" style =" padding-left:0">
                    <asp:Literal ID="lblGroup" runat="server" Text="  小组：" />
                </td>
                <td height="25" class="newstitle">
                    <asp:DropDownList ID="ddlGroup" runat="server">
                    </asp:DropDownList>
                    <asp:Literal ID="lblTipTitle" runat="server" Text="" />
                </td>
            </tr>
        </table>
        <div class="TabContent">
            <%-- 自动关注消息 --%>
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr class="txt_s">
                                    <td class="td_class">
                                    内容：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtContent" runat="server" Width="540" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    发送时间：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtPublishDate" runat="server" Width="350" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:Button ID="btnSaveText" runat="server" Text="保存" class="adminsubmit" OnClick="btnSaveText_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 图文消息 --%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr class="news_r">
                                    <td class="td_class">
                                        标题：
                                    </td>
                                    <td height="25" style="width: 350px">
                                        <input id="txtTitle" type="text" style="width: 350px" />
                                    </td>
                                    <td height="128" style="padding-left: 5px;" rowspan="3">
                                        <ul class="product_upload_img_ul" style="display: block">
                                            <li>
                                                <div class="ImgUpload ">
                                                    <input id="txtPath" type="hidden" />
                                                    <span id="Span1" class="cancel" style="display: none; z-index: 999999; height: 128px">
                                                        <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                            style="width: 127px; height: 128px; overflow: hidden; background-image: url(/admin/images/AddImage.gif)">
                                                            <input type="file" id="uploadify" />
                                                        </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr class="news_r">
                                    <td class="td_class">
                                        描述：
                                    </td>
                                    <td height="25" colspan="3">
                                        <textarea id="txtDesc" cols="20" rows="2" style="width: 346px"></textarea>
                                    </td>
                                </tr>
                                <tr class="news_r">
                                    <td class="td_class">
                                        链接地址：
                                    </td>
                                    <td height="25">
                                        <select id="ddlUrl">
                                            <option value="0">自定义网址</option>
                                            <option value="1">微信商城</option>
                                            <option value="2">微官网</option>
                                            <option value="3">我的账户</option>
                                            <option value="4">我的订单</option>
                                            <option value="5">商品分类</option>
                                        </select>
                                        <input id="txtUrl" type="text" style="width: 245px" />
                                        <asp:DropDownList ID="ddCategory" runat="server" Width="245px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="news_r">
                                    <td class="td_class">
                                    </td>
                                    <td height="25" colspan="3">
                                        <input id="btnSave" type="button" value="添加" class="adminsubmit_short" />
                                    </td>
                                </tr>
                                <tr class="news_r">
                                    <td class="td_class">
                                    </td>
                                    <td id="msgBox" class="msgBox" colspan="3">
                                        <div class="list">
                                            <ul>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    发送时间：
                                    </td>
                                    <td height="25">
                                       <input id="txtNewsDate" type="text" style="width: 350px" />
                                    </td>
                                </tr>
                                <tr class="txt_r">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <input id="btnPublish" type="button" value="全部保存" class="adminsubmit" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 自动关注消息 --%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr class="txt_s">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <input type="file" class="file_upload adminsubmit" id="UploadVoice" value="上传音频" />
                                        <span id="txtFileName"></span>
                                        <input id="txtVoicePath" type="hidden" />
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    发送时间：
                                    </td>
                                    <td height="25">
                                       <input id="txtVoiceDate" type="text" style="width: 350px" />
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <input id="btnPublishVoice" type="button" value="保存" class="adminsubmit" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

