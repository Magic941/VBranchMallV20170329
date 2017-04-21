
/**
* ApplyAgnetHao.js
* 功 能：修改用户信息
* 文件名称： ApplyAgnetHao.js
*
/*错误警告提示信息*/
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\">{0}</span></p>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\">{0}</span></p>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";


$(function () {

    /*验证用户真是姓名开始*/
    $("#txtTrueName").focus(function () {
        $("#truenameTip").removeClass("red").addClass("tipClass").html("填写真实姓名");
    }).blur(function () {
        //checktruename();
    });
    /*验证用户结束*/


    /*验证邮箱开始*/
    $("#txtEmail").focus(function () {
        $("#email").removeClass("red").addClass("tipClass").html("填写邮箱");
    }).blur(function () {
        checkemail();
    });
    /*验证邮箱结束*/

    /*验证手机号码开始*/
    $("#txtPhone").blur(function () {
        checkphone();
    });
    /*验证手机号码结束*/

    /*验证固定电话开始*/
    $("#txtTelPhone").blur(function () {
        checktelphone();
    });
    /*验证固定电话结束*/

    /*验证所在地开始*/
    $("#txtAddress").blur(function () {
        checkaddress();
    });
    /*验证所在地结束*/



    // 验证用户邮箱
    function checkemail() {
        /*验证用户邮箱开始*/
        var emailVal = $.trim($('#txtEmail').val());
        if (emailVal == '') {
            $("#email").removeClass("tipClass").addClass("red").html("邮箱不能为空！");
            return false;
        } else if (emailVal.length > 100) {
            $("#email").removeClass("tipClass").addClass("red").html("邮箱长度不能超过100个字符！");
            return false;
        } else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(emailVal)) {
            $("#email").removeClass("tipClass").addClass("red").html("请填写有效的Email地址");
            return false;
        } else {
            $("#email").removeClass("red").addClass("tipClass").html("&nbsp;");
            return true;
        }
        /*验证用户邮箱结束*/
    }
    // 验证用户手机号码
    function checkphone() {
        var phoneVal = $.trim($('#txtPhone').val());
        if (phoneVal != '') {
            if (!/^(1[3,4,5,8,7]{1}\d{9})$/.test(phoneVal)) {
                $("#phone").removeClass("tipClass").addClass("red").html("请输入正确的手机号！");
                return false;
            } else {
                $("#phone").removeClass("red").addClass("tipClass").html("&nbsp;");
                return true;
            }
        }
        return true;
    }

    // 验证用户固定电话
    function checktelphone() {
        var telphoneVal = $.trim($('#txtTelPhone').val());
        if (telphoneVal != '') {
            //if (!/^(0[0-9]{2,3}-)?([2-9][0-9]{6,7})+(-[0-9]{1,4})?$/.test(telphoneVal)) {
            var tel = /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0135-9]\d{8}$)|(18[267]\d{8}$)/;
            if (!tel.test(telphoneVal)) {
                $("#telphone").removeClass("tipClass").addClass("red").html("请输入正确的电话号码！");
                return false;
            } else {
                $("#telphone").removeClass("red").addClass("tipClass").html("&nbsp;");
                return true;
            }
        }
        return true;
    }

    // 验证用户所在地
    function checkaddress() {
        var addressVal = $.trim($('#txtAddress').val());
        if (addressVal != '') {
            if (addressVal.length > 100) {
                $("#address").removeClass("tipClass").addClass("red").html("请控制在0-300字符内！");
                return false;
            }
            $("#address").removeClass("red").addClass("tipClass").html("&nbsp;");
        }
        return true;
    }


    // 验证
    function checkremark() {
        var remarkVal = $.trim($('#txtRemark').val());
        if (remarkVal != '') {
            if (remarkVal.length > 100) {
                $("#remark").removeClass("tipClass").addClass("red").html("请控制在0~100字符！");
                return false;
            }
            $("#remark").removeClass("red").addClass("tipClass").html("&nbsp;");
        }

        return true;
    }
    //验证身份证号
    //function checkcardid(){
    //    var cardidVal = $.trim($('#txtCardId').val());
    //    if(cardidVal !='')
    //    {
    //        if(cardidVal.length !=15 && cardidVal.length !=18){
    //            $("#CardId").removeClass("tipClass").addClass("red").html("请输入正确的身份证号码");
    //            return false;
    //        }
    //        $("#CardId").removeClass("red").addClass("tipClass").html("&nbsp;");
    //    }
    //    return true;
    //}

    $('#btnUpdateForm').die().live('click', function () {

        var errnum = 0;
        //if (!checktruename()) {
        //    errnum++;
        //}

        //检查email
        //if (!checkemail()) {
        //    errnum++;
        //}
        //if (!checksex()) {
        //    errnum++;
        //}
        if (!checkphone()) {
            errnum++;
        }

        if (!checktelphone()) {
            errnum++;
        }
        if (!checkaddress()) {
            errnum++;
        }
        //if(!checkcardid())
        //{
        //    errnum++;
        //}
        if (!checkremark()) {
            errnum++;
        }
    
        if (!(errnum == 0 ? true : false)) {
            return false;
        } else {
            var truename = $.trim($("#txtTrueName").val());
            var email = $.trim($("#txtEmail").val());
            var phone = $.trim($("#txtPhone").val());
            var telphone = $.trim($("#txtTelPhone").val());
            var regionid = $.trim($("#hfSelectedNode").val());
            var remark = $.trim($("#txtRemark").val());
            var address = $.trim($("#txtAddress").val());
            var sex = -1;
            var cardid = $.trim($("#txtCardId").val());
            if ($("#radman").attr("checked") == "checked") {
                sex = 1;
            }
            if ($("#radwoman").attr("checked") == "checked") {
                sex = 0;
            }
            $.ajax({
                url: $Maticsoft.BasePath + "UserCenter/UpdateForm",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post",
                    TrueName: truename,
                    Email: email,
                    Phone: phone,
                    TelPhone: telphone,
                    RegionId:regionid,
                    Address: address,
                    Remark: remark,
                    Sex: sex,
                    CardId:cardid
                },
                success: function (JsonData) {
                    //debugger
                    switch (JsonData) {
                        case "true":
                            ShowSuccessTip("申请成功,请等待官方审核! 3秒后自动跳转!");
                            window.setTimeout("window.location='/MShop/u/ApplyAgent'",3000);
                            break;
                        case "false":
                            ShowFailTip("申请失败！");
                            break;
                        case "isnotnull":
                            ShowFailTip("对不起，您已经申请过个人微店了，请耐心等待工作人员审核!");
                            break;
                        default:
                            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                            break;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                }

            });
        }

    });
})