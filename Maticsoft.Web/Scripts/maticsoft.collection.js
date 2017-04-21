(function () {
    var hostname = "http://localhost:8050";
    function m(a, b, c) {
        a.attachEvent ? (a["e" + b + c] = c, a[b + c] = function () {
            a["e" + b + c](window.event);
        },
        a.attachEvent("on" + b, a[b + c])) : a.addEventListener(b, c, !1);
    }
    function v(a, b) {
        return a.className.match(RegExp("(\\s|^)" + b + "(\\s|$)"));
    }
    function s(a, b) {
        v(a, b) || (a.className += " " + b);
    }
    function t(a, b) {
        if (v(a, b)) a.className = a.className.replace(RegExp("(\\s|^)" + b + "(\\s|$)"), " ");
    }
    var k = !!window.ActiveXObject,
    w = k && !window.XMLHttpRequest,
    i = function (a) {
        for (var a = a.split(","), b = a.length, c = [], g = 0; g < b; g++) {
            var d = document.getElementById(a[g]);
            d && c.push(d);
        }
        return c;
    },
    h = function (a, b) {
        if (document.getElementsByClassName) return (b || document).getElementsByClassName(a);
        else {
            b = b || document;
            tag = "*";
            for (var c = [], g = tag === "*" && b.all ? b.all : b.getElementsByTagName(tag), d = g.length, a = a.replace(/\-/g, "\\-"), e = RegExp("(^|\\s)" + a + "(\\s|$)"); --d >= 0; ) e.test(g[d].className) && c.push(g[d]);
            return c;
        }
    },
    p = function () {
        for (var a = i("maticsoftShareBg,maticsoftShareToolBar,maticsoftShareBtn,maticsoftShareContent,maticsoftShareScript,maticsoftShareStyle"), b = a.length, c = 0; c < b; c++) {
            var g = a[c],
            d = g.parentNode;
            d && d.removeChild(g);
        }
    };
    if (i("maticsoftShareToolBar").length != 0 || i("maticsoftShareBtn").length != 0) p();
    else {
        var x = location.hostname;
        var reCat = new RegExp(x, "i");
        if (reCat.test(hostname)) alert("您就在本站，不能采集本站的图片");
        else {
            var y = function () {
                for (var a = [{
                    name: "duitang",
                    r: /duitang.com/i
                },
                {
                    name: "meilishuo",
                    r: /meilishuo.com/i
                },
                {
                    name: "huaban",
                    r: /huaban.com/i
                },
                {
                    name: "pinterest",
                    r: /pinterest.com/i
                }], b = 0; b < a.length; b++) if (a[b].r.test(x)) return a[b].name;
                return !1;
            };
            if (function (a) {
                var b = /tmall.com/i,
                c = /auction\d?.paipai.com/i,
                g = /buy.caomeipai.com\/goods/i,
                d = /www.360buy.com\/product/i,
                e = /product.dangdang.com\/Product.aspx\?product_id=/i,
                h = /book.360buy.com/i,
                i = /www.vancl.com\/StyleDetail/i,
                j = /www.vancl.com\/Product/i,
                k = /vt.vancl.com\/item/i,
                l = /item.vancl.com\/\d+/i,
                m = /mbaobao.com\/pshow/i,
                n = /[www|us].topshop.com\/webapp\/wcs\/stores\/servlet\/ProductDisplay/i,
                o = /quwan.com\/goods/i,
                p = /nala.com.cn\/product/i,
                q = /maymay.cn\/pitem/i,
                r = /asos.com/i;
                return /item(.lp)?.taobao.com\/(.?)[item.htm|item_num_id|item_detail|itemID|item_id|default_item_id]/i.test(a) || b.test(a) || h.test(a) || d.test(a) || c.test(a) || g.test(a) || e.test(a) || i.test(a) || j.test(a) || k.test(a) || l.test(a) || m.test(a) || n.test(a) || o.test(a) || p.test(a) || q.test(a) || r.test(a);
            } (location.href)) {
                var l = "#maticsoftShareBg {background-color:#f2f2f2; height:100%; width:100%; left:0px; top:0px; zoom:1; position:fixed; z-index:100000; opacity:0.8; FILTER:alpha(opacity=80); } #maticsoftShareBtn{position:absolute;top:50%;left:50%;width:480px;height:160px;margin:-80px 0 0 -240px;z-index: 100001;background:url(" + hostname + "/images/publish.gif) no-repeat;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:80px;top:79px;display:block;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:255px;top:79px;display:block;} #maticsoftShareBtn .maticsoftCancel{height:38px;width:80px;position:absolute;top:0;right:0;}",
                z = 'body{background-attachment:fixed; background-image:url("about:blank");}#maticsoftShareBg {background-color:#f2f2f2; height:expression(document.body.clientHeight); width:100%; left:0px; zoom:1; z-index:100000; FILTER:alpha(opacity=80); position:absolute; top:expression(document.compatMode && document.compatMode=="CSS1Compat" ? documentElement.scrollTop:document.body.scrollTop ); }  #maticsoftShareBtn{position:absolute;top:50%;left:50%;width:480px;height:160px;margin:-80px 0 0 -240px;z-index: 100001; background:url(' + hostname + '/images/publish.gif) no-repeat;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:80px;top:79px;display:block;} #maticsoftShareBtn .maticsoftPub{height:37px;width:144px;position:absolute;left:255px;top:79px;display:block;} #maticsoftShareBtn .maticsoftCancel{height:38px;width:80px;position:absolute;top:0;right:0;}',
                q = '<div id="maticsoftShareBg"></div><div id="maticsoftShareBtn"><a class="maticsoftPub" href="javascript:;"></a><a class="maticsoftPub" href="javascript:;"></a><a class="maticsoftCancel" href="javascript:;"></a></div>',
                n = document.createElement("div");
                n.innerHTML = q;
                document.body.appendChild(n);
                k ? (f = document.createElement("style"), f.type = "text/css", f.media = "screen", f.id = "maticsoftShareStyle", f.styleSheet.cssText = w ? z : l, document.getElementsByTagName("head")[0].appendChild(f)) : (f = document.createElement("style"), f.id = "maticsoftShareStyle", f.innerHTML = l, document.body.appendChild(f));
                window.scrollTo(0, 0);
                var k = h("maticsoftPub", i("maticsoftShareToolBar")[0]),
                // l = h("maticsoftPub", i("maticsoftShareToolBar")[0]),
                B = function (a) {
                    var b = [];
                    b.push(a);
                    b.push("?");
                    b.push("type=goods&");
                    b.push("goods=" + encodeURIComponent(location.href));
                    return b.join("");
                };

                k = h("maticsoftCancel", i("maticsoftShareToolBar")[0], "a");
                m(k[0], "click",
                function () {
                    p();
                })
            } else {
                var l = "#maticsoftShareBg {background-color:#f2f2f2; height:100%; width:100%; left:0px; top:0px; zoom:1; position:fixed; z-index:100000; opacity:0.8; FILTER:alpha(opacity=80); } #maticsoftShareContent {position:absolute; top:66px; left:0; z-index:100001; } #maticsoftShareContent .mgsFeed {width:200px; height:200px; border-right:1px solid #e7e7e7; border-bottom:1px solid #e7e7e7; float:left; cursor:pointer; text-align:center; background-color:#FFF; overflow:hidden; position:relative; } #maticsoftShareContent .mgsPic {max-height:200px; max-width:200px; } #maticsoftShareContent .mgsSize {position:absolute; bottom:5px; left:0; width:200px; text-align:center; } #maticsoftShareContent .mgsSize span {display:inline-block; background-color:#FFF; border-radius:4px; padding:0 2px; } #maticsoftShareContent .mgsSelect {position:absolute; right:12px; bottom:10px; width:28px; height:28px; background:url(" + hostname + "/images/select.png) 0 0 no-repeat; }  #maticsoftShareContent .selected {background-position:0 -50px;} #maticsoftShareToolBar {position:fixed; top:0; left:0; z-index:100002; height:75px; width:100%; overflow:hidden; background:url(" + hostname + "/images/mgs_bar_bg.png) top repeat-x; } #maticsoftShareToolBar .maticsoftShadow {position:absolute; width:100%; height:9px; overflow:hidden; top:65px; left:0; background:url(" + hostname + "/images/mgs_bar_bg_sd.png) repeat-x; } #maticsoftShareToolBar .maticsoftLogo {position:absolute; right:25px; top:15px; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:25px; top:8px; width:156px; height:49px; background:url(" + hostname + "/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:190px; top:8px; width:156px; height:49px; background:url(" + hostname + "/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftCancel {position:absolute; right:25px; top:16px; width:69px; height:31px; background:url(" + hostname + "/images/cancel.png) no-repeat; }#maticsoftShareToolBar .maticsoftNotice{position: absolute;font-size:14px;top:23px;left:360px;color:#555}",
                z = 'body{background-attachment:fixed; background-image:url("about:blank");}#maticsoftShareBg {background-color:#f2f2f2; height:expression(document.body.clientHeight); width:100%; left:0px; zoom:1; z-index:100000; FILTER:alpha(opacity=80); position:absolute; top:expression(document.compatMode && document.compatMode=="CSS1Compat" ? documentElement.scrollTop:document.body.scrollTop ); } #maticsoftShareContent {position:absolute; top:66px; left:0; z-index:100001; } #maticsoftShareContent .mgsFeed {width:200px; height:200px; border-right:1px solid #e7e7e7; border-bottom:1px solid #e7e7e7; float:left; cursor:pointer; text-align:center; background-color:#FFF; overflow:hidden; position:relative; } #maticsoftShareContent .mgsPic {max-height:200px; max-width:200px; } #maticsoftShareContent .mgsSize {position:absolute; bottom:5px; left:0; width:200px; text-align:center; } #maticsoftShareContent .mgsSize span {display:inline-block; background-color:#FFF; border-radius:4px; padding:0 2px; } #maticsoftShareContent .mgsSelect {position:absolute; right:12px; bottom:10px; width:28px; height:28px; background:url(' + hostname + '/images/select.png) 0 0 no-repeat; }  #maticsoftShareContent .selected {background-position:0 -50px;} #maticsoftShareToolBar {position:absolute; top:expression(document.compatMode && document.compatMode=="CSS1Compat" ? documentElement.scrollTop:document.body.scrollTop ); left:0; z-index:100002; height:75px; width:100%; overflow:hidden; background:url(' + hostname + '/images/mgs_bar_bg.png) top repeat-x; } #maticsoftShareToolBar .maticsoftShadow {position:absolute; width:100%; height:9px; overflow:hidden; top:65px; left:0; FILTER:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="' + hostname + '/images/mgs_bar_bg_sd.png",sizingMethod="scale"); background-image:none } #maticsoftShareToolBar .maticsoftLogo {position:absolute; right:25px; top:15px; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:25px; top:8px; width:156px; height:49px; background:url(' + hostname + '/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftPub {position:absolute; left:190px; top:8px; width:156px; height:49px; background:url(' + hostname + '/images/publish.gif) no-repeat; } #maticsoftShareToolBar .maticsoftCancel {position:absolute; right:25px; top:16px; width:69px; height:31px; background:url(' + hostname + '/images/cancel.png) no-repeat; }#maticsoftShareToolBar .maticsoftNotice{position: absolute;font-size:14px;top:23px;left:360px;color:#555}',
                q = '<div class="mgsFeed"><img class="mgsPic" alt="{alt}" style="{style}" src="{picUrl}" osrc="{opicUrl}" ><div class="mgsSize"><span>{width}x{height}</span></div><i class="mgsSelect"></i></div>',
                n = [],
                u = y();
                u && (q = '<div class="mgsFeed"><img class="mgsPic" alt="{alt}" style="{style}" src="{picUrl}" osrc="{opicUrl}"><div class="mgsSize"></div><i class="mgsSelect"></i></div>');
                for (var y = function (a) {
                    var b = new Image;
                    b.src = a.src;
                    var c = b.height,
                    g = b.width,
                    d = b.src,
                    b = b.src,
                    a = a.alt;
                    if (u) {
                        var e = b = d;
                        switch (u) {
                            case "duitang":
                                e = /\.thumb.200_0\./;
                                e = b.replace(e, ".");
                                break;
                            case "meilishuo":
                                e = /\/pic\/r\//;
                                e = b.replace(e, "/pic/_o/");
                                break;
                            case "huaban":
                                e = /_fw192|_fw554/;
                                e = b.replace(e, "");
                                break;
                            case "pinterest":
                                e = /_b\.|_c\./,
                                e = b.replace(e, ".");
                        }
                        b = e;
                        u == "huaban" && (d = b);
                    }
                    return {
                        w: g,
                        h: c,
                        src: d,
                        osrc: b,
                        alt: a
                    };
                },
                E = function (a) {
                    var b = "";
                    w && (b += "width:" + a.w + "px;height:" + a.h + "px;");
                    Math.max(a.h, a.w) > 199 ? a.h < a.w && (b += "margin-top: " + parseInt(100 - 100 * (a.h / a.w)) + "px;") : b += "margin-top: " + parseInt(100 - a.h / 2) + "px;";
                    return b;
                },
                o = 0; o < document.images.length; o++) {
                    var j = document.images[o],
                    j = y(j);
                    if (j.w > 80 && j.h > 80 && (j.h > 109 || j.w > 109))
                        j = q.replace(/{style}/, E(j)).replace(/{picUrl}/, j.src).replace(/{opicUrl}/, j.osrc).replace(/{width}/, j.w).replace(/{height}/, j.h).replace(/{alt}/, j.alt),
                        n.push(j);
                }
                q = '<div id="maticsoftShareBg"></div><div id="maticsoftShareToolBar"><a class="maticsoftPub" href="javascript:;"></a><a class="maticsoftCancel" href="javascript:;"></a><span class="maticsoftNotice"><span class="maticsoftNoticeText" >\u8bf7\u9009\u62e9\u8981\u53d1\u8868\u7684\u56fe\u7247\uff08\u53ef\u591a\u9009\uff09</span><b style="margin:0 5px;" ><input id="select_all" type="checkbox" />\u5168\u9009</b></span><div class="maticsoftShadow"></div></div>' + '<div id="maticsoftShareContent">{content}</div>'.replace(/{content}/, n.join(""));
                n = document.createElement("div");
                n.innerHTML = q;
                document.body.appendChild(n);
                k ? (f = document.createElement("style"), f.type = "text/css", f.media = "screen", f.id = "maticsoftShareStyle", f.styleSheet.cssText = w ? z : l, document.getElementsByTagName("head")[0].appendChild(f)) : (f = document.createElement("style"), f.id = "maticsoftShareStyle", f.innerHTML = l, document.body.appendChild(f));
                window.scrollTo(0, 0);
                k = h("maticsoftCancel", i("maticsoftShareToolBar")[0], "a");
                m(k[0], "click",
                function () {
                    p();
                });
                for (var r = h("mgsFeed", i("maticsoftShareContent")[0]), A = r.length, o = 0; o < A; o++) m(r[o], "click",
                function () {
                    if (v(this, "checked")) {
                        var a = h("mgsSelect", this);
                        t(a[0], "selected");
                        t(this, "checked");
                    } else s(this, "checked"),
                    a = h("mgsSelect", this),
                    s(a[0], "selected");
                    C();
                });
                var C = function () {
                    var a = h("checked", i("maticsoftShareContent")[0]).length;
                    h("maticsoftNoticeText", i("maticsoftShareToolBar")[0])[0].innerHTML = a == 0 ? "\u8bf7\u9009\u62e9\u8981\u53d1\u8868\u7684\u56fe\u7247\uff08\u53ef\u591a\u9009\uff09" : '\u5df2\u9009\u62e9<em style="color:#690;font-weight: bold;padding:0 2px;">' + a + "</em>\u5f20\u56fe\u7247";
                };
                m(i("select_all")[0], "click",
                function () {
                    if (this.checked) for (a = 0; a < A; a++) s(r[a], "checked"),
                    b = h("mgsSelect", r[a]),
                    s(b[0], "selected");
                    else for (var a = 0; a < A; a++) {
                        var b = h("mgsSelect", r[a]);
                        t(b[0], "selected");
                        t(r[a], "checked");
                    }
                    C();
                });
                var k = h("maticsoftPub", i("maticsoftShareToolBar")[0]),
                D = function (a) {
                    var b = [];
                    b.push(a);
                    b.push("?");
                    var a = h("checked", i("maticsoftShareContent")[0]),
                    c = a.length;
                    if (c < 1)
                        alert("\u8bf7\u9009\u62e9\u81f3\u5c11\u4e00\u5f20\u56fe\u7247\u3002");
                    else if (c > 10) alert("一次最多只能分享10张");
                    else {
                        for (var g = 0; g < c; g++) {
                            var d = h("mgsPic", a[g]),
                            e = d[0].getAttribute("osrc"),
                            d = d[0].alt;
                            b.push("pics[]=" + encodeURIComponent(e) + "----" + d + "&");
                        }
                        b.push("type=img");
                        window.open(b.join(""), "maticsoftShare" + (new Date).getTime(), "status=no,resizable=no,scrollbars=yes,personalbar=no,directories=no,location=no,toolbar=no,menubar=no,left=0,top=0");
                        p();
                    }

                };
                m(k[0], "click",
                function () {
                    D(hostname + "/home/ShareImage/");

                });
                m(l[0], "click",
                function () {
                    D(hostname + "/home/ShareImage/");
                });
            }
        }
    }
})();