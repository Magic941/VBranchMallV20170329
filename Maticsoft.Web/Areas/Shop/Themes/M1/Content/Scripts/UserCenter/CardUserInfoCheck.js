/*
好邻卡注册信息表单验证
edit by wangzhongyu 2014/7/7
**/



$(function () {
    //姓名
    $("#Name").focus(function () {
        //CheckPrompt("nameTip", "请填写姓名");
        CheckPrompt("Name", "请填写姓名");
    }).blur(function () {
        checkname();
    });

    //身份证号码
    $("#CardId").focus(function () {
        //CheckPrompt("cardTip", "请填写身份证号码");
        CheckPrompt("CardId", "请填写身份证号码");
    }).blur(function () {
        checkCardId();
    });

    //邮箱
    $("#Email").focus(function () {
        CheckPrompt("Email", "请填写邮箱");
    }).blur(function () {
        checkEmail();
    });

    //用户名
    $("#UserName").focus(function () {
        CheckPrompt("UserName","请填写用户名");
    }).blur(function () {
        checkUserName();
    });

    $("#S_Password").focus(function () {
        CheckPrompt("S_Password", "请填写密码");
    }).blur(function () {
        checkPassword();
    });
    $("#confirmPassword").focus(function () {
        CheckPrompt("confirmPassword", "请填写确认密码");
    }).blur(function () {
        confirmPassword();
    });

    //手机号
    $("#Moble").focus(function () {
        //CheckPrompt("mobleTip", "请填写手机号");
        CheckPrompt("Moble", "请填写手机号");
    }).blur(function () {
        checkMoble();
    });

    //地址
    $("#Address").focus(function () {
        //CheckPrompt("addressTip", "请填写详细地址");
        CheckPrompt("Address", "请填写详细地址");
    }).blur(function () {
        checkAddress();
    });

    //邮编
    $("#CodeNo").focus(function () {
        //CheckPrompt("codenoTip", "请填写邮编");
        CheckPrompt("CodeNo", "请填写邮编");
    }).blur(function () {
        checkCodeNO();
    });

    //职业
    $("#Job").focus(function () {
        //CheckPrompt("jobTip", "请填写职业");
        CheckPrompt("Job", "请填写职业");
    }).blur(function () {
        checkJob();
    });
})

function checkJob() {
    var job = $("#Job").val();
    if (job == "") {
        CheckFailed("Job", "职业不能为空！");
        return false;
    }
    else {
        CheckSuccess("Job");
        return true;
    }
}
function checkCodeNO() {
    var reg = /^[0-9]{6}$/;
    var codeno = $("#CodeNo").val();
    if (!reg.test(codeno)) {
        CheckFailed("CodeNo", "邮编不正确！");
        return false;
    }
    else {
        CheckSuccess("CodeNo");
        return true;
    }
}

function checkAddress() {
    var address = $("#Address").val();
    if (address == "") {
        CheckFailed("Address", "详细地址不能为空！");
        return false;
    }
    else {
        CheckSuccess("Address");
        return true;
    }
}

function checkUserName() {
    var result = true;
    var username = $("#UserName").val();
    if (!(/^[a-zA-Z0-9_]{3,100}$/).test(username)) {
        CheckFailed("UserName", "用户名必须为3-10数字、字母、下划线组合！");
        result = false;
    }
    else {
        $.ajax({
            url: '/Account/ExistsUserName',
            type:"post",
            async:false,
            data: { "UserName": username },
            success: function (data) {
                if (data == "True") {
                    CheckSuccess("UserName");
                    result = true;
                }
                else {
                    CheckFailed("UserName", "该用户名已经存在！");
                    result = false;
                }
            }
        })
        
    }
    return result;
}

function checkPassword(){
    var pwd = $("#S_Password").val();
    if (!(/^[a-zA-Z0-9]{6,30}$/).test(pwd)) {
        CheckFailed("S_Password", "密码6-30位，支持数字、字母！");
        return false;
    } else {
        CheckSuccess("S_Password");
        return true;
    }
}

function confirmPassword() {
    var pwd = $("#S_Password").val();
    var con_pwd = $("#confirmPassword").val();
    if (pwd != con_pwd) {
        CheckFailed("confirmPassword", "两次密码不一致！");
        return false;
    } else {
        CheckSuccess("confirmPassword");
        return true;
    }
}
function checkMoble() {
    var reg = /(^1[3|5|8][0-9]{9}$)/;
    var moble = $("#Moble").val();
    if (!reg.test(moble)) {
        CheckFailed("Moble", "手机号错误！");
        return false;
    }
    else {
        CheckSuccess("Moble");
        return true;
    }
}

function checkEmail() {
    var reg = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var Email = $("#Email").val();
    if (!reg.test(Email)) {
        CheckFailed("Email", "邮箱不能为空！");
        return false;
    }
    else {
        CheckSuccess("Email");
        return true;
    }
}


function checkname() {
    var reg = /^\s*$/g;
    var cardNo = $.trim($("#Name").val());

    if (cardNo == "" || reg.test(cardNo)) {
        CheckFailed("Name", "姓名不能为空！");
        return false;
    }
    else {
        CheckSuccess("Name");
        return true;
    }
}

function checkCardId() {
    var num = $("#CardId").val();
    num = num.toUpperCase();
    //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。
    if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))) {
        alert('输入的身份证号长度不对，或者号码不符合规定！\n15位号码应全为数字，18位号码末位可以为数字或X。');
        return false;
    }
    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
    //下面分别分析出生日期和校验位
    var len, re;
    len = num.length;
    if (len == 15) {
        re = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
        var arrSplit = num.match(re);

        //检查生日日期是否正确
        var dtmBirth = new Date('19' + arrSplit[2] + '/' + arrSplit[3] + '/' + arrSplit[4]);
        var bGoodDay;
        bGoodDay = (dtmBirth.getYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
        if (!bGoodDay) {
            alert('输入的身份证号里出生日期不对！');
            return false;
        }
        else {
            //将15位身份证转成18位
            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
            var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
            var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
            var nTemp = 0, i;
            num = num.substr(0, 6) + '19' + num.substr(6, num.length - 6);
            for (i = 0; i < 17; i++) {
                nTemp += num.substr(i, 1) * arrInt[i];
            }
            num += arrCh[nTemp % 11];

            var birthday = num.substring(6, 14);
            birthday = birthday.substring(0, 4) + "-" + birthday.substring(4, 6) + "-" + birthday.substring(6);
            $("#txtBirthday").val(birthday);
            return true;
        }
    }
    if (len == 18) {
        re = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
        var arrSplit = num.match(re);

        //检查生日日期是否正确
        var dtmBirth = new Date(arrSplit[2] + "/" + arrSplit[3] + "/" + arrSplit[4]);
        var bGoodDay;
        bGoodDay = (dtmBirth.getFullYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
        if (!bGoodDay) {
            alert('输入的身份证号里出生日期不对！');
            return false;
        }
        else {
            //检验18位身份证的校验码是否正确。
            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
            var valnum;
            var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
            var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
            var nTemp = 0, i;
            for (i = 0; i < 17; i++) {
                nTemp += num.substr(i, 1) * arrInt[i];
            }
            valnum = arrCh[nTemp % 11];
            if (valnum != num.substr(17, 1)) {
                alert('18位身份证的校验码不正确！');
                return false;
            }
            var birthday = num.substring(6, 14);
            birthday = birthday.substring(0, 4) + "-" + birthday.substring(4, 6) + "-" + birthday.substring(6);
            $("#txtBirthday").val(birthday);
            return true;
        }
    }
    return false;
}


function CheckSuccess(id) {
    
    $("#" + id).removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked");
    $("#" + id).innerHTML = "<i class=\"msg-ico\"></i><p>&nbsp;</p>";
    $("#" + id).css({ "border": "solid 1px #e7e5e6", "color": "black" }).removeAttr("title");
}

function CheckFailed(id, msg) {
    $("#" + id).css({ "border": "solid 1px red", "color": "#e7e5e6" }).attr("title", msg);
    //$("#" + id).removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>" + msg + "</p>");
}

function CheckPrompt(id, msg) {
    //$("#" + id).removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html(" <i class=\"msg-ico\"></i><p>" + msg + "</p>");
    //$("#" + id).css("border","solid 1px red").attr("title",msg);
    if ($("#" + id).attr("title")) {
        $("#" + id).css({ "border": "solid 1px #e7e5e6", "color": "black" });
    }
}

function NormalCheck() {
    var result = true;
    if (!checkname()) {
        result = false;
    }
    if (!checkAddress()) {
        result = false;
    }
    if (!checkCardId()) {
        result = false;
    }
    if (!checkCodeNO()) {
        result = false;
    }
    if (!checkEmail()) {
        result = false;
    }
    if (!checkJob()) {
        result = false;
    }
    if (!checkMoble()) {
        result = false;
    }

    return result;
}

function SimpleCheck() {
    var result = true;
    if (!checkname()) {
        result = false;
    }
    if (!checkMoble()) {
        result = false;
    }
    if (!checkJob()) {
        result = false;
    }
    if (!checkUserName()) {
        result = false;
    }
    if (!checkPassword()) {
        result = false;
    }
    if (!confirmPassword()) {
        result = false;
    }
    return result;
}