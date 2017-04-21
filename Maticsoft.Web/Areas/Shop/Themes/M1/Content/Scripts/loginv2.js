//记录用户的状态，当用户已经登陆过不就不用再次异步检查用户的登陆状态了
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
//联合登录
var SinaLogin = {
    CallBack: null,
    //登录成功回调方法
    Login: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/sina/logintransfer.aspx?from=m18&logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("body").CloseLogin();
    },
    AlipayLogin: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/Alipay/alipayTransferRequest.aspx?logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("body").CloseLogin();
    },
    KaixinLogin: function() {
        var strReturnUrl = "http://" + window.location.host + "/logintransfer.html?t=callback";
        window.open("http://login.maticsoft.com/Kaixin/KaixinTransferRequest.aspx?logintype=" + $(this).attr("logintype") + "&returnurl=" + strReturnUrl);
        $("body").CloseLogin();
    }
};

(function($) {
    var login = {
        validateOnce: {
            Email: "",
            Exists: false
        },
        userNameLoginStatus: false,
        passwordLoginStatus: false,
        verifyCodeLoginStatus: false,

        userNameRegStatus: false,
        pwdRegStatus: false,
        vpwdRegStatus: false,
        verifyCodeRegStatus: false,
        checkReadRegStatus: true,

        lastRegEmail: "",

        verifyCodeLogin: "",
        verifyCodeReg: "",

        loginDialog: null,

        regpwd: null,
        focusmsg: "",
        errormsg: "",

        //检查用户是否登陆
        CheckLogin: function(options, obj) {
            $.getJSON("http://login.maticsoft.com/Service/LoginV2.ashx?Method=CheckLogin&jsoncallback=?",
            function(data) {
                if (!data.error) {
                    UserLoginStatus.isLogin = true;
                    UserLoginStatus.membership = data.MemberShipId UserLoginStatus.contactId = data.ContactId;
                    options.success();
                } else {
                    options.DefaultLoginId = data.DefaultLoginId;
                    login.errormsg = data.errormsg;
                    login.focusmsg = data.focusmsg;
                    login.regpwd = eval("(" + data.regpwd + ")");
                    $("#btnLogin").unbind("click");
                    $("#loginChangeImage").unbind("click");
                    $("#regChangeImage").unbind("click");
                    $("#UserNameLogin").unbind("keypress");
                    $("#PasswordLogin").unbind("keypress");
                    $("#vcodeLogin").unbind("keypress");
                    $("#UserNameReg").unbind("keypress");
                    $("#pwdReg").unbind("keypress");
                    $("#vpwdReg").unbind("keypress");
                    $("#vcodeReg").unbind("keypress");
                    $("#btnReg").unbind("click");
                    login.ShowLoginForm(options, obj);
                }
            });
        },
        //显示登陆层
        ShowLoginForm: function(options, obj) {
            if (!login.loginDialog) {
                login.loginDialog = new Dialog({
                    title: '登录/注册',
                    body: options.loginFormHTML
                });
            }
            if (login.loginDialog.dialog) login.loginDialog.center();
            login.loginDialog.open();

            login.ClearLoginForm(options.DefaultLoginId);
            //更换登录验证码
            $("#loginChangeImage").click(function() {
                login.ChangeImage("login");
            });
            //更换注册验证码
            $("#regChangeImage").click(function() {
                login.ChangeImage("reg");
            });
            //登录控件事件
            $("#UserNameLogin").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnLogin").click();
                    return false;
                }
            }).focus(function() {
                if (this.value == "Email或手机号码") this.value = "";
                $("#userNameLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写Email地址或手机号码</p>");
                $("#userNameOkLoginTip").html("");
            }).blur(function() {
                login.CheckLoginName();
            });

            $("#PasswordLogin").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnLogin").click();
                    return false;
                }
            }).focus(function() {
                $("#pwdLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写密码</p>");
                $("#pwdOkLoginTip").html("");
            }).blur(function() {
                login.CheckLoginPwd();
            });

            $("#vcodeLogin").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnLogin").click();
                    return false;
                }
            }).focus(function() {
                $("#vcodeLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写四位验证码</p>");
                $("#vcodeOkLoginTip").html("");
            }).blur(function() {
                login.CheckLoginCode();
            });

            //注册控件事件
            $("#UserNameReg").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnReg").trigger("click");
                    return false;
                }
            }).focus(function() {
                $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址作为登录用户名</p>");
                $("#UserNameOkRegTip").html("");
            }).blur(function() {
                login.CheckRegName();
            });

            $("#pwdReg").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnReg").trigger("click");
                    return false;
                }
            }).focus(function() {
                $("#pwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>" + login.focusmsg + "</p>");
                $("#pwdOkRegTip").html("");
            }).blur(function() {
                login.CheckRegPwd();
            });

            $("#vpwdReg").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnReg").trigger("click");
                    return false;
                }
            }).focus(function() {
                $("#vpwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请再次填写密码，两次输入必须一致</p>");
                login.vpwdRegStatus = false;
                $("#vpwdOkRegTip").html("");
            }).blur(function() {
                login.CheckRegVpwd();
            });

            $("#vcodeReg").keypress(function(event) {
                if (event.which == 13) {
                    $("#btnReg").trigger("click");
                    return false;
                }
            }).focus(function() {
                $("#vcodeRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写四位验证码</p>");
                $("#vcodeOkRegTip").html("");
            }).blur(function() {
                login.CheckRegCode();
            });

            $("#agreement").click(function() {
                login.CheckAgreement();
            });
            // 用记点击登陆操作
            $("#btnLogin").bind("click",
            function() {
                var username = $("#UserNameLogin").val(); //取用户名
                var pwd = $("#PasswordLogin").val(); //取用户密码
                var vcode = $("#vcodeLogin").val();
                if (login.ValidateLoginData()) { //验证登陆信息
                    login.LoginNow(options, username, pwd, vcode);
                }
                return false;
            });

            $("#btnReg").bind("click",
            function() {
                var username = $("#UserNameReg").val(); //取用户名
                var pwd = $("#pwdReg").val(); //取用户密码
                var vcode = $("#vcodeReg").val();
                if (login.ValidateRegData()) {
                    login.RegNow(options, username, pwd, vcode);
                }
                return false;
            });

            //            $("#pwdReg").passStrength({
            //		        badPass: "f-pwd-w",
            //		        goodPass: "f-pwd-m",
            //		        strongPass: "f-pwd-s",
            //		        minLength: "6",//password min-length
            //		        maxLength: "30",//password max-length
            //		        tipsloc: 1 //tips position
            //	        });
            if ($.trim(options.content) != "") {
                $("#loginContent").html(options.content);
            } else {
                $("#loginContent").html("请登录!");
            }
            login.GetVerifyCode("login");
            login.GetVerifyCode("reg");
            if (options.DefaultLoginId != "") {
                $("#UserNameLogin").val(options.DefaultLoginId);
            } else {
                $("#UserNameLogin").val("Email或手机号码");
            }
            new TabLogin('#pop-tab', {
                indexChanged: function(obj) {
                    if (obj.tabIndex == 1) {
                        login.ClearRegForm();
                        var usernamelogin = $("#UserNameLogin").val();
                    } else {
                        login.ClearLoginForm(options.DefaultLoginId);
                    }
                }
            });
            $(".i-weibo").click(SinaLogin.Login);
            $(".i-kaixin").click(SinaLogin.KaixinLogin);
            $(".i-alipay").click(SinaLogin.AlipayLogin);
            SinaLogin.CallBack = options.success;
        },
        ClearLoginForm: function(DefaultLoginId) {
            if (DefaultLoginId != "") {
                $("#UserNameLogin").val(DefaultLoginId);
            } else {
                $("#UserNameLogin").val("Email或手机号码");
            }
            $("#userNameLoginTip").removeClass().html("");
            $("#userNameOkLoginTip").html("");

            $("#PasswordLogin").val("");
            $("#pwdOkLoginTip").html("");
            $("#pwdLoginTip").removeClass().html("");

            $("#vcodeLogin").val("");
            $("#vcodeLoginTip").removeClass().html("");
            $("#vcodeOkLoginTip").html("");
        },
        ClearRegForm: function() {
            $("#UserNameReg").val("");
            $("#UserNameRegTip").removeClass().html("");
            $("#UserNameOkRegTip").html("");

            $("#pwdReg").val("");
            $("#pwdOkRegTip").html("");
            $("#pwdRegTip").removeClass().html("");

            $("#vpwdReg").val("");
            $("#vpwdRegTip").removeClass().html("");
            $("#vpwdOkRegTip").html("");

            $("#vcodeReg").val("");
            $("#vcodeRegTip").removeClass().html("");
            $("#vcodeOkRegTip").html("");

            //$("#agreement").get(0).checked = true;
        },
        //关闭登陆层
        CloseLoginForm: function() {
            if (login.loginDialog) {
                login.loginDialog.close();
            }
        },
        //更换验证码
        ChangeImage: function(type) {
            login.GetVerifyCode(type);
        },
        //获取验证码
        GetVerifyCode: function(type) {
            $.getJSON("http://login.maticsoft.com/Service/LoginV2.ashx?Method=FloatVerifyCode&jsoncallback=?",
            function(data) {
                if (type == "login") {
                    login.verifyCodeLogin = data.code;
                    $("#loginVerifyCodeImg").attr("src", data.url + "?guid=" + data.code);
                } else {
                    login.verifyCodeReg = data.code;
                    $("#regVerifyCodeImg").attr("src", data.url + "?guid=" + data.code);
                }
            });
        },
        //开始注册
        RegNow: function(options, userName, pwd, vcode) {
            $.getJSON("http://login.maticsoft.com/Service/LoginV2.ashx?Method=floatRegister&userName=" + userName + "&pwd=" + encodeURIComponent(pwd) + "&verifyCode=" + vcode + "&oldGuid=" + login.verifyCodeReg + "&jsoncallback=?",
            function(data) {
                $("#vcodeReg").val("");
                $("#vcodeOkRegTip").html("");
                if (data.Msg == 6) {
                    login.CloseLoginForm(); //注册并登陆成功，关闭登陆显示层
                    //跨域写用户会话信息
                    UserLoginStatus.isLogin = true;
                    UserLoginStatus.membership = data.MemberShipId;
                    UserLoginStatus.contactId = data.ContactId;

                    $("body").append("<img src='" + data.Url + "' border='0' height='0' width='0' />");
                    window.setTimeout(function() {
                        options.success();
                    },
                    500);
                } else if (data.Msg == 1) {
                    login.verifyCodeReg = data.Guid;
                    $("#regVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#vcodeRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>验证码错误，请重新填写</p>");
                } else if (data.Msg == 4) {
                    login.verifyCodeReg = data.Guid;
                    $("#regVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#vcodeRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>注册失败!</p>");

                } else if (data.Msg == 3 || data.Msg == 2) {
                    login.verifyCodeReg = data.Guid;
                    $("#regVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请输入正确的电子邮件地址</p>");
                    $("#UserNameOkRegTip").html("");
                    $("#vcodeRegTip").removeClass("").html("");

                } else if (data.Msg == 5) {
                    login.verifyCodeReg = data.Guid;
                    $("#regVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#pwdRegTip").removeClass().html("");
                    $("#vpwdRegTip").removeClass().html("");
                    $("#vcodeRegTip").removeClass("").html("");

                    $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该Email已被注册，请使用其他Email地址注册</p>");
                    $("#UserNameOkRegTip").html("");

                } else if (data.Msg == 7) {
                    login.verifyCodeReg = data.Guid;
                    $("#regVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#vcodeRegTip").removeClass("").html("");
                    $("#pwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>" + login.errormsg + "</p>");
                }
            })
        },
        //开始登陆
        LoginNow: function(options, userName, pwd, vcode) {
            $.getJSON("http://login.maticsoft.com/Service/LoginV2.ashx?Method=floatLogin&userName=" + userName + "&pwd=" + encodeURIComponent(pwd) + "&vcode=" + vcode + "&oldGuid=" + login.verifyCodeLogin + "&jsoncallback=?",
            function(data) {
                $("#vcodeLogin").val("");
                $("#pwdOkLoginTip").html("");
                $("#vcodeOkLoginTip").html("");

                if (data.Msg == 1) {
                    login.CloseLoginForm(); //登陆成功了，关闭登陆显示层
                    //跨域写用户会话信息
                    UserLoginStatus.isLogin = true;
                    UserLoginStatus.membership = data.MemberShipId;
                    UserLoginStatus.contactId = data.ContactId;

                    $("body").append("<img src='" + data.Url + "' border='0' height='0' width='0' />");
                    window.setTimeout(function() {
                        options.success();
                    },
                    500);
                } else if (data.Msg == 2) {
                    login.verifyCodeLogin = data.Guid;
                    $("#loginVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#vcodeLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>验证码错误，请重新填写</p>");

                    $("#pwdLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").removeClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                } else if (data.Msg == 3) {
                    login.verifyCodeLogin = data.Guid;
                    $("#loginVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#userNameLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>用户名不存在，请重新填写</p>");
                    $("#userNameOkLoginTip").html("");

                    $("#vcodeLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").removeClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");

                    $("#pwdLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").removeClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                } else {
                    login.verifyCodeLogin = data.Guid;
                    $("#loginVerifyCodeImg").attr("src", data.GuidUrl + "?guid=" + data.Guid);

                    $("#pwdLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>密码与用户名不匹配，请重新填写</p>");

                    $("#vcodeLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").removeClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                }
            });
        },
        //登陆验证
        ValidateLoginData: function() {
            login.CheckLoginName();
            login.CheckLoginPwd();
            login.CheckLoginCode();
            if (login.userNameLoginStatus && login.passwordLoginStatus && login.verifyCodeLoginStatus) return true;
            return false;
        },
        //注册验证
        ValidateRegData: function() {
            login.CheckRegName();
            login.CheckRegPwd();
            login.CheckRegVpwd();
            login.CheckAgreement();
            login.CheckRegCode();
            if (login.userNameRegStatus && login.pwdRegStatus && login.vpwdRegStatus && login.verifyCodeRegStatus && login.checkReadRegStatus) return true;
            return false;
        },
        CheckAgreement: function() {
            if ($("#agreement").attr("checked") == true) {
                $("#vcodeRegTip").removeClass().html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                login.checkReadRegStatus = true;
            } else {
                $("#vcodeRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请先阅读并同意《动软用户服务协议》</p>");
                login.checkReadRegStatus = false;
            }
        },
        CheckRegPwd: function() {
            var pwd = $("#pwdReg").val();
            if (pwd.length == 0) {
                $("#pwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写密码</p>");
                $("#pwdOkRegTip").html("");
                login.pwdRegStatus = false;
                return;
            }
            if (!login.regpwd.test(pwd)) {
                $("#pwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>" + login.errormsg + "</p>");
                $("#pwdOkRegTip").html("");
                login.pwdRegStatus = false;
            } else {
                login.pwdRegStatus = true;
                $("#pwdOkRegTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>") $("#pwdRegTip").removeClass().html("");
            }
        },
        CheckRegVpwd: function() {
            var vpwd = $("#vpwdReg").val();
            if (vpwd != "") {
                if (vpwd != $("#pwdReg").val()) {
                    $("#vpwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>两次填写的不一致，请重新填写</p>");
                    login.vpwdRegStatus = false;
                    $("#vpwdOkRegTip").html("");
                } else {
                    login.vpwdRegStatus = true;
                    $("#vpwdRegTip").removeClass().html("");
                    $("#vpwdOkRegTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>");
                }
            } else {
                $("#vpwdRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请再次填写密码，两次输入必须一致</p>");
                login.vpwdRegStatus = false;
                $("#vpwdOkRegTip").html("");
            }
        },
        CheckRegCode: function() {
            var vcode = $("#vcodeReg").val();
            if (vcode.length == 4) {
                login.verifyCodeRegStatus = true;
                $("#vcodeRegTip").removeClass().html("");
                $("#vcodeOkRegTip").html("");
            } else {
                login.verifyCodeRegStatus = false;
                $("#vcodeRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写四位验证码</p>");
                $("#vcodeOkRegTip").html("");
            }
        },
        CheckRegName: function() {
            var emailval = $("#UserNameReg").val();

            var regs = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;

            if (emailval != "") {
                if (emailval.indexOf("@m18comm.com") > 0) {
                    $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址,不能使用@m18comm.com结尾的邮箱</p>");
                    login.userNameRegStatus = false;
                    $("#UserNameOkRegTip").html("");
                    return;
                }
                if (!regs.test(emailval)) {
                    $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址</p>");
                    login.userNameRegStatus = false;
                    $("#UserNameOkRegTip").html("");
                } else {
                    if (login.validateOnce.Email == emailval) {
                        if (login.validateOnce.Exists == true) {
                            $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该Email已被注册，请使用其他Email地址注册</a></p>");
                            $("#UserNameOkRegTip").html("");
                            login.userNameRegStatus = false;
                        } else {
                            $("#UserNameRegTip").removeClass().html("");
                            $("#UserNameOkRegTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>") login.userNameRegStatus = true;
                        }
                        return;
                    }
                    login.validateOnce.Email = emailval;
                    $.getJSON("http://login.maticsoft.com/Service/LoginV2.ashx?Method=floatCheckEmail&userName=" + emailval + "&jsoncallback=?",
                    function(data) {
                        if (data.code == "false") {
                            $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该Email已被注册，请使用其他Email地址注册</a></p>");
                            login.userNameRegStatus = false;
                            $("#UserNameOkRegTip").html("");

                            login.validateOnce.Exists = true;
                        } else {
                            $("#UserNameRegTip").removeClass().html("");
                            $("#UserNameOkRegTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>");
                            login.userNameRegStatus = true;

                            login.validateOnce.Exists = false;
                        }
                    });
                }
            } else {
                $("#UserNameRegTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址作为登录用户名</p>");
                login.userNameRegStatus = false;
                $("#UserNameOkRegTip").html("");
            }
        },
        //验证登录名
        CheckLoginName: function() {
            var username = $("#UserNameLogin");
            if (username.val() == "" || username.val() == "Email或手机号码") {
                $("#userNameLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写Email地址或手机号码</p>");
                $("#userNameOkLoginTip").html("");
                login.userNameLoginStatus = false;
            } else {
                $("#userNameLoginTip").removeClass().html("");
                $("#userNameOkLoginTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>");
                login.userNameLoginStatus = true;
            }
        },
        //验证登录密码
        CheckLoginPwd: function() {
            var pwd = $("#PasswordLogin");
            if (pwd.val() == "") {
                $("#pwdLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写密码</p>");
                $("#pwdOkLoginTip").html("");
                login.passwordLoginStatus = false;
            } else {
                login.passwordLoginStatus = true;
                $("#pwdOkLoginTip").html("<div class='msg msg-ok msg-naked'><i class='msg-ico'></i><p>&nbsp;</p></div>");
                $("#pwdLoginTip").removeClass().html("");
            }
        },
        //验证登录验证码
        CheckLoginCode: function() {
            var vcode = $("#vcodeLogin").val();
            if (vcode == "" || vcode.length != 4) {
                $("#vcodeLoginTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写四位验证码</p>");
                $("#vcodeOkLoginTip").html("");
                login.verifyCodeLoginStatus = false;
            } else {
                login.verifyCodeLoginStatus = true;
                $("#vcodeLoginTip").removeClass().html("");
                $("#vcodeOkLoginTip").html("");
            }
        }
    };
    $.fn.AjaxLogin = function(options) {
        var options = $.extend({},
        $.fn.AjaxLogin.defaults, options);
        var obj = $(this); //此对象已无意义
        login.CheckLogin(options, obj);
    };
    $.fn.CloseLogin = function() {
        login.CloseLoginForm();
    }
    $.fn.AjaxLogin.defaults = {
        //默认登录Id
        DefaultLoginId: "",
        //标题信息
        title: "",
        //提示内容
        content: "",
        loginFormHTML: "" + "	<div class=\"login-reg-pop\">" + "		<div class=\"mb5\">" + "			<div class=\"msg msg-info msg-naked\"><i class=\"msg-ico\"></i><p id=\"loginContent\"></p></div>" + "		</div>" + "		<div class=\"pop-tab\" id=\"pop-tab\">" + "			<div class=\"pop-tab-tit\">" + "				<ul class=\"J_tab_nav\">" + "					<li class=\"cur\">登&nbsp;&nbsp;录</li>" + "					<li>注&nbsp;&nbsp;册</li>" + "				</ul>" + "			</div>" + "			<div class=\"pop-tab-box login-pop J_tab_panel\">" + "				<form class=\"nform form\">" + "					<fieldset>" + "						<legend>登录</legend>" + "						<div class=\"f-box\">" + "							<label class=\"f-label\">用户名：</label>" + "							<div class=\"f-input\">" + "								<input type=\"text\" class=\"f-txt-b f-txt\" id=\"UserNameLogin\" name=\"xlInput\" value=\"\">" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"userNameLoginTip\">" + "								</div>" + "							</div>" + "                           <div id=\"userNameOkLoginTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "						</div>" + "						<div class=\"f-box\">" + "							<label class=\"f-label\">密&nbsp;&nbsp;&nbsp;码：</label>" + "							<div class=\"f-input\">" + "								<input type=\"password\" class=\"f-txt-b f-txt\" id=\"PasswordLogin\" name=\"xlInput\">" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"pwdLoginTip\">" + "								</div>" + "							</div>" + "                           <div id=\"pwdOkLoginTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "						</div>" + "						<div class=\"f-box\">" + "							<label class=\"f-label\">验证码：</label>" + "							<div class=\"f-input\">" + "								<input type=\"text\" maxlength=\"4\" class=\"f-txt-s f-txt\" id=\"vcodeLogin\" name=\"xlInput\">" + "								<div class=\"f-vcode\">" + "									<img src=\"http://verifycode.maticsoft.com/getimg.aspx?guid=2E269C90AAA14BCCA685D86EDFC4BEBC\" id=\"loginVerifyCodeImg\">" + "									<a href=\"javascript:void(0);\" class=\"imgchange c6\" id=\"loginChangeImage\">换一张</a>" + "								</div>" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"vcodeLoginTip\">" + "								</div>" + "							</div>" + "                           <div id=\"vcodeOkLoginTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "						</div>" + "						<div class=\"login-btn\">" + "							<a class=\"btn btn-important-large fb\" id=\"btnLogin\" href=\"#32\"><span>登<em class=\"s1em\"></em>录</span></a>" + "							<a class=\"ml10 c3\" href=\"http://login.maticsoft.com/contact/contactgetpwd.aspx\">忘记密码？</a>" + "						</div>" + "                       <div class=\"login-other cf\">" + "							<span class=\"c9\">其它登录方式：</span>" + "							<ul class=\"cf\">" + "								<li logintype='layer' class=\"i-weibo\"><a href=\"javascript:void(0);\">新浪微博</a></li>" + "								<li logintype='layer' class=\"i-kaixin\"><a href=\"javascript:void(0);\">开心网</a></li>" + "								<li logintype='layer' class=\"i-alipay\"><a href=\"javascript:void(0);\">支付宝快捷登录</a></li>" + "							</ul>" + "						</div>" + "					</fieldset>" + "				</form>" + "			</div>" + "			<div class=\"pop-tab-box reg-pop J_tab_panel none\">" + "				<form class=\"nform form\">" + "					<fieldset>" + "						<legend>注&nbsp;&nbsp;册</legend>" + "						<div class=\"f-box pop-username\" >" + "							<label class=\"f-label\">Email地址：</label>" + "							<div class=\"f-input\">" + "								<input type=\"text\" maxlength=\"40\" class=\"f-txt-b f-txt\" id=\"UserNameReg\" name=\"xlInput\">" + "							</div>" + "							<div id=\"UserNameOkRegTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"UserNameRegTip\">" + "								</div>" + "							</div>" + "						</div>" + "						<div class=\"f-box pop-password\">" + "							<label class=\"f-label\">设置密码：</label>" + "							<div class=\"f-input\">" + "								<input type=\"password\" class=\"f-txt-b f-txt\" id=\"pwdReg\" name=\"xlInput\">" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"pwdRegTip\">" + "								</div>" + "							</div>" + "							<div id=\"pwdOkRegTip\" class=\"f-msg f-msg-ok\">" +

        "							</div>" + "						</div>" + "						<div class=\"f-box\">" + "							<label class=\"f-label\">确认密码：</label>" + "							<div class=\"f-input\">" + "								<input type=\"password\" class=\"f-txt-b f-txt\" id=\"vpwdReg\" name=\"xlInput\">" + "							</div>" + "							<div id=\"vpwdOkRegTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"vpwdRegTip\">" + "								</div>" + "							</div>" + "						</div>" + "						<div class=\"f-box\">" + "							<label class=\"f-label\">验证码：</label>" + "							<div class=\"f-input\">" + "								<input type=\"text\" maxlength=\"4\" class=\"f-txt-s f-txt\" id=\"vcodeReg\" name=\"xlInput\">" + "								<div class=\"f-vcode\">" + "									<img src=\"http://verifycode.maticsoft.com/getimg.aspx?guid=2E269C90AAA14BCCA685D86EDFC4BEBC\" id=\"regVerifyCodeImg\">" + "									<a href=\"javascript:void(0);\" class=\"imgchange c6\" id=\"regChangeImage\">换一张</a>" + "								</div>" + "							</div>" + "							<div class=\"f-msg\">" + "								<div id=\"vcodeRegTip\">" + "								</div>" + "							</div>" + "							<div id=\"vcodeOkRegTip\" class=\"f-msg f-msg-ok\">" + "							</div>" + "						</div>" + "						<div class=\"agreement\"><label><input type=\"checkbox\" id=\"agreement\" checked=\"checked\">我已阅读并同意<a href=\"hHelp/Agreement.html\" target=\"_blank\" class=\"c6\">《动软用户服务协议》</a></label></div>" + "						<div class=\"login-btn\">" + "							<a href=\"javascript:void(0)\" id=\"btnReg\" class=\"btn btn-important-large fb\"><span>提交注册信息</span></a>" + "						</div>" + "					</fieldset>" + "				</form>" + "			</div>" + "		</div>" + "	</div>",
        success: function() {},
        error: function() {}
    };
})(jQuery);

function TabLogin(container, options, callback) {
    this.container = $(container);
    this.handle = $(container + ' .J_tab_nav li');
    this.panel = $(container + ' .J_tab_panel');
    this.count = this.handle.length;
    this.timer = null;
    this.eTime = null;
    this.tabIndex = options.index;
    this.options = $.extend({
        auto: false,
        delay: 4,
        event: 'click',
        index: 1,
        indexChanged: function() {}
    },
    options);
    this.init();
    if (callback) {
        callback.call(this);
    }
}
TabLogin.prototype = {
    init: function() {
        var that = this,
        count = this.count,
        op = this.options,
        auto = !!op.auto;
        this.handle.bind(op.event,
        function() {
            that._trigger(this);
        });
        if (op.index === 'r') {
            op.index = this._random(count)
        }
        this._show(op.index);
        if (auto) {
            this._auto();
            this.container.hover(function() {
                that._stop();
            },
            function() {
                that._auto();
            });
        }
    },
    _random: function(max) {
        return parseInt(Math.random() * max + 1);
    },
    _trigger: function(o) {
        var index, op = this.options,
        handle = this.handle;
        if (op.index === (handle.index(o) + 1)) {
            return;
        }
        index = op.index = handle.index(o) + 1;
        this.tabIndex = index;
        this._show(index);
        if (op.indexChanged) {
            op.indexChanged(this);
        }
    },
    _show: function(i) {
        this.handle.removeClass('cur').eq(i - 1).addClass('cur');
        this.panel.addClass('none').eq(i - 1).removeClass('none');
    },
    _auto: function() {
        var that = this,
        op = that.options;
        this.timer = setTimeout(function() {
            op.index = op.index < that.count ? ++op.index: 1;
            that._show(op.index);
            that._auto();
        },
        op.delay * 1000);
    },
    _stop: function() {
        clearTimeout(this.timer);
    }
};