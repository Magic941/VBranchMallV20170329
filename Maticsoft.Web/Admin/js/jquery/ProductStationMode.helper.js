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
        var GoodType = $("#ctl00_ContentPlaceHolder1_ddlGoodType").val();
        insertProductStationMode(pid, $.getUrlParam('type'), GoodType);
    });
    $('.submit_del').bind('click', function () {
        InitDelProduct(this, searchproductslist);
        //删除
        //        var currentTableTR = $(this).parents('tr:last');
        //        var currentTable = currentTableTR.find('table');
        var pid = $(this).attr('skuid');
        RemoveProductStationMode(pid, $.getUrlParam('type'));
    });

    $("[id$='btnClear']").click(function () {
        $($(window.parent.document.body)).find("[id$='hfRelatedProducts']").val('');
        $($(window.parent.document.body)).find("[id$='hfSelectedAccessories']").val('');
    });
});
//function GetSelectedSKUId() {
//    var tmpSkus = '';
//    //获取当前页选择的内容. *)未分页js添加数据
//    addedproductslist.find('[skuid]').each(function () {
//        tmpSkus += $(this).attr('skuid') + ',';
//    });
//    //父窗体隐藏域存储当前全部选择内容, 含分页
//    var parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');
//    tmpSkus += parentSelectedAccessories.val();
//    if (parentSelectedAccessories) {
//        //去重换转并双向输出到父窗体和当前窗体
//        tmpSkus = tmpSkus.split(',').distinct().join(",");
//        parentSelectedAccessories.val(tmpSkus);
//        $('[id$=hfSelectedData]').val(tmpSkus);
//    }
//}

//添加
function insertProductStationMode(productId, type, GoodTypeValue) {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { Action: "InsertProductStationMode", Callback: "true", ProductId: productId, Type: type, GoodType: GoodTypeValue },
        async: false,
        success: function (resultData) {
            if (resultData.STATUS == "Presence") {
                alert("该商品已存在！");
                return false;
            }
        }
    });
}
//删除
function RemoveProductStationMode(productId, type) {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { Action: "RemoveProductStationMode", Callback: "true", ProductId: productId, Type: type },
        async: false,
        success: function (resultData) {

        }
    });
}


function AddSelectedSKUId(skuid, thisEvent) {
    //双向追加到父窗体和当前窗体
    var hfSelectedData = $('[id$=hfSelectedData]');
    var parentSelectedAccessories;//  $(window.parent.document).find('[id$=hfSelectedAccessories]');
    if ($($(thisEvent).parents()).find('#relatedProc').length > 0) {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfRelatedProducts]');
    } else {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');
    }


    if (!parentSelectedAccessories || parentSelectedAccessories.length < 1) {
        alert('ERROR: [404] ParentSelectedAccessories Not Found!');
        return;
    }
    var tmpSKU = parentSelectedAccessories.val() + skuid + ',';
    parentSelectedAccessories.val(tmpSKU);
    hfSelectedData.val(tmpSKU);
}

function DelSelectedSKUId(skuid, thisEvent) {
    //双向删除到父窗体和当前窗体
    var hfSelectedData = $('[id$=hfSelectedData]');
    var parentSelectedAccessories;//= $(window.parent.document).find('[id$=hfSelectedAccessories]');
    if ($($(thisEvent).parents()).find('#relatedProc').length > 0) {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfRelatedProducts]');
    } else {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');
    }



    if (!parentSelectedAccessories || parentSelectedAccessories.length < 1) {
        alert('ERROR: [404] ParentSelectedAccessories Not Found!');
        return;
    }
    var skus = parentSelectedAccessories.val().split(',');
    if (skus) {
        //delete skuid
        skus.remove(skus.getIndexByValue(skuid));
    }
    var tmpSKU = skus.join(",");
    parentSelectedAccessories.val(tmpSKU);
    hfSelectedData.val(tmpSKU);
}

function InitDelProduct(send, targetContext) {
    $(send).unbind('click'); //撤销事件, 防止恶意点击
    var currentTableTR = $(send).parents('tr:last');
    var currentTable = currentTableTR.find('table');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_del').removeClass('submit_del').addClass('submit_add').text('添加').bind('click', function () {
        InitAddProduct(this, addedproductslist);
    });

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
    } else {
        $("[id$=dlstSearchProducts]").find('tbody:first').prepend(targetTableTR);
    }
    targetTableTR.queue(function () {
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

    targetTableTR.find('.submit_add').removeClass('submit_add').addClass('submit_del').text('删除').bind('click', function () {
        InitDelProduct(this, searchproductslist);
    });

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
    } else {
        $("[id$=dlstAddedProducts]").find('tbody:first').prepend(targetTableTR);
    }
    targetTableTR.queue(function () {
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

