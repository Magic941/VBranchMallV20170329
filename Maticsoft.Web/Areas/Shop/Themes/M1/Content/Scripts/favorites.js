$(document).ready(function () {
    $("a[favoritestyleid]").bind('click',
    function () {
        var styleid = $(this).attr('favoritestyleid');
        var skuitemid = $(this).attr('skuitemid') == 'undefined' ? '' : $.trim($(this).attr('skuitemid'));
        var originate = $(this).attr('originate') == 'undefined' ? '' : $.trim($(this).attr('originate'));
        var color = $(this).attr('color') == 'undefined' ? '' : $.trim($(this).attr('color'));
        var size = $(this).attr('size') == 'undefined' ? '' : $.trim($(this).attr('size'));
        var isboughtflag = 'False';
        Favorites.InsertFavoriteStyle(styleid, skuitemid, color, size, originate, isboughtflag);
    });
});

Favorites = {
    Url: 'http://member.maticsoft.com/service/ajaxservice.ashx',
    InsertFavoriteStyle: function (styleId, skuId, color, size, originate, isBoughtFlag) {
        $("body").AjaxLogin({
            success: function () {
                $.getJSON(Favorites.Url + "?Method=InsertFavoriteStyle&styleId=" + styleId + "&skuitemid=" + skuId + "&color=" + color + "&size=" + size + "&originate=" + originate + "&isboughtflag=" + isBoughtFlag + "&jsoncallback=?",
                function (data) {
                    if (data.d != null && data.d == '1') {
                        Favorites.BuildFavoriteHtml(styleId);
                        Favorites.SendDefaultMicroBlog(styleId);
                    } else {
                        Favorites.BuildFavoritedHtml(styleId);
                    }
                });
            },
            content: "请登录!"
        });
    },
    SendDefaultMicroBlog: function (styleId) {
        Favorites.ShareToSina(styleId, '0');
    },
    ShareToSina: function (styleId, type) {
        $.getJSON(this.Url + "?Method=GetStyleForMicBlogById&styleId=" + styleId + "&jsoncallback=?",
        function (data) {
            if (data != null) {
                var prdname = data._ChineseName;
                var imgurl = data._FullStylePicUrl;
                var prdurl = 'http://product.maticsoft.com/p-' + data._StyleId + '.htm#rel=xlwb1';
                var classid1 = data._ClassId1;
                //ShareToSina.TrackUrl = '/app/adentrance.aspx?from=xlwb1&targeturl=';
                ShareToSina.TrackUrl = "";
                if (type == '0') {
                    ShareToSina.Favourite(prdname, imgurl, prdurl, classid1, null, null);
                } else {
                    ShareToSina.Message = "我刚在动软看到这个“" + prdname + "”，觉得挺赞的! 正在考虑要不要买呢，先给大家看看，给我点意见吧！" + ShareToSina.TrackUrl + prdurl;
                    ShareToSina.ImageUrl = imgurl;
                    ShareToSina.ProductUrl = prdurl;
                    ShareToSina.Type = 2;
                    ShareToSina.Show();
                }
            }
        });
    },
    InsertLabels: function (styleId, labels) {
        $.getJSON(this.Url + "?Method=InsertLabels&styleId=" + styleId + "&labelNameList=" + escape(labels) + "&jsoncallback=?");
    },
    BuildFavoriteHtml: function (styleId) {
        html = '<div class="J_dialog" id="MtagList" style="margin-top:-256px; margin-left:-250px; visibility: visible;">';
        html += '<div style="width:492px;" class="J_dialog_content">';
        html += '<div class="J_dialog_hd"><h3 class="J_dialog_title">收藏提示</h3>';
        html += '<a onclick="$(\'#MtagList\').popup({ close: true });" class="J_dialog_close dclose pclose" href="javascript:;">关闭</a></div>';
        html += '<div class="J_dialog_bd"><div class="pt10 pb10 clearfix" id="copyBack">';
        html += '<div class="tips-r pb10 bbd">';
        html += '<span class="fb f14">商品收藏成功！</span><a target="_blank" href="http://member.maticsoft.com/favorites/favoritelist.aspx" class="a2">查看收藏夹</a>';
        html += '<span class="pl10 pr10">|</span><span class="share-weibo pl20"><a href="javascript:void(0);" class="a2">分享到微博</a></span></div>';
        html += '<div class="pb20 bbd "><p class="color-66 pt10 pb5">您可以添加标签备注该商品：</p>';
        html += '<p class="clearfix"><input id="txtlabelname" class="input1 fl mr5" type="text">';
        html += '<span class="tips-s-r none"></span><span class="lower">空格分隔，最多5个，单个最多10个字符</span></p>';
        html += '<p class="color-66 pt10">常用标签：</p>';
        html += '<div id="favoritetags" class="taglist mt10 clearfix">';
        $.ajax({
            url: this.Url,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            data: "Method=GetCommLables&styleId=" + styleId + "&showCount=3&showCountForClass=3",
            success: function (data) {
                if (data != null && data.length > 0) {
                    $.each(data,
                    function (i, labelname) {
                        html += '<a href="javascript:void(0);">' + labelname + '</a> ';
                    });
                }
                html += '</div>';
                html += '<div class="pt5"><a id="btnsave" class="btn" href="javascript:void(0);">';
                html += '<span class="loud">确<em class="s1em"></em>定</span></a><span class="tips-s ml10 none">保存成功</span>';
                html += '</div></div>';
                Favorites.GetAlsoBuyHtml(styleId, 0);
            }
        });
    },
    BuildFavoritedHtml: function (styleId) {
        html = '<div class="J_dialog" id="MtagList" style="margin-top:-256px; margin-left:-250px; visibility: visible;">';
        html += '<div style="width:492px;" class="J_dialog_content">';
        html += '<div class="J_dialog_hd"><h3 class="J_dialog_title">收藏提示</h3>';
        html += '<a onclick="$(\'#MtagList\').popup({ close: true });" class="J_dialog_close dclose pclose" href="javascript:;">关闭</a></div>';
        html += '<div class="J_dialog_bd"><div class="pt10 pb10 clearfix" id="copyBack">';
        html += '<div class="tips-r pb10 bbd">';
        html += '<span class="fb f14">您的收藏夹中已经有该商品了！</span><a target="_blank" href="http://member.maticsoft.com/favorites/favoritelist.aspx" class="a2"> 查看收藏夹</a>';
        html += '<span class="pl10 pr10">|</span><span class="share-weibo pl20"><a href="javascript:void(0);" class="a2">分享到微博</a></span></div>';
        $.ajax({
            url: this.Url,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            data: "Method=GetAlsoBuyForFavorite&styleId=" + styleId,
            success: function (data) {
                if (data != null && data.length > 0) {
                    html += '<div class="J_carousel" id="newStyle"><p class="color-66 pt10">收藏该商品的人还喜欢</p><ul class="J_carousel_trigger">';
                    html += '<li class="fl"><a href="javascript:;" title="向左" class="J_carousel_prev">向左</a></li>';
                    html += '<li class="fr"><a href="javascript:;" title="向右" class="J_carousel_next">向右</a></li></ul>';
                    html += '<div class="J_carousel_clip shop_scroll_list" style="position: relative; overflow-x: hidden; overflow-y: hidden; width: 416px; height: 179px; ">';
                    html += '<ul class="J_carousel_list clearfix" style="width: 832px; position: absolute; ">';
                    $.each(data,
                    function (i, style) {
                        var deptno = 'scfc2';
                        var prdurl = 'http://product.maticsoft.com/p-' + style._StyleId + '.htm#rel=' + deptno;
                        html += '<li class="J_carousel_item"> <a target="_blank" title="' + style._ChineseName + '" href="' + prdurl + '" target="_blank"> <img title="' + style._ChineseName + '" src="' + style._FullStylePicUrl + '">';
                        html += '<p class="block">' + style._ShortChineseName + '</p>';
                        html += '<p class="color-r">￥' + style._WebSalePrice + '</p></a></li>';
                    });
                    html += '</ul></div></div>';
                };
                html += '</div></div></div><div class="J_dialog_shadow"></div></div>';
                $('body').find('#MtagList').remove();
                $('body').append(html);
                new Carousel('#newStyle', 4, {
                    auto: true
                });
                $('#MtagList').find('.share-weibo').bind('click',
                function () {
                    Favorites.ShareToSina(styleId, '1');
                });
                $('#MtagList').popup();
            }
        });
    },
    GetLablesHtml: function (styleId) {
        $.ajax({
            url: this.Url,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            data: "Method=GetCommLables&styleId=" + styleId + "&showCount=3&showCountForClass=3",
            async: false,
            success: function (data) {
                $.each(data,
                function (i, labelname) {
                    html += '<a href="javascript:void(0);">' + labelname + '</a> ';
                });
            }
        });
    },
    GetAlsoBuyHtml: function (styleId, type) {
        $.ajax({
            url: this.Url,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            data: "Method=GetAlsoBuyForFavorite&styleId=" + styleId,
            success: function (data) {
                if (data != null && data.length > 0) {
                    html += '<div class="J_carousel" id="newStyle"><p class="color-66 pt10">收藏该商品的人还喜欢</p><ul class="J_carousel_trigger">';
                    html += '<li class="fl"><a href="javascript:;" title="向左" class="J_carousel_prev">向左</a></li>';
                    html += '<li class="fr"><a href="javascript:;" title="向右" class="J_carousel_next">向右</a></li></ul>';
                    html += '<div class="J_carousel_clip shop_scroll_list" style="position: relative; overflow-x: hidden; overflow-y: hidden; width: 416px; height: 179px; ">';
                    html += '<ul class="J_carousel_list clearfix" style="width: 832px; position: absolute; ">';
                    $.each(data,
                    function (i, style) {
                        var deptno = type == 0 ? 'scfc1' : 'scfc2';
                        var prdurl = 'http://product.maticsoft.com/p-' + style._StyleId + '.htm#rel=' + deptno;
                        html += '<li class="J_carousel_item"> <a target="_blank" title="' + style._ChineseName + '" href="' + prdurl + '" target="_blank"> <img title="' + style._ChineseName + '" src="' + style._FullStylePicUrl + '">';
                        html += '<p class="block">' + style._ShortChineseName + '</p>';
                        html += '<p class="color-r">￥' + style._WebSalePrice + '</p></a></li>';
                    });
                    html += '</ul></div></div>';
                }
                html += '</div></div></div><div class="J_dialog_shadow"></div></div>';
                Favorites.Popup(styleId);
            }
        });
    },
    Popup: function (styleId) {
        $('body').find('#MtagList').remove();
        $('body').append(html);
        new Carousel('#newStyle', 4, {
            auto: true
        });
        //$('#txtlabelname').bind('blur', function() {
        //    Favorites.CheckLabelValid();
        //});
        $('#btnsave').bind('click',
        function () {
            if (Favorites.CheckLabelValid()) {
                Favorites.InsertLabels(styleId, $.trim($('#txtlabelname').val()));
                $('#MtagList').find('.tips-s').show();
                setTimeout(function () {
                    $('#MtagList').find('.tips-s').hide();
                },
                4000);
            }
        });
        $('#favoritetags').find('a').each(function () {
            $(this).bind('click',
            function () {
                var label = $(this).text();
                var objlabellist = $('#txtlabelname');
                if (objlabellist.val().indexOf(label) == -1) {
                    objlabellist.val(objlabellist.val() + ' ' + label);
                }
                Favorites.CheckLabelValid();
            });
        });
        $('#MtagList').find('.share-weibo').bind('click',
        function () {
            Favorites.ShareToSina(styleId, '1');
        });
        $('#MtagList').popup();
    },
    CheckLabelValid: function () {
        var objlabel = $('#txtlabelname');
        if ($.trim(objlabel.val()).length == 0) {
            $(".tips-s-r").html('请输入标签');
            $(".tips-s-r").show();
            $(".tips-s-r").next().hide();
            $('#txtlabelname').focus();
            return false;
        };
        var arrLabel = $.trim(objlabel.val()).split(' ');
        var arrFiltedLabels = new Array();
        var result = false;
        for (var i = 0; i < arrLabel.length; i++) {
            if ($.trim(arrLabel[i]).length > 0) {
                result = Favorites.CheckInputValid($.trim(arrLabel[i]));
                if (!result) {
                    return false;
                }
            }
        }
        for (var i = 0; i < arrLabel.length; i++) {
            if ($.trim(arrLabel[i]).length > 0) {
                arrFiltedLabels.push($.trim(arrLabel[i]));
            }
        }
        if (arrFiltedLabels.length > 5) {
            $(".tips-s-r").html('标签数不能超过5个');
            $(".tips-s-r").show();
            $(".tips-s-r").next().hide();
            $('#txtlabelname').focus();
            return false;
        }
        if (Favorites.IsContainSameStr($.trim(objlabel.val()))) {
            $(".tips-s-r").html('输入的标签有重复');
            $(".tips-s-r").show();
            $(".tips-s-r").next().hide();
            $('#txtlabelname').focus();
            return false;
        }
        return result;
    },
    CheckInputValid: function (name) {
        var r1 = /^[\s]*$/;
        var r2 = /^[0-9]+$/;
        var r3 = /^[^'"\\<>]+$/;
        var r4 = /^[^'"\\<>]{2,10}$/;
        var msg = '';
        if (!r3.test(name)) {
            msg = '请勿输入特殊符号';
        } else if (!r4.test(name)) {
            msg = '单个标签长度在2-10个字符之间';
        }
        if (msg.length > 0) {
            $(".tips-s-r").html(msg);
            $(".tips-s-r").show();
            $(".tips-s-r").next().hide();
            $('#txtlabelname').focus();
            return false;
        } else {
            $(".tips-s-r").hide();
            $(".tips-s-r").next().show();
            return true;
        }
    },
    IsContainSameStr: function (str) {
        var singlestr = str.split(" ");
        for (i = 0; i < singlestr.length; i++) {
            for (j = i + 1; j < singlestr.length; j++) {
                if (singlestr[i] == singlestr[j]) {
                    return true;
                }
            }
        }
        return false;
    }
}