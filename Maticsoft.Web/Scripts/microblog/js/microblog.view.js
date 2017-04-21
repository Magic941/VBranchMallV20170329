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


    elePicPanel.live("click", function () {
        $(this).parent().hide();
        $(this).parent().prev().show();
    });

    eleVideo.live("click", function () {
        $(this).parent().hide();
        var flashHtml = $('.flashHtml', $(this).parent()).html();
        var panel = $(this).parent().next();
        $('.mvideoPanel', panel).html(flashHtml);
        panel.show();

    });

    eleZoomin.live("click",function () {
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