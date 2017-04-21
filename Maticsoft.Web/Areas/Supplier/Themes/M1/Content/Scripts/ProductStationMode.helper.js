//(function () {
var baseAddTable = '<table id="dlstAddedProducts" cellspacing="0" border="0" style="width:96%;border-collapse:collapse;"><tbody></tbody></table>';
var baseDelTable = '<table id="dlstSearchProducts" cellspacing="0" border="0" style="width:96%;border-collapse:collapse;"><tbody></tbody></table>';

var highlightTime = 1500;
//购物车动态效果函数
(function ($) {
    $.extend({
        add2cart: function (sender, target, text) {

            if (sender.length < 1) return;
            if (target.length < 1) return;

            var shadow = $('#' + sender.attr("id") + '_shadow');
            if (text == undefined || text == "") {
                text = "&nbsp;";
            }
            if (!shadow.attr('id')) {
                $('body').prepend('<div id="' + sender.attr("id") + '_shadow" style="display: none; background-color: #FFDA4D; border: solid 1px darkgray; position: static; top: 0px; z-index: 100000;">' + text + '</div>');
                var shadow = $('#' + sender.attr("id") + '_shadow');
            }
            if (!shadow) {
                alert('Cannot create the shadow div');
            }
            shadow.width(sender.css('width')).height(sender.css('height')).css('top', sender.offset().top).css('left', sender.offset().left).css('opacity', 0.5).show();
            shadow.css('position', 'absolute');

            //追加处理 目标高亮 取消JqueryUI
//            sender.hide('highlight', highlightTime);
//            target.show('highlight', highlightTime);

            shadow.animate({ width: target.innerWidth(), height: target.innerHeight(), top: target.offset().top, left: target.offset().left }, { duration: 400 })
                .animate({ opacity: 0 }, {
                    duration: 300,
                    complete: function () {
                        //                        target.queue(function() {
                        //                            $(this).dequeue();
                        //                        });

                        //追加处理 删除原对象/重置已选择SKU
                        sender.remove();
                        shadow.remove();
                    }
                });
        }
    });
})(jQuery);


var addedproductslist;
var searchproductslist;
$(document).ready(function () {
    addedproductslist = $(".addedproductslist");
    searchproductslist = $(".searchproductslist");

    $('.submit_add').bind('click', function () {
        InitAddProduct(this, addedproductslist);
        //添加
        var currentTableTR = $(this).parents('tr:last');
        var currentTable = currentTableTR.find('table');
        var pid = currentTable.attr('skuid');
        insertProductStationMode(pid, 0);
    });
    $('.submit_del').bind('click', function () {
        InitDelProduct(this, searchproductslist);
        //删除
//        var currentTableTR = $(this).parents('tr:last');
//        var currentTable = currentTableTR.find('table');
        var pid = $(this).attr('skuid');
        RemoveProductStationMode(pid, 0);
    });
    $('#btnClear').bind('click', function() {
        $.ajax({
            url: $Maticsoft.BasePath + "Product/ClearProductsStatMode",
            type: 'POST',
            dataType: 'text',
            timeout: 10000,
            data: { SelectType: 0 },
            async: false,
            success: function(resultData) {
                //if (resultData == "True") {
                    ShowSuccessTip("操作成功！");
                $('.addedproductslist').empty();
                setTimeout("window.location.reload()",2500);
                //} else {
                //ShowFailTip("操作失败!");
                //}
            }
        });
    });
     $('#btnSearch').bind('submit', function() {
         var cid = $('#drpProductCategory').val();
         var pname = $('#txtProductName').val();
         var url = $.setUrlParam('categoryId', cid);
         window.location =$.setUrlParam('productName', pname, url);
         return false;
     });
    $('#drpProductCategory').val($.getUrlParam('categoryId'));
    $('#txtProductName').val($.getUrlParam('productName'));
});

//添加
function insertProductStationMode(productId, type) {
    $.ajax({
        url: $Maticsoft.BasePath+ "Product/AddProductStationMode",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { ProductId: productId, Type: type },
        async: false,
        success: function (resultData) {
            if (resultData && resultData.STATUS == "Presence") {
                ShowServerBusyTip("该商品已存在！");
                return false;
            }
        }
    });
}
//删除
function RemoveProductStationMode(productId, type) {
    $.ajax({
        url: $Maticsoft.BasePath+ "Product/RemoveProductStationMode",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { ProductId: productId, Type: type },
        async: false,
        success: function (resultData) {

        }
    });
}


function AddSelectedSKUId(skuid, thisEvent) {
    var hfSelectedData = $('[id$=hfSelectedData]');
    var tmpSKU = hfSelectedData.val() + skuid + ',';
    hfSelectedData.val(tmpSKU);
}

function DelSelectedSKUId(skuid, thisEvent) {
    var hfSelectedData = $('[id$=hfSelectedData]');
    var skus = hfSelectedData.val().split(',');
    if (skus) {
        //delete skuid
        skus.remove(skus.getIndexByValue(skuid));
    }
    var tmpSKU = skus.join(",");
    hfSelectedData.val(tmpSKU);
}

function InitDelProduct(send, targetContext) {
    $(send).unbind('click'); //撤销事件, 防止恶意点击
    var currentTableTR = $(send).parents('tr:last');
    var currentTable = currentTableTR.find('table');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_del').removeClass('submit_del').addClass('submit_add').text('添加').bind('click', function() {
        InitAddProduct(this, addedproductslist);
    });

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
    } else {
        $("[id$=dlstSearchProducts]").find('tbody:first').prepend(targetTableTR);
    }
    targetTableTR.queue(function() {
        //重置左侧滚动条到顶部
        searchproductslist.scrollTo(0, highlightTime / 2, { queue: false });
        //删除选定SKU
        DelSelectedSKUId(currentTable.attr('skuid'), thisEvent);
        //购物车效果
        $.add2cart(currentTableTR, targetTableTR, currentTableTR.html());
        $(this).dequeue();
    });
}

function InitAddProduct(send, targetContext) {
    $(send).unbind('click'); //撤销事件, 防止恶意点击
    var currentTableTR = $(send).parents('tr:last');
    var currentTable = currentTableTR.find('table');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_add').removeClass('submit_add').addClass('submit_del').text('删除').bind('click', function() {
        InitDelProduct(this, searchproductslist);
    });

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
    } else {
        $("[id$=dlstAddedProducts]").find('tbody:first').prepend(targetTableTR);
    }
    targetTableTR.queue(function() {
        //重置右侧滚动条到顶部
        addedproductslist.scrollTo(0, highlightTime / 2, { queue: false });
        //追加选定SKU
        AddSelectedSKUId(currentTable.attr('skuid'), thisEvent);
        //购物车效果
        $.add2cart(currentTableTR, targetTableTR, currentTableTR.html());
        $(this).dequeue();
    });
}



//} ());

