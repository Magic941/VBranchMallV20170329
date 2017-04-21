//(function () {
var selectProductType;
var selectProductBrand;
var contetAttributesEx;
var contetSKUs;
var contetProductAccessories;
var contetRelatedProduct;
//var baseAccessorieHTML;

var hfCurrentProductType;
var hfCurrentProductBrand;
var hfCurrentAttributes;
var hfCurrentBaseProductSKUs;
var hfCurrentProductSKUs;
var hfProductImages;
var hfProductAccessories;
var btnOpenSKUs;
var btnAddProductAccessories;
var btnAddRelatedProduct;

var attributesTR;
var skusTR;
var productAccessoriesTR;

var optionDefault = '<option selected="selected" value="" >请选择</option>';
var optionHTML = '<option value="{0}">{1}</option>';

var attributHTML = '<table cellspacing="0" cellpadding="0" width="100%" AttributeId="{0}" attributemode="{1}" id="{2}"><tr><td class="td_class" id="AttributName" >{3} ：</td><td height="25" ><div style="width:800px;"><ul id="AttributeContent"></ul></div></td></tr></table><h2></h2>';
var SelectAttributHTML = '<tr><td class="td_class"></td><td><table cellspacing="0" cellpadding="0" width="100%" id="color_{0}" class="table" style="display: none; margin-top: 10px;margin-bottom: 10px;"><thead><tr><th class="th">颜色</th><th  class="th">图片（无图片可不填）</th></tr></thead><tbody></tbody></table></td></tr>';
var colorTr = '<tr id="{0}" style="display: none; "><td class="tile"><label id="selectValueLabel_{1}"  title="{2}">{3}</label></td><td class="td"><input type="file" id="File_{4}" accept="image/gif, image/jpeg"/><img id="imgURL_{5}" value="" style="width: 28px; height: 28px;" /><div id="fileQueue" style="display:none;"> </div></td></tr>';
//var colorTr = '<tr id="{0}" style="display: none; "><td class="tile"><label id="selectValueLabel_{1}"  title="{2}">{3}</label></td><td class="td"><input type="file" id="File_{4}" accept="image/gif, image/jpeg"/></td></tr>';

var attributInputHTML = '<input type="{0}" />';
var attributeLabelHTML = '<label for="{0}" title="{1}"  >{2}</label> ';
var attributeLabelHTMLForSpec = '<label for="{0}"  id="attrValuelabel_{1}" title="{6}">{2}</label> <input id="attrValueText_{3}" maxlength="15" type="text" value="{4}" vid="{5}" style="display: none; " class="edit">';

var alertMessageHTML = '<table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border"><tr><td class="td_class"></td><td height="25">{0}</td></tr></table>';

var attributInputMode = { "Radio": "radio", "CheckBox": "checkbox", "Text": "text" };

var SelectTypeId = '';

var skusTableHTML = '<table class="GridViewStyle" cellspacing="0" cellpadding="" rules="all" border="1" id="GeneralSKUs" style="background-color:White;border-color:#CCCCCC;border-width:1px;border-style:solid;width:98%;margin-left: 1%;border-collapse:collapse;">';
var skusTableHeadTr = '<tr class="GridViewHeaderStyle" style="background-color:#E3EFFF;height:35px;background:#FFF"></tr>';
var skusTableHeadTh = '<td id="SKUName" style="width:50px;text-align: center;" AttributeId="{0}">{1}</td>';
var skusTableHeadThBase = '<th style="width:30%;"><em>*</em>商品编码</th><th style="width:90px;"><em>*</em>销售价</th><th style="width:90px">成本价</th>';
skusTableHeadThBase += '<th style="width:40px;"><em>*</em>库存</th><th style="width:40px;"><em>*</em>警戒库存</th>';
skusTableHeadThBase += '<th style="width:40px;">重量</th><th style="width:30px;">上架</th>';
var skusTableTdBase = '<td ><input value="{0}" autocomplete="off" id="SKU" style="width:200px;" maxlength="20" type="text"/></td>';
skusTableTdBase += '<td ><input value="{1}" id="SalePrice" class="OnlyFloat" style="width:90px;text-align: right;" maxlength="9" type="text"/></td>';
skusTableTdBase += '<td ><input value="{2}" id="CostPrice" class="OnlyFloat" style="width:90px;text-align: right;" maxlength="9 type="text"/></td>';
skusTableTdBase += ' <td ><input value="{3}" id="Stock" class="OnlyNum" style="width:40px;" maxlength="5" type="text"/></td>';
skusTableTdBase += '<td ><input value="{4}" id="AlertStock" class="OnlyNum" style="width:40px;" maxlength="5" type="text"/></td>';
skusTableTdBase += '<td ><input value="{5}" id="Weight" class="OnlyNum" style="width:40px;" maxlength="10" type="text"/></td>';
skusTableTdBase += '<td ><input {6} id="Upselling" type="checkbox"/></td>';
var skusTableTd = '<td class="SKUValueTD" attributeid="{0}" valueid="{1}"><div id="SKUValueStr" class="specdiv"><label class="SkuLabel_{2}" >{3}</label></div></td>';
var skusTableTr = '<tr class="SKUItemTR grdrow" style="height: 25px; background-color: rgb(255, 255, 255); background-position: initial initial; background-repeat: initial initial; "  id="{0}"></tr>';
var hasSku = false;
var isFirstLoadRelatedProduct = true;
var isFirstLoadAccessories = true;

var currentPid = $.getUrlParam('pid');

$(document).ready(function () {

    InitInput();
    
    selectProductType = $("#SelectProductType");
    selectProductBrand = $("#SelectProductBrand");
    contetAttributesEx = $("#ContetAttributesEx");
    contetSKUs = $("#contetSKUs");
    contetProductAccessories = $("#contetProductAccessories");
    contetRelatedProduct = $('#contetRelatedProduct');

    attributesTR = $(".AttributesTR");
    skusTR = $(".SKUsTR");

    hfCurrentProductType = $('[id$=hfCurrentProductType]');
    hfCurrentProductBrand = $('[id$=hfCurrentProductBrand]');
    hfCurrentAttributes = $('[id$=hfCurrentAttributes]');
    hfCurrentBaseProductSKUs = $('[id$=hfCurrentBaseProductSKUs]');
    hfCurrentProductSKUs = $('[id$=hfCurrentProductSKUs]');
    hfProductImages = $('[id$=hfProductImages]');
    btnOpenSKUs = $('#btnOpenSKUs');
    btnCloseSkus = $('#btnCloseSkus');
    btnAddProductAccessories = $('#btnAddProductAccessories');
    btnAddRelatedProduct = $('#btnAddRelatedProducts');
    InitProductTypes();
    btnCloseSkus.hide();

    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "ProductInfo", pid: currentPid },
        //        beforeSend: function () {
        //            $.jBox.tip("努力为您加载中，请稍后...", 'loading');
        //        },
        //        complete: function () {
        //            $.jBox.closeTip();
        //        },
        success: function (resultData) {
            picServerUrl = resultData.PicServerUrl;
            switch (resultData.STATUS) {
                case "SUCCESS":
                    $(resultData.DATA).each(function () {
                        $("[id$='txtProductName']").val(this.ProductName);
                        $("[id$='txtUnit']").val(this.Unit);
                        $("[id$='txtMarketPrice']").val(this.MarketPrice);
                        $("[id$='txtDisplaySequence']").val(this.DisplaySequence);
                        $("[id$='txtProductSKU']").val(this.ProductCode);
                        $("[id$='txtSalePrice']").val();
                        $("[id$='SelectProductType']").val(this.TypeId);
                        LoadProductBrands();
                        $('#SelectProductBrand').val(this.BrandId);
                        $("[id$='txtShortDescription']").val(this.ShortDescription);
                        $("[id$='txtDescription']").val(this.Description);
                        $("[id$='txtMeta_Title']").val(this.Title);
                        $("[id$='txtMeta_Description']").val(this.Meta_Description);
                        $("[id$='txtMeta_Keywords']").val(this.Meta_Keywords);
                        $("[id$='litCategoryName']").val(this.CategoryId);

                        hfCurrentProductBrand.val(this.BrandId);

                        if (this.ThumbnailUrl1) {
                            //商品图片不显示修改
                            //$('[id$=hfImage0]').val(this.ThumbnailUrl1.replace('T150X150_', "{0}"));
                            $('[id$=hfImage0]').val(picServerUrl + "/" + this.ThumbnailUrl1.replace('T150X150_', "{0}"));
                        }

                        hfCurrentProductType.val(this.TypeId);
                        LoadAttributes();
                        if (btnOpenSKUs.css('display') == 'none') {
                            LoadSKUs();
                            InitInput();
                        }

                        //加载属性信息
                        AttributeLoad();

                        if (this.HasSKU) {

                            hasSku = this.HasSKU;
                            $(".haveSku").hide();
                            GeneratedSpecification();
                        } else {
                            GeneratedProductSaleInfo();
                            $("#specificationTab").bind('click', function () {
                                GeneratedSpecification();
                            });
                        }

                        //加载规格信息
                        GeneratedRelatedProduct();
                        ProductAccessoriesManage();
                        GeneratedImageInfo();
                    });
                    break;
                default:
                    break;
            }
        }
    });

    //Clear Backward Val
    hfCurrentProductType.val(selectProductType.val());

    //ProductType
    selectProductType.bind('change', function () {
        if (!$(this).val()) {
            hfCurrentProductType.val('');

            //Clear Brand
            selectProductBrand.find('option').remove();
            selectProductBrand.append(optionDefault);

            //Clear Attributes
            contetAttributesEx.find('table,h2').remove();
            attributesTR.hide();

            //Clear SKU
            if (btnOpenSKUs.css('display') == 'none') {
                contetSKUs.find('table').remove();
                contetSKUs.append(alertMessageHTML.format('请您先选择商品类型! <a href="javascript:void(0);" style="color:blue;" onclick=GoTab(0,"SelectProductType")>点此返回选择</a>'));
                skusTR.show();
            }

            return false;
        }

        if (hfCurrentProductType.val() && !confirm('切换商品类型将会导致已经编辑的品牌，属性和规格数据丢失，确定要切换吗？')) {//
            $(this).val(hfCurrentProductType.val());
            return false;
        }
        hfCurrentProductType.val($(this).val());
        LoadProductBrands();
        LoadAttributes();
        if (btnOpenSKUs.css('display') == 'none') {
            LoadSKUs();
            InitInput();
        }
    });

    //ProductBrand
    selectProductBrand.bind('change', function () {
        hfCurrentProductBrand.val($(this).val());
    });

    //OpenSKU
    btnOpenSKUs.bind('click', function () {
        $(this).hide();
        btnCloseSkus.show();
        if (!hfCurrentProductType.val()) {
            contetSKUs.find('table').remove();
            contetSKUs.append(alertMessageHTML.format('请您先选择商品类型! <a href="javascript:void(0);" style="color:blue;" onclick=GoTab(0,"SelectProductType")>点此返回选择</a>'));
            skusTR.show();
            return;
        }
        LoadSKUs();
        InitInput();
    });

    $('[id$=txtShortDescription]').autosize();
    //ProductAccessories
    btnAddProductAccessories.bind('click', function () {
        $('#AddProductAccessoriesTR').hide();
        contetProductAccessories.show();
    });

    btnAddRelatedProduct.bind('click', function () {
        $('#AddRelatedProductTR').hide();
        contetRelatedProduct.show();
    });

    $.dynatextarea($('[id$=txtShortDescription]'), 300, $('#progressbar1'));
    
    
    $("[id$='txtProductSKU']").blur(function () {
        var currentText = $("[id$='txtProductSKU']");
        if (currentText.val()) {
            LoadCheckResult(currentText, currentText.val());
        }
    });
});


//商品规格值
function GeneratedSpecification() {

    if (hasSku && $("#hidden_IsFirstLoad").val()) {
        $("#hidden_IsFirstLoad").val('');
        btnOpenSKUs.hide();
        btnCloseSkus.show();
        $(".haveSku").hide();
        if (!hfCurrentProductType.val()) {
            contetSKUs.find('table').remove();
            contetSKUs.append(alertMessageHTML.format('请您先选择商品类型! <a href="javascript:void(0);" style="color:blue;" onclick=GoTab(0,"SelectProductType")>点此返回选择</a>'));
            skusTR.show();
            return;
        }
        LoadSKUs();
        InitInput();


        //加载规格值
        AttributeValuesLoad();

        var skusContent = contetSKUs.find('table #AttributeContent');
        var countTR = skusContent.length;
        var selectedCountTR = skusContent.find('input:checked:first').length;

        //全部属性均选中, 生成SKU
        if (selectedCountTR == countTR) {
            contetSKUs.removeData(); //clear data
            contetSKUs.data('skus', skusContent);

            var htSkuData = new jQuery.Hashtable();
            var attributeIds = new Array(countTR);
            var attributeIdIndex = 0;
            //组装已选SKU
            contetSKUs.find('table #AttributeContent').find('input:checked').each(function () {
                var attributeId = $(this).attr('attrid');
                var attributeName = $(this).attr('attrname');
                var valueId = $(this).val();
                var value = $(this).attr('valuestr');

                if (htSkuData.containsKey(attributeId)) {
                    //存在 Add
                    htSkuData.get(attributeId).Values.push({
                        "AttributeId": attributeId,
                        "ValueId": valueId,
                        "Value": value
                    });
                } else {
                    //不存在 New
                    attributeIds[attributeIdIndex++] = attributeId;
                    var checkedValues = new Array();
                    checkedValues.push({
                        "AttributeId": attributeId,
                        "ValueId": valueId,
                        "Value": value
                    });
                    var attributInfo = {
                        "AttributeId": attributeId,
                        "AttributeName": attributeName,
                        "Values": checkedValues
                    };
                    htSkuData.add(attributeId, attributInfo);
                }
            });

            //组合已选SKU
            var skuValues = htSkuData.get(attributeIds[0]).Values;
            var skuArray = new Array(skuValues.length);

            $.each(skuValues, function (i, skuValue) {
                skuArray[i] = new Array(1);
                skuArray[i][0] = skuValue;
            });

            for (var index = 1; index < attributeIds.length; index++) {
                skuValues = htSkuData.get(attributeIds[index]).Values;
                var tmpArray = new Array(skuArray.length * skuValues.length);
                var rowCounter = 0;

                for (var sindex = 0; sindex < skuValues.length; sindex++) {
                    for (var cindex = 0; cindex < skuArray.length; cindex++) {
                        tmpArray[rowCounter] = new Array(index + 1);
                        for (var rindex = 0; rindex < (index + 1); rindex++) {
                            if (rindex == index)
                                tmpArray[rowCounter][rindex] = skuValues[sindex];
                            else {
                                tmpArray[rowCounter][rindex] = skuArray[cindex][rindex];
                            }
                        }
                        rowCounter++;
                    }
                }
                skuArray = tmpArray;
            }
            //向页面添加SKU
            GeneralHavnSKUItem(attributeIds, htSkuData, skuArray);
            TempSKUInfo();
        }
    }
}

//商品销售价格、库存信息
function GeneratedProductSaleInfo() {
    $.ajax({
        url: '/ShopManage.aspx',
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: true,
        data: { Action: "ProductSkuInfo", pid: currentPid },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    //SKU数据行
                    $(resultData.DATA).each(function (n) {
                        $("[id$='txtProductSKU']").val(!this.SKU ? "" : this.SKU);
                        $("[id$='txtCostPrice']").val(this.CostPrice > 0 ? this.CostPrice : 0);
                        $("[id$='txtSalePrice']").val(this.SalePrice > 0 ? this.SalePrice : 0);
                        $("[id$='txtStock']").val(this.Stock > 0 ? this.Stock : 0);
                        $("[id$='txtAlertStock']").val(this.AlertStock > 0 ? this.AlertStock : 0);
                        $("[id$='txtWeight']").val(this.Weight > 0 ? this.Weight : 0);
//                        if (this.Upselling) {
//                            $("#ctl00_ContentPlaceHolder1_rblUpselling_0").attr('checked', "checked");
//                        } else {
//                            $("#ctl00_ContentPlaceHolder1_rblUpselling_1").attr('checked', "checked");
//                        }
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

//商品图片修复
function GeneratedImageInfo() {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "ProductIamges", pid: currentPid },
        success: function (resultData) {
            //switch (resultData.STATUS) {
            //    case "SUCCESS":
            //        $(resultData.DATA).each(function (index) {
            //            if (index == 0) {
            //                $('[id$=hfImage1]').val(this.ThumbnailUrl1.replace('T150X150_', "{0}"));
            //            }
            //            if (index == 1) {
            //                $('[id$=hfImage2]').val(this.ThumbnailUrl1.replace('T150X150_', "{0}"));
            //            }
            //            if (index == 2) {
            //                $('[id$=hfImage3]').val(this.ThumbnailUrl1.replace('T150X150_', "{0}"));
            //            }
            //            if (index == 3) {
            //                $('[id$=hfImage4]').val(this.ThumbnailUrl1.replace('T150X150_', "{0}"));
            //            }
            //        });
            //        break;
            //    default:
            //        break;
            //}

            var picServerUrl = resultData.PicServerUrl;
            var picName = "T158X158_";
            switch (resultData.STATUS) {
                case "SUCCESS":
                    $(resultData.DATA).each(function (index) {

                        if (index == 0) {
                            $('[id$=hfImage1]').val(picServerUrl + "/" + this.ThumbnailUrl1.replace(picName, "{0}"));
                        }
                        if (index == 1) {
                            $('[id$=hfImage2]').val(picServerUrl + "/" + this.ThumbnailUrl1.replace(picName, "{0}"));
                        }
                        if (index == 2) {
                            $('[id$=hfImage3]').val(picServerUrl + "/" + this.ThumbnailUrl1.replace(picName, "{0}"));
                        }
                        if (index == 3) {
                            $('[id$=hfImage4]').val(picServerUrl + "/" + this.ThumbnailUrl1.replace(picName, "{0}"));
                        }
                    });
                    break;
                default:
                    break;
            }
        }
    });
}


//商品配件信息
function ProductAccessoriesManage() {
    //配件主题描述信息
    if (isFirstLoadAccessories) {
        isFirstLoadAccessories = false;
        $.ajax({
            url: "/ShopManage.aspx",
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: true,
            data: { Action: "ProductAccessoriesManage", pid: currentPid },
            success: function (resultData) {
                switch (resultData.STATUS) {
                    case "SUCCESS":
                        $('#AddProductAccessoriesTR').hide();
                        contetProductAccessories.show();
                        $(resultData.DATA).each(function () {
                            $('[id$=txtAccessorieName]').val(this.AccessoriesName);
                            $('[id$=txtMinQuantity]').val(this.MinQuantity);
                            $('[id$=txtDiscountAmount]').val(this.DiscountAmount);
                            $('[id$=txtMaxQuantity]').val(this.MaxQuantity);
                            if (this.DiscountType == 0) {
                                $('[id$=rdoDiscountType4D]').attr('checked', 'checked');
                            } else {
                                $('[id$=rdoDiscountType4A]').attr('checked', 'checked');
                            }
                        });
                        break;
                    default:
                        break;
                }
            }
        });
        //配件信息
        $.ajax({
            url: "/ShopManage.aspx",
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: true,
            data: { Action: "ProductAccessoriesValues", pid: currentPid },
//            beforeSend: function () {
//                $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//            },
//            complete: function () {
//                $.jBox.closeTip();
//                $("#Accessories").attr('src', '/Admin/Shop/Products/SelectAccessorieNew.aspx?pid=' + currentPid);
//            },
            success: function (resultData) {
                switch (resultData.STATUS) {
                    case "SUCCESS":
                        $('[id$=hfSelectedAccessories]').val(resultData.DATA);
                        break;
                    default:
                        break;
                }
            }
        });
    }
}

//相关商品信息
function GeneratedRelatedProduct() {//相关商品信息
    if (isFirstLoadRelatedProduct) {
        isFirstLoadRelatedProduct = false;
        $.ajax({
            url: "/ShopManage.aspx",
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { Action: "RelatedProductFactory", pid: currentPid },
//            beforeSend: function () {
//                $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//            },
//            complete: function () {
//                $.jBox.closeTip();
//            },
            success: function(resultData) {
                switch (resultData.STATUS) {
                case "SUCCESS":
                    $('#AddRelatedProductTR').hide();
                    contetRelatedProduct.show();
                    $('[id$=hfRelatedProducts]').val(resultData.DATA);
                    break;
                default:
                    break;
                }
            }
        });
    }
}

function AttributeValuesLoad() {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "LoadAttributesvalues", pid: currentPid },
//        beforeSend: function () {
//            $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//        },
//        complete: function () {
//            $.jBox.closeTip();
//        },
        success: function (resultData) {
            var picserverurl = resultData.PicServerUrl;
            switch (resultData.STATUS) {
                case "SUCCESS":
                    $(resultData.DATA).each(function () {
                        var selectAttributeValueId = this.ValueId;
                        var valueStr = this.ValueStr;
                        var isDefineImage = this.UserDefinedPic;
                        var CurrentAttribute = this.AttributeId;
                        var CurrentImage = this.ImageUrl;
                        $("#" + this.AttributeId).find("[name='AttributeValue" + this.AttributeId + "']").each(function (index) {
                            if (selectAttributeValueId == this.value) {
                                $("#" + this.id).attr("checked", "checked");
                                if (isDefineImage) {
                                    //$('#attrValueText_' + this.value).attr("style", "");
                                    //$('#attrValuelabel_' + this.value).attr("style", "display: none;");
                                    $('#attrValueText_' + this.value).val(valueStr);
                                    $('#color_' + CurrentAttribute).attr("style", "");
                                    $('#color_' + CurrentAttribute).find('tr').each(function () {
                                        if (selectAttributeValueId == this.id) {
                                            $('#' + this.id).attr("style", "");
                                            if (CurrentImage != null) {
                                                $('#imgURL_' + this.id).attr("src", picserverurl + CurrentImage.format('T32X32_'));
                                                $('#imgURL_' + this.id).attr("value",picserverurl+ CurrentImage);
                                            }
                                            
                                            $('#selectValueLabel_' + this.id).text(valueStr);
                                        }
                                    });
                                }
                            }
                        });
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

function GeneralHavnSKUItem(attributeIds, htSkuData, skuArray) {
    contetSKUs.find('#GeneralSKUs').remove();
    var skuTable = $(skusTableHTML);
    //表头处理
    var headTR = skuTable.append(skusTableHeadTr).find('tr:last');
    for (var i = 0; i < attributeIds.length; i++) {
        headTR.append(skusTableHeadTh.format(htSkuData.get(attributeIds[i]).AttributeId, htSkuData.get(attributeIds[i]).AttributeName));
    }
    headTR.append(skusTableHeadThBase);

    //SKU数据行
    $(skuArray).each(function (index) {
        var trId = "";
        $(this).each(function () {
            trId = this.AttributeId + '_' + this.ValueId + '_' + trId;
        });
        var skusTableTrFormat = skusTableTr;
        skusTableTrFormat = skusTableTrFormat.format(trId.substring(0, trId.length - 1));
        var dataTR = skuTable.append(skusTableTrFormat).find('tr:last');
        //var dataTR = skuTable.append(skusTableTr).find('tr:last');
        $(this).each(function () {
            dataTR.append(skusTableTd.format(this.AttributeId, this.ValueId, this.ValueId, this.Value));
        });
        $.ajax({
            url: '/ShopManage.aspx',
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { Action: "ProductSkuInfo", pid: currentPid },
//            beforeSend: function () {
//                $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//            },
//            complete: function () {
//                $.jBox.closeTip();
//            },
            success: function (resultData) {
                switch (resultData.STATUS) {
                    case "SUCCESS":

                        //SKU数据行
                        $(resultData.DATA).each(function (n) {
                            if (n == index) {
                                dataTR.append(skusTableTdBase.format(
                                    !this.SKU ? "" : this.SKU,
                                    this.SalePrice > 0 ? parseFloat(this.SalePrice).toFixed(2) : (0.00).toFixed(2),
                                    this.CostPrice > 0 ? parseFloat(this.CostPrice).toFixed(2) : (0.00).toFixed(2),
                                    this.Stock > 0 ? this.Stock : 0,
                                    this.AlertStock > 0 ? this.AlertStock : 0,
                                    this.Weight > 0 ? this.Weight : 0, 
                                    this.Upselling ? "checked='checked'" : ""));
                            }
                        });
                        break;
                    default:
                        break;
                }
            }
        });
    });
    contetSKUs.append(skuTable);
    GeneratedTempSkuInfo();
    $("#Hidden_TempSKUInfo").val('');
}


/*获取临时保存用户输入的商品信息 By Maticsoft： Rock 2012年12月22日 14:31:20*/
function GeneratedTempSkuInfo() {
    var hiddenTempValue = $("#Hidden_TempSKUInfo").val();
    if (hiddenTempValue) {
        var tempValues = hiddenTempValue.split(',');
        for (var i = 0; i < tempValues.length - 1; i++) {
            var oneTempValue = tempValues[i].split('|');
            var trId = "#" + oneTempValue[0];
            if ($(trId).length > 0) {
                //Fix 商品编码SKU 重排后 混乱BUG
                //$($(trId).find(':text')[0]).val(oneTempValue[1]);
                $($(trId).find(':text')[1]).val(oneTempValue[2]);
                $($(trId).find(':text')[2]).val(oneTempValue[3]);
                $($(trId).find(':text')[3]).val(oneTempValue[4]);
                $($(trId).find(':text')[4]).val(oneTempValue[5]);
                $($(trId).find(':text')[5]).val(oneTempValue[6]);
                $($(trId).find('input:last')).attr('checked', oneTempValue[7]);
            }
        }
    }
}


/*临时保存用户输入的商品信息 By Maticsoft： Rock 2012年12月22日 14:01:20*/
function TempSKUInfo() {
    var tempValue = '{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}';
    //Hidden_TempSKUInfo
    $(".GridViewStyle tr").each(function (index) {
        if (index != 0) {
            var trSkuId = $(this).attr('id');
            var tempCommodityCode = $($(this).find(':text')[0]).val() ? $($(this).find(':text')[0]).val() : "";
            var tempSalePrice = $($(this).find(':text')[1]).val() ? $($(this).find(':text')[1]).val() : 0;
            var tempCostPrice = $($(this).find(':text')[2]).val() ? $($(this).find(':text')[2]).val() : 0;
            var tempStock = $($(this).find(':text')[3]).val() ? $($(this).find(':text')[3]).val() : 0;
            var tempAlertStock = $($(this).find(':text')[4]).val() ? $($(this).find(':text')[4]).val() : 0;
            var tempWeight = $($(this).find(':text')[5]).val() ? $($(this).find(':text')[5]).val() : 0;
            var tempSaleStatus = $($(this).find('input:last')).attr('checked') ? $($(this).find('input:last')).attr('checked') : ""; // $($(".GridViewStyle tr")[1]).find('input:last')
            var hiddenTempValue = $("#Hidden_TempSKUInfo").val();
            $("#Hidden_TempSKUInfo").val(tempValue.format(trSkuId, tempCommodityCode, tempSalePrice, tempCostPrice, tempStock, tempAlertStock, tempWeight, tempSaleStatus) + ',' + hiddenTempValue);
        }
    });
}


/*检测商品编码是否存在 By Maticsoft： Rock 2012年12月22日 17:01:20*/
function IsExistSkuCode() {
    $(".GridViewStyle tr td :input").each(function (index) {
        var currentText = $(this);
        if ($(this).attr('id') == "SKU") {
            $(this).unbind('blur').bind('blur', function () {
                if ($(this).val()) {
                    LoadCheckResult(currentText, $(this).val());
                }
            });

        }
    });
}

function LoadCheckResult(senderevent, skucode) {
    $.ajax({
        url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "IsExistSkuCode", SKUCode: senderevent.val(), pid: currentPid },
        //        beforeSend: function () {
        //            $.jBox.tip("正在检测商品编码的唯一性，请稍后...", 'loading');
        //        },
        //        complete: function () {
        //            $.jBox.closeTip();
        //        },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "FAILED":
                    //  $.jBox.closeTip();
                    alert("该商品编码已经存在，请重新输入！");
                    senderevent.val('');
                    senderevent.focus();
                    break;
                default:
                    break;
            }
        }, error: function (xmlHttpRequest, textStatus, errorThrown) {
            alert(xmlHttpRequest.responseText);
        }
    });
    if ($(".GridViewStyle tr td :input[id=SKU][value='" + skucode + "']").length > 1) {
        alert("该商品编码已经存在，请重新输入！");
        senderevent.val('');
        senderevent.focus();
    }
}


function AttributeLoad() {
    $.ajax({
        url: "/ShopManage.aspx",
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "LoadExistAttributes", pid: currentPid },
//        beforeSend: function () {
//            $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//        },
//        complete: function () {
//            $.jBox.closeTip();
//        },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    $(resultData.DATA).each(function () {
                        var selectAttributeId = this.ValueId;
                        if (this.UsageMode == 2) {
                            $("#" + this.AttributeId).find("#AttributeContent input[type=text]").val(this.ValueStr);
                        } else {
                            $("#" + this.AttributeId).find("[name='AttributeValue" + this.AttributeId + "']").each(function (index) {
                                if (selectAttributeId == this.value) {
                                    $("#" + this.id).attr("checked", "checked");
                                }
                            });
                        }
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

function InitInput() {
    $(".OnlyNum").OnlyNum();
    $(".OnlyFloat").OnlyFloat();
}

function GoTab(index, targetId) {
    nTabs($('[onclick="nTabs(this,' + index + ');"]').get(0), 0);
    if (targetId) {
        $('#' + targetId).focus();
    }
}

function SubForm() {
    if (!PageIsValid()) {
        nTabs($('[onclick="nTabs(this,0);"]').get(0), 0);
        return false;
    }

    if (!$('[id$=Hidden_SelectValue]').val()) {
        alert('请选择商品所在分类');
        return false;
    }
    if (!hfCurrentProductType.val()) {
        alert('请选择商品类型');
        return false;
    }
    if (!$("[id$='txtMarketPrice']").val()) {
        alert('请输入商品市场价格');
        return false;
    }
    var flag_skuCodeExist = false;
    //未开启SKU
    if ($(".GridViewStyle tr td :input[id=SKU]").length == 0) {
        hasSku = false;
        $(".haveSku").show();
        if (!$('[id$=txtProductSKU]').val()) {
            alert('请输入商品编码');
            GoTab(0);
            return false;
        } else {
            //Check SKU 是否存在
            $.ajax({
                url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST',
                dataType: 'json',
                timeout: 10000,
                async: false,
                data: { Action: "IsExistSkuCode", SKUCode: $('[id$=txtProductSKU]').val(), pid: currentPid },
                //        beforeSend: function () {
                //            $.jBox.tip("正在检测商品编码的唯一性，请稍后...", 'loading');
                //        },
                //        complete: function () {
                //            $.jBox.closeTip();
                //        },
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "FAILED":
                            //  $.jBox.closeTip();
                            $('[id$=txtProductSKU]').val('');
                            $('[id$=txtProductSKU]').focus();
                            flag_skuCodeExist = true;
                            break;
                        default:
                            break;
                    }
                }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert(xmlHttpRequest.responseText);
                }
            });
            if (flag_skuCodeExist) {
                alert('该商品编码已经存在，请重新输入！');
                GoTab(0);
                return false;
            }
        }
        if (!$('[id$=txtSalePrice]').val()) {
            alert('请输入销售价');
            GoTab(0);
            return false;
        }
        if (!$('[id$=txtStock]').val()) {
            alert('请输入商品库存');
            GoTab(0);
            return false;
        }
        if (!$('[id$=txtAlertStock]').val()) {
            alert('请输入警戒库存');
            GoTab(0);
            return false;
        }
    } else {
        hasSku = true;
        $(".haveSku").hide();
    }  

    //AttributesJson
    hfCurrentAttributes.val(JSON.stringify(GetAttributes()));
//    if (!hfCurrentAttributes.val() || hfCurrentAttributes.val() == '[]') {
//        alert('请选择商品的属性');
//        return false;
//    }


    var flag_skuCode = false;
     flag_skuCodeExist = false;
    var flag_SalePrice = false;
    var flag_Stock = false;
    var flag_AlertStock = false;
    $(".GridViewStyle tr td :input").each(function (index) {
        if ($(this).attr('id') == "SKU") {
            if (!$(this).val()) {
                flag_skuCode = true;
                return false;
            } else {
                //Check SKU 是否存在
                $.ajax({
                    url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST',
                    dataType: 'json',
                    timeout: 10000,
                    async: false,
                    data: { Action: "IsExistSkuCode", SKUCode: $(this).val(), pid: currentPid },
                    //        beforeSend: function () {
                    //            $.jBox.tip("正在检测商品编码的唯一性，请稍后...", 'loading');
                    //        },
                    //        complete: function () {
                    //            $.jBox.closeTip();
                    //        },
                    success: function (resultData) {
                        switch (resultData.STATUS) {
                            case "FAILED":
                                //  $.jBox.closeTip();
                                $(this).val('');
                                $(this).focus();
                                flag_skuCodeExist = true;
                                break;
                            default:
                                break;
                        }
                    }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });
                if ($(".GridViewStyle tr td :input[id=SKU][value='" + $(this).val() + "']").length > 1) {
                    $(this).val('');
                    $(this).focus();
                    flag_skuCodeExist = true;
                }
                //不终止, 继续清空重复的SKU
//                if (flag_skuCodeExist) {
//                    return false;
//                }
            }
        }
        if ($(this).attr('id') == "SalePrice") {
            if (!$(this).val()) {
                flag_SalePrice = true;
                return false;
            }
        }
        if ($(this).attr('id') == "Stock") {
            if (!$(this).val()) {
                flag_Stock = true;
                return false;
            }
        }
        if ($(this).attr('id') == "AlertStock") {
            if (!$(this).val()) {
                flag_AlertStock = true;
                return false;
            }
        }
    });

    if (flag_skuCode) {
        alert("开启的规格中的商品编码不能为空！");
        return false;
    }
    if (flag_skuCodeExist) {
        alert("开启的规格中的商品编码已经存在，请重新输入！");
        return false;
    }
    if (flag_SalePrice) {
        alert("开启的规格中的销售价不能为空！");
        return false;
    }
    if (flag_Stock) {
        alert("开启的规格中的库存数量不能为空！");
        return false;
    }
    if (flag_AlertStock) {
        alert("开启的规格中的警戒库存数量不能为空！");
        return false;
    }


    if ($('[id$=hfSelectedAccessories]').val()) {
        if (!$('[id$=txtAccessorieName]').val()) {
            alert('请输入配件组名称');
            return false;
        }
        if (!$('[id$=txtMinQuantity]').val()) {
            alert('请输入最小购买量');
            return false;
        }
        if (!$('[id$=txtMaxQuantity]').val()) {
            alert('请输入最大购买量');
            return false;
        }
        if (!$('[id$=txtDiscountAmount]').val()) {
            alert('请输入优惠额度');
            return false;
        }
    }

    //check SKU
    //    if (btnOpenSKUs.css('display') == 'none') {
    //        if (!CheckBaseSKU) {
    //            nTabs($('[onclick="nTabs(this,0);"]').get(0), 0);
    //            return false;
    //        }
    //    } else {
    //        if (!CheckGenSKU) {
    //            nTabs($('[onclick="nTabs(this,2);"]').get(0), 0);
    //            return false;
    //        }
    //    }

    //GenBaseSKUJson
    hfCurrentBaseProductSKUs.val(JSON.stringify(GetBaseSKU()));
    //SelectedSKUJson
    hfCurrentProductSKUs.val(JSON.stringify(GetSelectedSKU()));

    hfProductImages.val('');
    //ProductImages
    $('.ImgUpload').each(function () {
        var img = $(this).find("input[type=hidden]").val();
        if (img) {
            hfProductImages.val(hfProductImages.val() + '|' + img);
        }
    });

    return true;
}

function InitProductTypes() {
    $.ajax({
        url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "GetProductTypesKVList" },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    selectProductType.find('option').remove();
                    selectProductType.append(optionDefault);
                    $(resultData.DATA).each(function () {
                        selectProductType.append(optionHTML.format(this.TypeId, this.TypeName));
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

function LoadProductBrands() {
    $.ajax({
        url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "GetBrandsKVList", ProductTypeId: selectProductType.val() },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    selectProductBrand.find('option').remove();
                    selectProductBrand.append(optionDefault);
                    $(resultData.DATA).each(function () {
                        selectProductBrand.append(optionHTML.format(this.BrandId, this.BrandName));
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

function LoadAttributes() {
    $.ajax({
        url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "GetAttributesList", DataMode: 0, ProductTypeId: hfCurrentProductType.val() },

//        beforeSend: function () {
//            $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//        },
//        complete: function () {
//            $.jBox.closeTip();
//        },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    contetAttributesEx.find('table,h2').remove();
                    attributesTR.hide();
                    $(resultData.DATA).each(function () {
                        var data = this;
                        var target = $(attributHTML.format(
                            data.AttributeId, data.AttributeUsageMode, data.AttributeId,
                            data.AttributeName));
                        var inputMode;
                        switch (data.AttributeUsageMode) {
                            case 0:
                                inputMode = attributInputMode.Radio;
                                break;
                            case 1:
                                inputMode = attributInputMode.CheckBox;
                                break;
                            case 2:
                                inputMode = attributInputMode.Text;
                                break;
                            default:
                                alert('扩展属性模式不正确!');
                                return;
                        }
                        if (data.AttributeUsageMode == 2) {
                            target.find('#AttributeContent').append(
                                $(attributInputHTML.format(inputMode)).width(200).attr('maxlength', '200'));
                        } else {
                            $(data.AttributeValues).each(function () {
                                target.find('#AttributeContent').append('<li id="li_' + this.valueId + '">');
                                var targetInput = attributInputHTML.format(inputMode, data.AttributeId, data.AttributeName, this.valueStr);
                                var tarId = "AttributeValue" + this.valueId;
                                targetInput = $(targetInput);
                                targetInput.attr('name', "AttributeValue" + this.attributeId);
                                targetInput.attr('value', this.valueId);
                                targetInput.attr('id', tarId);
                                targetInput.css('cursor', 'pointer');
                                target.find('#li_' + this.valueId).append(targetInput).append(
                                $(attributeLabelHTML.format(tarId, this.valueStr, cutstr(this.valueStr, 10))).css('cursor', 'pointer'));
//                                target.find('#AttributeContent').append(targetInput).append(
//                                $(attributeLabelHTML.format(tarId, this.valueStr, this.valueStr)).css('cursor', 'pointer'));
                                target.find('#AttributeContent').append('</li>');
                            });
                        }
                        // inputType
                        contetAttributesEx.append(target);
                        attributesTR.show();
                    });
                    break;
                default:
                    break;
            }
        }
    });
}


//截取字符串
function cutstr(str, len) {
    var str_length = 0;
    var str_len = 0;
    if (!str) {
        return "";
    }
    var str_cut = new String();
    str_len = str.length;
    for (var i = 0; i < str_len; i++) {
        var a = str.charAt(i);
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

var definepic = "";
function LoadSKUs() {
    $.ajax({
        url: ("/ShopManage.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { Action: "GetAttributesList", DataMode: 1, ProductTypeId: hfCurrentProductType.val() },

//        beforeSend: function () {
//            $.jBox.tip("努力为您加载中，请稍后...", 'loading');
//        },
//        complete: function () {
//            $.jBox.closeTip();
//        },
        success: function (resultData) {
            switch (resultData.STATUS) {
                case "SUCCESS":
                    contetSKUs.find('table').remove();
                    skusTR.hide();
                    if ($(resultData.DATA).length == 0) {
                        contetSKUs.append(alertMessageHTML.format('此商品类型没有设置规格!'));
                    } else {
                        $(resultData.DATA).each(function () {
                            var data = this;
                            var target = $(attributHTML.format(
                                data.AttributeId, data.AttributeUsageMode, data.AttributeId,
                                data.AttributeName));
                            var inputMode = attributInputMode.CheckBox;
                            $(data.AttributeValues).each(function () {
                                target.find('#AttributeContent').append('<li id="li_' + this.valueId + '">');
                                var targetInput = attributInputHTML.format(inputMode);
                                var tarId = "AttributeValue" + this.valueId;
                                targetInput = $(targetInput);
                                targetInput.attr('attrid', data.AttributeId);
                                targetInput.attr('attrname', data.AttributeName);
                                targetInput.attr('valuestr', this.valueStr);
                                targetInput.attr('name', "AttributeValue" + this.attributeId);
                                targetInput.attr('value', this.valueId);
                                targetInput.attr('id', tarId);
                                targetInput.css('cursor', 'pointer');

                                target.find('#li_' + this.valueId).append(targetInput).append(
                                    $(attributeLabelHTMLForSpec.format(tarId, this.valueId, cutstr(this.valueStr, 10), this.valueId, this.valueStr, this.valueId, this.valueStr)).css('cursor', 'pointer'));
//                                target.find('#AttributeContent').append(targetInput).append(
                                //                                    $(attributeLabelHTMLForSpec.format(tarId, this.valueId, this.valueStr, this.valueId, this.valueStr, this.valueId)).css('cursor', 'pointer'));
                                target.find('#AttributeContent').append('</li>');
                            });
                            // inputType
                            contetSKUs.append(target);
                            if (data.UserDefinedPic) {
                                definepic = data.AttributeId;
                                var Colortarget = $(SelectAttributHTML.format(data.AttributeId));

                                $('#' + data.AttributeId).append(Colortarget);
                                $(data.AttributeValues).each(function () {
                                    var colortarget = colorTr.format(this.valueId, this.valueId, this.valueStr, this.valueStr, this.valueId, this.valueId);
                                    $("#color_" + data.AttributeId).append(colortarget)
                                });
                                uploadValueImage();
                                
                                var ids = '#' + definepic + ' :checkbox';
                                contetSKUs.find(ids).bind('click', function () {
                                    colorManage();
                                });

                                contetSKUs.find('#' + definepic + ' :text').bind('change', function () {
                                    keepvalueStr();
                                });

                                contetSKUs.find('#' + definepic + ' :text').bind('blur', function () {
                                    changeValueStr();
                                });
                            }
                        });
                    }
                    skusTR.show();

                    //set click bind
                    contetSKUs.find('table #AttributeContent :input').bind('click', function () {
                        GeneralSKUs();
                    });
                    break;
                default:
                    break;
            }
        }
    });
}

function keepvalueStr() {
    contetSKUs.find('#' + definepic + ' :text').each(function (index) {
        var valueId = $(this).attr('vid');
        if ($("#AttributeValue" + valueId).attr('checked')) {
            $(".SkuLabel_" + valueId).text($("#attrValueText_" + valueId).val());
        }
    });
}

function uploadValueImage() {
    $("#color_" + definepic + " tbody tr").each(function(index) {
        var id = $($("#color_" + definepic + " tbody tr")[index]).attr("id");
        $("#File_" + id).uploadify({
            'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
            'script': '/ProductSkuImg.aspx',
            'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
            'buttonImg': '/admin/images/uploadfile.jpg',
            'folder': 'UploadFile',
            'queueID': 'fileQueue_' + id,
            'width': 76,
            'height': 25,
            'auto': true,
            'multi': true,
            'fileExt': '*.jpg;*.gif;*.png;*.bmp',
            'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
            'queueSizeLimit': 1,
            'sizeLimit': 1024 * 1024 * 1,
            'onInit': function() {
            },
            'onComplete': function(event, queueId, fileObj, response, data) {
                var responseJSON = $.parseJSON(response);
                if (responseJSON.success) {
                    $("#imgURL_" + id).attr("src",responseJSON.data.format('T32X32_'));
                    $("#imgURL_" + id).attr("value", responseJSON.data);
                }
            },
            'onError': function(event, ID, fileObj, errorObj) {
                //            alert('上传图片大小不能超过2M，尺寸不能大于1280×1280');
                alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
            }
        });
    });
}

function changeValueStr() {
    contetSKUs.find('#' + definepic + ' :text').each(function (index) {
        var valueId = $(this).attr('vid');
        if ($("#AttributeValue" + valueId).attr('checked')) {
            $("#selectValueLabel_" + valueId).attr("title", $("#attrValueText_" + valueId).val()).text($("#attrValueText_" + valueId).val());
            $(".SkuLabel_" + valueId).text($("#attrValueText_" + valueId).val());
        }
    });
}

function colorManage() {
    var ids = '#' + definepic + ' :checkbox';
    var checkedNum = 0;
    contetSKUs.find(ids).each(function (index) {
        var valueId = $(this).attr('value');
        if ($("#AttributeValue" + valueId).attr('checked')) {
            checkedNum++;
            $("#color_" + definepic).show();
            //$("#attrValuelabel_" + valueId).hide();
            //$("#attrValueText_" + valueId).show();

            $("#color_" + definepic + " #" + valueId).show();
            $("#selectValueLabel_" + valueId).attr("title", $("#attrValueText_" + valueId).val()).text($("#attrValueText_" + valueId).val());
            $(".SkuLabel_" + valueId).text($("#attrValueText_" + valueId).val());
        } else {
            $("#color_" + definepic + " #" + valueId).hide();
            $("#attrValuelabel_" + valueId).show();
            $("#attrValueText_" + valueId).hide();
        }
    });

    if (checkedNum <= 0) {
        $("#color_" + definepic).show();
    }
}

function GeneralSKUs() {
    var skusContent = contetSKUs.find('table #AttributeContent');
    var countTR = skusContent.length;
    var selectedCountTR = skusContent.find('input:checked:first').length;

    //全部属性均选中, 生成SKU
    if (selectedCountTR == countTR) {
        contetSKUs.removeData(); //clear data
        contetSKUs.data('skus', skusContent);

        var htSkuData = new jQuery.Hashtable();
        var attributeIds = new Array(countTR);
        var attributeIdIndex = 0;
        //组装已选SKU
        contetSKUs.find('table #AttributeContent').find('input:checked').each(function () {
            var attributeId = $(this).attr('attrid');
            var attributeName = $(this).attr('attrname');
            var valueId = $(this).val();
            var value = $(this).attr('valuestr');

            if (htSkuData.containsKey(attributeId)) {
                //存在 Add
                htSkuData.get(attributeId).Values.push({
                    "AttributeId": attributeId,
                    "ValueId": valueId,
                    "Value": value
                });
            } else {
                //不存在 New
                attributeIds[attributeIdIndex++] = attributeId;
                var checkedValues = new Array();
                checkedValues.push({
                    "AttributeId": attributeId,
                    "ValueId": valueId,
                    "Value": value
                });
                var attributInfo = {
                    "AttributeId": attributeId,
                    "AttributeName": attributeName,
                    "Values": checkedValues
                };
                htSkuData.add(attributeId, attributInfo);
            }
        });

        //组合已选SKU
        var skuValues = htSkuData.get(attributeIds[0]).Values;
        var skuArray = new Array(skuValues.length);

        $.each(skuValues, function (i, skuValue) {
            skuArray[i] = new Array(1);
            skuArray[i][0] = skuValue;
        });

        for (var index = 1; index < attributeIds.length; index++) {
            skuValues = htSkuData.get(attributeIds[index]).Values;
            var tmpArray = new Array(skuArray.length * skuValues.length);
            var rowCounter = 0;

            for (var sindex = 0; sindex < skuValues.length; sindex++) {
                for (var cindex = 0; cindex < skuArray.length; cindex++) {
                    tmpArray[rowCounter] = new Array(index + 1);
                    for (var rindex = 0; rindex < (index + 1); rindex++) {
                        if (rindex == index)
                            tmpArray[rowCounter][rindex] = skuValues[sindex];
                        else {
                            tmpArray[rowCounter][rindex] = skuArray[cindex][rindex];
                        }
                    }
                    rowCounter++;
                }
            }

            skuArray = tmpArray;
        }

        TempSKUInfo();
        //向页面添加SKU
        GeneralSKUItem(skuArray, htSkuData, attributeIds);
        IsExistSkuCode();

    }
    else {
        contetSKUs.find('#GeneralSKUs').remove();
    }
}

function GeneralSKUItem(skuArray, htSkuData, attributeIds) {
    contetSKUs.find('#GeneralSKUs').empty().remove();
    var skuTable = $(skusTableHTML);
    contetSKUs.append(skuTable);
    //var skuTable = $('#GeneralSKUs');
    //表头处理
    var headTR = skuTable.append(skusTableHeadTr).find('tr:last');
    for (var i = 0; i < attributeIds.length; i++) {
        headTR.append(skusTableHeadTh.format(htSkuData.get(attributeIds[i]).AttributeId, htSkuData.get(attributeIds[i]).AttributeName));
    }
    headTR.append(skusTableHeadThBase);

    var productSKU = GetBaseSKU();
    
    //SKU数据行
    for (var j = 0; j < skuArray.length; j++) {
        var trId = "";
        $(skuArray[j]).each(function () {
            trId = this.AttributeId + '_' + this.ValueId + '_' + trId;
        });
        var skusTableTrFormat = skusTableTr;
        skusTableTrFormat = skusTableTrFormat.format(trId.substring(0, trId.length - 1));
        var dataTR = skuTable.append(skusTableTrFormat).find('tr:last');
        //var dataTR = skuTable.append(skusTableTr).find('tr:last');
        $(skuArray[j]).each(function () {
            dataTR.append(skusTableTd.format(this.AttributeId, this.ValueId, this.ValueId, this.Value));
        });

        (function (lockedInIndex) {
            var tmpTableTdBase = new String(skusTableTdBase);
            var skuex = new String(productSKU.SKU ? productSKU.SKU + "-" + (lockedInIndex + 1) : "");
            dataTR.append(tmpTableTdBase.format(
                        skuex,
                        parseFloat(productSKU.SalePrice).toFixed(2),
                        parseFloat(productSKU.CostPrice).toFixed(2),
                        productSKU.Stock,
                        productSKU.AlertStock,
                        productSKU.Weight,
                        "checked='checked'"
                    ));
        })(j);
    }
    GeneratedTempSkuInfo();
    $("#Hidden_TempSKUInfo").val('');
}

function GetBaseSKU() {
    var txtProductSKU, txtSalePrice, txtCostPrice, txtStock, txtAlertStock, txtWeight, chkUpselling;
    txtProductSKU = $('[id$=txtProductSKU]').val();
    if ($('[id$=txtSalePrice]').val()) {
        txtSalePrice = parseFloat($('[id$=txtSalePrice]').val());
    } else {
        txtSalePrice = 0;
    }
    if ($('[id$=txtCostPrice]').val()) {
        txtCostPrice = parseFloat($('[id$=txtCostPrice]').val());
    } else {
        txtCostPrice = 0;
    }
    if ($('[id$=txtStock]').val()) {
        txtStock = parseInt($('[id$=txtStock]').val());
    } else {
        txtStock = 0;
    }
    if ($('[id$=txtAlertStock]').val()) {
        txtAlertStock = parseInt($('[id$=txtAlertStock]').val());
    } else {
        txtAlertStock = 0;
    }
    if ($('[id$=txtWeight]').val()) {
        txtWeight = parseInt($('[id$=txtWeight]').val());
    } else {
        txtWeight = 0;
    }
    if ($('[id$=chkUpselling]').length > 0) {
        chkUpselling = $('[id$=chkUpselling]').prop('checked');
    } else {
        chkUpselling = $($('[id$=rblUpselling]')).find(':input')[0].checked ? $($('[id$=rblUpselling]')).find(':input')[0].checked : $($('[id$=rblUpselling]')).find(':input')[1].checked;
    }

    var res = { "SKU": txtProductSKU,
        "SalePrice": txtSalePrice ? txtSalePrice : 0,
        "CostPrice": txtCostPrice ? txtCostPrice : 0,
        "Stock": txtStock,
        "AlertStock": txtAlertStock,
        "Weight": txtWeight,
        "Upselling": chkUpselling,
        "SKUItems": []
    };
    return res;
}

function GetSelectedSKU() {
    var txtProductSKU, txtSalePrice, txtCostPrice, txtStock, txtAlertStock, txtWeight, chkUpselling,
        SKUInfos, SKUItems, SKUItemTR, SKUValueTD;
    SKUInfos = [];
    SKUItemTR = contetSKUs.find('#GeneralSKUs').find(".SKUItemTR");
    SKUItemTR.each(function () {
        txtProductSKU = $(this).find('#SKU').val();
        txtSalePrice = $(this).find('#SalePrice').val();
        txtCostPrice = $(this).find('#CostPrice').val();
        txtStock = $(this).find('#Stock').val();
        txtAlertStock = $(this).find('#AlertStock').val();
        txtWeight = $(this).find('#Weight').val();
        chkUpselling = $(this).find('#Upselling').prop('checked');
        SKUItems = [];

        SKUValueTD = $(this).find('.SKUValueTD');
        SKUValueTD.each(function () {
            //Add SKUItem
            var valueStr = "";
            var imagepath = "";
            if (parseInt(definepic) == parseInt($(this).attr('attributeid'))) {
                valueStr = $($(".SkuLabel_" + $(this).attr('valueid'))[0]).text();
                imagepath = !$("#imgURL_" + $(this).attr('valueid')).attr("value") ? "" : $("#imgURL_" + $(this).attr('valueid')).attr("value");
            }
            SKUItems.push(
                {
                    "AttributeId": parseInt($(this).attr('attributeid')),
                    "ValueId": parseInt($(this).attr('valueid')),
                    "ValueStr": valueStr,
                    "ImageUrl": imagepath
                });
        });

        //Add SKUInfo
        SKUInfos.push({
            "SKU": txtProductSKU,
            "SalePrice": txtSalePrice ? parseFloat(txtSalePrice) : "",
            "CostPrice": txtCostPrice ? parseFloat(txtCostPrice) : "",
            "Stock": txtStock ? parseInt(txtStock) : "",
            "AlertStock": txtAlertStock ? parseInt(txtAlertStock) : "",
            "Weight": txtWeight ? parseInt(txtWeight) : "",
            "Upselling": chkUpselling,
            "SKUItems": SKUItems
        });
    });
    return SKUInfos.length > 0 ? SKUInfos : undefined;
}

function GetAttributes() {
    var attributeTable = contetAttributesEx.find('table');
    var attributeid, attributemode, attributinfo;
    var attributeJson = [];
    attributeTable.each(function () {
        attributeid = parseInt($(this).attr('attributeid'));
        attributemode = parseInt($(this).attr('attributemode'));

        attributinfo = {
            "AttributeId": attributeid,
            "AttributeMode": attributemode
        };

        switch (attributemode) {
            case 0:
                if ($(this).find(':checked').length == 0) {
                    return null;
                }
                attributinfo.ValueItem = parseInt($(this).find(':checked').val());
                break;
            case 1:
                attributinfo.ValueItem = [];
                if ($(this).find(':checked').length == 0) {
                    return null;
                }
                $(this).find(':checked').each(function () {
                    attributinfo.ValueItem.push(parseInt($(this).val()));
                });
                break;
            case 2:
                attributinfo.ValueItem = $(this).find('[type=text]').val();
                if (!attributinfo.ValueItem) {
                    return null;
                }
                break;
            default:
                alert('扩展属性获取失败, 指定数据类型不存在!');
                return;
        }
        attributeJson.push(attributinfo);
    });
    return attributeJson;
}

//} ()); 