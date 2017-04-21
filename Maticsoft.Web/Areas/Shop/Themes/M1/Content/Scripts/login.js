//记录用户的状态，当用户已经登陆过不就不用再次异步检查用户的登陆状态了
//var LinkCss = document.createElement("link");
//LinkCss.type = "text/css";
//LinkCss.rel = "stylesheet";
//LinkCss.href = "http://img.maticsoft.com/web/c/comm/login.css ";
//$("head").append(LinkCss);
window.UserLoginStatus = {
    //是否登陆
    isLogin: false,
    //会员级别
    membership: "",
    //是否审被邀请
    isInvite: "",
    //会员编号
    contactId: ""
};
//新浪联合登录
var SinaLogin = {
    CallBack: null,
    //登录成功回调方法
    Login: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/sina/logintransfer.aspx?from=m18&logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("#CloseLogin").click();
    },
    AlipayLogin: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/Alipay/alipayTransferRequest.aspx?logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("#CloseLogin").click();
    },
    KaixinLogin: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/Kaixin/KaixinTransferRequest.aspx?logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("#CloseLogin").click();
    }
};

(function($) {
    var screenCalculate = {
        // 计算当前窗口的宽度 //
        pageWidth: function() {
            return window.innerWidth != null ? window.innerWidth: document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth: document.body != null ? document.body.clientWidth: null;
        },
        // 计算当前窗口的高度 //
        pageHeight: function() {
            return window.innerHeight != null ? window.innerHeight: document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight: document.body != null ? document.body.clientHeight: null;
        },
        // 计算当前窗口的上边滚动条//
        topPosition: function() {
            return typeof window.pageYOffset != 'undefined' ? window.pageYOffset: document.documentElement && document.documentElement.scrollTop ? document.documentElement.scrollTop: document.body.scrollTop ? document.body.scrollTop: 0;
        },
        // 计算当前窗口的左边滚动条//
        leftPosition: function() {
            return typeof window.pageXOffset != 'undefined' ? window.pageXOffset: document.documentElement && document.documentElement.scrollLeft ? document.documentElement.scrollLeft: document.body.scrollLeft ? document.body.scrollLeft: 0;
        }
    };
    var login = {
        //检查用户是否登陆
        CheckLogin: function(options, obj) {
            $.getJSON("http://login.maticsoft.com/AjaxLoginForm.aspx?jsoncallback=?",
            function(data) {
                if (!data.error) {
                    UserLoginStatus.isLogin = true;
                    UserLoginStatus.membership = data.MemberShipId UserLoginStatus.contactId = data.ContactId;
                    options.success();
                } else {
                    login.ShowLoginForm(options, obj);
                }
            });
        },
        //显示登陆层
        ShowLoginForm: function(options, obj) {
            var loginDiv = $("<div id=\"login-pop\" ></div>");
            $(obj).append(loginDiv);
            //载入登陆页数据
            var pop = $("#login-pop");
            pop.html(options.loginFormHTML);
            var popmask = $('#login-pop').find(".mask");
            popmask.width($(document).width()).height($(document).height());
            pop.slideDown(300);

            $('.overlay').css({
                'marginTop': $(window).scrollTop() - 200
            });
            $('.overlaymain').css({
                'marginTop': $(window).scrollTop() - 190
            });
            //关闭登陆页
            $("#CloseLogin").bind("click",
            function() {
                login.CloseLoginForm();
                return false;
            });
            //用户注册后的返回页面设置
            $("#login-pop .handle a ").bind("click",
            function(e) {
                e.preventDefault();
                location.href = "http://login.maticsoft.com/UserRegister.aspx?ReturnUrl=" + escape(location.href);
                //				location.href = "http://login.maticsoft.com/ContactSimpleRegister.aspx?ReturnUrl=" + escape(location.href);
            });
            //回车事件
            $("#Password").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnLogin").trigger("click");
                    return false;
                }
            });
            //回车事件
            $("#UserName").keypress(function(event) {
                if (event.which == 13) {
                    $("#Password").get(0).focus();
                    return false;
                }
            });
            //窗口SIZE改变
            $(window).resize(function() {
                popmask.width($(document).width()).height($(document).height());
            });
            $("#UserName").focus(function() {
                $("#CellphoneTip").show();
            });
            //随窗口滚动
            //var overlaymain = $('#login-pop').find(".overlaymain");
            //var overlay = $('#login-pop').find(".overlay");
            //			$(window).scroll(function() {
            //				var _heightOverlaymain = overlaymain.height();
            //				var _heightOverlay = overlay.height();
            //				var height = screenCalculate.pageHeight();
            //				var top = screenCalculate.topPosition();
            //				log.debug("_heightOverlaymain:" + _heightOverlaymain + "height:" + height + "top:" + top);
            //				log.debug(top + (height / 2) - (_heightOverlaymain / 2));
            //				overlaymain.animate({ "top": top + (height / 2) }, 100);
            //				overlay.animate({ "top": top + (height / 2) }, 100);
            //			});
            $("select").hide();
            // 用记点击登陆操作
            $("#btnLogin").bind("click",
            function() {
                var UserToken = $("#UserName").val(); //取用户名
                var PasswordWord = $("#Password").val(); //取用户密码
                if (login.ValidateData(UserToken, PasswordWord)) { //验证登陆信息
                    login.LoginNow(options, UserToken, PasswordWord);
                }
                return false;
            });
            $(".m18login").click(SinaLogin.Login);
            $(".alipaylogin").click(SinaLogin.AlipayLogin);
            $(".kaixinlogin").click(SinaLogin.KaixinLogin);
            SinaLogin.CallBack = options.success;
            login.LoginTip(options.title, "", options.content);
        },
        //关闭登陆层
        CloseLoginForm: function() {
            $("select").show();
            var pop = $("#login-pop");
            pop.remove();
        },
        //开始登陆
        LoginNow: function(options, UserToken, PasswordWord) {
            $.getJSON("http://login.maticsoft.com/AjaxLoginForm.aspx?Uid=" + UserToken + "&Pwd=" + encodeURIComponent(PasswordWord) + "&jsoncallback=?",
            function(data) {
                if (!data.error) {
                    login.CloseLoginForm(); //登陆成功了，关闭登陆显示层
                    //跨域写用户会话信息
                    UserLoginStatus.isLogin = true;
                    UserLoginStatus.membership = data.MemberShipId;
                    UserLoginStatus.contactId = data.ContactId;
                    var transUrl = data.Url + "?q=" + data.Param;
                    $("body").append("<img src='" + transUrl + "' border='0' height='0' width='0' />");
                    window.setTimeout(function() {
                        options.success();
                    },
                    500);
                } else {
                    // 在登陆失败后提示相关的错误信息
                    login.LoginTip("", "用户名或密码不正确，请重新输入！", "");
                }
            });
        },
        //登陆验证
        ValidateData: function(UserToken, PasswordWord) {
            var reEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
            if (PasswordWord.length <= 0 || $.trim(UserToken).length <= 0) {
                login.LoginTip("", "用户名或密码不能为空，请重新输入！", "");
                return false;
            }
            return true;
        },
        //提示信息
        LoginTip: function(title, message, content) {
            //操作相关的提示信息
            if ($.trim(title) != "") $("#logTitle").text(title);
            if ($.trim(content) != "") {
                $("#loginContent").html(content).show();
            }
            if ($.trim(message) != "") $("#login_error").text(message);
        }
    };
    $.fn.AjaxLogin = function(options) {
        var options = $.extend({},
        $.fn.AjaxLogin.defaults, options);
        var obj = $(this);
        return this.each(function() {
            login.CheckLogin(options, obj);
        });
    };
    $.fn.AjaxLogin.defaults = {
        //标题信息
        title: "",
        //提示内容
        content: "",
        loginFormHTML: "<div class=\"mask\">" + "</div>" + "<div class=\"overlay\">" + "</div>" + "<div class=\"overlaymain\">" + "    <h2>" + "        会员登录</h2>" + "    <ul class=\"clew\" style=\"display: none;\" id=\"loginContent\">" + "    </ul>" + "    <form method=\"post\" class=\"clearfix\">" + "    <fieldset class=\"uname-length\">" + "        <legend>" + "            <p id=\"logTitle\">" + "            </p>" + "        </legend>" + "       <p class=\"hl mb5 zmt15\" id=\"CellphoneTip\" style=\"display:none;\">手机号如果已认证，可使用手机号登录</p>" + "        <div>" + "            <label for=\"username\">" + "                用户名/Email/手机：</label>" + "            <input type=\"text\" class=\"text\" id=\"UserName\" name=\"UserName\" />" + "        </div>" + "        <div>" + "            <label for=\"password\">" + "                密<span style=\"width:1em;display:inline-block;\"></span>码：</label>" + "            <input type=\"password\" class=\"text\" id=\"Password\" name=\"Password\" />" + "            <a href=\"/Contact/ContactGetPwdByMail.aspx\" target=\"_blank\">忘记密码？</a>" + "        </div>" + "    </fieldset>" + "    <div id=\"login_error\">" + "    </div>" + "    <div class=\"handle\">" + "        <input type=\"button\" id=\"btnLogin\" title=\"登录\" value=\"登录\" />" + "        <a href=\"http://login.maticsoft.com/UserRegister.aspx\" target=\"_blank\">快速注册</a>" + "    </div>" + "    </form>" + "    <div class='otherLogin pt5 mt10 tc'><a class='m18login'  logintype='layer' href='javascript:void(0);'><img src='http://img.maticsoft.com/web/i/mobile/blogbtn.gif' complete='complete' /></a><a id='Kaixin_login' logintype='layer' href='javascript:void(0);' class='ml5 kaixinlogin'><img src='http://img.maticsoft.com/web/i/mobile/kaixinbtn.gif' complete='complete' /></a><a id='Alipay_login' logintype='layer' href='javascript:void(0);' class='ml5 alipaylogin'><img src='http://img.maticsoft.com/web/i/mobile/alipaybtn.gif' complete='complete' /></a></div>" + "    <a href=\"#\" id=\"CloseLogin\" class=\"close\">关闭</a></div>",
        success: function() {},
        error: function() {}
    };
})(jQuery);