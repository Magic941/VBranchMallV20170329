/**
* tab (Version 1.3)
* 
*
* Create a Tab
* @example new Tab(container, options);
* on Jquery
*
*/
function Tab(container, options, callback) {
    this.container = $(container);
    this.handle = $(container + ' .J_tab_nav li');
    this.panel = $(container + ' .J_tab_panel');
    this.count = this.handle.length;
    this.timer = null;
    this.eTime = null;
    this.options = $.extend({
        auto: false,
        delay: 4,
        event: 'mouseover',
        index: 1
    },
    options);
    this.init();
    if (callback) {
        callback.call(this);
    }
}
Tab.prototype = {
    init: function () {
        var that = this,
        count = this.count,
        op = this.options,
        auto = !!op.auto;
        this.handle.bind(op.event,
        function () {
            that._trigger(this);
        });
        if (op.index === 'r') {
            op.index = this._random(count)
        }
        this._show(op.index);
        if (auto) {
            this._auto();
            this.container.hover(function () {
                that._stop();
            },
            function () {
                that._auto();
            });
        }
    },
    _random: function (max) {
        return parseInt(Math.random() * max + 1);
    },
    _trigger: function (o) {
        var index, op = this.options,
        handle = this.handle;
        if (op.index === (handle.index(o) + 1)) {
            return;
        }
        index = op.index = handle.index(o) + 1;
        this._show(index);
    },
    _show: function (i) {
        this.handle.removeClass('cur').eq(i - 1).addClass('cur');
        this.panel.addClass('none').eq(i - 1).removeClass('none');
    },
    _auto: function () {
        var that = this,
        op = that.options;
        this.timer = setTimeout(function () {
            op.index = op.index < that.count ? ++op.index : 1;
            that._show(op.index);
            that._auto();
        },
        op.delay * 1000);
    },
    _stop: function () {
        clearTimeout(this.timer);
    }
};