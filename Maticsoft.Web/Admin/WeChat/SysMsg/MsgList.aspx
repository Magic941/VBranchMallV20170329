<%@ Page Title="系统默认消息管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="MsgList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.SysMsg.MsgList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"  type="text/css" /> 
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [
            ['source', 'link', 'unlink']],
            initialContent: '',
            minFrameHeight: 220,
            initialFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
            , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('<%=txtSubscription.ClientID %>'); //将编译器渲染到容器

        var editor2 = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [
            ['source', 'link', 'unlink']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            initialFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor2.render('<%=txtReplyMsg.ClientID %>'); //将编译器渲染到容器
        var backgtoundImgBase = "url('{0}') no-repeat center center";
        $(function () {
            $("#baidu_editor_1").css("height", "220px");

            //初次加载数据
            var type_s = $("[name$='msgType_S'][checked='checked']").val();
            $("[id$='ddCategory_S']").hide();
            if (type_s == "news") {
                $("#timage_S").show();
                $(".news_s").show();
                $(".txt_s").hide();
                $("[id$='btnSave']").hide();

            }
            var type_r = $("[name$='msgType_R'][checked='checked']").val();
            $("[id$='ddCategory_R']").hide();
            if (type_r == "news") {
                $("#timage_R").show();
                $(".news_r").show();
                $(".txt_r").hide();
                $("[id$='btnSave']").hide();
            }

            //加载关注消息
            $.ajax({
                url: ("MsgList.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "GetMsgList", Callback: "true", SysType: 1 },
                success: function (resultData) {
                    if (resultData.Data.length > 0) {
                        var arry_obj = $.parseJSON(resultData.Data)
                        $.each(arry_obj, function (i, item) {
                            $("#msgBox_S").find("ul").append("<li><div class='content'> <div><img src='" + item.picurl.format('T_') + "' alt=''   /></div><div class='msgInfo'><span class='msgtitle'>" + item.title + "</span><br/> " + item.desc +
                                    "  <a class='delMsg' href='javascript:;' msgId='" + item.msgId + "'><img style='width:16px;height:16px' src='/Admin/Images/del.png' alt='删除'  /></a></div></div> </li>");
                        });
                    }
                    if (resultData.STATUS == "FAILED") {
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                    }
                }
            });
            //加载回复消息
            $.ajax({
                url: ("MsgList.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "GetMsgList", Callback: "true", SysType: 2 },
                success: function (resultData) {
                    if (resultData.Data.length > 0) {
                        var arry_obj = $.parseJSON(resultData.Data)
                        $.each(arry_obj, function (i, item) {
                            $("#msgBox_R").find("ul").append("<li><div class='content'><div><img src='" + item.picurl.format('T_') + "' alt=''   /></div> <div class='msgInfo'><span class='msgtitle'>" + item.title + "</span><br/> " + item.desc +
                                    "  <a class='delMsg' href='javascript:;' msgId='" + item.msgId + "'><img style='width:16px;height:16px' src='/Admin/Images/del.png' alt='删除'  /></a></div></div> </li>");
                        });
                    }
                    if (resultData.STATUS == "FAILED") {
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                    }
                }
            });

            //删除消息
            $(".delMsg").die("click").live("click", function () {
                var msgId = $(this).attr("msgId");
                var self = $(this);
                $.ajax({
                    url: ("MsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "DeleteMsg", Callback: "true", MsgId: msgId },
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


            $("#ddlUrl_S").change(function () {
                var value = $(this).val();
                $("#txtUrl_S").hide();
                $("[id$='ddCategory_S']").hide();
                if (value == 0) {
                    $("#txtUrl_S").show();
                }
                if (value == 5) {
                    $("[id$='ddCategory_S']").show();
                }
            })

            $("#ddlUrl_R").change(function () {
                var value = $(this).val();
                $("#txtUrl_R").hide();
                $("[id$='ddCategory_R']").hide();
                if (value == 0) {
                    $("#txtUrl_R").show();
                }
                if (value == 5) {
                    $("[id$='ddCategory_R']").show();
                }
            })

            //添加 关注消息
            $("#btnSave_S").die("click").live("click", function () {
                var title = $("#txtTitle_S").val();
                if (title == "") {
                    ShowFailTip("请填写标题");
                    return;
                }
                var path = $("#path_S").val();
                if (path == "") {
                    ShowFailTip("请上传图片");
                    return;
                }
                var urltype = $("#ddlUrl_S").val();
                var category = $("[id$='ddCategory_S']").val();
                var desc = $("#txtDesc_S").val();
                var imageUrl = $("#timage_S").attr("src");
                var txturl = $("#txtUrl_S").val();
                $.ajax({
                    url: ("MsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "AddMsg", Callback: "true", Title: title, ImageUrl: path, Desc: desc, UrlType: urltype, Url: txturl, Category: category, SysType: 1 },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#msgBox_S").find("ul").append("<li><div class='content'><div><img src='" + resultData.picurl.format('T_') + "' alt=''   /></div> <div class='msgInfo'><span class='msgtitle'>" + title + "</span><br/> " + desc +
                                    "  <a class='delMsg' href='javascript:;' msgId='" + resultData.Data + "'><img style='width:16px;height:16px' src='/Admin/Images/del.png' alt='删除'  /></a></div></div> </li>");
                            $("#msgBox_S").find(".file_uploadUploader").css("background", backgtoundImgBase.format("/admin/images/AddImage.gif"))
                            $("#path_S").val("");
                            $("#txtTitle_S").val("");
                            $("#ddlUrl_S").val("");
                            $("#txtUrl_S").val("");
                            $("#txtDesc_S").val("");
                            ShowSuccessTip("添加消息成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //添加回复消息
            $("#btnSave_R").die("click").live("click", function () {
                var title = $("#txtTitle_R").val();
                if (title == "") {
                    ShowFailTip("请填写标题");
                    return;
                }
                var path = $("#path_R").val();
                if (path == "") {
                    ShowFailTip("请上传图片");
                    return;
                }
                var urltype = $("#ddlUrl_R").val();
                var category = $("[id$='ddCategory_R']").val();
                var desc = $("#txtDesc_R").val();
                var imageUrl = $("#timage_R").attr("src");
                var txturl = $("#txtUrl_R").val();
                $.ajax({
                    url: ("MsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "AddMsg", Callback: "true", Title: title, ImageUrl: path, Desc: desc, UrlType: urltype, Url: txturl, Category: category, SysType: 2 },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#msgBox_R").find("ul").append("<li><div class='content'> <div><img src='" + resultData.picurl.format('T_') + "' alt=''  /></div><div class='msgInfo'><span class='msgtitle'>" + title + "</span><br/> " + desc +
                                    "  <a class='delMsg' href='javascript:;' msgId='" + resultData.Data + "'><img style='width:16px;height:16px' src='/Admin/Images/del.png' alt='删除'  /></a></div></div> </li>");
                            $("#msgBox_R").find(".file_uploadUploader").css("background", backgtoundImgBase.format("/admin/images/AddImage.gif"))
                            $("#path_R").val("");
                            $("#txtTitle_R").val("");
                            $("#ddlUrl_R").val("");
                            $("#txtUrl_R").val("");
                            $("#txtDesc_R").val("");
                            ShowSuccessTip("添加消息成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });


            $("[name$='msgType_S']").change(function () {
                var value = $(this).val();
                $(".news_s").hide();
                $("[id$='btnSave']").show();
                $(".txt_s").show();
                $("[id$='ddCategory_S']").hide();
                if (value == "news") {
                    $(".news_s").show();
                    $(".txt_s").hide();
                    $("[id$='btnSave']").hide();

                }
            });

            $("[name$='msgType_R']").change(function () {
                var value = $(this).val();
                $(".news_r").hide();
                $("[id$='btnSave']").show();
                $(".txt_r").show();
                if (value == "news") {
                    $(".news_r").show();
                    $(".txt_r").hide();
                    $("[id$='btnSave']").hide();
                }
            });

            $("#uploadify_s").uploadify({
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
                        //url: /upload/{0}xxx.jpg
                        $("#path_S").val(responseJSON.data);
                    }
                },
                //                    'onError': function(event, queueID, fileObj) {
                //                        alert("文件:" + fileObj.name + " 上传失败");
                //                    }
                'onError': function (event, ID, fileObj, errorObj) {
                    //            alert('上传图片大小不能超过2M，尺寸不能大于1280×1280');
                    alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
                }
            });

            $("#uploadify_R").uploadify({
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
                        //url: /upload/{0}xxx.jpg
                        $("#path_R").val(responseJSON.data);
                    }
                },
                //                    'onError': function(event, queueID, fileObj) {
                //                        alert("文件:" + fileObj.name + " 上传失败");
                //                    }
                'onError': function (event, ID, fileObj, errorObj) {
                    //            alert('上传图片大小不能超过2M，尺寸不能大于1280×1280');
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
            float:right;
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
            font-size: 12px;
            font-family: arial;
            word-wrap: break-word;
        }
        .msgBox .list .msgInfo
        {
            display: inline;
            word-wrap: break-word;
            float: left;
            width: 420px;
            padding-left:8px
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
                        <asp:Literal ID="Literal1" runat="server" Text="微信系统默认消息管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="设置微信默认回复信息的内容。" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);" id="tab0"><a href="javascript:;">关注时回复</a></li>
                    <li class="normal" onclick="nTabs(this,1);" id="tab1"><a href="javascript:;">默认回复消息</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <%-- 自动关注消息 --%>
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        消息类型：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="msgType_S" runat="server" RepeatDirection="Horizontal" align="left">
                                            <asp:ListItem Value="text" Text="文本消息" ></asp:ListItem>
                                            <asp:ListItem Value="news" Text="图文消息" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                
                                <tr class="news_s" style="display: none">
                                    <td class="td_class">
                                        标题：
                                    </td>
                                    <td height="25" style="width: 320px">
                                        <input id="txtTitle_S" type="text" style="width: 350px" />
                                    </td>
                           <td height="25" style="padding-left: 5px; "  rowspan="3">
                                        <ul class="product_upload_img_ul" style="display: block">
                                            <li>
                                                <div class="ImgUpload ">
                                                    <input id="path_S" type="hidden" />
                                                    <span id="Span2" class="cancel" style="display: none; z-index: 999999;height:128px"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> 
                                                        <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden; background-image:url(/admin/images/AddImage.gif)">
                                                            <input type="file" class="file_upload" id="uploadify_s"   />
                                                        </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                
                                <tr class="news_s" style="display: none">
                                    <td class="td_class">
                                        描述：
                                    </td>
                                    <td height="25" colspan="3">
                                        <textarea id="txtDesc_S" cols="20" rows="2" style="width: 346px"></textarea>
                                    </td>
                                </tr>
                                <tr class="news_s" style="display: none">
                                    <td class="td_class">
                                        链接地址：
                                    </td>
                                    <td height="25">
                                        <select id="ddlUrl_S">
                                            <option value="0">自定义网址</option>
                                            <option value="1">微信商城</option>
                                            <option value="2">微官网</option>
                                            <option value="3">我的账户</option>
                                            <option value="4">我的订单</option>
                                            <option value="5">商品分类</option>
                                            <option value="6">我的会员卡</option>
                                            <option value="7">会员签到</option>
                                            
                                        </select>
                                        <input id="txtUrl_S" type="text" style="width: 245px" />
                                        <asp:DropDownList ID="ddCategory_S" runat="server" Width="245px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="news_s" style="display: none">
                                    <td class="td_class">
                                    </td>
                                    <td height="25" colspan="3">
                                        <input id="btnSave_S" type="button" value="添加" class="adminsubmit" />
                                    </td>
                                </tr>
                                <tr class="news_s" style="display: none">
                                    <td class="td_class">
                                    </td>
                                    <td id="msgBox_S" class="msgBox" colspan="3">
                                        <div class="list">
                                            <ul>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSubscription" runat="server" Width="80%" TextMode="MultiLine"
                                            Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="txt_s">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:Button ID="Button1" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click">
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
            <%-- 自动回复消息 --%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        消息类型：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="msgType_R" runat="server" RepeatDirection="Horizontal" align="left">
                                            <asp:ListItem Value="text" Text="文本消息"  ></asp:ListItem>
                                            <asp:ListItem Value="news" Text="图文消息" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                              
                                <tr class="news_r" style="display: none">
                                    <td class="td_class">
                                        标题：
                                    </td>
                                    <td height="25" style="width:350px">
                                        <input id="txtTitle_R" type="text" style="width: 320px" />
                                    </td>
                                    <td height="25" style="padding-left: 5px; "  rowspan="3">
                                        <ul class="product_upload_img_ul" style="display: block">
                                            <li>
                                                <div class="ImgUpload ">
                                                    <input id="path_R" type="hidden" />
                                                    <span id="Span1" class="cancel" style="display: none; z-index: 999999;height:128px"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> 
                                                        <span class="file_uploadUploader" style="width: 127px;  height: 128px; overflow: hidden; background-image:url(/admin/images/AddImage.gif)">
                                                            <input type="file" class="file_upload" id="uploadify_R"     />
                                                        </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                
                                <tr class="news_r" style="display: none">
                                    <td class="td_class">
                                        描述：
                                    </td>
                                    <td height="25" colspan="3">
                                        <textarea id="txtDesc_R" cols="20" rows="2" style="width: 346px"></textarea>
                                    </td>
                                </tr>
                                <tr class="news_r" style="display: none">
                                    <td class="td_class">
                                        链接地址：
                                    </td>
                                    <td height="25">
                                        <select id="ddlUrl_R">
                                              <option value="0">自定义网址</option>
                                            <option value="1">微信商城</option>
                                            <option value="2">微官网</option>
                                            <option value="3">我的账户</option>
                                            <option value="4">我的订单</option>
                                            <option value="5">商品分类</option>
                                            <option value="6">我的会员卡</option>
                                            <option value="7">会员签到</option>
                                        </select>
                                        <input id="txtUrl_R" type="text" style="width: 245px" />
                                        <asp:DropDownList ID="ddCategory_R" runat="server" Width="245px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="news_r" style="display: none">
                                    <td class="td_class">
                                    </td>
                                    <td height="25" colspan="3">
                                        <input id="btnSave_R" type="button" value="添加" class="adminsubmit" />
                                    </td>
                                </tr>
                                <tr class="news_r" style="display: none">
                                    <td class="td_class">
                                    </td>
                                    <td id="msgBox_R" class="msgBox" colspan="3">
                                        <div class="list">
                                            <ul>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="txt_r">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtReplyMsg" runat="server" Width="80%" TextMode="MultiLine" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="txt_r">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:Button ID="btnSave1" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <table style="width: 100%; border-top: none; float: left; padding-top: 20px; padding-bottom: 20px"
            cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
