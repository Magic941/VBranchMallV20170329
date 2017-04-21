function LazyLoad(a, b) {
    if (b === undefined && a instanceof Object) {
        b = a;
        a = document
    }
    a = a || document;
    this.containers = $(a);
    this.config = $.extend({},
    LazyLoad.defaultConfig, b);
    this.callbacks = {
        els: [],
        fns: []
    };
    this._init()
}
LazyLoad.defaultConfig = {
    mod: "manual",
    diff: "default",
    imgFlag: "data-lazy",
    areaFlag: "lazy-area",
    placeholder: "none",
    execScript: true
};
LazyLoad.prototype = {
    _init: function () {
        this.threshold = this._getThreshold();
        this._filterItems();
        this._initLoadEvent()
    },
    _filterItems: function () {
        var a = this,
        b, c, d = [],
        e = [];
        a.containers.each(function (f, g) {
            b = $("img", g).toArray();
            d = d.concat($.grep(b,
            function (h) {
                return a._filterImg.call(a, h)
            }));
            c = $("textarea", g).toArray();
            e = e.concat($.grep(c,
            function (h) {
                return a._filterArea.call(a, h)
            }))
        });
        a.images = d;
        a.areas = e
    },
    _filterImg: function (a) {
        var b = this.config,
        c = $(a),
        d = a.getAttribute(b.imgFlag),
        e = this.threshold,
        f = b.placeholder;
        c.addClass('LazyLoad_min_v5');
        if (a.src && a.src.toLowerCase().endsWith('.jpg')) {
            c.addClass('LoadingImg');
        }
        if (!a.complete || (typeof a.naturalWidth != "undefined" && a.naturalWidth == 0)) {
//            a.src = "/Content/themes/base/images/404/224.jpg";
//            $(a).attr('loaded', 'error LazyLoad_min_v5').unbind('error');
        }

        if (b.mod === "manual") {
            if (d) {
                //                if (f !== "none") a.src = f;
                a.removeAttribute("src");
                return true
            }
        } else if (c.offset().top > e && !d) {
            c.attr(b.imgFlag, a.src);
            //            if (f !== "none") a.src = f;
            //            else a.removeAttribute("src");
            a.removeAttribute("src");
            return true
        }
    },
    _filterArea: function (a) {
        return $(a).hasClass(this.config.areaFlag)
    },
    _initLoadEvent: function () {
        function a() {
            c || (c = setTimeout(function () {
                b();
                c = null
            },
            20))
        }
        function b() {
            d._loadItems();
            if (d._getItemsLength() === 0) {
                f.unbind("scroll", a);
                f.unbind("resize", e)
            }
        }
        var c, d = this,
        e, f = $(window);
        f.bind("scroll", a);
        f.bind("resize", e = function () {
            d.threshold = d._getThreshold();
            a()
        });
        d._getItemsLength() && $(function () {
            b()
        })
    },
    _loadItems: function () {
        this._loadImgs();
        this._loadAreas();
        this._fireCallbacks()
    },
    _loadImgs: function () {
        var a = this;
        a.images = $.grep(a.images,
        function (b) {
            return a._loadImg.call(a, b)
        })
    },
    _loadImg: function (a) {
        var b = this.threshold + $(document).scrollTop();
        if ($(a).offset().top <= b) this._loadImgSrc(a);
        else return true
    },
    _loadImgSrc: function (a, b) {
        b = b || this.config.imgFlag;
        //        console.log('_loadImgSrc > ' + new Date()  + ' > ' + b);

        if (!a.complete || (typeof a.naturalWidth != "undefined" && a.naturalWidth == 0)) {
//            a.src = "/Content/themes/base/images/404/224.jpg";
//            $(a).attr('loaded', 'error LazyLoad_min_v5').unbind('error');
        }
        a.onerror = function () {
            a.onerror = null;
            a.src = '/Content/themes/base/images/404/224.jpg';
            $(a).attr('loaded', 'error');
        };
        var c = a.getAttribute(b);
        if (c && a.src != c) {
            a.src = c;
            a.removeAttribute(b)
        }
    },
    _loadAreas: function () {
        var a = this;
        a.areas = $.grep(a.areas,
        function (b) {
            return a._loadArea.call(a, b)
        })
    },
    _loadArea: function (a) {
        var b;
        b = $(a).css("display") === "none" ? a.parentNode : a;
        if ($(b).offset().top <= this.threshold + $(document).scrollTop()) this._loadAreaData(a.parentNode, a);
        else return true
    },
    _loadAreaData: function (a, b) {
        b.style.display = "none";
        b.className = "";
        var c = document.createElement("div");
        a.insertBefore(c, b);
        c.innerHTML = b.value
    },
    _fireCallbacks: function () {
        var a = this.callbacks,
        b = a.els,
        c = a.fns,
        d = this.threshold + $(document).scrollTop(),
        e,
        f,
        g,
        h = [],
        i = [];
        for (e = 0; (f = b[e]) && (g = c[e++]); ) if ($(f).offset().top <= d) g.call(f);
        else {
            h.push(f);
            i.push(g)
        }
        a.els = h;
        a.fns = i
    },
    addCallback: function (a, b) {
        var c = this.callbacks;
        a = $(a);
        if (a.length > 0 && $.isFunction(b)) {
            c.els.push(a);
            c.fns.push(b)
        }
    },
    _getThreshold: function () {
        var a = this.config.diff,
        b = $(window).height();
        return a === "default" ? 2 * b : b + +a
    },
    _getItemsLength: function () {
        return this.images.length + this.areas.length + this.callbacks.els.length
    },
    loadCustomLazyData: function (a, b) {
        var c = this,
        d, e;
        a = $(a);
        a.each(function (f, g) {
            switch (b) {
                case "img":
                    e = g.nodeName === "IMG" ? [g] : $(g).find("img").toArray();
                    $.grep(e,
                function (h) {
                    return c._loadImgSrc(h, "data-lazy-custom")
                });
                    break;
                default:
                    d = $(g).find("textarea");
                    d.hasClass("lazy-area-custom") && c._loadAreaData(g, d[0])
            }
        })
    }
};
LazyLoad.mix = function (a, b, c) {
    var d, e, f = c.length;
    for (d = 0; d < f; d++) {
        e = c[d];
        a[e] = b[e]
    }
    return a
};
LazyLoad.mix(LazyLoad, LazyLoad.prototype, ["loadCustomLazyData", "_loadImgSrc", "_loadAreaData"]);