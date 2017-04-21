/*!
 * jQuery JavaScript Library v1.8.2
 * http://jquery.com/
 *
 * Includes Sizzle.js
 * http://sizzlejs.com/
 *
 * Copyright 2012 jQuery Foundation and other contributors
 * Released under the MIT license
 * http://jquery.org/license
 *
 * Date: Thu Sep 20 2012 21:13:05 GMT-0400 (Eastern Daylight Time)
 */

/****************
*公用函数
****************/
 
//新窗口打开
function newWinOpen(jobUrl) {
    window.open(jobUrl);
}
//Cookie
function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}
function setCookie(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
        ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}
//系统检测
function detectOS() {
    detectOSWarning = getCookie('detectOSWarning');
    if (detectOSWarning != null && detectOSWarning != "") {
        //alert('Welcome again ' + detectOSWarning + '!')
    } else {
        var brow = $.browser;
        var warning = function () {
            var msg = "<div id='detectos' style=' position: fixed; top: 0px; right: 0px; left: 0px; z-index: 10000; background-color: #fff; color: #f00; text-align: center; line-height: 180%; border: 1px solid #f00;'>您正在使用低版本浏览器,为了保证您能有更好的访问效果,我们建议您使用能支持HTML5的谷歌Chrome、苹果Safari、火狐Firefox、欧朋opera、IE10版本浏览！   点击关闭提示</div>";
            $("body").prepend(msg);
            $("#detectos").bind("click", function () {
                $(this).hide().unbind("click");
                window.setTimeout(function () {
                    $("#detectos").remove();
                }, 500);
            });
        };

        if (brow.msie && (brow.version == "9.0" || brow.version == "8.0" || brow.version == "7.0" || brow.version == "6.0" || brow.version == "5.0")) {
            warning();
            setCookie('detectOSWarning', 1, 1);
        }
        /*
                else if (!brow.msie && (typeof (Worker) == "undefined")) {
                    warning();
                }
                //var bInfo = "";
                //if (brow.mozilla) { bInfo = "Mozilla Firefox " + brow.version; }
                //if (brow.safari) { bInfo = "Apple Safari " + brow.version; }
                //if (brow.opera) { bInfo = "Opera " + brow.version; }
                //
                */
    }
};

 



/****************
*功能插件
****************/

//多图弹出
(function ($) {

    $.fn.piclist = function (options) {
        var prototype = {
            id: "piclist",
            title: null,
            message: null,
            closelClass: "close",
            closelText: "close",
            prevClass: "prev",
            prevText: "prev",
            nextClass: "next",
            nextText: "next",
        };

        var opts = $.extend(prototype, options);
        var $that = $(this);
        var show = function ($that) {
            var self = opts;
            var markup = '<div id="' + self.id + '" class="hidden">' + opts.message + '\
                                <a id="close" href="javascript:;" class="' + self.closelClass + '">' + self.closelText + '</a>\
	        					<a id="prev" href="javascript:;" class="' + self.prevClass + '">' + self.prevText + '</a>\
	        					<a id="next" href="javascript:;" class="' + self.nextClass + '">' + self.nextText + '</a>\
	        			</div>';
            $("body").append(markup);
            var $el = $("#" + self.id);
            //positionPopup();
            $el.find('A').each(function () {
                var button = $(this);
                button.bind('click', function (e) {
                    if (button.attr('id') == 'next') {
                        imglistload("n");
                    }
                    else if (button.attr('id') == 'prev') {
                        imglistload("p");
                    }
                    else {
                        hide();
                    }
                });
            });
            $el.bind("orientationchange", function () {
                positionPopup();
            }).bind("swipeleft", function () {
                imglistload("n");
            }).bind("swiperight", function () {
                imglistload("p");
            });
            blockUI(0.5);
            $el.removeClass('hidden');
        };
        var hide = function () {
            var self = prototype;
            var $el = $("#" + self.id);
            $el.addClass('hidden');
            unblockUI();
            setTimeout(function () {
                remove();
            }, 250);
        };
        var remove = function () {
            var self = prototype;
            var $el = $("#" + self.id);
            $el.find('#close').unbind('click');
            $el.find('#prev').unbind('click');
            $el.find('#next').unbind('click');
            $el.find('#productpiclist').unbind('swipeleft').unbind('swiperight');
            $el.remove();
        };
        var imglistload = function (e) {
            var self = prototype;
            var $el = $("#" + self.id);
            var title = $el.find('header')
            var that = $el.find("#productpiclist");
            if (picListInitial < picListArrLength && (e == "n")) {
                picListInitial = picListInitial + 1;
                that.html('<img src="' + picListArr[picListInitial].src + '"/>');
                title.html(picListArr[picListInitial].title);
                $el.find("#productpiclist img").lazyload();
            }
            if (picListInitial > 0 && (e == "p")) {
                picListInitial = picListInitial - 1;
                that.html('<img src="' + picListArr[picListInitial].src + '"/>');
                title.html(picListArr[picListInitial].title);
                $el.find("#productpiclist img").lazyload();
            };
        };
        var positionPopup = function() {
            var popup = $("#" + opts.id);
            popup.css("top", ((window.innerHeight / 2.5) + window.pageYOffset) - (popup[0].clientHeight / 2) + "px");
            popup.css("left", (window.innerWidth / 2) - (popup[0].clientWidth / 2) + "px");
        };
        show();
        //positionPopup();
    };
    var uiBlocked = false;
    var blockUI = function (opacity) {
        if (uiBlocked)
            return;
        opacity = opacity ? " style='opacity:" + opacity + ";'" : "";
        $('BODY').prepend($("<div id='mask'" + opacity + "></div>"));
        $('BODY DIV#mask').bind("touchstart", function (e) {
            e.preventDefault();
        });
        $('BODY DIV#mask').bind("scrollstart", function (e) {
            e.preventDefault();
        });
        uiBlocked = true;
    };

    var unblockUI = function () {
        uiBlocked = false;
        $('BODY DIV#mask').unbind("touchstart");
        $('BODY DIV#mask').unbind("touchmove");
        $("BODY DIV#mask").remove();
    };
    


})(jQuery);
 
 

//显示分类
function showNavSortbar(el, sortUrl) {
    var $el = $('.ui-page-active .sortbar'), btn = $(el);
    btn.toggleClass("selected");
    if ($el.length > 0) {
        $el.toggle();
    } else {
        var $that = $('.ui-page-active');
        $.ajax({
            beforeSend: function () { $.mobile.showPageLoadingMsg() },
            dataType: "html",
            url: sortUrl,
            cache: false,
            success: function (data) {
                $that.append(data);
                $.mobile.hidePageLoadingMsg();
            }
        });
    };

};

 
 
 
//显示工具栏
function toggletools() {
    var $el = $('.ui-page-active .content');
    var $header = $("#header");
    var $footer = $("#navbar");
    $header.toggle();
    $el.toggleClass("showheader");
    $footer.toggle();
    $el.toggleClass("showfooter");
};
function toggleprevnext(el) {
    var $el = $('.ui-page-active .pagePrevNext');
    var that = $('.ui-page-active .showmenubtn');
    toggletools();
    $el.toggleClass("fixed");
    that.toggleClass("on");
};




/****************
*页面载入时运行
****************/
function pageEvent() {
 

    //图片滚动
    var imgScroller = function () {
        var eleIndex = $(".mainimgbox>.indexbox"), eleList = $(".mainimgbox>.box >*"), $el = $(".mainimgbox>.box");
        var indexElement = 0, eleSlideIn = null;
        var funIndex = function () {
            var htmlIndex = '';
            eleList.each(function () {
                if ($(this).hasClass("in")) {
                    htmlIndex += '<i></i>';
                } else {
                    htmlIndex += '<i class="on"></i>';
                }
            });
            eleIndex.html(htmlIndex);
        };
        $el.live({
            /* 屏蔽单击事件
            click: function () {
                if (indexElement >= eleList.length) {
                    indexElement = 0;
                }
                eleSlideIn && eleSlideIn.addClass("slide out").animationComplete(function () {
                    $(eleList.get(indexElement - 2)).removeClass("slide out").hide()
                });
                eleSlideIn = $(eleList.get(indexElement)).show().addClass("slide in").animationComplete(function () {
                    eleSlideIn.removeClass("slide in");
                });
                funIndex();
                indexElement++;
            },
            */
            swipeleft: function () {
                if (indexElement >= eleList.length) {
                    indexElement = 0;
                }
                eleSlideIn && eleSlideIn.addClass("slide out").animationComplete(function () {
                    $(eleList.get(indexElement - 2)).removeClass("slide out").hide();
                });
                eleSlideIn = $(eleList.get(indexElement)).show().addClass("slide in").animationComplete(function () {
                    eleSlideIn.removeClass("slide in");
                });
                funIndex();
                indexElement++;
            },
            swiperight: function () {
                if (indexElement <= 0) {

                    indexElement = eleList.length;
                }
                eleSlideIn && eleSlideIn.addClass("slide out reverse").animationComplete(function () {
                    $(eleList.get(indexElement)).removeClass("slide out reverse").hide();
                });
                eleSlideIn = $(eleList.get(indexElement - 2)).show().addClass("slide in reverse").animationComplete(function () {
                    eleSlideIn.removeClass("slide in reverse");
                });
                funIndex();
                indexElement--;
            }
        }).trigger("swipeleft");
        window.setInterval(function () { $el.trigger("swipeleft"); }, 10000);
    };
    //TAB列表
    var tablist = function () {
        $(".mainlistsbox").each(function (i, n) {
            var $el = $(this);
            var nav = $el.find(".bar_nav li");
            var item = $el.find(".nav_content_list > *");
            var indexElement = $el.find(".nav_content_list > *").length;
            var funIndex = function (i) {
                nav.eq(i).addClass("on").siblings().removeClass("on");
            };
            nav.live('click', function () {
                var that = $(this), index = $(this).index();
                item.each(function (i, n) {
                    var that = $(this);
                    if (that.css("display") == "block" && index != i) {
                        that.addClass("slide out").animationComplete(function () {
                            that.removeClass("slide out").hide();
                        });
                    }
                });
                item.eq(index).show().addClass("slide in").animationComplete(function () {
                    item.eq(index).removeClass("slide in");
                });
                funIndex(index);
            });
            item.live("swipeleft", function () {
                var that = $(this), index = $(this).index();
                if (indexElement != (index + 1)) {
                    that.addClass("slide out").animationComplete(function () {
                        that.removeClass("slide out").hide();
                    });
                    item.eq(index + 1).show().addClass("slide in").animationComplete(function () {
                        item.eq(index + 1).removeClass("slide in");
                    });
                    funIndex(index + 1);
                };
            });
            item.live("swiperight", function () {
                var that = $(this), index = $(this).index();
                if (index != 0) {
                    that.addClass("slide out reverse").animationComplete(function () {
                        that.removeClass("slide out reverse").hide();
                    });
                    item.eq(index - 1).show().addClass("slide in reverse").animationComplete(function () {
                        item.eq(index - 1).removeClass("slide in reverse");
                    });
                    funIndex(index - 1);
                }
            });

        });
    };
    tablist();
    imgScroller();
};
window.addEventListener("load", pageEvent, false);

/****************
*MOBI插件功能
****************/

//覆盖mobile
$(document).bind("mobileinit", function () {
    //$.mobile.page.prototype.options.backBtnText = "previous";
    // $.mobile.activePageClass = "ui-page-custom";
    // $.mobile.allowCrossDomainPages = "false";
    // $.mobile.activeBtnClass = "ui-btn-active";
    $.mobile.defaultPageTransition = "slide";
    $.event.special.swipe.horizontalDistanceThreshold = 100;


});



/****************
*页面切换函数
****************/

//页面切换变量
var prevId, nextId;
 

 
//页面 swipe
var swipePage = function (e) {
    var $el = $(".page[ax-type='ajax']");
    $el.live({
        swipeleft: function () {
            $.mobile.changePage("#" + nextId, { transition: "slide" }, true);
        },
        swiperight: function () {
            $.mobile.changePage("#" + prevId, { transition: "slide", reverse: true }, false);
        }
    });
};
window.addEventListener("load", swipePage, false);

//页面载入
$(document).on('pageshow', function () {
  //  checkurl();
    detectOS();
    var $el = $('.ui-page-active');
    var pageId = $('.ui-page-active').attr("id");
  
    $(".page[ax-type='ajax']").each(function (i) {
        var that = $(this).attr("id");
        if (that != prevId && that != pageId && that != nextId) {
            $(this).remove();
        }
    });
    window.setTimeout(function () {
        var dropList = $el.find(".dropList h2 span");
        if (dropList.length > 0) {
            dropList.unbind('vclick').bind('vclick', function () {
                var li = $(this).parent("h2").parent("li"), article = li.find('article'), ph;
                if (!li.hasClass('open')) {
                    li.addClass('open');
                    ph = article.children().height();
                    article.css({ height: 0 }).animate({ height: ph }, 'normal', 'easeOut');
                    article.find("img").lazyload();
                } else {
                    article.animate({ height: 0 }, 'normal', 'easeIn', function () {
                        li.removeClass('open');
                        article.css('height', 'auto');
                    });
                }
            });
        }
        var tabList = $el.find(".tab-buttons>*");
      
        if (tabList.length > 0) {
            var li = $el.find(".tab-content > *");
            tabList.bind('click', function() {
                var that = $(this), index = $(this).index();
                that.addClass("selected").siblings().removeClass("selected");
                li.eq(index).show().siblings().hide();

            });
        };

    }, 1000);
});
//页面退出
$(document).on('pagehide', function () {
});

 


