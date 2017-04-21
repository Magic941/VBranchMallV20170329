var StyleFavorite = {
    StyleId: null,
    CurrentObj: null,
    ContactId: null,
    AClick: function (obj) {
        this.CurrentObj = $(obj);
        this.StyleId = this.CurrentObj.attr("styleId");
        this.ContactId = this.CurrentObj.attr("contactId");
        UserLoginStatus.isLogin = false;
        $("body").AjaxLogin({
            success: function () {
                StyleFavorite.Callback();
            },
            content: "请登录!"
        });
        return false;
    },
    AddLoveStyle: function (contactId, styleId) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=UTF-8",
            url: "Http://myshishang.maticsoft.com/Service/AjaxService.ashx",
            data: "beContactId=" + contactId + "&styleId=" + styleId + "&Method=InsertLoveStyle",
            dataType: "jsonp",
            success: function (data) {
                try { //更新会员喜欢数
                    if (data.result != -1) {
                        var LoveCountObj = $("#FavoriteCount_Span");
                        var LoveContactId = LoveCountObj.attr("contactid");
                        if (LoveContactId == contactId)
                            LoveCountObj.text(parseInt(LoveCountObj.text()) + 1);
                    }
                } catch (e) { }
            }
        });
    },
    Callback: function () {
        try {
            if (StyleFavorite.ContactId == UserLoginStatus.contactId) {
                StyleFavorite.CurrentObj.addClass("btnfav_already").parent().find(".f").text("不能喜欢自己哦").addClass("favok_error").show();
                StyleFavorite.Hide();
                return;
            }
        } catch (e) { }

        $.ajax(
        {
            type: "GET",
            contentType: "application/json",
            url: "http://member.maticsoft.com/service/ajaxservice.ashx?styleId=" + StyleFavorite.StyleId + "&skuitemid=&color=&size=&originate=myshishang&isboughtflag=false",
            data: "Method=InsertFavoriteStyle",
            dataType: "jsonp",
            jsonp: "jsoncallback",
            success: function (result) {
                if (result.d != null && result.d == '1') {
                    StyleFavorite.CurrentObj.addClass("btnfav_already").parent().find(".f").text("已喜欢").removeClass("favok_error").show();
                    var countObj = StyleFavorite.CurrentObj.parent().find(".favtxt span")
                    countObj.text(parseInt(countObj.text()) + 1);
                    StyleFavorite.Hide();
                    StyleFavorite.AddLoveStyle(StyleFavorite.ContactId, StyleFavorite.StyleId);
                } else {
                    StyleFavorite.CurrentObj.addClass("btnfav_already").parent().find(".f").text("已喜欢过").addClass("favok_error").show();
                    StyleFavorite.Hide();
                }
            },
            error: function () {
            }
        });

    }, Hide: function () {
        setTimeout('$("li .favok").hide();', 5000);
    }
};
