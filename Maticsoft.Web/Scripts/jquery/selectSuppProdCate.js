/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  hhy  变更内容
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
    //正向加载分类信息
    var supplierid = parseInt($('#hidsupplierid').val());
    var suppParentCateId = parseInt($('#hidSuppParentCategoryId').val());
    var suppcateId = parseInt($('#hidSuppCategoryId').val());
    if (suppParentCateId > 0) {//当前分类有父分类
        LoadCate(0, $("#selectSuppParentCate"), supplierid); //加载父分类
        $("#selectSuppParentCate").val(suppParentCateId); //选中父分类
        LoadCate(suppParentCateId, $("#selectSuppChildCate"), supplierid); //加载当前分类
        $("#selectSuppChildCate").val(suppcateId);
        $("#selectSuppChildCate").show();
    } else {//无父分类
        if (suppcateId <= 0) {
            suppcateId = 0; //第一次加载无任何值时
        }
        LoadCate(0, $("#selectSuppParentCate"), supplierid);
        $("#selectSuppParentCate").val(suppcateId); 
    }
    //选择店铺商品分类的时候动态加载子分类 
    $("#selectSuppParentCate").change(function () {
        $('#hidSuppCategoryId').val($(this).val());
        if ($(this).val() > 0) {
            LoadCate($(this).val(), $("#selectSuppChildCate"), supplierid);
        } else {
            $('#selectSuppChildCate').hide();
            $('#selectSuppChildCate').empty();
            $('#hidSuppParentCategoryId').val(0);
        }
    });
    $('#selectSuppChildCate').change(function () {
        if ($(this).val() > 0) {
            $('#hidSuppCategoryId').val($(this).val());
            $('#hidSuppParentCategoryId').val($("#selectSuppParentCate").val());
        } else {
            $('#hidSuppCategoryId').val($("#selectSuppParentCate").val());
            $('#hidSuppParentCategoryId').val(0);
        }
    });
});



//加载店铺商品分类   parentId  父节点id
function LoadCate(parentId, $this, suppId) {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: { Action: "GetSuppCateNode", ParentId: parentId, SuppId: suppId },
        async: false,
        success: function (resultData) {
            $this.empty();
            $this.show();
            $("<option value='0'>请选择</option>").appendTo($this);
            switch (resultData.STATUS) {
                case "OK":
                    $(resultData.DATA).each(function () {
                        var value, name;
                        value = this['CategoryId'];
                        name = this['Name'];
                        $("<option value='" + value + "'>" + name + "</option>").appendTo($this);
                    });
                    break;
                default:
                    break;
            }
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            alert(xmlHttpRequest.responseText);
        }
    });
}

//根据子分类反查父分类
//function GetParendid(childdepartid) {
//    var parendid = "-1";
//    $.ajax({
//        url: "/ZSHandler.aspx",
//        type: 'post',
//        dataType: 'json',
//        timeout: 10000,
//        data: { Table: "Departments", Action: "GetParentDepart", ChildDepart: childdepartid },
//        async: false,
//        success: function (resultData) {
//            switch (resultData.STATUS) {
//                case "SUCCESS":
//                    parendid = resultData.DATA;
//                    break;
//                default:
//                    break;
//            }
//        },
//        error: function (xmlHttpRequest, textStatus, errorThrown) {
//            alert(xmlHttpRequest.responseText);
//        }
//    });
//    return parendid;
//}