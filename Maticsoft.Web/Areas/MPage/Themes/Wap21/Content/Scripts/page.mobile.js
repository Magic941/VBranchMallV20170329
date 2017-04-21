/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  $time$  $username$    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

//检查url 此方法只在页面加载完成之后执行一次
var checkurl = function () {
    var t1 = window.location.href;
    var widurl = t1.toString();
    if (widurl.indexOf("#") > 0 && widurl.indexOf("_") > 0 && widurl.indexOf("detail") > 0) {  //m#detail_22  接收从微信跳转过来的url
        var parameter = widurl.split("#");
        var contid = parameter[1].split("_");
        var controlname = "Article";
        var actionname = "NewsDetail";
        var viewname = "ProductsDetailPhoto";
        var pageId = "productsDetailPhoto_" + contid[1];
        var contendid = contid[1];
        $('#content').append("<div id='" + pageId + "' class='page'  data-role='page' ></div>");
        $('#' + pageId).load($Maticsoft.BasePath + controlname + '/' + actionname + '?viewName=' + viewname + '&ContID=' + contendid, function () {
        });
    } else {
        $('#content').append("<div id='index' class='page' title='首页' data-role='page' ></div>");
        $('#index').load($Maticsoft.BasePath + 'Home/_Index?viewName=_Index', function () {
        });
    }
};

//显示隐藏主菜单
var mainmenu = function (showOrHide) {
    var $jQUI = $("#jQUi"), $el = $("#menu"), $nav = $("#navbar");
    var show = function () {
        $el.show().removeClass("off").addClass("on").bind("click", hide);
        $el.find(">*").first().show("fast", function showNext() {
            $(this).next("*").show("fast", showNext);
        });
        $jQUI.css({ overflow: "hidden", position: "absolute", width: "100%", height: "100%" });
    };
    var hide = function () {
        $el.removeClass("on").addClass("off").unbind("click");
        window.setTimeout(function () { $el.hide(); }, 320);
        $el.find("a").hide(1000);
        $jQUI.removeAttr('style');
    };
    if ($el.css("display") == "none" && (showOrHide == true | showOrHide == null)) {
        show();
    } else {
        hide();
    };
};

//点击页面加载  controller名字 pageid   div 的id   标题    栏目编号  文章id  前几条 友情链接id  fid(表单id)
var pageload = function (controlname, pageId, title, actionname, viewname, classid, contendid, top, alinkid, vid, fid, topc) {
    var thisLoad = $("#" + pageId); 
    if (thisLoad.length > 0) {
        $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
    } else {
        $.mobile.showPageLoadingMsg(); //显示加载器
        var strurl = $Maticsoft.BasePath + controlname + '/' + actionname + '?viewName=' + viewname;
        if (parseInt(classid) > 0) {
            strurl += "&classID=" + classid;
        }
        if (parseInt(contendid) > 0) {
            strurl += '&ContID=' + contendid;
        }
        if (parseInt(top) > 0) {
            strurl += '&top=' + top;
        }
        if (parseInt(alinkid) > 0) {
            strurl += '&aid=' + alinkid;
        }
        if (parseInt(vid) > 0) {
            strurl += '&vid=' + vid;
        }
        if (parseInt(fid) > 0) {
            strurl += '&fid=' + fid;
        }
        if (parseInt(topc) > 0) {
            strurl += '&topclass=' + topc;
        }
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); },
            type: "get",
            url: strurl,
            success: function (data) { 
                $('#content').append("<div id='" + pageId + "' class='page' title='" + title + "' data-role='page'  >" + data + "</div>");
                $.mobile.hidePageLoadingMsg();
                $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
            }
        });

 
    }
};

//点击页面加载  pageid   div 的id   标题  
var pageload2 = function (controlname,pageId, title, actionname, viewname, tid) {
    var thisLoad = $("#" + pageId);
    if (thisLoad.length > 0) {
        $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
    } else {
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); },
            type: "get",
            url: $Maticsoft.BasePath + controlname + '/' + actionname + '?tid=' + tid + '&viewName=' + viewname + '&title=' + title,
            success: function (data) {
                $('#content').append("<div id='" + pageId + "' class='page' title='" + title + "' data-role='page'  >" + data + "</div>");
                $.mobile.hidePageLoadingMsg();
                $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
            }
        });
 
    }
};

//加载评论页  pageid   div 的id   标题    栏目编号  文章id  友情链接id
var pageloadComment = function (controlname,pageId, title, actionname, viewname, id, typeid, top) {
    var thisLoad = $("#" + pageId);
    if (thisLoad.length > 0) {
        $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
    } else {
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); },
            type: "get",
            url: $Maticsoft.BasePath + controlname + '/' + actionname + '?id=' + id + '&viewName=' + viewname + '&typeid=' + typeid + '&top=' + top,
            success: function (data) {
                $('#content').append("<div id='" + pageId + "' class='page' title='" + title + "' data-role='page' >" + data + "</div>");
                $.mobile.hidePageLoadingMsg();
                $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
            }
        });
 
    }
};
//分页加载
var pageloadByPage = function (controlname, pageId, title, actionname, viewname, classid, pageindex, hasImageurl, topc) {
    var thisLoad = $("#" + pageId);
    if (thisLoad.length > 0) {
        $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
    } else {
        var strurl = $Maticsoft.BasePath + controlname + '/' + actionname + '?viewName=' + viewname + '&pageIndex=' + pageindex;
        if (parseInt(classid) > 0) {
            strurl += "&classID=" + classid;
        }
        if (hasImageurl) {
            strurl += "&hasImageurl= " + hasImageurl; 
        }
        if (parseInt(topc) > 0) {
            strurl += "&topclass=" + topc;
        }
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); },
            type: "get",
            url: strurl,
            success: function (data) {
                $('#content').append("<div id='" + pageId + "' class='page' title='" + title + "' data-role='page'  >" + data + "</div>");
                $.mobile.hidePageLoadingMsg();
                $.mobile.changePage("#" + pageId, { transition: "slide" }, true);
            }
        });
 
    }
};

///为上一页下一页按钮赋值
var loadPrevNextBut = function (controlname, pageId, title, actionname, viewname, classid, hasImageurl, topc) {
    //立即获取到页码
    var pageindex = parseInt($('.hidpageindexnewsListpage:last').val()); //当前页码
    var totalpage = parseInt($('.hidtotalPagenewsListpage:last').val()); //总页码
    if (pageindex < 1) {
        pageindex = 1;
    }
    if (!hasImageurl) {
        hasImageurl = '';
    }
    if (parseInt(topc) <= 0) {
        topc = 0;//给个默认值
    }
    if (pageindex < totalpage) { //当前页码小于总页数  
        //设置下一页按钮
        $('.pageNextlink:last').attr('onclick', 'pageloadByPage("' + controlname + '","' + (pageId + (pageindex + 1)) + '","' + title + '","' + actionname + '", "' + viewname + '","' + classid + '","' + (pageindex + 1) + '","' + hasImageurl + '","' + topc + '")');
        $('.pageNextlink:last').text("下一页");
    }
    if (pageindex >= 2) {
        $('.pagePrevlink:last').attr('onclick', 'pageloadByPage("' + controlname + '","' + (pageId + (pageindex - 1)) + '","' + title + '","' + actionname + '", "' + viewname + '","' + classid + '","' + (pageindex - 1) + '","' + hasImageurl + '","' + topc + '")');
        $('.pagePrevlink:last').text("上一页");
    }
};

//搜索列表加载   点击搜索按钮以及搜索结果中的上一页下一页按钮
//actionname, viewname, menuid, 关键字, 页码
var searchpageload = function (controlname, actionname, viewname, menuid, kw, pageIndex) {
    $.ajax({
        beforeSend: function () { $.mobile.showPageLoadingMsg(); }, //显示加载器
        type: "get",
        url: $Maticsoft.BasePath + controlname + '/' + actionname + '?menuid=' + menuid + '&viewName=' + viewname + '&kw=' + kw + '&pageIndex=' + pageIndex,
        success: function (data) {
            $("#search_list").empty();
            $('#search_list').append(data);
            $.mobile.hidePageLoadingMsg(); //隐藏加载器
        }
    });
 
};

//为搜索结果列表中的内容赋值
var searchlistDataload = function() {
    /***a标签赋值开始***/
    //根据搜索类型的不同来确定要连接的页面
    var menuid = parseInt($('.hidmenuidnewsListpage:last').val()); //类型
    var searchalla = $('.hidmenuidnewsListpage:last').prev().prev().find('a'); //搜索后得到的所有a标签
    var i = 0;
    var searchaitemid; //contentid
    var searchaitemtitle; //标题
    switch (menuid) {
    case 19:
        for (i = 0; i < searchalla.length; i++) {
            searchaitemid = searchalla.eq(i).attr('itemid');
            searchaitemtitle = searchalla.eq(i).attr('itemtitle');
            searchalla.eq(i).attr('onclick', "pageload('Article','newsDetail_" + searchaitemid + "','" + searchaitemtitle + "','NewsDetail', 'NewsDetail','-1','" + searchaitemid + "')");
        }
        break;
    case 23:
        for (i = 0; i < searchalla.length; i++) {
            searchaitemid = searchalla.eq(i).attr('itemid');
            searchaitemtitle = searchalla.eq(i).attr('itemtitle');
            searchalla.eq(i).attr('onclick', "pageload('Article','productsDetailPhoto_" + searchaitemid + "','" + searchaitemtitle + "','NewsDetail', 'ProductsDetailPhoto','-1','" + searchaitemid + "')");
        }
        break;
    case 27:
        for (i = 0; i < searchalla.length; i++) {
            searchaitemid = searchalla.eq(i).attr('itemid');
            searchaitemtitle = searchalla.eq(i).attr('itemtitle');
            searchalla.eq(i).attr('onclick', "pageload('Article',,'jobsDetail_" + searchaitemid + "','" + searchaitemtitle + "','NewsDetail', 'JobsDetail','-1','" + searchaitemid + "')");
        }
        break;
    default:
        for (i = 0; i < searchalla.length; i++) {
            searchaitemid = searchalla.eq(i).attr('itemid');
            searchaitemtitle = searchalla.eq(i).attr('itemtitle');
            searchalla.eq(i).attr('onclick', "pageload('Article','newsDetail_" + searchaitemid + "','" + searchaitemtitle + "','NewsDetail', 'NewsDetail','-1','" + searchaitemid + "')");
        }
        break;
    }
    /***a标签赋值结束***/

    /*****上一页下一页赋值开始****/
    var keywords =$('.hidkwnewsListpage:last').val(); //关键字
    //立即获取到页码
    var pageindex = parseInt($('.hidpageindexnewsListpage:last').val()); //当前页码
    var totalpage = parseInt($('.hidtotalPagenewsListpage:last').val()); //总页码
    if (pageindex < 1) {
        pageindex = 1;
    }
    if (pageindex < totalpage) { //当前页码小于总页数  
        //设置下一页按钮
        $('.pageNextlink:last').attr('onclick', "searchpageload('Search','SearchList','_SearchList','" + menuid + "','" + keywords + "','" + (pageindex + 1) + "')");
        $('.pageNextlink:last').text("下一页");
    }
    if (pageindex >= 2) {
        $('.pagePrevlink:last').attr('onclick', "searchpageload('Search','SearchList','_SearchList','" + menuid + "','" + keywords + "','" + (pageindex - 1) + "')");
        $('.pagePrevlink:last').text("上一页");
    }
    /*****上一页下一页赋值结束****/

};
 
//调查结果颜色赋值
var rangetColor = function() {
    /***为调查结果各栏颜色赋值开始**/
    var itemcloor = $('.divresultcolor');
    for (var m = 0; m < itemcloor.length; m++) {
        var ranr = parseInt(Math.random() * 255);
        var rang = parseInt(Math.random() * 255);
        var ranb = parseInt(Math.random() * 255);
        var colorHex = ranr.toString(16) + rang.toString(16) + ranb.toString(16);
        itemcloor.eq(m).css('background-color', '#' + colorHex);
    }
    /***为调查结果各栏颜色赋值结束**/
};

//提交投票
var submitOptions = function () {
    var json = []; //声明json
    if ($('.topicsoptions').length<=0) {
        ShowFailTip("目前没有题目，请不要投票");     //$('.topicsoptions').eq(i).attr('qnumber')
        return false;
    }
    for (var i = 0; i < $('.topicsoptions').length; i++) {
        var toticsoptionsval = $('.topicsoptions').eq(i).val();
        if (toticsoptionsval == "" || toticsoptionsval == "0") {
            ShowFailTip("请填写第" + (i + 1) + "题");     //$('.topicsoptions').eq(i).attr('qnumber')
            return false;
        }
        json.push({ "topicid": $('.topicsoptions').eq(i).attr('topicsid'), "topicvlaue": $('.topicsoptions').eq(i).val(), "type": $('.topicsoptions').eq(i).attr('topicstype') });
    }
    $.ajax({
        url: $Maticsoft.BasePath + "Survey/SubmitPoll",
        type: 'post',
        dataType: 'text',
        async: false,
        timeout: 10000,
        data: { TopicIDjson: JSON.stringify(json) },
        success: function (resultData) {
            switch (resultData) {
                case "true":
                    ShowSuccessTip("投票成功！");
                    break;
                case "false":
                    ShowServerBusyTip("投票失败");
                    break;
                case "isnotnull":
                    ShowFailTip("不能重复投票！");
                    break;
                default:
                    break;
            }
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });
};


//隐藏页面头部多余的classname
hideclassname = function (hidea$) {
    if (hidea$.text() == hidea$.prev('a').text() || hidea$.text() == "" || hidea$.text() == "Null") {
        hidea$.css('display', 'none');
    }
};

//获得优酷播放地址
var getyoukump4url = function (youkuurlhtml, sourcesrc) {
    var result = "http://v.youku.com/player/getRealM3U8/vid/";
    var ykid = youkuurlhtml.slice(-18, -5);
    result += ykid;
    result += "/type/video.m3u8";
    sourcesrc.attr('src', result);
};

$(function () {

    /*****问卷调查开始****/
    $('[id$="_surveytopic0"]').die().live('click', function () { //单选按钮的单击事件
        $(this).parent().parent().find('[name="hidtopics0"]').val($(this).val()); //把当前选中的按钮的值保存到当前按钮的父节点的父节点中名字为hidtopics0的元素的value中 
    });

    $('[id$="_surveytopic1"]').die().live('click', function () { //多选按钮的单击事件  
        var values = ""; //得到与当前按钮同一组的被选中的checkbox的值
        $("input[name='" + $(this).attr('name') + "']:checked").each(function (i, d) {
            values += d.value + ",";
        });
        $(this).parent().parent().find('[name="hidtopics1"]').val(values.substring(0, values.length - 1)); //把当前选中的按钮的值保存到当前按钮的父节点的父节点中名字为hidtopics0的元素的value中 
    });
    /*****问卷调查结束****/


    /*****发表评论****/
    $('.commentsFormSubmit').die().live('click', function () {
        var commentform = $(this).parent();
        var id = commentform.find('[item="hidid"]').val();
        var typeid = commentform.find('[item="hidtypeid"]').val();
        var cont = commentform.find('[name="commentsInfos"]').val();  //内容
        var username = commentform.find('[name="commentstopic"]').val(); //用户
        if (username == "") {
            ShowFailTip("请填写用户昵称 !");
            return false;
        }
        if (cont == "") {
            ShowFailTip("请填写内容 !");
            return false;
        }
        //提交评论
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); }, //显示加载器
            url: $Maticsoft.BasePath + "Comment/AjaxComment",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                id: id, typeid: typeid, cont: cont, username: username
            },
            success: function (resultData) {
                $.mobile.hidePageLoadingMsg(); //隐藏加载器
                if (resultData == "true") {
                    ShowSuccessTip(" 评论已提交，请等待管理员审核!");
                    commentform.find('[name="commentsInfos"]').val('');
                    commentform.find('[name="commentstopic"]').val('');
                }
                else {
                    ShowFailTip(" 提交失败 !");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    });
    /******发表评论********/


    /******在线留言********/
    //清空留言 
    $('.gbookformnone').die().live('click', function () {
        var gbookform = $(this).parent();
        gbookform.find('[name="gbookMain"]').val('');
        gbookform.find('[name="gbookTopic"]').val('');
        gbookform.find('[name="userMail"]').val('');
    });

    //提交留言
    $('.gbookformsubmit').die().live('click', function () {
        var gbookform = $(this).parent();
        var goodcontent = gbookform.find('[name="gbookMain"]').val(); //内容
        var title = gbookform.find('[name="gbookTopic"]').val(); //标题
        var useremial = gbookform.find('[name="userMail"]').val(); //用户邮箱
        var regs = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/; ///^[\w-]+(\.[\w-]+)*\@@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if (title == "") {
            ShowFailTip("请填写标题 !");
            return false;
        }
        if (useremial != "") {
            if (!regs.test(useremial)) {
                ShowFailTip("请填写正确的邮箱!");
                return false;
            }
        } else {
            ShowFailTip("请填写邮箱!");
            return false;
        }

        if (goodcontent == "") {
            ShowFailTip("请填写内容 !");
            return false;
        }
        //提交留言
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); }, //显示加载器
            url: $Maticsoft.BasePath + "Gbook/AjaxGbook",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                Email: useremial, Content: goodcontent, Title: title
            },
            success: function (resultData) {
                $.mobile.hidePageLoadingMsg(); //隐藏加载器
                if (resultData == "true") {
                    ShowSuccessTip(" 已提交，请等待管理员审核!");
                    gbookform.find('[name="gbookMain"]').val(''); //内容
                    gbookform.find('[name="gbookTopic"]').val(''); //标题
                    gbookform.find('[name="userMail"]').val(''); //用户邮箱
                }
                else {
                    ShowFailTip(" 提交失败 !");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    });
    /******在线留言********/

    //搜索
    $('.searchFormSubmit').die().live('click', function () {
        var searchform = $(this).parent().parent();
        var keywords = searchform.find('[name="keywords"]').val();
        var menuid = searchform.find('[name="menuid"]').val();
        if (menuid == '0') {
            ShowFailTip("请选择类别 !");
            return false;
        }
        if (keywords == "") {
            ShowFailTip("请填写关键词 !");
            return false;
        }
        searchpageload('Search', 'SearchList', '_SearchList', menuid, keywords, '1');
    });

    //在线报名
    $('#btnEntryForm').die().live('click', function () {
        $(this).attr('disabled', 'disabled');
        var username = $.trim($('#username').val());
        if (username.length <= 0) {
            ShowFailTip("请填写姓名");
            $(this).removeAttr("disabled");
            return false;
        }

        var age = $.trim($('#age').val());
        if (age != "") {
            if (age.search(/^[1-9]\d{0,1}$/) == -1) {
                ShowFailTip(" 请填写正确的年龄");
                $(this).removeAttr("disabled");
                return false;
            }
        }

        var email = $.trim($('#Email').val());
        if (email.length > 0) {
            if (!/^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(email)) {
                ShowFailTip("请您输入正确的联系邮箱!");
                $(this).removeAttr("disabled");
                return false;
            }
        }

        var telPhone = $.trim($('#TelPhone').val());
        if (telPhone.length > 0) {
            if (!/^0\d{2,3}-?\d{7,8}$/.test(telPhone)) {
                ShowFailTip("请您输入正确的电话!");
                $(this).removeAttr("disabled");
                return false;
            }
        }

        var phone = $.trim($('#Phone').val());
        if (phone.length > 0) {
            if (!/^1([38][0-9]|4[57]|5[^4])\d{8}$/.test(phone)) {
                ShowFailTip("请您输入正确的手机号码!");
                $(this).removeAttr("disabled");
                return false;
            }
        } else {
            ShowFailTip("请您输入手机号码!");
            $(this).removeAttr("disabled");
            return false;
        }



        //地区
        var regionVal = $('#hfSelectedNode').val();
        if ((parseInt(regionVal) <= 0 || $.trim(regionVal) == '')) {
            ShowFailTip("请选择地区！");
            $(this).removeAttr("disabled");
            return false;
        }

        var qqVal = $.trim($('#QQ').val());
        if (qqVal != "") {
            if (qqVal.search(/^[1-9]\d{4,9}$/) == -1) {
                ShowFailTip(" 请填写有效的QQ号码");
                $(this).removeAttr("disabled");
                return false;
            }
        }
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg(); }, //显示加载器
            url: $Maticsoft.BasePath + "EntryForm/SubmitEntryForm",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                UserName: username,
                Age: age,
                Email: email,
                TelPhone: telPhone,
                Phone: phone,
                Region: regionVal,
                QQ: qqVal,
                Houseaddress: $('#HouseAddress').val(),
                CompanyAddress: $('#CompanyAddress').val(),
                Sex: $('#Sex').val(),
                Description: $('#Description').val(),
                Remark: $('#Remark').val()
            },
            success: function (JsonData) {
                $.mobile.hidePageLoadingMsg(); //隐藏加载器  
                switch (JsonData) {
                    case "isnotnull":
                        ShowFailTip("您已经提交过报名，请不要重复提交！");
                        break;
                    case "UserNameISNULL":
                        ShowFailTip(" 请填写名称！");
                        break;
                    case "true":
                        $("[name='clear_btn']").click();
                        ShowSuccessTip(" 提交成功！");
                        break;
                    default:
                        ShowServerBusyTip("提交失败，请稍后再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.mobile.hidePageLoadingMsg(); //隐藏加载器
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
        $(this).removeAttr("disabled");
    });
    $("[name='clear_btn']").die().live('click', function () {
        $('#hfSelectedNode').val(0);
        var selectList = $('#divaddressselect select');
        if (selectList.length > 1) {
            if (selectList.eq(1)) {
                selectList.eq(1).remove();
            }
            if (selectList.eq(2)) {
                selectList.eq(2).remove();
            }
        }

    });

});
