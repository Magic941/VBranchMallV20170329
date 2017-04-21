//判断是否含有禁用词
function ContainsDisWords(desc) {
    var isContain = false;
    $.ajax({
        url: $Maticsoft.BasePath + "Partial/ContainsDisWords",
        type: 'post', dataType: 'text', timeout: 10000,
        async: false,
        data: { Desc: desc },
        success: function (resultData) {
            if (resultData == "True") {
                isContain = true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    return isContain;
}

//检查是否登录
var CheckUserState = function () {
    var islogin;
    $.ajax({
        url: $Maticsoft.BasePath + "User/CheckUserState",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData != "Yes") {
                var html = "<div style='margin-left:90px;margin-top:20px;font-size: 16px;font-weight: bold;'>账号：<input type='text' style='height: 30px;width: 200px;' id='loginname' name='name' /><span style='display:none;color:red;font-size: 16px;' name='tipname' id='tipname'>请输入账号</span></div>";
                html += "<div style='margin-left:90px;margin-top:20px;font-size: 16px;font-weight: bold;'>密码：<input type='password' style='height: 30px;width: 200px;' id='loginpwd' name='pwd' /><span style='display:none;color:red;font-size: 16px;' name='tippwd' name='tippwd'>请输入密码</span></div>";
                html += "<div class='login'><ul><li><a href='#' id='ajaxlogin'> <img src='/Areas/SNS/Themes/M1/Content/images/login.jpg'></a></li></ul>";
                html += "<ul><li><a href='/Account/Register'><img src='/Areas/SNS/Themes/M1/Content/images/reg.jpg'></a></li></ul>";
                html += "<ul><li><a href='/social/sina'><img src='/Areas/SNS/Themes/M1/Content/images/sin.jpg'></a></li><li><a href='/social/sina'>新浪微博</a></li></ul>";
                html += "<ul style='display:none'><li><a href='http://open.denglu.cc/transfer/tencent?appid=" + resultData + "'><img src='/Areas/SNS/Themes/M1/Content/images/tengxun.jpg'></a></li><li><a href='http://open.denglu.cc/transfer/tencent?appid=" + resultData + "'>腾讯微博</a></li></ul>";
                html += "<ul><li><a href='/social/qq'> <img src='/Areas/SNS/Themes/M1/Content/images/qq.jpg'></a></li> <li><a href='/social/qq'>QQ登录</a></li></ul></div>";
                $.jBox(html, { title: "登录", submit: submitLogin, width: 550, top: 300, height: 250, buttons: { '登录': true} });
                $(".jbox-button").hide();
                islogin = false;
                return false;
            } else {
                islogin = true;
                return true;

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });

    return islogin;
};
var CheckUserState4UserType = function () {
    var islogin;
    $.ajax({
        url: $Maticsoft.BasePath + "User/CheckUserState4UserType",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData == "Yes") {
                islogin = true;
                return true;
            } else if (resultData == "Yes4AA") {
                $.jBox.tip('管理员不能创建专辑, 请您更换普通帐号再试!');
                $(".jbox-button").hide();
                islogin = false;
                return false;
            } else {
                var html = "<div style='margin-left:90px;margin-top:20px;font-size: 16px;font-weight: bold;'>账号：<input type='text' style='height: 30px;width: 200px;' id='loginname' name='name' /><span style='display:none;color:red;font-size: 16px;' name='tipname' id='tipname'>请输入账号</span></div>";
                html += "<div style='margin-left:90px;margin-top:20px;font-size: 16px;font-weight: bold;'>密码：<input type='password' style='height: 30px;width: 200px;' id='loginpwd' name='pwd' /><span style='display:none;color:red;font-size: 16px;' name='tippwd' name='tippwd'>请输入密码</span></div>";
                html += "<div class='login'><ul><li><a href='#' id='ajaxlogin'> <img src='/Areas/SNS/Themes/M1/Content/images/login.jpg'></a></li></ul>";
                html += "<ul><li><a href='/Account/Register'><img src='/Areas/SNS/Themes/M1/Content/images/reg.jpg'></a></li></ul>";
                html += "<ul><li><a href='/social/sina'><img src='/Areas/SNS/Themes/M1/Content/images/sin.jpg'></a></li><li><a href='/social/sina'>新浪微博</a></li></ul>";
                html += "<ul style='display:none'><li><a href='http://open.denglu.cc/transfer/tencent?appid=" + resultData + "'><img src='/Areas/SNS/Themes/M1/Content/images/tengxun.jpg'></a></li><li><a href='http://open.denglu.cc/transfer/tencent?appid=" + resultData + "'>腾讯微博</a></li></ul>";
                html += "<ul><li><a href='/social/qq'> <img src='/Areas/SNS/Themes/M1/Content/images/qq.jpg'></a></li> <li><a href='/social/qq'>QQ登录</a></li></ul></div>";
                $.jBox(html, { title: "登录", submit: submitLogin, width: 550, top: 300, height: 250, buttons: { '登录': true} });
                $(".jbox-button").hide();
                islogin = false;
                return false;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });

    return islogin;
};
var CheckUserLogin = function () {
    var islogin;
    $.ajax({
        url: $Maticsoft.BasePath + "User/CheckUserState",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData != "Yes") {
                islogin = false;
                return false;
            } else {
                islogin = true;
                return true;

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        }
    });
    return islogin;
};


$("#ajaxlogin").die("click").live("click", function () { $(".jbox-button").click(); });
var submitLogin = function (v, h, f) {
    if (f.name == '') {
        $("#tipname").show();
        $("#tippwd").hide();
        return false;
    }
    if (f.pwd == '') {
        $("#tippwd").show();
        $("#tipname").hide();
        return false;
    }
    $.ajax({
        type: "POST",
        dataType: "text",
        url: $Maticsoft.BasePath + "Account/AjaxLogin",
        async: false,
        data: { UserName: $("#loginname").val(), UserPwd: $("#loginpwd").val() },
        success: function (data) {
            if (parseInt(data) == -1) {
                $.jBox.tip('该功能已被管理员关闭，如有疑问，请联系网站管理员', 'success');
                return false;
            }
            if (parseInt(data.split("|")[0]) > 0) {
                AjaxLoginGetUserInfo(data.split("|")[1]);
                return true;
            }
            else {
                $.jBox.tip('用户名或者密码不正确，请重试', 'success');

            }
        }
    });
    return true;
};
var AjaxLoginGetUserInfo = function (pointer) {
    $.ajax({
        type: "get",
        dataType: "text",
        url: $Maticsoft.BasePath + "Partial/Header",
        data: { pointer: pointer },
        success: function (data) {
            $(".headers").html(data);
        }
    });
};