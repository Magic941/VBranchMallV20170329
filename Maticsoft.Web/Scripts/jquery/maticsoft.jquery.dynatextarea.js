/*
* File:        maticsoft.jquery.dynatextarea.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2012 Maticsoft. All Rights Reserved.
*/
;
(function ($) {
    $.dynatextarea = function (target, maxlength, progressbar) {
        $(target).bind("keydown keyup focus", function () {
            textCounter($(this), progressbar.attr('id'), maxlength);
        });
        textCounter($(target), progressbar.attr('id'), maxlength);
    };

    function textCounter(field, counter, maxlimit, linecounter) {
        // text width//
        //            var fieldWidth = parseInt(field.offsetWidth);
        var fieldHeight = parseInt(field.height());
        var charcnt = field.val().length;
        // trim the extra text
        if (charcnt > maxlimit) {
            field.val(field.val().substring(0, maxlimit));
        } else {
            // progress bar percentage
            var percentage = parseInt(100 - ((maxlimit - charcnt) * 100) / maxlimit);
            //                document.getElementById(counter).style.width = parseInt((fieldWidth * percentage) / 100) + "px";
            document.getElementById(counter).style.height = parseInt((fieldHeight * percentage) / 100) + "px";
            //                document.getElementById(counter).innerHTML = percentage + "%";
            // color correction on style from CCFFF -> CC0000
            setcolor(document.getElementById(counter), percentage, "background-color");
        }
    }

    function setcolor(obj, percentage, prop) {
        obj.style[prop] = "rgb(80%," + (100 - percentage) + "%," + (100 - percentage) + "%)";
    }
})(jQuery);
