///<reference path="lib\jquery-1.3.2-vsdoc2.js" />
///<reference path="lib\jquery.cookie.js" />
///<reference path="StyleCommon.js" />

var loadCount = 0;
var curSkuItem = null;
//如果超过product个数太多导致超长，则采用ajax方式获取，避免ie6下获取不到
if (typeof(isTooMuchProducts) != "undefined") {
    if (isTooMuchProducts != null && isTooMuchProducts) {
        $.ajax({
            type: "get",
            contentType: "application/json",
            url: "http://productutil.maticsoft.com/AllProductWarehouse.aspx",
            data: whParam,
            dataType: "jsonp",
            success: function(result) {
                eval(result);
            },
            error: function() {}
        });
    }
}

changeTwoDecimal = function(floatvar) {
    var f_x = parseFloat(floatvar);
    if (isNaN(f_x)) {
        return floatvar;
    }
    f_x = Math.round(f_x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }
    return s_x;
}

function LoadSKU() {
    if (typeof(aloneMessage) != "undefined" && aloneMessage != null && aloneMessage.length > 0) {
        //是单卖产品，执行返回操作
        $(".choicebox").html("");
        $(".choicebox").hide();
        //当该商品下架时，显示同源商品的连接
        if (typeof(styleIdBak) != "undefined" && styleIdBak != null && styleIdBak.length > 0) {

            var link = "http://product.maticsoft.com/p-" + styleIdBak + ".htm#rel=DPY_JGBG";
            $(".choicebox").after($("<div class=\"pInfoFrame pInfohover clearfix choicebox pInfoNogoods \"><div class=\"msg-m msg-info-m f14 c3\"><i class=\"msg-ico\"></i><p class=\"pt5\">亲，本款商品价格已调整，<a id=\"DPY_JGBG\" class=\"fb\" href=\"" + link + "\">点此立即查看最新详情</a></p></div></div>"));

        } else {
            var classId = styleEntityListJson[0].ClassName2 == styleEntityListJson[0].ClassName3 ? styleEntityListJson[0].ClassId2: styleEntityListJson[0].ClassId3;
            var link = "http://list.maticsoft.com/" + styleEntityListJson[0].ClassId1 + "-" + classId + "-12-60-1-00-1-1-N-N-0-0.htm";

            if (!styleEntityListJson[0].SaleOnWebFlag) {
                //<div class=\"tc sallputTips\"><p class=\"mt20 mb15\">" + aloneMessage + "</p><a class=\"f14 fb a2\" href=\"" + link + "\">点击查看其它商品</a></div>
                var likehtml = "<div class=\"pInfoFrame pInfohover clearfix choicebox pInfoNogoods \"><div class=\"msg-m msg-info-m f14 c3\"><i class=\"msg-ico\"></i><p class=\"pt5 fb\">" + aloneMessage + "<a class=\"f14 fb a2\" href=\"" + link + "\">点击查看其它商品</a></p></div></div>";
                if (typeof(saleOutRelation) != 'undefined' && saleOutRelation != null && saleOutRelation.length > 0) {
                    likehtml += '<div class="box scroll-relative">' + '<div class="hd">' + '<div>' + '<div class="h2">相似商品推荐</div>' + '</div>' + '</div>' + '<div class="bd">' + '<div id="ng-relative" class="J_carousel">' + '<ul class="J_carousel_trigger">' + '<li class="fl"><a href="javascript:;" title="向左" class="J_carousel_prev">向左</a></li>' + '<li class="fr"><a href="javascript:;" title="向右" class="J_carousel_next">向右</a></li>' + '</ul>' + '<div class="J_carousel_clip">' + '<ul class="clearfix J_carousel_list list-m">'
                    var num = 12;
                    if (typeof(saleOutRandCount) != 'undefined' && saleOutRandCount != null && saleOutRandCount) num = saleOutRandCount;
                    //alert(saleOutRelation.length)
                    if (saleOutRelation.length >= num) {
                        for (var i = 0; i < num; i++) {
                            //判断如果数组还有可以取出的元素,以防下标越界
                            if (saleOutRelation.length > 0) {
                                //在数组中产生一个随机索引
                                var arrIndex = Math.floor(Math.random() * saleOutRelation.length);
                                //将此随机索引的对应的数组元素值复制出来
                                var res = saleOutRelation[arrIndex];

                                likehtml += '<li class="item-m J_carousel_item">' + '<a name="__DPY_XSSP" href="http://product.maticsoft.com/p-' + res.StyleId + '.htm#rel=DPY_XSSP" title="' + res.ChineseName + '" target="_blank">' + '<img height="120" width="92" class="item-m-pic" src="http://img.maticsoft.com/GOODS/SMALL/' + res.ClassId3 + '/' + res.ImageFileName + '" alt="' + res.ChineseName + '">' + '<span class="item-m-title">' + res.ChineseName + '</span>' + '</a>' + '<p class="item-m-price"><span>' + changeTwoDecimal(res.WebSalePrice) + '</span></p>' + '</li>'
                                //然后删掉此索引的数组元素,这时候temp_array变为新的数组
                                saleOutRelation.splice(arrIndex, 1);
                            } else {
                                //数组中数据项取完后,退出循环,比如数组本来只有10项,但要求取出20项.
                                break;
                            }
                        }
                    } else {
                        for (var i = 0; i < saleOutRelation.length; i++) {
                            var res = saleOutRelation[i];
                            likehtml += '<li class="item-m J_carousel_item">' + '<a name="__DPY_XSSP" href="http://product.maticsoft.com/p-' + res.StyleId + '.htm#rel=DPY_XSSP" title="' + res.ChineseName + '" target="_blank">' + '<img height="120" width="92" class="item-m-pic" src="http://img.maticsoft.com/GOODS/SMALL/' + res.ClassId3 + '/' + res.ImageFileName + '" alt="' + res.ChineseName + '">' + '<span class="item-m-title">' + res.ChineseName + '</span>' + '</a>' + '<p class="item-m-price"><span>' + changeTwoDecimal(res.WebSalePrice) + '</span></p>' + '</li>'
                        }
                    }
                    likehtml += '</ul>' + '</div>' + '</div>' + '</div>' + '</div>'
                }
                $(".choicebox").after($(likehtml));
            } else {
                $(".choicebox").after($("<div class=\"pInfoFrame pInfohover clearfix choicebox pInfoNogoods \"><div class=\"tc sallputTips\"><p class=\"mt20 mb15\">" + aloneMessage + "</p><a class=\"f14 fb a2\" href=\"" + link + "\">点击查看其它商品</a></div></div>"));
            }
        }
        return;
    } else {
        try {
            CombSKUAndWH(styleEntityListJson, styleSKUItemsJson, productWarehouseJson);
        } catch(e) {

            loadCount++;
            if (loadCount <= 50) {
                setTimeout("LoadSKU()", 100);
                return;
            }
        }

        //2、天天抢逻辑判断
        if (typeof(scareBuyingItem) != "undefined" && scareBuyingItem != null) { //判断是否是天天抢
            //是天天抢但无法购买
            if (scareBuyingItem.ErrorMsg != null && scareBuyingItem.ErrorMsg.length > 0 && scareBuyingItem.ErrorMsg != "0") {
                var errorMsg = "";
                if (scareBuyingItem.ErrorMsg == "1") {
                    errorMsg = "对不起，动软团活动尚未开始或已结束。";
                } else if (scareBuyingItem.ErrorMsg == "2") {
                    errorMsg = "对不起，该商品已售完，请查看其它动软团商品。";
                } else if (scareBuyingItem.ErrorMsg == "3") {
                    errorMsg = "对不起，动软团活动尚未开始，请查看其它动软团商品。";
                } else if (scareBuyingItem.ErrorMsg == "4") {
                    errorMsg = "对不起，动软团活动已经结束，请查看其它动软团商品。";
                }
                $(".choicebox").html("");
                $(".choicebox").hide();
                $(".choicebox").after($("<div class=\"pInfoFrame pInfohover clearfix choicebox pInfoNogoods \"><div class=\"tc sallputTips\"><p class=\"mt20 mb15\">" + errorMsg + "</p><a class=\"f14 fb a2\" href=\"/Market/DailyScareBuying.aspx\">查看其它动软团商品</a></div></div>"));
                return;
            } else {
                //该天天抢可以购买，更改最大购买数量
                $(".choicebox").attr("maxqty", scareBuyingItem.MaxQtyPerUser);
            }
        }

        //初始化Dimention1
        InitDimention1ArrivingNotify();

        //5、特殊分类商品逻辑
        SpecialClassLogic();
    }
};

$(document).ready(function() {

    //1、PO逻辑
    var skuItemLists = GetSKUItems(styleEntityListJson[0].StyleId, styleSKUItemsJson);
    StyleDescPOLogic(skuItemLists, styleEntityListJson[0]);

    //隐藏人气区域
    if ($("div[styleid]").eq(0).attr("class") == "error") {
        //隐藏人气区域
        $(".gh-frame").hide();
    }

    //1、默认选中largemap图
    //$(".thumbpic li a").eq(0).attr("class", "cur");
    //2、为Largemap图片绑定over事件和Click事件
    BindPicEvent();

    $("a.vp").bind("click",
    function() {
        UserLoginStatus.isLogin = false;
        $("body").AjaxLogin({
            success: function() {
                var myDiv2 = $("<div id=\"Points_pop_wait\" style=\"display:none;\" class=\"detail-pop\"><div class=\"title\">积分详情</div><p><img src=\"images/load.gif\"/></p><div class=\"close-favlist\"><a href=\"#\" name=\"fClose\">关闭</a></div></div>");
                $("body").append(myDiv2);
                $("#Points_pop_wait").fadeIn("slow");
                m18.Points.GetUserPointsInfoByRequest(ShowUserPoint);
                return false;
            },
            content: "请登录"
        });
        return false;
    });

    $(".splitBox").hover(function() {
        $(this).find(".store").addClass("border-bf");
        $(this).find(".areaStore").show();
    },
    function() {
        $(this).find(".store").removeClass("border-bf");
        $(this).find(".areaStore").hide();
    }) $("a.area-close").click(function() {
        $(this).closest(".splitBox").find(".store").removeClass("border-bf");
        $(this).closest(".areaStore").hide();
        return false;
    }) $(".areaStore").find("a[provinceId]").click(function() {
        var curEle = $(this);
        $(".areaStore").hide();
        $(".splitBox").find(".selecetd-province").attr("displayName", curEle.html()).attr("provinceId", curEle.attr("provinceId"));
        if (typeof(window.m18) != "undefined" && typeof(window.m18.IPAssistant) != "undefined") {
            window.m18.IPAssistant.SetInfos(curEle.attr("provinceId"), "");
            if (window.m18.IPAssistant.AreaInfos.SelectionType == 2) {
                $(".tip-t").hide();
            }
        }

        CheckArea();
    });

    //3、绑定页面tab切换事件
    BindSwitchTab();

    //5、加载购买记录、评论、问答
    ShowPageInfo();

    //6、加载单品页左侧模块
    BindStyleDetailLeft();
    //7、绑定页面事件
    BindEvent();

    appendGoTop();

    document.domain = "maticsoft";

    //8、Large图默认图片是第一个小色块所对应的图，并largeMap图也要保持一致
    var oriSrc = $(".bigpic img").eq(0).attr("oriSrc");
    var blankSrc = $(".bigpic img").eq(0).attr("blankSrc");
    var src = $(".bigpic img").eq(0).attr("src");

    if (src == null || src == "" || src == blankSrc) {
        $(".bigpic img").eq(0).attr("src", oriSrc);
    }

    src = $(".bigpic img").eq(0).attr("src");
    if (src != "") {
        var reg = /.maticsoft\/GOODS\/LARGE\/(\w*)\/(\w*).\w*$/;
        var match = reg.exec(src);
        if (match && match[2]) {
            var picName = match[2];
            $(".thumbpic li").each(function() {
                var liPicName = $(this).find("img").attr("src");
                if (liPicName.indexOf(picName) > -1) {
                    $(".thumbpic a").removeClass();
                    $(this).find("a").addClass("cur");
                }
            });
        }
    }

    $(".thumbpic a").click(function() {
        return false;
    });
    //将天天抢对象放入jquery缓存，方便购物车等逻辑调用
    jQueryCache("divCache", "scareBuyingList", scareBuyingItem);

    if (location.href.indexOf("#comment") > 0) {
        $("#id-buyer-comment").trigger("click");
        window.location.hash = "comment";
    }

    $("#arrivingNotifyButton").click(function() {
        ArrivingNotify.StyleId = $(this).parents("div[styleId]").attr("styleId");
        ArrivingNotify.ProductId = $("#arrivingNotifyButton").attr("productId") ArrivingNotify.ItemId = $("#arrivingNotifyButton").attr("itemid");
        UserLoginStatus.isLogin = false;
        $("body").AjaxLogin({
            success: function() {
                ArrivingNotify.Callback();
            },
            content: "请登录"
        });
        return false;
    });

    var dateTemp = new Date();
    var timeInfo = dateTemp.getFullYear() + '-' + (dateTemp.getMonth() + 1) + '-' + dateTemp.getDate() + " " + dateTemp.getHours() + ":" + dateTemp.getMinutes() + ":" + dateTemp.getSeconds();
    var refer = "";
    if (typeof(document.referrer) != "undefined" && document.referrer != null) {
        refer = document.referrer;
    }
    var cid = ($.cookie("M18_CID") != null) ? $.cookie("M18_CID") : "";

    var imgurl = "http://productutil.maticsoft.com/StyleVisitLog.aspx?Refer=" + escape(refer) + "&Client=" + cid + "&ClientVisitTime=" + timeInfo + "&StyleId=" + styleEntityListJson[0].StyleId;
    $("body").append("<img src='" + imgurl + "' style='display:none' border='0' height='0' width='0' />");

});

/// <summary>
/// 本方法通过异步程序来处理单品页购买记录、评论、问答
/// </summary>
function ShowPageInfo() {
    var params = "'styleId':'" + styleEntityListJson[0].StyleId + "','classId3':'" + styleEntityListJson[0].ClassId3 + "','originalStyleId':'" + styleEntityListJson[0].OriginalStyleId + "'";

    $("#buy-annal").append("<p align='center' class='readyload'><img src='images/load.gif' /></p>");
    $("#buyer-comment").append("<p align='center' class='readyload'><img src='images/load.gif' /></p>");
    $("#buyer-leaveword").append("<p align='center' class='readyload'><img src='images/load.gif' /></p>");

    //3.向买家评论区域填充显示数据
    //GetComment(1);
    //BuyHistory();
    //GetLeaveword();
};

function GetLeaveword() {
    var origionalId = (styleEntityListJson[0].OriginalStyleId == null ? "": styleEntityListJson[0].OriginalStyleId);
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "http://comm.maticsoft.com/Question/IStyleQuestion.aspx",
        data: "originalStyleId=" + origionalId + "&QuestionShowCount=" + questionShowCount,
        dataType: 'jsonp',
        //返回的类型为Jsonp
        success: function(result) {
            $("#buyer-leaveword .readyload").remove();
            FillBuyerLeaveword(result);
        },
        error: function(result, status) { //如果没有上面的捕获出错会执行这里的回调函数
        }
    });
}

function FillBuyerLeaveword(questionList) {
    //此商品无买家问答记录
    if (questionList == null || questionList.length == 0) {
        $("#buyer-leaveword .bd div:first a:last").remove();
        $("#buyer-leaveword .bd div:first").append("<p class='no-content'>暂时没有买家询问此商品</p>");
        $("#buyer-leaveword .bd div:last").hide();
        return;
    }

    var ContactIds = "";
    for (var i in questionList) {
        ContactIds = ContactIds.length > 0 ? ContactIds + "," + questionList[i].ContactId: questionList[i].ContactId;
        var strMoban = "<div class=\"cf\">" + "     <a class=\"discusslist-Item-avatar fl\" target=\"_blank\" href=\"http://myshishang.maticsoft.com/share/" + questionList[i].ContactId + "-" + questionList[i].SpaceKey + "-1.htm\">" + "        <img src=\"images/load.gif\" onmouseout='MemberCard.PopHide()' onmouseover=\"MemberCard.Show(this,'" + questionList[i].ContactId + "')\" ></a>" + "     <div class=\"discusslist-item-con\">" + "         <div class=\"tr c9\">" + "             <span class=\"fl\"><a href=\"http://myshishang.maticsoft.com/share/" + questionList[i].ContactId + "-" + questionList[i].SpaceKey + "-1.htm\" target=\"_blank\">" + "             " + questionList[i].Name + " (" + questionList[i].MembershipName + ")</a>提问：</span>" + "             " + questionList[i].QuestionTime + "         </div>" + "         <p class=\"mt5\">" + questionList[i].Content + "</p>" + "    </div>" + "</div>";
        if (questionList[i].Answer.length > 0) {
            for (var j in questionList[i].Answer) {
                strMoban += " <div class=\"discusslist-back\">" + " 	<div class=\"cf mb5\"><span class=\"fr c9\">" + questionList[i].Answer[j].ReplyTime + "</span><span class=\"h\">" + questionList[i].Answer[j].ReplyName + "</span></div>" + " 	<p>" + questionList[i].Answer[j].ReplyContent + "</p>" + " </div>";
            }
        }
        $("#buyer-leaveword .discusslist").append("<li class=\"discusslist-item cf\">" + strMoban + "</li>");
    }
    //用户图像
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=UTF-8",
        url: "http://myshishang.maticsoft.com/service/ajaxservice.ashx",
        data: "contactIds=" + ContactIds + "&Method=GetShortMembersInfo",
        dataType: "jsonp",
        success: function(result) {
            if (result.Data != null) {
                var objChanges = $("#buyer-leaveword .discusslist-Item-avatar").find("img");
                $(result.Data).each(function(i, n) {
                    objChanges.eq(i).attr("src", n.MemberPhoto);
                });
            }
        }
    });

};

function GetComment(pageIndex) {
    var pageIndex = (pageIndex == null || pageIndex < 0 ? 1 : pageIndex);
    var pageSize = 10;
    var origionalId = (styleEntityListJson[0].OriginalStyleId == null ? "": styleEntityListJson[0].OriginalStyleId);
    if (pageIndex == 1) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "http://comm.maticsoft.com/Comment/IStyleComment.aspx",
            data: "styleid=" + styleEntityListJson[0].StyleId + "&originalStyleId=" + origionalId + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&version=2.0",
            dataType: 'jsonp',
            success: function(result) {
                $("#buyer-comment .readyload").remove();
                FillBuyerComment(result, pageIndex);
            },
            error: function(result, status) {}
        });
    }
    //当加载第二页以上数据时，使用cdn缓存
    else {
        window["commentcallback"] = function(result) {
            $("#buyer-comment .readyload").remove();
            FillBuyerComment(result, pageIndex);
            window["commentcallback"] = undefined;
            try {
                delete window["commentcallback"];
            } catch(e) {}
        }
        var url = "";
        if (origionalId) {
            var url = "http://comm.maticsoft.com/Comment/IStyleComment-commentcallback-" + pageIndex + "-" + pageSize + "-" + styleEntityListJson[0].StyleId + "-" + origionalId + ".htm";
        } else {
            var url = "http://comm.maticsoft.com/Comment/IStyleComment-commentcallback-" + pageIndex + "-" + pageSize + "-" + styleEntityListJson[0].StyleId + ".htm";
        }
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: url,
            dataType: 'script',
            cache: true,
            error: function(result, status) {}
        });
    }

}

/// <summary>
/// 向买家评论区域填充显示数据
/// </summary>
function FillBuyerComment(result, pageIndex) {
    //影藏loading
    $("#buyer-comment img.load-item-img,#buyer-comment div.load-item-mask").hide();
    //此商品无买家评论记录
    if (result == null || result.length == 0 || result.Comments == null || result.Comments.length == 0) {
        //$("#buyer-comment .mt10").eq(0).find("a:last").remove();
        //$("#buyer-comment .mt10").eq(0).append("<p class='no-content'>暂时没有顾客评论此商品</p>");
        $("#buyer-comment .hd").eq(0).find("a:first").remove();
        $("#buyer-comment .faq-vote").html("<p class='no-content'>暂时没有顾客评论此商品</p>") return;
    }

    var commentList = result.Comments;
    var ContactIds = "";

    $("#buyer-comment .discusslist").remove();

    //构建ul元素
    $("#buyer-comment .load-item").append("<ul class='discusslist c6'></ul>");
    for (var i in commentList) {
        ContactIds = ContactIds.length > 0 ? ContactIds + "," + commentList[i].ContactId: commentList[i].ContactId;
        $("#buyer-comment .load-item ul").eq(0).append("<li class='discusslist-item cf'></li>");
        var MembershipName = (commentList[i].MembershipName != null && commentList[i].MembershipName != "" ? " (" + commentList[i].MembershipName + ")": "");
        var innerHtml = "	<div class=\"cf\"  id=\"comment-main-" + i + "\"> " + " 		<a class=\"fl discusslist-Item-avatar\" target=\"_blank\" href=\"\"><img src=\"images/load.gif\" onmouseout='MemberCard.PopHide()' onmouseover=\"MemberCard.Show(this,'" + commentList[i].ContactId + "')\" /></a> " + " 		<div class=\"discusslist-item-con\"> " + " 	  		<div class=\"tr c9\"> <span class=\"fl\"><a href=\"\" target=\"_blank\">" + MembershipName + "</a></span>" + commentList[i].CommentTime + "</div> " + " 	  		<p class=\"mt5\"> " + commentList[i].Content + "</p> " + " 		</div> " + " 	</div> " + "<div class='discusslist-correlate'> " + " <div class='cf'> " + "    <div class='fr'><a class='btn'><span>对我有用(" + commentList[i].FavoriteCount + ")</span></a></div> ";

        if (commentList[i].Height != null && commentList[i].Height != "" && parseInt(jQuery.trim(commentList[i].Height)) > 0) {
            innerHtml += "<span class=\"mr15\">身高：" + parseInt(jQuery.trim(commentList[i].Height)) + "厘米</span>";
        }
        if (commentList[i].Weight != null && commentList[i].Weight != "" && parseInt(jQuery.trim(commentList[i].Weight)) > 0) {
            innerHtml += "<span class=\"mr15\">体重：" + parseInt(jQuery.trim(commentList[i].Weight)) + "公斤</span>";
        }
        if (commentList[i].StyleColor != null && commentList[i].StyleColor != "") {
            innerHtml += "<span class=\"mr15\">颜色：" + commentList[i].StyleColor + "</span>";
        }
        if (commentList[i].StyleSize != null && commentList[i].StyleSize != "") {
            innerHtml += "<span class=\"mr15\">尺码：" + commentList[i].StyleSize + "</span>";
        }
        innerHtml += "</div></div>"$("#buyer-comment .load-item ul li:last-child").append(innerHtml);
    }

    //评论分页
    if (result.TotalPageCount > 1) {
        $("#buyer-comment #pagebox a").each(function() {
            $(this).unbind("click");
        });
        $("#buyer-comment #pagebox").parent("div").remove();

        var strPage = "";

        var nCurr = pageIndex;
        var nPCount = result.TotalPageCount;

        var nLeft = 2;
        var nMiddle = 5;
        var nRight = 2;
        var nHalf = parseInt(nMiddle / 2);

        var nStartP = 1;
        var nEndP = nPCount;

        var setPage = function(i) {
            if (nCurr == i) {
                strPage += "<span class=\"cur\">" + i + "</span>";
            } else {
                strPage += "<a><span>" + i + "</span></a>";
            }
        };

        //previous page
        if (nCurr <= 1) {
            strPage += "";
        } else {
            strPage += "<a class=\"paging-prev\"><span>上一页</span></a>";
        }

        if (nCurr <= nLeft) { //left area
            for (var i = 1; i <= nLeft + nHalf; i++) {
                if (i > nPCount) break;
                setPage(i);
            }
            if (nPCount > nLeft + nHalf) {
                if (nPCount - nHalf > nLeft + nHalf) {
                    strPage += "<span class=\"paging-break\">...</span>";
                }
                nStartP = nPCount - nRight + 1 > nLeft + nHalf ? nPCount - nRight + 1 : nLeft + nHalf + 1;
                for (var i = nStartP; i <= nPCount; i++) {
                    setPage(i);
                }
            }
        } else if (nCurr > nPCount - nRight) { // right area
            for (var i = 1; i <= nLeft; i++) {
                if (i > nPCount) break;
                setPage(i);
            }
            if (nPCount > nLeft) {
                nStartP = nPCount - nRight - nHalf + 1 > nLeft ? nPCount - nRight - nHalf + 1 : nLeft + 1;
                if (nStartP > nLeft + 1) {
                    strPage += "<span class=\"paging-break\">...</span>";
                }
                for (var i = nStartP; i <= nPCount; i++) {
                    setPage(i);
                }
            }
        } else { // middle area
            nStartP = nCurr - nHalf > nLeft ? nCurr - nHalf: nLeft + 1;
            nStartP = nStartP - 1 == nLeft + 1 ? nStartP - 1 : nStartP;
            nEndP = nCurr + nHalf < nPCount ? nCurr + nHalf: nPCount;
            nEndP = nEndP + 1 == nPCount - nRight ? nEndP + 1 : nEndP;
            for (var i = 1; i <= nLeft; i++) {
                if (i > nPCount) break;
                setPage(i);
            }
            if (nStartP > nLeft + 1) {
                strPage += "<span class=\"paging-break\">...</span>";
            }
            for (var i = nStartP; i <= nEndP; i++) {
                if (i > nPCount) break;
                setPage(i);
            }
            if (nEndP < nPCount - nRight) {
                strPage += "<span class=\"paging-break\">...</span>";
            }
            nStartP = nPCount - nRight + 1 > nCurr + nHalf ? nPCount - nRight + 1 : nCurr + nHalf + 1;
            for (var i = nStartP; i <= nPCount; i++) {
                setPage(i);
            }
        }

        //next page
        if (nCurr >= nPCount) {
            strPage += "";
        } else {
            strPage += "<a class=\"paging-next\"><span>下一页</span></a>";
        }

        //append html
        $("#buyer-comment>.bd").append("<div class=\"cf mt15\"><div id=\"pagebox\" class=\"paging fr\">" + strPage + "</div></div>");

        $("#buyer-comment #pagebox a").each(function() {
            $(this).bind("click",
            function() {
                var PageValue = $(this).text();
                if (PageValue == "上一页") {
                    PageValue = parseInt(pageIndex - 1);
                } else if (PageValue == "下一页") {
                    PageValue = parseInt(pageIndex + 1);
                } else {
                    PageValue = parseInt(PageValue);
                }
                //添加loading
                $("#buyer-comment div.load-item-mask").height($("#buyer-comment ul.discusslist").height()).show();
                $("#buyer-comment img.load-item-img").css({
                    "top": "30px",
                    "display": "block"
                });
                GetComment(PageValue);
                //定位
                var t = $('#buyer-comment').offset().top;
                $(window).scrollTop(t);
                return false;
            });
        });
    }

    //评论用户链接及图像
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=UTF-8",
        url: "http://myshishang.maticsoft.com/service/ajaxservice.ashx",
        data: "contactIds=" + ContactIds + "&Method=GetShortMembersInfo",
        dataType: "jsonp",
        success: function(result) {
            if (result.Data != null) {
                var tempHtml = "";
                $(result.Data).each(function(i, n) {
                    $("#buyer-comment #comment-main-" + i).find("a").attr("href", "http://myshishang.maticsoft.com/share/" + n.ContactId + "-" + n.SpaceKey + "-1.htm");
                    $("#buyer-comment #comment-main-" + i).find("img").attr("src", n.MemberPhoto);
                    var tmpObj = $("#buyer-comment #comment-main-" + i + " .discusslist-item-con a");
                    tmpObj.text(n.NickName + tmpObj.text());
                });
            }
        }
    });

    //绑定评论投票
    $("#buyer-comment .discusslist-correlate").find(".btn").each(function(i, n) {
        var currObj = $(this);
        currObj.bind("click",
        function() {
            $("body").AjaxLogin({
                success: function() {
                    var CommentId = commentList[i].CommentId;
                    var CommentContactId = commentList[i].ContactId;
                    maticsoftment.SetUseful(CommentId, CommentContactId,
                    function() {
                        currObj.parent().find("div").remove();
                        if (maticsoftment.ResultStatus == 0) {
                            currObj.after("<div class=\"tips-succeed\">感谢投票</div>");
                            currObj.find("span").text("对我有用(" + parseInt(commentList[i].FavoriteCount + 1) + ")");
                        } else if (maticsoftment.ResultStatus == 1) {
                            currObj.after("<div class=\"tips-failed\">不能给自己投票</div>");
                        } else if (maticsoftment.ResultStatus == 2) {
                            currObj.after("<div class=\"tips-failed\">已投过票</div>");
                        } else if (maticsoftment.ResultStatus == 3) {
                            currObj.after("<div class=\"tips-failed\">请登录后投票</div>");
                        } else if (maticsoftment.ResultStatus == -1) {
                            currObj.after("<div class=\"tips-failed\">投票失败</div>");
                        }
                    });
                },
                content: "请登录"
            });
            return false;
        });
    });

};

function ShowUserPoint() {
    if (m18.Points.CurUserInfos != null && m18.Points.CurUserInfos.UserPoints != null && m18.Points.CurUserInfos.IsLogin) {
        setTimeout(function() {
            $("#Points_pop_wait").remove();

            var myDiv = $("<div id=\"Points_pop\" style=\"display:none;\" class=\"detail-pop\"><div class=\"title\">积分详情</div><p>您的当前积分为：" + m18.Points.CurUserInfos.UserPoints + "</p><div class=\"close-favlist\"><a href=\"#\" name=\"fClose\">关闭</a></div></div>");
            $("body").append(myDiv);
            $("#Points_pop").fadeIn("slow");

            var timeOut = setTimeout(function() {
                $("#Points_pop").fadeOut("slow",
                function() {
                    $(this).remove();
                });
            },
            4000);
            myDiv.find("a[name='fClose']").bind("click",
            function() {
                clearTimeout(timeOut);
                $("#Points_pop").remove();
                return false;
            });
        },
        1500);
    }
}

/// <summary>
/// 加载单品页左侧模块
/// </summary>
function BindStyleDetailLeft() {
    var classId3 = styleEntityListJson[0].ClassId3;
    var classId1 = classId3.substring(0, 2);
    var classId2 = classId3.substring(0, 4);

    //1.绑定订阅品牌的按钮事件
    $('.subscribeA').click(function() {
        if ($('#subscribe').length == 0) {
            var isrc = $(this).attr('href');
            $('<div id="subMask" style="background:#000;position:absolute;top:0;left:0;z-index:90;"></div>').appendTo('body').css({
                opacity: 0.5
            }).width($(document).width()).height($(document).height());
            $('<iframe id="subscribe" frameborder="0" style="width:550px;height:400px;position:absolute;top:50%;left:50%;margin-left:-275px;z-index:100;"></iframe>').attr('src', isrc).appendTo('body').css({
                marginTop: $(window).scrollTop() - 200
            });
        }
        return false;
    });
    //2.Last View
    ProcessLastView();
    //3.热门评论
    $.ajax({
        url: "http://comm.maticsoft.com/comment/hotcomment.htm?from=list&size=8&class=" + classId1,
        dataType: "jsonp",
        success: function(data) {

            if (data.length > 0) {
                var itemsHtml = "";
                $.each(data,
                function(i, n) {
                    var styleLink = "http://product.maticsoft.com/p-" + n.styleid + ".htm#rel=DPY_RPSP";
                    itemsHtml += "<li class=\"item-s\">" + "<div class=\"item-s-hd\">" + "		<a target=\"_blank\" title=\"" + unescape(n.name) + "\" href=\"" + styleLink + "\" class=\"item-s-title\">" + unescape(n.name) + "</a>" + "		<p class=\"item-s-price\"><span>" + unescape(n.price).replace(/￥/, '') + "</span></p>" + "	</div>" + "	<a target=\"_blank\" title=\"" + unescape(n.name) + "\" href=\"" + styleLink + "\" class=\"item-s-pic\"><img height=\"65\" width=\"50\" alt=\"\" src=\"" + n.image + "\"></a>" + "	<div class=\"item-s-txt\">" + "		<p class=\"item-s-name\"><a target=\"_blank\" href=\"http://myshishang.maticsoft.com/share/" + n.contactId + "-" + n.hmac + "-1.htm\">" + unescape(n.member) + "</a></p>" + "		<p class=\"item-s-comm\"><a target=\"_blank\" title=\"" + unescape(n.content) + "\" href=\"" + styleLink + "\">" + unescape(n.content) + "</a></p>" + "	</div>" + "</li>";

                });
                $("#areaHotComment").find("ul").append(itemsHtml) $("#areaHotComment").show();
            }
        },
        error: function() {}
    });

}

function BuyHistory() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "http://ProductUtil.maticsoft.com/BuyHistory.aspx",
        data: "StyleId=" + styleEntityListJson[0].StyleId,
        dataType: 'jsonp',
        success: function(result) {
            $("#buy-annal .readyload").remove();
            if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {
                var itemsHtml = "";
                var isEven = false;
                for (var i = 0; i < result.ReturnValue.length; i++) {
                    var curHistory = result.ReturnValue[i];
                    itemsHtml += "<tr class=\"" + (isEven ? "alt": "") + "\"><td>" + curHistory.BuyerNameDisplay + "</td><td>" + curHistory.MembershipName + "</td><td>" + curHistory.OrderCount + "</td><td></td><td class=\"c9 tr\">" + curHistory.OrderDate + "</td></tr>";
                    isEven = !isEven;
                }
                $("#buy-annal").find("tbody").append(itemsHtml);
                $("#buy-annal").find("table").show();
            } else {
                $("#buy-annal .bd").append("<div class='mt10 cf'><p class='no-content'>暂时没有此商品的购买记录</p></div>");
            }
        },
        error: function(result, status) {
            $("#buy-annal .readyload").remove();
            $("#buy-annal .bd").append("<div class='mt10 cf'><p class='no-content'>暂时没有此商品的购买记录</p></div>");
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
            url: "http://ProductUtil.maticsoft.com/LastView.aspx",
            data: "StyleIds=" + ids,
            dataType: 'jsonp',
            success: function(result) {
                var itemsHtml = "";
                if (result && result.IsSuccess && result.ReturnValue && result.ReturnValue.length > 0) {

                    for (var i = 0; i < result.ReturnValue.length; i++) {
                        var curStyle = result.ReturnValue[i];

                        var link = "http://product.maticsoft.com/" + curStyle.StyleType + "-" + curStyle.StyleId + ".htm#rel=DPY_ZJLL";

                        itemsHtml += "<li class=\"item-s\">" + "<a target=\"_blank\" title=\"" + curStyle.ChineseName + "\" href=\"" + link + "\" class=\"item-s-pic\"><img height=\"65\" width=\"50\" alt=\"" + curStyle.ChineseName + "\" src=\"" + curStyle.LargeMapImageUrl + "\"></a>" + "<div class=\"item-s-txt\">" + "<a target=\"_blank\" title=\"" + curStyle.ChineseName + "\" href=\"" + link + "\" class=\"item-s-title\">" + curStyle.ChineseName + "</a>" + "<p class=\"item-s-brand\">" + curStyle.TradeMarkName + "</p>" + "<p class=\"item-s-price\"><span>" + curStyle.WebSalePriceDisplay + "</span>" + (curStyle.GPPrice > 0 ? ("<em class=\"item-s-point\">+" + curStyle.GPPriceDisplay + "积分</em>") : "") + "</p>" + "</div>" + "</li>";
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

    //2.写入cookie
    var styleId = styleEntityListJson[0].StyleId;
    //当前浏览的商品信息
    var latestViewedItem = styleId;

    //判断Cookie中是否存在信息
    if (typeof($.cookie("LastView")) != "undefined" && $.cookie("LastView") != null && viewedItemArray.length > 0 && viewedItemArray[0]) {
        //标识是否添加当前的商品信息
        var canAddFlag = true;
        if (!styleEntityListJson[0].SaleOnWebFlag) {
            canAddFlag = false;
        }
        //判断是否需要添加商品信息
        for (var i in viewedItemArray) {
            var viewItems = viewedItemArray[i].split("^");
            //若当前商品编号已存在与cookie中，则不添加
            if (styleId == viewItems[0]) {
                canAddFlag = false;
            }
        }

        //获取老cookie字符串
        var oldLatestViewedItems = $.cookie("LastView");
        //若需要添加cookie，则将当前的cookie信息与老的cookie合并
        if (canAddFlag) {
            //当存有5条LastViewedItem信息时，移除最后一条
            if (viewedItemArray.length >= 5) {
                viewedItemArray.pop();
            }

            //构建移掉末尾元素的cookie
            var newLatestViewedItems = "";
            for (var i in viewedItemArray) {
                //最后一个元素末尾不加分隔符$
                if (i == viewedItemArray.length - 1) {
                    newLatestViewedItems += viewedItemArray[i];
                } else {
                    newLatestViewedItems += viewedItemArray[i] + "$";
                }
            }
            $.cookie('LastView', latestViewedItem + "$" + newLatestViewedItems, {
                expires: 365 * 10,
                path: "/",
                domain: "maticsoft",
                secure: false
            });
        } else {
            //不需要添加cookie，则写入cookie仍是老cookie
            $.cookie('LastView', oldLatestViewedItems, {
                expires: 365 * 10,
                path: "/",
                domain: "maticsoft",
                secure: false
            });
        }
    } else {
        $.cookie('LastView', latestViewedItem, {
            expires: 365 * 10,
            path: "/",
            domain: "maticsoft",
            secure: false
        });
    }
};

/// <summary>
/// 为页面上的元素绑定事件
/// </summary>
function BindEvent() {

    //10.选择省市区域的鼠标事件
    //3、为body绑定按esc时的事件
    $("body").bind("keypress",
    function(event) {
        if (event.keyCode == 27) {
            CloseSKUDiv();
        }
    });

    //4、为页面中的人气区域 放入购物车 添加事件
    $(".sku-PopSKU").bind("click",
    function() {
        PopSKUDiv();
        return false;
    });

    //5、购买记录事件
    $(".alsoSaled").bind("click",
    function() {
        $("#id-buy-annal").trigger("click");
        window.location.hash = "#tab";
    });
    //6.播放视频
    if ($("#video").length > 0) {
        var vSrc = 'videoURL=' + $(".video_start").attr("videoUrl");
        var s1 = new SWFObject("http://product.maticsoft.com/js/lib/jcplayer.swf", "play", "300", "390", "7", "#FFFFFF");
        s1.addParam("allowfullscreen", "true");
        s1.addParam("allowscriptaccess", "always");
        s1.addParam("wmode", "opaque");
        s1.addParam("scale", "noScale");
        s1.addParam("menu", "false") s1.addParam("flashVars", vSrc);
        s1.write("video");

        $(".video_start").click(OpenVideo);
        $(".video_ing").click(CloseVideo);

        $(".thumbbox").find(".thumbpic").find("a").click(CloseVideo);
        $("#iteminfo").find(".sku-color-select").find("a").click(CloseVideo);
        $("#iteminfo").find(".sku-size-select").find("a").click(CloseVideo);

    }
    //7.左侧图片滚动
    new Carousel('#thumb_container', 5, {
        auto: false,
        num: 1,
        vertical: true
    });

    //8.购物车添加，减去产品数量

    //	var chooseCount = obj.find("input:first").val();
    //	if (isNaN(chooseCount)) {
    //		this.ShowForm({ msg: "您输入的件数不正确，请重新输入！" });
    //		return false;
    //	}
    $("#plus").click(function() {
        var bcount = $("#productCount").val();
        var newCount;
        if (isNaN(bcount)) {
            newCount = 1;
        } else {
            bcount = parseInt(bcount);
            newCount = bcount + 1;
        }
        $("#productCount").val(newCount);
        return false;
    }) $("#subtract").click(function() {
        var bcount = $("#productCount").val();
        var newCount;
        if (isNaN(bcount)) {
            newCount = 1;
        } else {
            bcount = parseInt(bcount);
            newCount = bcount > 1 ? (bcount - 1) : 1;
        }
        $("#productCount").val(newCount);
        return false;
    })

    //搭配、套组切换
    new Tab('.correlativeStyle', {
        event: 'click'
    });
    //特惠组合TAB
    $(".combination_nav li").each(function(i) {
        $(this).mouseover(function() {
            $(this).siblings().removeClass("cur");
            $(this).addClass("cur");
            $("#combinationTab .combination_panel").hide();
            $("#combinationTab .combination_panel").eq(i).show();
        })
    })

    var isShowTip = !(typeof(window.m18) != "undefined" && typeof(window.m18.IPAssistant) != "undefined" && window.m18.IPAssistant.AreaInfos.SelectionType == 2);
    if (isShowTip) {
        $(".tip-t").show();
    }

    $(".tips-cls").click(function() {
        if (typeof(window.m18) != "undefined" && typeof(window.m18.IPAssistant) != "undefined") {
            window.m18.IPAssistant.SetInfos($(".selecetd-province").attr("provinceId"), "");
        }
        $(".tip-t").hide();
    });
    //点击评论数时,移动到评论Tab并显示
    $("#id-goto-buyercomment").click(function() {
        var $idbuyercomment = $("#id-buyer-comment");
        $(window).scrollTop($idbuyercomment.offset().top);
        $idbuyercomment.trigger("click");
        return false;
    })
    //当滚动到相应位置，或直接点击触发显示时，才加载相应内容
    LazyLoadEvent("buyer-comment", "comment",
    function() {
        GetComment(1);
    });
    LazyLoadEvent("buyer-leaveword", "leaveword", GetLeaveword);
    LazyLoadEvent("buy-annal", "annal", BuyHistory);
    function LazyLoadEvent(id, name, onTrigger) {
        $("#" + id).attr("loaded", false);
        //当滚动条移动到时，显示
        $(window).bind("scroll." + name + "LazyLoad",
        function() {
            var $window = $(window);
            if ($("#" + id).attr("loaded") == true) {
                $("#id-" + id).unbind("click." + name + "LazyLoad");
                $window.unbind("scroll." + name + "LazyLoad");
                return;
            }
            //获取窗体的高度
            var scrolltop = $window.scrollTop();
            var windowHeight = $window.height();
            var currentObj = $("#" + id);
            var _scrollTop = currentObj.offset().top - windowHeight;
            if ($.browser.safari) {
                _scrollTop = _scrollTop - scrolltop;
            }
            //当滚动条到达显示位置，并且该内容是显示的
            if (parseInt(scrolltop) >= parseInt(_scrollTop) && currentObj.css("display") != "none") {
                onTrigger();
                $("#id-" + id).unbind("click." + name + "LazyLoad");
                $window.unbind("scroll." + name + "LazyLoad");
                $("#" + id).attr("loaded", true);
            }
        });
        //点击时，显示
        $("#id-" + id).bind("click." + name + "LazyLoad",
        function() {
            if ($("#" + id).attr("loaded") == true) {
                $(window).unbind("scroll." + name + "LazyLoad");
                $("#id-" + id).unbind("click." + name + "LazyLoad");
                return;
            }
            onTrigger();
            $("#id-" + id).unbind("click." + name + "LazyLoad");
            $(window).unbind("scroll." + name + "LazyLoad");
            $("#" + id).attr("loaded", true);
        });
    }
}

function OpenVideo() {
    $("div.bigpic").addClass("relative");
    $("div.video").show();
    $(".video_start").removeClass("inblock").addClass("none");
    $(".video_ing").removeClass("none").addClass("inblock");
    $("div.J_zoom_focus").remove();
    $("#J_zoom").css("visibility", "hidden");
}

function CloseVideo() {
    $("div.bigpic").removeClass("relative");
    $("div.video").hide();

    if (! ($(".video_ing").hasClass("none") && $(".video_start").hasClass("none"))) {
        $(".video_ing").removeClass("inblock").addClass("none");
        $(".video_start").removeClass("none").addClass("inblock");
    }

    $("#J_zoom").css("visibility", "visible");
}

/// <summary>
/// 这个方法用来控制页面tab的切换
/// </summary>
function BindSwitchTab() {
    $(".pInfoTab a").click(function() {
        var type = $(this).attr("id");

        $(".pInfoTab a[class=cur]").attr("class", "");
        $(this).attr("class", "cur");

        switch (type) {
            //商品信息                                                                                             
        case "id-goods-info":
            $("#goods-info").show();
            $("#buy-annal").show();
            $("#buyer-comment").show();
            $("#buyer-leaveword").show();
            $("#buy-guarantee").show();
            $("#measure").hide();
            break;
            //购买记录                                                                                              
        case "id-buy-annal":
            $("#goods-info").hide();
            $("#buy-annal").show();
            $("#buyer-comment").hide();
            $("#buyer-leaveword").hide();
            $("#buy-guarantee").hide();
            $("#measure").hide();
            break;
            //买家评论                                                                                                
        case "id-buyer-comment":
            $("#goods-info").hide();
            $("#buy-annal").hide();
            $("#buyer-comment").show();
            $("#buyer-leaveword").hide();
            $("#buy-guarantee").hide();
            $("#measure").hide();
            break;
            //买家留言                                                                                                
        case "id-buyer-leaveword":
            $("#goods-info").hide();
            $("#buy-annal").hide();
            $("#buyer-comment").hide();
            $("#buyer-leaveword").show();
            $("#buy-guarantee").hide();
            $("#measure").hide();
            break;
            //myshishang尺码表                                                                       
        case "id-measure":
            $("#goods-info").hide();
            $("#buy-annal").hide();
            $("#buyer-comment").hide();
            $("#buyer-leaveword").hide();
            $("#buy-guarantee").hide();
            $("#measure").show();
            break;
        case "id-buy-guarantee":
            $("#goods-info").hide();
            $("#buy-annal").hide();
            $("#buyer-comment").hide();
            $("#buyer-leaveword").hide();
            $("#measure").hide();
            $("#buy-guarantee").show();
            break;
        default:
            break;
        }

        //去掉焦点虚线
        $(this).blur();
        return false;
    });
};

/// <summary>
/// 为LargMap图片加上mousemove事件，并去掉Click的链接功能
/// </summary>
function BindPicEvent() {
    $(".thumbpic li a").bind("mousemove",
    function() {
        //选中标识
        $(".thumbpic li a[class=cur]").attr("class", "");
        $(this).attr("class", "cur");

        var newPic = $(this).attr("href");
        $(".bigpic img").eq(0).attr("src", newPic);
    });

};

function SpecialClassLogic() {
    //获取当前单品页的各级ClassId
    var classId3 = styleEntityListJson[0].ClassId3;
    var classId1 = classId3.substring(0, 2);
    var classId2 = classId3.substring(0, 4);

    var parentObj = $("div.choicebox");

    //1、 当除指定classId外，如果二维显示是“均码”，则不显示维度2，并已选择中不显示“均码”(详见bug 691)
    if (parentObj.find(".sku-size-select li").size() == 1) {

        if ((classId3 != "N30101" && classId3 != "N30102" && classId3 != "N30103" && classId3 != "N30104" && classId3 != "N20201" && classId3 != "N20202" && classId3 != "N20203" && classId3 != "N20205" && classId2 != "N703" && classId2 != "N704")) {

} else {
            //1、隐藏维度2区域
            parentObj.find(".sku-size-title").hide();
            parentObj.find(".sku-size-select").hide();
            //2、隐藏已选择中的尺寸
            parentObj.find("span#sku-select-size").hide();
            //3、未当前维度1、维度2的所有按钮按钮绑定click事件
            parentObj.find(".sku-color-select li").bind("click",
            function() {
                parentObj.find("span#sku-select-size").hide();
            });
            parentObj.find(".sku-size-select li").bind("click",
            function() {
                parentObj.find("span#sku-select-size").hide();
            });
        }
    }
};

//用来表示skuDiv是否显示
var skuDivShowFlag = false;

///	<summary>
///	在界面中间弹出维度选择层，并添加遮罩层
///	<summary>
function PopSKUDiv() {
    //弹出层
    var width = $("#skuDiv").width();
    var height = $("#skuDiv").height();

    document.getElementById("skuDiv").style.position = 'absolute';
    document.getElementById("skuDiv").style.display = "block";
    document.getElementById("skuDiv").style.left = ($(window).width() - width) / 2 + $(window).scrollLeft() + "px";
    document.getElementById("skuDiv").style.top = ($(window).height() - height) / 2 + $(window).scrollTop() + "px";

    document.getElementById("skuDiv").style.zIndex = 1000;
    skuDivShowFlag = true;
    MaskSKUDiv();
    $(window).resize(MaskSKUDiv);
    return false;
}

function MaskSKUDiv() {
    if (skuDivShowFlag) {
        //mask遮罩层
        if ($("#mask").size() == 0) {
            $('<div id="mask"></div>').appendTo('body').css({
                opacity: 0.5,
                width: $(document).width(),
                height: $(document).height(),
                top: 0,
                left: 0,
                position: 'absolute',
                background: '#000',
                'z-index': 20
            });
        } else {
            $('#mask').css({
                opacity: 0.5,
                width: $(document).width(),
                height: $(document).height(),
                top: 0,
                left: 0,
                position: 'absolute',
                background: '#000',
                'z-index': 20
            });
        }
    }
}
///	<summary>
///	关闭维度选择层和遮罩层
///	<summary>
function CloseSKUDiv() {

    skuDivShowFlag = false;
    if (null != document.getElementById("skuDiv")) {
        document.getElementById("skuDiv").style.display = "none";
    }

    if (null != document.getElementById("mask")) {
        document.body.removeChild(document.getElementById("mask"));
    }

    void(0);
};

///	<summary>
///	StyleDetail页面商品描述中预期到货时间的逻辑
/// <param name="styleSKUItems"> 当前单品页所有维度信息，已合并库存信息 </param>
/// <param name="styleEntity"> 当前Style的实体对象 </param>
///	<summary>
function StyleDescPOLogic(styleSKUItems, styleEntity) {
    //此逻辑仅在商品无库存压力时有效
    if (typeof(styleEntity) == "undefined" || styleEntity == null || styleEntity.StockAllocateMode == 1) {
        return;
    }

    if (styleSKUItems.length == 1) {
        if (!styleSKUItems[0].IsStock) {
            var dateString = Date.Format(Date.ToDate(styleSKUItems[0].ExpectReceiptDate), "MM月dd日");
            var poString = "";
            if (String.IsNullOrEmpty(dateString)) {
                poString = "积极补货中，预计到货时间超过4周。";
            } else {
                if (styleSKUItems[0].POType == 1) {
                    poString = "本商品积极补货中，预计" + Date.Format(Date.ToDate(styleSKUItems[0].ExpectReceiptDate), "MM月dd日") + "有货。";
                } else {
                    poString = "本商品积极补货中，预计到货时间超过4周。";
                }
            }

            //向商品描述处添加文字
            if ($("div.ico-warn").size() == 0) {
                $("#styleBrief").after("<div class='ico-warn'></div>");
            }
            $("div.ico-warn").append("<p>" + poString + "</p>");
        }
    } else {
        //将所有sku按维度1分组
        var sku1IdArray = new Array();

        //循环每种颜色
        for (var i in styleSKUItems) {
            if (!IsInArray(styleSKUItems[i].SKUDimentionId1, sku1IdArray)) {
                sku1IdArray.push(styleSKUItems[i].SKUDimentionId1);
                //获取此sku1的所有维度项
                var skuItems = GetSKUItemListBySkuId1(styleSKUItems[i].SKUDimentionId1, styleSKUItems);
                //标识此种颜色是否需要显示po信息
                var showFlag = false;
                var poString = "";
                //记录最早到货日期
                var minExpectReceiptDate = null;
                var poType = new Number();
                for (var j in skuItems) {
                    if (!skuItems[j].IsStock) {
                        showFlag = true;
                        if (minExpectReceiptDate == null) {
                            minExpectReceiptDate = Date.ToDate(skuItems[j].ExpectReceiptDate);
                            poType = skuItems[j].POType;
                        } else {
                            if (Date.ToDate(skuItems[j].ExpectReceiptDate != null)) {
                                minExpectReceiptDate = minExpectReceiptDate <= Date.ToDate(skuItems[j].ExpectReceiptDate) ? minExpectReceiptDate: Date.ToDate(skuItems[j].ExpectReceiptDate);
                                poType = skuItems[j].POType;
                            }
                        }

                        poString += "<span class='color-red'>";

                        //如果是没有颜色或者颜色是“单色”，文字中去掉“颜色”和“空格”。
                        if (skuItems[j].SKUDimentionName1 != null && skuItems[j].SKUDimentionName1 != '单色') {
                            poString += skuItems[j].SKUDimentionName1;
                        }

                        //如果是“均码”或者没有尺码的商品就不显示尺码和这个“号”字。
                        if (skuItems[j].SKUDimentionName2 != null && skuItems[j].SKUDimentionName2 != '均码') {
                            if (skuItems[j].SKUDimentionName1 != null && skuItems[j].SKUDimentionName1 != '单色') {
                                poString += " ";
                            }
                            poString += skuItems[j].SKUDimentionName2 + "号";
                        }

                        poString += "</span>";

                        var expectReceiptDate = skuItems[j].ExpectReceiptDate;

                        if (String.IsNullOrEmpty(expectReceiptDate)) {
                            poString += "预计到货时间超过4周";
                        } else {
                            if (poType == 1) {
                                poString += "预计" + Date.Format(minExpectReceiptDate, "MM月dd日") + "有货";
                            } else {
                                poString += "预计到货时间超过4周";
                            }
                        }

                        ! skuItems[j].IsStock

                        if (j != skuItems.length - 1) {
                            poString += "，";
                        } else {
                            poString += "。";
                        }
                    }
                }

                //判断最后一个符号
                if (poString[poString.length - 1] != "。") {
                    poString = poString.substring(0, poString.length - 1) + "。";
                }

                if (showFlag) {
                    //向商品描述处添加文字
                    if ($("div.ico-warn").size() == 0) {
                        $("#styleBrief").after("<div class='ico-warn'><p>以下商品正在积极补货中：</p></div>");
                    }
                    $("div.ico-warn").append("<p>" + poString + "</p>");
                }
            }
        }
    }
};

/****************************zipcode begin****************************/

//ArrivingNotify.currDialog = SetDialog(ArrivingNotify.currDialog, ArrivingNotify.d1);
//ArrivingNotify.currDialog.open();
var RegionChecking = {

    currDialog: null,
    d1: {
        title: "查询送货范围",
        body: "<div class=\"pb20\">" + "<div class=\"popCon pt20\">" + "<div>" + "<label class=\"f14 mr5\" for=\"queryPostId1\">" + "邮编：</label><input type=\"text\" id=\"queryPostId1\" class=\"queryText\" value=\"\" /><a href=\"javascript:void(0);\"" + "class=\"btn f14 fb ml10 btn-important\" onclick=\"RegionChecking.CheckZipCode(1);\"><span>查<em class=\"s1em\"></em>询</span></a></div>" + "<div class=\"lower f14 mt5 ml50\">请输入收货地址的邮编</div>" + "</div>" + "</div>",
        footer: "",
        width: 400
    },
    d2: {
        title: "查询送货范围",
        body: "<div class=\"pb20\">" + "<div class=\"popCon iconSucceed pt20\">" + "<h4 class=\"f14 mb10 fb\">您所在的地区在此商品的送货范围内</h4>" + "<div class=\"lower f14 mt5 h\" id=\"alertInfo2\">收货地址所在的地区：</div>" + "</div>" + "</div>",
        footer: "",
        width: 400
    },
    d3: {
        title: "查询送货范围",
        body: "<div class=\"pb20\">" + "<div class=\"popCon iconError pt20\">" + "<h4 class=\"f14 mb5 fb\" id=\"regionMess3\">找不到您输入的邮编</h4>" + "<h4 class=\"f14 mb10\">输入其它邮编试试</h4>" + "<div>" + "<label class=\"f14 mr5\" for=\"queryPostId3\">邮编：</label>" + "<input type=\"text\" id=\"queryPostId3\" class=\"queryText\" value=\"\" />" + "<a href=\"javascript:void(0);\" class=\"btn f14 fb ml10 btn-important\" onclick=\"RegionChecking.CheckZipCode(3);\"><span>重新查询</span></a>" + "</div>" + "</div>" + "</div>",
        footer: "",
        width: 400
    },
    d4: {
        title: "查询送货范围",
        body: "<div class=\"pb20\">" + "<div class=\"popCon iconError pt20\">" + "<h4 class=\"f14 mb5 fb\" id=\"regionMess4\">抱歉，该地区不在此商品的送货范围内</h4>" + "<h4 class=\"f14 mb10\">输入其它邮编试试</h4>" + "<div>" + "<label class=\"f14 mr5\" for=\"queryPostId4\">邮编：</label>" + "<input type=\"text\" id=\"queryPostId4\" class=\"queryText\" value=\"\" />" + "<a href=\"javascript:void(0);\" class=\"btn f14 fb ml10 btn-important\" onclick=\"RegionChecking.CheckZipCode(4)\"><span>重新查询</span></a>" + "</div>" + "<div class=\"lower f14 mt5 h\" id=\"alertInfo4\">收货地址所在的地区：</div>" + "</div>" + "</div>",
        footer: "",
        width: 400
    },
    CheckZipCodeText: function(ob) {
        var res = "";
        if (ob == null || ob == "") {
            res = '邮编不能为空';
        } else {
            if (ob.length > 6) {
                res = "您输入的邮编格式不正确";
            } else {
                for (var i = 0; i < ob.length; i++) {
                    var ct = ob.charAt(i);
                    if (! (ct >= '0' && ct <= '9')) {
                        res = "您输入的邮编格式不正确";

                    }
                }
            }

        }

        return res;
    },
    CheckZipCode: function(obs) {
        var zipcode = '';
        switch (obs) {
        case 1:
            zipcode = $("#queryPostId1").val();
            break;
            //case 2: zipcode = $("#queryPostId2").val(); break;                                                                            
        case 3:
            zipcode = $("#queryPostId3").val();
            break;
        case 4:
            zipcode = $("#queryPostId4").val();
            break;
        }
        var resMes = RegionChecking.CheckZipCodeText(zipcode);
        if (resMes == "") {

            var whids = $("#queryArealink").attr("whIds");

            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "http://ProductUtil.maticsoft.com/Region.aspx",
                data: "zipCode=" + zipcode + "&whids=" + whids,
                dataType: 'jsonp',
                success: function(result) {

                    if (result && result.IsSuccess && result.ReturnValue != null && result.ReturnValue.Address != null && result.ReturnValue.Address != "") {
                        if (result.ReturnValue.IsDelivery == 1) {
                            if (RegionChecking.currDialog) {
                                RegionChecking.currDialog.close();
                            }
                            RegionChecking.currDialog = SetDialog(RegionChecking.currDialog, RegionChecking.d2);
                            RegionChecking.currDialog.open();
                            RegionChecking.currDialog.center();
                            $("#alertInfo2").html("收货地址所在的地区：" + result.ReturnValue.Address);
                        } else {

                            if (RegionChecking.currDialog) {
                                RegionChecking.currDialog.close();
                            }
                            RegionChecking.currDialog = SetDialog(RegionChecking.currDialog, RegionChecking.d4);
                            RegionChecking.currDialog.open();
                            RegionChecking.currDialog.center();
                            $("#alertInfo4").html("收货地址所在的地区：" + result.ReturnValue.Address);
                        }

                    } else {

                        if (RegionChecking.currDialog) {
                            RegionChecking.currDialog.close();
                        }
                        RegionChecking.currDialog = SetDialog(RegionChecking.currDialog, RegionChecking.d3);
                        RegionChecking.currDialog.open();
                        RegionChecking.currDialog.center();
                    }
                },
                error: function(result, status) {}
            });
        } else {

            if (RegionChecking.currDialog) {
                RegionChecking.currDialog.close();
            }
            RegionChecking.currDialog = SetDialog(RegionChecking.currDialog, RegionChecking.d3);
            RegionChecking.currDialog.open();
            RegionChecking.currDialog.center();
            $("#regionMess3").html(resMes);

        }
    },

    PopZipCodeWindow: function() {

        if (RegionChecking.currDialog) {
            RegionChecking.currDialog.close();
        }
        RegionChecking.currDialog = SetDialog(RegionChecking.currDialog, RegionChecking.d1);
        RegionChecking.currDialog.open();
        RegionChecking.currDialog.center();
    }

}

/****************************zipcode end****************************/

function appendGoTop() {
    $('<a href="#" id="gotop">返回顶部</a>').appendTo('body').hide().click(function() {
        $(document).scrollTop(0);
        $(this).hide();
        return false;
    });
    var $gotop = $('#gotop');
    function backTopLeft() {
        var btLeft = $(window).width() / 2 + 493;
        if (btLeft <= 980) {
            $gotop.css({
                'left': 985
            });

            $(".btn-survey").css({
                'left': 985
            });
        } else {
            $gotop.css({
                'left': btLeft
            });
            $(".btn-survey").css({
                'left': btLeft
            });

        }
    }
    backTopLeft();
    $(window).resize(backTopLeft);
    $(window).scroll(function() {
        if ($(document).scrollTop() === 0) {
            $gotop.hide();
        } else {
            $gotop.show();
        }
        if ($.browser.msie && $.browser.version == 6.0 && $(document).scrollTop() !== 0) {
            $gotop.css({
                'opacity': 1
            });
            $(".btn-survey").css({
                'opacity': 1
            });
        }
    })
}

$(function() {
    var $brand = $('#iteminfo .brand').text();
    var $price = $('#iteminfo .price').text() $price = $price.substring(0, $price.length - 3);
    if ($price >= 299) {
        $('#spromotion li:eq(3)').show();
    }
    if ($('#iteminfo del').length == 0 && $brand == 'RAMPAGE') {
        $('#spromotion li:eq(0)').show();
        $('#spromotion li:eq(1)').show();
    }
});
function sizeLink() {
    var classid3 = styleEntityListJson[0].ClassId3;
    var classid1 = classid3.substring(0, 2);
    var classid2 = classid3.substring(0, 4);

    if ($('ul.sku-size-select a:first').eq(0).text() == '均码' || classid1 != 'N1') {} else {

        if ($('#iteminfo').find('.a2&.viewSize').size() == 0) {
            $('<a id="TLXYJSQ" class="a2 viewSize ml10" style="cursor:pointer;"><img alt="量体选衣" src="http://img.maticsoft.com/asset/m18/product/i/mb.gif" class="mr5">查看适合我的尺码</a>').appendTo('.sizeOptions').show();
            $('a.viewSize').bind('click', CalculateSize);
        }
    }

};

/********************Arriving Notify Begin************************/

function IsMobile(mobile) {

    if (mobile == null || mobile == "") {
        return false;
    }
    var regex = new RegExp("^1[358][0-9]{9}$");
    return regex.test(mobile);
}

function IsEmail(email) {

    if (email == null || email == "") {
        return false;
    }
    var regex = new RegExp(".+@.+");
    return regex.test(email);
}

var ArrivingNotify = {
    d1: {
        title: '到货通知',
        body: '<div class="pt10 pl30 pr30 pb20 f14">' + '<p class="mb10">亲：</p>' + '<p class="mb10">商品到货后，您希望如何通知您？(手机和Email可任填一项）</p>' + '<ul class="arrnotice">' + '<li class="clearfix">' + '<label class="arritem fl">手机：</label>' + '<input type="text" class="text" name="anMobile" id="anMobile">' + '</li>' + '<li class="clearfix">' + '<label class="arritem fl">Email：</label>' + '<input type="text" class="text" name="anEmail" id="anEmail">' + '</li>	' + '</ul>' + '<div class="palign mt10">' + '<p id="arrivingNotifyMsg" class="red f12"></p>' + '<p><a id="arrivingNotifySubmit" class="btn btn-important" href="#31"><span>确<em class="s1em"></em>定</span></a></p>' + '</div>' + '</div>',
        footer: '',
        width: 470
    },
    d2: {
        title: '到货通知订阅成功',
        body: '<div class="pt10">' + '<div class="pt30 pb20">' + '<p class="order-sucess orderw1 f14">您已成功订阅到货通知。</p>' + '<p class="mt20 tc"><a class="btn btn-important" id="arrivingNotifyConfirm" href="#31"><span>确<em class="s1em"></em>定</span></a></p>' + '</div>' + '<div class="order-warn tc">' + '<p>当您收到到货通知后，请第一时间来购买，货源紧张哟。<a class="a2" target="_blank" href="http://static.maticsoft.com/help/help_arrival.shtml">查看帮助&gt;&gt;</a></p>' + '</div>' + '</div>',
        footer: '',
        width: 470
    },
    currDialog: null,
    StyleId: "",
    ProductId: "",
    ItemId: "",
    Callback: function() {
        if (this.StyleId) {
            var thisObj = this;
            $.ajax({
                type: "get",
                contentType: "application/json",
                url: "http://ProductUtil.maticsoft.com/ArrivingNotify.aspx",
                data: "Operation=GetUserInfo",
                dataType: 'jsonp',
                success: function(data) {
                    if (data && data.IsSuccess && data.ReturnValue) {
                        var email = data.ReturnValue.Email;
                        if (email == null) {
                            email = "";
                        }
                        var mobile = data.ReturnValue.Mobile;
                        if (mobile == null) {
                            mobile = "";
                        }
                        ArrivingNotify.currDialog = SetDialog(ArrivingNotify.currDialog, ArrivingNotify.d1);
                        ArrivingNotify.currDialog.open();

                        $("#anMobile").blur(function() {
                            $("#arrivingNotifyMsg").html("");
                            var mobile = jQuery.trim($("#anMobile").val());

                            if (mobile != "" && !IsMobile(mobile)) {
                                $("#arrivingNotifyMsg").html("请输入正确的手机号。");
                                return false;
                            }
                            return false;
                        });

                        $("#anEmail").blur(function() {
                            $("#arrivingNotifyMsg").html("");
                            var email = jQuery.trim($("#anEmail").val());
                            if (email != "" && !IsEmail(email)) {
                                $("#arrivingNotifyMsg").html("请输入正确的邮件地址。");
                                return false;
                            }
                            return false;
                        }) $("#arrivingNotifySubmit").bind("click",
                        function() {
                            $("#arrivingNotifyMsg").html("");
                            var mobile = jQuery.trim($("#anMobile").val());
                            var email = jQuery.trim($("#anEmail").val());

                            if (mobile != "" && !IsMobile(mobile)) {
                                $("#arrivingNotifyMsg").html("请输入正确的手机号。");
                                return false;
                            }
                            if (email != "" && !IsEmail(email)) {
                                $("#arrivingNotifyMsg").html("请输入正确的邮件地址。");
                                return false;
                            }
                            if (mobile == "" && email == "") {
                                $("#arrivingNotifyMsg").html("手机和Email需任填一项。");
                                return false;
                            }

                            $.ajax({
                                url: "http://ProductUtil.maticsoft.com/ArrivingNotify.aspx",
                                data: "Operation=AddNotify&StyleId=" + ArrivingNotify.StyleId + "&ProductId=" + ArrivingNotify.ProductId + "&ItemId=" + ArrivingNotify.ItemId + "&mobile=" + mobile + "&email=" + email,
                                type: "get",
                                contentType: "application/json",
                                dataType: "jsonp",
                                success: function(res) {
                                    if (res && res.IsSuccess) {
                                        ArrivingNotify.currDialog.close();
                                        ArrivingNotify.currDialog = SetDialog(ArrivingNotify.currDialog, ArrivingNotify.d2);
                                        ArrivingNotify.currDialog.open();
                                        $("#arrivingNotifyConfirm").bind("click",
                                        function() {
                                            ArrivingNotify.currDialog.close();
                                            return false;
                                        });
                                    } else {
                                        alert("添加到货通知失败。")
                                    }
                                },
                                error: function() {
                                    ArrivingNotify.currDialog.close();
                                }
                            });
                            return false;
                        });
                        $("#anEmail").val(email);
                        $("#anMobile").val(mobile);

                    } else {
                        alert(data.ErrorMsg);
                    }
                },
                error: function() {}
            });

        }
    }
}

function InitDimention1ArrivingNotify() {
    var curUrl = location.href;
    if (curUrl != null && curUrl != "" && (curUrl.indexOf("?") >= 0 || curUrl.indexOf("#") >= 0)) {
        if (curUrl.indexOf("?") >= 0) curUrl = curUrl.substring(curUrl.indexOf("?") + 1);
        if (curUrl.indexOf("#") >= 0) curUrl = curUrl.substring(curUrl.indexOf("#") + 1);
        var splits = curUrl.split("&");
        if (splits != null && splits.length > 0) {
            for (var i = 0; i < splits.length; i++) {
                var currSplit = splits[i];
                if (currSplit != null && currSplit != "") {
                    if (currSplit.indexOf("#") >= 0) {
                        currSplit = currSplit.substring(0, currSplit.indexOf("#"));
                    }
                    if (currSplit.indexOf("?") >= 0) {
                        currSplit = currSplit.substring(currSplit.indexOf("?") + 1);
                    }
                    if (currSplit.indexOf("color=") >= 0) {
                        currSplit = currSplit.replace(/color=/, "");
                        autoSku1 = currSplit;
                    }
                    if (currSplit.indexOf("size=") >= 0) {

                        currSplit = currSplit.replace(/size=/, "");
                        autoSku2 = currSplit;
                    }
                }
            }
        }
    }

    $(".choicebox").each(function() {
        // 1、获取当前需要使用的对象
        var styleId = $(this).attr("styleId");
        var styleEntity = GetStyleEntity(styleId, styleEntityListJson);
        //此时的sku已包含库存信息
        var skuItemList = GetSKUItems(styleId, styleSKUItemsJson);

        // 2、绘制维度1
        var noStockSku1Array = new Array();
        for (var index in skuItemList) {
            //判断此维度1是否已输出过
            var flagDimention1 = false;
            $(this).find(".sku-color-select li").each(function() {
                var skuDimentionId1 = $(this).attr("skuId1");
                if (skuItemList[index].SKUDimentionId1 == skuDimentionId1) {
                    flagDimention1 = true;
                }
            });

            //判断此SKU1所对应的所有产品是否有库存(混入checkPO逻辑)
            var isStock = IsWarehouseBySKUDimention1(skuItemList[index].SKUDimentionId1, skuItemList, styleEntity);
            var mainStockCount = GetD1MainStockSkuCount(skuItemList[index].SKUDimentionId1, skuItemList, styleEntity);
            if (!skuItemList[index].POFlag && styleEntity.StockAllocateMode == 0) {
                isStock = true;
            }

            //如果未输出，则绘制维度1
            if (!flagDimention1) {

                PaintDimention1ArrivingNotify(skuItemList[index], $(this), isStock, mainStockCount);
                // 3、绑定单击事件
                BindDimention1ClickArrivingNotify(skuItemList[index], styleEntity.StockAllocateMode, skuItemList, styleEntity, $(this), $(this).find(".sku-color-select li[skuid1=" + skuItemList[index].SKUDimentionId1 + "]"));

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
        var a = "";
        if (typeof(window.m18) != "undefined" && typeof(window.m18.IPAssistant) != "undefined") {
            window.m18.IPAssistant.ServerPath = "http://productutil.maticsoft";
            window.m18.IPAssistant.GetAreaInfoByRequest(function() {
                a += "0";
                if (window.m18.IPAssistant.AreaInfos.AreaName && window.m18.IPAssistant.AreaInfos.AreaName != "") {
                    var tmpLi = $(".AstoreBox").find("a[provinceId='" + window.m18.IPAssistant.AreaInfos.AreaName + "']");
                    if (tmpLi.length > 0) {
                        $("p.selecetd-province").attr("displayName", tmpLi.html()).attr("provinceId", tmpLi.attr("provinceId"));
                    }
                }
                AutoClickSku1(autoSku1, styleEntity);
            });
        } else {
            AutoClickSku1(autoSku1, styleEntity);
        }

    });
};

function GetD1MainStockSkuCount(sKUDimentionId1, itemList, styleEntity) {
    var count = 0;
    for (var index in itemList) {
        var isMainStock = false;
        if (sKUDimentionId1 == itemList[index].SKUDimentionId1 && !itemList[index].IsRegionSell) {
            if (!itemList[index].POFlag && styleEntity.StockAllocateMode == 0) {
                isMainStock = true;
            }
            if (styleEntity.CheckPOFlag && styleEntity.StockAllocateMode == 0) {
                if (itemList[index].IsStock == true || !itemList[index].POFlag) {
                    isMainStock = true;
                }
            } else {
                if (itemList[index].IsStock == true) {
                    isMainStock = true;
                }
            }
        }
        if (isMainStock) {
            count++;
        }
    }
    return count;
};

function AutoClickSku1(autoSku1, styleEntity) {
    var parentDiv = $(".choicebox");
    if (autoSku1 != null && autoSku1 != "" && parentDiv.find(".sku-color-select li[skuId1=" + autoSku1 + "]").size() > 0) {
        parentDiv.find(".sku-color-select li[skuId1=" + autoSku1 + "]").trigger("click");
    } else {
        parentDiv.find(".sku-color-select li:nth-child(1)").trigger("click");
    }

    // 6、判断维度1的数量和纬度2的数量，若数量都为0，说明该商品不允许超买且没有库存，则隐藏购买区域，显示无库存提示信息
    if (parentDiv.find(".sku-color-select li").size() == 0 && parentDiv.find(".sku-size-select li").size() == 0) {
        parentDiv.html("");
        parentDiv.hide();
        var classId = styleEntity.ClassName2 == styleEntity.ClassName3 ? styleEntity.ClassId2: styleEntity.ClassId3;
        var link = "http://list.maticsoft.com/" + styleEntity.ClassId1 + "-" + classId + "-12-60-1-00-1-1-N-N-0-0.htm";
        parentDiv.after($("<div class=\"pInfoFrame pInfohover clearfix choicebox pInfoNogoods \"><div class=\"tc sallputTips\"><p class=\"mt20 mb15\">您查看的商品已售完，感谢您的关注。</p><a class=\"f14 fb a2\" href=\"" + link + "\">点击查看其它商品</a></div></div>"));
    } else {
        BindSku1EventArrivingNotify(parentDiv.attr("styleId"), parentDiv);
    }

}

function BindSku1EventArrivingNotify(styleId, parentObj) {
    parentObj.find(".sku-color-select a").bind("mouseover",
    function() {
        //更换大图
        var previousSrc = $(".bigpic img").eq(0).attr("src");
        $(".bigpic img").eq(0).attr("previousSrc", previousSrc);
        var currentSrc = $(this).find("img").eq(0).attr("largePicUrl");
        $(".bigpic img").eq(0).attr("src", currentSrc);
    });

    parentObj.find(".sku-color-select a").bind("mouseout",
    function() {
        //更换大图
        var previousSrc = $(".bigpic img").eq(0).attr("previousSrc");
        var currentSrc = $(".bigpic img").eq(0).attr("src");
        $(".bigpic img").eq(0).attr("src", previousSrc);
        $(".bigpic img").eq(0).attr("previousSrc", "");
    });
};

function PaintDimention1ArrivingNotify(skuItem, parentObj, isStock, mainStockCount) {
    var pos = 0;
    var max = parentObj.find(".sku-color-select li").length;
    parentObj.find(".sku-color-select li").each(function() {
        var tmpMainStockCount = parseInt($(this).attr("mainStockCount"));
        if (tmpMainStockCount > mainStockCount) {
            pos++;
        }
    });
    if (!isStock) {
        pos = max;
    }
    var stockText = isStock ? "1": "0";
    var linkClass = isStock || (styleEntityListJson[0].PreSaleFlag && styleEntityListJson[0].PreSaleFlag == true) ? "": "none";
    var currD1 = "<li mainStockCount='" + mainStockCount + "' isStock='" + stockText + "' skuId1='" + skuItem.SKUDimentionId1 + "'></li>";
    if (pos == 0) {
        parentObj.find(".sku-color-select").prepend(currD1);
    } else {
        parentObj.find(".sku-color-select li").eq(pos - 1).after(currD1);
    }

    var displayD1;
    if (skuItem.SKUDimentionName1 == null) {
        displayD1 = null;
    } else {
        displayD1 = skuItem.SKUDimentionName1.length > 3 ? skuItem.SKUDimentionName1.substring(0, 3) : skuItem.SKUDimentionName1;
    }
    if (displayD1 != null && displayD1.length == 2) {
        displayD1 = displayD1.substring(0, 1) + "<em class=\"s1em\"></em>" + displayD1.substring(1, 2);
    }

    //向li节点中添加<a>节点
    if (isStock || (styleEntityListJson[0].PreSaleFlag && styleEntityListJson[0].PreSaleFlag == true)) {
        parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").append("<a class=\"" + linkClass + "\" href='javascript:void(0);' title='" + skuItem.SKUDimentionName1 + "' alt='" + skuItem.SKUDimentionName1 + "'></a>");
        //向<a>节点中添加<img>节点
        parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "] a").append("<img width='20' height='20' class='vm mr5' src='" + skuItem.SKUColorImageFileName + "' largePicUrl='" + skuItem.DisplaySKUImageLarge + "' smallPicUrl='" + skuItem.DisplaySKUImageSmall + "' alt='" + skuItem.SKUDimentionName1 + "' />");
    } else {
        parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").append("<a class=\"" + linkClass + "\" href='javascript:void(0);' title='" + skuItem.SKUDimentionName1 + "色已售完' alt='" + skuItem.SKUDimentionName1 + "色已售完'></a>");
        //向<a>节点中添加<img>节点
        parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "] a").append("<img width='20' height='20' class='vm mr5' src='" + skuItem.SKUColorImageFileName + "' largePicUrl='" + skuItem.DisplaySKUImageLarge + "' smallPicUrl='" + skuItem.DisplaySKUImageSmall + "' alt='" + skuItem.SKUDimentionName1 + "色已售完" + "' />");
    }
    parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "] a").append("<span class=\"mr5 vm\">" + displayD1 + "</span></a>");
    //向li节点中添加<span>节点
    parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]").append("<span class=\"selected\">选中</span>");
};

function PaintDimention2ArrivingNotify(noneflag, skuItem, parentObj) {
    var isMainStock = noneflag && !skuItem.IsRegionSell;
    var isRegionSell = skuItem.IsRegionSell;
    var pos = 0;
    var max = parentObj.find(".sku-size-select li").length;
    if (!noneflag) {
        pos = max;
    } else {
        parentObj.find(".sku-size-select li").each(function() {
            var tmpIsMainStock = ($(this).attr("isMainStock") == "1");
            if (tmpIsMainStock) {
                pos++;
            } else if ($(this).attr("isStock") == "1" && isRegionSell) {
                pos++;
            }
        });
    }
    var stockText = noneflag ? "1": "0";
    //向ul节点中添加<li>节点
    var currD2 = "<li isMainStock='" + (isMainStock ? 1 : 0) + "' isStock='" + stockText + "' skuId2='" + skuItem.SKUDimentionId2 + "'></li>";
    parentObj.find(".sku-size-select").append(currD2);

    //向li节点中添加<a>节点
    if (noneflag || (styleEntityListJson[0].PreSaleFlag && styleEntityListJson[0].PreSaleFlag == true)) {
        parentObj.find(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<a href='javascript:void(0);' title='" + skuItem.SKUDimentionName2 + "' alt='" + skuItem.SKUDimentionName2 + "'>" + skuItem.SKUDimentionName2 + "</a>");
    } else {
        parentObj.find(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<a href='javascript:void(0);' class='none' title='尺码：" + skuItem.SKUDimentionName2 + " 已售完' alt='尺码：" + skuItem.SKUDimentionName2 + " 已售完'>" + skuItem.SKUDimentionName2 + "</a>");
    }
    //向li节点中添加<span>节点
    parentObj.find(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").append("<span  class=\"selected\">选中</span>");
    $("#iteminfo").find(".sku-size-select").find("a").click(CloseVideo);
};

/// <summary>
/// 绑定维度1的事件
/// </summary>
function BindDimention1ClickArrivingNotify(skuItem, stockAllocateMode, itemList, styleEntity, parentObj, objDom) {
    objDom.click(function() {

        //1、更改选中状态
        //更改维1当前li元素的选中状态
        parentObj.find(".sku-color-select li.cur").removeClass("cur");
        parentObj.find(".sku-color-select li.loseCur").removeClass("loseCur");

        var currLi = parentObj.find(".sku-color-select li[skuId1=" + skuItem.SKUDimentionId1 + "]");
        var toSetClass = currLi.find("a").hasClass("none") ? "loseCur": "cur";
        currLi.addClass(toSetClass);

        var imgSrc = currLi.find("img").eq(0).attr("largePicUrl");
        $(".bigpic img").eq(0).attr("src", imgSrc);
        $(".bigpic img").eq(0).attr("previousSrc", imgSrc);

        // 2、更改维度1所关联的界面文字
        var currStyleId = styleEntity.StyleId;
        if (currStyleId.indexOf("-") >= 0) {
            var index = currStyleId.indexOf("-");
            currStyleId = styleEntity.StyleId.substring(0, index);
        }
        parentObj.find(".sku-color-title .hl2").text(currStyleId + skuItem.SKUDimentionId1 + "-" + skuItem.SKUDimentionName1);

        //3、按照逻辑绘制维度2的Html
        //获得当前skuDimentionId1所关联的所有SKU Item
        var skuItemList = GetSKUItemListBySkuId1(skuItem.SKUDimentionId1, itemList);
        //绘制维度2，需要先清空目前维度2的内容
        parentObj.find(".sku-size-select").html("");
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
            PaintDimention2ArrivingNotify(noneflag, skuItemList[index], parentObj);
            //4、为维度2绑定事件
            BindDimention2ClickArrivingNotify(skuItemList[index], styleEntity, parentObj, parentObj.find(".sku-size-select li[skuid2=" + skuItemList[index].SKUDimentionId2 + "]"), noneflag);
        }

        // 5、为维度2排序
        //parentObj.find(".sku-size-select a[class=none]").parent("li").insertAfter(parentObj.find(".sku-size-select a[class=]").parent("li:last"));
        // 6、触发选中元素的click事件，
        //如果url中带有sku2的参数,这直接定位到它
        var toClickDefult = true;
        if (isFirstEnterAN && autoSku2 != null && autoSku2 != "" && parentObj.find(".sku-size-select li[skuId2=" + autoSku2 + "]").size() > 0) {
            parentObj.find(".sku-size-select li[skuId2=" + autoSku2 + "]").trigger("click");
            toClickDefult = false;
        } else if (IsContainKey(styleEntity.StyleId, currSKUId2ArrivingNotifyArray)) {

            //如果上次有选择
            var currSKUId2 = GetItemByKey(styleEntity.StyleId, currSKUId2ArrivingNotifyArray).value;

            //上次选择的还在且可以被选中就触发它
            if (parentObj.find(".sku-size-select li[skuId2=" + currSKUId2 + "]").size() > 0) {
                parentObj.find(".sku-size-select li[skuId2=" + currSKUId2 + "]").trigger("click");
                toClickDefult = false;
            }

        }
        if (toClickDefult) {
            var targetList = parentObj.find(".sku-size-select li[ismainstock=1]");
            var target;
            if (targetList.length == 0) {
                targetList = parentObj.find(".sku-size-select li[isstock=1]");
                if (targetList.length == 0) {
                    targetList = parentObj.find(".sku-size-select li");
                }
            }

            targetList.find("a").eq(0).trigger("click");
        }
        isFirstEnterAN = false;
        // 7、如果维度1仅有1个，维度2也仅有1个，那么维度1、维度2和已选择区域都不显示
        if (parentObj.find(".sku-color-select li").size() == 1 && parentObj.find(".sku-size-select li").size() == 1) {

            if (parentObj.find(".sku-color-select li a").eq(0).attr("alt").indexOf("null") >= 0 || parentObj.find(".sku-color-select li a").eq(0).attr("alt") == "单色") {
                parentObj.find(".sku-color-div").hide();
            }

            //若唯一一个维度2的值是null，则不显示维度2区域和已选择区域
            if (parentObj.find(".sku-size-select li").eq(0).text().indexOf("null") >= 0) {
                parentObj.find(".sku-size-div").hide();
                parentObj.find(".sku-selected-title").hide();
            }
        } else {
            var sizeEle = parentObj.find(".sku-size-select li").eq(0).find("a");
            if (sizeEle.text().indexOf("null") >= 0) {
                sizeEle.text("均码");
                sizeEle.attr("alt", (sizeEle.attr("alt").replace("null", "均码")));
                sizeEle.attr("title", (sizeEle.attr("title").replace("null", "均码")));

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

        //9、添加查看我的尺码功能
        try {
            sizeLink();
        } catch(e) {

}
        return false;
    });
};

function BindDimention2ClickArrivingNotify(skuItem, styleEntity, parentObj, objDom, canBuy) {
    objDom.click(function() {
        parentObj.find("div.areaStore").hide();
        //设置当前选中的Dimention2
        var currSKUId2 = new KeyValueItem();
        currSKUId2.key = styleEntity.StyleId;
        currSKUId2.value = skuItem.SKUDimentionId2;
        AddOrUpdateToKeyValueArray(currSKUId2, currSKUId2ArrivingNotifyArray);

        //1. 更改维2当前li元素的选中状态
        parentObj.find(".sku-size-select li.cur").removeClass("cur");
        parentObj.find(".sku-size-select li.loseCur").removeClass("loseCur");
        var d2SelectedClass = canBuy || (styleEntityListJson[0].PreSaleFlag && styleEntityListJson[0].PreSaleFlag == true) ? "cur": "loseCur";
        parentObj.find(".sku-size-select li[skuId2=" + skuItem.SKUDimentionId2 + "]").addClass(d2SelectedClass);
        //2. 更改维度2所关联的界面文字
        parentObj.find(".sku-size-title .hl2").text(skuItem.SKUDimentionName2);
        //3. 已选择文字
        var skuName2 = skuItem.SKUDimentionName2;
        if (skuName2 == null || skuName2 == "null") {
            skuName2 = "均码";
        } else if (skuName2 == "001") {
            skuName2 = "";
        }
        var ma = "";
        if (skuName2 != null && skuName2 != "" && skuName2.indexOf("码") < 0) {
            ma = "码";
        }
        var selectedText = "已选择<span class=\"h ml10\">" + skuItem.SKUDimentionName1 + "</span><span class=\"h ml10\" id=\"sku-select-size\">" + skuName2 + ma + "</span>";
        parentObj.find(".sku-selected-title").html(selectedText);
        curSkuItem = skuItem;

        parentObj.find(".sku-addcart").attr("itemid", skuItem.ItemId);
        parentObj.find(".sku-addcart").attr("productId", skuItem.ProductId);
        parentObj.find("#arrivingNotifyButton").attr("itemid", skuItem.ItemId);
        parentObj.find("#arrivingNotifyButton").attr("productId", skuItem.ProductId);

        parentObj.find("#presellButton").attr("itemid", skuItem.ItemId);
        parentObj.find("#presellButton").attr("productId", skuItem.ProductId);

        $("#spanItemId").html(skuItem.ItemId);
        $("#itemNumberArea").show();

        parentObj.attr("maxqty", skuItem.QtyAvailable);
        CheckArea();
        return false;
    });
};

function CheckArea() {
    var parentObj = $(".choicebox");

    /*预售逻辑**************************************/
    if (typeof(styleEntityListJson[0].PreSaleFlag) != "undefined" && styleEntityListJson[0].PreSaleFlag == true) {
        //预售
        $(".icon-presell").removeClass("none");
        var whMsgEle = parentObj.find(".selecetd-province");
        whMsgEle.html(whMsgEle.attr("displayName") + "(预订)").removeClass("c9");
        parentObj.find("#divBuyInfo").hide();
        parentObj.find("a.sku-addcart").hide();
        parentObj.find("#arrivingNotifyButton").hide();
        parentObj.find("#sellOutButton").hide();
        $("#presellButton").show();

        if (curSkuItem.ExpectReceiptDate != null) {
            var d = new Date(Date.parse(Date.ToDate(curSkuItem.ExpectReceiptDate)) + (86400000 * preSaleLimit));
            var preym = d.getFullYear() + "年" + (d.getMonth() + 1) + "月";
            var predd = d.getDate();
            var premsg = "";
            if (predd <= 10) {
                premsg = "上旬";
            } else if (predd > 10 && predd <= 20) {
                premsg = "中旬";
            } else {
                premsg = "下旬";
            }
            parentObj.find(".msg-presale").html("<p>正在积极补货中，预计到货时间：" + preym + premsg + "</p>").get(0).style.display = "";
        } else {
            parentObj.find(".msg-presale").html("").hide();
        }
    }
    /*以上预售逻辑**************************************/
    else {
        $(".icon-presell").addClass("none");
        parentObj.find(".msg-presale").html("").hide();
        $("#presellButton").hide();

        var curD2 = parentObj.find(".sku-size-select").find("li.cur");

        var isStock;
        if (curD2.length > 0) {
            isStock = curD2.attr("isStock") == 1;
        }

        var canProvinceBuy;

        if (isStock && curSkuItem.IsRegionSell) {
            var whs = "";
            for (var i in curSkuItem.RegionAreaWHIds) {
                var wh = curSkuItem.RegionAreaWHIds[i];
                if (wh && wh != "" && wh.length > 2) {
                    whs += "'" + wh.substring(0, 2) + "',";
                }
            }
            if (whs != "") {
                whs = whs.substring(0, whs.length - 1);
            }

            $("#queryAreaInnerlink").attr("whIds", whs);
            $("#queryAreaInnerlink").show();
            $("#queryArealink").attr("whIds", whs);
            $("#queryArealink").show();
        } else {
            $("#queryArealink").hide();
            $("#queryAreaInnerlink").hide();
        }
        if (curSkuItem.IsRegionSell) {
            canProvinceBuy = false;
            var provinceId = parentObj.find(".selecetd-province").attr("provinceId");
            var tmpWh = "";

            if (provinceWarehouseMapping && provinceWarehouseMapping.length > 0) {
                for (var mapping in provinceWarehouseMapping) {
                    if (provinceWarehouseMapping[mapping].Key == provinceId) {
                        tmpWh = provinceWarehouseMapping[mapping].Value;
                        break;
                    }
                }
            }
            if (isStock && curSkuItem.RegionAreaWHIds != null && curSkuItem.RegionAreaWHIds.length > 0) {
                for (var i in curSkuItem.RegionAreaWHIds) {
                    var tmpId = curSkuItem.RegionAreaWHIds[i];
                    if (tmpId != null && tmpId != "" && provinceId != null && provinceId != "" && tmpId.length > 2 && tmpId.substring(0, 2) == tmpWh) {
                        canProvinceBuy = true;
                        break;
                    }
                }
            }
        } else {
            canProvinceBuy = isStock;
        }

        var whMessage = curSkuItem.WHShowMsg;
        parentObj.find("#pSkuNotify").hide();
        parentObj.find("#pSkuNotify").text(whMessage);
        var whMsgEle = parentObj.find(".selecetd-province");
        if (canProvinceBuy) {
            if (whMessage && whMessage != "" && whMessage.indexOf("补货中") >= 0) {
                parentObj.find("#pSkuNotify").show();
            }
            var tmpMessage = "有货";
            if (styleEntityListJson[0].StockAllocateMode == 0 && curSkuItem.QtyAvailable <= 0) {

                tmpMessage = "预订";

            }
            whMsgEle.html(whMsgEle.attr("displayName") + "(" + tmpMessage + ")").removeClass("c9");
            parentObj.find(".selecetd-province") parentObj.find("#divBuyInfo").show();
            parentObj.find("a.sku-addcart").show();
            parentObj.find("#arrivingNotifyButton").hide();
            parentObj.find("#sellOutButton").hide();
        } else {
            whMsgEle.html(whMsgEle.attr("displayName") + "(" + "无货" + ")").addClass("c9");
            parentObj.find("#divBuyInfo").hide();
            parentObj.find("a.sku-addcart").hide();

            if (isCloseArrivingNotify) {
                parentObj.find("#arrivingNotifyButton").hide();
                parentObj.find("#sellOutButton").show();
            } else {
                parentObj.find("#arrivingNotifyButton").show();
                parentObj.find("#sellOutButton").hide();
            }
        }
    }

    var selectTitle = parentObj.find(".sku-selected-title");
    if (selectTitle.html().indexOf("null") < 0) {
        selectTitle.show();
    }

}
/********************Arriving Notify End************************/
