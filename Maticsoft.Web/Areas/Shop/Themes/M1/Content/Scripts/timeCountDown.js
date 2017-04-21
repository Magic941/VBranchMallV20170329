/**
* User: qxhy123
* Date: 12-7-20
* Time: 下午5:08
*/

function CountDown(parameters) {
    var obj = {

        //countdown元素id
        countId: '',

        //倒计时结束时请求的url
        endurl: '',

        //倒计时结束时的回调函数,用于处理页面倒计时元素的移除等操作
        endcallback: '',

        //倒计时是否结束
        isEnd: false,

        //结束时间点时间对象
        endStemp: null,
        //天数
        day_Dom: null,
        //小时
        hour_Dom: null,
        //分钟
        min_Dom: null,
        //秒
        sec_Dom: null,

        Server_Time: null,
        /**
        * 初始化倒计时
        * @param currnt
        * @param end
        */
        init: function (o) {
            this.countId = o.id || 'CountDown';
            this.endurl = o.endurl || '';
            this.day_Dom = o.day_Dom;
            this.hour_Dom = o.hour_Dom;
            this.min_Dom = o.min_Dom;
            this.sec_Dom = o.sec_Dom;
            this.endcallback = o.callback || function () { return false; };
            this.endStemp = new Date(o.endTime);
            this.startStemp = new Date();
            this.Server_Time = o.Server_Time;
            this._CountDownLoop();
        },

        /**
        * 倒计时循环
        * @private
        */
        _CountDownLoop: function () {
            
            //alert("1111111");
            // var currStemp = this.Server_Time;
            var currStemp = new Date("2014-9-30 12:00:00");
           // alert(currStemp);
            //        console.log(currStemp.getTime());
            //如果结束时间戳减去当前时间时间戳小于等于0则设置倒计时结束标识为true

            if ((this.endStemp.getTime() - currStemp.getTime()) <= 0) {
                this.isEnd = true;
            }
            //如果结束则调用结束回调
            if (this.isEnd === true) {
                // console.log('countdown end');
                this.endcallback.apply(this, [this.endurl]);
            } else {

                var that = this;
                requestAnimation(function () {
                    that._render(currStemp);
                    that._CountDownLoop();
                });
            }
        },

        /**
        * 使用倒计时时间渲染倒计时元素
        * @private
        */
        _render: function (currStemp) {

            var t = this.endStemp.getTime() - ((new Date()).getTime() - this.startStemp.getTime() + currStemp.getTime());
            // 总秒数
            var xt = parseInt(t / 1000);
            // 秒数  
            var remain_sec = xt % 60;
            xt = parseInt(xt / 60);
            // 分数  
            var remain_minute = xt % 60;
            xt = parseInt(xt / 60);
            // 小时数  
            var remain_hour = xt % 24;
            xt = parseInt(xt / 24);
            // 天数  
            var remain_day = xt;
            //        console.log(remain_day);
            //        console.log($('#day'));
            this.day_Dom.val(remain_day);
            this.hour_Dom.val(remain_hour);
            this.min_Dom.val(remain_minute);
            this.sec_Dom.val(remain_sec);
        }

    }, requestAnimation = (function (callback) {
        return window.requestAnimationFrame || window.webkitRequestAnimationFrame
            || window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame || window.msRequestAnimationFrame ||
        function (callback) {
            console.log('我执行了?');
            window.setTimeout(callback, 10000);
        };
    })();
    return obj;
}
