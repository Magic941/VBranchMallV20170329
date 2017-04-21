


$(document).ready(function () {
    $("#presellButton").click(function () {
        $("body").AjaxLogin({
            success: function () {
                showPreSale();
            },
            content: "预售登记商品需要登录后才能操作"
        });
        return false;
    });
});

var presaleDialog;
var UserInfo = {
    IsGet: false,
    Email: "",
    CellPhone: ""
};

function showPreSale() {
    showsuccessed = false;
    if (!UserInfo.IsGet) {
        $.ajax({
            type: "get",
            url: "http://productutil.maticsoft.com/ArrivingNotify.aspx?Operation=GetUserInfo&callback=?",
            dataType: "jsonp",
            success: function (data) {
                if (data
					&& data.IsSuccess
					&& data.ReturnValue) {
                    UserInfo.IsGet = true;
                    UserInfo.Email = data.ReturnValue.Email;
                    UserInfo.CellPhone = data.ReturnValue.Mobile;
                }
                showPreSaleDialog();
            }
        });
    }
    else {
        showPreSaleDialog();
    }
    timeoutShow();
}
var showloadCount = 0;
var showsuccessed = false;
function timeoutShow() {
    showloadCount++;
    if (showloadCount <= 4 && showsuccessed == false) {
        setTimeout("timeoutShow()", 1000);
        return;
    }
    if (showloadCount > 0 && showsuccessed == false) {
        showPreSaleDialog();
    }
}

function showPreSaleDialog() {
    $("#presaleCellphoneTip").removeClass().html("");
    $("#presaleEmailTip").removeClass().html("");
    cphoneStatus = false;
    mailStatus = false;
    showsuccessed = true;
    if (typeof (presaleDialog) == "undefined" || presaleDialog == null) {
        presaleDialog = new Dialog({
            title: '预售登记',
            footer: '',
            width: 570,
            body: '<div class="presell-form">' +
                        '<p class="mb10">亲：</p>' +
                        '<p class="mb10">商品到货后，您希望如何通知您？(手机和Email可任填一项）</p>' +
                        '<div class="f-box">' +
	                        '<label class="f-label" for="input-error1">手机：</label>' +
	                        '<div class="f-input">' +
  		                        '<input type="text" class="f-txt f-txt-b" id="presaleCellphone">' +
	                        '</div>' +
	                        '<div class="f-msg">' +
    	                        '<div id="presaleCellphoneTip">' +

		                        '</div>' +
	                        '</div>' +
                        '</div>' +
                        '<div class="f-box">' +
	                        '<label class="f-label" for="input-error1">Email：</label>' +
	                        '<div class="f-input">' +
  		                        '<input type="text" class="f-txt f-txt-b" id="presaleEmail">' +
	                        '</div>' +
	                        '<div class="f-msg">' +
    	                        '<div id="presaleEmailTip">' +

		                        '</div>' +
	                        '</div>' +
                        '</div>' +
                        '<div class="f-act">' +
	                        '<div id="presaleTip"></div>' +
	                        '<div class="mt20"><a id="btnPreSaleSubmit" class="btn mr15 btn-important"><span>确<em class="s1em"></em>定</span></a></div>' +
                        '</div>' +
                    '</div>'
        });
    }
    if (presaleDialog.dialog)
        presaleDialog.center();
    presaleDialog.open();
    if (UserInfo.IsGet) {//如果取到数据
        $("#presaleCellphone").val(UserInfo.CellPhone);
        $("#presaleEmail").val(UserInfo.Email);
    }

    $("#presaleEmail").focus(function () {
        $("#presaleEmailTip").removeClass().addClass("msg msg-info")
            .html("<i class=\"msg-ico\"></i><p>请填写Email</p>");
    }).blur(function () {
        CheckEmail($(this));
    })
    //    .keypress(function(event) {
    //        if (event.which == 13) {
    //            $("#btnPreSaleSubmit").trigger("click");
    //        }
    //    });

    $("#presaleCellphone").focus(function () {
        $("#presaleCellphoneTip").removeClass().addClass("msg msg-info")
            .html("<i class=\"msg-ico\"></i><p>请填写手机</p>");
    }).blur(function () {
        CheckCellPhone($(this));
    })
    //    .keypress(function(event) {
    //        if (event.which == 13) {
    //            $("#btnPreSaleSubmit").trigger("click");
    //        }
    //    });

    $("#btnPreSaleSubmit").click(submitPreSale)
}

var premsgDialog;
function submitPreSale() {
    CheckCellPhone($("#presaleCellphone"));
    CheckEmail($("#presaleEmail"));
    if (!cphoneStatus || !mailStatus) {
        return;
    }
    var cellphone = $("#presaleCellphone").val();
    var email = $("#presaleEmail").val();
    if (cellphone == "" && email == "") {
        $("#presaleTip").removeClass().addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>手机和Email需任填一项</p>");
        return;
    }
    UserInfo.Email = email;
    UserInfo.CellPhone = cellphone;

    var itemid = $("#presellButton").attr("itemid");
    var productid = $("#presellButton").attr("productid");
    email = encodeURIComponent(email);
    if (presaleDialog)
        presaleDialog.close();
    $.getJSON("http://productutil.maticsoft.com/ArrivingNotify.aspx?Operation=AddNotify&styleId=" + styleEntityListJson[0].StyleId + "&itemId=" + itemid + "&productId=" + productid + "&mobile=" + cellphone + "&email=" + email + "&flag1=1&callback=?", function (res) {
        if (res && res.IsSuccess) {
            if (typeof (premsgDialog) == "undefined" || premsgDialog == null) {
                premsgDialog = new Dialog({
                    title: '预售登记成功',
                    body: '<div class="pt10">' +
                            '<div class="pt30 pb20">' +
                                '<p class="order-sucess orderw1 f14">您的预售登记提交成功</p>' +
                                '<p class="mt20 tc"><a class="btn btn-important" href="javascript:premsgDialog.close();"><span>确<em class="s1em"></em>定</span></a></p>' +
                            '</div>' +
                            '<div class="order-warn tc">' +
                                '<p>当您收到到货通知后，请第一时间来购买，货源紧张哟。<a class="a2" target="_blank" href="http://static.maticsoft.com/help/help_presell.shtml">查看帮助&gt;&gt;</a></p>' + '</div>' +
                        '</div>',
                    width: 470,
                    footer: ''
                });
            }
            if (premsgDialog.dialog)
                premsgDialog.center();
            premsgDialog.open();
        }
        else {
            alert("提交预售登记失败")
        }
    });
}

var mailStatus = false;
function CheckEmail(obj) {
    var regs = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    var emailval = obj.val();
    if (emailval != "") {
        if (emailval.indexOf("@m18comm.com") > 0) {
            $("#presaleEmailTip").removeClass().addClass("msg msg-err")
                    .html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址</p>");
            mailStatus = false;
            return;
        }
        if (!regs.test(emailval)) {
            $("#presaleEmailTip").removeClass().addClass("msg msg-err")
                    .html("<i class=\"msg-ico\"></i><p>请填写有效的Email地址</p>");
            mailStatus = false;
        }
        else {
            $("#presaleEmailTip").removeClass().addClass("msg msg-ok msg-naked")
				        .html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
            mailStatus = true;
            $("#presaleTip").removeClass().html("");
        }
    }
    else {
        $("#presaleEmailTip").removeClass().html("");
        mailStatus = true;
    }
}

var cphoneStatus = false;
function CheckCellPhone(obj) {
    var regs = /^(13|14|15|18)\d{9}$/;
    var cphone = obj.val();
    if (cphone != "") {
        if (!regs.test(cphone)) {
            $("#presaleCellphoneTip").removeClass().addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>手机号码格式不正确</p>");
            cphoneStatus = false;
        }
        else {
            $("#presaleCellphoneTip").removeClass().addClass("msg msg-ok msg-naked")
				        .html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
            cphoneStatus = true;
            $("#presaleTip").removeClass().html("");
        }
    }
    else {
        $("#presaleCellphoneTip").removeClass().html("");
        cphoneStatus = true;
    }

}