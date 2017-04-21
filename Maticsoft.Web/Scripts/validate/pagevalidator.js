/*
* To Run This Code jQuery 1.3.2(http://www.jQuery.com) Required.
*/
//base class for a validator
function Validator(srcID, empty, msgOnTip, msgOnErr, msgOnEmpty, onValid) {
    // 2007-12-7 by jeffery
    // 不使用默认的消息
    //
    this.msgOnTip = msgOnTip;
    this.msgOnError = msgOnErr;
    this.msgOnEmpty = msgOnEmpty;
    this._parent = srcID;
    this.empty = empty;

    if (null != onValid && undefined != onValid)
        this.onValid = onValid;

    this.isValid = false;
}

//note: tip panel must with these format : valid control id and "Tip"
Validator.prototype.updateShow = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    var tip = $("#" + srcCTLWithJQuery.attr("id") + "Tip");

    if (!this.isValid) {
        tip.html(this.msgOnError);
        tip.removeClass();
        tip.addClass("msgError");
    }
    else {
        if (null != this.onValid)
            this.onValid(srcCTLWithJQuery);

        if (this.empty && srcCTLWithJQuery.val().length == 0) {
            // 2007-12-7 by jeffery
            // 如果没有设置当前为空的提示信息，则使用第一个验证类型的为空提示信息
            // 主要用在append多个验证的时候
            //
            //            if (this.msgOnEmpty == null)
            //                this.msgOnEmpty = srcCTLWithJQuery.validator[0].msgOnEmpty;

            tip.html(this.msgOnEmpty);
        }
        else {
            tip.html(srcCTLWithJQuery.attr("alt"));
        }

        tip.removeClass();
        tip.addClass("msgOK");
    }
}

function SelectValidator(srcID, empty, msgOnErr, msgOnEmpty, onValid) {
    this._base = Validator;
    this._base(srcID, empty, null, msgOnErr, msgOnEmpty, onValid);

    if (empty) {
        this.isValid = true;
    }
}
SelectValidator.prototype.updateShow = Validator.prototype.updateShow;
SelectValidator.prototype.valid = function () {
    if (this.empty)
        this.isValid = true;
    else {
        var isValid = true;
        var srcCTLWithJQuery = $("#" + this._parent);
        var groupname = srcCTLWithJQuery.attr("groupname");
        var ctls = $("select[groupname='" + groupname + "']");

        ctls.each(function () {
            if (this.options.length > 0) {
                if (this.value == "" || this.value == "-1")
                    isValid = false;
                else
                    isValid = isValid && true;
            }
            else {
                isValid = isValid && true;
            }
        });
        this.isValid = isValid;
    }
}

//this class represent checking a input element inherit from Validator
function InputValidator(srcID, min, max, empty, regex, msgOnTip, msgOnErr, msgOnEmpty, onValid) {
    this._base = Validator;
    this._base(srcID, empty, msgOnTip, msgOnErr, msgOnEmpty, onValid);

    this.min = min;
    this.max = max;
    this.regex = regex;

    if (empty) {
        this.isValid = true;
    }
}
InputValidator.prototype.updateShow = Validator.prototype.updateShow;
InputValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    var val = srcCTLWithJQuery.val();
    var len = val.length;

    // 2007-12-21 by jeffery
    // 如果输入框不能为空且没有输入任何内容，则直接验证失败
    //
    if (!this.empty && (val.length == 0)) {
        this.isValid = false;
        return;
    }

    //check input length
    //    for ( var i = 0 ; i<val.length  ; i++ )
    //    {
    //        // 如果是汉字则占两字节
    //        //
    //        if ( val.charCodeAt(i)>=0x4e00 && val.charCodeAt(i)<= 0x9fa5 )
    //            len+=2;
    //        else
    //            len++;
    //    }

    if (val.length == 0 && this.empty == true) {
        this.isValid = true;
    }
    else if ((len < this.min) || ((this.max > 0) && (len > this.max))) {
        this.isValid = false;
    }
    //check with regexpression
    else if (this.regex != null && this.regex != undefined && typeof this.regex == "string" && this.regex != "") {
        var exp = new RegExp("^" + this.regex + "$", "i");
        // 2007-12-7 by jeffery
        // 改为直接将匹配结果赋值给isValid
        //
        this.isValid = exp.test(val);
    }
    else {
        this.isValid = true;
    }
}

//this class represent compare valid
function CompareValidator(srcID, desID, msgOnErr) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, null);

    this._compare = desID;
}
CompareValidator.prototype.updateShow = Validator.prototype.updateShow;
CompareValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    var desCTLWithJQuery = $("#" + this._compare);

    // 2007-12-7 by jeffery
    // 直接将比较结果赋值给isValid
    //
    this.isValid = (srcCTLWithJQuery.val() == desCTLWithJQuery.val());
}

// 2007-12-05 by jeffery
// 添加整数值范围的验证类型
//
function NumberRangeValidator(srcID, minValue, maxValue, msgOnErr, msgOnValid) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, msgOnValid);
    this._minValue = minValue;
    this._maxValue = maxValue;
}
NumberRangeValidator.prototype.updateShow = Validator.prototype.updateShow;
NumberRangeValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    if (srcCTLWithJQuery.val().length == 0) {
        this.isValid = true;
    }
    else {
        var num = parseInt(srcCTLWithJQuery.val());
        this.isValid = ((num >= this._minValue) && (num <= this._maxValue));
    }
}

// 添加金额范围的验证类型
//
function MoneyRangeValidator(srcID, minValue, maxValue, msgOnErr, msgOnValid) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, msgOnValid);

    this._minValue = minValue;
    this._maxValue = maxValue;
}
MoneyRangeValidator.prototype.updateShow = Validator.prototype.updateShow;
MoneyRangeValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    if (srcCTLWithJQuery.val().length == 0) {
        this.isValid = true;
    }
    else {
        var num = parseFloat(srcCTLWithJQuery.val());
        this.isValid = ((num >= this._minValue) && (num <= this._maxValue));
    }
}


//this class represent ajax valid
function AjaxValidator(srcID, url, msgOnErr, ajaxCallback) {
    this.base = Validator;
    this.base(srcID, true, null, msgOnErr, null);

    this.url = url;
    this.isAjax = true;

    if (null != ajaxCallback)
        this.callback = ajaxCallback;
}
AjaxValidator.prototype.updateShow = Validator.prototype.updateShow;
AjaxValidator.prototype.valid = function () {
    var srcCTLWithJQuery = $("#" + this._parent);
    var tip = $("#" + srcCTLWithJQuery.attr("id") + "Tip");

    tip.html("loading...");
    tip.removeClass();
    tip.addClass("msgAjaxing");

    srcCTLWithJQuery.get(0).ajaxvalid = this;
    $.ajax({
        type: "POST",
        url: this.url,
        data: srcCTLWithJQuery.attr("name") + "=" + srcCTLWithJQuery.val(),
        success: function (data) {
            var obj = eval("({value:" + data + "})");
            var t = srcCTLWithJQuery.get(0).ajaxvalid;

            if (undefined != t.callback) {
                t.callback(obj, t);
            }
            else {
                if (obj.value)
                    t.isValid = true;
                else
                    t.isValid = false;
            }

            t.updateShow();
        }
    });
}

//function represent init a element need to valid
function initValid(validator) {
    var srcID = validator._parent;
    var srcCTLWithJQuery = $("#" + srcID);

    // 2008-09-12 by jeffery 判断目标控件是否为空，为空的话就不进行初始化
    if (srcCTLWithJQuery == null || srcCTLWithJQuery.get(0) == null)
        return;

    var srcTag = srcCTLWithJQuery.get(0).tagName;
    var arrayValidator = new Array();
    arrayValidator.push(validator);
    srcCTLWithJQuery.get(0).validator = arrayValidator;
    var tip = $("#" + srcID + "Tip");
    tip.html(srcCTLWithJQuery.attr("description"));
    tip.addClass("msgNormal");


    if (srcTag == "INPUT" || srcTag == "TEXTAREA") {

        var type = srcCTLWithJQuery.attr("type");
        if (type == "text" || type == "password" || type == "file" || srcTag == "TEXTAREA") {
            var defaultVal = srcCTLWithJQuery.attr("value");
            if (null != defaultVal && defaultVal != undefined && defaultVal != "") {
                validator.isValid = true;
            }
            srcCTLWithJQuery.focus(function () {
                var tip = $("#" + this["id"] + "Tip");
                tip.html(this.validator[0].msgOnTip);
                tip.removeClass();
                tip.addClass("msgOnFocus");
            });
            srcCTLWithJQuery.blur(function () {
                for (var i = 0; i < this.validator.length; i++) {
                    this.validator[i].valid();
                    if (this.validator[i].isAjax == null || this.validator[i].isAjax == undefined) {
                        this.validator[i].updateShow();
                        if (!this.validator[i].isValid)
                            break;
                    }
                }
            });
        } else if (type == "checkbox" || type == "radio") {
            var ctls = $("input[name=" + srcCTLWithJQuery.attr("name") + "]");
            var defaultVal = srcCTLWithJQuery.attr("checkedValue");

            if (null != defaultVal && defaultVal != undefined) {
                ctls.each(function () {
                    if (this.value == defaultVal) {
                        this.checked = "checked";
                        validator.isValid = true;
                    }
                });
            }
            ctls.bind("click", function () {
                var val;
                if (this.validator == undefined) {
                    val = ctls.get(0).validator[0];
                }
                else
                    val = this.validator[0];
                val.isValid = true;
                val.updateShow();
            });
        }
    } else if (srcTag == "SELECT") {
        var groupname = srcCTLWithJQuery.attr("groupname");
        var ctls = $("select[groupname='" + groupname + "']");

        // 2007-12-7 by jeffery
        // 如果初始化的时候有选中的值，则将isValid设为true
        //
        if (srcCTLWithJQuery.val() != null && srcCTLWithJQuery.val() != "" && srcCTLWithJQuery.val() != "-1")
            validator.isValid = true;

        ctls.each(function () {
            var defaultVal = $(this).attr('selectedValue');
            if (null != defaultVal && defaultVal != undefined) {
                $.each(this.options, function () {
                    if ($.trim(this.value) == $.trim(defaultVal) || this.text == defaultVal) {
                        this.selected = true;
                    }
                });
                validator.isValid = true;
            }
        });

        ctls.bind("change", function () {
            var validators = ctls.get(0).validator;
            for (var i = 0; i < validators.length; i++) {
                if (validators[i].isAjax == null || validators[i].isAjax == undefined) {
                    validators[i].valid();
                    validators[i].updateShow();

                    if (!validators[i].isValid)
                        break;
                }
                else {
                    if (this.id == srcID)
                        validators[i].valid();
                }
            }
        });
    }
}

function appendValid(validator) {
    var srcCTLWithJQuery = $("#" + validator._parent).get(0);
    if (srcCTLWithJQuery.validator == undefined)
        srcCTLWithJQuery.validator = new Array();
    // 2007-12-07 by jeffery
    // 追加的验证在初始化时默认为true
    //
    validator.isValid = true;
    srcCTLWithJQuery.validator.push(validator);
}

// 2007-12-7 by jeffery
// 添加验证分组
//
function PageIsValid() {
    var isValid = true;
    var validateGroup = "default"; // 默认分组

    if (arguments.length > 0)
        validateGroup = arguments[0];

    var ctls = $("[ValidateGroup='" + validateGroup + "']");
    ctls.each(function () {
        if ($("#" + this["id"]).get(0).validator != undefined && $("#" + this["id"]).get(0).validator != null) {
            for (var i = 0; i < $("#" + this["id"]).get(0).validator.length; i++) {
                if ($("#" + this["id"]).get(0).validator[i].isValid == false) {
                    $("#" + this["id"]).get(0).validator[i].updateShow();
                    isValid = false;
                }
            }
        }
    });
    return isValid;
}

//function ValidatePage(validationGroups) {
//    var list = validationGroups.split('&');
//    for (var i = 0; i < Page_Validators.length; i++) {
//        var validator = Page_Validators[i];
//        if ((validator.validationGroup && ExistsGroup(list, validator.validationGroup))
//            || (!validator.validationGroup && ExistsGroup(list, ''))) {
//            ValidatorValidate(validator, validator.validationGroup);
//            Page_IsValid = Page_IsValid && validator.isvalid;
//        }
//        else {
//            validator.isvalid = true;
//            ValidatorUpdateDisplay(validator);
//        }
//    }
//    //ValidationSummary
//    for (var i = 0; i < list.length; i++) {
//        ValidationSummaryOnSubmit(list[i]);
//    }
//    Page_BlockSubmit = !Page_IsValid;

//    return Page_IsValid;
//}
//function ExistsGroup(list, group) {
//    var found = false;
//    for (i = 0; i < list.length; i++) {
//        if (list[i] == group) {
//            found = true;
//            break;
//        }

//    }
//    return found;
//}
