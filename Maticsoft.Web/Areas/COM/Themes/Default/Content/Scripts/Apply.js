$('#btnEntryForm').click( function () {
    $(this).attr('disabled', 'disabled');
    var username = $.trim($('#username').val());
    var sex = $("[name='radman']").val();
    if (username.length <= 0) {
        ShowFailTip("请填写姓名");
        $(this).removeAttr("disabled");
        return false;
    }
    var age = $.trim($('#age').val());
    if (age != "") {
        if (age.search(/^[1-9]\d{0,1}$/) == -1) {
            ShowFailTip(" 请填写正确的年龄");
            $(this).removeAttr("disabled");
            return false;
        }
    }

    var email = $.trim($('#Email').val());
    if (email.length > 0) {
        if (!/^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(email)) {
            ShowFailTip("请您输入正确的联系邮箱!");
            $(this).removeAttr("disabled");
            return false;
        }
    }

    var phone = $.trim($('#Phone').val());
    if (phone.length > 0) {
        if (!/^1([38][0-9]|4[57]|5[^4])\d{8}$/.test(phone)) {
            ShowFailTip("请您输入正确的手机号码!");
            $(this).removeAttr("disabled");
            return false;
        }
    } else {
        ShowFailTip("请您输入手机号码!");
        $(this).removeAttr("disabled");
        return false;
    }

    $.ajax({
        beforeSend: function () { $.mobile.showPageLoadingMsg(); }, //显示加载器
        url: $Maticsoft.BasePath + "WeChat/SubmitEntryForm",
        type: 'post',
        dataType: 'text',
        async: false,
        data: {
            UserName: username,
            Age: age,
            Email: email,
            Phone: phone,
            Houseaddress: $('#HouseAddress').val(),
            Sex: sex,
            Description: $('#Description').val()
        },
        success: function (JsonData) {
            $.mobile.hidePageLoadingMsg(); //隐藏加载器  
            switch (JsonData) {
                case "isnotnull":
                    ShowFailTip("您已经提交过报名，请不要重复提交！");
                    break;
                case "UserNameISNULL":
                    ShowFailTip(" 请填写名称！");
                    break;
                case "true":
                    ShowSuccessTip(" 报名成功，请等待后台管理员审核！");
                    setTimeout(function () {
                        location.href = $Maticsoft.BasePath + "WeChat/Apply";
                    }, 2000);
                    break;
                default:
                    ShowServerBusyTip("提交失败，请稍后再试！");
                    break;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.mobile.hidePageLoadingMsg(); //隐藏加载器  
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });
    $(this).removeAttr("disabled");
});