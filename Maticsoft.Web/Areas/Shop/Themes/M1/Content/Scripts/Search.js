function CheckSearchForm() {
    var keyword = $(".KeyWordButton").val();
    if (keyword == null) {
        keyword = "";
    }
    keyword = jQuery.trim(keyword);
    $(".KeyWordButton").val(keyword);
    var defaultTip = "输入您要搜索的内容";
    return (keyword != null && keyword != "" && keyword != defaultTip);
}

function BindClassesEvent() {

    $("#search-menu strong.sub-title").find("a.menutool[autoOpen='true']").each(function() {
        if ($(this).parent().next(".sub-list").size() > 0) {
            $(this).parent().next(".sub-list").toggle();
            $(this).toggleClass("packup");
        };
    });

    $("#search-menu strong.sub-title").find("a.menutool").click(function() {
        if ($(this).parent().next(".sub-list").size() > 0) {
            $(this).parent().next(".sub-list").toggle();
            $(this).toggleClass("packup");
            return false;
        };
    });

    $("#search-menu strong.sub-title").each(function() {
        if ($(this).next(".sub-list").size() === 0) {
            $(this).find(".menutool").css("visibility", "hidden");
        }
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
                        var link = "http://product.maticsoft.com/" + curStyle.StyleType + "-" + curStyle.StyleId + ".htm#rel=ZJLL011";
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

function GetAreaHotComment() {
    if (typeof(styleClassInfo) != "undefined" && styleClassInfo != null && styleClassInfo.ClassId1 != null && styleClassInfo.ClassId1 != "") {
        $.ajax({
            url: "http://comm.maticsoft.com/comment/hotcomment.htm?from=list&size=8&class=" + (styleClassInfo.ClassId1 == null ? "": styleClassInfo.ClassId1),
            dataType: "jsonp",
            success: function(data) {
                if (data.length > 0) {
                    var itemsHtml = "";
                    $.each(data,
                    function(i, n) {

                        var tmpLink = "http://product.maticsoft.com/p-" + n.styleid + ".htm#rel=RPSP011";
                        itemsHtml += "<li class=\"item-s\">" + "<div class=\"item-s-hd\">" + "		<a title=\"" + unescape(n.name) + "\" href=\"" + tmpLink + "\" class=\"item-s-title\" target=\"_blank\">" + unescape(n.name) + "</a>" + "		<p class=\"item-s-price\"><span>" + unescape(n.price).replace(/￥/, '') + "</span></p>" + "	</div>" + "	<a title=\"" + unescape(n.name) + "\" href=\"" + tmpLink + "\" class=\"item-s-pic\" target=\"_blank\"><img height=\"65\" width=\"50\" alt=\"\" src=\"" + n.image + "\"></a>" + "	<div class=\"item-s-txt\">" + "		<p class=\"item-s-name\"><a href=\"http://myshishang.maticsoft.com/share/" + n.contactId + "-" + n.hmac + "-1.htm\">" + unescape(n.member) + "</a></p>" + "		<p class=\"item-s-comm\"><a title=\"" + unescape(n.content) + "\" href=\"" + tmpLink + "\">" + unescape(n.content) + "</a></p>" + "	</div>" + "</li>";

                    });

                    $("#areaHotComment").find("ul").append(itemsHtml) $("#areaHotComment").show();
                }
            },
            error: function() {}
        });
    }
}
//精简，展开事件绑定
function BindPropertyToggle() {
    var typeItem = $("#toggleTypes");
    //属性更多选项
    $(typeItem).click(function() {
        if ($(this).hasClass("inscroll")) {
            $(this).removeClass('inscroll') $(this).addClass('exscroll').text('更多选项');
            $('.proTypeBox:gt(1)').hide();
        } else {
            $(this).removeClass('exscroll') $(this).addClass('inscroll').text('精简选项');
            $('.proTypeBox:gt(1)').show();
        }
    });
    //品牌展开收起
    $("#toggleTradeMark").click(function() {
        if ($(this).hasClass("ex")) {
            $(this).removeClass('ex');
            $(this).addClass('in').text('收起');
            $("ul.brandBox ").addClass('mt35');
            $("ul.brandBox ").children('li:gt(7)').show();
        } else {
            $(this).removeClass('in') $(this).addClass('ex').text('展开');
            $("ul.brandBox ").removeClass('mt35');
            $("ul.brandBox ").children('li:gt(7)').hide();
        }
    });
    //属性展开收起
    $(".toggleValues").click(function() {
        if ($(this).hasClass("ex")) {
            $(this).removeClass('ex');
            $(this).addClass('in').text('收起');
            $(this).prev().children('li:gt(4)').show();
        } else {
            $(this).removeClass('in') $(this).addClass('ex').text('展开');
            $(this).prev().children('li:gt(4)').hide();
        }
    });
}

//SKU的click事件
function SKUChangeBind(styleId, skuId) {
    var thisli = $("li[styleId=" + styleId + "] .SkuColorSelect a[skuId=" + skuId + "]");
    var skuid = $(thisli).attr("skuId");

    //选中标志
    $("li[styleId=" + styleId + "] .SkuColorSelect li[class='cur']").attr("class", "");
    $("li[styleId=" + styleId + "] .SkuColorSelect a[skuId=" + skuid + "]").parent().attr("class", "cur");
    //换图片
    var skuImg = $(thisli).find("img").eq(0).attr("src");
    if (skuImg.indexOf("COLOR") > -1) {
        var newSrc = $(thisli).attr("imgName");
        $("li[StyleId=" + styleId + "] img").eq(0).attr("src", newSrc);

        //跟新链接,链接到单品页带SKU
        ChangeHrefBySkuId(styleId, skuId)

    }

};
//跟新链接,链接到单品页带SKU
function ChangeHrefBySkuId(styleId, skuId) {

    //图片链接
    var itemHref = $("li[StyleId=" + styleId + "] a.ForSkuLink").eq(0);
    //评论链接
    var commHref = $("li[StyleId=" + styleId + "] a.CommSkuLink").eq(0);
    //是否本来就带有SkuId
    var oldSkuId = itemHref.attr("href").indexOf("color=");
    var itemNewHref = "";
    //有老的SkuId则替换
    if (oldSkuId != undefined && oldSkuId > 0) {
        itemNewHref = itemHref.attr("href").substring(0, oldSkuId + 6) + skuId;
    } else {
        itemNewHref = itemHref.attr("href") + "?color=" + skuId;
    }
    itemHref.attr("href", itemNewHref);
    commHref.attr("href", itemNewHref + "#comment");

};
//价格范围url
function ElementsClick(obj) {
    if ($(obj).attr("id") == "SubPrice") {
        var url = $(obj).attr("oriHref");
        var price1 = $("#txtPrice1").val();
        var price2 = $("#txtPrice2").val();

        if (price1 != "" && price2 != "") {
            price1 = parseInt(price1);
            price2 = parseInt(price2);
            if (price1 > price2) {
                var tmp = price1;
                price1 = price2;
                price2 = tmp;
            }
            $(obj).attr("href", url + "?MaxPrice=" + price2 + "&MinPrice=" + price1 + "#Sort");
            return true;
        } else if (price1 != "") {
            $(obj).attr("href", url + "?MaxPrice=" + price2 + "&MinPrice=" + price1 + "#Sort");
            return true;
        } else if (price2 != "") {
            price1 = 0;
            $(obj).attr("href", url + "?MaxPrice=" + price2 + "&MinPrice=" + price1 + "#Sort");
            return true;
        } else {
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
                if (splitCount == 3) {
                    start = i + 1;

                } else if (splitCount == 4) {
                    end = i - 1;
                }
            }

        }
        var nowurl = oldurl.substring(0, start) + pageNum + oldurl.substring(end + 1);
        $(obj).attr("href", nowurl);
        return true;
    }

}
//检测规范
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
//AJAX
function SetUpAjaxLoading() {

    var links = $("#mainDiv").find("[canAjax=1]");
    if (links && links.length > 0) {

        links.each(function() {
            var cur = $(this);
            cur.click(function() {
                var toAjax = true;
                if (cur.attr("overrideClick") != null && cur.attr("overrideClick") == 1) {
                    toAjax = ElementsClick(this);
                }

                if (toAjax) {
                    dialog.open();
                    dialog.center();
                    var ajaxUrl = BuildAjaxLink(cur.attr("href"));
                    $('#J-pengding div.J_dialog_hd').css({
                        height: '0',
                        overflow: 'hidden'
                    });
                    var coreUrl = ajaxUrl;
                    var hashPosition = "";
                    if (ajaxUrl.indexOf("#") > 0) {
                        coreUrl = ajaxUrl.substring(0, ajaxUrl.indexOf("#"));
                        hashPosition = ajaxUrl.substring(ajaxUrl.indexOf("#") + 1);

                    }
                    $.ajax({
                        type: "get",
                        contentType: "text/html",
                        url: coreUrl,
                        data: null,
                        dataType: "html",
                        //返回的类型为Json
                        success: function(result) {
                            $("#mainDiv").find("[ajaxContent=1]").remove();
                            var newContent = $(result);
                            var pos = $("#ajaxContentPosition");
                            pos.after(newContent);
                            dialog.close();

                            if (hashPosition != null && hashPosition != "") {
                                window.location.hash = "";
                                window.location.hash = hashPosition;
                            }
                            BindPropertyToggle();
                            if (isOpenAjax) {
                                SetUpAjaxLoading();
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
                            }) new LazyLoad('#J_lazyload');
                            return false;
                        },
                        error: function(result, status) {
                            dialog.close();
                            return true;
                        }
                    });
                    return false;
                }
                return false;

            });
        });
    }
}

function BuildAjaxLink(link) {
    if (link == null || link.length == 0) {
        return "";
    }

    var index = link.indexOf("maticsoft.com/");
    if (index < 0) {

} else {
        index += 7;
    }
    var newLink = link.substring(0, index + 1) + "Service/" + link.substring(index + 1);
    return newLink;
}

function GetRecommend() {
    var id1 = "";
    var id2 = "";
    var id3 = "";
    if (typeof(styleClassInfo) == "undefined" || styleClassInfo == null) {
        id1 = "N1";
    } else {
        id1 = styleClassInfo.ClassId1 == null ? "N1": styleClassInfo.ClassId1;
        id2 = styleClassInfo.ClassId2 == null ? "": styleClassInfo.ClassId2;
        id3 = styleClassInfo.ClassId3 == null ? "": styleClassInfo.ClassId3;
    }

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
                        var content = '<div class="item-cont item-m-v">' + '<a class="item-m-img" target="_blank" href="http://product.maticsoft.com/p-' + item.StyleId + '.htm#rel=LTXY011" title=""><img height="120" width="92" src="' + item.ImageFileName + '" alt="" /></a>' + '	<div class="item-m-txt">' + '	<a class="item-m-title" target="_blank" href="http://product.maticsoft.com/p-' + item.StyleId + '.htm#rel=LTXY011" title="">' + item.ChineseName + '</a>' + '	<p class="item-m-price"><span>' + item.WebSalePrice + '</span></p>' + '</div>' + '</div>';
                        if ($("#divChooseClothes") != null) {
                            $("#divChooseClothes").after(content);
                        }
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
            var link = "http://product.maticsoft.com/" + curStyle.StyleType + "-" + curStyle.StyleId + ".htm#rel=CNXH011";

            itemsHtml += "<li class=\"item-m\">" + "<a href=\"" + link + "\" title=\"" + curStyle.ChineseName + "\" target=\"_blank\">" + "<img height=\"120\" width=\"92\" class=\"item-m-pic\" src=\"" + curStyle.SuperMapImageUrl + "\" alt=\"" + curStyle.ChineseName + "\">" + "<span class=\"item-m-title\">" + curStyle.ChineseName + "</span>" + "</a>" + "<p class=\"item-s-price\"><span>" + curStyle.WebSalePriceDisplay + "</span>" + (curStyle.GPPrice > 0 ? ("<em style=\"display:none;\" class=\"item-s-point\">+" + curStyle.GPPriceDisplay + "积分</em>") : "") + "</p>" + "</li>";

        }

        $("#areaRecommend").find("ul").html(itemsHtml);
        $("#areaRecommend").show();
    }
}
function DisplayAllLeftMenu() {

    if ($("#search-menu li [class='sub-title first-title']").length == 1) {
        var tempbuttom = $("#search-menu strong.sub-title").find("a.menutool") if ($(tempbuttom).parent().next(".sub-list").size() > 0) {
            $(tempbuttom).parent().next(".sub-list").show();
            $(tempbuttom).addClass("packup");
        };

    }

}
//修改面包屑数字
function ChangeBreadCount() {
    if (productcount != undefined) {
        $("#spPCount").html(productcount);
    }
}

function BindSearchResultEvent() {
    if (isOpenAjax) {
        SetUpAjaxLoading();
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

    BindClassesEvent();
    GetAreaHotComment();
    ProcessLastView();
    GetRecommend();

    DisplayAllLeftMenu();

}