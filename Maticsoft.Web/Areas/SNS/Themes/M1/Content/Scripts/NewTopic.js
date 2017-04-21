var ImageUrl = "";
var Pid = "";
var GroupId = $("#groupid").val();
var isJoin = false;
var CheckUserIsJoinGroup = function () {

    $.ajax({
        url: $Maticsoft.BasePath + "profile/AJaxCheckUserIsJoinGroup",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        data: { GroupId: GroupId },
        async: false,
        success: function (resultData) {
            if (resultData == "joined") {
                isJoin = true;
                $.jBox.tip('您已经加入', 'success');

            } else {
                isJoin = false;
                $.jBox.confirm("确定加入此小组确定吗？", "提示", submit);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
    return isJoin;
};

var submitJoin = function (v, h, f) {
    if (v == 'ok') {
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxJoinGroup",
            type: 'post', dataType: 'text', timeout: 10000,
            async: false,
            data: { GroupId: GroupId },
            success: function (resultData) {
                if (resultData == "joined") {
                    isJoin = true;
                    $.jBox.tip('您已经加入', 'success');
                }
                else if (resultData == "Yes") {
                    isJoin = true;
                    $.jBox.tip('您已经成功加入', 'success');
                }
                else { isJoin = false; $.jBox.tip('出现异常', 'error'); }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    }
    return isJoin;
};


$(function () {
    Upload("#UploadPhoto");
    $("#addProduct").click(addProduct);
    $("#LoadUrlShow").click(function () { $("#LoadProductWindow").fadeIn(300); $("#tbiaoqing").hide(); });
    $("#LoadUrlClose").click(function () { $("#LoadProductWindow").fadeOut(300); $("#tbiaoqing").hide() });
    $("#biaoqingclose").click(function () { $("#tbiaoqing").hide(); });
    $("#UploadPhoto").click(function () { $("#tbiaoqing").hide(); $("#LoadProductWindow").hide(); });
    $(".biaoqingshow").click(function (e) {
        e.preventDefault();
        $("#tbiaoqing").slideToggle(0);
        $("#LoadProductWindow").hide();
    });
    $("#CancelImage").click(function (e) {
        e.preventDefault();
        $("#yulanImage").fadeOut(300);
    });
    $("#SubmitTopic").click(function () {
        var Title = $("#titleTopic").val();
        var Des = editor.getContent();
        if (Title == "" || Des == "") {
            $.jBox.tip('请填写完整', 'error');
            return;
        }
        if (ContainsDisWords(Title + Des)) {
            $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
            return;
        }
        var GroupId = $.getUrlParam("GroupId");
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AJaxCreateTopic",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            data: { ImageUrl: ImageUrl, Title: Title, Des: Des, Pid: Pid, GroupId: GroupId },
            success: function (resultData) {
                if (resultData == "No") {

                } else {
                    var mediaIds = "";
                    if ($(".isSendAll").attr('checked') != undefined) {
                        mediaIds = "-1";
                    } else {
                        var i = 0;
                        $(".bind>span").each(function () {
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

                    //同步到微博
                    var Option = {
                        ShareDes: Title,
                        ImageUrl: ImageUrl,
                        TopicID: resultData,
                        mediaIds: mediaIds
                    };
                    InfoSync.InfoSending(Option);
                    window.location = $Maticsoft.BasePath + "Group/GroupInfo?GroupId=" + GroupId;
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    });
});
function Upload(control) {

    $(".btn-success").find("input").css("height", "28px");
    var uploadbutton = $(control).html();
    var templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#fff;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style=" text-align: center; "></ul>' +
            '</div>';
    var uploader = new qq.FineUploader({
        element: $(control)[0],
        request: {
            endpoint: '/Upload/SNSUploadTmpImg.aspx'
        },
        text: {
            uploadButton: uploadbutton,
            waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
        },
        multiple: false,
        template: templatehtml,
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png'],
            sizeLimit: 10485760 // 10M = 10 * 1024*1024 bytes
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                if (responseJSON.success) {
                    $(".qq-upload-list").hide();
                    $.jBox.tip('上传成功', 'success');
                    ImageUrl = responseJSON.data;
                    $("#yulantu").attr("ref", ImageUrl.format("T116x170_"));
                    $("#yulantu").removeAttr("loaded").removeAttr("src");
                    $.scaleLoad('.xyu_a', 150, 137);
                    $("#yulanImage").fadeIn(300);
                    $(".btn-success").css("overflow", "");
                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
}
function addProduct() {
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
                $("#LoadProductWindow").fadeOut(300);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.jBox.tip('出现异常', 'error');
        }
    });
}

function insertsmilie(smilieface) {
    $("[id$='contentTopic']").val($("[id$='contentTopic']").val() + smilieface);
    $("#tbiaoqing").hide();
}
