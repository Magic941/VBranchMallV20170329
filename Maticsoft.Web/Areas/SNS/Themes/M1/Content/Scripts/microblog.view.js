/**
* microblog.view.js
*
* 功 能：微博视频展示效果
* 文件名称： microblog.view.js
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
function addBlogEvent(pageContext) {

    var elePicPanel;
    var eleVideo;
    var eleZoomin;
    

    if (pageContext) {
        elePicPanel = $('.mpicPanel', pageContext);
        eleVideo = $('.blogVideo', pageContext);
        eleZoomin = $('.cmdZoominLink', pageContext);
        
    }
    else {
        elePicPanel = $('.mpicPanel');
        eleVideo = $('.blogVideo');
        eleZoomin = $('.cmdZoominLink');
    }

    elePicPanel.die("click").live("click", function () {
        $(this).parent().hide();
        $(this).parent().prev().show();
    });
    eleVideo.die("click").live("click", function () {
        $(this).parent().hide();
        var flashHtml = $('.flashHtml', $(this).parent()).html();
        var panel = $(this).parent().next();
        $('.mvideoPanel', panel).html(flashHtml);
        panel.show();

    });
    eleZoomin.die("click").live("click",function () {
        var sPanel = $(this).parent().parent().parent().prev();
        $(this).parent().parent().parent().hide();
        var divObject = $(".mvideoPanel", $(this).parent().parent().parent());
        divObject.empty();
        sPanel.show();
    });
    var rotate = function (ele, rotationVal) {
        rotationPic($('img', $(ele).parent().parent().next()), rotationVal);
    };

}

$(document).ready(function () {
    addBlogEvent();

});