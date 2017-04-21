//// 需要分享的内容，请放到ready里
//WeixinApi.ready(function (Api) {

//    // 微信分享的数据
//    var wxData = {
//        "appId": "", // 服务号可以填写appId
//        "imgUrl": imgUrlVal,
//        "link": url,
//        "desc": desc,
//        "title": title
//    };

//    // 分享的回调
//    var wxCallbacks = {
//        // 分享操作开始之前
//        ready: function () {
//            // 你可以在这里对分享的数据进行重组
//            //alert("准备分享");
//        },
//        // 分享被用户自动取消
//        cancel: function (resp) {
//            // 你可以在你的页面上给用户一个小Tip，为什么要取消呢？
//            // alert("分享被取消，msg=" + resp.err_msg);
//        },
//        // 分享失败了
//        fail: function (resp) {
//            // 分享失败了，是不是可以告诉用户：不要紧，可能是网络问题，一会儿再试试？
//           // alert("分享失败，msg=" + resp.err_msg);
//        },
//        // 分享成功
//        confirm: function (resp) {
//            // 分享成功了，我们是不是可以做一些分享统计呢？
//            // alert("分享成功，msg=" + resp.err_msg);
//        },
//        // 整个分享过程结束
//        all: function (resp, shareTo) {
//            // 如果你做的是一个鼓励用户进行分享的产品，在这里是不是可以给用户一些反馈了？
//           // alert("分享" + (shareTo ? "到" + shareTo : "") + "结束，msg=" + resp.err_msg);
//        }
//    };

//    // 用户点开右上角popup菜单后，点击分享给好友，会执行下面这个代码
//    Api.shareToFriend(wxData, wxCallbacks);

//    // 点击分享到朋友圈，会执行下面这个代码
//    Api.shareToTimeline(wxData, wxCallbacks);

//    // 点击分享到腾讯微博，会执行下面这个代码
//    Api.shareToWeibo(wxData, wxCallbacks);

//    // iOS上，可以直接调用这个API进行分享，一句话搞定
//    Api.generalShare(wxData, wxCallbacks);
//});



//微信分享功能

wx.ready(function () {
    // config信息验证后会执行ready方法，
    //所有接口调用都必须在config接口获得结果之后，
    //config是一个客户端的异步操作，
    //所以如果需要在页面加载时就调用相关接口，
    //则须把相关接口放在ready函数中调用来确保正确执行。
    //对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。

    //获取“分享到朋友圈”按钮点击状态及自定义分享内容接口
    alert(title + imgUrlVal + url);
    wx.onMenuShareTimeline({
        title: title, // 分享标题
        link: url, // 分享链接
        imgUrl: imgUrlVal, // 分享图标
        success: function () {
            // 用户确认分享后执行的回调函数
        },
        cancel: function () {
            // 用户取消分享后执行的回调函数
        }
    });

    //获取“分享给朋友”按钮点击状态及自定义分享内容接口
    wx.onMenuShareAppMessage({
        title: title, // 分享标题
        desc: desc, // 分享描述
        link: url, // 分享链接
        imgUrl: imgUrl, // 分享图标
        type: 'link', // 分享类型,music、video或link，不填默认为link
        dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
        success: function () {
            // 用户确认分享后执行的回调函数
        },
        cancel: function () {
            // 用户取消分享后执行的回调函数
        }
    });
});

wx.error(function (res) {

    // config信息验证失败会执行error函数，如签名过期导致验证失败，
    //具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，
    //对于SPA可以在这里更新签名。
});

wx.checkJsApi({
    jsApiList: ['onMenuShareAppMessage'],// 需要检测的JS接口列表
    success: function (res) {
        // 以键值对的形式返回，可用的api值true，不可用为false
    }
});



