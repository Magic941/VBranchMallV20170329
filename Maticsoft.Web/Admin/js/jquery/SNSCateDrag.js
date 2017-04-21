
var sitemapHistory = {
    stack: new Array(),
    temp: null,
    //takes an element and saves it's position in the sitemap.
    //note: doesn't commit the save until commit() is called!
    //this is because we might decide to cancel the move
    saveState: function (item) {
        sitemapHistory.temp = { item: $(item), itemParent: $(item).parent(), itemAfter: $(item).prev() };
    },
    commit: function () {
        if (sitemapHistory.temp != null) sitemapHistory.stack.push(sitemapHistory.temp);
    },
    //restores the state of the last moved item.
    restoreState: function () {
        var h = sitemapHistory.stack.pop();
        if (h == null) return;
        if (h.itemAfter.length > 0) {
            h.itemAfter.after(h.item);
        } else {
            h.itemParent.prepend(h.item);
        }
        //checks the classes on the lists
        $('#sitemap li.sm2_liOpen').not(':has(li)').removeClass('sm2_liOpen');
        $('#sitemap li:has(ul li):not(.sm2_liClosed)').addClass('sm2_liOpen');
    }
};

function LoadData(CID,type) {
    $.ajax({
        url: "/SNSCategories.aspx",
        type: 'post',
        dataType: 'json',
        async:false,
        timeout: 10000,
        data: {
            action: "GetCategoryInfo",
            CID: CID,
            Type: type
        },
        success: function (resultData) {
            if (resultData.STATUS == "Success") {
                $.each(resultData.DATA, function (i, n) {
                    var hasChildrenCss = 'sm2_liClosed';
                    var one = '<li class="{0}" pid="{3}" ci="{4}" ><dl class="sm2_s_published ui-droppable"  i="panel"><a class="sm2_expander">&nbsp;</a><dt><a class="sm2_title">【{4}】{1}</a></dt><dd class="sm2_actions"><span class="sm2_move" title="排序">排序</span><span class="sm2_delete" title="删除" id="{4}">删除</span></dd><dd class="sm2_status"  style="top: 8px;"><a  style="margin-right: 300px;text-align: left;color: black;cursor: default;display: none;">{5}</a><a href="Modify.aspx?id={4}" title="编辑"  >编辑</a></dd></dl>{2}</li>'; //sm2_liOpen
                    if (!n.HasChildren) {
                        var tmp = one.format('', n.Name, '', 0, n.CategoryId);
                        $("#sitemap").append(tmp);
                    } else {
                        $("#sitemap").append(one.format(hasChildrenCss, n.Name, CreateCTable(n.CategoryId, type), 0, n.CategoryId));
                    }
                });
            }
        }
    });
}
function CreateCTable(CID,Type) {
    var res = '';
    $.ajax({
        url: "/SNSCategories.aspx",
        type: 'post',
        dataType: 'json',
        async: false,
        timeout: 10000, 
        data: {
            action: "GetCategoryInfo",
            CID: CID,
            Type: Type
        },
        success: function (resultData) {
            if (resultData.STATUS == "Success") {
                $.each(resultData.DATA, function (i, n) {
                    var hasChildrenCss = 'sm2_liClosed';
                    var one = '<ul><li class="{0}" pid="{3}"" ci="{4}"><dl class="sm2_s_published ui-droppable"  i="panel"><a class="sm2_expander">&nbsp;</a><dt><a class="sm2_title">【{4}】{1}</a></dt><dd class="sm2_actions"> <span class="sm2_move" title="排序">排序</span><span class="sm2_delete" title="删除" id="{4}">删除</span></dd><dd class="sm2_status"  style="top: 8px;"><a  style="margin-right: 300px;text-align: left;color: black;cursor: default;display: none;">{5}</a><a href="Modify.aspx?id={4}" title="编辑" >编辑</a></dd></dl>{2}</li></ul>'; //sm2_liOpen
                    var two = '';
                    if (!n.HasChildren) {
                        res += one.format('', n.Name, '', n.ParentID, n.CategoryId);
                    } else {
                        res += one.format(hasChildrenCss, n.Name, CreateCTable(n.CategoryId, Type), n.ParentID, n.CategoryId);
                    }
                });
            }
        }
    });
    return res;
}
//init functions
$(function () {
    $("[id$='btnDelete']").click();
    var type = $("[id$='txtType']").val();
    LoadData(0, type);
    $(".sm2_move").hide();
    $('#sitemap li').prepend('<div class="dropzone"></div>');
    //    $('#sitemap .dropzone').droppable({
    //        accept: '#sitemap li',
    //        tolerance: 'pointer',
    //        drop: function (e, ui) {
    //            var li = $(this).parent();
    //            var child = !$(this).hasClass('dropzone');
    //            if (child && li.children('ul').length == 0) {
    //                li.append('<ul/>');
    //            }
    //            //  li.attr('pid')
    //            if (ui.draggable.attr('pid') == li.attr('pid')) {
    //                if (!$(this).attr('i')) {
    //                    if (child) {
    //                        li.addClass('sm2_liOpen').removeClass('sm2_liClosed').children('ul').append(ui.draggable);
    //                    }
    //                    else {
    //                        $.ajax({
    //                            url: "/SNSCategories.aspx",
    //                            type: 'post',
    //                            dataType: 'json',
    //                            timeout: 10000,
    //                            async: false,
    //                            data: {
    //                                action: "SwaoSequence",
    //                                FID: ui.draggable.attr('ci'),
    //                                TID: li.attr('ci')
    //                            },
    //                            success: function (resultData) {
    //                                if (resultData.STATUS == "Success") {
    //                                    li.before(ui.draggable);
    //                                }
    //                            }
    //                        });
    //                    }
    //                } else {
    //                    alert('不允许跨分类进行排序');
    //                }
    //            } else {
    //                alert('不允许跨分类进行排序');
    //            }

    //            $('#sitemap li.sm2_liOpen').not(':has(li:not(.ui-draggable-dragging))').removeClass('sm2_liOpen');
    //            li.find('dl,.dropzone').css({ backgroundColor: '', borderColor: '' });
    //            sitemapHistory.commit();
    //        },
    //        over: function () {
    //            //$(this).filter('dl').css({ backgroundColor: '#ccc' });
    //            $(this).filter('.dropzone').css({ borderColor: '#aaa' });
    //        },
    //        out: function () {
    //            //$(this).filter('dl').css({ backgroundColor: '' });
    //            $(this).filter('.dropzone').css({ borderColor: '' });
    //        }
    //    });
    //    $('#sitemap li').draggable({
    //        handle: ' > dl',
    //        opacity: .8,
    //        addClasses: false,
    //        helper: 'clone',
    //        zIndex: 100,
    //        start: function (e, ui) {
    //            sitemapHistory.saveState(this);
    //        }
    //    });
    $('.sitemap_undo').click(sitemapHistory.restoreState);
    $(document).bind('keypress', function (e) {
        if (e.ctrlKey && (e.which == 122 || e.which == 26))
            sitemapHistory.restoreState();
    });
    $('.sm2_expander').live('click', function () {
        $(this).parent().parent().toggleClass('sm2_liOpen').toggleClass('sm2_liClosed');
        return false;
    });

    $('.sm2_delete').live('click', function () {
        if (confirm('删除分类会删除该分类下所有子分类，确定要删除选择的分类吗？')) {
            var currentLi = $(this);
            $.ajax({
                url: "/SNSCategories.aspx",
                type: 'post',
                dataType: 'json',
                timeout: 10000,
                data: {
                    action: "DeleteCategory",
                    CID: $(this).attr('id')
                },
                success: function (resultData) {
                    if (resultData.STATUS == "Success") {
                        alert("删除成功！");
                        currentLi.parent().parent().parent().remove();
                    } else {
                        alert("删除失败，请稍后再试！");
                    }
                }
            });
        }
    });
});

