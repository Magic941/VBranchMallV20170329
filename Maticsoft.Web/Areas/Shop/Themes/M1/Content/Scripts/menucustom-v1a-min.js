function MenuCustom(a, b, c, d) {
    this.max = c;
    this.wrapId = a;
    this.menuSet = b;
    this.html = d || '<div class="box link-set none" id="J_menucontent" style="overflow:hidden;"><div class="hd pb5 mb10"><div><div class="h2">\u8bbe\u7f6e\u5e38\u7528\u94fe\u63a5</div><span class="c9">(\u6700\u591a\u53ef\u4ee5\u8bbe\u7f6e5\u4e2a)</span></div><div class="act"><i class="menu-close" id="J_menu_close">\u5173\u95ed</i></div></div><div class="bd"><div class="menu-loading tc pt30 pb30">\u52a0\u8f7d\u4e2d...<img src="/Areas/Shop/Themes/M1/Content/images/loading.gif" class="vm ml10"></div></div><div class="ft pt10 tc"><div class="link-set-btn pt10"><a class="btn" href="#" id="J_menu_btn"><span>\u786e&nbsp;&nbsp;\u5b9a</span></a></div></div></div>';
    this.selected = [];
    this.init()
}
MenuCustom.prototype = {
    init: function () {
        var a = this;
        this.getSelected();
        $(this.menuSet).click(function () {
            a.createMenuBox()
        })
    },
    bindEdit: function () {
        var a = this;
        $("#J_menu_close").click(function () {
            a.hideContent()
        });
        $("#J_menu_btn").click(function () {
            var b = [];
            $("#J_menucontent div.bd a.cur").each(function () {
                b.push($(this).attr("menuid"))
            });
            a.setMenu(b);
            return false
        });
        $("#J_menucontent div.bd").click(function (b) {
            b = b.target;
            var c = b.className;
            if (b.tagName.toLowerCase() === "a") if (c !== "disable") {
                b.className = c === "cur" ? "" : "cur";
                a.checkSelected()
            }
        });
        $("#J_menucontent").click(function (b) {
            b.stopPropagation()
        })
    },
    getSelected: function () {
        var a = this;
        $.ajax({
            type: "get",
            url: "http://member.maticsoft.com/Service/AjaxMenuService.ashx",
            data: "Method=GetMemberMenu",
            dataType: "jsonp",
            success: function (b) {
                a.selected = [];
                var c = a.createSelected(b);
                $(a.wrapId + " .bd").fadeOut(200,
                function () {
                    $(this).empty().append(c).fadeIn(300)
                })
            }
        })
    },
    getMenu: function () {
        var a = this;
        $.ajax({
            type: "get",
            url: "http://member.maticsoft.com/Service/AjaxMenuService.ashx",
            data: "Method=GetMemberCenterMenu",
            dataType: "jsonp",
            success: function (b) {
                b = a.createMenu(b);
                $("#J_menucontent div.menu-loading").replaceWith(b)
            }
        })
    },
    setMenu: function (a) {
        var b = this;
        a = a.join();
        var c = this.selected.sort(function (d, e) {
            return d - e
        }).join();
        if (a === c) b.hideContent();
        else {
            this.selected = a.split(",");
            $.ajax({
                type: "get",
                url: "http://member.maticsoft.com/Service/AjaxMenuService.ashx",
                data: "Method=SetMemebrMenu&ids=" + a,
                dataType: "jsonp",
                success: function (d) {
                    if (d.IsSuccess) {
                        b.getSelected();
                        b.hideContent()
                    } else alert(d.Msg)
                }
            })
        }
    },
    createSelected: function (a) {
        if (a.length === 0) return "";
        a = a.sort(function (d, e) {
            return d.MenuId - e.MenuId
        });
        var b = '<ul class="sub-nav-list">';
        i = 0;
        for (len = a.length; i < len && i < this.max; i++) {
            var c = a[i];
            this.selected.push(c.MenuId);
            b += '<li><a title="' + c.Title + '" href="' + c.Link + '">';
            b += c.Title + "</a></li>"
        }
        b += "</ul>";
        return b
    },
    createMenu: function (a) {
        var b = "",
        c, d, e, f, g, h, j, k = this.selected,
        m = k.length >= 5 ? "disable" : "",
        l;
        f = 0;
        for (h = a.length; f < h; f++) {
            c = a[f];
            b += '<dl class="link-item">';
            b += "<dt>" + c.Title + "</dt>";
            c = c.SubMenus.sort(function (n, o) {
                return n.MenuId - o.MenuId
            });
            g = 0;
            for (j = c.length; g < j; g++) {
                d = c[g];
                e = +d.MenuId;
                l = $.inArray(e, k) > -1 ? "cur" : m;
                b += '<dd><a title="' + d.Title + '" href="javascript:;" menuId="' + e + '" class="' + l + '">';
                b += d.Title + "</a></dd>"
            }
            b += "</dl>"
        }
        return b
    },
    createMenuBox: function () {
        var a = $("#J_menucontent"),
        b = this;
        if (a.length > 0) a.fadeIn(300);
        else {
            a = $(this.html);
            a.appendTo("div.col-sub");
            $.browser.msie && $.browser.version == 6 && a.append('<iframe src="" frameborder="0" style="position:absolute;left:0;top:0;z-index:-1;width:552px;height:500px"></iframe>');
            a.fadeIn(300,
            function () {
                b.bindEdit()
            });
            this.getMenu()
        }
        $(this.wrapId).click(function (c) {
            c.stopPropagation()
        });
        $("body").addClass("menu-edit").bind("click", this.hideContent)
    },
    hideContent: function () {
        $("body").unbind("click", this.hideContent).removeClass("menu-edit");
        $("#J_menucontent").fadeOut(300)
    },
    checkSelected: function () {
        var a = this.max;
        $("#J_menucontent div.bd a.cur").length >= a ? $("#J_menucontent div.bd a").not(".cur").addClass("disable") : $("#J_menucontent div.bd a").removeClass("disable")
    }
};
$(function () {
    new MenuCustom("#J_menucustom", "#J_menuset", 5)
});