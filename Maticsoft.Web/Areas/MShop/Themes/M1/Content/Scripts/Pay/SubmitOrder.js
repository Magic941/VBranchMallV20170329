/*
* File:        SubmitOrder.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2013 Maticsoft. All Rights Reserved.
*/
;

function Edit_Consignee(sender, addressId) {
    if (sender) $(sender).hide();
    var $target = $('#step-1');
    //Status_Editing($target);
    $target.load($Maticsoft.BasePath + 'Order/AddressInfo', { addressId: addressId });
}

function Edit_Payment(sender, isShowTip) {
    if (sender) $(sender).hide();
    var $target = $('#step-2'),
        payId = $target.find('#PaymentModeId').val(),
        shipId = $target.find('#ShippingTypeId').val();
    //Status_Editing($target);
    $target.load($Maticsoft.BasePath + 'Order/PayAndShipInfo', {
        payId: payId,
        shipId: shipId
    }, function () {
        if (isShowTip) $('#save-consignee-tip').show();
    });
}

function Status_Editing(target) {
    $('.step').removeClass('step-current');
    target.addClass('step-current');
    target.find('.step-content').empty().append('<div class="step-loading"><div class="loading-style1" style="margin-bottom: 20px;"><b></b>正在加载中，请稍候...</div></div>');
    $(document.body).append(
        ('<div id="mask_maticsoft" style="width: 100%; height: {0}px; position: absolute; top: 0px; left: 0px; z-index: 9998; opacity: 0.7; display: block;"></div>').format(
            $(document).height())
    );
}

function Status_None() {
    $('.step').removeClass('step-current');
    $('#mask_maticsoft').remove();
}

//提交订单
function SubmitOrder(sender, shippingAddressId, shippingTypeId, paymentModeId, conpon, supplierFreight) {
    //    $.alert("此功能还在完善中, 敬请期待.");
    //    return;
    var skuArry = [];
    var skuId = $('#SkuInfo').val();
    var count = $('#SkuCount').val();
    var proSaleId = $('#ProSale').val();
    var groupBuyId = $('#GroupBuy').val();

    if (skuId) {
        var sku = {};
        sku.SKU = skuId;
        sku.Count = count ? count : 1;
        skuArry.push(sku);
    } else {
        skuArry = null;
    }

    var checkoutLoading = $('<span id="order-loading" class="checkout-state"><b></b>\u6B63\u5728\u63D0\u4EA4\u8BA2\u5355\uFF0C\u8BF7\u7A0D\u5019\uFF01</span>');
    var originSubmit = $("#order-submit").clone(true);
    //lock
    $(sender).fadeOut('slow', function () {
        $("#order-loading").replaceWith(originSubmit);
        //进入Jquery队列执行
        $(this).replaceWith(checkoutLoading).queue(function (next) {
            $.ajax({
                url: '/Pay/OrderHandler.aspx',
                type: 'post',
                dataType: 'json',
                timeout: 0,
                async: true,
                data: { Action: "SubmitOrder",
                    ShippingAddressId: shippingAddressId,
                    ShippingTypeId: shippingTypeId,
                    PaymentModeId: paymentModeId,
                    Coupon: conpon,
                    SupplierFreight: supplierFreight,
                    ProSaleId: proSaleId,
                    GroupBuyId: groupBuyId,
                    SkuInfos: skuArry ? JSON.stringify(skuArry) : null
                },
                success: function (resultData) {
                    var isOK = false;
                    switch (resultData.STATUS) {
                        // 提交订单成功 
                        case "SUCCESS":
                            isOK = true;
                            //延迟两秒后跳转
                            $("#order-loading").html("<b></b>订单提提交成功, 请稍后..").animate({ opacity: 1.0 }, 1000).fadeOut("slow", function () {
                                //DONE: 货到付款/银行汇款 跳转 BEN MODIFY 20131205
                                //if (resultData.GATEWAY == 'cod' || resultData.GATEWAY == 'bank') {
                                //    window.location.replace('/pay/certification' + resultData.DATA.OrderId + '/' + $Maticsoft.CurrentArea);
                                //} else {
                                //    window.location.replace($Maticsoft.BasePath + 'Order/SubmitSuccess/' + resultData.DATA.OrderId);
                                //}
                                window.location.replace('/pay/certification' + resultData.DATA.OrderId + '/' + $Maticsoft.CurrentArea);
                            });
                            break;
                        case "NOSTOCK":
                            $.alertEx('很抱歉.<br/><br/><br/>您购买的部分商品已经被其TA人抢先下单了, <br/> 确认后返回购物车.', function () {
                                $.navURL($Maticsoft.BasePath +'ShoppingCart/CartInfo');
                            });
                            break;
                        case "NOSHOPPINGCARTINFO":
                            $.alertEx('您的购物车是空的, 请加入商品后提交订单!', function () {
                                $.navURL($Maticsoft.BasePath +'ShoppingCart/CartInfo');
                            });
                            break;
                        case "NOLOGIN":
                            // 用户未登陆
                            $.alertEx('您还没有登陆或者登陆已超时，请您登陆后提交订单．', function () {
                                $.navURL($Maticsoft.BasePath + 'Account/Login?return=' + encodeURIComponent(window.location.href));
                            });
                            break;
                        case "UNAUTHORIZED":
                            // 权限不足
                           $.alertEx("只有普通用户才可以提交订单喔, 请您登陆普通用户再提交订单.");
                         
                            break;
                        default:
                            // 抛出异常消息
                            $.alertError(resultData.STATUS);
                            break;
                    }
                    if (!isOK) {
                        $("#order-loading").fadeOut('slow', function () {
                            $("#order-loading").replaceWith(originSubmit);
                            $(sender).fadeIn('slow');
                        });
                    }
                },

                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    if (textStatus != 'timeout') {
                        $.alertError(xmlHttpRequest.responseText);
                    } else {
                        $("#submit_message").html("噗, 您的网络忒慢了! 访问服务器超时了, 请再试一下!");
                    }
                    $("#order-loading").replaceWith(originSubmit);
                    $("#submit_message").show();
                    $(sender).fadeIn('slow');
                }
            });

            next();
        });
    });
}

