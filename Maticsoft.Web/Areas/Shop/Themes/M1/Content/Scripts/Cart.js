///<reference path="../lib/jquery-1.3.2.js"/>
///<reference path="../lib/jquery.json-1.3.min.js"/>
///<reference path="../app/comm/jquery.m18ext.js"/>
///<reference path="../../lib/jquery.string.1.0.js"/>
///<reference path="../../lib/jquery.blockUI.js"/>

function Cart(columnName, itemColumnName) {

    this.ColumnName = columnName;
    this.ItemAmount = 0;
    this.ItemCount = 0;
    this.GpAmount = 0;
    this.GpPoint = 0;
    this.Lines = new Array();
    this.KitLines = new Array();

    /*
    this.GetItem = RSDSWeb.Order.OrderEntryService.GetItem;
    this.AddKit = RSDSWeb.Order.OrderEntryService.AddKit;
    this.Add = RSDSWeb.Order.OrderEntryService.Add;
    this.Modify = RSDSWeb.Order.OrderEntryService.Modify;
    this.Cancel = RSDSWeb.Order.OrderEntryService.Cancel;
    this.GetPrice = RSDSWeb.Order.OrderEntryService.GetDiscountPrice;
    this.GetCarrierGroup = RSDSWeb.Order.OrderEntryService.GetCarrierGroup;

    this.PayService = RSDSWeb.Order.OrderEntryService.Pay
    this.CreditCardPayService = RSDSWeb.Order.OrderEntryService.CreditCardPay
    this.PayTypeChange = RSDSWeb.Order.OrderEntryService.GetPayType;
    this.DeletePay = RSDSWeb.Order.OrderEntryService.DeletePay;

    this.OrderItem = new OrderItem(this, itemColumnName);
    this.OrderPay = new OrderPay(this, payColumnName);
    */

}

Cart.prototype = new RSBase();

Cart.prototype.ErrorCallbackRSBase = Cart.prototype.ErrorCallback;

Cart.prototype.ErrorCallback = function (error, callBackContext, response) {
    callBackContext.Object.ErrorCallbackRSBase(error, callBackContext, response);
    //callBackContext.Element.value = "";
    document.location.reload();
}

Cart.GetElementById = function (controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}

Cart.prototype.GetElementById1 = function (controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}


Cart.prototype.Initial = function () {

}

Cart.prototype.GetCallbackContext = function (element) {
    return new CallbackContext(element, this);
}

Cart.prototype.Add = function (orderline) {
    this.Lines[this.Lines.length] = orderline
    //alert(this.Lines.length);
}


Cart.prototype.AddKit = function (orderKitline) {
    this.KitLines[this.KitLines.length] = orderKitline
}

Cart.prototype.GetLine = function (lineId) {

    for (var i = 0; i < this.Lines.length; i++) {
        if (this.Lines[i].LineId == lineId)
            return this.Lines[i];
    }
    alert("行号:" + lineId + "不存在!");
    return null;
}

Cart.prototype.ModifyQty1 = function (lineId, qtyText) {
    var orderLine = this.GetLine(lineId)
    orderLine.ModifyQty1();
    //myCart.GiftQtyChange();
}

Cart.prototype.ModifyQty = function (lineId, qtyChanged) {
    var orderLine = this.GetLine(lineId)
    var qty = orderLine.GetQty() + qtyChanged;
    if (qty <= 0) return;
    orderLine.ModifyQty(qty);
    //myCart.GiftQtyChange();
}

Cart.prototype.Delete = function (lineId, element) {
    M18Web.Order.CartService.Delete(lineId, this.DeleteCallback,
        this.ErrorCallback, new CallbackContext(element, this));
}


Cart.prototype.DeleteCallback = function (orderInfo, callBackContext) {
    callBackContext.Object.SetOrderInfo(orderInfo);
    callBackContext.Object.DeleteLine(callBackContext.Element.value);
}

Cart.prototype.DeleteLine = function (lineId) {
    var newOrderLines = new Array();
    for (var i = 0; i < this.Lines.length; i++) {
        if (this.Lines[i].LineId != lineId) {
            newOrderLines[newOrderLines.length] = this.Lines[i];
        }
    }
    this.Lines = newOrderLines;
    var rowElement = this.GetElementById("trLine" + lineId);
    rowElement.parentNode.removeChild(rowElement);

}

Cart.prototype.RefreshGift = function (overInfo, isLogin) {
    var tr = document.getElementById("trGiftOver");
    tr.style.display = overInfo.IsOver ? "block" : "none";
    var tGifttxt = '免费领取赠品';
    if (overInfo.IsOverType == 1 || overInfo.IsOverType == 5) {
        tGifttxt = '免费领取赠品';
    }
    if (overInfo.IsOverType == 2 || overInfo.IsOverType == 4) {
        tGifttxt = '低价换购赠品';
    }
    if (overInfo.IsOverType == 3) {
        tGifttxt = '获得赠品';
    }
    document.getElementById("SettleGiftErrorInfo").style.display = overInfo.IsOver ? "block" : "none";
    if (overInfo.AmountOver == 0) {
        document.getElementById("spanGiftErrorInfo").innerHTML = "您只需继续购物且满足<a href='/static/prom/09-3/promotion.html' class='hl' target='_blank'>赠品活动条件</a>，即可" + tGifttxt + "，<a href='OrderShopContinue.aspx' class='hl' target='_blank'>继续购物</a>";
        document.getElementById("SettleGiftErrorInfo").innerText = "您只需继续购物且满足赠品活动条件，即" + tGifttxt + "";
    }
    else {
        document.getElementById("spanGiftErrorInfo").innerHTML = "您只需继续购物且满足<a href='/static/prom/09-3/promotion.html' class='hl' target='_blank'>赠品活动条件</a>，即可" + tGifttxt + "，<a href='OrderShopContinue.aspx' class='hl' target='_blank'>继续购物</a>";
        document.getElementById("SettleGiftErrorInfo").innerText = "您只需继续购物且满足赠品活动条件，即可" + tGifttxt + "";
    }
    var rows = this.Lines;
    //alert(rows.length);
    for (var i = 0; i < rows.length; i++) {
        var row = this.GetElementById("trLine" + rows[i].LineId);
        //alert(row.isGiftLine);
        if (row.isGiftLine) {
            for (var j = 0; j < row.cells.length; j++) {
                row.cells[j].style.backgroundColor = overInfo.IsOver ? "#ffffec" : "#ffffff";
            }
        }
    }
    document.frames["iframeRampageGift"].location.reload();
    document.frames["iframeGift"].location.reload();
}

Cart.prototype.RefreshGp = function (overInfo, isLogin) {
    document.getElementById("trGpOver").style.display = (overInfo.IsOver ? "block" : "none");
    document.getElementById("SettleGpErrorInfo").style.display = (overInfo.IsOver ? "block" : "none");
    //document.getElementById("trGpOver").style.display = (overInfo.IsOver && orderInfo.isLogin ) ? "block" : "none";

    var rows = this.Lines;
    if (isLogin) {
        document.getElementById("spanGpErrorInfo").innerText = "您的积分不足， 请修改积分商品或只需再购买" + parseFloat((overInfo.AmountOver / 10.00)).toFixed(2) + "元商品即可";
        document.getElementById("SettleGpErrorInfo").innerText = document.getElementById("spanGpErrorInfo").innerText;
    }
    else {
        document.getElementById("spanGpErrorInfo").innerHTML = "您的积分不足，只需再购买" + parseFloat((overInfo.AmountOver / 10.00)).toFixed(2) + "元商品即可."
            + "<a href='javascript:void' onclick='myCart.StartLogin(false);'>登录查看帐户积分</a>";
        document.getElementById("SettleGpErrorInfo").innerText = document.getElementById("spanGpErrorInfo").innerText;
    }


    //alert(rows.length);
    for (var i = 0; i < rows.length; i++) {
        var row = this.GetElementById("trLine" + rows[i].LineId);
        if (row.isGpLine) {
            for (var j = 0; j < row.cells.length; j++) {
                row.cells[j].style.backgroundColor = overInfo.IsOver ? "#ffffec" : "#ffffff";
            }
        }
    }
}


Cart.prototype.Settle = function () {
    M18Web.Order.CartService.Settle(this.SettleCallback,
        this.ErrorCallback, new CallbackContext(null, this));
}


$(document).ready(function () {

    $('.jclose').click(function () {
        $('#gifts').hide();
    })
})


Cart.prototype.SettleCallback = function (settleResult, callBackContext) {
    document.getElementById("gifts").style.display = "none";
    if (settleResult.IsOk) {
        document.location.href = settleResult.NextUrl;
    }
    else {
        document.getElementById("SettleErrorInfo").innerText = settleResult.ErrorInfo;
    }

    if (!settleResult.IsOkAlert) {
        document.getElementById("giftserror").innerHTML = settleResult.ErrorAlertInfo.replace('您的赠品不满足领取条件,请修改赠品数量或继续购物。', '您的赠品不满足领取条件,请修改赠品数量或继续购物。<a href=/static/prom/09-3/promotion.html#gift3 target=_blank>详情查看>></a>');
        document.getElementById("gifts").style.display = "block";
    }
    document.getElementById("SettleErrorInfo").style.display = (settleResult.ErrorInfo.length == 0 ? "none" : "block");
    callBackContext.Object.RefreshGift(settleResult.GiftOverInfo, settleResult.IsLogin);
    callBackContext.Object.RefreshGp(settleResult.GpOverInfo, settleResult.IsLogin);

}

Cart.prototype.ClearProgram = function () {

    M18Web.Order.CartService.CheckProgram("", this.CheckProgramCallback,
        this.ErrorCallback, new CallbackContext("Clear", this));
}

Cart.prototype.CheckProgram = function () {
    var element = Cart.GetElementById("ProgramId");
    if (element.value.length == 0) {
        alert("请输入优惠代码!")
        return;
    }
    M18Web.Order.CartService.CheckProgram(element.value, this.CheckProgramCallback,
        this.ErrorCallback, new CallbackContext(null, this));
}

Cart.prototype.CheckProgramCallback = function (result, callBackContext) {
    if (!result.IsOk) {
        alert(result.ErrorInfo);
        if (callBackContext.Element != "Clear") {
            return;
        }
        //element.value = result.OldProgramId
    }

    if (result.ErrorInfo.length == 0) result.IsOk = false;
    var noProgramId = document.getElementById("NoProgramId");
    //noProgramId.innerHTML= "您好，您获得"+result.ErrorInfo+",欢迎继续购物！";
    noProgramId.style.display = result.IsOk ? "none" : "block";

    var hasProgramId = document.getElementById("hasProgramId");
    hasProgramId.style.display = result.IsOk ? "block" : "none";

    document.getElementById("programDescription").innerText = result.ErrorInfo;

    if (callBackContext.Element == "Clear") {
        var element = Cart.GetElementById("ProgramId");
        element.value = "";
        noProgramId.style.display = "block";
        hasProgramId.style.display = "none";
    }

}

$(document).ready(function () {
    $('.mask').width($(document).width());
    $('.mask').height($(document).height());

})

Cart.prototype.SetOrderInfo = function (orderInfo) {
    this.GetElementById(this.ColumnName["GpAmount"]).innerText = orderInfo.GpAmount;
    this.GetElementById(this.ColumnName["Point"]).innerText = orderInfo.GpPoint;
    this.GetElementById(this.ColumnName["ItemAmount"]).innerText = orderInfo.ItemAmount.toFixed(2);
    this.RefreshGift(orderInfo.GiftOverInfo, orderInfo.IsLogin);
    this.RefreshGp(orderInfo.GpOverInfo, orderInfo.IsLogin);
    document.getElementById("FreeShipInfo").innerHTML = orderInfo.CartTip;

    for (var i = 0; i < orderInfo.GiftLines.length; i++) {
        for (var j = 0; j < this.Lines.length; j++) {
            if (orderInfo.GiftLines[i].LineId == this.Lines[j].LineId) {
                this.Lines[j].MaxQty = orderInfo.GiftLines[i].MaxQty;
                OrderLine.GetElementById("aAdd" + this.Lines[j].LineId).style.visibility =
                    (this.Lines[j].Qty >= this.Lines[j].MaxQty ? "hidden" : "visible");


            }
        }
    }

}
//设置登陆信息
Cart.prototype.StartLogin = function (isForFreight) {
    //document.getElementById("login-pop").style.display = "block";
    M18Web.Order.CartService.IsOKLogin(IsOKLoginCallBack);
}

function IsOKLoginCallBack(str) {
    if (str == "Y") {
        document.location.reload();
    } else {
        $("body").AjaxLogin({
            //用户登陆成功
            success: function () {
                document.location.reload();
            },
            //用户登陆失败
            error: function () {
                //登陆失败后需要执行的代码写在这里 
                return false;
            }
        });
    }
}


Cart.prototype.CancelLogin = function () {
    document.getElementById("login-pop").style.display = "none";
}

Cart.prototype.Login = function () {
    var userId = document.getElementById("txtUserId").value;
    var password = document.getElementById("txtPassword").value;
    if (userId.length == 0) {
        alert("请输入用户名！");
        document.getElementById("txtUserId").focus();
        return;
    }
    if (password.length == 0) {
        alert("请输入密码！");
        document.getElementById("txtPassword").focus();
        return;
    }

    M18Web.Order.CartService.Login(userId, password, this.LoginCallback,
        this.ErrorCallback, new CallbackContext(null, this));

}

Cart.prototype.ForgetPwd = function () {
    var userId = document.getElementById("txtUserId").value;
    if (userId.length == 0) {
        alert("请输入用户名！");
        document.getElementById("txtUserId").focus();
        return;
    }
    //window.open("../Contact/ContactForetpwd.aspx?")

}


Cart.prototype.LoginCallback = function (result, callBackContext) {
    if (!result.IsOK) {
        alert(result.ErrorInfo);
    }
    else {
        document.getElementById("login-pop").style.display = "none";
        document.location.reload();
    }
}

Cart.prototype.ShowKitDetail = function (lineId) {
    var table = document.getElementById("spanKitDetail");
    for (var i = table.rows.length - 1; i >= 0; i--) {
        table.deleteRow(i);
    }
    //table.rows.length = 0;
    var hasLine = false;
    var row = table.insertRow();
    var cell = row.insertCell();
    cell.innerText = "商品号";
    cell = row.insertCell();
    cell.innerText = "商品名称";
    cell = row.insertCell();
    cell.innerText = "数量";
    for (var i = 0; i < this.KitLines.length; i++) {


        if (lineId == this.KitLines[i].LineId) {
            var row = table.insertRow();
            var cell = row.insertCell();
            cell.innerText = this.KitLines[i].ItemId;
            cell = row.insertCell();
            cell.innerText = this.KitLines[i].ItemName;
            cell = row.insertCell();
            cell.innerText = this.KitLines[i].Qty;
            hasLine = true;
        }
        //alert(this.KitLines.length);
        //
        // span.innerHTML += this.KitLines[i].ItemId + "   " + this.KitLines[i].ItemName + "  " + this.KitLines[i].Qty + "<br>";
        //}
    }
    if (!hasLine)
        return;
    //span.InnerHTML+="</span>"
    //alert(span.innerHTML);
    table.style.display = "inline";
    if (document.all && document.readyState == "complete")  //针对IE
    {
        table.style.pixelLeft = event.clientX + document.body.scrollLeft + 10;
        table.style.pixelTop = event.clientY + document.body.scrollTop;
        table.style.visibility = "visible";
        $("#spanKitDetail").css('top', event.clientY + $(window).scrollTop());
    } else if (document.layers)    //针对Netscape
    {
        table.left = e.pageX + 10;
        table.top = e.pageY + 10;
        table.visibility = "show";
    }

}
Cart.prototype.HideKitDetail = function () {
    var span = document.getElementById("spanKitDetail");
    span.style.display = "none";
}

Cart.prototype.SetCanModify = function () {
    var rows = this.Lines;
    for (var i = 0; i < rows.length; i++) {
        rows[i].SetCanModify();
    }
}

// 11 月 - 18 日

//新加方法 订单行删除 开始
//页面调用原型 myCart.NewDelete(1,this)
//2009-1-25修改
//增加显示,关闭删除图层
Cart.prototype.NewDelete = function (lineId, element) {
    var orderLine = this.GetLine(lineId);
    if (orderLine.SaleType > 2) {   //2010-8-25  删除奖品时提示
        if (confirm('您是否确认删除此奖品！提醒奖品删除后，不能再领取。')) {
            M18Web.Order.CartService.NewDelete(lineId, this.NewDeleteCallback,
                this.ErrorCallback, new CallbackContext(element, this));
        }
    }
    else {
        M18Web.Order.CartService.NewDelete(lineId, this.NewDeleteCallback,
             this.ErrorCallback, new CallbackContext(element, this));
    }
    this.DeleteLayerHidden(lineId);
}
//新加 删除数据回掉
Cart.prototype.NewDeleteCallback = function (myCartInfo, callBackContext) {
    //设置订单返回数据
    callBackContext.Object.SetMyCartInfo(myCartInfo);
}
//显示删除图层
Cart.prototype.DeleteLayerShow = function (lineId) {
    $("#orderLayer" + lineId).addClass("cur");
}
//关闭删除图层
Cart.prototype.DeleteLayerHidden = function (lineId) {
    $("#orderLayer" + lineId).removeClass("cur");
}
//新加方法 订单行删除 结束


//新加 订单行修改数量 开始
// 购物车 直接修改文本框更新方法
// myCart.NewModifyQty1(1,this)
Cart.prototype.NewModifyQty1 = function (lineId, qtyText) {
    var orderLine = this.GetLine(lineId);
    if (orderLine.MaxQty == 0) {
        alert('对不起，商品当前已暂时缺货，不修改数量，你可以稍后再试或暂时从购物车去删除此商品。');
        return;
    }
    if (orderLine.CheckQty()) {
        orderLine.NewModifyQty1();
    }
    else {
        window.location.reload();
    }
}
// 页面 + - 符号调用Js
// 页面方法原型
// - myCart.NewModifyQty(1,-1)
// + myCart.NewModifyQty(1,1)
Cart.prototype.NewModifyQty = function (lineId, qtyChanged) {
    var orderLine = this.GetLine(lineId);
    if (orderLine.MaxQty == 0) {
        alert('对不起，商品当前已暂时缺货，不修改数量，你可以稍后再试或暂时从购物车去删除此商品。');
        return;
    }
    if (orderLine.CheckQty()) {
        var qty = orderLine.GetQty() + qtyChanged;
        if (qty <= 0) return;
        orderLine.NewModifyQty(qty);
    }
    else {
        window.location.reload();
    }
}
//新加 订单行修改数量 结束


Cart.prototype.SetMyCartInfo = function (myCartInfo) {
    //刷新 增品区
    this.RefreshGift(isShowAll);
    //购物车顶部 提示（2011-2-21 临时由于部门号免运费去掉）
    //this.RefreshTip();

    //更新当前购物车 总金额 总花费积分 总获得积分
    $('#' + this.ColumnName["GpAmount"]).text(myCartInfo.GpAmount);
    $('#' + this.ColumnName["Point"]).text(myCartInfo.GpPoint);
    $('#' + this.ColumnName["ItemAmount"]).text(myCartInfo.ItemAmount.toFixed(2));
    //刷新所有的订单行
    $('#' + this.ColumnName["OrderLineHtml"]).html(myCartInfo.OrderLineHtml);
    // //update 20110830 每满立减
    $('#' + this.ColumnName["PromotionOrderLineHtml"]).html(myCartInfo.PromotionOrderLineHtml);
    $('#SettleErrorInfo').html('');
    if (!myCartInfo.isOk) {
        //$('#SettleErrorInfo').html('');
        $('#SettleErrorInfo').html(myCartInfo.BottomNotice);
    }
    //刷新购物车 缺货商品
    if (myCartInfo.isShowSkuOos) {
        $("#" + this.ColumnName["SkuOosHtml"] + "Line").html(myCartInfo.SkuOosGiftHtml);
        $("#" + this.ColumnName["SkuOosHtml"]).show();
    }
    else {
        $("#" + this.ColumnName["SkuOosHtml"]).hide();
    }
}

//提交订单 按钮事件 开始
Cart.prototype.NewSettle = function () {
    M18Web.Order.CartService.NewSettle(this.NewSettleCallback, this.ErrorCallback, new CallbackContext(null, this));
    //拆单修改,此过程会检查是否有售完商品
    //M18Web.Order.CartService.NewSettleWithStock(this.NewSettleCallback, this.ErrorCallback, new CallbackContext(null, this));
}
Cart.prototype.NewSettleCallback = function (newSettleResult, callBackContext) {
    document.getElementById("gifts").style.display = "none";
    document.getElementById("StockFailed").style.display = "none";
    if (newSettleResult.IsOk) {
        document.location.href = newSettleResult.NextUrl;
    }
    else {
        if (newSettleResult.IsOkAlert) {
            if (newSettleResult.ErrorCode == 1) {
                $('#giftserror').html(newSettleResult.ErrorInfo);
                document.getElementById("gifts").style.display = "block";
                $("#gifts").css('top', 155 + $(window).scrollTop());
            }
            else if (newSettleResult.ErrorCode == 2) {
                $('#oosproduct').html(newSettleResult.ErrorInfo);
                document.getElementById("StockFailed").style.display = "block";
                $("#StockFailed").css('top', 155 + $(window).scrollTop());
            }
        }
        //刷新 购物车订单行列表 数据  --可以不刷新 领留
    }
}
Cart.prototype.CancelNewSettle = function () {
    document.getElementById("gifts").style.display = "none";
    document.getElementById("StockFailed").style.display = "none";
}
//新加 提交订单 按钮事件 结束

//新加 刷新 购物车每满立减提示 开始
Cart.prototype.RefreshPromotion = function () {
    M18Web.Order.CartService.RePromotionList(this.GetPromotionCallback,
        this.ErrorCallback, new CallbackContext(null, this));
}

Cart.prototype.GetPromotionCallback = function (tipTxt, callBackContext) {
    alert(tipTxt);
    $('#PromotionOrder').html(tipTxt);
}
//新加 刷新 购物车每满立减提示 结束

//新加 刷新 购物车顶部提示 开始
Cart.prototype.RefreshTip = function () {
    M18Web.Order.CartService.GetTip(this.GetTipCallback,
        this.ErrorCallback, new CallbackContext(null, this));
}
Cart.prototype.GetTipCallback = function (tipTxt, callBackContext) {
    $('#FreeShipInfo').html(tipTxt);
}
//新加 刷新 购物车顶部提示 结束

//新加 刷新 购物车顶部提示 开始
Cart.prototype.RefreshGift = function (isShowAll) {
    M18Web.Order.CartService.RefreshGiftList(isShowAll, this.RefreshGiftCallback,
        this.ErrorCallback, new CallbackContext(null, this));
}
Cart.prototype.RefreshGiftCallback = function (tipTxt, callBackContext) {
    $('#MyGiftList').html(tipTxt);
    //alert("服务时间" + tipTxt.ServiceTime + "$$拼装时间" + tipTxt.HtmlTime);
}
//新加 刷新 购物车顶部提示 结束

//新加  删除售完商品 开始
Cart.prototype.DeleteStockFailed = function () {
    M18Web.Order.CartService.DeleteStockFailed(this.StockFailedCallback,
        this.ErrorCallback, new CallbackContext(null, this));
    document.getElementById("StockFailed").style.display = "none";
}
Cart.prototype.StockFailedCallback = function (myCartInfo, callBackContext) {
    //设置订单返回数据
    callBackContext.Object.SetMyCartInfo(myCartInfo);
}
//新加  删除售完商品 结束