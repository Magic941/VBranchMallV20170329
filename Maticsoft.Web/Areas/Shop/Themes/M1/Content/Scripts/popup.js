(function() {
    $.fn.popup = function(options) {
        var defaults = {
            close: "false",
            callback: ""
        }
        var options = $.extend(defaults, options);
        this.each(function() {
            var $pobj = $(this);
            var $close = $(this).find(".dclose") var $mask = jQuery("<div class='mask'></div>") var dialog = {
                dclose: function() {
                    //$mask.remove();
                    $('div.mask').remove();
                    $pobj.hide();
                    if (options.callback) {
                        options.callback();
                    };
                },
                dcontent: function() {
                    $mask.appendTo(document.body);
                    $mask.css({
                        display: "block",
                        opacity: ".30",
                        zIndex: "100",
                        filter: "Alpha(Opacity=30)",
                        background: "#000",
                        position: "absolute",
                        top: "0",
                        left: "0",
                        width: "100%",
                        height: "100%"
                    }).width($('body').width()).height($(document).height());
                    var dml = $pobj.outerWidth() / 2;
                    var dmt = $pobj.outerHeight() / 2 - $(window).scrollTop();
                    $pobj.show().css({
                        marginLeft: -dml,
                        marginTop: -dmt,
                        top: "50%",
                        zIndex: "101",
                        position: "absolute",
                        left: "50%"
                    });
                }
            };
            if (options.close == true) {
                dialog.dclose();
                return false;
            };
            dialog.dcontent();
            $close.click(function() {
                dialog.dclose();
                return false;
            });
            $mask.click(function() {
                dialog.dclose();
            });
            $(window).resize(function() {
                $mask.width($('body').width()).height($(document).height());
            }) return false;

        })
    }
})(jQuery)