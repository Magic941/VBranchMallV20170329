/**
* User: qxhy123
* Date: 12-7-20
* Time: 下午5:08
*/
function CountDown(parameters) {
    var obj = {

        productId:0,
        thisObj:null,

        //倒计时结束时请求的url
        endurl: '',

        //倒计时结束时的回调函数,用于处理页面倒计时元素的移除等操作
        endcallback: '',

        //倒计时是否结束
        isEnd: false,

        //结束时间点时间对象
        endStemp: null,
        //活动开始时间
        startStemp:null,
        //天数
        day_Dom: null,
        //小时
        hour_Dom: null,
        //分钟
        min_Dom: null,
        //秒
        sec_Dom: null,

        //服务器的当前时间
        serverStemp: null,
      
        renderCallback:'',

        //客户端进入时间
        clientEntryTime: null,
        //逝去的时间
        diffTime:0,
        
        /**
        * 初始化倒计时
        * @param currnt
        * @param end
        */
        init: function (o) {
          
            this.endurl = o.endurl || '';
            this.day_Dom = o.day_Dom;
            this.hour_Dom = o.hour_Dom;
            this.min_Dom = o.min_Dom;
            this.sec_Dom = o.sec_Dom;
            this.endcallback = o.callback || function () { return false; };
            this.renderCallback = o.renderCallback || function () { return false; };
            //进入时间
            this.clientEntryTime = new Date();
            this.productId = o.productId;
            this.endStemp = new Date(o.endTime);
            this.startStemp = new Date(o.startTime);
            this.serverStemp = new Date(o.serverTime);
           
            this.thisObj = o.thisObj;
            this._CountDownLoop();
            var thisObj = this;
            window.setInterval(function () { thisObj.diffTime++; }, 500);
            
        },

        /**
        * 倒计时循环
        * @private
        */
        _CountDownLoop: function () {          
            
            //$(this.thisObj).html("sssDDD");
            var thisobj = this;
           var interverid = window.setInterval(function () {
              
               var val = thisobj.diffTime / 2 * 1000;
               if (val + thisobj.serverStemp.getTime() >= thisobj.endStemp.getTime()) {
                   thisobj._render(-1);
                   thisobj.isEnd = true;
                   window.clearInterval(interverid);
               }
               else {

                   //活动未开始的
                   if (val + thisobj.serverStemp.getTime() < thisobj.startStemp.getTime()) {

                       var kaiqiangDiffTime = thisobj._getDiffTimeServer(thisobj.startStemp, thisobj.serverStemp) - val;
                       thisobj._render(0, kaiqiangDiffTime);
                   }
                   else {
                       //活动开始了
                       var kaiqiangDiffTime = thisobj._getDiffTimeServer(thisobj.endStemp, thisobj.serverStemp) - val;
                       thisobj._render(1, kaiqiangDiffTime);
                   }
               }              

            }, 500);
        },

        //获取差值时间
        _getDiffTime: function () {

            return (new Date()).getTime() - this.clientEntryTime.getTime();
        },

        //计划两个时间的差值
        _getDiffTimeServer:function(time1,time2)
        {
            return time1.getTime() - time2.getTime();
        },

        /**
        * 使用倒计时时间渲染倒计时元素
        *-1结束，时间不动，写活动已结束，
        * 0未开始 
        * 1开始
        * @private
        */
        
        _render: function (status, difTime) {
            if(this.isEnd) return;
            this.thisObj.empty();
            var timeObj = null;
            if (difTime) {
                var t = difTime;
                //// 总秒数
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
                timeObj = {status:-1, title: "title", day: remain_day, hour: remain_hour, minute: remain_minute, second: remain_sec }
               
            }
           // alert(status);
            if (status == -1)
            {
                
                //按钮禁用事件
                this.renderCallback ? this.renderCallback.call(this.thisObj, {status:-1},this.productId) : null;
                //$(this.thisObj).html('抢购已结束');               
                return;
               
            }
            if (status == 0)
            {
                //timeObj.title = "准备开抢";
                timeObj.status = 0;
                var val = this.renderCallback ? this.renderCallback.call(this.thisObj, timeObj) : null;
                //$(thisobj.thisObj).html(' 准备开抢');
               val.appendTo(this.thisObj);
                // $(this.thisObj).html(val);
               return;
            }
            
            if (status == 1) {
                timeObj.title = "剩余";
                timeObj.status = 1;
                var val = this.renderCallback ? this.renderCallback.call(this.thisObj, timeObj) : null;

                val.appendTo(this.thisObj);

                //$(thisobj.thisObj).html('剩余');
                // $(this.thisObj).html(val);
                return;
            }
        }

    }, requestAnimation = (function (callback) {
        return  function (callback) {
            // console.log('我执行了?');
            window.setTimeout(callback, 1000);
        };
    })();
    return obj;
}
