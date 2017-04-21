$(document).ready(function () {

    //展示图片

    $("#imgShow").hide();

    $("[id$='lnkDelete']").click(function () {
        $("[id$='hfLogoUrl']").val("");
        $("[id$='lnkDelete']").hide();
        $("#imgShow").hide();
        clickautohide(4, "删除成功！", 2000);
    });

    $("[id$='lnkDelete']").hide();
    $("#uploadify").uploadify({
        'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
        'script': '/UploadNormalImg.aspx',
        'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
        'buttonImg': '/admin/images/uploadfile.jpg',
        'folder': 'UploadFile',
        'queueID': 'fileQueue',
        'width': 76,
        'height': 25,
        'auto': true,
        'multi': true,
        'fileExt': '*.jpg;*.gif;*.png;*.bmp',
        'fileDesc': 'All Files',
        'queueSizeLimit': 1,
        'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024,
        'onInit': function () {
        },

        'onSelect': function (e, queueID, fileObj) {
        },
        'onComplete': function (event, queueId, fileObj, response, data) {           
            if (response.split('|')[0] == "1") {
                var src = response.split('|')[1];
                $("#uploadfileNamShow").append("<p style='float:left; margin:5px 5px;'><img width='80px' height='40px' id='logo' src='" + src.format('') + "' /><input type=\"hidden\"  value=\"" + src.format('') + "\" name=\"acttachment\"></p>");
            } else {
                alert("图片上传失败！");
            }
        }
    });

    $("#submit_ad").click(function () {
        $("#Formmy").submit();
    });

    $("#cancel_ad").click(function () {
        window.location.href = document.referrer;
    });

    $(".reduceprice").each(function () {
        $(this).click(function () {
            var currentVal = parseInt($(this).next().val());
            currentVal = currentVal - 1;
            if (currentVal == 0) {
                currentVal = 1;
            }
            var price = $(this).next().attr("data-price");
            var amount=parseFloat(price) * currentVal;
            $("#allReturnAmount").text("￥" + amount.toFixed(2));
            $(this).next().val(currentVal);
        });

    });
    $(".increaseprice").each(function () {
        $(this).click(function () {
            var currentVal = parseInt($(this).prev().val());
            var maxVal = parseInt($(this).prev().attr("data-max"));
            currentVal = currentVal + 1;
            if (currentVal >= maxVal) {
                currentVal = maxVal;
            }
            var price = $(this).prev().attr("data-price");
            var amount = parseFloat(price) * currentVal;
            $("#allReturnAmount").text("￥" + amount.toFixed(2));
            $(this).prev().val(currentVal);
        });

    });
});
