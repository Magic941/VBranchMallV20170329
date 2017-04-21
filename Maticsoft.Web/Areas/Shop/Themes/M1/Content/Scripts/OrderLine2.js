function OrderLine(orderLineInfo) {

    this.LineId = orderLineInfo.LineId;
    this.MaxQty = parseInt(orderLineInfo.MaxQty);
    //this.ColumnName = columnName;
    this.Amount = 0;
    this.Unitprice = 0;
    this.GpAmount = 0;
    this.GpPoint = 0;
    this.Qty = parseInt(orderLineInfo.Qty);
    this.CanModify = orderLineInfo.CanModify;
    this.SaleType = orderLineInfo.SaleType;

    //新修改  开始
    //    OrderLine.GetElementById("aMinus" + this.LineId).style.visibility = (this.Qty > 1 ? "visible" : "hidden");
    //    OrderLine.GetElementById("aAdd" + this.LineId).style.visibility = (this.Qty >= this.MaxQty ? "hidden" : "visible");
    //新修改  结束
    //OrderLine.GetElementById("aMinus" + this.LineId).isDisabled = (this.Qty <= 1);
    //OrderLine.GetElementById("aAdd" + this.LineId).isDisabled = (this.Qty >= this.MaxQty);
}

OrderLine.prototype = new RSBase();

OrderLine.prototype.ErrorCallbackRSBase = OrderLine.prototype.ErrorCallback;

OrderLine.prototype.ErrorCallback = function(error, callBackContext, response) {

    callBackContext.Object.ErrorCallbackRSBase(error, callBackContext, response);
    callBackContext.Element.value = "";
}

OrderLine.GetElementById = function(controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}

OrderLine.prototype.GetElementById1 = function(controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}

OrderLine.prototype.Initial = function() {

}

OrderLine.prototype.GetCallbackContext = function(element) {
    return new CallbackContext(element, this);
}

OrderLine.prototype.ModifyQty = function(newQty) {
    var errorInfo = OrderLine.GetElementById("tdErrorInfo" + this.LineId);
    //alert(this.MaxQty);
    if (newQty > this.MaxQty) {
        errorInfo.innerText = "您一次最多可以购买" + this.MaxQty.toString() + "件，请重新输入";
        return;
    }
    errorInfo.innerText = "";

    M18Web.Order.CartService.ModifyQty(this.LineId, newQty, this.ModifyQtyCallback, this.ErrorCallback, new CallbackContext(this.GetQtyElement(), this));
}

OrderLine.prototype.ModifyQty1 = function() {
    var qtyString = this.GetQtyElement().value;

    var errorInfo = OrderLine.GetElementById("tdErrorInfo" + this.LineId);
    if (!this.ValidateQty(qtyString)) {
        errorInfo.innerText = "您输入的件数不正确，请重新输入";
        return;
    }
    var qty = parseInt(qtyString);
    if (isNaN(qty)) {
        errorInfo.innerText = "您输入的件数不正确，请重新输入";
        return;
    }

    if (qty > this.MaxQty) {
        errorInfo.innerText = "您一次最多可以购买" + this.MaxQty.toString() + "件，请重新输入";
        return;
    }
    errorInfo.innerText = "";
    this.ModifyQty(qty);
}

OrderLine.prototype.CheckQty = function() {
    return this.GetQty() > 0;
}

OrderLine.prototype.GetQty = function() {
    var element = this.GetQtyElement();
    if (isNaN(element.value)) {
        alert("数量不正确") return 0;
    }
    var qty = parseInt(element.value);
    return qty;
}

OrderLine.prototype.GetQtyElement = function() {
    return OrderLine.GetElementById("txtQty" + this.LineId);
}

OrderLine.prototype.ModifyQtyCallback = function(orderLineReturnInfo, callBackContext) {
    //alert(orderLineReturnInfo.OrderLineInfo.Qty);
    callBackContext.Object.SetData(orderLineReturnInfo.OrderLineInfo);
    myCart.SetOrderInfo(orderLineReturnInfo.OrderInfo);
}

OrderLine.prototype.SetData = function(orderLineInfo) {
    this.GetQtyElement().value = orderLineInfo.Qty;

    OrderLine.GetElementById("aMinus" + this.LineId).style.visibility = (orderLineInfo.Qty > 1 ? "visible": "hidden");
    OrderLine.GetElementById("aAdd" + this.LineId).style.visibility = (orderLineInfo.Qty >= this.MaxQty ? "hidden": "visible");

    this.CanModify = orderLineInfo.CanModify;
    this.SetCanModify();

    var row = this.GetElementById("trLine" + this.LineId);
    if (!row.isGiftLine) OrderLine.GetElementById("spanAmount" + this.LineId).innerText = parseFloat(orderLineInfo.Amount).toFixed(2);
    if (orderLineInfo.GpAmount <= 0) OrderLine.GetElementById("spanGpAmount" + this.LineId).innerText = "--";
    else OrderLine.GetElementById("spanGpAmount" + this.LineId).innerText = orderLineInfo.GpAmount;

}

OrderLine.prototype.ValidateQty = function(qtyString) {
    //alert(qtyString);
    if (qtyString.length == 0) {
        return false;
    }
    if (isNaN(qtyString)) {
        return false;
    }
    var qty = parseInt(qtyString);
    if (qty <= 0) return false;
    if (qty > 30000) return false;

    return true;

}

function OrderKitLine(lineId, itemId, itemName, qty) {
    this.LineId = lineId;
    this.ItemId = itemId;
    this.ItemName = itemName;
    this.Qty = qty;
}

OrderLine.prototype.SetCanModify = function() {
    if (!this.CanModify) {
        OrderLine.GetElementById("aMinus" + this.LineId).style.visibility = "hidden";
        OrderLine.GetElementById("aAdd" + this.LineId).style.visibility = "hidden";
        this.GetQtyElement().readOnly = true;
    }

}

//新加方法
//新加 订单行商品数量修改  开始
OrderLine.prototype.NewModifyQty = function(newQty) {
    //alert(this.MaxQty);
    if (newQty > this.MaxQty) {
        $('#' + 'tdErrorInfo' + this.LineId).text("您一次最多可以购买" + this.MaxQty.toString() + "件，请重新输入");
        return;
    }
    $('#' + 'tdErrorInfo' + this.LineId).text("");
    M18Web.Order.CartService.NewModifyQty(this.LineId, newQty, this.NewModifyQtyCallback, this.ErrorCallback, new CallbackContext(this.GetQtyElement(), this));
}
OrderLine.prototype.NewModifyQty1 = function() {
    var qtyString = this.GetQtyElement().value;

    var errorInfo = OrderLine.GetElementById("tdErrorInfo" + this.LineId);
    if (!this.ValidateQty(qtyString)) {
        $('#' + 'tdErrorInfo' + this.LineId).text("您输入的件数不正确，请重新输入");
        return;
    }
    var qty = parseInt(qtyString);
    if (isNaN(qty)) {
        $('#' + 'tdErrorInfo' + this.LineId).text("您输入的件数不正确，请重新输入");
        return;
    }
    if (qty > this.MaxQty) {
        $('#' + 'tdErrorInfo' + this.LineId).text("您一次最多可以购买" + this.MaxQty.toString() + "件，请重新输入");
        return;
    }
    $('#' + 'tdErrorInfo' + this.LineId).text("");
    this.NewModifyQty(qty);
}
////新加 订单行商品数据修改  结束
//新加 修改数量后的回掉函数
OrderLine.prototype.NewModifyQtyCallback = function(myCartInfo, callBackContext) {
    //alert(orderLineReturnInfo.OrderLineInfo.Qty);
    //callBackContext.Object.SetData(myCartInfo);
    myCart.SetMyCartInfo(myCartInfo);
}