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
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
    //日期控件
    $("[id$='txt_start']").prop("readonly", true).datepicker({
        numberOfMonths: 1, //显示月份数量
        onClose: function () {
            $(this).css("color", "#000");
        }
    }).focus(function () { $(this).val(''); });
    $("[id$='txt_end']").prop("readonly", true).datepicker({
        numberOfMonths: 1, //显示月份数量
        onClose: function () {
            $(this).css("color", "#000");
        }
    }).focus(function () { $(this).val(''); });
    function showBg() {
        var bh = $("body").height();
        var bw = $("body").width();
        $("#divMode").css({
            height: bh,
            width: bw,
            display: "block"
        });
        $("#divMode").show();
    }
    //关闭灰色 jQuery 遮罩 
    function closeBg() {
        $("#fullbg,#dialog").hide();
    }
    addedproductslist = $(".addedproductslist");
    searchproductslist = $(".searchproductslist");

    $('.submit_add').click(function () {
        var current = $(this);
        
        $("body").append("<div id='mask'></div>");
        $("#mask").addClass("mask").fadeIn();
        $("#divMode").fadeIn();

        $("#btnOK").click(function () {
            if (!checkquantity() || !checkdate()) {
                return;
            }
            $("#divMode").fadeOut();
            $("#mask").fadeOut();

            var currentTableTR = current.parents('tr:last');
            var currentTable = currentTableTR.find('table');
            var pid = currentTable.attr('skuid');

            InitAddProduct(current, addedproductslist, $("#txt_quantity").val(), $("#txt_start").val(), $("#txt_end").val());
            insertProductFreightFree(pid, $.getUrlParam('type'), $("#txt_quantity").val(), $("#txt_start").val(), $("#txt_end").val(), $("#UserId").val());
        });
        $("#btnCancel").click(function () {
            $("#divMode").fadeOut();
            $("#mask").fadeOut();
        });
    });
    $('.submit_del').bind('click', function () {
        InitDelProduct(this, searchproductslist);
        //删除
        //        var currentTableTR = $(this).parents('tr:last');
        //        var currentTable = currentTableTR.find('table');
        var pid = $(this).attr('skuid');
        RemoveFreeFreight(pid, $.getUrlParam('type'));
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
function checkquantity() {
    var quantity = $("#txt_quantity").val();
    if (!(/^[0-9]+$/.test(quantity))) {
        alert("数量不能为空且必须为数字");
        return false;
    }
    return true;
}

function checkdate()
{
    var startDate = $("#txt_start").val();
    var endDate = $("#txt_end").val();
    if (startDate == "" || endDate == "") {
        alert("有效期不能为空");
        return false;
    }
    return true;
}
//添加
function insertProductFreightFree(productId, type, quantity, startDate, endDate,UserId) {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { Action: "InsertFreeFreight", Callback: "true", ProductId: productId, Type: type, Quantity: quantity, StartDate: startDate, EndDate: endDate,UserId:UserId},
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
function RemoveFreeFreight(productId, type) {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST', dataType: 'json', timeout: 10000,
        data: { Action: "RemoveFreeFreight", Callback: "true", ProductId: productId, Type: type },
        async: false,
        success: function (resultData) {

        }
    });
}



function InitDelProduct(send, targetContext) {
    $(send).unbind('click'); //撤销事件, 防止恶意点击
    var currentTableTR = $(send).parents('tr:last');
    var currentTable = currentTableTR.find('table');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_del').removeClass('submit_del').addClass('submit_add').text('添加').bind('click', function () {
        InitAddProduct($(this), addedproductslist);
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
        //购物车效果
        $.add2cart(currentTableTR, targetTableTR, currentTableTR.html());
        $(this).dequeue();
    });
}

function InitAddProduct(send, targetContext, quantity, start, end) {
    
    send.unbind('click'); //撤销事件, 防止恶意点击
    var currentTableTR = send.parents('tr:last');
    var currentTable = currentTableTR.find('table');
    var targetTableTR = currentTableTR.clone();

    var thisEvent = targetContext;

    targetTableTR.find('.submit_add').removeClass('submit_add').addClass('submit_del').text('删除').bind('click', function () {
        $("td", targetTableTR.find("tr")[1]).first()[0].innerHTML = "<span class=\"colorC\"></span>";
        InitDelProduct(this, searchproductslist);
    });

    var html = "<span class=\"colorC\">免邮数量：" + quantity + "</span><br />";
    html += "<span class=\"colorC\">有效日期：" + start + " - " + end + "</span><br />";
    $("td", targetTableTR.find("tr")[1]).first()[0].innerHTML = html;


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
        //购物车效果
        $.add2cart(currentTableTR, targetTableTR, currentTableTR.html());
        $(this).dequeue();
    });
}



//} ());

