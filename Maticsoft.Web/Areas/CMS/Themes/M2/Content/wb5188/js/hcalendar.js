/*
@author   DengHailong haiyume@163.com
@date:    2015-1-12
@edition: 1.1 beta
@info:    多功能炫彩日历组件（哈利路亚 没有BUG -_-!）
@relay:   依赖 jquery
*/
;(function($){
    $.hcalendar = $.hcalendar || function(opt){
        var defaults = {
            cName : '',                 //类名
            headerSin : '<span rel="0">今天</span>|<span rel="1">昨天</span>|<span rel="7">7天前</span>|<span rel="14">14天前</span>|<span rel="30">30天前</span>',
            headerMul : '<span rel="0">今天</span>|<span rel="3">近3天</span>|<span rel="7">近7天</span>|<span rel="14">近14天</span>|<span rel="30">近30天</span>',  
            splitTex : ' 至 ',          //开始结束日期分隔符
            ymd : ["年","月","日","时","分"],     //年月日小时分钟的汉字显示                   
            weeks : ["一","二","三","四","五","六","日"],  //周一到周日的显示
            months : ["一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月"], //一月到十二月的显示
            btTrue : '确定',            //确定按钮显示字体
            btFalse : '取消',           //取消按钮显示字体
            gap : '-',                  //日期分隔符
            //fillDate : true,          //页面初始化时是否填充指定日期，默认否，开启比较耗性能
            ta : 'left',                //对齐方式，默认左对齐，right为右对齐
            left : 0,                   //位置left修正值
            top : 0,                    //位置top修正值
            startWeek : 1,              //开始显示的周，默认从周一开始显示
            readonly : false,           //是否为只读
            sdate : '',                 //设置默认显示开始时间(一般不用设置)
            edate : '',                 //设置默认显示结束时间(一般不用设置)
            isTodaySelect : true,       //值为空的时候是否显示今天，默认是
            isSin : true,               //是否是单日历，默认是
            isSection : false,          //是否是区间选择
            isWeek : false,             //是否周显示，默认否
            isMonth : false,            //是否按月显示
            isTime : false,             //是否按时间显示(如12:00)
            isHeader : false,           //是否显示头部，默认否
            isFooter : false,           //是否显示尾部按钮区，默认否
            disFormat : null,           //当日是否可选的format函数，返回false不可选，参数：年，月，日，周，时间撮
            isFuture : true,            //未来是否可选，默认是，disFormat激活时失效（仅选择日期时有效）
            isToday : true,             //今天是否可选，默认是，disFormat激活时失效（仅选择日期时有效）
            format : null,              //日历显示的format函数，参数：年，月，日，周，时间撮
            cb : null,                  //回调函数
            selectCb : null             //选择日期之后触发的函数
            //changeCb : null           //改变时触发
        };
        var options = $.extend({}, defaults);
        
        //初始化
        if(!$("body").children(".hcalendar").length){
            var bodyStr = '<div class="hc-info"><i class="hc-fl"></i><i class="hc-fr"></i><div class="hc-title"></div></div><table class="hc-table"><thead></thead><tbody></tbody></table>';   
            $("body").prepend('<div class="hcalendar" style="display:none;"><div class="hcalendar-header"></div><div class="hcalendar-body hc-bd1">'+bodyStr+'</div><div class="hcalendar-body hc-bd2">'+bodyStr+'</div><div class="hcalendar-footer"><a class="hc-bt hc-bt-n">取消</a><a class="hc-bt hc-bt-y">确定</a><input type="text" class="hc-fot-date hc-fot-sdate" disabled="disabled" /><input type="text" class="hc-fot-date hc-fot-edate" disabled="disabled" /></div></div>');   
        }
        var $picker = $("body").children(".hcalendar:first");

        var hdata = {
            date : '',  //单日选择使用
            sdate : '', //区间选择开始日期
            edate : ''  //区间选择结束日期
        };
        var curDom = null;  //当前作用对象
        if(!$.isArray(opt) && opt){opt = [opt];}
  
        //set
        function reset(){
            if(options.readonly){
                $picker.addClass("hc-readonly");
            }else{
                $picker.removeClass("hc-readonly");
            }
            if(options.isTime){
                options.isSection = options.isWeek = options.isHeader = options.isFooter = options.isSin = false;
                options.isFooter = true;
                $picker.removeClass("hc-table-month").addClass("hc-table-time");
            }else if(options.isMonth){
                options.isSection = options.isWeek = options.isHeader = options.isFooter = false;
                $picker.removeClass("hc-table-time").addClass("hc-table-month").children(".hcalendar-body").children(".hc-table");
            }else{
                $picker.removeClass("hc-table-month hc-table-time").children(".hcalendar-body").children(".hc-table").html('<thead></thead><tbody></tbody>').each(function(){
                    var ws = (function(){
                        var str = '<tr>';
                        var eq;
                        for(var i = 0; i < 7; i++){
                            eq = options.startWeek + i;
                            if(eq > 7){eq = eq%7;}
                            str += '<td>'+options.weeks[eq - 1]+'</td>';
                        }
                        return str + '</tr>';
                    })();
                    $(this).children("thead").html(ws);
                });
            }
            //条件限制
            if(options.isWeek){
                options.isHeader = false;
            }
            if(options.isHeader){
                var str = options.isSection ? options.headerMul : options.headerSin;
                $picker.children(".hcalendar-header").html(str).show();
            }else{
                $picker.children(".hcalendar-header").hide();
            }
            if(options.isSin){
                $picker.children(".hcalendar-body").eq(1).hide().prev(".hcalendar-body").children(".hc-info").children(".hc-fr").show();
            }else{
                $picker.children(".hcalendar-body").eq(1).show().children(".hc-info").children(".hc-fl").hide();
                $picker.children(".hcalendar-body").eq(0).children(".hc-info").children(".hc-fr").hide();
            }
            if(options.isFooter){
                $picker.children(".hcalendar-footer").show();
            }else{
                $picker.children(".hcalendar-footer").hide();
            }
            if(options.isWeek || options.isSection){
                $picker.children(".hcalendar-footer").children(".hc-fot-date").show();
            }else{
                $picker.children(".hcalendar-footer").children(".hc-fot-date").hide();
            }
            if(options.isFooter){
                $picker.children(".hcalendar-footer").find(".hc-bt-y").text(options.btTrue).siblings(".hc-bt-n").text(options.btFalse);
            }
            
            //默认显示今天
            if(options.isTodaySelect){
                var nD = new Date();
                var cday = nD.getFullYear() + options.gap + (nD.getMonth()+1) + options.gap + nD.getDate();
                hdata.sdate = hdata.edate = hdata.date = cday;
            }
            
        }

        //补0
        function bze(num){
            num = String(num);
            if(num.length < 2){
                return '0' + num;
            }
            return num;
        }

        //渲染月
        function renderMonth(year, ds){
            var arr = hdata.date.split(new RegExp(options.gap,'g'));
            if(options.isTodaySelect && !arr[1]){
                arr[0] = new Date().getFullYear();
                arr[1] = new Date().getMonth()+1;
            }
            $picker.children(".hcalendar-body").each(function(ii){
                if(options.isSin && ii === 1){return;}
                var str = (function(){
                    var s = '';
                    for(var i = 0; i < 12; i++){
                        var ca = '';
                        if(typeof options.disFormat === 'function' && options.disFormat(year+ii, i+1) === false){
                            ca = ' disabled';
                        }else if(arr[0] == (year+ii) && arr[1] == (i+1)){
                            ca = ' hc-month-td-se';
                        }
                        if(i === 0 || i === 4 || i === 8){
                            s += '<tr>';
                        }
                        s += '<td><div y="'+(year+ii)+'" m="'+(i+1)+'" class="hc-month-td'+ca+'">'+(typeof(options.format)==='function' ? (options.format(year+ii,i+1,options.months[i])||options.months[i]) : options.months[i])+'</div></td>';
                        if(i === 3 || i === 7 || i === 11){
                            s += '</tr>';
                        }
                    }
                    return s;
                })();
                $(this).children(".hc-info").children(".hc-title").html((year + ii) + options.ymd[0]);
                $(this).attr("y", (year+ii)).children(".hc-table").html(str);
            });    
        }

        //渲染时间
        function renderTime(t, ds){
            var t = t ? t.split(":") : [];
            if(!t || !t.length){
                if(options.isTodaySelect){t = [new Date().getHours(), new Date().getMinutes()];}
            }else{
                t[0] = t[0] ? Number(t[0]) : '';
                t[1] = t[1] ? Number(t[1]) : '';
            }
            var redStr = function(n, sp, type){
                sp = sp || 4;
                var str = '<tr>';
                for(var i = 0; i <= n; i++){
                    if(i%sp === 0 && i > sp-1){str += '<tr>';}
                    var st = bze(i);
                    var ca = '';
                    if(n === 23 && typeof(options.disFormat) === 'function' && options.disFormat(i) === false){
                        ca = ' disabled';
                    }else if( (n === 23 && i === t[0]) ||(n===59 && i === t[1]) ){
                        ca = ' hc-time-td-se';
                    }
                    str += '<td><div t="'+st+'" class="hc-time-td'+ca+'">'+(typeof(options.format) === 'function'?(options.format(st,type)||st):st)+'</div></td>';
                    if(i%sp === sp-1 && i === sp-1){str += '</tr>';}
                }
                return str;
            };
            $picker.children(".hcalendar-body").each(function(eq){
                $(this).children(".hc-info").children(".hc-title").html( eq ? options.ymd[4] : options.ymd[3] );
                $(this).children(".hc-table").html(redStr( eq ? 59 : 23, eq ? 6 : 4, eq ? 'm' : 'h' ));
            });
        }

        //生成日月(年，月，方向<left/right>)
        function picRender(year, month, ds){
            if(options.isTime){
                renderTime(year, ds);
                return;
            }
            //如果是右边方向则转换为左边
            if(ds === 'right'){
                month--;
                if(month < 1){
                    month = 12;
                    year--;
                }
            }
            var curDate = new Date();
            if(!year || isNaN(year) || year > 10000){
                year = curDate.getFullYear();
            }
            if(!month || isNaN(month) || month <= 0 || month > 12){
                month = curDate.getMonth() + 1;
            }
            year = Number(year);
            month = Number(month);
            var curTime = new Date(curDate.getFullYear() + '/' + (curDate.getMonth() + 1) + '/' + curDate.getDate()).getTime();
            if(options.isMonth){
                renderMonth(year, ds);
            }else{
                //处理sdate edate date
                var reg = new RegExp(options.gap,'g');
                var sTime = new Date( String(hdata.sdate).replace(reg,'/') ).getTime();
                var eTime = new Date( String(hdata.edate).replace(reg,'/') ).getTime();
                var seTime = new Date( String(hdata.date).replace(reg,'/') ).getTime();
                if(sTime && eTime && sTime > eTime){
                    var seDate = hdata.sdate;
                    hdata.sdate = hdata.edate;
                    hdata.edate = seDate;
                    seDate = sTime;
                    sTime = eTime;
                    eTime = seDate;
                }
                $picker.children(".hcalendar-footer").children(".hc-fot-sdate").val(hdata.sdate).siblings(".hc-fot-edate").val(hdata.edate);
                //console.log(hdata.sdate + '---' + hdata.edate);
                $picker.children(".hcalendar-body").each(function(eq){
                    if(options.isSin && eq){return;}
                    year = month + eq > 12 ? year + 1 : year;
                    month = month + eq > 12 ? 1 : month + eq;
                    $(this).attr({
                        y : year,
                        m : month
                    }).children(".hc-info").children(".hc-title").html( year + options.ymd[0] + (month) + options.ymd[1] );
                    var dTime = 1000 * 3600 * 24;   //一天
                    var tdStr = [];
                    var time = new Date(year + '/' + (month) + '/1').getTime();
                    var date = new Date(time);
                    var w = date.getDay();  //1号是周几
                    if(w == 0){w = 7;}
                    var fd = w - options.startWeek < 0 ? (w - options.startWeek + 7) : (w - options.startWeek); //前面补齐天数
                    var y,m,d,wk;   //年月日周
                    var isF = false;    //本月是否已经遍历完毕
                    for(var i = 0; i < 6; i++){
                        if(isF){break;}
                        if(i){tdStr.push('<tr>');}
                        for(var j = 0; j < 7; j++){
                            if(!i && j < fd){continue;}
                            date = new Date(time);
                            y = date.getFullYear();
                            m = date.getMonth() + 1;
                            d = date.getDate();
                            wk = date.getDay();
                            if(wk == 0){wk = 7;}
                            if(m === month){
                                var cla = '';
                                //处理可选可不选
                                if( (!options.isFuture && curTime < time) || (!options.isToday && curTime === time) ){
                                    cla = ' disabled';
                                }
                                if(typeof options.disFormat === 'function'){
                                    if(options.disFormat(y,m,d,wk,time) === false){
                                        cla = ' disabled';
                                    }else{
                                        cla = '';
                                    }
                                }
                                //处理已选
                                if(cla !== ' disabled'){
                                    if( (options.isWeek || options.isSection) && (sTime && eTime) && (sTime !== eTime) ){
                                        if(time === sTime){
                                            cla = '  hc-sdate';
                                        }else if(time === eTime){
                                            cla = ' hc-edate';
                                        }else if(time > sTime && time < eTime){
                                            cla = ' hc-se';
                                        }
                                    }else{
                                        var csTime =  (options.isWeek || options.isSection) ? (sTime || eTime) : seTime;
                                        if(csTime && time === csTime){
                                            cla = ' hc-select';
                                        }
                                    }
                                }
                                tdStr.push('<td title="'+(y+options.gap+bze(m)+options.gap+bze(d))+'" class="hc-td'+cla+'" time="'+time+'" date="'+(y+options.gap+bze(m)+options.gap+bze(d))+'">'+( typeof options.format === 'function' ? (options.format(y,m,d,wk,time)||d) : d )+'</td>');
                            }else{
                                isF = true;
                                if(j === 0){break;}
                                tdStr.push('<td time="'+time+'" date="'+(y+options.gap+bze(m)+options.gap+bze(d))+'" class="disabled">'+d+'</td>');  
                            }
                            time = dTime + time;
                        }
                        tdStr.push('</tr>');  
                    }
                    if(tdStr[tdStr.length - 2] === '<tr>'){
                        tdStr.pop();
                        tdStr.pop();
                    }
                    time = new Date(year + '/' + month + '/1').getTime();
                    for(i = 0; i < fd; i++){
                        time = time - dTime;
                        date = new Date(time);
                        tdStr.unshift('<td time="'+time+'" date="'+(date.getFullYear()+options.gap+bze(date.getMonth()+1)+options.gap+bze(date.getDate()))+'" class="disabled">'+date.getDate()+'</td>');
                    }
                    tdStr.unshift('<tr>');
                    $(this).children(".hc-table").children("tbody").html(tdStr.join(''));
                });
            }
        }

        //显示日历,参数$el-dom目标，opt-参数
        function picOpen($el, opt){
            if($picker.is(":visible")){return;}
            if(!$el[0]){$el = $($el);}
            curDom = $el.addClass("hcalendar-open");
            if(opt){options = $.extend({}, defaults, opt);}
            reset();
            var v = $.trim($el.val());
            if( (options.sdate && options.edate) || (options.sdate && !options.isWeek && !options.isSection) ){
                v = options.sdate + options.splitTex + options.edate;
            }
            if(v){
                var arr = v.split(new RegExp(options.splitTex,'g'));
                hdata.sdate = hdata.date = arr[0] || '';
                hdata.edate = arr[1] || '';
                var ymd = hdata.sdate.split(new RegExp(options.gap,'g'));
                picRender(ymd[0], ymd[1]);
            }else{
                picRender();
            }
            var style1 = {
                display : 'block',
                opacity : 0,
                //left : options.ta === 'right' ? ($el.offset().left - ($picker.width() - $el.width()) + parseInt($el.css('paddingLeft')) + parseInt($el.css('paddingRight')) + options.left) : ($el.offset().left + options.left),
                //top : $el.offset().top + $el.height() + 2 + options.top,
                left : options.ta === 'right' ? ($el.offset().left - ($picker.outerWidth() - $el.outerWidth()) + options.left) : ($el.offset().left + options.left),
                top : $el.offset().top + $el.outerHeight() + options.top,
                marginTop : -20
            };
            var style2 = {
                opacity : 1,
                marginTop : 0
            };
            //判断日历方向
            var bHeight = $(window).height() - $el.offset().top + $(window).scrollTop();   //距底端的高度
            var pHeight = $picker.height(); //日历高度
            options.posUp = bHeight < pHeight ? true : false;  //日历是否位于输入框上方
            if(options.posUp){
                style1 = $.extend(style1, {
                    marginTop : 20,
                    top : style1.top - pHeight - $el.height() - 4 - 2*options.top
                });
            }
            //显示日历
            $picker.stop().css(style1).animate(style2, 200);
            if(typeof options.cb === 'function'){options.cb.call($el[0]);}
        }

        //关闭日历
        function picClose(){
            if(curDom){curDom.removeClass("hcalendar-open");}
            $picker.stop().animate({
                opacity : 0,
                marginTop : options.posUp ? 20 : -20
            }, 200, function(){
                $picker.hide();
                $e = null;
            });
        }

        //选中日期(头部点击类型 年/月/日)
        function picTrue(htype){
            if(options.isTime){
                var h = $picker.children(".hcalendar-body").eq(0).find(".hc-time-td-se").attr("t");
                var m = $picker.children(".hcalendar-body").eq(1).find(".hc-time-td-se").attr("t") || '00';
                if(h){
                    curDom.val(h+':'+m);
                }
            }else if(options.isMonth){
                curDom.val(hdata.date);
            }else if(options.isSection || options.isWeek){
                if(hdata.sdate && hdata.edate){
                    curDom.val(hdata.sdate + options.splitTex + hdata.edate).attr({
                        sdate : hdata.sdate || '',
                        edate : hdata.edate || ''
                    });
                }
            }else{
                curDom.val(hdata.date);
            }
            curDom.trigger("change");
            picClose();
            if(typeof options.selectCb === 'function'){options.selectCb.call(curDom[0], hdata, htype);}
        }

        //点击任意地方关闭日历
        function pickerClose(e){
            var $e = $(e.target);
            if(opt){
                $.each(opt, function(i, j){ 
                    if(j.cName && $e.hasClass(j.cName) && $e[0].tagName.toUpperCase() === 'INPUT'){
                        picOpen($e, j);
                        return false;
                    }
                });
            }
            if($e.hasClass("hcalendar-open")){return;}
            //关闭
            if($e && $picker.is(":visible") && $e.is(":visible") && !$e.closest(".hcalendar").length){
                picClose();
            }
            $e = null;
        }

        /*事件集*/
        //切换月、年
        $picker.off().on("click", ".hc-fl", function(){
            var $b = $(this).closest(".hcalendar-body");
            var y = Number($b.attr("y"));
            var m = Number($b.attr("m"));
            $b = null;
            if(options.isMonth){
                y--;
            }else{
               
                m--;
                if(m < 1){
                    m = 12;
                    y--;
                }
            }
            picRender(y, m);

        //切换月、年
        }).on("click", ".hc-fr", function(){
            var $b = $(this).closest(".hcalendar-body");
            var y = Number($b.attr("y"));
            var m = Number($b.attr("m"));
            $b = null;
            if(options.isMonth){
                if(options.isSin){y++;}
            }else{
                if(options.isSin){
                    m++;
                    if(m > 12){
                        m = 1;
                        y++;
                    }
                }
            }
            picRender(y, m);
        
        //选择日
        }).on("click", ".hc-td", function(){
            if($(this).hasClass("disabled") || $picker.hasClass("hc-readonly")){return;}
            var time = Number( $(this).attr("time") );
            if(!time){return;}
            var date = new Date(time);
            $picker.find("td").removeClass("hc-sdate hc-edate hc-se hc-select");
            //周显示
            if(options.isWeek){
                hdata.sdate = hdata.edate = '';
                $(this).closest("tr").children("td").each(function(){
                    var eq = $(this).index();
                    if(eq === 0){
                        hdata.sdate = $(this).attr("date");
                    }else if(eq === 6){
                        hdata.edate = $(this).attr("date");
                    }
                });

            }else if(options.isSection){
                if( (hdata.sdate && hdata.edate) || (!hdata.sdate && !hdata.edate) ){
                    hdata.sdate = $(this).attr("date");
                    hdata.edate = '';
                }else if(!hdata.sdate){
                    hdata.sdate = $(this).attr("date");
                }else if(!hdata.edate){
                    hdata.edate = $(this).attr("date");
                }
            }else{
                hdata.date = $(this).attr("date");
            }
            var ds = $(this).closest(".hcalendar-body").index();
            picRender(date.getFullYear(), date.getMonth() + 1, ds==2?'right':'left');
            //没有确定按钮的情况
            if(!options.isFooter){
                if(options.isWeek || options.isSection){
                    if(hdata.sdate && hdata.edate){picTrue();}
                }else if(hdata.date){
                    picTrue();
                }
            }
        
        //点击header
        }).on("click", ".hcalendar-header>span", function(){
            if($picker.hasClass("hc-readonly")){return;}
            var rel = Number($(this).attr("rel"));
            var type = $(this).attr("type") || 'day';   //day(天)/month(月-暂不支持)/year(年)

            if(type === 'day'){
                var dTime = 1000 * 3600 * 24;   //一天
                var todayTime = options.isToday ? new Date().getTime() : new Date().getTime() - dTime;
                var today = new Date(todayTime);
                if(options.isWeek || options.isSection){
                    if(rel > 0){rel--;} 
                    hdata.sdate = today.getFullYear() + options.gap + bze(today.getMonth()+1) + options.gap + bze(today.getDate());
                    var past = new Date( todayTime - (rel * dTime) );
                    hdata.edate = past.getFullYear() + options.gap + bze(past.getMonth()+1) + options.gap + bze(past.getDate());              
                }else{
                    if(rel > 1){rel--;} 
                    var past = new Date( todayTime - (rel * dTime) );
                    hdata.date = past.getFullYear() + options.gap + bze(past.getMonth()+1) + options.gap + bze(past.getDate());
                }
            }else if(type === 'year'){
                var today = new Date();
                var y =today.getFullYear();
                y = y - rel;
                hdata.sdate = y + options.gap + '01' + options.gap + '01';
                hdata.edate = y + options.gap + '12' + options.gap + '31';
                //今年只取到今天（可修改）
                if(rel === 0){
                    if(!options.isToday){
                        today = today - 1000 * 3600 * 24;
                    }
                    hdata.edate = today.getFullYear() + options.gap + bze(today.getMonth()+1) + options.gap + bze(today.getDate());
                }
            }
            
            picRender();

            picTrue(type);  //触发确定按钮

        //确定按钮
        }).on("click", ".hc-bt-y", function(){
            if($picker.hasClass("hc-readonly")){return;}
            picTrue();

        //取消按钮
        }).on("click", ".hc-bt-n", function(){
            picClose();
        
        //点击月
        }).on("click", ".hc-month-td", function(){
            if($(this).hasClass("disabled") || $picker.hasClass("hc-readonly")){return;}
            var y = $(this).attr("y");
            var m = $(this).attr("m");
            hdata.date = y + options.gap + bze(m);
            picTrue();

        //点击时间
        }).on("click", ".hc-time-td", function(){
            if($(this).hasClass("disabled") || $(this).hasClass("hc-time-td-se") || $picker.hasClass("hc-readonly")){return;}
            $(this).closest(".hc-table").find(".hc-time-td").removeClass("hc-time-td-se");
            $(this).addClass("hc-time-td-se");
        });


        //显示关闭事件
        $(document).on("click.hcalendar", pickerClose);

        //返回值
        return {
            //显示
            show : function(dom, opt){
                picOpen(dom, opt);
            },
            //销毁(一般不用销毁，因为是事件委托机制，且不会冒泡，所以性能较高)
            remove : function(){
                $picker.off().remove();
                $(document).off("click.hcalendar", pickerClose);
            },
            //修改默认参数
            setDefaults : function(opt){
                defaults = opt;
            }
        };

    };
})(jQuery);