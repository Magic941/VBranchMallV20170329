/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  $time$  $username$    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  $time$  $username$    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

$(function () {
    $('#txtAmount').OnlyFloat();
    //单选按钮单击事件
    $('[name="radType"]').unbind().bind('click', function () {
        if ($(this).val() == "1") {
            $('.bank').css('display', 'block');
        } else {
            $('.bank').css('display', 'none');
        }
    });

    /*验证提现金额*/
    $("#txtAmount").focus(function () {
        $("#AmountTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写提现金额</p>");
    }).blur(function () {
        checkAmount();
    });
    /*验证提现金额*/


    /*验证卡号开始*/
    $("#txtBankCard").focus(function () {
        $("#BankCardTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写卡号</p>");
    }).blur(function () {
        checBankCard();
    });
    /*验证卡号结束*/


    /*验证开户行名称开始*/
    $("#txtBankName").focus(function () {
        $("#BankNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写开户行名称</p>");
    }).blur(function () {
        checkBankName();
    });
    /*验证开户行名称结束*/

    /*验证开户姓名开始*/
    $("#txtTrueName").focus(function () {
        $("#TrueNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写开户姓名</p>");
    }).blur(function () {
        checkTrueName();
    });
    /*验证开户姓名结束*/


});

// 验证提现金额
function checkAmount() {
    var i = 0;
    var AmountVal = $.trim($('#txtAmount').val());
    var balanceval = $('#Balance').val();
    if (AmountVal == "") {
        $("#AmountTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写提现金额！</p>");
        return false;
    } else {
        if (AmountVal.indexOf(";") > -1 || AmountVal.indexOf(",") > -1 || AmountVal.indexOf("'") > -1) {
            ShowFailTip('大神，请您手下留情！');
            $(this).val("");
            i++;
            if (i >= 3) {
                ShowFailTip('别玩了，这样有意思吗？');
            }
            return false;
        }
        if (parseFloat(AmountVal) > parseFloat(balanceval)) {
            $("#AmountTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>您的提现金额大于余额！</p>");
            return false;
        }
        $("#AmountTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        return true;
    }
}


// 验证帐号类型
function checktype() {
    var typevalue = $("[name='radType']:checked").val();
    if (!typevalue || parseInt(typevalue) <= 0) {
        $("#radTypeTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请选择帐号类型！</p>");
        return false;
    }
    $("#radTypeTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
    return true;
}

// 验证帐号
function checBankCard() {
    /*验证帐号开始*/
    var cardVal = $.trim($('#txtBankCard').val());
    if (cardVal == '') {
        $("#BankCardTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>帐号不能为空！</p>");
        return false;
    } else if (cardVal.length > 50) {
        $("#BankCardTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>帐号长度不能超过50个字符！</p>");
        return false;
    } else {
        $("#BankCardTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        return true;
    }
    /*验证帐号结束*/
}


// 验证开户行名称
function checkBankName() {
    var bankNameVal = $.trim($('#txtBankName').val());
    if (bankNameVal == '') {
        $("#BankNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写开户行名称！</p>");
        return false;
    }
    if (bankNameVal.length > 200) {
        $("#BankNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请控制在0-200字符内！</p>");
        return false;
    } else {
        $("#BankNameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        return true;
    }
}

// 验证用户姓名
function checkTrueName() {
    var trueNameVal = $.trim($('#txtTrueName').val());
    if (trueNameVal == '') {
        $("#TrueNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写开户姓名！</p>");
        return false;
    }
    if (trueNameVal.length > 50) {
        $("#TrueNameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请控制在0-50字符内！</p>");
        return false;
    }
    $("#TrueNameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
    return true;
}



 function gosubmit() {
     //var type = $.trim($("[name='radType']:checked").val());
     var type = 1;
    var errnum = 0;
    if (!checkAmount()) {
        errnum++;
    }
    //if (!checktype()) {
    //    errnum++;
    //}
    if (!checBankCard()) {
        errnum++;
    }
    if (type == "1") {//当帐号类型为 "银行卡"时需要填写以下信息
        if (!checkBankName()) {
            errnum++;
        }
        if (!checkTrueName()) {
            errnum++;
        }
    }
    if (!(errnum == 0 ? true : false)) {
        return false;
    } else {
        var amount = $.trim($("#txtAmount").val());
        var bankCard = $.trim($("#txtBankCard").val());
        var bankName = $.trim($("#txtBankName").val());
        var trueName = $.trim($("#txtTrueName").val());
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/AjaxDraw",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                Amount: amount,
                BankCard: bankCard,
                Type: type,
                BankName: bankName,
                TrueName: trueName
            },
            success: function(result) {
                switch (result) {
                case "low":
                    ShowFailTip("余额不足！");
                    break;
                case "no":
                    ShowFailTip("提交失败，请稍后再试！");
                    break;
                case "ok":
                    ShowSuccessTip("提交成功！");
                    setTimeout(function() {
                        window.location.href = $Maticsoft.BasePath + "UserCenter/Balance";
                    }, 2000);
                    break;
                default:
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    break;
                }
        },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    }

};
