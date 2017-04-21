(function () {
    var relHash = location.hash,
    relLoc, relStr, serverURL = '/',
    toLoc = '&targetURL=/Areas/Shop/Themes/M1/Content/images/ph.png';
    relLoc = relHash.indexOf('rel=') + 4;
    if (relLoc === 3) {
        return;
    }
    relStr = relHash.slice(relLoc); (new Image()).src = serverURL + relStr + toLoc;
})();

var $MaticsofthdMenu;
$(function () {
    //新COMM
    //搜索框
    var $hdText, isSearchKey, curKeyWords, daykwords, daykwordsUrl;
    $hdText = $('#hd-search .hd-text');
    //判断当前页面的搜索关键字
    isSearchKey = location.href.indexOf("search.maticsoft");
    curKeyWords = $("body").attr("data-keywords");
    daykwords = $("#day-keywords").attr("name");
    daykwordsUrl = $("#day-keywords").val();
    if (isSearchKey != -1 && curKeyWords != "") {
        $hdText.val(curKeyWords).removeClass("c9");
    } else {
        $hdText.val(daykwords);
        $hdText.focus(function () {
            $(this).val('').removeClass("c9");
        }).blur(function () {
            if ($(this).val() != '') {
                return false;
            } else {
                $(this).addClass('c9').val(daykwords);
            }
        });
    }
    $('#hd-search form:first').submit(function () {
        if ($hdText.val() == $hdText[0].defaultValue || $hdText.val() == '') {
            return false;
        } else if ($hdText.val() === daykwords) {
            window.open(daykwordsUrl);
            return false;
        }
    });
    //auto complete
    //注释于2012/08/29
    //导航菜单
    var $menuList = $("#hd-nav li"),
    $menuContent = $("#menuItem2").find(".mdetail"),
    $menuContainer = $("#menuItem2");

    $MaticsofthdMenu = {
        showMenu: function(i, isLoadData) {
            var n = i,
                me = this,
                $winWidth = ($(window).width() >= 980) ? $(window).width() : 980,
                showfun;
            clearTimeout(me.hideMenuTimer);

            if (isLoadData) {
                var cid = $menuList.eq(i).attr("cid");
                $("#links").load("/Main_Page #p-Getting-Started li");
                if (parseInt(cid) > 0) {
                    $.ajax({
                        type: "POST",
                        dataType: "text",
                        url: $Maticsoft.BasePath + "Partial/MenuDetail",
                        async: false,
                        data: { Cid: cid },
                        success: function(data) {
                            $("#menuItem2").html(data.replace('{Index}', n));
                            $menuContent = $("#menuItem2").find(".mdetail");
                        }
                    });
                }
            }

            showfun = function() {
                if ($menuList.eq(n).hasClass("noSubmenu")) {
                    me.hideMenu();
                    return false;
                } else {
                    $menuList.eq(n).addClass('hover');
                }
                if ($.browser.msie && $.browser.version == 6) {
                    $("#selectMask").height($menuContent.eq(0).height() + 11).width($menuContent.eq(0).width() + 4);
                }
                $menuList.eq(n).siblings().removeClass('hover');
                $menuContainer.css({
                    visibility: "visible"
                });
                $menuContent.css("visibility", "hidden");
                $menuContent.eq(0).css("visibility", "visible");
                $menuContainer.css("left", (($winWidth - 980) / 2) + 46 * (n - 1));
                $menuContainer.css("top", $('#hd').height() - $('#hd-submenu').height());
            };
            me.showMenuTimer = setTimeout(showfun, 80);
        },
        hideMenu: function() {
            var me = this,
                hidefun;
            clearTimeout(me.showMenuTimer);
            hidefun = function() {
                $menuList.removeClass('hover');
                $menuContainer.css({
                    visibility: "hidden"
                });
                $menuContent.css("visibility", "hidden");
            };
            me.hideMenuTimer = setTimeout(hidefun, 80);
        },
        init: function() {
            $menuList.hover(function() {
                var n = $menuList.index($(this));
                $MaticsofthdMenu.showMenu(n, true);
            },
                function() {
                    $MaticsofthdMenu.hideMenu();
                });
        },
        initSubMenu: function() {
            $menuContent = $("#menuItem2").find(".mdetail");
            $menuContainer = $("#menuItem2");
            $menuContent.hover(function() {
                var n = $menuContent.attr('index');
                $MaticsofthdMenu.showMenu(n);
            },
                function() {
                    $MaticsofthdMenu.hideMenu();
                }
            );
        }
    };
    //搜索关键字
    var $skeywcur = $("#sKeywords-cur"),
    $skeyw = $("#sKeywords");
    if ($skeywcur.find("dd").size() === 0) {
        $("#hdsearch-box").append($skeyw);
        $skeyw.css("visibility", "visible");
    } else {
        $("#hdsearch-box").append($skeywcur);
        $skeywcur.css("visibility", "visible");
    }
    //顶部slide
    $("#headSlide-box").append($("#headSlide"));
    $("#headSlide").css("visibility", "visible");
    $MaticsofthdMenu.init();

    //顶部下拉菜单
    $("li.hd-qbar-list-drop").hover(function () {
        $(this).find("ul").show();
    },
    function () {
        $(this).find("ul").hide();
    });

    //联合登录修改Email
    var $hd = $('#hd-login');

    /* //去除判断条件的新脚本 2012-08-30
    var changeEmailHtml = '<div class="tips tip-t" id="J_qqbox"><div><p><a href="/Contact/ContactEmail.aspx" target="_blank">设置Email地址</a>，及时了解订单状态，收到礼券、促销信息</p></div><div class="tip" style="right:85%"><b></b><i></i></div><a href="javascript:;" class="tips-cls qqclose"></a></div>';
    var offset = $hd.offset();
    if (!offset) {
    return;
    }
    $(changeEmailHtml).appendTo('body').css({
    left: offset.left + 0,
    top: offset.top + 30,
    position: 'absolute',
    zIndex: 10
    });
    $('#J_qqbox .qqclose').click(function () {
    $('#J_qqbox').remove();
    });
    */

    //购物车数量
    if (window.cartItemSum !== undefined) {
        $('#hd-cart-amount').text(cartItemSum);
    }
    //问卷调查入口
    function creatLink(link, txt) {
        return '<div class="wrap" style="margin-bottom:-30px"><div style="padding-left:18px;background:url(/Areas/Shop/Themes/M1/Content/images/ico-talk.gif) no-repeat 0 4px;" class="mt20">您对' + txt + '有意见或建议么？ <a class="a2" href="http://static.maticsoft.com/research/' + link + '.shtml" target="_blank">请告诉我们</a></div></div>';
    }
    var regList = /list/i,
    regSearch = /search/i,
    regCart = /OrderShopCart/i,
    locHost = location.hostname,
    locHref = location.href;
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

});