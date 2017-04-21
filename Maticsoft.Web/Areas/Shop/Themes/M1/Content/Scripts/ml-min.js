(function (r, i) {
    var s = {}, l = {}, o = {}, j = { autoLoad: true, coreLib: ['404.js'], mods: {} },
        m = function () { var a = i.getElementsByTagName("script"); return a[a.length - 1] }(),
        t = function (a) {
            var b = j.mods;
            return typeof a === "string" ? b[a] ? b[a] : { path: a } : a
        },
        p = function (a, b, d, e) {
            var c, k, g, h = function ()
            {
                s[a] = 1; e && e(a); e = null
            };
            if (a)
                if (s[a]) {
            o[a] = false; e && e(a)
                } else if (o[a])
                    setTimeout(function () { p(a, b, d, e) }, 10);
               else {
                    o[a] = true; k = b || "js"; if (k === "js") {
                        c = i.createElement("script");
                        c.setAttribute("type", "text/javascript");
                        c.setAttribute("src", a);
                        c.setAttribute("async", true)
                    } else if (k === "css")
                    {
                        c = i.createElement("link"); c.setAttribute("type", "text/css");
                        c.setAttribute("rel", "stylesheet");
                        c.setAttribute("href", a)
                    }
                    if (d) c.charset = d;
                    if (k === "css")
                    {
                        g = new Image;
                        g.onerror = function () {
                            h(); g = g.onerror = null
                        };
                        g.src = a
                    } else {
                        c.onerror = function () {
                            h(); c.onerror = null
                        };
                        c.onload = c.onreadystatechange = function () {
                            if (!this.readyState || this.readyState === "loaded" || this.readyState === "complete")
                            {
                                h(); c.onload = c.onreadystatechange = null
                            }
                        }
                    }
                    m.parentNode.insertBefore(c, m)
        } 
        },
    q = function (a, b) {
        function d() { if (! --g) { l[e] = 1; b() } } var e, c, k = 0, g;
        e = a.join(""); g = a.length; if (l[e]) b(); else for (; c = a[k++];)
        {
            c = t(c);
            c.requires ?
            q(c.requires, function (h)
            {
                    return function () {
                        p(h.path, h.type, h.charset, d)
                    }
            }(c)) : p(c.path, c.type, c.charset, d)
        }
    },
    f = function () {
        var a = [].slice.call(arguments), b, d;
        if (j.autoLoad && !l[j.coreLib.join("")]) q(j.coreLib, function () { f.apply(null, a) });
        else {
            if (typeof a[a.length - 1] === "function") b = a.pop();
            d = a.join("");
            (a.length === 0 || l[d]) && b ? b() : q(a, function () { l[d] = 1; b && b() })
        }
    }; f.add = function (a, b) { if (!a || !b || !b.path) return f; j.mods[a] = b; return f };
    f.delay = function () {
        var a = [].slice.call(arguments), b = a.shift();
        r.setTimeout(function () { f.apply(this, a) }, b)
    };
    f.css = function (a) {
        var b = i.getElementById("do-inline-css");
        if (!b) {
            b = i.createElement("style"); b.type = "text/css"; b.id = "do-inline-css";
            m.parentNode.insertBefore(n,m)
        } if (b.styleSheet)
            b.styleSheet.cssText += a;
        else
            b.appendChild(i.createTextNode(a))
    };
    f.setConfig = function (a, b) { j[a] = b; return f };
    r.ML = f
})(window, document);
