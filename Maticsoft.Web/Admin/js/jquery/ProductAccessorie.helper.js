//(function () {
var baseAddTable = '<table id="dlstAddedProducts" cellspacing="0" border="0" style="width:96%;border-collapse:collapse;"><tbody></tbody></table>';
var baseDelTable = '<table id="dlstSearchProducts" cellspacing="0" border="0" style="width:96%;border-collapse:collapse;"><tbody></tbody></table>';
var baseRelatedInput = '<div class="relatedInfo"><input type="radio" name="relationproducts{0}" value="0" id="singleRela_{0}" checked="checked"/><label for="singleRela_{0}"  i="{0}"  value="0" onclick="relatedChanged(this)">单向关联</label><input type="radio" name="relationproducts{0}" value="1" id="doubleRela_{0}" /><label for="doubleRela_{0}" i="{0}"  value="1" onclick="relatedChanged(this)">双向关联</label></div>'

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
    });
    $('.submit_del').bind('click', function () {
        InitDelProduct(this, searchproductslist);
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
function AddSelectedSKUId(skuid, thisEvent) {
    //双向追加到父窗体和当前窗体
    var hfSelectedData = $('[id$=hfSelectedData]');
    var parentSelectedAccessories;//  $(window.parent.document).find('[id$=hfSelectedAccessories]');
//    if ($($(thisEvent).parents()).find('#relatedProc').length>0) {
//        parentSelectedAccessories = $(window.parent.document).find('[id$=hfRelatedProducts]');
//    } else {
//        parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');
//    }

//    var selectProductsInfo;
//    if ($($(thisEvent).parents()).find('#relatedProc').length > 0) {
//        selectProductsInfo = $(window.parent.document).find('[id$=HiddenField_RelatedProductInfo]');
//    } else {
//        selectProductsInfo = $(window.parent.document).find('[id$=hfSelectedAccessories]');
//    }

//    if (!parentSelectedAccessories || parentSelectedAccessories.length < 1) {
//        alert('ERROR: [404] ParentSelectedAccessories Not Found!');
//        return;
//    }
//    var tmpSKU = parentSelectedAccessories.val() + skuid + ',';
//    parentSelectedAccessories.val(tmpSKU);
//    hfSelectedData.val(tmpSKU);

//    var hfSelectRelatedData = $('[id$=HiddenField_SelectRelatedData]');
//    if (hfSelectRelatedData.val()) {
//        hfSelectRelatedData.val(hfSelectRelatedData.val() + ',' + skuid + '_0');
//    } else {
//        $("[id$='HiddenField_SelectRelatedData']").val(skuid + '_0' );
//    }
//    //alert($("[id$='HiddenField_SelectRelatedData']").val());
//    selectProductsInfo.val($("[id$='HiddenField_SelectRelatedData']").val());


    if ($($(thisEvent).parents()).find('#relatedProc').length > 0) {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfRelatedProducts]');
        selectProductsInfo = $(window.parent.document).find('[id$=HiddenField_RelatedProductInfo]');

        var tmpSKU = parentSelectedAccessories.val() + skuid + ',';
        parentSelectedAccessories.val(tmpSKU);
        hfSelectedData.val(tmpSKU);

        var hfSelectRelatedData = $('[id$=HiddenField_SelectRelatedData]');
        if (hfSelectRelatedData.val()) {
            hfSelectRelatedData.val(hfSelectRelatedData.val() + ',' + skuid + '_0');
        } else {
            hfSelectRelatedData.val(skuid + '_0');
        }
        selectProductsInfo.val(hfSelectRelatedData.val());
    } else {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');

        var tmpSKU = parentSelectedAccessories.val() + skuid + ',';
        parentSelectedAccessories.val(tmpSKU);
        hfSelectedData.val(tmpSKU);
    }

    if (!parentSelectedAccessories || parentSelectedAccessories.length < 1) {
        alert('ERROR: [404] ParentSelectedAccessories Not Found!');
        return;
    }

}

function relatedChanged(thisEvent) {
    var hfSelectRelatedData = $('[id$=HiddenField_SelectRelatedData]');
    var SelectRelatedDataArray = hfSelectRelatedData.val().split(',');

    for (var i = 0; i <= SelectRelatedDataArray.length - 1; i++) {
        var relatedData = SelectRelatedDataArray[i].split('_');
        if ($(thisEvent).attr('i') == relatedData[0]) {
            SelectRelatedDataArray[i] = $(thisEvent).attr('i') + '_' + $(thisEvent).attr('value');
        }
    }

    var parentSelectedAccessories;
    if ($($(thisEvent).parents()).find('#relatedProc').length > 0) {
        parentSelectedAccessories = $(window.parent.document).find('[id$=HiddenField_RelatedProductInfo]');
    } else {
        parentSelectedAccessories = $(window.parent.document).find('[id$=hfSelectedAccessories]');
    }
    hfSelectRelatedData.val(SelectRelatedDataArray.join(','));
    parentSelectedAccessories.val(SelectRelatedDataArray.join(','));
    
//    alert(hfSelectRelatedData.val());
}

function DelSelectedSKUId(skuid, thisEvent) {
    //双向删除到父窗体和当前窗体
    var hfSelectedData = $('[id$=hfSelectedData]');
    var parentSelectedAccessories ;//= $(window.parent.document).find('[id$=hfSelectedAccessories]');
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
    var currentTableId = currentTableTR.find('table').attr('id');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_del').removeClass('submit_del').addClass('submit_add').text('添加').bind('click', function() {
        InitAddProduct(this, addedproductslist);
    });

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
        $("[id$=dlstSearchProducts]").find('#' + currentTableId + ' .relatedInfo').remove();
    } else {
        $("[id$=dlstSearchProducts]").find('tbody:first').prepend(targetTableTR);
        $("[id$=dlstSearchProducts]").find('#' + currentTableId + ' .relatedInfo').remove();
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
    var currentTableId= currentTableTR.find('table').attr('id');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_add').removeClass('submit_add').addClass('submit_del').text('删除').bind('click', function() {
        InitDelProduct(this, searchproductslist);
    });

    var relatedHtml = baseRelatedInput.format(currentTableId);

    if (targetContext.find('table').length == 0) {
        var tmp = $(baseAddTable);
        tmp.find('tbody').append(targetTableTR);
        targetContext.prepend(tmp);
        $("[id$=dlstAddedProducts]").find('#' + currentTableId + ' .tdmanage').prepend(relatedHtml);
    } else {
        $("[id$=dlstAddedProducts]").find('tbody:first').prepend(targetTableTR);
        $("[id$=dlstAddedProducts]").find('#' + currentTableId + ' .tdmanage').prepend(relatedHtml);
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

