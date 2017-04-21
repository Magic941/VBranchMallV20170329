
//判断是否含有禁用词
function ContainsDisWords(desc) {
    var isContain = false;
    $.ajax({
        url: "/Partial/ContainsDisWords",
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
    var dialogOpts = {
        title: "登录",
        width: 400,
        modal: true,
        resizable: false,
        buttons: {
            "确定": function () {
                submitAjaxLogin();
            },
            "取消": function () {
                //  $(this).dialog("close"); //关闭层
                $("#divAjaxLogin").dialog("close");
            }
        }
    };
    var islogin;
    $.ajax({
        url: "/Account/AjaxIsLogin",
        type: 'post',
        dataType: 'text',
        async: false,
        success: function (resultData) {
            if (resultData != "True") {
                $("#divAjaxLogin").dialog(dialogOpts);  
                //dialog层中项的设置
                islogin = false;
                return false;
            } else {
                islogin = true;
                return true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    return islogin;
};

function submitAjaxLogin() {
    var userName = $('#txtEmail').val();
    var pwd = $('#txtPwd').val();
    var str = '';
    var regStr = $('#hfRegisterToggle').val(); //注册方式
    if (regStr == "Phone") {
        str = '手机号码';
    } else {
        str = '邮箱地址';
    }
    if (userName == '') {
        ShowFailTip("请输入" + str+"!");
        return false;
    }
    if (regStr == "Phone") {//手机登录
        var regs = /^(1(([35][0-9])|(47)|[8][0126789]))\d{8}$/;
        if (!regs.test(userName)) {
            ShowFailTip("请填写有效的手机号码");
            return false;
        } 
    } else {//邮箱登录
        var regsEmail = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if (!regsEmail.test(userName)) {
            ShowFailTip("请填写有效的Email地址");
            return false;
        } 
    }
   
    if (pwd == '') {
        ShowFailTip("请输入密码！");
        return false;
    }

    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/Account/AjaxLogin",
        async: false,
        data: { UserName: userName, UserPwd: pwd },
        success: function (data) {
            if (parseInt(data) == -1) {
                ShowFailTip('该功能已被管理员关闭，如有疑问，请联系网站管理员');
                return false;
            } else if (data== "NotActivity") {
                ShowFailTip('您的账户已被冻结，如有疑问，请联系网站管理员');
                return false;
            }
            if (parseInt(data.split("|")[0]) > 0) {
                $("#divAjaxLogin").dialog("close");
                $('#hd-login').load('/Partial/Login');
                return true;
            }
            else {
                ShowFailTip('用户名或者密码不正确，请重试');
            }
        }
    });
}
var CheckUserState4UserType = function () {
    var islogin;
    $.ajax({
        url: "/User/CheckUserState4UserType",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        async: false,
        success: function (resultData) {
            if (resultData == "Yes") {
                islogin = true;
                return true;
            } else if (resultData == "Yes4AA") {
                $.jBox.tip('管理员不能操作, 请您更换普通帐号再试!');
                $(".jbox-button").hide();
                islogin = false;
                return false;
            } else {

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
        url: "/User/CheckUserState",
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
        url: "/Account/AjaxLogin",
        async: false,
        data: { UserName: $("#loginname").val(), UserPwd: $("#loginpwd").val() },
        success: function (data) {
            if (parseInt(data) == -1) {
                $.jBox.tip('该功能已被管理员关闭，如有疑问，请联系网站管理员', 'success');
                return false;
            }
            if (parseInt(data.split("|")[0]) > 0) {
                //AjaxLoginGetUserInfo(data.split("|")[1]);
                //$("#divAjaxLogin").dialog("close");
                $('#hd-login').load('/Partial/Login');
                return true;
            }
            else {
                $.jBox.tip('用户名或者密码不正确，请重试', 'success');

            }
        }
    });
    return true;
};


$(function () {
    //商品咨询
    $(".btnAddConsult").die("click").live("click", function () {
        if (CheckUserState()) {
            var dialogOpts = {
                title: "商品咨询",
                width: 400,
                modal: true,
                buttons: {
                    "确定": function () {
                        submitAjaxAddConsult();
                    }
                }
            };
            $("#divAjaxConsults").dialog(dialogOpts);
        }
    });
    //商品评论
    $(".btnAddComment").die("click").live("click", function () {
        if (CheckUserState()) {
            var dialogOpts = {
                title: "商品评论",
                width: 400,
                modal: true,
                buttons: {
                    "确定": function () {
                        submitAjaxAddComment();
                    }
                    //                        "取消": function () {
                    //                            //  $(this).dialog("close"); //关闭层
                    //                            $("#divAjaxComments").dialog("close");
                    //                        }

                }
            };
            $("#divAjaxComments").dialog(dialogOpts);
        }
    });
});

function submitAjaxAddConsult() {
    var productId = $("#hdProductId").val();
    var content = $("#txtConsult").val();
    if (content == "") {
        ShowFailTip('请填写咨询内容！');
        return;
    }
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/UserCenter/AjaxAddConsult",
        data: { ProductId: productId, Content: content },
        success: function (data) {
            if (data == "True") {
                ShowSuccessTip('咨询成功！请等待管理员回复');
                $("#divAjaxConsults").dialog("close");
                $(".ui-dialog").empty();
            } else {
                ShowFailTip('服务器繁忙，请稍候再试！');
            }
        }
    });
}

function submitAjaxAddComment() {
    var productId = $("#hdProductId").val();
    var productName = $("#hdProductName").val();
    var content = $("#txtComment").val();
    if (content == "") {
        ShowFailTip('请填写评论内容！');
        return;
    }
    $.ajax({
        type: "POST",
        dataType: "text",
        url: "/UserCenter/AjaxAddComment",
        data: { ProductId: productId, Content: content, ProductName: productName },
        success: function (data) {
            if (data == "True") {
                ShowSuccessTip('评论成功!');
                $("#divAjaxComments").dialog("close");
                $(".ui-dialog").empty();
            } else {
                ShowFailTip('服务器繁忙，请稍候再试！');
            }
        }
    });
}

$(function () {
    $("#msgTab li").each(function (index, data) {
        $(this).bind("click", function () {
            $(this).addClass("chose");
            $("#msgTab li").each(function (_i, _data) { if (_i != index) { $(this).removeClass("chose"); } });
            $(".W-msg-cont").each(function (_i, _data) { if (_i == index) {$(this).show(); } else { $(this).hide(); } });
        });
    });
})
