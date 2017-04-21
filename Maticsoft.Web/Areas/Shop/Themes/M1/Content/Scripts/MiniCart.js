function GetMiniCart() {
    $(".minicart").show();
    $(".minicart").html("<p class=\"tc\"><i class=\"loading-ico\"></i></p>");
    $.ajax({
        type: "GET",
        url: "/order/minishopcart.aspx?callback=?",
        dataType: 'jsonp',
        jsonp: 'callback',
        success: function (data) {
            try {
                $("#hd-cart-amount").html(data.PNum);
                var miniCartHtml = '<div class="fb c6">最近加入的商品：</div>';
                if (data.OrderLines != null && data.OrderLines.length > 0) {
                    miniCartHtml += " <ul class=\"minicart-list\">";
                    var i = 0;
                    for (; i < data.OrderLines.length; i++) {
                        var styleUrl = " http://product.maticsoft.com/" + (data.OrderLines[i].IsKit ? "k" : "p") + "-" + data.OrderLines[i].StyleID + ".htm";
                        miniCartHtml += "<li class=\"minicart-item\"><a title=\"\" href=" + styleUrl + " class=\"minicart-item-pic\" target=\"_blank\">";
                        miniCartHtml += " <img width=\"50\" height=\"65\" alt=\"\" src='http://img.maticsoft.com/GOODS/" + data.OrderLines[i].PictureFileName + "'></a>";
                        miniCartHtml += "<div class=\"minicart-item-txt\">";
                        miniCartHtml += "<a title=\"\" href=" + styleUrl + " class=\"minicart-item-title c6 mb5\" target=\"_blank\">" + data.OrderLines[i].StyleName.substr(0, 22) + "</a>";
                        miniCartHtml += "<p class=\"minicart-item-type c9 mb5\">" + (data.OrderLines[i].ItemColor == "" ? "" : "<span class=\"mr10\">" + data.OrderLines[i].ItemColor + "</span>") + "" + (data.OrderLines[i].ItemSize == "" ? "" : "<span class=\"mr10\">" + data.OrderLines[i].ItemSize + "</span>") + "</p>";
                        miniCartHtml += "<p class=\"minicart-item-price c6\"><button onclick=\"del('" + data.OrderLines[i].LineID + "')\" >删除</button>";
                        miniCartHtml += "<span class=\"h\">" + data.OrderLines[i].Qty + "</span><span class=\"ml5\">件</span></p></div></li>";
                    }
                    miniCartHtml += "</ul>";
                    miniCartHtml += "<div class=\"minicart-amount\"><p class=\"mb10\"><a class=\"fr c9\" href=\"/Order/OrderShopCart.aspx\">查看全部</a>共有<span class=\"fb f16 h ml5 mr5\">" + data.PNum + "</span>件商品</p>";
                    miniCartHtml += "<p class=\"tc\"><a class=\"minicart-btn\" href=\"/Order/OrderShopCart.aspx\"><span>去购物车结账</span></a></p></div>";
                    $(".minicart").html(miniCartHtml);
                } else {
                    $(".minicart").html("<p class=\"tc c6\">您的购物车里暂无商品</p>");
                }
            } catch (e) {
            }
        }
    });
}

function del(lineId) {
    $.ajax({
        type: "GET",
        url: "/order/minishopcart.aspx?lineId=" + lineId + "&callback=?",
        dataType: 'jsonp',
        jsonp: 'callback',
        success: function (data) {
            if (data.error) {
                alert("删除失败啦!");
            } else {
                GetMiniCart();
            }
        }
    });
}
$(document).ready(function () {
    $(".icon-cart").mouseenter(function () {
//        GetMiniCart();
    });
    $(".minicart").mouseleave(function () {
//        $(".minicart").hide();
    });
});