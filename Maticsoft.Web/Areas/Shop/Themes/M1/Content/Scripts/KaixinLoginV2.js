var Request = {
    QueryString: function (key) {
        var search = location.search.slice(1).toLowerCase();
        var arr = search.split("&");
        for (var i = 0; i < arr.length; i++) {
            var ar = arr[i].split("=");
            if (ar[0] == key) {
                return ar[1];
            }
        }

    }
}

$(function () {
    $(".i-kaixin").click(function () {
        var strLoginType = $.trim($(this).attr("logintype"));
        var strReturnUrl = ""

        switch (strLoginType) {
            case "default":
                //login.maticsoft域登录
                strReturnUrl = Request.QueryString("returnurl");
                break;
            /*case "layer": //浮层登录        ========已在http://login.maticsoft.com/js/login.js中实现
            strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
            break;*/ 
            case "other":
                //不是login.maticsoft域且不为浮存登录
                strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=refresh";
                break;

        }

        if (strReturnUrl == undefined) strReturnUrl = "";
        // 		window.open("http://login.maticsoft.com/Kaixin/KaixinTransferRequest.aspx?logintype=" + strLoginType + "&returnurl=" + strReturnUrl, 'kaixin', 'width=350,height=80,left=10,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no').blur();
        window.open("http://login.maticsoft.com/Kaixin/KaixinTransferRequest.aspx?logintype=" + strLoginType + "&returnurl=" + strReturnUrl);
    });
});