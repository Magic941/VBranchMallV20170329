var ShareToSina = {
    Message: "",
    //微博内容，不含单品路径
    ImageUrl: "",
    //图片路径
    ProductUrl: "",
    //产品路径
    Type: 1,
    //Type:1评论 2收藏 3购买
    TrackUrl: "/app/AdEntrance.aspx?from=xlwb&targetURL=",
    BindStatus: 0,
    //绑定状态
    CommentShareStatus: 0,
    //评论自动转发状态
    FavouriteShareStatus: 0,
    //收藏自动转发状态
    OrderShareStatus: 0,
    //购物自动转发状态
    IsAsyncStatus: false,
    //是否已从服务端同步BindStatus,CommentShareStatus，FavouriteShareStatus，OrderShareStatus等状态
    IsExistsFirstShare: 0,
    //判断是不是首次分享
    Comment: function(strComment, strProductName, strImageUrl, strProductUrl, ClassId1, ShowBtnCallBackFunction, SuccessCallBackFunction) {
        /// <summary>
        /// 评论转发 strComment:评论内容，strImageUrl:产品图片，strProductUrl:产品路径,ClassId1:产品大类,ShowBtnCallBackFunction:符合条件显示按钮回调方法,SuccessCallBackFunction:符合自动转发，转发成功回调方法,可以为null
        /// </summary>
        this.Type = 1;
        if (strComment.length == 0) return;
        this.Message = "我买的" + strProductName + "，不错吧！" + strComment;
        this.ImageUrl = strImageUrl;
        this.ProductUrl = this.TrackUrl + strProductUrl;

        if (this.Message.length > 140) {
            this.Message = this.Message.substring(0, 140);
        }

        if (ShareToSina.Check(strComment, ClassId1)) {
            ShareToSina.Send(SuccessCallBackFunction, 1);
        } else {
            if (ShowBtnCallBackFunction != null && ShowBtnCallBackFunction != undefined) {
                ShowBtnCallBackFunction(ShareToSina.Show, this.IsExistsFirstShare); //回调显示按钮方法，参数为按扭单击事件
            }
        }
    },
    Favourite: function(strProductName, strImageUrl, strProductUrl, ClassId1, ShowBtnCallBackFunction, SuccessCallBackFunction) {
        /// <summary>
        /// 收藏转发 strProductName:产品名称，strImageUrl:产品图片，strProductUrl:产品路径,ClassId1:产品大类,ShowBtnCallBackFunction:符合条件显示按钮回调方法,SuccessCallBackFunction:符合自动转发，转发成功回调方法,可以为null
        /// </summary>
        this.Type = 2;
        this.Message = "我刚在动软看到这个“" + strProductName + "”，觉得挺赞的! 正在考虑要不要买呢，先给大家看看，给我点意见吧！";
        this.ImageUrl = strImageUrl;
        this.ProductUrl = this.TrackUrl + strProductUrl;

        if (ShareToSina.Check(this.Message, ClassId1)) {
            ShareToSina.Send(SuccessCallBackFunction, 2);
        } else {
            if (ShowBtnCallBackFunction != null && ShowBtnCallBackFunction != undefined) {
                ShowBtnCallBackFunction(ShareToSina.Show, this.IsExistsFirstShare); //回调显示按钮方法，参数为按扭单击事件               
            }
        }

    },
    Order: function(strProductName, strImageUrl, strProductUrl, ClassId1, ShowBtnCallBackFunction, SuccessCallBackFunction) {
        /// <summary>
        /// 订购转发 strProductName:产品名称，strImageUrl:产品图片，strProductUrl:产品路径,ClassId1:产品大类,ShowBtnCallBackFunction:符合条件显示按钮回调方法,SuccessCallBackFunction:符合自动转发，转发成功回调方法,可以为null
        /// </summary>
        this.Type = 3;
        this.Message = "我在动软买了个（" + strProductName + "），挺不错的噢！大家也来看看吧！";
        this.ImageUrl = strImageUrl;
        this.ProductUrl = this.TrackUrl + strProductUrl;

        if (ShareToSina.Check(this.Message, ClassId1)) {
            ShareToSina.Send(SuccessCallBackFunction, 3);
        } else {
            if (ShowBtnCallBackFunction != null && ShowBtnCallBackFunction != undefined) {
                ShowBtnCallBackFunction(ShareToSina.Show, this.IsExistsFirstShare); //回调显示按钮方法，参数为按扭单击事件                
            }
        }
    },
    AsyncStatus: function() {
        /// <summary>
        /// 同步js对象状态
        /// </summary>
        $.ajax({
            type: "GET",
            url: "http://comm.maticsoft.com/microblog/CheckShareRight.aspx",
            data: "{}",
            dataType: "jsonp",
            async: false,
            success: function(result) {
                ShareToSina.BindStatus = result.BindStatus;
                ShareToSina.CommentShareStatus = result.CommentShareStatus;
                ShareToSina.FavouriteShareStatus = result.FavouriteShareStatus;
                ShareToSina.OrderShareStatus = result.OrderShareStatus ShareToSina.IsAsyncStatus = true;
                ShareToSina.IsExistsFirstShare = result.IsExistsFirstShare;
            },
            error: function(e) {}
        });
    },
    Check: function(strInfo, strClassId1) {
        /// <summary>
        /// 检查特殊分类及特殊字符
        /// </summary>
        if (strClassId1.toLowerCase().indexOf("me") != -1) { //特殊分类检查
            return false;
        }

        var reg = /\d+/i; //检查是否含数字
        if (strInfo.search(reg) != -1 && this.Type != 2) {
            return false;
        }

        //===========状态检查Begin=========
        if (ShareToSina.IsAsyncStatus) { //服务器获取状态成，继续执行
            var bReturn = true;
            switch (ShareToSina.Type) { //Type:1评论 2收藏 3购买
            case 1:
                bReturn = (ShareToSina.CommentShareStatus == 1 && ShareToSina.BindStatus == 1);
                break;
            case 2:
                bReturn = (ShareToSina.FavouriteShareStatus == 1 && ShareToSina.BindStatus == 1);
                break;
            case 3:
                bReturn = (ShareToSina.OrderShareStatus == 1 && ShareToSina.BindStatus == 1);
                break;
            }
            return bReturn;
        }
        //===========状态检查End=========
        return true;
    },
    Send: function(SuccessCallBackFunction, ShareType) {
        /// <summary>
        /// 发送信息到队列
        /// </summary>
        $.ajax({
            type: "GET",
            url: "http://comm.maticsoft.com/microblog/SendMessageToQueue.aspx",
            data: "Message=" + encodeURIComponent(ShareToSina.Message) + "&MediaUrl=" + encodeURIComponent(ShareToSina.ImageUrl) + "&ProductUrl=" + encodeURIComponent(this.ProductUrl) + "&Type=" + encodeURIComponent(ShareType),
            dataType: "jsonp",
            success: function(result) {
                if (SuccessCallBackFunction != null && SuccessCallBackFunction != undefined) {
                    SuccessCallBackFunction(result); //提交成功回调方法
                }
            },
            error: function(e) {}
        });
    },
    Show: function() {
        /// <summary>
        /// 展示信息
        /// </summary>
        var url = "http://comm.maticsoft.com/microblog/ShowSendMessage.aspx?Message=" + encodeURIComponent(ShareToSina.Message) + "&MediaUrl=" + encodeURIComponent(ShareToSina.ImageUrl) + "&ProductUrl=" + encodeURIComponent(ShareToSina.ProductUrl) + "&Type=" + ShareToSina.Type;
        window.open(url, '_blank');
    }
}

ShareToSina.AsyncStatus();

//----------------------------------Static Method-----------------------
$.ContentLoader = function(url, data, callback, error) {
    $.ajax({
        url: url,
        data: data,
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: callback,
        error: error
    });
};