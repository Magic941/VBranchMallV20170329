/*
检查是否包含引号（单引号和双引号）
参数说明：
ao_Object			对象
as_DisplayStr		控件对应的中文名称 例如 用户名
abln_AllowNothing 	是否允许为空（true,false）
ai_Length			长度(不传此参数表示不检查长度)
返回值：检查成功返回true，否则返回false
*/
function f_StringCheck(ao_Object, as_DisplayStr, abln_AllowNothing, ai_Length) {
    var ls_Value;
    ls_Value = f_PubStrTrim(ao_Object.value);
    if (abln_AllowNothing == false) {
        if (ls_Value == "") {
            alert(as_DisplayStr + "不能为空！");
            ao_Object.focus();
            return false;
        }
    }
    if (ls_Value.indexOf("'") > -1 || ls_Value.indexOf('"') > -1) {
        alert(as_DisplayStr + "不能有单双引号！");
        ao_Object.focus();
        return false;
    }
    if (typeof (ai_Length) != "undefind") {
        if (f_GetStringLen(ls_Value) > ai_Length) {
            alert(as_DisplayStr + "的最大长度为" + ai_Length + "(汉字" + ai_Length / 2 + ")");
            ao_Object.focus();
            return false;
        }
    }
    return true;
}
function f_GetStringLen(as_Value) {
    var li_Loop, li_Len;
    li_Len = 0;
    for (li_Loop = 0; li_Loop < as_Value.length; li_Loop++) {
        if (as_Value.charCodeAt(li_Loop) > 255) li_Len = li_Len + 2;
        else li_Len++;
    }
    return li_Len;
}
function f_NameCheck(ao_Object, ai_Length) {
    var li_Loop, li_Len;
    li_Len = 0;
    for (li_Loop = 0; li_Loop < ao_Object.value.length; li_Loop++) {
        if (ao_Object.value.charCodeAt(li_Loop) > 255) li_Len = li_Len + 1;
        else {
            alert("姓名必须输入中文");
            ao_Object.focus();
            return false;
        }
    }
    if (li_Len > ai_Length) {
        alert("姓名长度必须在" + ai_Length + "个字以内");
        ao_Object.focus();
        return false;
    }
    if (li_Len == 1) {
        alert("姓名长度必须在1个字以上");
        ao_Object.focus();
        return false;
    }
    return true;
}
function f_AddressCheck(ao_Object) {
    var li_Loop, li_Len;
    li_Len = 0;
    for (li_Loop = 0; li_Loop < ao_Object.value.length; li_Loop++) {
        if (ao_Object.value.charCodeAt(li_Loop) > 255) li_Len = li_Len + 1;
        else {
            alert("省份及城市必须输入中文");
            ao_Object.focus();
            return false;
        }
    }
    return true;
}
function CellphoneCheck(Cellphone) {
    var myreg = /^(((13[0-9]{1})|159|(15[0-9]{1})|(18[0-9]{1})|(14[0-9]{1}))+\d{8})$/;
    if (!myreg.test(Cellphone)) {
        return false;
    }
}

/*
function f_NumCheck(ao_Object,as_DisplayStr, al_MaxNum)
{
var ll_Check;
ll_Check = ao_Object.value;
if (ll_Check == "")
{
ao_Object.value = 0 ;
}
if(isNaN(ll_Check)||ll_Check < 0)
{
alert(as_DisplayStr + "应输入正整数！");
ao_Object.focus();
return false;
}
var numobj = new Number(ll_Check);
if(numobj > parseInt(al_MaxNum,10))
{
alert(as_DisplayStr + "超过最大允许值" + al_MaxNum + "！");
ao_Object.focus();
return false;
}
ao_Object.value = Math.round(ll_Check);
return true;
}
*/
function f_NumCheck(ao_Object, as_DisplayStr, al_MaxNum, abln_PlusInd, abln_IntInd) {
    var ll_Check;
    ll_Check = ao_Object.value;
    if (ll_Check == "") {
        ao_Object.value = 0;
    }
    if (isNaN(ll_Check)) {
        alert(as_DisplayStr + "应输入半角数字！");
        ao_Object.focus();
        return false;
    }
    if (abln_PlusInd == true) {
        if (ll_Check < 0) {
            alert(as_DisplayStr + "应输入正数！");
            ao_Object.focus();
            return false;
        }
    }
    var numobj = new Number(ll_Check);
    if (numobj > parseInt(al_MaxNum, 10)) {
        alert(as_DisplayStr + "超过最大允许值" + al_MaxNum + "！");
        ao_Object.focus();
        return false;
    }
    if (abln_IntInd == true) {
        ao_Object.value = Math.round(ll_Check);
    }
    return true;
}

function f_NumBlankCheck(ao_Object, as_DisplayStr, al_MaxNum, abln_PlusInd, abln_IntInd, abln_IsAllowBlank) {
    var ll_Check;
    ll_Check = ao_Object.value;
    if (ll_Check == "") {
        if (!abln_IsAllowBlank) {
            alert("请填写" + as_DisplayStr + "！");
            ao_Object.focus();
            return false;
        } else return true;
    }

    if (isNaN(ll_Check)) {
        alert(as_DisplayStr + "应输入半角数字！");
        ao_Object.focus();
        return false;
    }
    if (abln_PlusInd == true) {
        if (ll_Check < 0) {
            alert(as_DisplayStr + "应输入正数！");
            ao_Object.focus();
            return false;
        }
    }
    var numobj = new Number(ll_Check);
    if (numobj > parseFloat(al_MaxNum, 10)) {
        alert(as_DisplayStr + "超过最大允许值" + al_MaxNum + "！");
        ao_Object.focus();
        return false;
    }
    if (abln_IntInd == true) {
        ao_Object.value = Math.round(ll_Check);
    }
    return true;
}

function f_QtyCheck(ao_Object, as_DisplayStr, al_MaxNum, abln_PlusInd, abln_IntInd, abln_IsAllowBlank) {
    var ll_Check;
    ll_Check = ao_Object.value;
    if (ll_Check == "") {
        if (!abln_IsAllowBlank) {
            alert("请填写" + as_DisplayStr + "！");
            ao_Object.focus();
            return false;
        } else return true;
    }

    if (isNaN(ll_Check)) {
        alert(as_DisplayStr + "应输入半角数字！");
        ao_Object.focus();
        return false;
    }

    if (abln_IntInd == true) {
        if (ll_Check.indexOf(".") > 0) {
            alert(as_DisplayStr + "应输入整数！");
            return false;
        }
    }
    if (abln_PlusInd == true) {
        if (ll_Check < 1) {
            alert(as_DisplayStr + "应大于等于1！");
            ao_Object.focus();
            return false;
        }
    }
    var numobj = new Number(ll_Check);
    if (numobj > parseInt(al_MaxNum, 10)) {
        alert("您购买数量超过允许范围！");
        ao_Object.focus();
        return false;
    }
    if (numobj > 32767) {
        alert(as_DisplayStr + "超过最大允许值" + 32767 + "！");
        ao_Object.focus();
        return false;
    }

    return true;
}

function f_QtyCheckAlert(ao_Object, as_DisplayStr, al_MaxNum, abln_PlusInd, abln_IntInd, abln_IsAllowBlank, abln_IsAlert) {
    var ll_Check;
    ll_Check = ao_Object.value;
    if (ll_Check == "") {
        if (!abln_IsAllowBlank) {
            if (abln_IsAlert) alert("请填写" + as_DisplayStr + "！");
            ao_Object.focus();
            return false;
        } else return true;
    }

    if (isNaN(ll_Check)) {
        if (abln_IsAlert) alert(as_DisplayStr + "应输入半角数字！");
        ao_Object.focus();
        return false;
    }

    if (abln_IntInd == true) {
        if (ll_Check.indexOf(".") > 0) {
            if (abln_IsAlert) alert(as_DisplayStr + "应输入整数！");
            return false;
        }
    }
    if (abln_PlusInd == true) {
        if (ll_Check < 1) {
            if (abln_IsAlert) alert(as_DisplayStr + "应大于等于1！");
            ao_Object.focus();
            return false;
        }
    }
    var numobj = new Number(ll_Check);
    if (numobj > parseInt(al_MaxNum, 10)) {
        if (abln_IsAlert) alert("您购买数量超过允许范围！");
        ao_Object.focus();
        return false;
    }
    if (numobj > 32767) {
        if (abln_IsAlert) alert(as_DisplayStr + "超过最大允许值" + 32767 + "！");
        ao_Object.focus();
        return false;
    }

    return true;
}

//
//
//EMail检查
//使用正则表达式
function f_CheckEMail(email) {

    if (email == null || email.length == 0) return true;
    if (email.indexOf(' ') >= 0) return false;
    //var reg = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/i;
    var reg = /([-])?\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/i;
    var result = email.match(reg);
    if (result == null) return false;
    if (result[0] != email) return false;
    return true;
}
//
//
//电话号码检查,普通家用电话号码
function f_CheckHomePhone(phone) {
    if (phone == null || phone.length == 0) return true;
    var reg = /(\(\d{3}\)|\d{3}-)?\d{6,8}/;
    var result = phone.match(reg);
    if (result == null) return false;
    return true;
}

//
//检查邮编
function f_CheckZip(zip) {
    if (zip == null || zip.length != 6) return false;
    var reg = /\d{6}/;
    var result = zip.match(reg);
    if (result == null) return false;
    return true;

}
function f_CheckIDCard(idCard) {
    if (idCard == null || ((idCard.length != 15) && (idCard.length != 18))) return false;
    var reg;
    if (idCard.length == 18) reg = /\d{6}(19\d{2}|200\d{1})[0-1]\d[0-3]\d{4}(\d|X|x)/;
    else reg = /\d{8}[0-1]\d[0-3]\d{4}/;

    var result = idCard.match(reg);
    if (result == null) return false;

    var dateString;

    if (idCard.length == 15) {
        dateString = '19' + idCard.substr(6, 6);
    } else {
        dateString = idCard.substr(6, 8);
    }
    if (!((dateString.substr(0, 2) == "19") || (dateString.substr(0, 2) == "20"))) {
        return false;
    }
    if (dateString > '2005') {
        return false;

    }

    return true;

}

function checkMobile(mobileNo) {
    var reg0 = /^13\d{9}$/;
    var reg1 = /^15\d{9}$/;
    var reg2 = /^18\d{9}$/;
    var reg3 = /^14\d{9}$/;
    var my = false;
    if (reg0.test(mobileNo)) my = true;
    if (reg1.test(mobileNo)) my = true;
    if (reg2.test(mobileNo)) my = true;
    if (reg3.test(mobileNo)) my = true;
    return my;
}

function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]);
    return null;
}
function setCookie(name, value) {
    var exp = new Date();
    document.cookie = name + "=" + value;
    //alert( document.cookie );
}
//modify by jean
function IsNullOrEmpty(v) {
    return !(typeof (v) === "string" && v.length != 0);
}
function Trim(v) {
    return v.replace(/^\s+|\s+$/g, "")
}