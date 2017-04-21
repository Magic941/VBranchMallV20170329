<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Modify1.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Modify1" %>
    
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register Assembly="Maticsoft.Controls" Namespace="Maticsoft.Controls" TagPrefix="Maticsoft" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/MasterPage<%=Session["Style"] %>.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/admin.css" type="text/css" rel="stylesheet" charset="utf-8">
    <link type="text/css" href="/admin/js/msgbox/css/msgbox.css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript" src="/admin/js/msgbox/script/msgbox.js"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script src="/admin/js/colorbox/jquery.colorbox.js" type="text/javascript"></script>

        <link href="../../js/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../js/lib/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../js/lib/json2.js" type="text/javascript"></script>
    <script src="../../js/lib/ligerUI/js/ligerui.all.js"></script>

    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        var Tid;
        var grid, manager, gridR, managerR;
        $(document).ready(function () {
            Tid = getQueryString("tid");
            $("#m1").attr("href", "Modify1.aspx?tid=" + Tid);
            $("#m2").attr("href", "Modify2.aspx?tid=" + Tid);
            $("#m3").attr("href", "Modify3.aspx?tid=" + Tid);

            grid = manager = $("#list").ligerGrid({
                columns: [
                { display: '品牌编号', name: 'BrandId', width: 80, type: 'text' },
                {
                    display: '品牌名称', name: 'BrandName', width: 300, align: 'left', type: 'text'
                }
                ], url: '/Ajax_Handle/ProductBrands.ashx', parms: { Action: "GetProductTypeList" },
                usePager: true, pageSize: 20, rownumbers: true,
                width: '400px', checkbox: true
            });

            gridR = managerR = $("#result").ligerGrid({
                columns: [
                { display: '品牌编号', name: 'BrandId', width: 80, type: 'text' },
                {
                    display: '品牌名称', name: 'BrandName', width: 300, align: 'left', type: 'text'
                }
                ], url: '/Ajax_Handle/ProductBrands.ashx', parms: { Value1: Tid, Action: "GetBrandList" },
                usePager: false, rownumbers: true,
                width: '400px', checkbox: true
            });

            $.post("/Ajax_Handle/ProductBrands.ashx", { Value1: Tid, Action: "GetTypeInfo" }, function (data) {
                var obj = $.parseJSON(data)
                $("[id$=txtTypeName]").val(obj.TypeName);
                $("[id$=txtRemark]").val(obj.Remark);
            });

            $("#toRight").click(function () {
                var rows = manager.getCheckedRows();
                if (!rows || rows == "") {
                    $.ligerDialog.warn('请选择关联品牌!');
                    return;
                }
                for (var i = 0; i < rows.length; i++) {
                    //验证定额是否已存在
                    var query = managerR.getData();
                    for (var t = 0; t < query.length; t++) {
                        if (query[t]["BrandId"] == rows[i]["BrandId"]) {
                            $.ligerDialog.warn("品牌/" + rows[i]["BrandName"] + "/已存在！");
                            return false;
                        }
                    }
                }

                for (var i = 0; i < rows.length; i++) {

                    managerR.add({
                        BrandId: rows[i]["BrandId"],
                        BrandName: rows[i]["BrandName"],
                    });
                }
            });

            $("#toLeft").click(function () {
                var rows = managerR.getCheckedRows();
                if (!rows || rows == "") {
                    $.ligerDialog.warn('请选择品牌!');
                    return;
                }
                managerR.deleteRange(rows);
            });

            $("#btnSearch").click(function () {
                var manager = $("#list").ligerGrid({ parms: { Value1: $("#txtName").val(), Action: "GetProductTypeList" } });
                grid.loadData(true);
            });

            $("#btnNext").click(function () {
                var brandList = "";
                var rows = managerR.getData();

                if (rows && rows != "") {
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i]["BrandId"].toString().trim() != "") {
                            if (brandList == "") {
                                brandList = rows[i]["BrandId"].toString().trim();
                            }
                            else {
                                brandList = brandList + "," + rows[i]["BrandId"].toString().trim();
                            }
                        }
                    }
                }
                $.post("/Ajax_Handle/ProductBrands.ashx", { Value1: $("[id$=txtTypeName]").val(), Value2: $("[id$=txtRemark]").val(), Value3: brandList, Value4: Tid, Action: "UpdateProductType" }, function (data) {
                    if (data== "1") {
                        $.ligerDialog.success("保存成功！");
                    }
                    else {
                        $.ligerDialog.error("保存失败！");
                    }
                });
            });

            $("#btnCancel").click(function () {
                location.href = "list.aspx";
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="编辑商品类型" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="商品类型是一系属性的组合，可以用来向顾客展示某些商品具有的特有的属性，一个商品类型下可添加多种属性.一种是供客户查看的扩展属性,如图书类型商品的作者，出版社等，一种是供客户可选的规格,如服装类型商品的颜色、尺码。 " />
                    </td>
                </tr>
            </table>
        </div>        
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" ><a id="m1">基本设置</a></li>
                    <li class="normal" ><a id="m2">扩展属性</a></li>
                    <li class="normal"><a  id="m3">规格</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">商品类型名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtTypeName" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <div id="txtTypeNameTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" OkMessage="输入正确！" Description="商品类型名称不能为空，长度限制在1-50个字符之间！"
                                            ControlToValidate="txtTypeName" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="商品类型名称不能为空，长度限制在1-50个字符之间！" LowerBound="1"
                                                    UpperBound="50" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 8px;"></td>
                                    <td height="6"></td>
                                </tr>
                                <tr>
                                    <td class="td_class">品牌名称：
                                    </td>
                                    <td height="6">
                                        <input id="txtName" class="l-text" type="text" value="" style="height: 25px; width: 200px; min-height: 0px;" />&nbsp;&nbsp;&nbsp;&nbsp;<input id="btnSearch" class="l-button" type="button" value="查询" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="vertical-align: top;">关联商品品牌 ：
                                    </td>
                                    <td height="25">
                                        <table>
                                            <tr>
                                                <td style="vertical-align: top;">
                                                    <div id="list"></div>
                                                </td>
                                                <td style="width: 100px;">
                                                    <div>
                                                        <table style="height: 100%; text-align: center;">
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center; height: 40px; padding-left: 20px;">
                                                                    <input id="toRight" type="button" value=">>" class="l-button" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center; height: 40px; padding-left: 20px;">
                                                                    <input id="toLeft" type="button" value="<<" class="l-button" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <div id="result"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 200px">
                                            <asp:Literal ID="Literal1" runat="server" Text="选择与些商品类型关联的商品品牌" /></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 8px;"></td>
                                    <td height="6"></td>
                                </tr>
                                <tr>
                                    <td class="td_class">备注 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtRemark" CssClass="l-text" runat="server" Width="372px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px;"></td>
                                    <td height="6"></td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <input id="btnNext" class="l-button" type="button" value="保存" />
                                        <input id="btnCancel" class="l-button" type="button" value="取消" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
        </div>
    </div>
    <br />
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</div>
            </div>
        </form>
</body>
