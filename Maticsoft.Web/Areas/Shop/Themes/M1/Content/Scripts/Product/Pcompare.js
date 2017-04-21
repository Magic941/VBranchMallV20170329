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

    //加载已经加入对比的商品
    loadPCompare();

    //加入对比
    $('#btnpcompare').click(function () {
        var productId = $('#hdProductId').val();
        var pname = $('#hdProductName').val();
        var price = $(this).attr("price");
        var imageurl = $('#hdProdThumUrl1').val();
        var typeid = $('#hdtypeid').val();
        var json = JSON.parse(unescape($.cookie('p_compare')));
        if (json) {
            if (json.length >= 4) {
                ShowFailTip("对比列表已满。");
                $('#pop-compare-tips').show();
                return false;
            }
            for (var i = 0; i < json.length; i++) {
                if (json[i].PId == productId) { //是否已经加入
                    ShowFailTip("该商品已经在对比列表中，请不要重复加入。");
                    return false;
                }
                if (json[i].TypeID != typeid) {
                    ShowFailTip("只能添加同一类别的商品！");
                    return false;
                }
            }
        } else {
            json = [];
        }
        json.push({ "PId": productId, "Name": pname, "Price": price, "ImageUrl": imageurl, 'TypeID': typeid });
        $.cookie('p_compare', escape(JSON.stringify(json)), { expires: 1 });
        ShowSuccessTip("加入成功！");

        var currentItem = $('#cmp_item_' + (json.length - 1));
        currentItem.removeClass('item-empty');
        currentItem.find('dt').empty();
        currentItem.find('dt').append("<a target=\"_blank\" href=\"" + $Maticsoft.BasePath + "Product/Detail/" + productId + "\" ><img  width=\"50\" src=\"" + imageurl.format('T50X65_') + "\" height=\"50\"></a>");
        currentItem.find('dd').empty();
        currentItem.find('dd').append("<a target=\"_blank\" class=\"diff-item-name\" href=\"" + $Maticsoft.BasePath + "Product/Detail/" + productId + "\">" + pname + "</a><span class=\"p-price\"><strong class=\"J-p-1052308220\">￥" + price + "</strong><a class=\"del-comp-item\" prodid=\"" + productId + "\">删除</a></span> ");
        $('#pop-compare').show();
    });

    $('[id^=cmp_item_]').hover(
        function () {
            $(this).find('.del-comp-item').addClass("show-del-comp-item");
        },
        function () {
            $(this).find('.del-comp-item').removeClass("show-del-comp-item");
        });

    //删除
    $('.show-del-comp-item').die('click').live('click', function () {
        var prodid = $(this).attr('prodid');
        var json = JSON.parse(unescape($.cookie('p_compare')));
        var jsonEx = [];
        if (json) {
            var currentItem = $('#cmp_item_' + (json.length - 1));
            currentItem.addClass('item-empty');
            currentItem.find('dt').empty();
            currentItem.find('dt').append(json.length);
            currentItem.find('dd').empty();
            currentItem.find('dd').append("您还可以继续添加");

            for (var i = 0; i < json.length; i++) {
                if (json[i].PId == prodid) {
                    continue;
                }
                jsonEx.push(json[i]);
            }
            $.cookie('p_compare', escape(JSON.stringify(jsonEx)), { expires: 1 });
            $('#pop-compare-tips').hide();
            loadPCompare();
        }
    });

    //清空商品对比项
    $('#delPCItems').click(function () {
        var json = [];
        $.cookie('p_compare', escape(JSON.stringify(json)), { expires: 1 });
        var item = $('[id^=cmp_item_]');
        for (var i = 0; i < item.length; i++) {
            var currentItem = item.eq(i);
            currentItem.addClass('item-empty');
            currentItem.find('dt').empty();
            currentItem.find('dt').append(i + 1);
            currentItem.find('dd').empty();
            currentItem.find('dd').append("您还可以继续添加");
        }
    });


    //对比
    $('#goto-contrast').click(function () {
        var json = JSON.parse(unescape($.cookie('p_compare')));
        if (json && json.length > 1) {
            var prodids = '';
            var type = '';
            for (var i = 0; i < json.length; i++) {
                prodids += json[i].PId + '_';
                type = json[i].TypeID;
            }
            window.open($Maticsoft.BasePath + "Product/Compare/"+type+"/" + prodids);
            return true;
        }
        ShowFailTip("至少要有两件商品才能对比哦~");
        return false;
    });

});

//加载对比商品
function loadPCompare() {
    var jsonExists = JSON.parse(unescape($.cookie('p_compare')));
    if (jsonExists && jsonExists.length>0) {
        $('#pop-compare').show();
        for (var i = 0; i < jsonExists.length; i++) {
            var currentItem = $('#cmp_item_' + i);
            currentItem.removeClass('item-empty');
            currentItem.find('dt').empty();
            currentItem.find('dt').append("<a target=\"_blank\" href=\"" + $Maticsoft.BasePath + "Product/Detail/" + jsonExists[i].PId + "\" ><img  width=\"50\" src=\"" + jsonExists[i].ImageUrl.format('T50X65_') + "\" height=\"50\"></a>");
            currentItem.find('dd').empty();
            currentItem.find('dd').append("<a target=\"_blank\" class=\"diff-item-name\" href=\"" + $Maticsoft.BasePath + "Product/Detail/" + jsonExists[i].PId + "\">" + jsonExists[i].Name + "</a><span class=\"p-price\"><strong class=\"J-p-1052308220\">￥" + jsonExists[i].Price + "</strong><a class=\"del-comp-item\" prodid=\"" + jsonExists[i].PId + "\">删除</a></span> ");
            if (i == 3) {//最多4个
                break;
            }
        }
    }
}