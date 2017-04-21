$(function() {
    if (isOpenAjax) {
        SetUpAjaxLoading('init');
    } else {
        $("a[overrideClick]").click(function() {
            return ElementsClick(this);
        });
    }
    $("#txtPrice1").blur(function() {
        CheckNumber($(this));
    }) $("#txtPrice2").blur(function() {
        CheckNumber($(this));
    }) $("#txtPage").blur(function() {
        CheckNumber($(this));
    })
    //ChangeSKU
    var $skuObj = $(".SkuColorSelect").find("a");
    $skuObj.mouseover(function() {
        var $me = $(this);
        skuChangeHover($me);
    });
     GetAreaHotComment();
    ProcessLastView();
    GetRecommend();
    GetFirstHotComment();
});

function SetUpAjaxLoading(type) {
    try {
        var links = $("#mainDiv").find("[canAjax=1]");
        if (links && links.length > 0) {
            links.unbind("click");
            links.each(function() {
                var cur = $(this);
                cur.click(function() {
                    var toAjax = true;
                    if (cur.attr("overrideClick") != null && cur.attr("overrideClick") == 1) {
                        toAjax = ElementsClick(this);
                    }
                    if (cur.attr("pos") != null && cur.attr("pos") == "bottom") {
                        hashInfo.pos = "bottom";
                    } else {
                        hashInfo.pos = "";
                    }
                    if (toAjax) {
                        DoAjax(cur.attr("href"), cur.attr("anchor"));
                    }
                    return false;
                });
            });
        }
    } catch(ex) {
        var targetUrl = hashInfo.url;
        if (type == "init") {
            targetUrl = window.location.href;
        }
        AddJsErrorLog(targetUrl, "SetUpAjaxLoading" + type, "message:" + ex.message + ";lineNumber:" + ex.lineNumber);
    }
}

function DoAjax(url, anchorName) {
    try {
        url = $.trim(url);
        if (anchorName != "" && anchorName != null && typeof(anchorName) != "undefined") {
            var tmpIndex = url.toLowerCase().indexOf(".maticsoft.com/") + 8;
            url += "#" + url.substring(tmpIndex + 1, url.length - 4) + "-" + anchorName;
        }
        BuildAjaxLink(url);
        if (hashInfo.current != hashInfo.target && hashInfo.url.length > 0) {
            var h = $("#J_lazyload").height();
            $(".loadingmask").css("height", h);
            $(".loadingmask-bg").css("height", h);
            $(".loadingmask").show();
            $(".fixtop").show();
            $.ajax({
                type: "get",
                url: hashInfo.url,
                //cache: false,
                //tt: new Date(),
                data: null,
                dataType: "json",
                async: true,
                contentType: "application/json; charset=utf-8",
                success: function(result) {
                    try {
                        // 更新数据
                        UpdateUI(result);
                        hashInfo.current = hashInfo.target;
                        hashInfo.Repeatflag = true;
                        // 维护Filter展开（收起）区域
                        BindFilterToggle();
                        if (isOpenAjax) {
                            SetUpAjaxLoading('ajaxInit');
                        } else {
                            $("a[overrideClick]").unbind("click");
                            $("a[overrideClick]").click(function() {
                                return ElementsClick(this);
                            });
                        }
                        $("#txtPrice1").blur(function() {
                            CheckNumber($(this));
                        }) $("#txtPrice2").blur(function() {
                            CheckNumber($(this));
                        }) $("#txtPage").blur(function() {
                            CheckNumber($(this));
                        })
                        // 维护Hash
                        if (hashInfo.anchor != null && hashInfo.anchor != "") {
                            var windowHash = window.location.hash;
                            var toHash = hashInfo.current + "-" + hashInfo.anchor;
                            if (windowHash == null) {
                                windowHash = "";
                            }
                            if (windowHash.indexOf("#") >= 0) {
                                windowHash = windowHash.replace(/#/g, "");
                            }
                            var targetAnchor = hashInfo.current + "_" + hashInfo.anchor;
                            var targetObject = $("#" + targetAnchor);
                            if (toHash != windowHash && targetObject.length > 0) {
                                window.location.hash = toHash;
                            }
                            if (targetObject.length > 0) {
                                ScrollToTargetAnchor(targetAnchor);
                            }
                        }
                        if (typeof(LazyLoad) == "undefined" || LazyLoad == null) {
                            ImportScriptionFile("http://img.maticsoft.com/web/j/app/comm/lazyLoad.js");
                            setTimeout("new LazyLoad('#J_lazyload')", 500);
                        } else {
                            new LazyLoad('#J_lazyload');
                        }
                        //ChangeSKU
                        var $skuObj = $(".SkuColorSelect").find("a");
                        $skuObj.mouseover(function() {
                            var $me = $(this);
                            skuChangeHover($me);
                        })
                    } catch(ex) {
                        $(".loadingmask").hide();
                        AddJsErrorLog(hashInfo.url, "DoAjaxSuccess", "message:" + ex.message + ";lineNumber:" + ex.lineNumber);
                        setTimeout("RefreshTargetUrl()", 500);
                    }
                },
                error: function(result, status) {
                    $(".loadingmask").hide();
                    try {
                        var content = "";
                        var start = result.responseText.search("<title>") + 7;
                        var end = result.responseText.search("</title>");
                        if (start > 0 && end > 0) {
                            content = result.responseText.substring(start, end);
                        }
                        start = result.responseText.search("<code><pre>") + 11;
                        end = result.responseText.search("</pre></code>");
                        if (start > 0 && end > 0) {
                            content += result.responseText.substring(start, end);
                        }
                        AddJsErrorLog(hashInfo.url, "DoAjaxAjax", "status:" + status + ";Contetn:" + content + ";Content-Type:" + (result.getResponseHeader("content-type") ? result.getResponseHeader("content-type") : ""));
                    } catch(ex) {}
                    setTimeout("RefreshTargetUrl()", 500);
                }
            });
        }
    } catch(ex) {
        AddJsErrorLog(url, "DoAjax", "message:" + ex.message + ";lineNumber:" + ex.lineNumber);
    }
    return false;
}

function RefreshTargetUrl() {
    var host = window.location.host;
    window.location.href = "http://" + host + "/" + hashInfo.target + ".htm";
}
function BuildAjaxLink(link) {
    hashInfo.target = "";
    hashInfo.anchor = "";
    hashInfo.url = "";
    if (link == null || link.length == 0) {
        return;
    }

    //var currentHash
    var tmp = "";
    var tmpIndex = link.indexOf("#");
    if (tmpIndex >= 0) {
        tmp = link.substring(tmpIndex + 1);
    }
    tmpIndex = tmp.lastIndexOf("-");

    if (tmpIndex >= 0) {
        hashInfo.target = tmp.substring(0, tmpIndex);
        hashInfo.anchor = tmp.substring(tmpIndex + 1);
    } else {
        hashInfo.target = hashInfo.init;
        hashInfo.anchor = "";
    }
    if (hashInfo.current != "" && hashInfo.target != "" && hashInfo.current != hashInfo.target) {
        var tempAnchorIndex = hashInfo.anchor.indexOf("?");
        if (tempAnchorIndex >= 0) {
            hashInfo.anchor = hashInfo.anchor.substring(0, tempAnchorIndex);
        }
        var index = link.toLowerCase().indexOf("maticsoft.com/") + 7;
        hashInfo.url = link.substring(0, index + 1) + "Service/" + hashInfo.target + "-" + (hashInfo.anchor == "" ? "Filter": hashInfo.anchor) + ".htm";
    }
}
//列表页的“热评商品”增加评论列表页入口商品
function GetFirstHotComment() {
    //N1分类下子分类传2级分类参数，其余的传1级分类参数
    var classParam = styleClassInfo.ClassId1 == "N1" && styleClassInfo.ClassId2 != null && styleClassInfo.ClassId2 != undefined && styleClassInfo.ClassId2.length > 0 ? "&class=" + styleClassInfo.ClassId2 + "&level=2": "&class=" + styleClassInfo.ClassId1 + "&level=1";
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "http://comm.maticsoft.com/Comment/HotComment.aspx",
        data: "from=list&type=favorite" + classParam,
        dataType: 'jsonp',
        //返回的类型为Jsonp
        success: function(data) {
            if (data.length > 0) {
                var itemHtml = "";
                var tmpLink = "http://comm.maticsoft.com/comment/" + styleClassInfo.ClassId + "-1-40-1-00-0.htm";
                itemHtml += "<a href=\"" + tmpLink + "\" target=\"_blank\">";
                itemHtml += "<img src=\"" + ImageUrl.ImageUrlConvert(ImageUrl.ImageType.small, data[0].classid3 == null ? data[0].classid2: data[0].classid3, data[0].imagefile) + "\"></a>";
                itemHtml += "<a class=\"hcomment-entrance-txt\" href=\"" + tmpLink + "\" target=\"_blank\">";
                itemHtml += "<span>" + CutWord(unescape(data[0].name), 10) + "</span>";
                itemHtml += "<p>" + CutWord(unescape(data[0].content), 26) + "</p>";
                itemHtml += "<span href=\"" + tmpLink + "\" class=\"btn-mcomm\" target=\"_blank\">更多有用评论</span>";
                itemHtml += "</a>";
                itemHtml += "<div class=\"hcomment-entrance-bg\"></div>";
            }
            $("#FirstHotComment").append(itemHtml);
            $("#FirstHotComment").show();
        },

        error: function() { //如果没有上面的捕获出错会执行这里的回调函数
        }
    });

};
//截取
function CutWord(oldStr, count) {
    if (oldStr.length > count) {
        oldStr = oldStr.substring(0, count);
    }
    return oldStr;
}
function GetRecommend() {
    var id1 = styleClassInfo.ClassId1 == null ? "": styleClassInfo.ClassId1;
    var id2 = styleClassInfo.ClassId2 == null ? "": styleClassInfo.ClassId2;
    var id3 = styleClassInfo.ClassId3 == null ? "": styleClassInfo.ClassId3;
    //1.获取量体选衣推荐商品，如果有结果，则随机显示一件，无结果则显示量体选衣入口
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=UTF-8",
        url: "http://myshishang.maticsoft.com/Service/MyStyleService.ashx",
        data: "classId1=" + id1 + "&classId2=" + id2 + "&classId3=" + id3 + "&topN=" + 5 + "&Method=GetTopSellWellStyleByClassId",
        dataType: "jsonp",
        jsonp: "jsoncallback",
        success: function(result) {
            if (result && result.StyleInfoList && result.StyleInfoList.length > 0) {
                var len = result.StyleInfoList.length;
                var iIndex = parseInt(Math.random() * 100 % len);
                $(result.StyleInfoList).each(function(index, item) {
                    if (index == iIndex) {
                        var content = '<div class="item-m item-cont">' + '<a target="_blank" href="http://product.maticsoft.com/p-' + item.StyleId + '.htm#rel=GXTJ04" title="' + item.ChineseName + '"><img height="120" width="92" src="' + item.ImageFileName + '" alt="" class="item-m-pic"/>' + '<span class="item-m-title">' + item.ChineseName + '</span></a>' + '<p class="item-m-price"><span>' + item.WebSalePrice + '</span></p>' + '</div>';
                        $("#divChooseClothes").after(content);
                        $("#chooesClothes1").hide();
                        $("#chooesClothes2").show();
                    }
                });
            } else {
                $("#chooesClothes1").show();
                $("#chooesClothes2").hide();
            }
        }
    });
    //2.获取根据用户购买记录计算的推荐商品
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "http://productutil.maticsoft.com/IndividualRecommend.aspx",
        data: "Operation=GetRecommendForUser",
        dataType: "jsonp",
        success: function(result) {
            if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {
                FillStyles(result.ReturnValue);
            } else {
                var ids = ""
                if (typeof($.cookie("LastView")) != "undefined" && $.cookie("LastView") != null) {
                    //获取cookie
                    var viewedItemArray = $.cookie("LastView").split("$");
                    //1. 读取Cookie
                    //获取每次浏览记录的数组
                    var viewedItemRetunArray = new Array();
                    //使用Ajax方式
                    //构建Ajax请求的参数
                    for (var i in viewedItemArray) {
                        //若当前浏览记录为空，则跳过进行下次循环
                        if (viewedItemArray[i] == null || viewedItemArray[i] == "") {
                            continue;
                        }
                        ids += viewedItemArray[i] + ",";
                    }
                    if (ids.length > 0) {
                        ids = ids.substring(0, ids.length - 1);
                    }
                }
                $.ajax({
                    //3.获取根据用户浏览记录计算的推荐商品
                    type: "GET",
                    contentType: "application/json",
                    url: "http://productutil.maticsoft.com/IndividualRecommend.aspx",
                    data: "Operation=GetRecommendForStyles&StyleIds=" + ids,
                    dataType: "jsonp",
                    success: function(result) {
                        if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {
                            FillStyles(result.ReturnValue);
                        } else {
                            $.ajax({
                                //4.获取根据女装推荐商品
                                type: "GET",
                                contentType: "application/json",
                                url: "http://productutil.maticsoft.com/IndividualRecommend.aspx",
                                data: "Operation=GetBackUpStyles&ClassId1=N1&ShowCount=6&PageSize=60&PageIndex=2",
                                dataType: "jsonp",
                                success: function(result) {
                                    if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {
                                        FillStyles(result.ReturnValue);
                                    } else {
                                        FillStyles(null);
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }
    });
}

function FillStyles(styles) {
    if (styles == null || styles.length == 0) {
        $("#areaRecommend").find("ul").html("");
        $("#areaRecommend").hide();
    } else {
        var itemsHtml = "";
        for (var i = 0; i < styles.length; i++) {
            var curStyle = styles[i];
            var link = "http://product.maticsoft.com/" + curStyle.StyleType + "-" + curStyle.StyleId + ".htm#rel=GXTJ04";
            itemsHtml += "<li class=\"item-m\">" + "<a href=\"" + link + "\" title=\"" + curStyle.ChineseName + "\" target=\"_blank\">" + "<img height=\"120\" width=\"92\" class=\"item-m-pic\" src=\"" + curStyle.SuperMapImageUrl + "\" alt=\"" + curStyle.ChineseName + "\">" + "<span class=\"item-m-title\">" + curStyle.ChineseName + "</span>" + "</a>" + "<p class=\"item-s-price\"><span>" + curStyle.WebSalePriceDisplay + "</span>" + (curStyle.GPPrice > 0 ? ("<em style=\"display:none;\" class=\"item-s-point\">+" + curStyle.GPPriceDisplay + "积分</em>") : "") + "</p>" + "</li>";
        }
        $("#areaRecommend").find("ul").html(itemsHtml);
        $("#areaRecommend").show();
    }
}

function BuildPriceLink(url, min, max) {
    var start = url.lastIndexOf("/") + 1;
    var end = url.indexOf(".htm");
    var q = url.substring(start, end);

    var arr = q.split("-");
    arr[8] = min;
    arr[9] = max;
    var newq = arr.join("-");
    var newUrl = url.substring(0, url.lastIndexOf("/") + 1) + newq + url.substring(url.indexOf(".htm"), url.indexOf(".htm") + 4);
    var indHash = url.indexOf("#");
    if (indHash > 0) {
        var tmpHash = url.substring(indHash + 1);
        newUrl += "#" + tmpHash;
    }
    return newUrl;
}

function ElementsClick(obj) {
    try {
        if ($(obj).attr("id") == "SubPrice") {
            var url = $(obj).attr("oriHref");
            var price1 = $("#txtPrice1").val();
            var price2 = $("#txtPrice2").val();
            if (price1 != "" || price2 != "") {
                if (price1 != "" && price2 != "") {
                    price1 = parseInt(price1);
                    price2 = parseInt(price2);
                    if (price1 > price2) {
                        var tmp = price1;
                        price1 = price2;
                        price2 = tmp;
                    }
                }
                price1 = price1 == "" ? "N": price1;
                price2 = price2 == "" ? "N": price2;
                $(obj).attr("href", BuildPriceLink(url, price1, price2));
                return true;
            } else {
                $(obj).attr("href", BuildPriceLink(url, 'N', 'N'));
                return true;
            }
        } else {
            var pageNum = $("#txtPage").val();
            if (pageNum == "" || pageNum == undefined) {
                return false;
            }
            var oldurl = $(obj).attr("href");
            var start = 0;
            var end = 0;
            var splitCount = 0;
            for (var i = 0; i < oldurl.length; i++) {
                var letter = oldurl.substring(i, i + 1);
                if (letter == "-") {

                    splitCount++;
                    if (splitCount == 4) {
                        start = i + 1;

                    } else if (splitCount == 5) {
                        end = i - 1;
                    }
                }

            }
            var nowurl = oldurl.substring(0, start) + pageNum + oldurl.substring(end + 1);
            $(obj).attr("href", nowurl);
            return true;
        }
    } catch(ex) {}
}
function CheckNumber(object) {
    var isTxtPage = $(object).attr("id") == "txtPage";
    var txt = object.val();
    if (isTxtPage) {
        if (!CheckIntRegex(txt)) {
            object.val("");
        }
    } else {
        if (!CheckIntRegex(txt) && txt != "0") {
            object.val("");
        }
    }
}
//检测正整数
function CheckIntRegex(value) {
    var myreg = /^[1-9]\d*$/;
    return myreg.test(value);
}

function BindFilterToggle() {
    var allTm = $("div.branditem").find("li");
    if (allTm.length > 8) {
        var isClose = $("div.branditem").find("li:hidden").length > 0;
        var toggleLink = $("<a class=\"icobtn " + (isClose ? "ex": "in") + "\" id=\"toggleTradeMark\" href=\"javascript:void(0);\">" + (isClose ? "展开": "收起") + "</a>");
        $("div.branditem").find("dd").append(toggleLink);
    }

    $("#toggleTradeMark").click(function() {
        if ($(this).hasClass("ex")) {
            $(this).removeClass('ex');
            $(this).addClass('in').text('收起');
            $("ul.brandBox ").children('li:gt(7)').show();
        } else {
            $(this).removeClass('in') $(this).addClass('ex').text('展开');
            $("ul.brandBox ").children('li:gt(7)').hide();
        }
    });
    var allPt = $("#propretyArea").find("div.prop-item");
    allPt.each(function() {
        if ($(this).find("li").length > 7) {
            var isClose = $(this).find("li:hidden").length > 0;
            var toggleLink = $("<a class=\"icobtn " + (isClose ? "ex": "in") + " toggleValues\" href=\"javascript:void(0);\">" + (isClose ? "展开": "收起") + "</a>");
            $(this).find("dd").append(toggleLink);
        }
    });

    if (allPt.length > 2) {
        var isClose = $("#propretyArea").find("div.prop-item:hidden").length > 0;
        var toggleLink = $("<div class=\"act-scroll proTypeChoose\" id=\"proTypeChoose\"><a href=\"javascript:void(0);\" class=\"" + (isClose ? "exscroll": "inscroll") + " db\" id=\"toggleTypes\">" + (isClose ? "更多选项": "精简选项") + "</a></div>");
        $("div.proditembox").append(toggleLink);
    }

    $("#toggleTypes").click(function() {
        if ($(this).hasClass("inscroll")) {
            $(this).removeClass('inscroll') $(this).addClass('exscroll').text('更多选项');
            $('.proTypeBox:gt(1)').hide();
        } else {
            $(this).removeClass('exscroll') $(this).addClass('inscroll').text('精简选项');
            $('.proTypeBox:gt(1)').show();
        }
    });

    $(".toggleValues").click(function() {
        if ($(this).hasClass("ex")) {
            $(this).removeClass('ex');
            $(this).addClass('in').text('收起');
            //$(this).prev().children('li:gt(6)').show();
            $(this).prev().prev().children('li:gt(6)').show();
        } else {
            $(this).removeClass('in') $(this).addClass('ex').text('展开');
            //$(this).prev().children('li:gt(6)').hide();
            $(this).prev().prev().children('li:gt(6)').hide();
        }
    });
}

/*Change SKU
By WD
*/
function skuChangeHover($me) {
    clearTimeout(this.skuChangeTimer);
    function skuChangeFun() {
        //选中标志
        $me.parent().siblings().removeClass("cur");
        $me.parent().addClass("cur");
        //换图片
        var skuImg = $me.find("img").attr("src");
        var skuId = $me.attr("skuid")
        //Editer：jianghaibin
        //UpdateTime：2012-08-29 17:43:00
        if (skuImg.length != 0) {
            var newSrc = $me.attr("imgName"),
            $skuContainer = $me.parents(".item[styleid]");
            $skuContainer.find("img").eq(0).attr("src", newSrc);

            //跟新链接,链接到单品页带SKU
            var itemHref = $skuContainer.find("a.ForSkuLink").eq(0);
            //评论链接
            var commHref = $skuContainer.find("a.CommSkuLink").eq(0);
            //是否本来就带有SkuId
            var oldSkuId = itemHref.attr("href").indexOf("color=");
            var itemNewHref = "";
            //有老的SkuId则替换
            if (oldSkuId != undefined && oldSkuId > 0) {
                itemNewHref = itemHref.attr("href").substring(0, oldSkuId + 6) + skuId;
            } else {
                itemNewHref = itemHref.attr("href") + "#color=" + skuId;
            }
            itemHref.attr("href", itemNewHref);

            //原版本：需要COLOR文件夹的话加上此判断Start
            //        if (skuImg.indexOf("COLOR") > -1) {
            //            var newSrc = $me.attr("imgName"), $skuContainer = $me.parents(".item[styleid]");
            //            $skuContainer.find("img").eq(0).attr("src", newSrc);
            //            //跟新链接,链接到单品页带SKU
            //            var itemHref = $skuContainer.find("a.ForSkuLink").eq(0);
            //            //评论链接
            //            var commHref = $skuContainer.find("a.CommSkuLink").eq(0);
            //            //是否本来就带有SkuId
            //            var oldSkuId = itemHref.attr("href").indexOf("color=");
            //            var itemNewHref = "";
            //            //有老的SkuId则替换
            //            if (oldSkuId != undefined && oldSkuId > 0) {
            //                itemNewHref = itemHref.attr("href").substring(0, oldSkuId + 6) + skuId;
            //            }
            //            else {
            //                itemNewHref = itemHref.attr("href") + "#color=" + skuId;
            //            }
            //            itemHref.attr("href", itemNewHref);
            //原版本：需要COLOR文件夹的话加上此判断END
            //脚本下载到本地之前被注释的
            //            commHref.attr("href", itemNewHref + "#comment");
        }
    }
    this.skuChangeTimer = setTimeout(skuChangeFun, 300)
}

function GetAreaHotComment() {
    $.ajax({
        url: "http://comm.maticsoft.com/comment/hotcomment.htm?from=list&size=8&class=" + (styleClassInfo.ClassId1 == null ? "": styleClassInfo.ClassId1),
        dataType: "jsonp",
        success: function(data) {
            if (data.length > 0) {
                var itemsHtml = "";
                $.each(data,
                function(i, n) {
                    var tmpLink = "http://product.maticsoft.com/p-" + n.styleid + ".htm#rel=splbrmpl";
                    itemsHtml += "<li class=\"item-s\">" + "<div class=\"item-s-hd\">" + "		<a title=\"" + unescape(n.name) + "\" href=\"" + tmpLink + "\" class=\"item-s-title\" target=\"_blank\">" + unescape(n.name) + "</a>" + "		<p class=\"item-s-price\"><span>" + unescape(n.price).replace(/￥/, '') + "</span></p>" + "	</div>" + "	<a title=\"" + unescape(n.name) + "\" href=\"" + tmpLink + "\" class=\"item-s-pic\" target=\"_blank\"><img height=\"65\" width=\"50\" alt=\"\" src=\"" + n.image + "\"></a>" + "	<div class=\"item-s-txt\">" + "		<p class=\"item-s-name\"><a href=\"http://myshishang.maticsoft.com/share/" + n.contactId + "-" + n.hmac + "-1.htm\">" + unescape(n.member) + "</a></p>" + "		<p class=\"item-s-comm\"><a title=\"" + unescape(n.content) + "\" href=\"" + tmpLink + "\">" + unescape(n.content) + "</a></p>" + "	</div>" + "</li>";
                });
                $("#areaHotComment").find("ul").append(itemsHtml) $("#areaHotComment").show();
            }
        },
        error: function() {}
    });

}

function ProcessLastView() {
    $("#clearView").bind("click",
    function() {
        $.cookie("LastView", "", {
            expires: -1,
            path: "/",
            domain: "maticsoft",
            secure: true
        });
        $("#areaLastView ul").hide();
        return false;
    });
    //1、异步读取cookie并展示
    if (typeof($.cookie("LastView")) != "undefined" && $.cookie("LastView") != null) {
        //获取cookie
        var viewedItemArray = $.cookie("LastView").split("$");
        //1. 读取Cookie
        //获取每次浏览记录的数组
        var viewedItemRetunArray = new Array();
        //使用Ajax方式
        //构建Ajax请求的参数
        var ids = ""
        for (var i in viewedItemArray) {
            //若当前浏览记录为空，则跳过进行下次循环
            if (viewedItemArray[i] == null || viewedItemArray[i] == "") {
                continue;
            }
            ids += viewedItemArray[i] + ",";

        }
        if (ids.length > 0) {
            ids = ids.substring(0, ids.length - 1);
        }
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "http://productutil.maticsoft.com/LastView.aspx",
            data: "StyleIds=" + ids,
            dataType: "jsonp",
            success: function(result) {
                var itemsHtml = "";
                if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {
                    for (var i = 0; i < result.ReturnValue.length; i++) {
                        var curStyle = result.ReturnValue[i];
                        var link = "http://product.maticsoft.com/" + curStyle.StyleType + "-" + curStyle.StyleId + ".htm#rel=ZJLL004";
                        itemsHtml += "<li class=\"item-s\">" + "<a title=\"" + curStyle.ChineseName + "\" href=\"" + link + "\" class=\"item-s-pic\" target=\"_blank\"><img height=\"65\" width=\"50\" alt=\"" + curStyle.ChineseName + "\" src=\"" + curStyle.LargeMapImageUrl + "\"></a>" + "<div class=\"item-s-txt\">" + "<a title=\"" + curStyle.ChineseName + "\" href=\"" + link + "\" class=\"item-s-title\" target=\"_blank\">" + curStyle.ChineseName + "</a>" + "<p class=\"item-s-brand\">" + curStyle.TradeMarkName + "</p>" + "<p class=\"item-s-price\"><span>" + curStyle.WebSalePriceDisplay + "</span>" + (curStyle.GPPrice > 0 ? ("<em class=\"item-s-point\">+" + curStyle.GPPriceDisplay + "积分</em>") : "") + "</p>" + "</div>" + "</li>";
                    }
                    $("#areaLastView").find("ul").append(itemsHtml)
                }
                $("#areaLastView").show();
            },
            error: function(result, status) {
                $("#areaLastView").show();
            }
        });
    }
}
///新老图片改变方法    
var ImageUrl = {
    ImageType: {
        large: "Large",
        largemap: "LargeMap",
        origin: "Origin",
        supermap: "SuperMap",
        small: "Small"
    },
    ImageHostAddress: 'http://img.maticsoft',
    GetMapImage: function(ImageFileName, replaceChar) {
        if (ImageFileName != "") {
            ImageFileName = ImageFileName.toLocaleUpperCase();
            var index = ImageFileName.lastIndexOf("S");
            if (index != -1) {
                ImageFileName = ImageFileName.slice(0, index) + replaceChar + ImageFileName.slice(index + 1, ImageFileName.length);
            }
            return ImageFileName;
        }
        return ImageFileName;
    },
    ImageUrlConvert: function(type, classId, imagefile) {
        /// <param name="type">图片类型</param>
        /// <param name="classId">商品三级分类Id/classId3</param>
        /// <param name="imagefile">保存的图片路径/ImageFileName</param>
        if (imagefile == "") return "";
        if (imagefile.indexOf("/") < 0 && imagefile.indexOf("\\") < 0) {
            switch (type) {
            case "Small":
                return ImageUrl.ImageHostAddress + "/GOODS/SMALL/" + classId + "/" + imagefile;
            case "Large":
                return ImageUrl.ImageHostAddress + "/GOODS/LARGE/" + classId + "/" + ImageUrl.GetMapImage(imagefile, 'B');
            case "LargeMap":
                return ImageUrl.ImageHostAddress + "/GOODS/LARGEMAP/" + classId + "/" + ImageUrl.GetMapImage(imagefile, 'B');
            case "Origin":
                return ImageUrl.ImageHostAddress + "/GOODS/ORIGIN/" + classId + "/" + ImageUrl.GetMapImage(imagefile, 'M');
            case "SuperMap":
                return ImageUrl.ImageHostAddress + "/GOODS/SUPERMAP/" + classId + "/" + ImageUrl.GetMapImage(imagefile, 'M');
            default:
                return ImageUrl.ImageHostAddress + "/GOODS/SMALL/" + classId + "/" + imagefile;
            }
        } else {
            switch (type) {
            case "Small":
                return ImageUrl.ImageHostAddress + "/PRODUCT/SMALL/" + imagefile.replace('\\', '/');
            case "Large":
                return ImageUrl.ImageHostAddress + "/PRODUCT/LARGE/" + imagefile.replace('\\', '/');
            case "LargeMap":
                return ImageUrl.ImageHostAddress + "/PRODUCT/LARGEMAP/" + imagefile.replace('\\', '/');
            case "Origin":
                return ImageUrl.ImageHostAddress + "/PRODUCT/ORIGIN/" + imagefile.replace('\\', '/');
            case "SuperMap":
                return ImageUrl.ImageHostAddress + "/PRODUCT/SUPERMAP/" + imagefile.replace('\\', '/');
            default:
                return ImageUrl.ImageHostAddress + "/PRODUCT/SMALL/" + imagefile.replace('\\', '/');
            }
        }
    }
}

/*  将滚动条滚动至当前Anchor位置
*/
function ScrollToTargetAnchor(id) {
    var object = document.getElementById(id);
    if (typeof(object) != "underfined" && object != null) {
        object.scrollIntoView();
    }
}

/* Mask滚动样式
*/
$(window).scroll(function() {
    var loadsize = $("#J_lazyload").offset();
    if ($("#J_lazyload").offset() != undefined && $("#J_lazyload").offset() != null) {
        loadsize = $("#J_lazyload").offset().top;
    }
    if ($(window).scrollTop() > loadsize) {
        $("div.loadingmask-txt").removeClass("fixtop").addClass("fixtedload");
        if ($.browser.msie && $.browser.version == 6) {
            var ftop = $(window).scrollTop() - loadsize;
            $("div.loadingmask-txt").css("top", ftop)
        }
    } else {
        $("div.loadingmask-txt").removeClass("fixtedload").addClass("fixtop");
    }

})

/*  记录Log
*/
function AddJsErrorLog(targetUrl, funName, content) {
    if ((targetUrl == null || targetUrl.length == 0) && (funName == null || funName.length == 0)) {
        return;
    }
    try {
        content += "-------BroswerInfo:" + navigator.appName + "--" + navigator.userAgent + "--" + navigator.appVersion + "--" + navigator.platform + "--" + navigator.javaEnabled() + "--" + navigator.browserLanguage + "--" + (document.charset ? document.charset: document.characterSet);
        $.get("Log.aspx", {
            targetUrl: encodeURI(targetUrl),
            funName: funName,
            content: escape(content)
        });
    } catch(ex) {}
}

function ChangePageTitle(pageTitle) {
    if (pageTitle != undefined && pageTitle != "") {
        document.title = pageTitle;
    }
}

/*  引入新的Js文件
*/
function ImportScriptionFile(src) {
    var jsElement = document.createElement("script");
    jsElement.setAttribute("type", "text/javascript");
    jsElement.setAttribute("src", src);
    document.getElementsByTagName("head")[0].appendChild(jsElement);
}