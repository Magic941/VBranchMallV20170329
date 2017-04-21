function OrderGift(styleId, itemId, maxQty, hasItem) {
    this.StyleId = styleId;
    this.Size = "";
    this.Color = "";
    this.ItemId = itemId;
    this.MaxQty = maxQty;
    this.HasItem = hasItem;
}

OrderGift.prototype = new RSBase();

OrderGift.prototype.ErrorCallbackRSBase = OrderGift.prototype.ErrorCallback;

OrderGift.prototype.ErrorCallback = function(error, callBackContext, response) {

    callBackContext.Object.ErrorCallbackRSBase(error, callBackContext, response);
    callBackContext.Element.value = "";
}

OrderGift.GetElementById = function(controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}

OrderGift.prototype.GetElementById1 = function(controlId) {
    var o = document.getElementById(controlId);
    if (o == null) {
        alert("控件编号:" + controlId + "不存在");
        return null;
    }
    return o;
}
OrderGift.prototype.HideErrorInfo = function() {
    var element = document.getElementById("divErrorInfo");
    element.style.display = "none";
}
OrderGift.prototype.ShowErrorInfo = function(errorInfo) {
    var element = document.getElementById("divErrorInfo");
    element.innerText = errorInfo;
    element.style.display = "inline";

}

OrderGift.prototype.GetCallbackContext = function(element) {
    return new CallbackContext(element, this);
}

OrderGift.prototype.SelectColor = function(colorId, colorName) {
    this.ClearSelectColor();
    this.Color = colorId;
    this.Size = "";
    this.ItemId = "";
    var element = document.getElementById("liColor" + colorId);
    element.className = "zp_text_img";

    document.getElementById("SizeSelected").innerText = "";
    document.getElementById("ColorSelected").innerText = colorName;

    M18Web.Style.StyleService.GetStyleSize(this.StyleId, colorId, this.SelectColorCallback, this.ErrorCallback, new CallbackContext(null, this));
}

OrderGift.prototype.SelectColorCallback = function(skuResult, callBackContext) {
    var ul = document.getElementById("ulSize");
    var children = ul.childNodes;
    for (var i = children.length; i > 0; i--) {
        //ul.removeChild(children[i - 1]);
        children[i - 1].style.display = "none";
        for (var j = 0; j < skuResult.Sizes.length; j++) {
            if (children[i - 1].dimensionId == skuResult.Sizes[j].DimensionID) {
                children[i - 1].style.display = "block";
                children[i - 1].itemId = skuResult.Sizes[j].ItemId children[i - 1].className = "";
            }

        }
    }
    callBackContext.Object.HideErrorInfo();
}

OrderGift.prototype.SelectSize = function(a) {
    if (this.Color == null || this.Color == undefined || this.Color.length == 0) {
        if (this.HasItem) this.ShowErrorInfo("请选择所需要的颜色")
        else this.ShowErrorInfo("对不起，礼品已售完！")

        return;
    }
    this.ClearSelectSize();

    this.Size = a.parentNode.dimensionId;
    this.ItemId = a.parentNode.itemId;
    //alert(a.dimensionId);
    var element = document.getElementById("liSize" + this.Size);
    //alert("liSize" + this.Size);
    element.className = "zp_size_2";

    document.getElementById("SizeSelected").innerText = a.dimentionDesc;
    this.HideErrorInfo()
}

OrderGift.prototype.ClearSelectSize = function() {
    if (this.Size.length != 0) {
        var element = document.getElementById("liSize" + this.Size);
        element.className = "";
    }

}

OrderGift.prototype.ClearSelectColor = function() {
    if (this.Color.length != 0) {
        var element = document.getElementById("liColor" + this.Color);
        element.className = "";
    }

}

OrderGift.prototype.ValidateQty = function() {

    var qty = document.getElementById("txtQty").value;
    if (qty.length == 0) {
        this.ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }
    //alert(isNaN(qty));
    if (isNaN(qty)) {
        this.ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }
    qty = parseInt(qty);
    if (isNaN(qty)) {
        this.ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }

    //alert(this.ItemId);
    if ((this.Color == null || this.Color == undefined || this.Color.length == 0) && (this.ItemId == null || this.ItemId == undefined || this.ItemId.length == 0)) {
        this.ShowErrorInfo("请选择所需要的颜色") return false;
    }

    if (qty <= 0) {
        this.ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }

    if (this.ItemId == null || this.ItemId == undefined || this.ItemId.length == 0) {
        this.ShowErrorInfo("请选择所需要的尺寸！") return false;
    }

    if (qty > this.MaxQty) {
        this.ShowErrorInfo("您一次最多只能领取" + this.MaxQty.toString() + "件，请重新输入");
        return false;
    }
    this.HideErrorInfo();
    return true;

}

OrderGift.prototype.QtyChange = function() {
    if (!this.ValidateQty()) {
        document.getElementById("txtQty").focus();
    }
}

OrderGift.prototype.AddToCart = function() {
    if (!this.ValidateQty()) {
        return;
    }

    var qty = parseInt(document.getElementById("txtQty").value);
    M18Web.Order.CartService.AddToCart(this.ItemId, qty, this.AddToCartCallback, this.ErrorCallback, new CallbackContext(document.getElementById("txtQty"), this));

}

OrderGift.prototype.AddToCartCallback = function(callBackContext) {
    window.parent.location.href = "./OrderShopCart.aspx"
}

//--------------- 2009-11-11 修改  --------
//修改颜色选择和尺码选择功能, 保证页面中多个商品颜色,尺码选择不冲突
//------------------------------------------
OrderGift.prototype.New_SelectColor = function(colorId, colorName, styleId) {
    this.New_ClearSelectColor(styleId);
    this.Color = colorId;
    this.Size = "";
    this.ItemId = "";
    this.StyleId = styleId;
    var element = document.getElementById("liColor" + colorId + "_" + styleId);
    element.className = "cur";

    if (navigator.appName.indexOf("Explorer") > -1) { //ie
        document.getElementById("SizeSelected" + "_" + styleId).innerText = "";
        document.getElementById("ColorSelected" + "_" + styleId).innerText = colorName;
    } else { //ff
        document.getElementById("SizeSelected" + "_" + styleId).textContent = "";
        document.getElementById("ColorSelected" + "_" + styleId).textContent = colorName;
    }

    document.getElementById("ulColor" + "_" + this.StyleId).setAttribute("colorId", colorId);
    M18Web.Style.StyleService.GetStyleSize(this.StyleId, colorId, this.New_SelectColorCallback, this.ErrorCallback, new CallbackContext(null, this));
}

OrderGift.prototype.New_ClearSelectColor = function(styleId) {
    var ul = document.getElementById("ulColor" + "_" + styleId);
    var children = ul.childNodes;
    for (var i = children.length; i > 0; i--) {
        if (children[i - 1].nodeName == "#text") continue;
        children[i - 1].className = "";
    }
}

OrderGift.prototype.New_SelectSize = function(a) {
    this.Color = document.getElementById("ulColor" + "_" + this.StyleId).getAttribute("colorId");
    if (this.Color == null || this.Color == undefined || this.Color.length == 0) {
        if (this.HasItem) this.New_ShowErrorInfo("请选择所需要的颜色")
        else this.New_ShowErrorInfo("对不起，礼品已售完！") return;
    }
    this.New_ClearSelectSize();
    this.StyleId = a.parentNode.getAttribute("styleId");
    this.Size = a.parentNode.getAttribute("dimensionId");
    this.ItemId = a.parentNode.getAttribute("itemId");
    var element = document.getElementById("liSize" + this.Size + "_" + this.StyleId);
    element.className = "cur";
    if (navigator.appName.indexOf("Explorer") > -1) { //ie
        document.getElementById("SizeSelected" + "_" + this.StyleId).innerText = a.getAttribute("dimentionDesc");
    } else { //ff
        document.getElementById("SizeSelected" + "_" + this.StyleId).textContent = a.getAttribute("dimentionDesc");
    }
    document.getElementById("ulSize" + "_" + this.StyleId).setAttribute("itemId", this.ItemId);
    this.New_HideErrorInfo();
}

OrderGift.prototype.New_ClearSelectSize = function() {
    var ul = document.getElementById("ulSize" + "_" + this.StyleId);
    var children = ul.childNodes;
    for (var i = children.length; i > 0; i--) {
        children[i - 1].className = "";
    }
}

OrderGift.prototype.New_SelectColorCallback = function(skuResult, callBackContext) {
    var ul = document.getElementById("ulSize" + "_" + callBackContext.Object.StyleId);
    var children = ul.childNodes;
    for (var i = children.length; i > 0; i--) {
        if (children[i - 1].nodeName == "#text") continue;
        children[i - 1].style.display = "none";
        for (var j = 0; j < skuResult.Sizes.length; j++) {
            if (children[i - 1].getAttribute("dimensionId") == skuResult.Sizes[j].DimensionID) {
                children[i - 1].style.display = "block";
                children[i - 1].setAttribute("itemId", skuResult.Sizes[j].ItemId);
                children[i - 1].className = "";
            }

        }
    }
    for (var i = 0; i < children.length; i++) {
        if (children[i].nodeName == "#text") continue;
        if (children[i].style.display != "none") {
            ul.setAttribute("itemId", children[i].getAttribute("itemId"));
            children[i].className = "cur";
            break;
        };
    }
    callBackContext.Object.New_HideErrorInfo();
}

OrderGift.prototype.New_HideErrorInfo = function() {
    var element = document.getElementById("divErrorInfo" + "_" + this.StyleId);
    element.style.display = "none";
}

OrderGift.prototype.New_ShowErrorInfo = function(errorInfo) {
    var element = document.getElementById("divErrorInfo" + "_" + this.StyleId);
    if (navigator.appName.indexOf("Explorer") > -1) { //ie
        element.innerText = errorInfo;
    } else { //ff
        element.textContent = errorInfo;
    }
    element.style.display = "inline";
}

OrderGift.prototype.New_AddToCart = function(styleId) {
    var txtQtyName = "txtQty" + styleId;
    if (!this.New_ValidateQty(styleId)) {
        return;
    }
    var qty = parseInt(document.getElementById(txtQtyName).value);
    M18Web.Order.CartService.AddToCart(this.ItemId, qty, this.AddToCartCallback, this.ErrorCallback, new CallbackContext(document.getElementById(txtQtyName), this));

}

OrderGift.prototype.New_ValidateQty = function(styleId) {
    var txtQtyName = "txtQty" + styleId;
    var qty = document.getElementById(txtQtyName).value;
    if (qty.length == 0) {
        this.New_ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }
    //alert(isNaN(qty));
    if (isNaN(qty)) {
        this.New_ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }
    qty = parseInt(qty);
    if (isNaN(qty)) {
        this.New_ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }

    //alert(this.ItemId);
    if ((this.Color == null || this.Color == undefined || this.Color.length == 0) && (this.ItemId == null || this.ItemId == undefined || this.ItemId.length == 0)) {
        if (this.HasItem == true) {
            this.New_ShowErrorInfo("请选择所需要的颜色");
            return false;
        }

    }

    if (qty <= 0) {
        this.New_ShowErrorInfo("您输入的件数不正确，请重新输入") return false;
    }

    if (this.ItemId == null || this.ItemId == undefined || this.ItemId.length == 0) {
        if (this.HasItem == true) {
            this.New_ShowErrorInfo("请选择所需要的尺寸！") return false;
        }
    }

    if (qty > this.MaxQty) {
        this.New_ShowErrorInfo("您一次最多只能领取" + this.MaxQty.toString() + "件，请重新输入");
        return false;
    }
    this.New_HideErrorInfo();
    return true;
}