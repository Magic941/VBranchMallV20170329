$(function () {
    if ($.getUrlParam("type") == "all") {
        $('#AllPostBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#FriendPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#MyPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#ReferBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#EachOtherPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "user") {
        $('#AllPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#MyPostBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#FriendPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#ReferBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#EachOtherPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "referme") {
        $('#AllPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#referMyBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#FriendPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#MyPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#EachOtherPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        //  $("#referDiv").show();
    }
    if ($.getUrlParam("type") == "ReferMyComment") {
        $('#AllPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#referMyBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#FriendPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#MyPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#EachOtherPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "eachother") {
        $('#AllPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#referMyBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#FriendPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#MyPostBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#EachOtherPostBtn').removeClass('tmenu_b').addClass('tmenu_a');
    }
    if (parseInt($("#pointer").val()) > 0) {
        $.MessageShow("登录成功！积分，<span style='color:yellow;font-weight: bolder;'>+" + $("#pointer").val() + "</span>");
    }
    //发布长微博
    $("#postBlog").click(function () {
        var title = $("#titleBlog").val();
        var des = editor.getContent();
        if (title == "") {
            $.jBox.tip('微博标题不能为空！', 'error');
            return;
        }
        if (title.length >= 40) {
            ShowFailTip("微博标题不能超出40个字！");
            return;
        }
        if (ContainsDisWords(title + des)) {
            $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
            return;
        }
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxAddBlog",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            data: { Title: title, Des: des },
            success: function (resultData) {
                $(".dialogDiv").hide();
                if (resultData == "No") {
                    ShowFailTip("操作失败，请您重试！");
                } else if (resultData == "AA") {
                    $.jBox.tip('管理员不能操作', 'error');
                } else {
                    var data = $(resultData);
                    TargetID = data.find(".favourite").attr("targetid");
                    var PostID = data.find(".CreateReport").attr("targetid");
                    data.filter('div').hide();
                    $("#PostAllContent").prepend(data);
                    data.filter('div').slideDown();
                    $("#titleBlog").val("");
                    $("#titleConent").val("");
                    editor.setContent("");
                    $(".dialogDiv").hide();
                }
            }
        });
    });

});
///发动态，内容框变颜色
$(function () {
    $("#contentWeibo").focus(function () {
        $(this).parent().css("border", "#FCD559 1px solid");
    }).blur(function () { $(this).parent().css("border", "#D2D2D2 1px solid"); });
});
var ImageUrl = "", Type, Pid = -1, AblumId = -1, TargetID = -1, VideoUrl = "", PostExUrl = "", VideoRawUrl, AudioUrl = "", MusicTipisShow = "", ProductName;
// VideoUrl = "http://v.youku.com/v_show/id_XNDcyODQxMTk2.html";
function Upload(control, type) {
    $(".btn-success").find("input").css("height", "28px");
    Type = type;
    var multiple = true;
    var uploadbutton = "请选择图片";
    var templatehtml;
    templatehtml = '<div class="qq-uploader span12">' +
        '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
        '<div class="qq-upload-button btn-success" style="background: #f69;float: none;">{uploadButtonText}</div>' +
        '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
        '<ul class="qq-upload-list" style="margin-top: 10px; text-align: center;"></ul>' +
        '</div>';
    if (type == "Image") {
        multiple = false;
        uploadbutton = $("#UploadImage").html();
        templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;background:#f7f7f7;">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style="margin-top: 0px; text-align: center; "></ul>' +
            '</div>';
    }
    var uploader = new qq.FineUploader({
        element: $(control)[0],
        request: {
            endpoint: '/Upload/SNSUploadTmpImg.aspx'
        },
        text: {
            uploadButton: uploadbutton,
            waitingForResponse: "\r处理中", dragZone: "上传", dropProcessing: "正在上传，请稍候..."
        },
        template: templatehtml,
        multiple: multiple,
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png']
            // sizeLimit: 51200 // 50 kB = 50 * 1024 bytes
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                VideoUrl = "";
                AudioUrl = "";
                PostExUrl = "";
                $(".qq-upload-list").hide();
                $(".btn-success").css("overflow", "");
                $(".btn-success").find("input").css("height", "28px").css("width", "50px").css("font-size", "12px");
                if (type == "Photo") {
                    $(".qq-uploader").hide();
                    $("#txtBtnPostPhoto").show();
                    $("#UploadPhotoWindow").hide();
                    if (responseJSON.success) {
                        $("#PhotoResultTemplate").load($Maticsoft.BasePath + "profile/AjaxGetUploadPhotoResult?image=" + responseJSON.data.format("T116x170_") + "&data=" + responseJSON.data, { limit: 25 },
                     function () {
                         $("#PhotoResultWindow").append($("#PhotoResultTemplate").html());
                         if ($("#PhotoResultWindow").find('.photolist').length > 1) {
                             $(".lyz_tab_left").css({ "background-image": "none" });
                             $(".lyz_tab_right").css({ "background-image": "none",
                                 "background-color": "#f7f7f7",
                                 "border-style": "solid", "border-width": "1px", "width": "603px",
                                 "border-color": "#ccc"
                             });
                         }
                     });
                    }
                    else {
                        $("#txtBtnPostPhoto").hide();
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                    }
                }
                else if (type == "Product") {
                    $(".qq-uploader").hide();
                    $("#txtBtnPostProduct").show();
                    $("#UploadProductWindow").hide();
                    if (responseJSON.success) {
                        $("#ProductResultTemplate2").load($Maticsoft.BasePath + "profile/AjaxProductResult?image=" + responseJSON.data.format("T116x170_") + "&data=" + responseJSON.data, { limit: 25 },
                     function () {
                         $("#ProductResultWindow").append($("#ProductResultTemplate2").html());
                         $(".lyz_tab_left").css({ "background-image": "none" });
                         $(".lyz_tab_right").css({ "background-image": "none",
                             "background-color": "#f7f7f7",
                             "border-style": "solid", "border-width": "1px", "width": "603px",
                             "border-color": "#ccc"
                         });

                     });
                    }
                    else {
                        $("#txtBtnPostProduct").hide();
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                    }
                }
                else if (type == "Image") {
                    if (responseJSON.success) {
                        ImageUrl = responseJSON.data;
                        $("#yulantuimage").attr("src", responseJSON.data.format("T116x170_"));
                        $("#yulantu").fadeIn(300);
                        $("#contentWeibo").val($("#contentWeibo").val() + "分享图片");
                        resizeImg('#yulantu', 211, 1280);
                    }
                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
}
function DelYulanTu() {
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxDelYulanTu",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { ImageUrl: ImageUrl },
        success: function (resultData) {
            if (resultData == "No") {
                $.jBox.tip("操作失败...", 'error');
            }
            else {
                ImageUrl = "";
                $("#yulantu").fadeOut(300);
                $("#yulantuimage").attr("src", "");
                $("#imagename").text("");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
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
                $("#UploadProductWindow").hide();
                Pid = Datas[0];
                ImageUrl = Datas[1];
                //                if ($("#con_one_3").children().length < 2) {
                //                    if ($("#con_one_2").children("#PhotoResultWindow")) {
                //                        $("#con_one_2").children("#PhotoResultWindow").remove();
                //                    }
                LoadAblumFun();
                $("#ProductResultWindow").append($("#ProductResultTemplate").html().format(Datas[1], "Product"));
                //$("#ProductResultTemplate").html().format(Datas[1], "Product").show();
                //                }
                //                else {
                //                    $("#picinfo").attr("src", Datas[1]);
                //                    $("#ProductResultTemplate").show();
                //                }
                $.jBox.tip('获取成功', 'success');
                VideoUrl = "";
                PostExUrl = "";
                AudioUrl = "";
                ProductName = Datas[2];
                $("#imagetype").hide();

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}
//发布图片
function addPhotoPost(imageurl, sharedes, albumid, maplng, maplat, address, categoryId) {
    $("#txtBtnPostPhoto").hide();
    if (ContainsDisWords(sharedes)) {
        $.jBox.tip('亲，您输入的内容含有禁用词，请重新输入！', 'error');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxPostAdd",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { Type: "Photo", ImageUrl: imageurl, ShareDes: sharedes, AblumId: albumid, MapLng: maplng, MapLat: maplat, Address: address, PhotoCateId: categoryId },
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else if (resultData == "AA") {
                $.jBox.tip('管理员不能操作', 'error');
            }
            else {
                var data = $(resultData);
                TargetID = data.find(".favourite").attr("targetid");
                var PostID = data.find(".CreateReport").attr("targetid");
                data.hide();
                $("#PostAllContent").prepend(data);
                data.slideDown();

                var mediaIds = "";
                AddPoint("Photo");
                var i = 0;
                if ($("#postPhoto").parents(".fabiao_cs").find(".isSendAll").attr('checked') != undefined) {
                    mediaIds = "-1";
                }
                else {
                    $("#postPhoto").parents(".fabiao_cs").find(".bind>span").each(function () {
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
                    Type: "Photo",
                    ShareDes: sharedes,
                    ImageUrl: imageurl.format(""),
                    TargetID: TargetID,
                    PostID: PostID,
                    VideoRawUrl: VideoRawUrl,
                    mediaIds: mediaIds
                };
                InfoSync.InfoSending(Option);
                //以上是同步到微博
                ImageUrl = "";
                VideoUrl = "";
                AudioUrl = "";
                TargetID = "";
                VideoRawUrl = "";
                mediaIds = "";
                data.slideDown();
                $("#imagetype").hide();

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}

//发布商品
function addProductPost(imageurl, sharedes, albumid, proname, proprice, prourl) {
    $("#txtBtnPostProduct").hide();
    if (ContainsDisWords(sharedes)) {
        ShowFailTip('亲，您输入的内容含有禁用词，请重新输入！');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxProductPost",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { ImageUrl: imageurl, ShareDes: sharedes, AblumId: albumid, ProductName: proname, ProductPrice: proprice, ProductUrl: prourl },
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else if (resultData == "AA") {
                ShowFailTip('管理员不能操作');
            }
            else {
                var data = $(resultData);
                TargetID = data.find(".favourite").attr("targetid");
                var PostID = data.find(".CreateReport").attr("targetid");
                data.hide();
                $("#PostAllContent").prepend(data);
                data.slideDown();

                var mediaIds = "";
                var i = 0;
                if ($("#postProductList").parents(".fabiao_cs").find(".isSendAll").attr('checked') != undefined) {
                    mediaIds = "-1";
                }
                else {
                    $("#postProductList").parents(".fabiao_cs").find(".bind>span").each(function () {
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
                    Type: "Product",
                    ShareDes: sharedes,
                    ImageUrl: imageurl.format(""),
                    TargetID: TargetID,
                    PostID: PostID,
                    VideoRawUrl: VideoRawUrl,
                    mediaIds: mediaIds
                };
                InfoSync.InfoSending(Option);
                //以上是同步到微博
                ImageUrl = "";
                VideoUrl = "";
                AudioUrl = "";
                TargetID = "";
                VideoRawUrl = "";
                mediaIds = "";
                data.slideDown();
                $("#imagetype").hide();

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}


function addPost(type) {
    var ShareDes = $("#contentWeibo").val();
    var ImageType = $('input[name="selecttype"]:checked').val();
    if (type == "Photo") {
        ShareDes = $("#contentPhoto").val();
    }
    if (type == "Product") {
        ShareDes = $("#contentProduct").val();
    }
    if (type == "Weibo" && $("#contentWeibo").val() == "") {

        $.jBox.tip('请输入内容', 'success');
        return;
    }
    if (type == "Weibo" && $.cookie("contentWeibo") == $("#contentWeibo").val() && ImageUrl == "" && VideoUrl == "" && AudioUrl == "") {
        $.jBox.tip('同样的内容不能连着发两次', 'error');
        return;
    }
    if (type != "Weibo" && ($("#myAlbums").val() == null || $("#myAlbums").val() == 'undefined')) {
        $.jBox.tip('请先选择或新建专辑', 'error');
        return;
    }
    if (ContainsDisWords(ShareDes)) {
        $.jBox.tip('亲，您输入的内容含有禁用词，请重新输入！', 'error');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxPostAdd",
        type: 'post', dataType: 'text', timeout: 10000,
        async: false,
        data: { Type: type, ImageUrl: ImageUrl, ShareDes: ShareDes, AblumId: $("#myAlbums").val(), Pid: Pid, ImageType: ImageType, VideoUrl: VideoUrl, PostExUrl: PostExUrl, AudioUrl: AudioUrl, ProductName: ProductName },
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else if (resultData == "AA")
            { $.jBox.tip('管理员不能操作', 'error'); }
            else if (resultData == "ProductRepeat")
            { $.jBox.tip('同样的商品您已经发布了一次', 'error'); }
            else {
                var data = $(resultData);
                TargetID = data.find(".favourite").attr("targetid");
                var PostID = data.find(".CreateReport").attr("targetid");
                data.filter('div').hide();
                $("#PostAllContent").prepend(data);
                data.filter('div').slideDown();
                if (type == "Photo") {
                    $("#UploadPhotoWindow").show();
                    $("#contentPhoto").val("");
                    $("#PhotoResultWindow").hide();
                }
                if (type == "Product") {
                    Pid = 0;
                    $("#UploadProductWindow").show();
                    $("#ProductLink").val("");
                    $("#contentProduct").val("");

                }
                if (type == "Weibo") {
                    $.cookie("contentWeibo", $("#contentWeibo").val());
                    $("#contentWeibo").val("");
                    $("#yulantu").fadeOut(300);
                    $("#yulanvideo").fadeOut(300);
                }
                var mediaIds = "";
                AddPoint(type);
                if (type == "Weibo") {
                    var i = 0;
                    if ($("#postWeibo").parents(".lyz_tab_right_b").find(".isSendAll").attr('checked') != undefined) {
                        mediaIds = "-1";
                    } else {
                        $(".lyz_tab_right_b>.bind>span").each(function () {
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
                } else if (type == "Photo") {
                    var i = 0;
                    if ($("#postPhoto").parents(".fabiao_cs").find(".isSendAll").attr('checked') != undefined) {
                        mediaIds = "-1";
                    } else {
                        $("#postPhoto").parents(".fabiao_cs").find(".bind>span").each(function () {
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
                } else if (type == "Product") {
                    var i = 0;
                    if ($("#postProduct").parents(".fabiao_cs").find(".isSendAll").attr('checked') != undefined) {
                        mediaIds = "-1";
                    } else {
                        $("#postProduct").parents(".fabiao_cs").find(".bind>span").each(function () {
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
                    $("#ProductResultWindow").empty();
                }


                //同步到微博
                var Option = {
                    Type: type,
                    ShareDes: ShareDes,
                    ImageUrl: ImageUrl.format(""),
                    TargetID: TargetID,
                    PostID: PostID,
                    VideoRawUrl: VideoRawUrl,
                    mediaIds: mediaIds
                };
                InfoSync.InfoSending(Option);
                //以上是同步到微博
                ImageUrl = "";
                VideoUrl = "";
                AudioUrl = "";
                TargetID = "";
                PostID = "";
                VideoRawUrl = "";
                mediaIds = "";
                data.filter('div').slideDown();
                $("#imagetype").hide();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}

function AddPoint(type) {
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxAddPoint",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { Type: type },
        success: function (resultData) {
            if (parseInt(resultData) > 0) {
                $.MessageShow("恭喜！积分，<span style='color:yellow;font-weight: bolder;'>+" + resultData + "</span>");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}
var submitAlbum = function (v, h, f) {
    var html = "";
    if (f.AlbumName == '') {
        ShowFailTip('请填写专辑的名称');
        return;
    }
    if (ContainsDisWords(f.AlbumName)) {
        ShowFailTip("您输入的名称含有禁用词");
        return;
    }
    if (!f.albumtype) {
        ShowFailTip('请选择类型');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxAddAlbum",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { AlbumName: f.AlbumName, Type: f.albumtype },
        success: function (resultData) {
            if (resultData == "NO") {
                $.jBox.tip('出现异常');
            }
            else if (resultData == "NoLogin") {
                $.jBox.tip('您还没有登录, 请刷新页面, 并登录后再试!');
            }
            else if (resultData == "AA") {
                $.jBox.tip('管理员不能创建专辑, 请您更换普通帐号再试!');
            } else {
                html += "<option selected='selected' value=" + resultData + ">" + f.AlbumName + "</option>";
                $("#myAlbums").append(html);
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    $.jBox.tip('创建成功', 'success');
    return true;
};
$("#createAlblums").die("click").live("click", function () {
    var html = "";
    $('.cre_a_2b input:first').attr('checked', 'true');
    html = $("#CreateAlbumsTemplate").html();
    $.jBox(html, { title: "新建专辑", buttons: { '创建': 1 }, submit: submitAlbum, width: 400, top: 300 });
});

//添加相片 时创建相册
$(".createAlblums").die("click").live("click", function () {
    var html = "";
    $('.cre_a_2b input:first').attr('checked', 'true');
    html = $("#CreateAlbumsTemplate").html();
    $.jBox(html, { title: "新建专辑", buttons: { '创建': 1 }, submit: submitAlbum, width: 400, top: 300 });
});
function LoadAblumFun() {
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxGetMyMyAblum",
        type: 'post', dataType: 'text', timeout: 10000,
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else {
                var Datas = $.parseJSON(resultData);
                var html;
                for (var i = 0; i < Datas.length; i++) {
                    html += "<option value=" + Datas[i].AlbumID + ">" + Datas[i].AlbumName + "</option>";
                }
                $("#myAlbums").html(html);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
}
function TopicFun() {
    $("#topic").click(function (e) {
        e.preventDefault();
        $("#contentWeibo").val($("#contentWeibo").val() + "#此处插入话题#");
    });
}

$(function () {
    $("#delyulavideo").click(function (e) {
        e.preventDefault();
        $("#yulanvideo").hide();
        VideoRawUrl = "";
        VideoUrl = "";
        PostExUrl = "";
        if (e && e.stopPropagation)
        //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        else
        //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        $("#contentWeibo").val("");

    });
    $("#addVideo").click(function (e) {

        var videourlVal = $.trim($('#txtVideoUrl').val());
        if (videourlVal == "") {
            $("#txtVideoUrl").empty();
            $.jBox.tip('视频地址不能为空', 'error');
            return false;
        }
        //$.jBox.tip("努力给您获取中...", 'loading');
        var errnum = 0;
        $.ajax({
            url: $Maticsoft.BasePath + "Profile/CheckVideoUrl",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                VideoUrl: videourlVal
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "NotNull":
                        $("#txtVideoUrl").empty();
                        $.jBox.tip('视频地址不能为空', 'error');
                        break;
                    case "Error":
                        $("#txtVideoUrl").empty();
                        $.jBox.tip('视频地址错误', 'error');
                        break;
                    case "Succ":

                        VideoUrl = JsonData.VideoUrl;
                        PostExUrl = JsonData.ImageUrl;
                        $("#contentWeibo").val(JsonData.VideoTitle);
                        $("#LoadVideoWindow").hide();
                        VideoRawUrl = $("#txtVideoUrl").val();
                        $("#txtVideoUrl").val("");
                        $("#yulanvideo").show();
                        $("#yulantuvideo").attr("src", PostExUrl);
                        $("#loadingvideo").hide();
                        $("#yulantuvideo").show();
                        ImageUrl = "";
                        Pid = "";

                        break;
                    default:
                        errnum++;
                        $.jBox.tip('服务器没有返回数据，可能服务器忙，请稍候再试', 'error');
                        break;
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                errnum++;
                $.jBox.tip('服务器没有返回数据，可能服务器忙，请稍候再试', 'error');
            }
        });
        if (e && e.stopPropagation)
        //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        else
        //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        return errnum == 0 ? true : false;
    });
});
// 验证视频地址
function CheckVideo(e) {

}
$(function () {

    $("#btnCanclePhoto").click(function () {
        //        $(".lyz_tab_left").css("background", "url()");
        //        $(".lyz_tab_right").css("background", "rgb(240, 240, 240)");
        $("#PhotoResultWindow").empty();
        $("#UploadPhotoWindow").show();
        $(".qq-uploader").show();
        $("#txtBtnPostPhoto").hide();
    });

    $("#btnCancleProduct").click(function () {
        //        $(".lyz_tab_left").css("background", "url()");
        //        $(".lyz_tab_right").css("background", "rgb(240, 240, 240)");
        $("#ProductResultWindow").empty();
        $("#UploadProductWindow").show();
        $(".qq-uploader").show();
        $("#txtBtnPostProduct").hide();
    });

    $("#ProductResultWindow").find(".btnCancleProduct").die("click").live("click", function (e) {
        e.preventDefault();
        $("#ProductResultWindow").empty();
        $("#UploadProductWindow").show();
        $("#ProductLink").val("");
    });

    $("#one2").click(function () {
        //        $(".lyz_tab_left").css("background", "url()");
        //        $(".lyz_tab_right").css("background", "rgb(240, 240, 240)");
        //        $("#PhotoResultWindow").empty();
        //        $("#UploadPhotoWindow").show();
        //        $("#txtBtnPostPhoto").hide();
    });

    //长微博
    $("#AddContent").click(function () {
        $(".dialogDiv").show();
    });
    $("#closeDialog").click(function () {
        $(".dialogDiv").hide();
    });

    $("#one1").click(function () {
        //        $(".lyz_tab_left").css("background", " url(../images/left3.jpg)");
        //        $(".lyz_tab_right").css("background", "background: url(../images/user_12.jpg)");
    });

    Upload("#UploadImage", "Image");
    Upload("#UploadPhoto", "Photo");
    Upload("#UploadProductBtn", "Product");
    $(".productType").each(function() {
        var value = $(this).css("display");
        if (value != "none") {
            var item = $(this).attr("item");
            $(".productType").removeClass("current_fabuaa");
            $(this).addClass("current_fabuaa");
            if (item == "1") {
                $("#LocalProduct").show();
                $("#NetworkProduct").hide();
            }
            if (item == "2") {
                $("#LocalProduct").hide();
                $("#NetworkProduct").show();
            }
            return;
        }
    });
    $(".productType").click(function () {
        var item = $(this).attr("item");
        $(".productType").removeClass("current_fabuaa");
        $(this).addClass("current_fabuaa");
        if (item == "1") {
            $("#LocalProduct").show();
            $("#NetworkProduct").hide();
        }
        if (item == "2") {
            $("#LocalProduct").hide();
            $("#NetworkProduct").show();
        }
    });

    $("#addProduct").click(addProduct);
    $("#delyulatu").click(DelYulanTu);
    $("#postWeibo").click(function () { addPost("Weibo"); });
    $("#postProduct").die("click").live("click", function (e) {
        e.preventDefault(); addPost("Product");
    });
    // $("#postPhoto").die("click").live("click", function (e) { e.preventDefault(); addPost("Photo"); });
    $("#postPhoto").die("click").live("click", function (e) {
        e.preventDefault();
        $("#con_one_2 .photolist").each(function () {
            var albumId = $(this).find(".fabiao_bs_2").children().val();
            var address = $(this).find(".fabiao_cs_2").children("[name='Address']").attr("province") + $(this).find(".fabiao_cs_2").children("[name='Address']").val();
            var maplng = $(this).find(".fabiao_cs_2").children("[name='Address']").next().val();
            var maplat = $(this).find(".fabiao_cs_2").children("[name='Address']").next().next().val();
            var sharedes = $(this).find(".fabiao_cs_2").children("[name='PhotoAddAlbumscontent']").val();
            var imageurl = $(this).find(".fabiao_a").children("[name='imagedata']").val();
            var categoryId = $(this).find(".fabiao_bs_2").children("[name='PhotoCate']").val();
            if (!albumId) {
                ShowFailTip("请选择专辑！");
                return;
            }
            else {
                addPhotoPost(imageurl, sharedes, albumId, maplng, maplat, address, categoryId);
            }
        });
        $(".qq-uploader").show();
        $("#PhotoResultWindow").empty();
        $("#txtBtnPostPhoto").hide();
        $("#UploadPhotoWindow").show();
        $("#imagetype").hide();
        $("#con_one_2").show();

    });

    $("#postProductList").die("click").live("click", function (e) {
        e.preventDefault();
        $("#con_one_3 .productlist").each(function () {
            var albumId = $(this).find(".fabiao_bs_2").children().val();
            var productname = $(this).find(".fabiao_cs_2").children("[name='ProductName']").val();
            var price = $(this).find(".fabiao_cs_2").children("[name='ProductPrice']").val();
            var producturl = $(this).find(".fabiao_cs_2").children("[name='ProductUrl']").val();
            var sharedes = $(this).find(".fabiao_cs_2").children("[name='ProductDescription']").val();
            var imageurl = $(this).find(".fabiao_a").children("[name='imagedata']").val();
            if (!albumId) {
                ShowFailTip("请选择专辑！");
                return;
            }
            if (producturl.indexOf("http://") == 0) {
                addProductPost(imageurl, sharedes, albumId, productname, price, producturl);
            } else {
                ShowFailTip("链接地址不合法！");
            }
        });
        $(".qq-uploader").show();
        $("#ProductResultWindow").empty();
        $("#txtBtnPostProduct").hide();
        $("#UploadProductWindow").show();
        $("#imagetype").hide();
        $("#con_one_3").show();

    });


    TopicFun();
    $("#biaoqingclose").click(function () { $("#tbiaoqing").hide(); });
    $("#biaoqingclose").click(function () { $("#tbiaoqing").hide(); });
    $("#biaoqingshow").click(function (e) {
        e.preventDefault(); $("#tbiaoqing").slideToggle(0);
        $(".yshopp").hide();
    });
    $("#UploadVideo").click(function (e) {
        e.preventDefault();
        $("#LoadVideoWindow").show(); $("#LoadAudioWindow").hide(); $("#yulantu").hide();
        $("#yulanvideo").hide();
        $("#tbiaoqing").hide();
    });
    $("#UploadAudio").click(function (e) {
        e.preventDefault(); $("#LoadAudioWindow").show(); $("#LoadVideoWindow").hide(); $("#yulantu").hide(); $("#yulanvideo").hide();
        $("#tbiaoqing").hide();
    });
    $("#UploadImage").click(function (e) {
        $("#tbiaoqing").hide();
        $(".yshopp").hide();
    });
    $("#LoadUrlClose").click(function (e) {
        e.preventDefault();
        $("#LoadVideoWindow").hide();
        if (e && e.stopPropagation)
        //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        else
        //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;

        $("#txtVideoUrl").val("");
    });
    $("#LoadAudioClose").click(function (e) {
        e.preventDefault(); $("#LoadAudioWindow").hide();

        if (e && e.stopPropagation)
        //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        else
        //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;

        $("#search_input").val("");
    });
    $(".music_list li").live('click', function () {
        // <h3>曲名：' + $(this).attr("name") + '</h3>
        var PL = '<embed src="http://www.xiami.com/widget/470304_' + $(this).attr("id") + '/singlePlayer.swf" type="application/x-shockwave-flash" width="257" height="33" wmode="transparent"></embed>';
        AudioUrl = PL;
        ImageUrl = "";
        VideoUrl = "";
        VideoRawUrl = "";
        var posttext = "分享音乐：" + $(this).attr("name") + "";
        $("#contentWeibo").val(posttext);
        $("#LoadAudioClose").click();
    });
    $("#SearchAudio").click(function () {
        searchMusicList(1);
        MusicTipisShow = true;
    });



});

function insertsmilie(smilieface) {
    $("[id$='contentWeibo']").val($("[id$='contentWeibo']").val() + smilieface);
    $("#tbiaoqing").hide();
}


function setTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {
        var menu = document.getElementById(name + i);
        var con = document.getElementById("con_" + name + "_" + i);
        menu.className = i == cursel ? "hover" : "";
        //con.style.display = i == cursel ? "block" : "none";
        i == cursel ? $(con).show() : $(con).hide();
        $(con).parent().hide().fadeIn(10);
    }
}
