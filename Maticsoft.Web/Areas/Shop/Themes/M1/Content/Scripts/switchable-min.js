function Switchable(a, b, c) { this.container = $(a); this.config = $.extend({}, Switchable.defaultConfig, b); this.callback = c; this.activeIndex = this.config.activeIndex; this.firstSign = 1; this._init() } Switchable.defaultConfig = { navCls: "j-sw-nav", contentCls: "j-sw-c", activeTriggerCls: "cur", hasTriggers: true, triggerType: "mouse", delay: 0.1, interval: 5, duration: 0.5, effect: "none", autoplay: false, pauseOnHover: true, activeIndex: 0, random: false, steps: 1, viewSize: [], lazyDataType: "img" };
Switchable.prototype = { _init: function () { var a = this.config; this._preProcess(); a.hasTriggers && this._bindTriggers(); this.setEffect(); this.switchTo(a.random && this._random(this.panels.length) || a.switchTo && a.switchTo - 1 || this.activeIndex); a.autoplay && this.setAutoPlay(); this instanceof Carousel && Carousel.init(this); this.firstSign = null }, _preProcess: function () {
    var a = this.container, b = this.config, c; c = c = null; if (c = $("." + b.navCls, a)) this.triggers = c.children(); else b.hasTriggers = false; this.content = c = $("." + b.contentCls,
a); this.panels = c = c.children(); this.length = Math.ceil(c.length / b.steps)
}, _random: function (a) { return parseInt(Math.random() * a) }, _bindTriggers: function () { var a = this, b = a.config.triggerType, c = a.triggers, e = c.length, d, f; for (f = 0; f < e; f++) (function (g) { d = c.eq(g); d.click(function () { a._onFocus(g) }); b === "mouse" && d.hover(function () { a._onMouseEnter(g) }, function () { a._onMouseLeave(g) }) })(f) }, _onFocus: function (a) { if (this._triggerIsValid(a)) { this._cancelSwitchTimer(); this.switchTo(a) } }, _onMouseEnter: function (a) {
    var b =
this; if (b._triggerIsValid(a)) b.switchTimer = setTimeout(function () { b.switchTo(a) }, b.config.delay * 1E3)
}, _onMouseLeave: function () { this._cancelSwitchTimer() }, _triggerIsValid: function (a) { return this.activeIndex !== a }, _cancelSwitchTimer: function () { if (this.switchTimer) { clearTimeout(this.switchTimer); this.switchTimer = undefined } }, switchTo: function (a, b) {
    var c = this.config, e = this.callback, d = this.triggers, f = this.panels, g = this.activeIndex, h = c.steps, i = g * h, j = a * h; if (!this.firstSign && !this._triggerIsValid(a)) return this;
    this._lazyload(a); if (e && e.beforeSwitch && e.beforeSwitch(a, this) === false) return this; if (c.hasTriggers) this._switchTrigger(g > -1 ? d.eq(g) : null, d.eq(a)); if (b === undefined) b = a > g ? "forward" : "backward"; this._switchView(f.slice(i, i + h), f.slice(j, j + h), a, b); this.activeIndex = a; return this
}, _switchTrigger: function (a, b) { var c = this.config.activeTriggerCls; a && a.removeClass(c); b.addClass(c) }, _switchView: function (a, b, c, e) {
    var d = this, f = d.config, g = Switchable.Effect[f.effect]; if (f.circular && (f.effect === "scrollx" || f.effect ===
"scrolly")) { f.scrollType = f.effect; g = Switchable.Effect.circularScroll } g.call(d, a, b, function () { d._fireOnSwitch(c) }, c, e)
}, _lazyload: function (a) { function b() { var e, d, f; e = c.config.lazyDataType; var g = e === "img"; if (e = g ? "img" : e === "area" ? "textarea" : "") { e = $(e, c.container); d = 0; for (f = e.length; d < f; d++) if (g ? e[d].getAttribute("data-lazy-custom") : e.eq(d).hasClass("lazy-area-custom")) return false } return true } var c = this; if (b()) c._lazyload = function () { }; Switchable.LazyLoad(a, c) }, _fireOnSwitch: function (a) {
    var b = this.callback;
    !this.config.circular && this instanceof Carousel && Carousel.noCircular(a, this); b && b.onSwitch && b.onSwitch(a, this)
}, prev: function () { var a = this.activeIndex; this.switchTo(a > 0 ? a - 1 : this.length - 1, "backward") }, next: function () { var a = this.activeIndex; this.switchTo(a < this.length - 1 ? a + 1 : 0, "forward") }, setAutoPlay: function () {
    function a() { b.timer = setInterval(function () { b.paused || b.switchTo(b.activeIndex < b.length - 1 ? b.activeIndex + 1 : 0, "forward") }, c.interval * 1E3) } var b = this, c = b.config; c.pauseOnHover && b.container.hover(function () {
        b.stop();
        b.paused = true
    }, function () { b.paused = false; a() }); a()
}, stop: function () { if (this.timer) { clearInterval(this.timer); this.timer = undefined } }, setEffect: function () {
    var a = this.config, b = a.effect, c = this.panels, e = this.content, d = a.steps, f = this.activeIndex, g = c.length; this.viewSize = [a.viewSize[0] || parseInt(c.width()) * d, a.viewSize[1] || parseInt(c.height()) * d]; if (b !== "none") {
        c.css("display", "block"); switch (b) {
            case "scrollx": case "scrolly": e.css("position", "absolute"); e.parent().css("position", "relative"); if (b === "scrollx") {
                    c.css("float",
"left"); e.width(this.viewSize[0] * (g / d))
                } break; case "fade": a = f * d; d = a + d; c.css({ position: "absolute", opacity: 0, "z-index": 1 }); c.slice(a, d).css({ opacity: 1, "z-index": 9 }); break
        } 
    } 
} 
};
Switchable.Effect = { none: function (a, b, c) { a.css("display", "none"); b.css("display", "block"); c() }, fade: function (a, b, c) { if (a.length === 1) { var e = this.config, d = this.panels; d = a.index(d) === b.index(d); if (this.firstSign && d) { b.css({ "z-index": 9, opacity: 1 }); return true } b.stop().css("opacity", 1); a.animate({ opacity: 0 }, this.firstSign || e.duration * 1E3, function () { b.css("z-index", 9); a.css("z-index", 1); c() }) } }, scroll: function (a, b, c, e) {
    a = this.config; b = a.effect === "scrollx"; var d = {}; d[b ? "left" : "top"] = -(this.viewSize[b ? 0 : 1] *
e) + "px"; this.content.stop().animate(d, this.firstSign || a.duration * 1E3, function () { c() })
}, circularScroll: function (a, b, c, e, d) {
    function f(q, k, n, o, l) { k = (n ? -1 : 1) * l * i; q.slice(t, u).css("position", "relative").css(o, k); return n ? l : -l * i } function g(q, k, n, o, l) { k = n ? -l * (i - 1) : ""; q.slice(t, u).css("position", "").css(o, ""); h.content.css(o, k) } var h = this; a = h.config; b = a.steps; var i = h.length, j = h.activeIndex, m = a.scrollType === "scrollx", r = m ? "left" : "top", s = h.viewSize[m ? 0 : 1]; m = -s * e; var v = {}, w, p = d === "backward", x = p ? i - 1 : 0, t = x * b,
u = (x + 1) * b; if (w = p && j === 0 && e === i - 1 || d === "forward" && j === i - 1 && e === 0) m = f.call(h, h.panels, e, p, r, s); v[r] = m + "px"; h.content.stop().animate(v, h.firstSign || a.duration * 1E3, function () { w && g.call(h, h.panels, e, p, r, s); c() })
} 
}; Switchable.Effect.scrollx = Switchable.Effect.scrolly = Switchable.Effect.scroll; Switchable.LazyLoad = function (a, b) { var c = b.config, e = c.steps; a = a * e; LazyLoad.loadCustomLazyData(b.panels.slice(a, a + e), c.lazyDataType) };
Switchable.extend = function (a, b) { var c = function () { }; c.prototype = b.prototype; a.prototype = new c; a.prototype.constructor = b }; function Tab(a, b, c) { Switchable.call(this, a, b, c) } Switchable.extend(Tab, Switchable); function Slide(a, b, c) { b = $.extend({}, Slide.defaultConfig, b); Switchable.call(this, a, b, c) } Slide.defaultConfig = { autoplay: true, circular: true, effect: "scrolly" }; Switchable.extend(Slide, Switchable); function Carousel(a, b, c) { b = $.extend({}, Carousel.defaultConfig, b); Switchable.call(this, a, b, c) }
Carousel.defaultConfig = { circular: true, effect: "scrollx", prevCls: "prev", nextCls: "next", disableCls: "disable" }; Carousel.init = function (a) { var b = a.config, c = b.disableCls; $.each(["prev", "next"], function (e, d) { var f = a[d + "Btn"] = $("." + b[d + "Cls"], a.container); f.bind("click", function (g) { g.preventDefault(); f.hasClass(c) || a[d]() }) }) }; Carousel.noCircular = function (a, b) { var c = b.config.disableCls, e = b.prevBtn, d = b.nextBtn; a = a === 0 ? e : a === b.length - 1 ? d : undefined; e.removeClass(c); d.removeClass(c); a && a.addClass(c) };
Switchable.extend(Carousel, Switchable);
