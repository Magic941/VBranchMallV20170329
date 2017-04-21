(function() {
    var formformat = {
        addfocus: function(fobject) {
            $(fobject).focus(function() {
                $(this).addClass("focus");
            }) $(fobject).blur(function() {
                $(this).removeClass("focus");
            })
        },
        radioInt: function(fobject) {
            //var $radioItem=$(fobject).find(".f-radio-item");
            $(fobject).click(function() {
                var radioName = $(this).find("input").attr("name");
                $(fobject).find("input[name=" + radioName + "]").parent().removeClass("focus");
                $(this).addClass("focus");
            }) $(fobject).hover(function() {
                $(this).addClass("hover");
            },
            function() {
                $(this).removeClass("hover");
            })
        },
        int: function() {
            var $inputText = $("form.form").find("input:text,textarea"),
            $radioItems = $(".f-radio-item");
            if ($.browser.msie && $.browser.version < 8) {
                formformat.addfocus($inputText);
            }
            var radioImage = new Image();
            radioImage.src = "../images/radio-bg.gif";
            radioImage.onerror = function() {
                $radioItems.removeClass("f-radio-item");
            }
            radioImage.onload = function() {
                formformat.radioInt($radioItems);
            };

        }
    }
    formformat.int();
})()