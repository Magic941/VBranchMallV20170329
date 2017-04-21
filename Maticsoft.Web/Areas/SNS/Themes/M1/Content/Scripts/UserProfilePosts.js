//@*控制字数*@
function textCounter(fieldId, countfieId, maxlimit) {
    var fieldEle = fieldId;
    var countfieldEle = $("#" + countfieId + "");
    if (fieldEle == null || countfieldEle == null) {
        return false;
    }
    if (fieldEle.value.gblen() > maxlimit) // too long... trim it
    //fieldEle.value = fieldEle.value.substring(0, maxlimit);
    {
        $.jBox.tip('亲，您输入的太多了', 'error');
        fieldEle.value = fieldEle.value.gbtrim(maxlimit, '');
    }
    else {
        countfieldEle.text(maxlimit - fieldEle.value.gblen());
    }

}



String.prototype.gblen = function() {
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
String.prototype.gbtrim = function(len, s) {
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

//@*控制字数*@

var submit = function (v, h, f) {
    if (f.content == '') {
        f.content = '转发';
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxPostForward",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { content: f.content, origid: f.origid, origuserid: f.origuserid, orignickname: f.orignickname, forwardid: f.forwardid },
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else {
                var Data = $(resultData);
                Data.hide();
                $("#PostAllContent").prepend(Data);
                Data.slideDown();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    $.jBox.tip('转发成功', 'success');
    return true;
};
$(".postforward").die("click").live("click", function (e) {
    if (CheckUserState()) {
        e.preventDefault();
        var content = $("#des" + $(this).attr("postid") + "").text();
        var forwardvalue = $(this).attr("forwardvalue");
        var forwardcontent = $(this).attr("forwardcontent");
        var origid = $(this).attr("origid");
        var origuserid = $(this).attr("origuserid");
        var orignickname = $(this).attr("orignickname");
        var forwardid = $(this).attr("postid");
        var html = "<div style='padding-left:20px;padding-top:10px;' >" + forwardvalue + "</div>";
        var regExp = /<a.*?>(.*?)<\/a>/ig;
        forwardcontent = forwardcontent.replace(regExp, "$1");
        html += "<div style='padding:10px;'><textarea class='fw_content' id='fcontent' onclick='process(0,'fcontent');'  name=content style='height: 78px;width:300px;margin-left:15px;font-size:12px'>" + forwardcontent + "</textarea></div>";
        html += "<input type='hidden' name='origid'' value='" + origid + "'>";
        html += "<input type='hidden' name='origuserid'' value='" + origuserid + "'>";
        html += "<input type='hidden' name='orignickname'' value='" + orignickname + "'>";
        html += "<input type='hidden' name='forwardid'' value='" + forwardid + "'>";
        $.jBox(html, { title: "转发动态", buttons: { '转发': 1 }, submit: submit, width: 350, top: 300 });
        process(0, "fcontent");
    }
});


function setCursorPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}
//test 
function process(id, targetId) {
    var no = id;
    setCursorPosition(document.getElementById(targetId), no);
} 

function commentFun() {
    $(".postComment").die("click").live("click", function(e) {
        if (CheckUserState()) {
            var postid = $(this).attr("postid");
            var posttype = $(this).attr("type");
            e.preventDefault();
            if ($("#Inputcomment" + postid + "").children('div').length == 0) {
                $("#Inputcomment" + postid + "").prepend($("#commentInputTemplate").html().format(postid, posttype)).slideToggle();
                $.ajax({
                    url: $Maticsoft.BasePath + "profile/AjaxGetCommentByPostId",
                    type: 'post',
                    dataType: 'text',
                    timeout: 10000,
                    data: { PostId: postid },
                    success: function(resultData) {
                        if (resultData != "No") {
                            $("#pinglun" + postid + "").prepend(resultData);
                            $("#pinglun" + postid + "").css("padding", "10px").css("margin-top", "40px");
                            //  $("#pinglun" + postid + "").slideToggle();
                        }
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        ShowFailTip("操作失败：" + errorThrown);
                    }
                });
            } else {
                $("#Inputcomment" + $(this).attr("postid") + "").slideToggle();
                $("#pinglun" + postid + "").slideToggle();
            }
        }
    });
}
function commentRelyBtnFun() {
    $(".replyBtnPost").die("click").live("click", function (e) {
        CheckUserState();
        var postid = $(this).attr("postid");
        var posttype = $(this).attr("type");
        var objectThis = $(this);
        var Des = $("#replyContent" + postid + "").val();
        if (Des.length == 0) {
            $.jBox.tip('请输入内容哦', 'error');
            return;
        }
        e.preventDefault();
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxAddComment",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { PostId: postid, Des: Des },
            success: function (resultData) {
                if (resultData == "No") {
                    ShowFailTip("操作失败，请您重试！");
                }
                else {
                    $("#pinglun" + postid + "").prepend(resultData);
                    $("#pinglun" + postid + "").css("padding", "10px").css("margin-top", "40px");
                    $("#CommentCount" + postid + "").text(parseInt($("#CommentCount" + postid + "").text()) + 1);
                    $("#replyContent" + postid + "").val("");

//                    var mediaIds = "";
//                    if (objectThis.parents(".answer_a_wn_d").find(".isSendAll").attr('checked') != undefined) {
//                        mediaIds = "-1";
//                    } else {
//                        var i = 0;
//                        objectThis.parents(".answer_a_wn_d").find(".bind>span").each(function () {
//                            if ($(this).attr("s_type") == "1" &&$(this).attr("value") != "") {
//                                if (i == 0) {
//                                    mediaIds = $(this).attr("value");
//                                } else {
//                                    mediaIds = mediaIds + "," + $(this).attr("value");
//                                }
//                                i++;
//                            }
//                        });
//                    }
//                    alert(mediaIds);
//                    //同步到微博
//                    var Option = {
//                        ShareDes: Des,
//                        PostID: postid,
//                        mediaIds: mediaIds
//                    };
//                    InfoSync.InfoSending(Option);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
        //下面是同时转发的微博的操作
        if ($("#chkforward" + postid + "").attr("checked") == "checked") {
            var forwardbtn = $("#postforward" + postid + "")
            var forwardcontent = forwardbtn.attr("forwardcontent").length > 0 ? Des + "\\" + forwardbtn.attr("forwardcontent") : Des;
            var origuserid = forwardbtn.attr("origuserid");
            var orignickname = forwardbtn.attr("orignickname");
            var origid = forwardbtn.attr("origid");
            var forwardid = forwardbtn.attr("postid");
            ForwardFun(origid, forwardid, forwardcontent, origuserid, orignickname);
            $.jBox.tip('操作成功', 'success');
        }
    })
}
function ForwardFun(origid, forwardid, forwardcontent, origuserid, orignickname) {
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxPostForward",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { content: forwardcontent, origid: origid, forwardid: forwardid, origuserid: origuserid, orignickname: orignickname },
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else {
                var Data = $(resultData);
                Data.hide();
                $("#PostAllContent").prepend(Data);
                Data.slideDown();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    $.jBox.tip('转发成功', 'success');
    return true;
}

function replyCommentFun() {
    $(".replyCommentBtn").die("click").live("click", function (e) {
        CheckUserState();
        e.preventDefault();
        var postid = $(this).attr("postid");
        var replyname = $(this).attr("replyName");
        $("#replyContent" + postid + "").val("@"+replyname + ":");
    });
}
function DelPostFun() {
    $(".DelPost").die("click").live("click", function (e) {
        CheckUserState();
        e.preventDefault();
        var postid = $(this).attr("postid");
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxDelPost",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { PostId: postid },
            success: function (resultData) {
                if (resultData == "No") {
                    ShowFailTip("操作失败，请您重试！");
                }
                else {
                    $("#wpostid" + resultData + "").remove();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    });
}
function insertsmilieToCom(sender, smilieface) {

    $(sender).parents("[id^=Inputcomment]").find("[id^=replyContent]").val($(sender).parents("[id^=Inputcomment]").find("[id^=replyContent]").val() + smilieface);
    // $("[id$='contentWeibo']")
    $(".cbiaoqing").hide();

}

$(function() {
    commentFun();
    commentRelyBtnFun();
    replyCommentFun();
    DelPostFun();
});

//////举报相关
var typeid;
var reporttargetid;
$(function() {
    $(".CreateReport").die("click").live("click", function() {
        reporttargetid = $(this).attr("targetid");
        $.ajax({
            type: "POST",
            dataType: "text",
            async: false,
            url: $Maticsoft.BasePath + "Profile/AjaxCreateReport",
            success: function(data) {
                $(data).find('input:radio:first').attr('checked', 'checked');
                var html = $(data).find("#CreateReport").html();
                $(data).find('.cre_a_2b input:first').attr('checked', 'true');
                $.jBox(html, { title: "举报", buttons: { '创建': 1 }, submit: submitReport, width: 450, height: 250, top: 400 });
            }
        });
    });
});

var submitReport = function (v, h, f) {
    var html;
    if (f.Description == '') {
        $.jBox.tip('您填写理由', 'success');
        return;
    }
    if (!f.reporttype) {
        $.jBox.tip('请选择类型', 'success');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxAddReport",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { Description: f.Description, Type: f.reporttype, TargetId: reporttargetid },
        success: function (resultData) {
            if (resultData != "No") {
                $.jBox.tip('提交成功，请耐心等待结果', 'success');
            }
            else { $.jBox.tip('出现异常', 'error'); }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.jBox.tip('出现异常', 'error');
        }
    });

    return true;
};





///// 下面是无刷新分页的开始
$(function () {
    var DataCount = $("#DataCount").val();
    var PageSize = $("#PageSize").val();
    var PostType = $("#PostType").val();
    var UserId = $("#UserId").val();
    if (parseInt(DataCount) <= parseInt(PageSize)) {
        $("#Pagination").hide();
    }
    //此demo通过Ajax加载分页元素
    var initPagination = function () {
        var num_entries = DataCount;
        // 创建分页
        $("#Pagination").pagination(num_entries, {
            num_edge_entries: 1, //边缘页数
            num_display_entries: 10, //主体页数
            callback: pageselectCallback,
            items_per_page: parseInt(PageSize), //每页显示1项
            prev_text: "前一页",
            next_text: "后一页"
        });
    };
    initPagination();
    function pageselectCallback(page_id, jq) {
        //alert(page_id);
        InitData(page_id + 1);
    }
    function InitData(pageIndex) {
        $("#PostAllContent").html("<div style='margin:0 auto;text-align: center;'><img src='/Areas/SNS/Themes/M1/Content/images/loads.gif' ></div>");
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "User/AjaxGetPostByIndex",
            data: { pageIndex: pageIndex, Type: PostType, UserId: UserId },
            success: function (data) {
                if (data != "No") {
                    $("#PostAllContent").html(data);
                } else {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            }
        });
    }
    $(".posttype").click(function () {
        $(this).addClass("tmenu_a").parent().siblings().children("a").removeClass("tmenu_a").addClass("tmenu_b");
        PostType = $(this).attr("data_posttype");
        ReBindData(PostType);
        initPagination();
    });
    function ReBindData(PostType) {
        $("#PostAllContent").html("<div style='margin:0 auto;text-align: center;'><img src='/Areas/SNS/Themes/M1/Content/images/loads.gif' ></div>");
        $.ajax({
            type: "POST",
            dataType: "text",
            async: false,
            url: $Maticsoft.BasePath + "User/GetDataCountByPageType",
            data: { PostType: PostType, UserId: UserId },
            success: function (data) {
                if (parseInt(data) > 0) {
                    DataCount = data;
                    if (parseInt(DataCount)> parseInt(PageSize)) {
                        $("#Pagination").show();
                    } else {
                        $("#Pagination").hide();
                    }
                }
                else if (parseInt(data) == 0) {
                    $("#Pagination").hide();
                    $("#PostAllContent").html("");
                }
                else {
                    $.jBox.tip('出现异常', 'error');

                }
            }
        });
    }
});
///// 下面是无刷新分页的开始

$(function () {

})