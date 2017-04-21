/*
* File:        maticsoft.jqueryui.tip.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2012 Maticsoft. All Rights Reserved.
*/
(function ($) {
    $.tipHighlight = function (send, effect, options, callback) {
        return send.highlightStyle().hide().show(effect, options, 500, callback);
    };

    $.tipAlert = function (send, effect, options, callback) {
        return send.alertStyle().hide().show(effect, options, 500, callback);
    };

    var infoTitle = '提示 : ';
    var alertTitle = '警告 : ';

    $.fn.highlightStyle = function (message) {
        if (message === undefined) {
            message = $(this).text().replace(infoTitle, '');
        }
        var id = $(this).attr('id');
        $(this).replaceWith(function (i, html) {
            var StyledHighlight = "<div id=\"" + id + "\" class=\"ui-state-highlight ui-corner-all BenAlertMode\" style=\"margin-top: .2em; padding: 0 .7em;\">";
            StyledHighlight += "<p><span class=\"ui-icon ui-icon-info\" style=\"float: left;margin-top: .45em; margin-right: .3em;\">";
            StyledHighlight += "</span><strong>" + infoTitle + "</strong>";
            StyledHighlight += message;
            StyledHighlight += "</p></div>";
            return StyledHighlight;
        });
        return $("#" + id);
    };

    $.fn.alertStyle = function (message) {
        if (message === undefined) {
            message = $(this).text().replace(infoTitle, '');
        }
        var id = $(this).attr('id');
        this.replaceWith(function (i, html) {
            var StyledError = "<div id=\"" + id + "\" class=\"ui-state-error ui-corner-all BenAlertMode\" style=\"margin-top: .2em;padding: 0 .7em;\">";
            StyledError += "<p><span class=\"ui-icon ui-icon-alert\" style=\"float:margin-top: .45em; left; margin-right: .3em;\">";
            StyledError += "</span><strong>" + alertTitle + "</strong>";
            StyledError += message;
            StyledError += "</p></div>";
            return StyledError;
        });
        return $("#" + id);
    };
})(jQuery);
