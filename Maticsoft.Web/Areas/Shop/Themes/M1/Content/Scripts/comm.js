(function () {
    var relHash = location.hash, relLoc, relStr, serverURL = '/app/AdEntrance.aspx?from=', toLoc = '&targetURL=/Areas/Shop/Themes/M1/Content/images/ph.png';
    relLoc = relHash.indexOf('rel=') + 4;
    if (relLoc === 3) {
        return;
    }
    relStr = relHash.slice(relLoc);
    (new Image()).src = serverURL + relStr + toLoc;
})();
$(function () {
    //我的动软下拉v2
    $("li.hdMy").hover(function () {
        $(this).find("ul").show();
    }, function () {
        $(this).find("ul").hide();
    });

    //联合登录修改Email
    var $hd = $('ul.hplogin');
    if (window.IsShowChangeEmail && $hd.length > 0) {
        var changeEmailHtml = '<div class="qqbox" id="J_qqbox"><div class="qq-arrow"></div><div class="qq-txt"><a href="/Contact/ContactEmail.aspx" class="a2" target="_blank">设置Email地址</a>，及时了解订单状态，收到礼券、促销信息<a href="javascript:;" class="qqclose">关闭</a></div></div>';
        var offset = $hd.offset();
        $(changeEmailHtml).appendTo('body').css({ left: offset.left + 25, top: offset.top + 20, position: 'absolute', zIndex: 10 });
        $('#J_qqbox .qqclose').click(function () {
            $('#J_qqbox').remove();
        });
    }

    //问卷调查入口
    function creatLink(link, txt) {
        return '<div class="wrap" style="margin-bottom:-30px"><div style="padding-left:18px;background:url(/Areas/Shop/Themes/M1/Content/images/ico-talk.gif) no-repeat 0 4px;" class="mt20">您对' + txt + '有意见或建议么？ <a class="a2" href="http://static.maticsoft.com/research/' + link + '.shtml" target="_blank">请告诉我们</a></div></div>';
    }
    var regList = /list/i, regSearch = /search/i, regCart = /OrderShopCart/i, locHost = location.hostname, locHref = location.href;
    if (regList.test(locHost) && !window.researchHTML) {
        window.researchHTML = creatLink('list', '列表页');
        $('#help3').parent().before(researchHTML);
    } else if (regSearch.test(locHost) && !window.researchHTML) {
        window.researchHTML = creatLink('search', '搜索结果');
        $('#help3').parent().before(researchHTML);
    } else if (regCart.test(locHref) && !window.researchHTML) {
        window.researchHTML = creatLink('cart', '购物车');
        $('#sft2').before(researchHTML);
    }

    //add compagin banner
    /*var $banner,$bannerBbs,burl,istuan,isnew,ishot,islost,isbbs,showbanner,mbbs;
    $bannerBbs=$('<div class="wrap980 none b-tempos mb10"><a href="/market/LuckyPointSweepstake.aspx#rel=MDH06" target="_blank"><img src="http://img.maticsoft.com/web/pic/other/1208/0802/980x60.jpg" width="980" height="60"  style="vertical-align:bottom" alt="八月积分大返现，全场满59免运费" /></a></div>')
    burl=window.location.href;
    ishot  = burl.indexOf("comment/index.htm")!=-1;
    isbbs  = burl.indexOf("square.htm")!=-1;
    mbbs=ishot||isbbs;
    if(!mbbs || $("div.b-tempos").size()!=0){
    return false;
    }
    else{
    if(mbbs && $("div.b-tempos").size()==0){
    $("#divmenu").after($bannerBbs);
    $("div.b-tempos").css("marginTop","-20px");
    $bannerBbs.slideDown("1000");
    }
    }*/

});

