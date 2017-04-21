/**
* dialog (Version 1.3)
* 
*
* Create a Dialog
* @example new Dialog(options);
* on Jquery
*
*/
function Dialog(options, callback, obj) {
    this.callback = callback;
    this.obj = obj;
    this.options = $.extend({
        id: '',
        trigger: '',
        header: '',
        //header区域HTML
        title: '',
        //修改显示标题
        body: '',
        //内容区域HTML
        footer: '',
        //footer区域HTML
        width: 420,
        //宽度设置
        height: '',
        //高度设置,默认自适应
        mask: 1 //0或false为遮罩层不显示
    },
    options);
    this.ie6 = $.browser.msie && $.browser.version == 6;
}
Dialog.prototype = {
    open: function () {
        var op = this.options;
        if (this.dialog === undefined) {
            this._creatDialog();
            this.center(this.dialog);
            return;
        } else {
            this.dialog.css({
                visibility: 'visible'
            });
            if (op.mask) {
                this._setMaskHeight(this.mask);
                this.mask.css({
                    visibility: 'visible'
                });
                if (this.ie6) {
                    this._setMaskHeight(this.ifr);
                    this.ifr.css({
                        visibility: 'visible'
                    });
                }
            } else if (this.ie6) {
                this._setMaskHeight(this.ifr);
                this.ifr.css({
                    visibility: 'visible'
                });
            }
        }
    },
    _creatDialog: function () {
        var that = this,
        op = this.options,
        basicHTML = '<div class="J_dialog" id=' + op.id + '><div></div></div>';
        this.dialog = $('<div class="J_dialog" id=' + op.id + '></div>');
        var dhd = op.header ? this._creatHTML('J_dialog_hd', op.header) : this._creatHTML('J_dialog_hd', '<h3 class="J_dialog_title">' + op.title + '</h3><a href="javascript:;" class="J_dialog_close">关闭</a>');
        var dbd = this._creatHTML('J_dialog_bd', op.body);
        var dft = this._creatHTML('J_dialog_ft', op.footer);
        var dcontent = '<div class="J_dialog_content" style="width:' + op.width + 'px;height:' + op.height + 'px">' + dhd + dbd + dft + '</div><div class="J_dialog_shadow"></div>';
        this.dialog.append(dcontent).appendTo('body');
        this.dialog.find('.J_dialog_close').live('click',
        function () {
            that.close()
        });
        if (op.mask) {
            this.mask = $(this._creatHTML('J_dialog_mask', ''));
            this._setMaskHeight(this.mask);
            this.mask.appendTo('body');
            if (this.ie6) {
                this.ifr = $('<iframe class="J_dialog_ifr" style="top:0;left:0;width:100%;"></iframe>');
                this.ifr.appendTo('body');
                this._setMaskHeight(this.ifr);
            }
        } else if (this.ie6) {
            this.ifr = $('<iframe class="J_dialog_ifr" style="top:50%;left:50%;"></iframe>');
            var DW = this.dialog.outerWidth();
            var DH = this.dialog.outerHeight();
            this.ifr.css({
                width: DW,
                height: DH
            }).appendTo('body');
            this.center(this.ifr);
        }
    },
    _creatHTML: function (className, html) {
        return '<div class="' + className + '">' + html + '</div>';
    },
    setTitle: function (html) {
        this.dialog.find('.J_dialog_title').html(html);
    },
    setHeader: function (html) {
        this.dialog.find('.J_dialog_hd').html(html);
    },
    setBody: function (html) {
        this.dialog.find('.J_dialog_bd').html(html);
    },
    setFooter: function (html) {
        this.dialog.find('.J_dialog_ft').html(html);
    },
    close: function () {
        var cb = this.callback;
        $('.J_dialog_mask').css({
            visibility: 'hidden',
            height: 0
        });
        if (this.ie6) {
            this.ifr.css({
                visibility: 'hidden',
                height: 0
            });
        }
        this.dialog.css({
            visibility: 'hidden'
        });
        if (cb) {
            cb.call(this, this.obj);
        }
    },
    center: function (node) {
        var node = node || this.dialog;
        var offsetTop = parseInt(node.outerHeight() / 2);
        var offsetLeft = parseInt(node.outerWidth() / 2);
        if (this.ie6) {
            offsetTop = offsetTop - $(window).scrollTop();
        }
        node.css({
            marginTop: -offsetTop,
            marginLeft: -offsetLeft
        });
    },
    setWidth: function (width) {
        this.dialog.find('.J_dialog_content').width(width);
    },
    _setMaskHeight: function (element) {
        var DocHeight = $(document).height();
        element.height(DocHeight);
    }
}