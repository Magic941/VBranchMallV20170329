
$(function () {
    $("#plus").click(function () {
        var count = parseInt($("#productCount").val()) + 1;
        $("#productCount").val(count);
    });
    $("#subtract").click(function () {
        var count = parseInt($("#productCount").val());
        if (count > 1) {
            count = count - 1;
        }
        $("#productCount").val(count);
    });
    $("#btnAddToCart").click(function () {
        //暂时是需要登录才能购买
        if ($(this).hasClass('addCart-gray')) return false;
        if (!$(this).attr('itemid')) {
            $('#SKUOptions,#SKUOptions a').effect('highlight', 500);
            ShowFailTip('请选择商品规格属性！');
            return false;
        }
        var count = parseInt($("#productCount").val());
        if (Shop_BuyMode && Shop_BuyMode == "BuyNow") {
            //立刻购买
            location.href = $Maticsoft.BasePath + "Order/SubmitOrder?sku=" + $(this).attr('itemid') + "&Count=" + count;
        } else {
            location.href = $Maticsoft.BasePath + "ShoppingCart/AddCart?sku=" + $(this).attr('itemid') + "&Count=" + count;
        }
    });
    //收藏操作
    $("#btnProductFav").click(function () {
        if (CheckUserState()) {
            var productId = $(this).attr("productId");
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "u/AjaxAddFav",
                async: false,
                data: { ProductId: productId },
                success: function (data) {
                    if (data == "Rep") {
                        ShowSuccessTip('您已经收藏了该商品，请不要重复收藏');
                    } else if (data == "True") {
                        ShowSuccessTip('收藏商品成功');
                    } else {
                        ShowFailTip('服务器繁忙，请稍候再试！');
                    }
                }
            });
        }
    });
});
//判断是否含有禁用词
function ContainsDisWords(desc) {
    var isContain = false;
    $.ajax({
        url: $Maticsoft.BasePath +"Partial/ContainsDisWords",
        type: 'post', dataType: 'text', timeout: 10000,
        async: false,
        data: { Desc: desc },
        success: function (resultData) {
            if (resultData == "True") {
                isContain = true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });
    return isContain;
}
//检查是否登录
var CheckUserState = function () {
    var islogin;
    var url = $.getUrlMiddle();
    $.ajax({
        url: $Maticsoft.BasePath +"Account/AjaxIsLogin",
        type: 'post',
        dataType: 'text',
        async: false,
        success: function (resultData) {
            if (resultData != "True") {
                //dialog层中项的设置
                location.href = $Maticsoft.BasePath +"a/l?returnUrl=" + url;
                return false;
            } else {
                islogin = true;
                return true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    return islogin;
};




