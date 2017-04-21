(function ($) {
    $.fn.badPass = 'Weak';
    $.fn.goodPass = 'Good';
    $.fn.strongPass = 'Strong';
    $.fn.resultStyle = "";
    $.fn.passStrength = function (options) {
        var defaults = {
            badPass: "badPass",
            goodPass: "goodPass",
            strongPass: "strongPass",
            minLength: "6",
            maxLength: "30",
            tipsloc: 1 //1 tips input bottom,0 tips input right
        };
        var opts = $.extend(defaults, options);
        return this.each(function () {
            var obj = $(this);
            $(obj).unbind().keyup(function () {
                //return strength tips words
                var results = $.fn.teststrength($(this).val(), opts);
                var sTips = $('<div class="f-pwd-t"><span class="f-pwd-t-h">密码强度：</span><span class="f-pwd-t-w f-pwd-t-item">弱</span><span class="f-pwd-t-m f-pwd-t-item">中</span><span class="f-pwd-t-s f-pwd-t-item">强</span></div>');
                //check password length if it>30 or it<6 tips hidden
                if (opts.minLength <= $(this).val().length && opts.maxLength >= $(this).val().length) {
                    $(this).next(".f-pwd-t").remove();
                    $(this).after(sTips);
                    $(this).next(".f-pwd-t").addClass($(this).resultStyle);
                } else {
                    $(this).next(".f-pwd-t").remove();
                }
                //tips position
                if (opts.tipsloc == 1) {
                    $(this).next(".f-pwd-t").parent().addClass("f-pwdb");
                }
                if (opts.tipsloc == 0) {
                    $(this).next(".f-pwd-t").parent().addClass("f-pwdr");
                }
            });
            //FUNCTIONS
            $.fn.teststrength = function (password, option) {
                var score = 0;
                //password length
                score += password.length * 4;
                score += ($.fn.checkRepetition(1, password).length - password.length) * 1;
                score += ($.fn.checkRepetition(2, password).length - password.length) * 1;
                score += ($.fn.checkRepetition(3, password).length - password.length) * 1;
                score += ($.fn.checkRepetition(4, password).length - password.length) * 1;
                //password has 3 numbers
                if (password.match(/(.*[0-9].*[0-9].*[0-9])/)) {
                    score += 5;
                }
                //password has 2 symbols
                if (password.match(/(.*[!,@,#,$,%,^,&,*,?,_,~].*[!,@,#,$,%,^,&,*,?,_,~])/)) {
                    score += 5;
                }
                //password has Upper and Lower chars
                if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
                    score += 10;
                }
                //password has number and chars
                if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) {
                    score += 15;
                }
                //
                //password has number and symbol
                if (password.match(/([!,@,#,$,%,^,&,*,?,_,~])/) && password.match(/([0-9])/)) {
                    score += 15;
                }
                //password has char and symbol
                if (password.match(/([!,@,#,$,%,^,&,*,?,_,~])/) && password.match(/([a-zA-Z])/)) {
                    score += 15;
                }
                //password is just a numbers or chars
                if (password.match(/^\w+$/) || password.match(/^\d+$/)) {
                    score -= 10;
                }
                //verifying 0 < score < 100
                if (score < 0) {
                    score = 0;
                }
                if (score > 100) {
                    score = 100;
                }
                if (score < 40) {
                    this.resultStyle = option.badPass;
                    return $(this).badPass;
                }
                if (score < 75) {
                    this.resultStyle = option.goodPass;
                    return $(this).goodPass;
                }
                this.resultStyle = option.strongPass;
                return $(this).strongPass;
            };
        });
    };
})(jQuery);
$.fn.checkRepetition = function (pLen, str) {
    var res = "";
    for (var i = 0; i < str.length; i++) {
        var repeated = true;
        for (var j = 0; j < pLen && (j + i + pLen) < str.length; j++) {
            repeated = repeated && (str.charAt(j + i) == str.charAt(j + i + pLen));
        }
        if (j < pLen) {
            repeated = false;
        }
        if (repeated) {
            i += pLen - 1;
            repeated = false;
        } else {
            res += str.charAt(i);
        }
    }
    return res;
};