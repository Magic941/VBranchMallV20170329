function slideProduct(a, b) {
    this.container = $(a);
    this.mask = $("#newprd-img-b");
    this.link = $("#newprd-img-b-a");
    this.maskBg = $("#newprd-mask-bg");
    this.maskImg = $("#newprd-mask-img");
    this.config = $.extend({},
    slideProduct.defaultConfig, b);
    this._init()
}
slideProduct.prototype = {
    _init: function () {
        this.mask.animate({
            opacity: 1
        },
        this.config.duration);
        this._bindEvent()
    },
    startSlide: function (a, b) {
        b = $(b).find("img")[a];
        this._cancelMove();
        this._slideMove(a, b)
    },
    _bindEvent: function () {
        var a = this;
        a.container.bind("mouseover",
        function (b) {
            a._slideShow.call(a, b)
        })
    },
    _slideShow: function (a) {
        a = a.target;
        var b;
        b = $(a).parents("ul").find("img").index(a);
        b > -1 ? this._slideMove(b, a) : this._cancelMove()
    },
    _slideMove: function (a, b) {
        function c() {
            var f = d.index;
            f = $(b).parents("ul").find("img")[f];
            var g = d.config.position[d.index];
            d.mask.css("opacity", 0).css("left", g);
            d.reset();
            e(f, d);
            d._loadImg(f)
        }
        function e(f, g) {
            if (!f || !f.parentNode) return;
            f = f.parentNode;
            var h = f.title;
            g.link.attr("href", f.href).attr("title", h)
        }
        var d = this;
        d.index = a;
        if (!d.timer) d.timer = setTimeout(function () {
            c()
        },
        100)
    },
    _cancelMove: function () {
        if (this.timer) {
            clearTimeout(this.timer);
            this.timer = undefined
        }
    },
    _loadImg: function (a) {
        if (!a) return;
        //        var b = a.getAttribute("data-large");
        this.maskBg.attr("src", a.src).show();
        this.maskImg.attr("src", a.src).show();
        this.mask.stop().animate({
                opacity: 1
            },
            this.config.duration);
    },
    caculatePos: function () {
        for (var a = parseInt(this.mask.css("left")), b = this.config.position, c = 0; c < 5; c++) if (a === b[c]) return c;
        return 3
    },
    reset: function () {
        this.maskBg.removeAttr("src").hide();
        this.maskImg.removeAttr("src").hide();
    }
};
slideProduct.defaultConfig = {
    position: [25, 212, 399, 586, 773],
    duration: 150
};
$("#j-prdsort").mouseover(function (a) {
    $(a.target).parents("div.prdsort-list").addClass("prdsort-list-hover")
}).mouseout(function () {
    $(this).find("div.prdsort-list").removeClass("prdsort-list-hover")
});
$(".pic-box").mouseover(function (a) {
    $(this).find("img").css("opacity", 0.75);
    $(a.target).css("opacity", 1)
}).mouseout(function () {
    $(this).find("img").css("opacity", 1)
}); (function () {
    var a = $("#j-email");
    a.focus(function () {
        this.value === this.defaultValue && $(this).val("").removeClass("c9")
    }).blur(function () {
        if (this.value === "") {
            this.value = this.defaultValue;
            a.addClass("c9")
        }
    });
    $("#j-email-btn").click(function () {
        var b = $.trim(a.val());
        if (b === a[0].defaultValue) {
            alert("\u8bf7\u586b\u5199Email\u5730\u5740\uff0c\u8c22\u8c22\uff5e");
            a.focus()
        } else if (/\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/i.test(b)) window.open("/Contact/ContactEMailList.aspx?EMail=" + b);
        else {
            alert("\u60a8\u8f93\u5165\u7684Email\u683c\u5f0f\u6709\u8bef\uff0c\u8bf7\u68c0\u67e5\u5e76\u91cd\u65b0\u8f93\u5165\uff0c\u8c22\u8c22\uff5e");
            a.focus()
        }
    })
})();
var mmTuan = {
    baseImg: "../images/t.jpg",
    pad2: function (a) {
        return a.toString().length === 1 ? "0" + a : a
    },
    init: function (a) {
        return;
        var b = new Date,
        c = b.getMonth() + 1,
        e,
        d = mmTuan.pad2,
        f = $(a);
        e = "http://img.maticsoft.com/web/pic/hp/tuan/t" + b.getFullYear().toString().slice(2) + d(c) + d(b.getDate()) + ".jpg";
        a = new Image;
        a.onerror = function () {
            e = mmTuan.baseImg;
            f.attr("src", e)
        };
        a.onload = function () {
            f.attr("src", e)
        };
        a.src = e
    }
},
randomSlide = {
    random: function (a) {
        return parseInt(Math.random() * a)
    },
    init: function (a) {
        a = $(a);
        var b = a.length;
        if (b !== 1) {
            b = randomSlide.random(b);
            a.not(a.eq(b)).remove()
        }
    }
},
hotComment = {
    baseUrl: "http://comm.maticsoft.com/comment/hotcomment.htm?from=list&size=9&class=",
    commentId: ["N1", "N6", "FY", "N4", "N5", "ME", "N3", "N2"],
    complete: [],
    ajaxRequest: function (a, b) {
        return;
        a = hotComment.baseUrl + hotComment.commentId[a];
        var c, e = $(b);
        e.addClass("loading");
        $.ajax({
            url: a,
            dataType: "jsonp",
            success: function (d) {
                c = hotComment.createDOM(d);
                e.removeClass("loading");
                hotComment.setHtml(e, c)
            },
            error: function () {
                c = '<div class="mt50 ml10 mr10 tc">:(\u52a0\u8f7d\u6570\u636e\u5931\u8d25\uff0c\u8bf7\u5237\u65b0\u9875\u9762\u91cd\u65b0\u83b7\u53d6\uff0c\u8c22\u8c22</div>';
                hotComment.setHtml(e, c)
            }
        })
    },
    setHtml: function (a, b) {
        a.html(b)
    },
    createDOM: function (a) {
        var b = "",
        c = "",
        e, d = {},
        f = 0,
        g = a.length,
        h = hotComment.substitute;
        c += '<li class="comment-item"><a href="{href}" title="{title}" target="_blank">';
        c += '<img class="comment-pic" width="50" height="65" alt="{title}" src="{src}"></a>';
        c += '<div class="comment-info">';
        c += '<p class="cf"><a href="{url}" target="_blank" class="fl">{user}</a><i class="ico_star fr">\u4e94\u9897\u661f</i></p>';
        for (c += '<p class="comment-txt"><a href="{href}" title="{content}" target="_blank">{content}</a></p></div></li>'; f < g; f++) {
            e = a[f];
            d.title = e.name;
            d.href = e.url + "#rel=syrmpl01";
            d.src = e.image;
            d.url = "http://myshishang.maticsoft.com/share/" + e.contactId + "-" + e.hmac + "-1.htm";
            d.user = e.member;
            d.content = e.content;
            b += h(c, d)
        }
        return b
    },
    substitute: function (a, b) {
        return a.replace(/\{([^{}]+)\}/g,
        function (c, e) {
            c = b[e];
            return c !== undefined ? "" + c : ""
        })
    }
};