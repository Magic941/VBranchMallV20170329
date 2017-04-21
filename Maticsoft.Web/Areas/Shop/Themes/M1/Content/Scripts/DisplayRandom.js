/*
随机显示UL中li
ObjNode:$(ul)对像
iPageSize:随机显示数
*/
var DisplayRandom = {
    init: function (ObjNode, iPageSize) {
        var ObjRow = ObjNode.find("li");
        var iRowCount = ObjRow.length;
        var iPageCount = Math.floor((iRowCount % iPageSize == 0 ? iRowCount / iPageSize : iRowCount / iPageSize + 1));
        var iPageIndex = Math.floor(Math.random() * iPageCount) + 1;
        var iRowIndex = (iPageIndex - 1) * iPageSize;
        var iHasDisplayCount = iPageSize;
        for (; iRowIndex < iRowCount; iRowIndex++) {
            if (iHasDisplayCount-- != 0) {
                ObjRow.eq(iRowIndex).removeClass("none").show();
                if (iRowIndex == iRowCount - 1) iRowIndex = 0;
            } else {
                break;
            }
        }
    },
    initv2: function (ObjNode, iPageSize) {
        var ObjRow = ObjNode.find("li[litype!='custom']");
        var iRowCount = ObjRow.length;
        var iPageCount = Math.floor((iRowCount % iPageSize == 0 ? iRowCount / iPageSize : iRowCount / iPageSize + 1));
        var iPageIndex = Math.floor(Math.random() * iPageCount) + 1;
        var iRowIndex = (iPageIndex - 1) * iPageSize;
        var iHasDisplayCount = iPageSize;
        for (; iRowIndex < iRowCount; iRowIndex++) {
            if (iHasDisplayCount-- != 0) {
                ObjRow.eq(iRowIndex).removeClass("none").show();
                if (iRowIndex == iRowCount - 1) iRowIndex = 0;
            } else {
                break;
            }
        }
    }
}