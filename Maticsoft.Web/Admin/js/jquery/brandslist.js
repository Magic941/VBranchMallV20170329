var thisobjInfo;
var typeId = -1;
var AllpageSize = 10;
var selectTabNum = 0;
$(document).ready(function () {
    nTabs(null, 0, -1);
    //全选
    $("#btnSelect").click(function () {
        var thisSelected = $(this).val() == "全 选";
        $("#tbBrandsList tr:gt(0) input[type='checkbox']").attr("checked", thisSelected);
        //$("#tbBrandsList tr:gt(0)")[thisSelected ? "addClass" : "removeClass"]("selected");
        $(this).val([thisSelected ? "反 选" : "全 选"]);
        $(this).attr("title", [thisSelected ? "反 选" : "全 选"]);
    });
    //删除之后总是得到第一页数据
    $("#linkLi").click(function () {
        if (confirm('确定要删除数据吗？')) {
            var Select = $("#tbBrandsList tr:gt(0) input[type='checkbox']:checked");
            DeleteSelect(Select);
        }
    });
    $("#btnDelete").click(function () {
        if (confirm('确定要删除数据吗？')) {
            var Select = $("#tbBrandsList tr:gt(0) input[type='checkbox']:checked");
            DeleteSelect(Select);
        }
    });
    // 查询
    $("#btnQuery").click(function () {
        searchPagedList(-1, 1, 0, $("#txtBrandName").val(), $("#txtBrandSpell").val());
    });
});

function DeleteSelect(obj) {
    if (obj.length == 0) {
        ShowFailTip("没有选择数据!");
        return;
    }
    var selectList = "";
    obj.each(function () {
        selectList += "-" + $(this).attr("id");
    });
    $.ajax({
        url: "/Shopmanage.aspx",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        data: {
            ProductTypeId: typeId,
            action: "DeleteBrands",
            idList: selectList
        },
        beforeSend: function () {
            $("#div_load").show();
        },
        complete: function () {
            $("#div_load").hide();
        },
        success: function (resultData) {
            if (resultData == "SUCCESS") {
                //删除成功
                loadPagedList(typeId, 1, selectTabNum);
                ShowSuccessTip("删除成功！");
            } else {
                //删除失败
                ShowFailTip("删除失败，请稍后再试!");
            }
        }
    });
}

function DeleteSelectBid(thisId) {
    if (confirm('确定要删除数据吗？')) {
        if (!thisId) {
            ShowFailTip("没有选择数据!");
            return;
        }
        $.ajax({
            url: "/Shopmanage.aspx",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            data: {
                ProductTypeId: typeId,
                action: "DeleteBrands",
                idList: thisId
            },
            beforeSend: function () {
                $("#div_load").show();
            },
            complete: function () {
                $("#div_load").hide();
            },
            success: function (resultData) {
                if (resultData.STATUS == "SUCCESS") {
                    //删除成功
                    loadPagedList(typeId, 1, selectTabNum);
                    ShowSuccessTip("删除成功！");
                } else {
                    //删除失败
                    ShowFailTip(resultData.DATA);
                }
            }
        });
    }
}

function nTabs(thisObj, Num, TypeId) {
    if (thisObj) {
        $("#btnSelect").val("全 选");
        thisobjInfo = thisObj;
        typeId = TypeId;
        selectTabNum = Num;
        if (thisObj.className == "active") return;
        var tabObj = thisObj.parentNode.id;
        var tabList = document.getElementById(tabObj).getElementsByTagName("li");
        for (i = 0; i < tabList.length; i++) {
            if (i == Num) {
                thisObj.className = "active";
            } else {
                tabList[i].className = "normal";
            }
        }
    }
    var pageBarDiv = $("#pageBar");
    pageBarDiv.find('a').remove();
    $.ajax({
        url: "/Shopmanage.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "GetBrandsList", ProductTypeId: TypeId, pageIndex: 1, pageSize: AllpageSize, TabNum: Num },
        beforeSend: function () { $("#div_load").show(); },
        complete: function () { $("#div_load").hide(); },
        success: function (resultData) {
            createRows(resultData.DATA, resultData.PicServerUrl);
            if (resultData.rowCount > AllpageSize) {
                createPageBar(1, resultData.rowCount, resultData.pageCount);
            }
        }
    });
}

function loadPagedList(TypeId, pi, num) {
    var pageBarDiv = $("#pageBar");
    $("#btnSelect").val("全 选");
    pageBarDiv.find('a').remove();
    pageBarDiv.find('br').remove();
    $.ajax({
        url: "/Shopmanage.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "GetBrandsList", ProductTypeId: TypeId, pageIndex: pi, pageSize: AllpageSize, TabNum: num },
        beforeSend: function () { $("#div_load").show(); },
        complete: function () { $("#div_load").hide(); },
        success: function (resultData) {
            createRows(resultData.DATA, resultData.PicServerUrl);
            if (resultData.rowCount > AllpageSize) {
                createPageBar(pi, resultData.rowCount, resultData.pageCount);
            }
        }
    });
}

function searchPagedList(TypeId, pi, num, name, spell)
{
    var pageBarDiv = $("#pageBar");
    $("#btnSelect").val("全 选");
    pageBarDiv.find('a').remove();
    pageBarDiv.find('br').remove();
    $.ajax({
        url: "/Shopmanage.aspx",
        type: 'post', dataType: 'json', timeout: 10000,
        data: { action: "SearchBrandsList", ProductTypeId: TypeId, pageIndex: pi, pageSize: AllpageSize, TabNum: num, BrandName:name, BrandSpell:spell },
        beforeSend: function () { $("#div_load").show(); },
        complete: function () { $("#div_load").hide(); },
        success: function (resultData) {
            createRows(resultData.DATA, resultData.PicServerUrl);
            if (resultData.rowCount > AllpageSize) {
                createPageBar(pi, resultData.rowCount, resultData.pageCount);
            }
        }
    });
}

//根据数据数组，添加表格行列
function createRows(jsonArr,picServerUrl) {
    var tbody = "";
    var MainTable = $("#tbBrandsList");
    MainTable.find('td').parent().remove();
    $.each(jsonArr, function (i, n) {
        var trs = "";
        trs += "<tr  height='27px' style=' background: #FFFFFF' >";
        trs += "<td style='text-align:center;display:none;' ><input id='" + n.BrandId + "' type='checkbox' /></td>";
        trs += "<td valign='middle' style='padding-left: 5px; height: 27px;'> " + n.DisplaySequence + " </td> ";
        trs += "<td align='center' style='padding-left: 5px; height: 27px;'> " + n.BrandName + " </td>";
        trs += "<td align='center' style='padding-left: 5px; height: 27px;'> <img src='"+ picServerUrl +  n.Logo + "' width='80' height='47' /> </td>";
        trs += "<td align='left' height='27px'> " + cutstr(n.Description, 100) + " </td>";
        trs += "<td align='center' style='padding-left: 5px; height: 27px;'> <a href='Show.aspx?id=" +  n.BrandId + "' style='display: inline-block; width: 50px;'>详细</a>&nbsp;<a href='Modify.aspx?id=" + n.BrandId + "' class='modify' style='display: inline-block; width: 50px;'>编辑</a>&nbsp;<a href='javascript:void(0);' style='display: inline-block; width: 50px;'  class='delete'  onclick='DeleteSelectBid(" + n.BrandId + ")'>删除</a>  </td>";
        trs += "</tr>";
        tbody += trs;
    });
    MainTable.append(tbody);

    //功能行为   隐藏按钮
    if ($('[id$="hidModifybtn"]').val() == "hidden") {
        $('a.modify').css('display', 'none');
    }
    if ($('[id$="hidDelbtn"]').val() == "hidden") {
        $('a.delete').css('display', 'none');
    }
}

//分页
function createPageBar(pageIndex, rowcount, pagecount) {
    var pageBarDiv = $("#pageBar");
    pageBarDiv.find('a').remove();
    pageBarDiv.find('br').remove();
    var typeid = typeId;
    pageBarDiv.append("<br/><a href='javascript:loadPagedList(" + typeId + ",1," + selectTabNum + ")'>首页</a> <a href='javascript:loadPagedList(" + typeId + "," + prevPage(pageIndex) + "," + selectTabNum + ")'>上一页</a> <a href='javascript:loadPagedList(" + typeId + "," + lastPage(pageIndex, pagecount) + "," + selectTabNum + ")'>下一页</a> <a href='javascript:loadPagedList(" + typeId + "," + pagecount + "," + selectTabNum + ")'>尾页</a><a href='javascript:void(0)'>     共" + pagecount + "页</a><br/>");
}

//上一页
function prevPage(pageIndex) {
    if (pageIndex > 1) return pageIndex - 1;
    else return 1;
}
//下一页
function lastPage(pageIndex, pageCount) {
    if (pageIndex < pageCount) return pageIndex + 1;
    else return pageCount;
}

//截取字符串
function cutstr(str, len) {
    var str_length = 0;
    var str_len = 0;
    if (!str) {
        return "";
    }
    str_cut = new String();
    str_len = str.length;
    for (var i = 0; i < str_len; i++) {
        a = str.charAt(i);
        str_length++;
        if (escape(a).length > 4) {
            //中文字符的长度经编码之后大于4
            str_length++;
        }
        str_cut = str_cut.concat(a);
        if (str_length >= len) {
            str_cut = str_cut.concat("...");
            return str_cut;
        }
    }
    //如果给定字符串小于指定长度，则返回源字符串；
    if (str_length < len) {
        return str;
    }
}