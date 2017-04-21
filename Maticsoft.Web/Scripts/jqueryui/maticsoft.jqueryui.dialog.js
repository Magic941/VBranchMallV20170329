/*
* File:        maticsoft.jqueryui.dialog.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2012 Maticsoft. All Rights Reserved.
*/
(function ($) {
    if ($.ui) {
        $.info = function (message, options) {
            var defaultOptions = {
                title: '提示信息',
                open: function () {
                    $(this).find("span").each(function (i) {
                        if (this.innerHTML == "close") {
                            this.style.display = "none";
                        }
                    });
                },
                icon: 'info',
                defaultResult: ''
            };

            if (options)
                defaultOptions = $.extend(defaultOptions, options);

            var $dialog = $('#infoDialog');
            $dialog.remove();

            $dialog = $("<div id='infoDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#infoMessage').remove();
            $("<p id='infoMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#infoDialog');

            $dialog.dialog({
                resizable: false,
                height: 'auto',
                width: $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5),
                bgiframe: true,
                modal: true
            });
        };

        $.prompt = function (message, options) {
            var defaultOptions = {
                title: 'Prompt',
                icon: 'help',
                defaultResult: '',
                buttons: {
                    "Ok": function () { $(this).dialog("close"); },
                    Cancel: function () { $(this).dialog("close"); }
                }
            };

            if (options)
                defaultOptions = $.extend(defaultOptions, options);

            var $dialog = $('#promptDialog');
            $dialog.remove();

            $dialog = $("<div id='promptDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#promptMessage').remove();
            $("<p id='promptMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#promptDialog');
            $('<hr />').appendTo('#promptMessage');
            $("<input id='result' type='textbox' style='width:100%' value='" + defaultOptions.defaultResult + "' />").appendTo('#promptMessage');

            $dialog.dialog({
                resizable: false,
                height: 'auto',
                width: $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5),
                bgiframe: true,
                modal: true,
                buttons: defaultOptions.buttons
            });
        };

        $.confirm = function (message, options) {
            var defaultOptions = {
                height: 'auto',
                width: $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5),
                title: 'Confirm',
                icon: 'help',
                buttons: {
                    "Yes": function () { $(this).dialog("close"); },
                    "No": function () { $(this).dialog("close"); },
                    Cancel: function () { $(this).dialog("close"); }
                }
            };

            if (options)
                defaultOptions = $.extend(defaultOptions, options);

            var $dialog = $('#confirmDialog');
            $dialog.remove();

            $dialog = $("<div id='confirmDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#confirmMessage').remove();
            $("<p id='confirmMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#confirmDialog');

            $dialog.dialog({
                resizable: false,
                height: defaultOptions.height,
                width: defaultOptions.width,
                bgiframe: true,
                modal: true,
                buttons: defaultOptions.buttons
            });
        };

        $.alert = function (message, options) {
            var defaultOptions = {
                title: '提示信息',
                icon: 'alert',
                exception: '',
                stack: '',
                buttons: { "确定": function () { $(this).dialog("close"); } }
            };

            if (options) {
                defaultOptions = $.extend(defaultOptions, options);
            }

            var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5) : 'auto';

            var $dialog = $('#alertDialog');
            $dialog.remove();

            $dialog = $("<div id='alertDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#alertMessage').remove();
            $("<p id='alertMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#alertDialog');

            $('#alertException').remove();
            if (defaultOptions.exception != '') {
                if ('' != defaultOptions.stack) {
                    $("<div id='alertException'><hr /></div>").appendTo('#alertDialog');
                    $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo('#alertException');
                    // Max width
                    dlgWidth = '960';

                } else {
                    $("<div id='alertException'></div>").appendTo('#alertDialog');
                    $("<p >" + defaultOptions.exception + "</p>").appendTo('#alertException');
                }
            }

            $dialog.dialog({
                resizable: false,
                height: 'auto',
                width: dlgWidth,
                bgiframe: true,
                modal: true,
                buttons: defaultOptions.buttons
            });
        };

        $.alertEx = function (message, funEx) {
            var defaultOptions = {
                title: '提示信息',
                icon: 'alert',
                exception: '',
                stack: '',
                buttons: { "确定": function () {
                    $(this).dialog("close");
                    funEx();
                }
                }
            };

            var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5) : 'auto';

            var $dialog = $('#alertDialog');
            $dialog.remove();

            $dialog = $("<div id='alertDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#alertMessage').remove();
            $("<p id='alertMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#alertDialog');

            $('#alertException').remove();
            if (defaultOptions.exception != '') {
                if ('' != defaultOptions.stack) {
                    $("<div id='alertException'><hr /></div>").appendTo('#alertDialog');
                    $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo('#alertException');
                    // Max width
                    dlgWidth = '960';

                } else {
                    $("<div id='alertException'></div>").appendTo('#alertDialog');
                    $("<p >" + defaultOptions.exception + "</p>").appendTo('#alertException');
                }
            }

            $dialog.dialog({
                resizable: false,
                height: 'auto',
                width: dlgWidth,
                bgiframe: true,
                modal: true,
                buttons: defaultOptions.buttons
            });
        };

        $.alertError = function (message, options) {
            var defaultOptions = {
                title: '提示信息',
                icon: 'alert',
                exception: '',
                stack: '',
                buttons: { "确定": function () { $(this).dialog("close"); } }
            };

            if (options) {
                defaultOptions = $.extend(defaultOptions, options);
            }

            var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? $(window).width() * (($(window).width() > 1024) ? 0.25 : 0.5) : 'auto';

            var $dialog = $('#alertErrorDialog');
            $dialog.remove();

            $dialog = $("<div id='alertErrorDialog' style='display:hidden;' title='" + defaultOptions.title + "'></div>").appendTo('body');

            $('#alertErrorMessage').remove();
            $("<p id='alertErrorMessage'><span class='ui-icon ui-icon-" + defaultOptions.icon + "' style='float:left; margin:0px 10px 20px 0;'></span>" + message + "</p>").appendTo('#alertErrorDialog');

            $('#alertErrorException').remove();
            if (defaultOptions.exception != '') {
                if ('' != defaultOptions.stack) {
                    $("<div id='alertErrorException'><hr /></div>").appendTo('#alertErrorDialog');
                    $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo('#alertErrorException');
                    dlgWidth = '960';

                } else {
                    $("<div id='alertErrorException'></div>").appendTo('#alertErrorDialog');
                    $("<p >" + defaultOptions.exception + "</p>").appendTo('#alertErrorException');
                }
            }

            $dialog.dialog({
                resizable: false,
                height: 'auto',
                width: dlgWidth,
                bgiframe: true,
                modal: true,
                buttons: defaultOptions.buttons
            });
        };
    }

    $.frameTopDialog = function (title, src, width, height) {
        var defaultOptions = {
            exception: '',
            stack: ''
        };
        var frame = '<iframe width="' + width + '" height="' + height + '"src="' + src + '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe>';

        var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? parseInt(width) + 20 : 'auto';

        var topWindow = $(window.top.document);
        //        if (topWindow.length == 0) {
        //            topWindow = $(window.document);
        //        }

        var $dialog = topWindow.find('#frameTopDialog');
        $dialog.remove();

        $dialog = $("<div id='frameTopDialog' style='display:hidden;' title='" + title + "'></div>").appendTo(topWindow.find('body'));

        topWindow.find('#frameTopMessage').remove();
        $("<p id='frameTopMessage'>" + frame + "</p>").appendTo(topWindow.find('#frameTopDialog'));

        topWindow.find('#frameTopException').remove();
        if (defaultOptions.exception != '') {
            if ('' != defaultOptions.stack) {
                $("<div id='frameTopException'><hr /></div>").appendTo(topWindow.find('#frameTopDialog'));
                $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo(topWindow.find('#frameTopException'));
                dlgWidth = '960';
            } else {
                $("<div id='frameTopException'></div>").appendTo(topWindow.find('#frameTopDialog'));
                $("<p >" + defaultOptions.exception + "</p>").appendTo(topWindow.find('#frameTopException'));
            }
        }

        $dialog.dialog({
            resizable: false,
            height: 'auto',
            width: dlgWidth,
            bgiframe: true,
            modal: true
        });
    };

    $.frameDialog = function (title, src, width, height) {
        var defaultOptions = {
            exception: '',
            stack: ''
        };

        var frame = '<iframe width="' + width + '" height="' + height + '"src="' + src + '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe>';

        var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? parseInt(width) + 20 : 'auto';

        var $dialog = $('#frameDialog');
        $dialog.remove();

        $dialog = $("<div id='frameDialog' style='display:hidden;' title='" + title + "'></div>").appendTo('body');

        $('#frameMessage').remove();
        $("<p id='frameMessage'>" + frame + "</p>").appendTo('#frameDialog');

        $('#frameException').remove();
        if (defaultOptions.exception != '') {
            if ('' != defaultOptions.stack) {
                $("<div id='frameException'><hr /></div>").appendTo('#frameDialog');
                $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo('#frameException');
                dlgWidth = '960';
            } else {
                $("<div id='frameException'></div>").appendTo('#frameDialog');
                $("<p >" + defaultOptions.exception + "</p>").appendTo('#frameException');
            }
        }

        $dialog.dialog({
            resizable: false,
            height: 'auto',
            width: dlgWidth,
            bgiframe: true,
            modal: true
        });
    };
    
    $.domTopDialog = function (title, dom, width) {
        var defaultOptions = {
            exception: '',
            stack: ''
        };
        var dlgWidth = $.browser.msie && (($.browser.version == "6.0") || ($.browser.version == "7.0")) ? parseInt(width) + 20 : 'auto';

        var topWindow = $(window.top.document);
        //        if (topWindow.length == 0) {
        //            topWindow = $(window.document);
        //        }

        var $dialog = topWindow.find('#domTopDialog');
        $dialog.remove();

        $dialog = $("<div id='domTopDialog' style='display:hidden;' title='" + title + "'></div>").appendTo(topWindow.find('body'));

        topWindow.find('#domFrameTopMessage').remove();
        $("<p id='domFrameTopMessage'>" + dom + "</p>").appendTo(topWindow.find('#domTopDialog'));

        topWindow.find('#domFrameTopException').remove();
        if (defaultOptions.exception != '') {
            if ('' != defaultOptions.stack) {
                $("<div id='domFrameTopException'><hr /></div>").appendTo(topWindow.find('#domTopDialog'));
                $("<p ><strong>" + defaultOptions.exception + "</strong> " + defaultOptions.stack + "</p>").appendTo(topWindow.find('#domFrameTopException'));
                dlgWidth = '960';
            } else {
                $("<div id='domFrameTopException'></div>").appendTo(topWindow.find('#domTopDialog'));
                $("<p >" + defaultOptions.exception + "</p>").appendTo(topWindow.find('#domFrameTopException'));
            }
        }

        $dialog.dialog({
            resizable: false,
            height: 'auto',
            width: dlgWidth,
            bgiframe: true,
            modal: true
        });
    };
})(jQuery);
