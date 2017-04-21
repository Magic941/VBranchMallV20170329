function Carousel(a, c, b, d) {
    this.container = $(a);
    this.clip = $(a + " .J_carousel_clip");
    this.list = $(a + " .J_carousel_list");
    this.item = $(a + " .J_carousel_item");
    this.trigger = $(a + " .J_carousel_trigger");
    this.showCount = c;
    this.timer = null;
    this.options = $.extend({
        auto: true,
        delay: 4,
        direction: "",
        duration: 100,
        etype: "click",
        num: c,
        vertical: false
    },
    b);
    this.init();
    if (d) {
        d()
    }
}
Carousel.prototype = {
    init: function () {
        var e = this,
        k = this.item,
        f = this.list,
        l = this.options,
        j = !!l.auto,
        c = !!l.vertical,
        h = k.length,
        i = k.outerWidth(true),
        d = k.outerHeight(true),
        b = c ? k.outerHeight(true) : k.outerWidth(true);
        f[c ? "height" : "width"](b * h).css({
            position: "absolute"
        });
        var a = f.outerWidth(true),
        g = f.outerHeight(true);
        this.clip.css({
            position: "relative",
            overflow: "hidden",
            width: (c ? Math.max(a, i) : b * this.showCount),
            height: (c ? b * this.showCount : Math.max(g, d))
        });
        if (h <= this.showCount) {
            this.trigger.hide();
            return
        } else {
            this.trigger.show()
        }
        this.trigger.bind(l.etype, this._trigger(this, b));
        if (j) {
            this._auto();
            this.container.hover(function () {
                e._stop()
            },
            function () {
                e._auto()
            })
        }
    },
    _trigger: function (b, a) {
        return function (o) {
            var t, c, d = b.trigger,
            p = b.list,
            f = b.showCount,
            u = b.options,
            s = u.etype,
            h = u.duration,
            n = u.num,
            g = !!u.vertical,
            q = b.item.length,
            l = q / f < 2 || q / n < 2 ? (q / f < 2 ? (n < q % f ? n : q % f) : f) : n,
            r = o.target.className,
            k = g ? [{
                top: 0,
                opacity: 1
            },
            {
                top: -a * l,
                opacity: 1
            }] : [{
                left: 0,
                opacity: 1
            },
            {
                left: -a * l,
                opacity: 1
            }];
            if (/prev/i.test(r)) {
                d.unbind(s);
                for (var j = 0,
                m = l; j < m; j++) {
                    t = p.find(".J_carousel_item:last");
                    t.prependTo(p)
                }
                p.css(k[1]);
                p.animate(k[0], h * l,
                function () {
                    d.bind(s, b._trigger(b, a))
                })
            }
            if (/next/i.test(r)) {
                d.unbind(s);
                p.animate(k[1], h * l,
                function () {
                    p.css(k[0]);
                    for (var v = 0,
                    e = l; v < e; v++) {
                        c = p.find(".J_carousel_item:first");
                        c.appendTo(p)
                    }
                    d.bind(s, b._trigger(b, a))
                })
            }
            return false
        }
    },
    _auto: function () {
        var c = this,
        b = c.options,
        a;
        this.timer = setTimeout(function () {
            a = b.direction ? ".J_carousel_prev" : ".J_carousel_next";
            c.trigger.find(a).trigger(b.etype);
            c._auto()
        },
        b.delay * 1000)
    },
    _stop: function () {
        clearTimeout(this.timer)
    }
};
function Dialog(e, d, f) {
    this.callback = d;
    this.obj = f;
    this.options = $.extend({
        id: "",
        trigger: "",
        header: "",
        title: "",
        body: "",
        footer: "",
        width: 420,
        height: "",
        mask: 1
    },
    e);
    this.ie6 = $.browser.msie && $.browser.version == 6
}
Dialog.prototype = {
    open: function () {
        var b = this.options;
        if (this.dialog === undefined) {
            this._creatDialog();
            this.center(this.dialog)
        } else {
            this.dialog.css({
                visibility: "visible"
            });
            if (b.mask) {
                this._setMaskHeight(this.mask);
                this.mask.css({
                    visibility: "visible"
                })
            }
            if (this.ie6) {
                this._setMaskHeight(this.ifr);
                this.ifr.css({
                    visibility: "visible"
                })
            }
        }
    },
    _creatDialog: function () {
        var g = this,
        f = this.options;
        this.dialog = $('<div class="J_dialog" id=' + f.id + "></div>");
        var j = f.header ? this._creatHTML("J_dialog_hd", f.header) : this._creatHTML("J_dialog_hd", '<h3 class="J_dialog_title">' + f.title + '</h3><a href="javascript:;" class="J_dialog_close">\u5173\u95ed</a>'),
        i = this._creatHTML("J_dialog_bd", f.body),
        h = this._creatHTML("J_dialog_ft", f.footer);
        this.dialog.append('<div class="J_dialog_content" style="width:' + f.width + "px;height:" + f.height + 'px">' + j + i + h + '</div><div class="J_dialog_shadow"></div>').appendTo("body");
        this.dialog.find(".J_dialog_close").live("click",
        function () {
            g.close()
        });
        if (f.mask) {
            this.mask = $(this._creatHTML("J_dialog_mask", ""));
            this._setMaskHeight(this.mask);
            this.mask.appendTo("body");
            if (this.ie6) {
                this.ifr = $('<iframe class="J_dialog_ifr" style="top:0;left:0;width:100%;"></iframe>');
                this.ifr.appendTo("body");
                this._setMaskHeight(this.ifr)
            }
        } else {
            if (this.ie6) {
                this.ifr = $('<iframe class="J_dialog_ifr" style="top:50%;left:50%;"></iframe>');
                f = this.dialog.outerWidth();
                j = this.dialog.outerHeight();
                this.ifr.css({
                    width: f,
                    height: j
                }).appendTo("body");
                this.center(this.ifr)
            }
        }
    },
    _creatHTML: function (d, c) {
        return '<div class="' + d + '">' + c + "</div>"
    },
    setTitle: function (b) {
        this.dialog.find(".J_dialog_title").html(b)
    },
    setHeader: function (b) {
        this.dialog.find(".J_dialog_hd").html(b)
    },
    setBody: function (b) {
        this.dialog.find(".J_dialog_bd").html(b)
    },
    setFooter: function (b) {
        this.dialog.find(".J_dialog_ft").html(b)
    },
    close: function () {
        var b = this.callback;
        $(".J_dialog_mask").css({
            visibility: "hidden",
            height: 0
        });
        this.ie6 && this.ifr.css({
            visibility: "hidden",
            height: 0
        });
        this.dialog.css({
            visibility: "hidden"
        });
        b && b.call(this, this.obj)
    },
    center: function (e) {
        e = e || this.dialog;
        var d = parseInt(e.outerHeight() / 2),
        f = parseInt(e.outerWidth() / 2);
        if (this.ie6) {
            d -= $(window).scrollTop()
        }
        e.css({
            marginTop: -d,
            marginLeft: -f
        })
    },
    setWidth: function (b) {
        this.dialog.find(".J_dialog_content").width(b)
    },
    _setMaskHeight: function (d) {
        var c = $(document).height();
        d.height(c)
    }
};
function ImageZoom(a, c) {
    this.zoom = $(a);
    this.zoomImg = $(a + " img:first");
    this.options = $.extend({
        xzoom: 365,
        yzoom: 390,
        offset: 10,
        zoomW: 800,
        zoomH: 1040,
        url: null
    },
    c);
    this.imgInfo = {};
    this.init()
}
ImageZoom.prototype = {
    init: function () {
        var a = this;
        this.zoom.hover(function () {
            a._pretreat()
        },
        function () {
            a._removeZoom()
        })
    },
    _pretreat: function () {
        function g() {
            setTimeout(function () {
                var a = k[l].largeSrc;
                if (a) {
                    i._addZoom(a, 800, 1040)
                } else {
                    a !== 0 && g()
                }
            },
            10)
        }
        var i = this,
        k = this.imgInfo,
        h = this.options,
        l = this.zoomImg.attr("src"),
        j;
        if (!(l in k)) {
            j = l.replace(new RegExp(h.url[0], "i"), h.url[1]);
            k[l] = {};
            h = new Image;
            h.onload = function () {
                k[l].largeSrc = j
            };
            h.onerror = function () {
                k[l].largeSrc = 0
            };
            h.src = j
        }
        g()
    },
    _addZoom: function (z, v, x) {
        var A = this.zoom,
        y = this.zoomImg,
        w = this.options,
        r = y.attr("src");
        z = z;
        var q, p, o, i, u, s, B;
        q = A.offset().left;
        p = A.offset().top;
        o = y[0].offsetWidth;
        i = y[0].offsetHeight;
        B = q + o + w.offset;
        A.css({
            position: "relative"
        });
        this.alt = y.attr("alt");
        y.attr("alt", "");
        A.next(".J_zoom_div").length !== 0 && A.next(".J_zoom_div").remove();
        A.append('<div class="J_zoom_focus">&nbsp;</div>');
        A.after('<div class="J_zoom_div"><img src="' + r + '" class="J_zoom_small"><img src="' + z + '" class="J_zoom_large"></div>');
        this.zoomDiv = u = A.next("div.J_zoom_div");
        this.zoomFocus = s = A.find("div.J_zoom_focus");
        u.css({
            width: w.xzoom,
            height: w.yzoom,
            left: B,
            top: p,
            overflow: "hidden",
            position: "absolute",
            zIndex: 100,
            border: "1px solid #333",
            opacity: 1,
            display: "none"
        }).stop().fadeIn(500);
        s.css({
            cursor: "move",
            position: "absolute",
            border: "1px solid #E4E4E4",
            zIndex: 10,
            opacity: 0.5,
            backgroundColor: "#FFF"
        });
        z = u.find(".J_zoom_large");
        u.find(".J_zoom_small").css({
            position: "absolute",
            top: 0,
            left: 0,
            zIndex: 1,
            width: v,
            height: x
        });
        z.css({
            position: "absolute",
            top: 0,
            left: 0,
            zIndex: 2,
            width: v,
            height: x
        });
        A.bind("mousemove",
        function (c) {
            var d = c.pageX;
            c = c.pageY;
            var b = v / o,
            a = x / i;
            s.css({
                width: w.xzoom / b,
                height: w.yzoom / a
            });
            var h = s.width(),
            g = s.height(),
            f = d - h / 2 - q,
            e = c - g / 2 - p;
            f = d - h / 2 < q ? 0 : d + h / 2 > o + q ? o - h - 2 : f;
            e = c - g / 2 < p ? 0 : c + g / 2 > i + p ? i - g - 2 : e;
            s.css({
                top: e,
                left: f
            });
            u[0].scrollTop = e * a;
            u[0].scrollLeft = f * b
        })
    },
    _removeZoom: function () {
        this.zoom.unbind("mousemove");
        if (this.zoomDiv) {
            this.zoomDiv.stop().fadeOut(400,
            function () {
                $(this).remove()
            });
            this.zoomFocus.remove()
        }
        this.zoomImg.attr("alt", this.alt)
    }
};
if (typeof deconcept == "undefined") {
    var deconcept = new Object()
}
if (typeof deconcept.util == "undefined") {
    deconcept.util = new Object()
}
if (typeof deconcept.SWFObjectUtil == "undefined") {
    deconcept.SWFObjectUtil = new Object()
}
deconcept.SWFObject = function (m, b, n, e, j, k, g, f, d, l) {
    if (!document.getElementById) {
        return
    }
    this.DETECT_KEY = l ? l : "detectflash";
    this.skipDetect = deconcept.util.getRequestParameter(this.DETECT_KEY);
    this.params = new Object();
    this.variables = new Object();
    this.attributes = new Array();
    if (m) {
        this.setAttribute("swf", m)
    }
    if (b) {
        this.setAttribute("id", b)
    }
    if (n) {
        this.setAttribute("width", n)
    }
    if (e) {
        this.setAttribute("height", e)
    }
    if (j) {
        this.setAttribute("version", new deconcept.PlayerVersion(j.toString().split(".")))
    }
    this.installedVer = deconcept.SWFObjectUtil.getPlayerVersion();
    if (!window.opera && document.all && this.installedVer.major > 7) {
        deconcept.SWFObject.doPrepUnload = true
    }
    if (k) {
        this.addParam("bgcolor", k)
    }
    var a = g ? g : "high";
    this.addParam("quality", a);
    this.setAttribute("useExpressInstall", false);
    this.setAttribute("doExpressInstall", false);
    var i = (f) ? f : window.location;
    this.setAttribute("xiRedirectUrl", i);
    this.setAttribute("redirectUrl", "");
    if (d) {
        this.setAttribute("redirectUrl", d)
    }
};
deconcept.SWFObject.prototype = {
    useExpressInstall: function (a) {
        this.xiSWFPath = !a ? "expressinstall.swf" : a;
        this.setAttribute("useExpressInstall", true)
    },
    setAttribute: function (a, b) {
        this.attributes[a] = b
    },
    getAttribute: function (a) {
        return this.attributes[a]
    },
    addParam: function (b, a) {
        this.params[b] = a
    },
    getParams: function () {
        return this.params
    },
    addVariable: function (b, a) {
        this.variables[b] = a
    },
    getVariable: function (a) {
        return this.variables[a]
    },
    getVariables: function () {
        return this.variables
    },
    getVariablePairs: function () {
        var c = new Array();
        var b;
        var a = this.getVariables();
        for (b in a) {
            c[c.length] = b + "=" + a[b]
        }
        return c
    },
    getSWFHTML: function () {
        var b = "";
        if (navigator.plugins && navigator.mimeTypes && navigator.mimeTypes.length) {
            if (this.getAttribute("doExpressInstall")) {
                this.addVariable("MMplayerType", "PlugIn");
                this.setAttribute("swf", this.xiSWFPath)
            }
            b = '<embed type="application/x-shockwave-flash" src="' + this.getAttribute("swf") + '" width="' + this.getAttribute("width") + '" height="' + this.getAttribute("height") + '" style="' + this.getAttribute("style") + '"';
            b += ' id="' + this.getAttribute("id") + '" name="' + this.getAttribute("id") + '" ';
            var f = this.getParams();
            for (var e in f) {
                b += [e] + '="' + f[e] + '" '
            }
            var d = this.getVariablePairs().join("&");
            if (d.length > 0) {
                b += 'flashvars="' + d + '"'
            }
            b += "/>"
        } else {
            if (this.getAttribute("doExpressInstall")) {
                this.addVariable("MMplayerType", "ActiveX");
                this.setAttribute("swf", this.xiSWFPath)
            }
            b = '<object id="' + this.getAttribute("id") + '" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="' + this.getAttribute("width") + '" height="' + this.getAttribute("height") + '" style="' + this.getAttribute("style") + '">';
            b += '<param name="movie" value="' + this.getAttribute("swf") + '" />';
            var c = this.getParams();
            for (var e in c) {
                b += '<param name="' + e + '" value="' + c[e] + '" />'
            }
            var a = this.getVariablePairs().join("&");
            if (a.length > 0) {
                b += '<param name="flashvars" value="' + a + '" />'
            }
            b += "</object>"
        }
        return b
    },
    write: function (b) {
        if (this.getAttribute("useExpressInstall")) {
            var a = new deconcept.PlayerVersion([6, 0, 65]);
            if (this.installedVer.versionIsValid(a) && !this.installedVer.versionIsValid(this.getAttribute("version"))) {
                this.setAttribute("doExpressInstall", true);
                this.addVariable("MMredirectURL", escape(this.getAttribute("xiRedirectUrl")));
                document.title = document.title.slice(0, 47) + " - Flash Player Installation";
                this.addVariable("MMdoctitle", document.title)
            }
        }
        if (this.skipDetect || this.getAttribute("doExpressInstall") || this.installedVer.versionIsValid(this.getAttribute("version"))) {
            var c = (typeof b == "string") ? document.getElementById(b) : b;
            c.innerHTML = this.getSWFHTML();
            return true
        } else {
            if (this.getAttribute("redirectUrl") != "") {
                document.location.replace(this.getAttribute("redirectUrl"))
            }
        }
        return false
    }
};
deconcept.SWFObjectUtil.getPlayerVersion = function () {
    var f = new deconcept.PlayerVersion([0, 0, 0]);
    if (navigator.plugins && navigator.mimeTypes.length) {
        var a = navigator.plugins["Shockwave Flash"];
        if (a && a.description) {
            f = new deconcept.PlayerVersion(a.description.replace(/([a-zA-Z]|\s)+/, "").replace(/(\s+r|\s+b[0-9]+)/, ".").split("."))
        }
    } else {
        if (navigator.userAgent && navigator.userAgent.indexOf("Windows CE") >= 0) {
            var b = 1;
            var c = 3;
            while (b) {
                try {
                    c++;
                    b = new ActiveXObject("ShockwaveFlash.ShockwaveFlash." + c);
                    f = new deconcept.PlayerVersion([c, 0, 0])
                } catch (d) {
                    b = null
                }
            }
        } else {
            try {
                var b = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7")
            } catch (d) {
                try {
                    var b = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");
                    f = new deconcept.PlayerVersion([6, 0, 21]);
                    b.AllowScriptAccess = "always"
                } catch (d) {
                    if (f.major == 6) {
                        return f
                    }
                }
                try {
                    b = new ActiveXObject("ShockwaveFlash.ShockwaveFlash")
                } catch (d) { }
            }
            if (b != null) {
                f = new deconcept.PlayerVersion(b.GetVariable("$version").split(" ")[1].split(","))
            }
        }
    }
    return f
};
deconcept.PlayerVersion = function (a) {
    this.major = a[0] != null ? parseInt(a[0]) : 0;
    this.minor = a[1] != null ? parseInt(a[1]) : 0;
    this.rev = a[2] != null ? parseInt(a[2]) : 0
};
deconcept.PlayerVersion.prototype.versionIsValid = function (a) {
    if (this.major < a.major) {
        return false
    }
    if (this.major > a.major) {
        return true
    }
    if (this.minor < a.minor) {
        return false
    }
    if (this.minor > a.minor) {
        return true
    }
    if (this.rev < a.rev) {
        return false
    }
    return true
};
deconcept.util = {
    getRequestParameter: function (c) {
        var d = document.location.search || document.location.hash;
        if (c == null) {
            return d
        }
        if (d) {
            var b = d.substring(1).split("&");
            for (var a = 0; a < b.length; a++) {
                if (b[a].substring(0, b[a].indexOf("=")) == c) {
                    return b[a].substring((b[a].indexOf("=") + 1))
                }
            }
        }
        return ""
    }
};
deconcept.SWFObjectUtil.cleanupSWFs = function () {
    var b = document.getElementsByTagName("OBJECT");
    for (var c = b.length - 1; c >= 0; c--) {
        b[c].style.display = "none";
        for (var a in b[c]) {
            if (typeof b[c][a] == "function") {
                b[c][a] = function () { }
            }
        }
    }
};
if (deconcept.SWFObject.doPrepUnload) {
    if (!deconcept.unloadSet) {
        deconcept.SWFObjectUtil.prepUnload = function () {
            __flash_unloadHandler = function () { };
            __flash_savedUnloadHandler = function () { };
            window.attachEvent("onunload", deconcept.SWFObjectUtil.cleanupSWFs)
        };
        window.attachEvent("onbeforeunload", deconcept.SWFObjectUtil.prepUnload);
        deconcept.unloadSet = true
    }
}
if (!document.getElementById && document.all) {
    document.getElementById = function (a) {
        return document.all[a]
    }
}
var getQueryParamValue = deconcept.util.getRequestParameter;
var FlashObject = deconcept.SWFObject;
var SWFObject = deconcept.SWFObject;
function Tab(a, b, c) {
    this.container = $(a);
    this.handle = $(a + " .J_tab_nav li");
    this.panel = $(a + " .J_tab_panel");
    this.count = this.handle.length;
    this.timer = null;
    this.eTime = null;
    this.options = $.extend({
        auto: false,
        delay: 4,
        event: "mouseover",
        index: 1
    },
    b);
    this.init();
    if (c) {
        c.call(this)
    }
}
Tab.prototype = {
    init: function () {
        var b = this,
        a = this.count,
        d = this.options,
        c = !!d.auto;
        this.handle.bind(d.event,
        function () {
            b._trigger(this)
        });
        if (d.index === "r") {
            d.index = this._random(a)
        }
        this._show(d.index);
        if (c) {
            this._auto();
            this.container.hover(function () {
                b._stop()
            },
            function () {
                b._auto()
            })
        }
    },
    _random: function (a) {
        return parseInt(Math.random() * a + 1)
    },
    _trigger: function (c) {
        var a, d = this.options,
        b = this.handle;
        if (d.index === (b.index(c) + 1)) {
            return
        }
        a = d.index = b.index(c) + 1;
        this._show(a)
    },
    _show: function (a) {
        this.handle.removeClass("cur").eq(a - 1).addClass("cur");
        this.panel.addClass("none").eq(a - 1).removeClass("none")
    },
    _auto: function () {
        var a = this,
        b = a.options;
        this.timer = setTimeout(function () {
            b.index = b.index < a.count ? ++b.index : 1;
            a._show(b.index);
            a._auto()
        },
        b.delay * 1000)
    },
    _stop: function () {
        clearTimeout(this.timer)
    }
}; (function () {
    $.fn.popup = function (a) {
        var b = {
            close: "false",
            callback: ""
        };
        var a = $.extend(b, a);
        this.each(function () {
            var f = $(this);
            var d = $(this).find(".dclose");
            var e = jQuery("<div class='mask'></div>");
            var c = {
                dclose: function () {
                    $("div.mask").remove();
                    f.hide();
                    if (a.callback) {
                        a.callback()
                    }
                },
                dcontent: function () {
                    e.appendTo(document.body);
                    e.css({
                        display: "block",
                        opacity: ".30",
                        zIndex: "100",
                        filter: "Alpha(Opacity=30)",
                        background: "#000",
                        position: "absolute",
                        top: "0",
                        left: "0",
                        width: "100%",
                        height: "100%"
                    }).width($("body").width()).height($(document).height());
                    var g = f.outerWidth() / 2;
                    var h = f.outerHeight() / 2 - $(window).scrollTop();
                    f.show().css({
                        marginLeft: -g,
                        marginTop: -h,
                        top: "50%",
                        zIndex: "101",
                        position: "absolute",
                        left: "50%"
                    })
                }
            };
            if (a.close == true) {
                c.dclose();
                return false
            }
            c.dcontent();
            d.click(function () {
                c.dclose();
                return false
            });
            e.click(function () {
                c.dclose()
            });
            $(window).resize(function () {
                e.width($("body").width()).height($(document).height())
            });
            return false
        })
    }
})(jQuery);