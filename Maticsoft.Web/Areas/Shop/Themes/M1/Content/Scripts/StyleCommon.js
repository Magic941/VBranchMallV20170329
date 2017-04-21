///<reference path="lib\jquery-1.3.2-vsdoc2.js" />
//用来存储维度1的选中位置
var currSKUId1Array = new Array();
//用来存储维度2的选中位置
var currSKUId2Array = new Array();
var currSKUId2ArrivingNotifyArray = new Array();
/// <summary>
/// 判断字符串是否为null或为empty
/// </summary>
function IsNullOrEmpty(str) {
    if (str == null || str.length == 0) {
        return true;
    }
    return false;
}

/// <summary>
/// 简单的键值对实体
/// </summary>
function KeyValueItem(key, value) {
    this.key = key;
    this.value = value;
};

/// <summary>
/// 判断数组是否包含指定的键的对象，包含返回true，反之返回false
/// </summary>
function IsContainKey(key, keyValueItemArray) {
    for (var index in keyValueItemArray) {
        if (key == keyValueItemArray[index].key) {
            return true;
        }
    }
    return false;
};

/// <summary>
/// 从一个简单的键值对实体数组中根据key获取对象
/// </summary>
function GetItemByKey(key, keyValueItemArray) {
    for (var index in keyValueItemArray) {
        if (key == keyValueItemArray[index].key) {
            return keyValueItemArray[index];
        }
    }
    return null;
};

/// <summary>
/// 修改指定键值对实体数组中指定key的值，如果不存在key，在进行Add操作
/// </summary>
function AddOrUpdateToKeyValueArray(keyValueItem, keyValueItemArray) {
    var flag = false;
    for (var index in keyValueItemArray) {
        if (keyValueItem.key == keyValueItemArray[index].key) {
            keyValueItemArray[index].value = keyValueItem.value;
            flag = true;
        }
    }

    if (!flag) {
        keyValueItemArray.push(keyValueItem);
    }
};

/// <summary>
/// 根据StyleId从指定的Style列表Json对象中获取Style实体
/// </summary>
function GetStyleEntity(styleId, styleList) {
    for (var index in styleList) {
        if (styleId == styleList[index].StyleId) {
            return styleList[index];
        }
    }
};

/// <summary>
/// 根据StyleId从指定json对象中取到SKUItem List
/// </summary>
function GetSKUItems(styleId, skuJsonList) {
    //遍历存放当前页面所有style的维度信息的json对象
    for (var index in skuJsonList) {
        //循环json中与当前styleId匹配的数据
        if (styleId == skuJsonList[index].Key) {
            return skuJsonList[index].Value;
        }
    }
    return null;
};

/// <summary>
/// 判断某SKUDimention1对应的产品在合并后的的SKUItem列表中是否有库存
/// </summary>
function IsWarehouseBySKUDimention1(sKUDimentionId1, itemList, styleEntity) {
    //首先循环SKUItem列表
    for (var index in itemList) {
        if (styleEntity.CheckPOFlag && styleEntity.StockAllocateMode == 0) {
            if (sKUDimentionId1 == itemList[index].SKUDimentionId1 && (itemList[index].IsStock == true || !itemList[index].POFlag)) {
                return true;
            }
        } else {
            if (sKUDimentionId1 == itemList[index].SKUDimentionId1 && itemList[index].IsStock == true) {
                return true;
            }
        }
    }
    return false;
};

//根据SKUDimentionId1从指定SKUItemList中获取此SKUId所匹配的所有SKU Item
//skuItemId:一个SKU Item的SKUDimentionId1
//某个styleId相对应的SKU Item的集合
function GetSKUItemListBySkuId1(skuId1, itemList) {
    var skuItemArray = new Array();
    for (var index in itemList) {
        if (skuId1 == itemList[index].SKUDimentionId1) {
            skuItemArray.push(itemList[index]);
        }
    }
    return skuItemArray;
};

/// <summary>
/// 根据指定的itemId和styleId获取SkuItem对象
/// </summary>
function GetSkuItemByItemIdAndstyleId(styleId, itemId, styleSKUItemsJson) {
    for (var index in styleSKUItemsJson) {
        if (styleId == styleSKUItemsJson[index].Key) {
            for (var j in styleSKUItemsJson[index].Value) {
                if (itemId == styleSKUItemsJson[index].Value[j].ItemId) {
                    return styleSKUItemsJson[index].Value[j];
                }
            }
        }
    }
};

//将字符串转换问Date对象
function StringToDate(str) {
    return new Date(Date.parse(str.replace(/-/g, "/")));
};

/// <summary>
/// 判断某元素是否存在于指定数组中，存在返回true，否则返回false;此方法过期，推荐使用M18Common.js中Array对象的IsContain
/// </summary>
/// <param name="item"> </param>
/// <param name="array"> </param>
function IsInArray(item, array) {
    for (var index in array) {
        if (item == array[index]) {
            return true;
        }
    }
    return false;
};

/// <summary>
/// 根据StyleId获取相对应的库存列表信息
/// </summary>
/// <param name="jsonWH"> 库存的hashTable形式json对象 </param>
/// <param name="styleId"> styleId </param>
function GeyWarehouseByStyleId(styleId, jsonWH) {
    //遍历存放当前页面所有style的维度信息的json对象
    for (var index in jsonWH) {
        //循环json中与当前styleId匹配的数据
        if (styleId == jsonWH[index].Key) {
            return jsonWH[index].Value;
        }
    }
    return null;
};

/// <summary>
/// 根据ProductId从库存列表信息中获取库存对象
/// </summary>
/// <param name="productId"> productId </param>
/// <param name="warehouseList"> 库存对象列表的集合 </param>
function GetWarehouseByProductId(productId, warehouseList) {
    if (typeof (warehouseList) != "undefined" && warehouseList != null && warehouseList.length > 0) {
        for (var index in warehouseList) {
            if (productId == warehouseList[index].ProductId) {
                return warehouseList[index];
            }
        }
    }

    return null;
};

/// <summary>
/// 初始化一级维
/// </summary>
/// <param name="scareBuyingList"> 天天抢信息的对象列表 </param>
function InitDimention1(scareBuyingList) {
    $(".choicebox").each(function () {
        // 1、获取当前需要使用的对象
        var styleId = $(this).attr("styleId");
        var styleEntity = GetStyleEntity(styleId, styleEntityListJson);
        //注：此时的sku已包含库存信息
        var skuItemList = GetSKUItems(styleId, styleSKUItemsJson);

        // 2、在界面显示当前style的Dimention1TypeName和Dimention2TypeName
        var skuDimentionTypeName1 = styleEntity.SKUDimentionTypeName1.indexOf("颜色") >= 0 ? "颜&nbsp;&nbsp;色" : styleEntity.SKUDimentionTypeName1;
        var skuDimentionTypeName2 = styleEntity.SKUDimentionTypeName2.indexOf("尺寸") >= 0 ? "尺&nbsp;&nbsp;寸" : styleEntity.SKUDimentionTypeName2;
        $(this).find(".sku-color-title").prepend(skuDimentionTypeName1 + "：");
        $(this).find(".sku-size-title").prepend(skuDimentionTypeName2 + "：");

        // 3、绘制维度1
        var noStockSku1Array = new Array();
        for (var index in skuItemList) {
            //绘制维度1
            //判断此维度1是否已输出过
            var flagDimention1 = false;
            $(this).find(".sku-color-select li").each(function () {
                var skuDimentionId1 = $(this).attr("skuId1");
                if (skuItemList[index].SKUDimentionId1 == skuDimentionId1) {
                    flagDimention1 = true;
                }
            });

            //判断此SKU1所对应的所有产品是否有库存(混入checkPO逻辑)
            var isStock = IsWarehouseBySKUDimention1(skuItemList[index].SKUDimentionId1, skuItemList, styleEntity);

            //将缺货的skuId1记入数组
            if ((styleEntity.StockAllocateMode == 1 && !isStock) || (styleEntity.StockAllocateMode == 0 && styleEntity.CheckPOFlag && !isStock)) {
                var noStockSku1ArrayFlag = false;
                //判断维度1唯一值是否已存在数组noStockSku1Array中
                for (var i in noStockSku1Array) {
                    if (skuItemList[index].SKUDimentionId1 == noStockSku1Array[i].SKUDimentionId1) {
                        noStockSku1ArrayFlag = true;
                        break;
                    }
                }

                if (!noStockSku1ArrayFlag) {
                    noStockSku1Array.push(skuItemList[index]);
                }
            }

            //如果未输出，则绘制维度1
            if (!flagDimention1) {
                //满足checkPO逻辑，当有库存时才绘制维度1，无库存则不会绘制维度1，会将其移至已售完区域
                if (skuItemList[index].POFlag) {
                    if (isStock == true) {
                        PaintDimention1(skuItemList[index], $(this));
                        // 3、绑定单击事件
                        BindDimention1Click(skuItemList[index], styleEntity.StockAllocateMode, skuItemList, styleEntity, $(this), $(this).find(".sku-color-select li[skuid1=" + skuItemList[index].SKUDimentionId1 + "]"));
                    }
                } else {
                    //（以前的逻辑）不满足checkPO逻辑，只要是无库存压力或有货都会绘制维度1
                    if (styleEntity.StockAllocateMode == 0 || isStock == true) {
                        PaintDimention1(skuItemList[index], $(this));
                        // 3、绑定单击事件
                        BindDimention1Click(skuItemList[index], styleEntity.StockAllocateMode, skuItemList, styleEntity, $(this), $(this).find(".sku-color-select li[skuid1=" + skuItemList[index].SKUDimentionId1 + "]"));
                    }
                }
            }
        }

        // 4、根据sku-color-select元素的数量，决定是否要显示维一的内容
        if ($(this).find(".sku-color-select li").size() == 1) {
            //维1元素仅1个，不显示维度1区域
            if ($(this).find(".sku-color-select li a").eq(0).attr("alt") == "单色") {
                $(this).find(".sku-color-title").hide();
                $(this).find(".sku-color-select").hide();
            }
        }

        // 5、默认选择第一个，并触发第一个元素的click事件
        $(this).find(".sku-color-select li:nth-child(1)").trigger("click");

        // 6、判断维度1的数量和纬度2的数量，若数量都为0，说明该商品不允许超买且没有库存，则隐藏购买区域，显示无库存提示信息
        if ($(this).find(".sku-color-select li").size() == 0 && $(this).find(".sku-size-select li").size() == 0) {
            $(this).html("该商品已售完，我们将尽快补充货源，请随时关注。");
            $(this).attr("class", "error");
            return;
        }

        //7、构建已售完的颜色区域
        if (noStockSku1Array.length > 0) {
            $(this).find(".sku-color-select").after("<div class='colornone mb10 clearfix'><span class='saleoutfont mr5'>售完：</span></div>");
            for (var i in noStockSku1Array) {
                $(this).find(".colornone").append("<img alt='" + noStockSku1Array[i].SKUDimentionName1 + "' title='很抱歉，" + noStockSku1Array[i].SKUDimentionName1 + " 颜色已售完。&#10;您可以选择其它颜色。" + "' src='" + noStockSku1Array[i].SKUColorImageFileName + "' />");
            }
        }

        // 8、为维度1色块加上浮动事件
        BindSku1Event(styleId);
    });
};

/// <summary>
/// 绑定维度1的事件
/// </summary>
function BindDimention1Click(skuItem, stockAllocateMode, itemList, styleEntity, parentObj, objDom) {
    objDom.click(function () {
        //1、更改选中状态
        //更改维1当前li元素的选中状态
        $(".sku-color-select li[class=cur]").attr("class", "");
        $(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").attr("class", "cur");

        // 2、更改维度1所关联的界面文字
        var currStyleId = styleEntity.StyleId;
        if (currStyleId.indexOf("-") >= 0) {
            var index = currStyleId.indexOf("-");
            currStyleId = styleEntity.StyleId.substring(0, index);
        }
        $(".sku-color-title .hl2").text(currStyleId + skuItem.SKUDimentionId1 + "-" + skuItem.SKUDimentionName1);

        //3、按照逻辑绘制维度2的Html
        //获得当前skuDimentionId1所关联的所有SKU Item
        var skuItemList = GetSKUItemListBySkuId1(skuItem.SKUDimentionId1, itemList);
        //绘制维度2，需要先清空目前维度2的内容
        $(".sku-size-select").html("");
        for (var index in skuItemList) {
            //用来标记维度2是否变灰不可买。true为可买，false为不可买
            var noneflag = false;
            //有checkpo逻辑
            if (styleEntity.CheckPOFlag && styleEntity.StockAllocateMode == 0) {
                //根据逻辑判断来绘制SKUDomimention2的Html
                //有库存或不满足checkPO逻辑
                if (skuItemList[index].IsStock || !skuItemList[index].POFlag) {
                    noneflag = true;
                } else {
                    noneflag = false;
                }
            } else {
                //根据逻辑判断来绘制SKUDomimention2的Html
                //允许超卖或有库存才绘制维度2
                if (stockAllocateMode == 0 || skuItemList[index].IsStock) { //有库存
                    noneflag = true;
                }

                //不允许超卖且无库存的判断
                if (stockAllocateMode == 1 && !skuItemList[index].IsStock) {
                    noneflag = false;
                }
            }
            //绘制维度2
            PaintDimention2(noneflag, skuItemList[index], parentObj);
            //4、为维度2绑定事件
            BindDimention2Click(skuItemList[index], styleEntity, parentObj, $(".sku-size-select li[skuid2=" + skuItemList[index].SKUDimentionId2 + "]"));
        }

        // 5、为维度2排序
        parentObj.find(".sku-size-select a[class=none]").parent("li").insertAfter(parentObj.find(".sku-size-select a[class=]").parent("li:last"));

        // 6、触发选中元素的click事件，
        if (IsContainKey(styleEntity.StyleId, currSKUId2Array)) {
            //如果上次有选择
            var currSKUId2 = GetItemByKey(styleEntity.StyleId, currSKUId2Array).value;
            //上次选择的还在且可以被选中就触发它
            if ($(".sku-size-select li[skuId2=" + currSKUId2 + "]").size() > 0 && $(".sku-size-select li[skuId2=" + currSKUId2 + "] a").attr("class") != "none") {
                $(".sku-size-select li[skuId2=" + currSKUId2 + "]").trigger("click");
            } else { //上次选择的在这次没了，也选择第一个
                $(".sku-size-select li:nth-child(1)").trigger("click");
            }
        } else {
            parentObj.find(".sku-size-select li:nth-child(1)").trigger("click");
        }

        // 7、如果维度1仅有1个，维度2也仅有1个，那么维度1、维度2和已选择区域都不显示
        if (parentObj.find(".sku-color-select li").size() == 1 && parentObj.find(".sku-size-select li").size() == 1) {

            if (parentObj.find(".sku-color-select li a").eq(0).attr("alt") == 'null' || parentObj.find(".sku-color-select li a").eq(0).attr("alt") == "单色") {
                parentObj.find(".sku-color-title").hide();
                parentObj.find(".sku-color-select").hide();
            }

            //若唯一一个维度2的值是null，则不显示维度2区域和已选择区域
            if (parentObj.find(".sku-size-select li").eq(0).text() == 'null选中') {
                parentObj.find(".sku-size-title").hide();
                parentObj.find(".sku-size-select").hide();
                parentObj.find(".sku-selected-title").hide();
            }
        }

        var classId1 = styleEntity.ClassId3.substring(0, 2);
        var classId2 = styleEntity.ClassId3.substring(0, 4);
        var classId3 = styleEntity.ClassId3;

        //8、当一级分类为：FY、N4、N2或二级分类为N502的时候， 如果二维（即尺寸维）只有一个001时，不显示二维度，包括“尺寸”两个字。整个二维不显示。
        if (parentObj.find(".sku-size-select li").size() == 1 && (classId1 == "FY" || classId1 == "N4" || classId1 == "N2" || classId2 == "N502") && parentObj.find(".sku-size-select a").eq(0).text() == "001") {

            parentObj.find(".sku-size-title").hide();
            parentObj.find(".sku-size-select").hide();
        }

        //9、强制界面上两个sku2区域显示相同内容
        parentObj.find(".sku-size-select").clone(true).replaceAll($(".sku-size-select"));

        //10、添加查看我的尺码功能
        try {
            sizeLink();
        } catch (e) {

        }

        //11、去掉链接效果
        return false;
    });
};

/// <summary>
/// 绑定维度2的事件
/// </summary>
function BindDimention2Click(skuItem, styleEntity, parentObj, objDom) {
    objDom.click(function () {
        if (objDom.find("a").attr("class") == "none") {
            return false;
        }

        //设置当前选中的Dimention2
        var currSKUId2 = new KeyValueItem();
        currSKUId2.key = styleEntity.StyleId;
        currSKUId2.value = skuItem.SKUDimentionId2;
        AddOrUpdateToKeyValueArray(currSKUId2, currSKUId2Array);

        //1. 更改维2当前li元素的选中状态
        $(".sku-size-select li[class=cur]").attr("class", "");
        $(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").attr("class", "cur");

        //2. 更改维度2所关联的界面文字
        $(".sku-size-title .hl2").text(skuItem.SKUDimentionName2);

        //3. 已选择文字
        var skuName2 = skuItem.SKUDimentionName2 == "001" ? "" : "\"" + skuItem.SKUDimentionName2 + "\"";
        var text = "\"" + skuItem.SKUDimentionName1 + "\" <span id='sku-select-size'>" + skuName2 + "</span>";

        $(".sku-selected-title .hl2").html(text);

        //4. 库存状态文字
        //判读是否区域独卖
        if (skuItem.IsRegionSell) {
            ShowRegionArea(skuItem, objDom);
        } else {
            $(".notice").find(".regionArea").hide();
            $(".sku-notify").text(skuItem.WHShowMsg);
            $(".sku-notify").show();

            //移除掉区域独卖事件
            $("a.sku-addcart").unbind("mouseover");
            $("a.sku-addcart").unbind("mouseout");

            $("a.sku-addcart").each(function () {
                //区分是否是弹出层区域
                if ($(this).parents("#skuDiv").size() > 0) {
                    $(this).text("确定");
                } else {
                    $(this).attr("class", "sku-addcart addcart");
                }
            });

        }

        //5. 为加入购物车的按钮<a>标签加入自定义属性itemId
        $(".sku-addcart").attr("itemid", skuItem.ItemId);

        //6、去掉链接效果
        return false;
    });
};

/// <summary>
/// 展示界面上的区域独卖区域
/// </summary>
function ShowRegionArea(skuItem, objDom) {

    var newRegionArea = skuItem.RegionArea.clone(true);
    skuItem.RegionArea = newRegionArea;
    var parentDiv = objDom.parents(".choicebox");

    parentDiv.find("#pSkuNotify").hide();
    parentDiv.find("a.sku-addcart[isTop=1]").show();
    parentDiv.find("#arrivingNotifyButton").hide();
    parentDiv.find(".sku-notify").hide();

    if (parentDiv.find(".regionArea .storeBox").size() > 0) {
        parentDiv.find(".regionArea .storeBoxF").remove();
        parentDiv.find(".regionArea .storeBox").remove();
    }
    parentDiv.find(".regionArea").prepend(skuItem.RegionArea);
    parentDiv.find(".regionArea").show();

    //区域购买 按钮浮层内容构建
    parentDiv.find("div.AstoreBox div.AstoreItem").hide();

    skuItem.RegionArea.find("div.store").each(function () {
        var whId = $(this).attr("whId");
        parentDiv.find(".AstoreItem[whId=" + whId + "]").show();
    });

    //改变放入购物车按钮的图片，添加mouseover事件
    parentDiv.find("a.sku-addcart").each(function () {
        //区分是否是弹出层区域
        if ($(this).parents("#skuDiv").size() > 0) {
            $(this).text("区域购买");
        } else {
            $(this).attr("class", "sku-addcart areaCart");
        }
    });
    parentDiv.find("a.sku-addcart").bind("mouseover",
    function () {
        $(this).parents(".choicebox").find("ul.handlebox").css("overflow", "visible");
        $(this).parents(".choicebox").find("div.Astore").show();

    });

    parentDiv.find("div.Astore").bind("mouseout",
    function () {
        $(this).parents(".choicebox").find("ul.handlebox").css("overflow", "hidden");
        $(this).parents(".choicebox").find("div.Astore").hide();
    });
    parentDiv.find("div.AstoreBox").bind("mouseover",
    function () {
        $(this).parents(".choicebox").find("ul.handlebox").css("overflow", "visible");
        $(this).parents(".choicebox").find("div.Astore").show();
    });
    //绑定仓库面板事件，显示或隐藏区域的面板
    parentDiv.find(".regionArea .store").bind("mouseover",
    function () {
        var whId = $(this).attr("whId");
        parentDiv.find(".regionArea dl." + whId).show();

        //记录第一个仓库的名称
        var firstStore = parentDiv.find(".regionArea div[whId]:first").attr("whId");
        //记录最后一个仓库的名称
        var endStore = parentDiv.find(".regionArea div[whId]:last").attr("whId");

        if (whId == firstStore) {
            parentDiv.find(".regionArea div[whId=" + whId + "]").addClass("cur sline");
        } else if (whId == endStore) {
            parentDiv.find(".regionArea div[whId=" + whId + "]").addClass("cur");
            parentDiv.find(".regionArea div[whId=" + whId + "]").prev().addClass("sline");
        } else {
            parentDiv.find(".regionArea div[whId=" + whId + "]").addClass("cur sline");
            parentDiv.find(".regionArea div[whId=" + whId + "]").prev().addClass("sline");
        }
    });
    parentDiv.find(".regionArea .store").bind("mouseout",
    function () {
        var whId = $(this).attr("whId");

        parentDiv.find(".regionArea dl." + whId).hide();
        //记录第一个仓库的名称
        var firstStore = parentDiv.find(".regionArea div[whId]:first").attr("whId");
        //记录最后一个仓库的名称
        var endStore = parentDiv.find(".regionArea div[whId]:last").attr("whId");

        if (whId == firstStore) {
            parentDiv.find(".regionArea div[whId=" + whId + "]").removeClass("cur sline");
        } else if (whId == endStore) {
            parentDiv.find(".regionArea div[whId=" + whId + "]").removeClass("cur");
            parentDiv.find(".regionArea div[whId=" + whId + "]").prev().removeClass("sline");
        } else {
            parentDiv.find(".regionArea div[whId=" + whId + "]").removeClass("cur sline");
            parentDiv.find(".regionArea div[whId=" + whId + "]").prev().removeClass("sline");
        }
    });
}

/// <summary>
/// 为维度1的a标签mousemove事件
/// </summary>
function BindSku1Event(styleId) {
    $(".choicebox[styleid=" + styleId + "] .sku-color-select a").bind("mousemove",
    function () {
        $(".bigpic&.fl img").eq(0).attr("src", $(this).find("img").eq(0).attr("largePicUrl"));
        $("#skuDiv .fl img").eq(0).attr("src", $(this).find("img").eq(0).attr("largePicUrl"));

    });
    $(".choicebox[styleid=" + styleId + "] .sku-color-select a").bind("mousemove",
    function () {

        $(".itemlist99&.clearfix img[styleId=" + styleId + "]").eq(0).attr("src", $(this).find("img").eq(0).attr("smallPicUrl"));
        //套组页使用
        $(".showchoice .p-layout img[styleId=" + styleId + "]").eq(0).attr("src", $(this).find("img").eq(0).attr("smallPicUrl"));

    });

};

/// <summary>
/// 绘制维度1中的1个色块
/// </summary>
/// <param name="skuItem"> 一个skuItem对象 </param>
///	<param name="parentObj"> 当前总结点元素的jquery包装集对象 </param>
function PaintDimention1(skuItem, parentObj) {
    //找到ul元素向其插入li元素
    parentObj.find(".sku-color-select").append("<li skuId1='" + skuItem.SKUDimentionId1 + "'></li>");
    //向li节点中添加<a>节点
    parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").append("<a href='#' title='" + skuItem.SKUDimentionName1 + "' alt='" + skuItem.SKUDimentionName1 + "'></a>");
    //向<a>节点中添加<img>节点
    parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "] a").append("<img src='" + skuItem.SKUColorImageFileName + "' largePicUrl='" + skuItem.DisplaySKUImageLarge + "' smallPicUrl='" + skuItem.DisplaySKUImageSmall + "' alt='" + skuItem.SKUDimentionName1 + "' />");
    //向li节点中添加<span>节点
    parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").append("<span>选中</span>");
};

/// <summary>
/// 绘制维度1中的1个色块
/// </summary>
/// <param name="noneflag"> 一个标识位，true标识允许超买，维度2正常显示；false标识不允许超买，维度2做灰色处理 </param>
/// <param name="skuItem"> 一个skuItem对象 </param>
///	<param name="parentObj"> 当前总结点元素的jquery包装集对象 </param>
function PaintDimention2(noneflag, skuItem, parentObj) {
    //向ul节点中添加<li>节点
    $(".sku-size-select").append("<li skuId2='" + skuItem.SKUDimentionId2 + "'></li>");
    //向li节点中添加<a>节点
    if (noneflag) {
        $(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<a href='#' title='" + skuItem.SKUDimentionName2 + "' alt='" + skuItem.SKUDimentionName2 + "'>" + skuItem.SKUDimentionName2 + "</a>");
    } else {
        $(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<a class='none' href='#' title='尺寸：" + skuItem.SKUDimentionName2 + " 已售完' alt='尺寸：" + skuItem.SKUDimentionName2 + " 已售完'>" + skuItem.SKUDimentionName2 + "</a>");
    }
    //向li节点中添加<span>节点
    $(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<span>选中</span>");
};

///	<summary>
///	将库存信息合并到SKU对象中
///	<summary>
function CombSKUAndWH(styleEntityList, styleSKUItemsJson, productWarehouseJson) {
    //三层循环
    for (var i in styleEntityList) {
        //循环StyleEntity
        var skuItemList = GetSKUItems(styleEntityList[i].StyleId, styleSKUItemsJson);
        var stockAllocateMode = styleEntityList[i].StockAllocateMode;

        //构建默认显示的到货信息
        var oneFlag = false;

        //若是无颜色无尺码的商品
        if (skuItemList.length == 1) {
            oneFlag = true;
        }

        for (var j in skuItemList) {
            //循环SKU
            var whItem = GetWarehouseByProductId(skuItemList[j].ProductId, productWarehouseJson);
            if (whItem != null && typeof (whItem.IsRegionSell) != "undefined" && whItem.IsRegionSell) {
                skuItemList[j].IsRegionSell = true;
            }
            //附件文字，默认是颜色+尺寸
            var addMsg = new String();
            if (oneFlag) {
                addMsg = "";
            } else {
                //如果是没有颜色或者颜色是“单色”，文字中去掉“颜色”和“空格”。
                if (skuItemList[j].SKUDimentionName1 != null && skuItemList[j].SKUDimentionName1 != '单色') {
                    addMsg += skuItemList[j].SKUDimentionName1;
                }

                //如果是“均码”或者没有尺码的商品就不显示尺码和这个“号”字。
                if (skuItemList[j].SKUDimentionName2 != null && skuItemList[j].SKUDimentionName2 != '均码') {
                    if (skuItemList[j].SKUDimentionName1 != null && skuItemList[j].SKUDimentionName1 != '单色') {
                        addMsg += " ";
                    }
                    addMsg += skuItemList[j].SKUDimentionName2 + "号";
                }
            }

            if (!skuItemList[j].IsRegionSell) {
                if (typeof (haveLoadWarehouse) == "undefined" || !haveLoadWarehouse) {
                    whItem = {
                        "ExpectReceiptDate": null,
                        "IsStock": true,
                        "POFlag": false,
                        "POType": 0,
                        "ProductId": skuItemList[j].ProductId,
                        "QtyAvailable": 50.00,
                        "WareHouseIdList": null
                    };
                }
            }
            if (typeof (whItem) == "undefined" || whItem == null) { //用来容错
                skuItemList[j].IsStock = false;
                skuItemList[j].ExpectReceiptDate = "";
                skuItemList[j].POType = 0;
                skuItemList[j].POFlag = false;
                skuItemList[j].QtyAvailable = 0;
                if (oneFlag) {
                    skuItemList[j].WHShowMsg = "积极补货中，预计到货时间超过4周。";
                } else {
                    skuItemList[j].WHShowMsg = addMsg + " 正在积极补货中，预计到货时间超过4周。";
                }
            } else {
                //为SkuItem添加库存信息
                skuItemList[j].IsStock = whItem.IsStock;
                skuItemList[j].ExpectReceiptDate = whItem.ExpectReceiptDate;
                skuItemList[j].POType = whItem.POType;
                skuItemList[j].POFlag = whItem.POFlag;
                skuItemList[j].QtyAvailable = whItem.QtyAvailable;

                if (skuItemList[j].IsRegionSell) {
                    skuItemList[j].RegionArea = GenerateRegionSellArea(whItem);
                    skuItemList[j].RegionAreaWHIds = GenerateRegionSellWHIds(whItem);
                    //容错
                    skuItemList[j].WHShowMsg = "";
                } else {
                    skuItemList[j].WHShowMsg = GenerateWarehourseMsg(stockAllocateMode, whItem, addMsg, oneFlag);
                    //容错
                    skuItemList[j].RegionArea = "";
                }
            }
        }

        //将附加了库存的SKUList附加到StyleEntity之上
        styleEntityList[i].SKUList = skuItemList;
    }
};

///	<summary>
///	构建库存文字信息
/// <param name="stockAllocateMode">标识商品是否可以超卖</param>
/// <param name="whItem">当前此Item的库存对象，对应服务端ProductWarehouseBizEntity类</param>
/// <param name="addMsg">此商品若无颜色无尺寸，此值为空字符串，若有，则为 颜色+尺寸</param>
/// <param name="oneFlag" type="Int">标识此商品是否无颜色无尺寸</param>
///	<summary>
function GenerateWarehourseMsg(stockAllocateMode, whItem, addMsg, oneFlag) {
    if (whItem.IsStock) {
        return "库存有货，立即发出";
    }

    if (stockAllocateMode == 0) {
        var dateString = Date.Format(Date.ToDate(whItem.ExpectReceiptDate), "MM月dd日");
        //无库存压力，允许超卖
        //判断预期到货的类型
        switch (whItem.POType) {
            case 0:
                if (oneFlag) {
                    return "积极补货中，预计到货时间超过4周。";
                } else {
                    return addMsg + " 正在积极补货中，预计到货时间超过4周。";
                }
            case 1:
                if (oneFlag) {

                    return "积极补货中，预计" + dateString + "有货。";
                } else {
                    return addMsg + " 正在积极补货中，预计" + dateString + "有货。";
                }
            case 2:
                if (oneFlag) {
                    return "积极补货中，预计到货时间超过4周。";
                } else {
                    return addMsg + " 正在积极补货中，预计到货时间超过4周。";
                }
            default:
                return "积极补货中，预计到货时间超过4周。";
        }
    }
    return "";
};

///	<summary>
///	生成区域独卖区域html，返回一个jquery对象
///	<summary>
function GenerateRegionSellArea(whItem) {
    //若区域独卖库存不存在，则此维度在界面上是不可选的，返回空字符串
    if (!whItem.WareHouseIdList || whItem.WareHouseIdList.length == 0) {
        return $("");
    }

    var html = $('<div class="clearfix storeBoxF"><div class="storeBox clearfix fl mr5"></div><span class="fl">有货</span></div>');
    for (var i in whItem.WareHouseIdList) {
        switch (whItem.WareHouseIdList[i].WarehouseId) {
            case "BJ3":
                if (whItem.WareHouseIdList[i].IsStock == true) {
                    html.find(".storeBox").append('<div whId="BJ3" class="store fl"><p class="border-rf">北京仓</p></div>');
                }
                break;
            case "GZ3":
                if (whItem.WareHouseIdList[i].IsStock == true) {
                    html.find(".storeBox").append('<div whId="GZ3" class="store fl"><p class="border-rf">广州仓</p></div>');
                }
                break;
            case "CD3":
                if (whItem.WareHouseIdList[i].IsStock == true) {
                    html.find(".storeBox").append('<div whId="CD3" class="store fl"><p class="border-rf">成都仓</p></div>');
                }
                break;
            default:
                return "";
        }
    }
    //样式要求，最后一个div样式不加border-rf
    html.find("p:last").attr("class", "");
    return html;
}

function GenerateRegionSellWHIds(whItem) {
    var whIds = [];
    if (!whItem.WareHouseIdList || whItem.WareHouseIdList.length == 0) {
        return whIds;
    }
    for (var i in whItem.WareHouseIdList) {
        if (whItem.WareHouseIdList[i].IsStock && (whItem.WareHouseIdList[i].WarehouseId == "BJ3" || whItem.WareHouseIdList[i].WarehouseId == "GZ3" || whItem.WareHouseIdList[i].WarehouseId == "CD3")) {
            whIds.push(whItem.WareHouseIdList[i].WarehouseId);
        }
    }
    return whIds;
}

//此jQuery静态方法用于常用缓存的处理
function jQueryCache(cID, key, value) {
    var contain = $("<div id='" + cID + "' style=\"display:none;\"></div>");
    $("body").append(contain);
    $("#" + cID).data(key, value);
};

/********************PO使用************************/

/********************PO使用************************/
String.IsNullOrEmpty = function (v) {
    return !(typeof (v) === "string" && v.length != 0);
};

String.prototype.StartWith = function (pattern) {
    return this.lastIndexOf(pattern, 0) === 0;
}

/// <summary>
/// 将一个符合要求的字符串转换为Date格式(字符串为服务端json序列化后的日期或YYYY/MM/DD)
/// </summary>
Date.ToDate = function (str) {
    if (str instanceof Date) {
        return str;
    }

    if (String.IsNullOrEmpty(str)) {
        return null;
    }

    str = str.replace('-', '/').replace('-', '/');

    var date = new Date(str);
    if (!isNaN(date)) {
        return date;
    }

    var reg = /^\/Date\((\d+)(\+0800)*\)\/$/;
    var flag = str.match(reg);
    if (!flag) {
        return null;
    }
    date = new Date(parseFloat(flag[1]));

    if (!isNaN(date)) {
        return date;
    }
    return null;
};

Date.Format = function (date, pattern) {
    if (date == null || !(date instanceof Date)) {
        return "";
    }

    return (date.getMonth() + 1) + "月" + date.getDate() + "日";
};
/********************PO使用************************/
