$(function () {
    //此方法用户div下的ul滚动效果
    $.fn.scrollEndWise = function (o) {
        var defaults = {
            speed: 20,
            amount: 0,//animate动画时长
            step: 1,//步长
            dir: "left",//方向
            leftClick: '',//按钮控制向左滚动
            rightClick: '',//按钮控制向右滚动
            upClick: '',//按钮控制向上滚动
            downClick: ''//按钮控制向下滚动
        };
        obj = $.extend({}, o);
        return this.each(function () {
            var _li = $("li", this);
            _li.parent().parent().css({ overflow: "hidden", position: "relative" }); //div
            _li.parent().css({ margin: "0", padding: "0", overflow: "hidden", position: "relative", "list-style": "none" }); //ul
            _li.css({ position: "relative", overflow: "hidden" }); //li
            if (obj.dir == "left" || obj.dir == "right") _li.css("float", "left");
            //初始大小
            var _li_size = 0;
            for (var i = 0; i < _li.size() ; i++)
                _li_size += obj.dir == "left" || obj.dir == "right" ? _li.eq(i).outerWidth(true) : _li.eq(i).outerHeight(true);

            if (obj.dir == "left" || obj.dir == "right") _li.parent().css({ width: (_li_size * 3) + "px" });
            _li.parent().empty().append(_li.clone()).append(_li.clone()).append(_li.clone());
            _li = $("li", this);

            //滚动
            var _li_scroll = 0;
            function goto(direction, step) {
                _li_scroll += step;
                if (direction == "left" || direction == "up") {
                    if (_li_scroll > _li_size) {
                        _li_scroll = 0;
                        _li.parent().css(direction == "left" ? { left: -_li_scroll } : { top: -_li_scroll });
                        _li_scroll += step;
                    }
                } else if (direction == "right") {
                    if (_li_scroll == 0) {
                        _li_scroll = _li_size;
                        _li.parent().css({ left: -_li_scroll });
                        _li_scroll += step;
                    }
                }

                _li.parent().animate({ left: -_li_scroll }, obj.amount);
            }

            //开始
            var move = setInterval(function () { goto(obj.dir, obj.step); }, obj.speed);
            _li.parent().hover(function () {
                clearInterval(move);
            }, function () {
                clearInterval(move);
                move = setInterval(function () { goto(obj.dir, obj.dir == "left" ? obj.step : -obj.step); }, obj.speed);

            });

            //鼠标点击控制滚动方向
            $(obj.leftClick).click(function () {
                clearInterval(move);
                obj.dir = "left";
                move = setInterval(function () { goto(obj.dir, obj.step); }, obj.speed);
            });
            $(obj.rightClick).click(function () {
                clearInterval(move);
                obj.dir = "right";
                move = setInterval(function () { goto(obj.dir, -obj.step); }, obj.speed);
            });
        })
    }

    /*
    *顶部查询框浮动定位
    **/
    $.fn.capacityFixed = function (options) {
        var opts = $.extend({}, $.fn.capacityFixed.deflunt, options);
        var FixedFun = function (element) {
            var top = opts.top;
            element.css({
                "top": top
            });
            $(window).scroll(function () {
                var scrolls = $(this).scrollTop();
                if (scrolls > top) {

                    if (window.XMLHttpRequest) {
                        element.css({
                            position: "fixed",
                            top: 0
                        });
                    } else {
                        element.css({
                            top: scrolls
                        });
                    }
                } else {
                    element.css({
                        position: "absolute",
                        top: top
                    });
                }
            });
        };
        return $(this).each(function () {
            FixedFun($(this));
        });
    };
    $.fn.capacityFixed.deflunt = {
        right: 0, //相对于页面宽度的右边定位
        top: 80
    };
});