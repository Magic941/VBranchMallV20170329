///<reference path="../../lib/jquery-1.3.2.js"/>
///<reference path="../../lib/jquery.string.1.0.js"/>
/*=============================1,jquery plugin of maticsoft=================================*/
//You need an anonymous function to wrap around your function to avoid conflict
; (function ($) {

    //Attach this new method to jQuery
    $.fn.extend({

        //This is where you write your plugin's name
        //1,singleSelect plugin.
        //说明:点击选中dom元素,同时赋予选中的元素指定的css class,其他的元素移除改css class
        singleSelect: function (opts) {
            var defaultOpts = {
                CssClass: 'cur',
                Assert: null,
                Callback: null
            };
            opts = $.extend(defaultOpts, opts);
            //Iterate over the current set of matched elements
            var items = this;

            return this.each(function () {

                $(this).click(function () {
                    //选中该元素前的断言函数（precondition method），如果断言函数返回false，则不选中该元素
                    if ($.isFunction(opts.Assert)) {
                        if (!opts.Assert(this)) return false;
                    };
                    items.filter(":visible").removeClass(opts.CssClass);
                    $(this).addClass(opts.CssClass);
                    if ($.isFunction(opts.Callback)) {
                        opts.Callback(this);
                    };
                });

            });
        },
        //endof singleSelect plugin
        preInput: function (opts) {
            ///<summary>为文本框显示默认值</summary>
            var needEmpty = function (defaultVal, curVal) {
                if ($.trim(curVal) == "" || $.trim(curVal) == defaultVal) {
                    return true;
                };
                return false;
            };
            return this.each(function () {
                var defaultVal = opts.val || "";
                $(this).val(defaultVal).focus(function () {
                    if (needEmpty(defaultVal, this.value)) {
                        $(this).val("");
                    };
                }).blur(function () {
                    var val = $(this).val();
                    if (needEmpty(defaultVal, val)) {
                        $(this).val(defaultVal);
                    };
                    if (opts.afterblur) {
                        opts.afterblur(this);
                    };
                });
            });
        },
        //endof preInput plugin
        toObject: function (opts) {
            ///<summary>用选定元素的name||id和value||innerHTML构造一个匿名对象</summary>
            ///<param>默认为{useid:true,prefix:""}，即使用id做匿名对象的属性</param>
            ///<return>匿名js对象</return>
            var retVal = {};
            var defaultOpts = {
                useid: true,
                prefix: ""
            };
            opts = $.extend(defaultOpts, opts);
            this.each(function () {
                if (opts.useid) {
                    if ((!this.id) || $.string(this.id).blank()) return true;
                    if (!$.string(opts.prefix).blank()) {
                        if (!$.string(this.id).startsWith(opts.prefix)) {
                            return true; //continue
                        };
                    };
                    retVal[this.id] = this.value || (this.innerHTML || "");
                } else {
                    if ((!this.name) || $.string(this.name).blank()) return true;
                    if (!$.string(opts.prefix).blank()) {
                        if (!$.string(this.name).startsWith(opts.prefix)) {
                            return true; //continue
                        };
                    };
                    retVal[this.name] = this.value || (this.innerHTML || "");
                };
            }); //endof this.each
            return retVal;
        } //endof toObject
    }); //endof $.fn.extend
    //Static methods
    $.wcfJsonPost = function (url, data, callback, error) {
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            contentType: "application/json",
            timeout: 10000,
            dataType: "json",
            success: callback,
            error: error
        });
    }; //endof $.wcfJsonPost
    $.asmxJsonPost = function (url, data, callback, error) {
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            contentType: "application/json",
            timeout: 10000,
            dataType: "json",
            success: callback,
            error: error
        });
    }; //endof $.asmxJsonPost
    $.preloadImages = function () {
        //<summary>reference:www.mattfarina.com/2007/02/01/preloading_images_with_jquery</summary>
        //<remarks>$.preloadImages('xx.gif','yy.png');</remarks>
        for (var i = 0; i < arguments.length; i++) {
            jQuery("<img>").attr("src", arguments[i]);
        }
    }; //endof preloadImages
    $.navTo = function (opts) {
        ///<summary>将当前页面导航至指定路径</summary>
        ///<param name="opts">{url:'www.vivasky.com',timeout:3000}</param>
        var timeout = 0;
        if (opts && opts.timeout) timeout = opts.timeout;
        var refresh = function () {
            if (opts && opts.url) {
                window.location.href = opts.url;
            } else {
                //refresh current page
                window.location.reload();
                //window.history.go(0);
                //window.location.href = window.location.href;
            };
        };
        setTimeout(refresh, timeout);
    }; //endof navTo
    $.noScript = function (text) {
        ///<summary>判断指定的字符串没有有害的script字符</summary>
        ///<return>bool</return>
        var flag = true;
        var scriptWord = "<|>|script|alert|{|}|(|)|#|$|'|\"|:|;|&|*|@|%|^|?";
        var words = scriptWord.split('|');
        for (var i = 0; i < words.length; i++) {
            if (text.indexOf(words[i]) != -1) {
                flag = false;
                break;
            };
        };
        return flag;
    }; //endof $.noScript
    $.clearSql = function (text) {
        ///<summary>清除字符串中的sql关键字</summary>
        var repWord = "|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|set|;|from";
        var repWords = repWord.split('|');
        var appIndex;
        for (var i = 0; i < repWords.length; i++) {
            appIndex = text.indexOf(repWords[i]);
            if (appIndex != -1) {
                text = text.replace(repWords[i], "");
            }
        }
        return text;
    };
    $.hasQuote = function (text) {
        var yes = text.indexOf("'") > -1 || text.indexOf('"') > -1;
        return yes;
    }; //endof $.noQuote
    $.hasher = function (hash) {
        ///<summary>设置或获取当前url地址的hash值</summary>
        if (hash || hash == "") {
            window.location.hash = hash;
        } else {
            var _hash = window.location.hash;
            return _hash.substr(1);
        };
    };

    //pass jQuery to the function, 
    //So that we will able to use any valid Javascript variable name 
    //to replace "$" SIGN. But, we'll stick to $ (I like dollar sign: ) )		
})(jQuery);

/*==============================2,其他通用的方法================================*/
var M18 = {
    AbsoluteWebRoot: null,
    //网站绝对路径
    WcfUrl: null
};
M18.Init = function (opts) {
    ///<summary>初始化函数。site.master页面调用</summary>
    M18.AbsoluteWebRoot = opts.AbsoluteWebRoot;
    M18.WcfUrl = opts.WcfUrl;
    M18.AsmxCartUrl = opts.AsmxCartUrl;
    M18.AsmxStyleUrl = opts.AsmxStyleUrl;
    M18.LoginUrl = opts.LoginUrl;
    M18.ImageRootURL = opts.ImageRootURL;
};

/*=============================3,jquery.validate.js扩展===========================*/
$(document).ready(function () {
    if (!jQuery.validator) return;
    jQuery.validator.addMethod('notEqualTo',
    function (value, element, param) {
        return value != jQuery(param).val();
    },
    'Must not be equal to {0}.');
    jQuery.validator.addMethod('greaterThan',
    function (value, element, param) {
        return (IsNaN(value) && IsNaN(jQuery(param).val())) || (value > jQuery(param).val());
    },
    'Must be greater than {0}.');
    jQuery.validator.addMethod('lesserThan',
    function (value, element, param) {
        return (IsNaN(value) && IsNaN(jQuery(param).val())) || (value < jQuery(param).val());
    },
    'Must be lesser than {0}.');
    jQuery.validator.addMethod('numberNative',
    function (value, element, param) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:\,\d+)?$/.test(value);
    },
    'Not a valid number.');
    jQuery.validator.addMethod('simpleDate',
    function (value, element, param) {
        return this.optional(element) || /^\d{1,2}\-\d{1,2}\-\d{4}$/.test(value);
    },
    'Not a valid date.');
});