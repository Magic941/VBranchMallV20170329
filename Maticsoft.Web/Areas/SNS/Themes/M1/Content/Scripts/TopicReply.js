$(function () {
    $('#slides').slides({
        preload: true,
        preloadImage: 'images/download.gif',
        play: 5000,
        pause: 2500,
        hoverPause: true,
        animationStart: function (current) {
            $('.caption').animate({
                bottom: -35
            }, 100);
            if (window.console && console.log) {
                // example return of current slide number
                console.log('animationStart on slide: ', current);
            }
            ;
        },
        animationComplete: function (current) {
            $('.caption').animate({
                bottom: 0
            }, 200);
            if (window.console && console.log) {
                // example return of current slide number
                console.log('animationComplete on slide: ', current);
            }
            ;
        },
        slidesLoaded: function () {
            $('.caption').animate({
                bottom: 0
            }, 200);
        }
    });
});
function insertsmilieToCom(sender, smilieface) {
    $(sender).parents(".pinglunkuang").find("input").val($(sender).parents(".pinglunkuang").find("input").val() + smilieface);
    // $("[id$='contentWeibo']")
    $(".cbiaoqing").hide();
}
var ImageUrl = "";
var Pid = "";
$(function () {
    $(".AddTopicToFav").click(function () {
        var TopicId = $(this).attr("topicid");
        if (CheckUserState()) {
            $.ajax({
                url: $Maticsoft.BasePath + "profile/AjaxAddTopicToFav",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                data: { TopicId: TopicId },
                success: function (resultData) {
                    if (resultData == "Repeate") {
                        $.jBox.tip(' 您已收藏', 'success');
                    } else if (resultData == "Yes") {
                        $.jBox.tip('收藏成功', 'success');
                    } else {
                        $.jBox.tip('出现异常', 'error');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            });
        }
    });
    resizeImg('.pic_load', 460, 800);
    $(".cbiaoqingshow").die("click").live("click", function (e) {
        e.preventDefault();
        $(this).parent().find(".cbiaoqing").slideToggle(0);
    });
    $(".biaoqing_b1_y").die("click").live("click", function () { $(this).parent().parent().parent().hide(); });
    $(".replyTopic").die("click").live("click", function () {
        if ($(this).parents(".answer_a_wn").find(".pinglunkuang").length == 0) {
            $(this).parents(".answer_a_wn").append($("#inputReplyTemplete").html());
            $(this).parents(".answer_a_wn").find(".btnReply").attr("replyid", $(this).attr("replyid"));
            $(this).parents(".answer_a_wn").find(".answer_a_wn_c").show(0);
        } else {
            $(this).parents(".answer_a_wn").find(".answer_a_wn_c").slideToggle(0);
        }
    });

    $(".btnReply").die("click").live("click", function () {
        var ReplyId = $(this).attr("replyid");
        var Des = $(this).parents(".answer_a_wn_c").find("input").val();
        var ObjectThis = $(this);
        if (Des == "") {
            $.jBox.tip('请填写内容', 'error');
            return;
        }
        if (ContainsDisWords(Des)) {
            $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
            return;
        }
        if (CheckUserState()) {
            $.ajax({
                url: $Maticsoft.BasePath + "profile/AJaxCreateReply",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                data: { Des: Des, ReplyId: ReplyId },
                success: function (resultData) {
                    $("#MaticsoftTopicReply").prepend(resultData);
                    ObjectThis.parents(".answer_a_wn_c").find("input").val("");
                    $('body,html').animate({ scrollTop: 0 }, 1000);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            });
        }
    });
    Upload("#UploadPhoto");
    $("#addProduct").click(addProduct);
    $("#LoadUrlShow").click(function () { $("#LoadProductWindow").fadeIn(300) });
    $("#LoadUrlClose").click(function () { $("#LoadProductWindow").fadeOut(300) });
    $("#biaoqingclose").click(function () { $("#tbiaoqing").hide(); });
    $(".biaoqingshow").click(function (e) {
        e.preventDefault();
        $("#tbiaoqing").slideToggle(0);
    });
    $("#CancelImage").click(function (e) {
        e.preventDefault();
        $("#yulanImage").fadeOut(300);
    });
    $("#PostReply").click(function () {
        var Des = $("#contentTopic").val();
        var GroupId = $("#GroupId").val();
        var TopicId = $("#TopicId").val();
        var ObjectThis = $(this);
        if (Des == "") {
            $.jBox.tip('请填写内容', 'error');
            return;
        }
        if (CheckUserState()) {
            $.jBox.tip("发布中，请稍后...", 'loading');
            $.ajax({
                url: $Maticsoft.BasePath + "profile/AJaxCreateTopicReply",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                data: { ImageUrl: ImageUrl, Des: Des, Pid: Pid, GroupId: GroupId, TopicId: TopicId },
                success: function (resultData) {
                    if (resultData == "No") {
                        //                            ShowFailTip("操作失败，请您重试！");
                        $.jBox.tip("出现异常请重试...", 'error');
                    } else {
                        var mediaIds = "";
                        if (ObjectThis.parents(".whole_fom").find(".isSendAll").attr('checked') != undefined) {
                            mediaIds = "-1";
                        } else {
                            var i = 0;
                            ObjectThis.parents("whole_fom").find(".bind>span").each(function () {
                                if ($(this).attr("s_type") == "1" && $(this).attr("value") != "") {
                                    if (i == 0) {
                                        mediaIds = $(this).attr("value");
                                    } else {
                                        mediaIds = mediaIds + "," + $(this).attr("value");
                                    }
                                    i++;
                                }
                            });
                        }
                        var ReplyId = $(resultData).find(".replyTopic").attr("replyid");

                        //同步到微博
                        var Option = {
                            ShareDes: Des,
                            ImageUrl: ImageUrl,
                            TopicID: TopicId,
                            ReplyId: ReplyId,
                            mediaIds: mediaIds
                        };
                        InfoSync.InfoSending(Option);
                        $("#yulanImage").fadeOut(300);
                        ImageUrl = "";
                        Pid = "";
                        $("#contentTopic").val("");
                        $("#MaticsoftTopicReply").prepend(resultData);
                        $.jBox.tip("发布成功...", 'success');


                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            });
        }
    });
});
function Upload(control) {
    var uploadbutton = $(control).html();
    var templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;auto: hidden;background:#fff;height: 32px">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style=" text-align: center; "></ul>' +
            '</div>';

    var uploader = new qq.FineUploader({
        element: $(control)[0],
        request: {
            endpoint: '/Upload/SNSUploadTmpImg.aspx'
        },
        text: {
            uploadButton: uploadbutton
        },
        template: templatehtml,
        multiple: false,
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png']
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                if (responseJSON.success) {
                    $(".qq-upload-list").hide();
                    $(".btn-success").find("input").css("height", "28px");
                    $.jBox.tip('上传成功', 'success');
                    ImageUrl = responseJSON.data;
                    $("#yulantu").attr("src", responseJSON.data.format("T116x170_"));
                    $("#yulanImage").fadeIn(300);
                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
}
function addProduct() {
    if (CheckUserState()) {
        var LinkUrl = $("#ProductLink").val() + "&";
        ImageUrl = "";
        var urlreg = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/;
        if (LinkUrl && LinkUrl.length > 0 &&
                        !urlreg.test(LinkUrl)) {
            $.jBox.tip('请输入正确的链接', 'success');
            return false;
        }
        $.jBox.tip("努力给您获取中...", 'loading');
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxGetProductInfo",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { ProductLink: LinkUrl },
            success: function (resultData) {
                if (resultData == "No") {
                    $.jBox.tip('亲，获取失败，如果一直不成功，请联系管理员检查淘宝设置是否正确', 'error');
                }
                else {
                    var Datas = resultData.split("|");
                    Pid = Datas[0];
                    ImageUrl = Datas[1];
                    $("#yulantu").attr("src", ImageUrl);
                    $("#yulanImage").fadeIn(300);
                    $.jBox.tip('获取成功', 'success');
                    $("#LoadProductWindow").fadeOut(300)
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.jBox.tip('出现异常', 'error');
            }
        });
    }
}
function insertsmilie(smilieface) {
    $("[id$='contentTopic']").val($("[id$='contentTopic']").val() + smilieface);
    $("#tbiaoqing").hide();
}