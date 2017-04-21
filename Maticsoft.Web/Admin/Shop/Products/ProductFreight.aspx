<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFreight.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductFreight" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../js/lib/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../js/lib/json2.js" type="text/javascript"></script> 
    <script src="../../js/lib/ligerUI/js/ligerui.all.js"></script>

    <script type="text/javascript">
        var grid, manager;        $(function () {
            grid = manager = $("#list").ligerGrid({
                columns: [
                { display: '商品编号', name: 'ProductCode', width: 200, type: 'text' },
                {
                    display: '商品名称', name: 'ProductName', width: 500, align: 'left', type: 'text'
                },
                { display: '运费', name: 'Freight', width: 100, type: 'float', editor: { type: 'float' } },
                {
                    display: '编辑者', name: 'Eidtor', width: 100, align: 'center'
                },
                {
                    display: '操作', isSort: false, width: 120, render: function (rowdata, rowindex, value) {
                         return "<a href='javascript:deleteRow(" + rowindex + ")'>删除</a> ";
                    }
                }
                ], url: '/Ajax_Handle/ProductFreight.ashx', parms: { Action: "GetFreightList" }, sortName: 'ProductCode',
                enabledEdit: true, clickToEdit: true, usePager: true, pageSize: 20, rownumbers: true,
                width: '1050px',
                onAfterEdit: f_onAfterEdit
            });
        });


        //打开定额库
        var dia1;
        var rid = null;
        function f_import() {
            dia1 = $.ligerDialog.open({
                title: '选择商品', width: 760, height: 470, showMax: true, modal: true, url: 'ProductSearchList.aspx', buttons: [
                    { text: '确定', onclick: f_importOK },
                    { text: '取消', onclick: f_importCancel }
                ]
            });
        }
        function f_importOK(item, dialog) {
            var result = true;
            var rows = dialog.frame.f_select();
            if (!rows) {
                alert('请选择行!');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                //验证商品是否已存在
                $.ajax({
                    type: 'POST', async: false, url: '/Ajax_Handle/ProductFreight.ashx', data: { Value1: rows[i]["ProductCode"], Action: "IsExist" }, success: function (data) {
                        if (data == '1') {
                                $.ligerDialog.warn("商品/" + rows[i]["ProductCode"] + "/已存在！");
                                result = false;
                        }
                    }
                });
            }

            if (result == true)
            {
                for (var i = 0; i < rows.length; i++) {
                    $.ajax({
                        type: 'POST', async: false, url: '/Ajax_Handle/ProductFreight.ashx', data: { Value1: rows[i]["ProductCode"], Value2: "", Value3: "0", Value4: "0", Action: "AddFreight" }, success: function (data) {
                            if (data == 1) {
                                manager.add({
                                    ProductCode: rows[i]["ProductCode"],
                                    ProductName: rows[i]["ProductName"],
                                    Freight: 0
                                });
                            }
                            else {
                                $.ligerDialog.error("商品/" + rows[i]["ProductCode"] + "/添加失败！");
                                return;
                            }
                        }
                    });
                }
                dialog.close();
            }
        }

        function f_importCancel(item, dialog) {
            dialog.close();
        }

        function deleteRow(rowid) {
            $.ligerDialog.confirm('确定要删除吗？', "系统提示", function (result) {
                if (result == true) {
                    var row = manager.getSelectedRow();
                    $.post("/Ajax_Handle/ProductFreight.ashx", { Value1: row.ProductCode, Action: "DeleteFreight" }, function (data) {
                        if (data == 1) {
                            manager.deleteRow(rowid);
                            $.ligerDialog.success("删除成功！");
                        }
                        else if (data == 0) {
                            $.ligerDialog.error("删除失败！");
                        }
                    });
                }
            });
        }

        function f_onAfterEdit(e) {
            $.post("/Ajax_Handle/ProductFreight.ashx", { Value1: e.record["ProductCode"], Value2: e.record["Freight"], Action: "UpdateFreight" }, function (data) {
                if (data != 1) {
                    $.ligerui.error("更新失败！");
                }
            });
        }

        function search() {
            var manager = $("#list").ligerGrid({ parms: { Value1: $("#txt_ProductCode").val(), Value2: $("#txt_ProductName").val(), Action: "GetFreightList" } });
            grid.loadData(true);
        }
    </script>
</head>
<body>
    <div class="newslist_title" style="margin:10px; border:1px solid #ccc; background-color:#fff; padding:15px 10px;">
        商品编号：<input type="text" id="txt_ProductCode" />&nbsp;&nbsp;商品名称：<input type="text" id="txt_ProductName" />&nbsp;&nbsp;<input type="button" value="查询" style="width:80px; height:25px;" onclick="search();"/>&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="btn_ref"   style="width:80px; height:25px;"  value="新增运费" onclick="    f_import();" />
    </div>

    <div style="margin:10px; border:1px solid #ccc; background-color:#fff; padding:15px 10px;">
        <div id="list"></div>
    </div>
</body>
</html>
