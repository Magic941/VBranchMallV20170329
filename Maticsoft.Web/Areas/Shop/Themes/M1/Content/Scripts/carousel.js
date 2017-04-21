/**
* carousel (Version 1.5)
* 
*
* Create a carousel
* @example new Carousel(container, showCount, options, callback);
* on Jquery
*
*/
function Carousel(container, showCount, options, callback) {
    this.container = $(container);
    this.clip = $(container + ' .J_carousel_clip');
    this.list = $(container + ' .J_carousel_list');
    this.item = $(container + ' .J_carousel_item');
    this.trigger = $(container + ' .J_carousel_trigger');
    this.showCount = showCount;
    this.timer = null;
    this.options = $.extend({
        auto: true,
        delay: 4,
        direction: '',
        duration: 100,
        etype: 'click',
        num: showCount,
        vertical: false
    },
    options);
    this.init();
    if (callback) {
        callback();
    }
}
Carousel.prototype = {
    init: function () {
        var _this = this,
        item = this.item,
        list = this.list,
        options = this.options,
        auto = !!options.auto,
        vertical = !!options.vertical,
        itemLen = item.length,
        itemW = item.outerWidth(true),
        itemH = item.outerHeight(true),
        itemWH = vertical ? item.outerHeight(true) : item.outerWidth(true);
        list[vertical ? 'height' : 'width'](itemWH * itemLen).css({
            position: 'absolute'
        });
        var listW = list.outerWidth(true),
        listH = list.outerHeight(true);
        this.clip.css({
            position: 'relative',
            overflow: 'hidden',
            width: (vertical ? Math.max(listW, itemW) : itemWH * this.showCount),
            height: (vertical ? itemWH * this.showCount : Math.max(listH, itemH))
        });
        if (itemLen <= this.showCount) {
            this.trigger.hide();
            return;
        } else {
            this.trigger.show();
        }
        this.trigger.bind(options.etype, this._trigger(this, itemWH));
        if (auto) {
            this._auto();
            this.container.hover(function () {
                _this._stop();
            },
            function () {
                _this._auto();
            });
        }
    },
    _trigger: function (_this, itemWH) {
        return function (e) {
            var listLast, listFirst, trigger = _this.trigger,
            list = _this.list,
            showCount = _this.showCount,
            options = _this.options,
            etype = options.etype,
            duration = options.duration,
            oNum = options.num,
            vertical = !!options.vertical,
            itemLen = _this.item.length,
            num = itemLen / showCount < 2 || itemLen / oNum < 2 ? (itemLen / showCount < 2 ? (oNum < itemLen % showCount ? oNum : itemLen % showCount) : showCount) : oNum,
            targetClass = e.target.className,
            animatePara = vertical ? [{
                top: 0,
                opacity: 1
            },
            {
                top: -itemWH * num,
                opacity: 1
            }] : [{
                left: 0,
                opacity: 1
            },
            {
                left: -itemWH * num,
                opacity: 1
            }];
            //opacity修正IE下大图滚动视觉断开bug
            if (/prev/i.test(targetClass)) {
                trigger.unbind(etype);
                for (var i = 0,
                len = num; i < len; i++) {
                    listLast = list.find('.J_carousel_item:last');
                    listLast.prependTo(list);
                }
                list.css(animatePara[1]);
                list.animate(animatePara[0], duration * num,
                function () {
                    trigger.bind(etype, _this._trigger(_this, itemWH));
                });
            }
            if (/next/i.test(targetClass)) {
                trigger.unbind(etype);
                list.animate(animatePara[1], duration * num,
                function () {
                    list.css(animatePara[0]);
                    for (var i = 0,
                    len = num; i < len; i++) {
                        listFirst = list.find('.J_carousel_item:first');
                        listFirst.appendTo(list);
                    }
                    trigger.bind(etype, _this._trigger(_this, itemWH));
                });
            }
            return false;
            //避免99click统计点击
        };
    },
    _auto: function () {
        var _this = this,
        options = _this.options,
        trigger;
        this.timer = setTimeout(function () {
            trigger = options.direction ? '.J_carousel_prev' : '.J_carousel_next';
            _this.trigger.find(trigger).trigger(options.etype);
            _this._auto();
        },
        options.delay * 1000);
    },
    _stop: function () {
        clearTimeout(this.timer);
    }
};