
/**
* checkvideo.js
* 粘贴视频播放页地址(支持优酷、酷六)
* 功 能：检测优酷、酷六视频是否正确。
* 文件名称： checkvideo.js
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/09/25 12:00:00  蒋海滨   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

/*错误警告提示信息*/
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\">{0}</span></p>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\">{0}</span></p>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";

function addcss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt");
}

function addclass(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipts");
}

function addactivecss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-active");
}

function adderrcss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-err");
}

function adderrclass(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-errs");
}

function cleartip() {
    $("#videourlTip").empty();
}

function Initializationstyle() {
    addcss("txtVideoUrl");
}


// 验证视频地址
function CheckVideo() {

    var videourlVal = $.trim($('#txtVideoUrl').val());

    if (videourlVal=="") {
        adderrcss("txtVideoUrl");
        $("#videourlTip").empty();
        $("#videourlTip").append(warningTip.format("视频地址不能为空！"));
        return false;
    }

    var errnum = 0;

    $.ajax({
        url: $Maticsoft.BasePath + "UserCenter/CheckVideoUrl",
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
                    $("#videourlTip").empty();
                    $("#videourlTip").append(warningTip.format("视频地址不能为空！"));
                    break;
                case "Error":
                    $("#videourlTip").empty();
                    $("#videourlTip").append(warningTip.format("视频地址错误！"));
                    break;
                case "Succ":
                    addcss("txtVideoUrl");
                    $("#Test1").attr("src", JsonData.ImageUrl);
                    $("#Test2").attr("src", JsonData.VideoUrl);
                    //视频的原始地址
                    $("#Test3").attr("href", videourlVal);
                    $("#videourlTip").empty();
                    $("#videourlTip").append(succTip.format(""));
                    break;
                default:
                    errnum++;
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    break;
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            errnum++;
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });

    return errnum == 0 ? true : false;

}