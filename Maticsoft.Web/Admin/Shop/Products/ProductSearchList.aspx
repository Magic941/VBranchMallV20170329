<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductSearchList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductSearchList" %>

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
            String.prototype.format = function (args) {
                if (arguments.length > 0) {
                    var result = this;
                    if (arguments.length == 1 && typeof (args) == "object") {
                        for (var key in args) {
                            var reg = new RegExp("({" + key + "})", "g");
                            result = result.replace(reg, args[key]);
                        }
                    }
                    else {
                        for (var i = 0; i < arguments.length; i++) {
                            if (arguments[i] == undefined) {
                                return "";
                            }
                            else {
                                var reg = new RegExp("({[" + i + "]})", "g");
                                result = result.replace(reg, arguments[i]);
                            }
                        }
                    }
                    return result;
                }
                else {
                    return this;
                }
            }


            grid = manager = $("#list").ligerGrid({
                columns: [
                { display: '商品编号', name: 'ProductCode', width: 100, type: 'text' },
                {
                    display: '商品名称', name: 'ProductName', width: 500, align: 'left'
                }
                ], url: '/Ajax_Handle/ProductFreight.ashx', parms: { Action: "ProductList", Value1: '', Value2: '' },
                usePager: true, pageSize: 10, rownumbers: true,checkbox:true,width: '630px'
            });
        });

        function f_select() {
            var rows = manager.getCheckedRows();
            return rows;
        }

        function search() {
            var manager = $("#list").ligerGrid({ parms: { Value1: $("#txt_ProductCode").val(), Value2: $("#txt_ProductName").val(), Action: "ProductList" } });
            grid.loadData(true);
        }
    </script>
</head>
<body>
    <div class="newslist_title" style="margin:10px; border:1px solid #ccc; background-color:#fff; padding:15px 10px;">
        商品编号：<input type="text" id="txt_ProductCode" />&nbsp;&nbsp;商品名称：<input type="text" id="txt_ProductName" style="width:300px;" />&nbsp;&nbsp;<input type="button" value="查询"  style="width:80px; height:25px;"  onclick="search();" /> 
    </div>
    <div style="margin:10px; border:1px solid #ccc; background-color:#fff; padding:15px 10px;">
        <div id="list"></div>
    </div>
</body>
</html>
