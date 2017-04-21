$(function () {
    $(".search_box").capacityFixed();
    //商品图滚动预览
    marquee();
    //查询条件显示隐藏
    $(".s-mor").bind({
        click: function () {
            $(".s-mor ul").slideDown();

        }
    });
    $(".s-mor ul li").bind({
        click: function (event) {
            $("#search_condition").html($(this)[0].innerHTML + "<i class='def'></i>");
            $(".s-mor ul").slideUp();
            event.stopPropagation();
        }
    });
    
    //tab页切换
    $(".Floor").each(function (_fIndex, _Floor) {
        $(".Catg_ul li", _Floor).each(function (_liIndex, _liData) {
            $(this).mouseover(function () {
                $(this).addClass("chose");
                for (var i = 0; i < $(".Catg_ul li", _Floor).length; i++) {
                    var tab_index = ".tab_" + i;
                    //var tab_low_index = ".tab_low_" + i;
                    if (i == _liIndex) {
                        $(tab_index, _Floor).css("display", "block");
                        //$(tab_low_index, _Floor).css("display", "block");
                    }
                    else {
                        $(tab_index, _Floor).css("display", "none");
                        //$(tab_low_index, _Floor).css("display", "none");
                    }
                }
            })
            $(this).mouseout(function () {
                $(this).removeClass("chose");
            })
        });
    });

    //三级目录tab页切换
    $(".Floor_btR .lowleft").each(function (_index, _div) {
        $(" .hot-sell-tit ul li", _div).each(function (_i, _li) {
            var tab_index = ".tab-hot-sell-" + _i;
            $(this).mouseover(function () {
                $(tab_index, _div).css("display", "block");
                $(this).addClass("chose");
                if (_i > 0) {
                    $(".tab-hot-sell-0", _div).css("display", "none");
                    $(".tab-hot-sell-" + parseInt(_i - 1), _div).css("display", "none");
                }
            });
            $(this).mouseout(function () {
                $(this).removeClass("chose");
            })
        });
    });

    //热卖精品 滚动效果
    goodSaleMarquee("#HOT", "#small_marq_left", "#small_marq_right", "#pic_index_hot");
    goodSaleMarquee("#NEW", "#small_marq_left_new", "#small_marq_right_new", "#pic_index_new");
    //gSlideShowPic("#side-AD-1", "#AD-glide_trigger-1 i", 4000);
    //gSlideShowPic("#side-AD-2", "#AD-glide_trigger-2 i", 2000);
    //gSlideShowPic("#side-AD-3", "#AD-glide_trigger-3 i", 3000);
    //gSlideShowPic("#side-AD-4", "#AD-glide_trigger-4 i", 3000);
    //gSlideShowPic("#side-AD-5", "#AD-glide_trigger-5 i", 3000);
    //gSlideShowPic("#side-AD-6", "#AD-glide_trigger-6 i", 3000);
    //gSlideShowPic("#side-AD-4-3", "#AD-glide_trigger-4-3 i", 3000);
    //gSlideShowPic("#side-AD-4-4", "#AD-glide_trigger-4-4 i", 3000);
    //gSlideShowPic("#side-AD-5", "#AD-glide_trigger-5 i", 3000);
    //gSlideShowPic("#side-AD-11", "#AD-glide_trigger-2 i", 2000);

    
    //$("#colee_up").scrollEndWise({
    //    "speed": 20,
    //    "amount": 0,
    //    "step": 1,
    //    "dir": "up"
    //});
    //横向跑马灯
    $(".Catg-list").scrollEndWise({
        "speed": 20,
        "amount": 0,
        "step": 1,
        "dir": "left",
        "leftClick": ".cart-left",
        "rightClick": ".cart-right"
    });

    //小tab切换效果
    //好邻公告
    $("#notice").mouseover(function () {
        $(this).addClass("chose");
        $("#brand").removeClass("chose");
        $(".HL_notic").show();
        $(".Pre-brand").hide();
    });
    //顶上品牌
    $("#brand").mouseover(function () {
        $(this).addClass("chose");
        $("#notice").removeClass("chose");
        $(".HL_notic").hide();
        $(".Pre-brand").show();
    });
    //精品热卖
    $("#hotsell").mouseover(function () {
        $(this).addClass("chose");
        $("#newprod").removeClass("chose");
        $("#NEW").hide();
        $("#HOT").show();
    });

    //新品上市
    $("#newprod").mouseover(function () {
        $(this).addClass("chose");
        $("#hotsell").removeClass("chose");
        $("#NEW").show();
        $("#HOT").hide();
    });

    //分类鼠标经过效果
    catg();
});

function catg() {
    $('.all-sort-list > .item').hover(function () {
        var eq = $('.all-sort-list > .item').index(this),				//获取当前滑过是第几个元素
			h = $('.all-sort-list').offset().top,						//获取当前下拉菜单距离窗口多少像素
			s = $(window).scrollTop(),									//获取游览器滚动了多少高度
			i = $(this).offset().top,									//当前元素滑过距离窗口多少像素
			item = $(this).children('.item-list').height(),				//下拉菜单子类内容容器的高度
			sort = $('.all-sort-list').height();						//父类分类列表容器的高度

        if (item < sort) {												//如果子类的高度小于父类的高度
            if (eq == 0) {
                $(this).children('.item-list').css('top', (i - h));
            } else {
                $(this).children('.item-list').css('top', (i - h) + 1);
            }
        } else {
            if (s > h) {												//判断子类的显示位置，如果滚动的高度大于所有分类列表容器的高度
                if (i - s > 0) {											//则 继续判断当前滑过容器的位置 是否有一半超出窗口一半在窗口内显示的Bug,
                    $(this).children('.item-list').css('top', (s - h) + 2);
                } else {
                    $(this).children('.item-list').css('top', (s - h) - (-(i - s)) + 2);
                }
            } else {
                $(this).children('.item-list').css('top', 3);
            }
        }

        $(this).addClass('hover');
        $(this).children('.item-list').css('display', 'block');
    }, function () {
        $(this).removeClass('hover');
        $(this).children('.item-list').css('display', 'none');
    });

    $('.item > .item-list > .close').click(function () {
        $(this).parent().parent().removeClass('hover');
        $(this).parent().hide();
    });
}




function goodSaleMarquee(div, left, right,picIndex) {
    
    var sWidth = $(div+" .Pic-area").width();
    var len = $(div+" .Pic-area ul li").length;
    var index = 0;

    var timer;

    $(left).click(function () {
        index -= 1;
        if (index == -1) { index = len - 1; }
        $(picIndex).html(index + 1);
        showPics(index);
    });

    $(right).click(function () {
        
        index += 1;
        if (index == len) { index = 0; }
        $(picIndex).html(index + 1);
        showPics(index);
    });

    function showPics(index) {
        var nowLeft = -index * sWidth;
        $(div+" .Pic-area ul").stop(true, false).animate({ "left": nowLeft }, 300);
    }


    timer = setInterval(function () {
        $(right).click();
    }, 4000);
}

function gSlideShowPic(divId, picDivId, speed) {
    var glideWidth = $(divId).width();
    var glideLen = $(divId + " ul li").length;
    var index = 0;
    var timer;

    //添加mouseover事件
    $(picDivId).css("opacity", 0.4).mouseover(function () {
        index = $(picDivId).index(this);
        showPics(index);
        $(this).addClass("glide_on");
    }).mouseout(
        function () {
            $(this).removeClass("glide_on");
        }
    );

    function showPics(index) {
        var nowLeft = -index * glideWidth;
        $(divId + " ul").stop(true, false).animate({ "left": nowLeft }, 300);
        $(divId + " ul li");
    }

    timer = setInterval(function () {
        showPics(index);
        index++;
        if (index == glideLen) { index = 0; }
    }, speed);
}

function marquee() {
    var sWidth = $("#main_msg-M").width(); //获取焦点图的宽度（显示面积）
    var len = $("#main_msg-M ul li").length; //获取焦点图个数
    var index = 0;
    var picTimer;

    picTimer = setInterval(function () {
        showPics(index);
        index++;
        if (index == len) { index = 0; }
    }, 3000); //此4000代表自动播放的间隔，单位：毫秒

    //以下代码添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
    var btn = "<div class='btnBg'></div><div class='mid_btn'>";
    for (var i = 0; i < len; i++) {
        btn += "<span></span>";
    }
    //btn += "</div><div class='preNext pre'></div><div class='preNext next'></div>";
    btn += "</div>";
    $("#main_msg-M").append(btn);
    $("#main_msg-M .btnBg").css("opacity", 0);

    //为小按钮添加鼠标滑入事件，以显示相应的内容
    $("#main_msg-M .mid_btn span").css("opacity", 0.4).mouseover(function () {
        index = $("#main_msg-M .mid_btn span").index(this);
        showPics(index);
    })

    //上一页、下一页按钮透明度处理
    $("#main_msg-M .preNext").css("opacity", 0.2).hover(function () {
        $(this).stop(true, false).animate({ "opacity": "0.5" }, 300);
    }, function () {
        $(this).stop(true, false).animate({ "opacity": "0.2" }, 300);
    });

    //上一页按钮
    $("#main_msg-M .pre").click(function () {
        index -= 1;
        if (index == -1) { index = len - 1; }
        showPics(index);
    });

    //下一页按钮
    $("#main_msg-M .next").click(function () {
        index += 1;
        if (index == len) { index = 0; }
        showPics(index);
    });

    //左右滚动，即所有li元素都是在同一排向左浮动，所以这里需要计算出外围ul元素的宽度
    $("#main_msg-M ul").css("width", sWidth * (len));

    //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
    $("#main_msg-M").hover(function () {
        clearInterval(picTimer);
    }, function () {
        picTimer = setInterval(function () {
            showPics(index);
            index++;
            if (index == len) { index = 0; }
        }, 3000); //此4000代表自动播放的间隔，单位：毫秒
    })
    
    //显示图片函数，根据接收的index值显示相应的内容
    function showPics(index) { //普通切换
        var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
        $("#main_msg-M ul").stop(true, false).animate({ "left": nowLeft }, 300); //通过animate()调整ul元素滚动到计算出的position
        $("#main_msg-M .mid_btn span").stop(true, false).animate({ "opacity": "0.4" }, 300).eq(index).stop(true, false).animate({ "opacity": "1" }, 300); //为当前的按钮切换到选中的效果
    }
}
