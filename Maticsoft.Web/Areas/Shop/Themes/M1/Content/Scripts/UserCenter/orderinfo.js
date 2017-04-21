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
    var orderstates = $('#hidorderstates').val().toString();
    switch (orderstates) {
        case "等待付款":
            //去付款 
            $('.ftx14').text('等待付款');
            var orderid = $('#hidorderid').val();
            if (orderid) {
                $('#pay-button').prepend('<a  href="/pay/certification' + orderid + '/' + $Maticsoft.CurrentArea + '"  > <img  src="/Areas/Shop/Themes/M1/Content/images/btn_pay.gif" width="46" height="25" style="display: inline;"> </a>');
            }
            break;
        case "正在处理":
        case "等待处理":
        case "配货中": //待发货
            $('#process').find('div').eq(3).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(4).removeClass('wait').addClass('ready');
            $('#pay-button').empty();
            $('.ftx14').text('等待发货');
            break;
        case "已发货": //等待收货
            $('#process').find('div').eq(3).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(4).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(5).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(6).removeClass('wait').addClass('ready');
            $('.ftx14').text('已发货');
            break;
        case "已完成": //完成
            $('#process').find('div').eq(3).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(4).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(5).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(6).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(7).removeClass('wait').addClass('ready');
            $('#process').find('div').eq(8).removeClass('wait').addClass('ready');
            $('.ftx14').text('已完成');
            break;
        case "未知状态":
            break;
        default:
            break;

    }

    //case "等待付款确认":   
    // case "订单锁定":
    // case "取消订单": 
});
 