(function () {
    var selectedCategoryId = 0;
    var placeHolder; // 分类选择框容器
    var nextButton;
    var lblFullname;
    var productId = "";
    var rowcount = 4;   //显示数量
    var contextWidth = 0;   //容器宽度
    var olOffsetDefLeft = 90;   //默认左边距
    var bodyClientWidth = 0;
    var singleWidth = 223;
    var categoryArray = new Array();
    var liTemp = '<li class="{0}"><img src="http://img.baidu.com/hi/img/del.gif" class="cat-{1}" id="{2}"><span>{3}</span></li>'; //rowBKcolor

    var currentClass;

    // 初始化操作
    $(document).ready(function () {
        bodyClientWidth = document.body.clientWidth;
        CalcRowCount();

        //resize
        $(".dataarea").width(singleWidth * rowcount + 54);
        $(".results_pos").width(singleWidth * rowcount - 1);

        $('.results_pos').queue(function () {
            $(this).hide().show(function () {
                $(this).width(singleWidth * rowcount - 1);

                placeHolder = $(".results_ol");
                contextWidth = $(this).width() + 1 + rowcount;
                nextButton = $("#btnNext");
                addButton = $("#btnAdd");
                lblFullname = $("#fullName");

                nextButton.bind("click", function () {
                    CreatedNewCate(false);
                    $("#litCategoryName").text($("[id$='Hidden_SelectName']").val().replace(/,/g, '\n'));
                    if (selectedCategoryId) {
                        selectedCategoryId = '';
                        //$('.results_s1').attr('selected', '');
                        //$('.results_s1').removeClass('results_s1').addClass('results_n1')
                        $.fn.colorbox.close();
                    }
                });
                //添加到已选择列表
                //var js = document.createElement("script");
                // js.type = "text/javascript";
                // js.src = "new.js";
                addButton.bind("click", function () {
                    CreatedNewCate(true);
                });
                var mainCategories = GetCategories(0);

                if (mainCategories == null || mainCategories.length == 0) {
                    // 没有可选分类
                    alert("\u6CA1\u6709\u53EF\u9009\u5206\u7C7B");
                    return;
                } else {
                    CreateBox(0, mainCategories);

                    var currentCategoryId = $.getUrlParam("CategoryId");
                    if (currentCategoryId) {
                        productId = $.getUrlParam("ProductId");
                        LoadState(currentCategoryId);
                    }
                }
            });
            $(this).dequeue();
        });
    });

    function CreatedNewCate(flag) {
        if (!selectedCategoryId && !flag) {
            $.fn.colorbox.close();
            return;
        }

        if (!selectedCategoryId && flag) {
            alert("请选择一个分类！");
            return;
        }
        if ($("[id$='Hidden_SelectValue']").val()) {
            categoryArray = $("[id$='Hidden_SelectValue']").val().split(',');
            var isHaveValue = false;
            for (var i = 0; i <= categoryArray.length - 1; i++) {
                if (categoryArray[i] == selectedCategoryId) {
                    isHaveValue = true;
                }
            }
            if (isHaveValue) {
                alert("该分类已添加，请勿重复添加！");
                return;
            } else {
                $("[id$='Hidden_SelectValue']").val($("[id$='Hidden_SelectValue']").val() + ',' + selectedCategoryId);
                var spanText = $("#fullName").text();
                $("[id$='Hidden_SelectName']").val($("[id$='Hidden_SelectName']").val() + ',' + spanText);
                //                if (spanText.indexOf("»") >= 0) {
                //                    $("[id$='Hidden_SelectName']").val($("[id$='Hidden_SelectName']").val() + ',' + spanText.substr(spanText.lastIndexOf("»") + 1));
                //                } else {
                //                    $("[id$='Hidden_SelectName']").val($("[id$='Hidden_SelectName']").val() + ',' + spanText);
                //                }
                CreatedLi();
                if (!flag) {
                    $.fn.colorbox.close();
                }
            }
        } else {
            var spanText = $("#fullName").text();
            $("[id$='Hidden_SelectValue']").val(selectedCategoryId);

            $("[id$='Hidden_SelectName']").val(spanText);
            //            if (spanText.indexOf("»") >= 0) {
            //                $("[id$='Hidden_SelectName']").val(spanText.substr(spanText.lastIndexOf("»") + 1));
            //            } else {
            //                $("[id$='Hidden_SelectName']").val(spanText);
            //            }
            CreatedLi();
            if (!flag) {
                $.fn.colorbox.close();
            }
        }
        //为删除按钮绑定事件
        $("#category ul li img").unbind('click').bind('click', function () {
            var cateId = $(this).attr('id');
            $(this).parent().remove();
            if ($("#category ul li ").length == 0) {
                $("#category").hide();
            }
            // 删除隐藏域中的分类ID
            categoryArray = $("[id$='Hidden_SelectValue']").val().split(',');
            var delIndex = -1;
            for (var i = 0; i <= categoryArray.length - 1; i++) {
                if (categoryArray[i] == cateId) {
                    categoryArray.remove(i);
                    delIndex = i;
                }
            }
            var categoryNameArray = $("[id$='Hidden_SelectName']").val().split(',');
            categoryNameArray.remove(delIndex);
            $("[id$='Hidden_SelectName']").val(categoryNameArray.join(','));
            $("[id$='Hidden_SelectValue']").val(categoryArray.join(','));
            $.colorbox.resize();
        });
        //鼠标覆盖变色
        $("#category ul li").hover(function () {
            currentClass = $(this).attr('class');
            $(this).removeClass("rowBKcolor");
            $(this).addClass("mover");
        }, function () {
            $(this).removeClass("mover");
            if (currentClass && currentClass != "mover") {
                $(this).addClass(currentClass);
            }
        });
    }

    //动态计算 根据可见区域大小调整显示数量
    function CalcRowCount() {
        if (singleWidth * rowcount > bodyClientWidth) {
            rowcount = parseInt(bodyClientWidth / singleWidth);
        }
    }

    //将选择的分类添加到已选择列表中
    function CreatedLi() {
        choseCategory = true;
        var liIndex = $("#category ul li ").length;
        if (liIndex == 0) {
            $("#category").show();
        }
        var spanText = $("#fullName").text();
        if (liIndex % 2 == 0) {
            $("#selectCategory").append(liTemp.format('', liIndex, selectedCategoryId, spanText));
        } else {
            $("#selectCategory").append(liTemp.format('rowBKcolor', liIndex, selectedCategoryId, spanText));
        }
        $.colorbox.resize();
        selectedCategoryId = '';
    }

    // 绑定左右移动按钮的单击事件并设置按钮样式
    function BindButtonEvents() {
        // 移除事件,防止多重绑定;
        $(".search_right").unbind("click");
        $(".search_right").click(function () {
            var olWidth = $(".results_ol").width();
            if (olWidth > contextWidth &&
                olWidth + $(".results_ol").offset().left - olOffsetDefLeft > contextWidth) {
                //        if ($(".results_ol div:last").offset().left > $(".results_pos").width()) {
                $(".search_left").addClass("search_leftD");
                $(this).removeClass("search_righD");
                $(this).attr("disabled", "disabled");
                var sender = $(this);
                placeHolder.animate({ left: "-=" + singleWidth + "px" }, 350, null, function () {
                    sender.removeAttr("disabled");
                });
            }
        });
        $(".search_left").unbind("click");
        $(".search_left").click(function () {
            if (parseInt(placeHolder.css("left")) < 0) {
                $(".search_right").addClass("search_righD");
                $(this).removeClass("search_leftD");
                $(this).attr("disabled", "disabled");
                var sender = $(this);
                placeHolder.animate({ left: "+=" + singleWidth + "px" }, 350, null, function () {
                    sender.removeAttr("disabled");
                });
            }
        });
    }

    // 移除当前已选分类直属下级以外的所有子分类选择框
    function RemoveSelectors(startIndex) {
        if ($(".results_ol div").length > 1) {
            $(".results_ol div").each(function (x) {
                if (x > startIndex) {
                    $(this).remove();
                }
            });
        }
    }

    function UpdateBoxes(parentCategoryId, classIndex) {
        var classIndex = parseInt(classIndex) + 1;

        //Sale
        //    if ($(".results_ol div").length > 4) {
        //        return;
        //    }

        var categories = GetCategories(parentCategoryId);
        if (categories == null || categories.length == 0)
            return;

        CreateBox(classIndex, categories);

        var olWidth = $(".results_ol").width();
        if (olWidth > contextWidth &&
                olWidth + $(".results_ol").offset().left - olOffsetDefLeft > contextWidth) {
            placeHolder.animate({ left: "-=" + singleWidth + "px" }, 350);
            $(".search_left").addClass("search_leftD");
            $(".search_right").removeClass("search_righD");
            BindButtonEvents();
        }
    }

    // 根据指定的classIndex创建一个分类选择框，并使用categories中包含的分类列表填充选择框
    function CreateBox(classIndex, categories) {
        var divBox = $("<div class=\"results_z results_margin\" classIndex=" + classIndex + "><\/div>");
        var ulBox = $("<ul><\/ul>");
        placeHolder.append(divBox);

        $.each(categories, function (i, category) {
            var item = $("<li id=\"" + category.CategoryId + "_" + category.Path + "\"  hasChildren=\"" + category.HasChildren + "\">" + category.CategoryName + "<\/li>");
            if (category.HasChildren == true) {
                item.addClass("results_n1");
            }
            //        else {
            //            item.removeClass();
            //        }

            item.bind("click", function () { ItemClick($(this), classIndex); });
            ulBox.append(item);
        });
        divBox.empty();
        divBox.append(ulBox);

        placeHolder.width(singleWidth * $(".results_ol div").length);
    }

    // 商品分类单击事件
    function ItemClick(obj, classIndex) {
        // 移除当前分类框的所有选中状态
        $.each($("[classIndex=" + classIndex + "] li"), function (i, item) {
            $(item).removeAttr("selected");
            $(item).removeClass();
            if ($(item).attr("hasChildren") == "true") {
                $(item).addClass("results_n1");
            }
        });
        obj.removeClass();
        obj.attr("selected", "selected");
        RemoveSelectors($("[classIndex=" + classIndex + "]").attr("classIndex"));
        var Hidden_isCate = $("#Hidden_isCate").val();
        if (Hidden_isCate) {
            if (obj.attr("hasChildren") == "true") {
                obj.addClass("results_s1");
            } else {
                obj.addClass("results_s2");
            }
            // 当前选中的是最后一级分类，设置当前已选分类ID
            selectedCategoryId = obj.attr("id");
            // 当前选中分类有子分类，则显示子分类，并清空当前已选的分类ID
            UpdateBoxes(parseInt(obj.attr("id").split('_')[0]), classIndex);
        } else if (obj.attr("hasChildren") == "false") {
            obj.addClass("results_s2");
            // 当前选中的是最后一级分类，设置当前已选分类ID
            selectedCategoryId = obj.attr("id");
        } else {
            obj.addClass("results_s1");
            // 清空已选分类ID
            selectedCategoryId = 0;
            // 当前选中分类有子分类，则显示子分类，并清空当前已选的分类ID
            UpdateBoxes(parseInt(obj.attr("id").split('_')[0]), classIndex);
        }

        UpdateStatus();
    }

    // 根据指定的上级分类ID获取下级分类列表，parentCategoryId==0表示取所有顶级分类
    function GetCategories(parentCategoryId) {
        var categories = null;

        $.ajax({
            url: ("SelectCategory.aspx?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            data: { Action: "GetList", Callback: "true", ParentCategoryId: parentCategoryId },
            async: false,
            success: function (resultData) {
                if (resultData.STATUS == "OK") {
                    categories = resultData.DATA;
                }
            }
        });

        return categories;
    }

    function UpdateStatus() {
        if (parseInt(selectedCategoryId.split('_')[0]) > 0) {
            nextButton.removeAttr("disabled");
        } else {
            nextButton.attr("disabled", "disabled");
        }

        lblFullname.empty();
        var fullname = "";
        var selectedList = $("li[selected=selected]");

        $.each(selectedList, function (i, element) {
            fullname += $(element).html();
            if (i < selectedList.length - 1)
                fullname += "&nbsp;&raquo;&nbsp;";
        });

        lblFullname.html(fullname);
    }

    function LoadState(categoryId) {
        $.ajax({
            url: ("SelectCategory.aspx?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            data: { Action: "GetInfo", Callback: "true", CategoryId: categoryId },
            async: false,
            success: function (resultData) {
                if (resultData.STATUS == "OK") {
                    var pathArr = resultData.DATA.path.split("|");
                    if (pathArr.length > 0 && SelectItem(pathArr[0], 0)) {
                        for (index = 1; index < pathArr.length; index++) {
                            if (!SelectItem(pathArr[index], index)) {
                                break;
                            }
                        }
                    }
                }
            }
        });
    }

    function SelectItem(categoryId, classIndex) {
        var item = $("li[id=" + categoryId + "]");
        if (item.length == 0) {
            return false;
        }

        ItemClick(item, classIndex);
        return true;
    }
} ());