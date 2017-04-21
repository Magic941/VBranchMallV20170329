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
                lblFullname = $("#fullName");

                nextButton.bind("click", function () { return GotoNext(); });
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

    //动态计算 根据可见区域大小调整显示数量
    function CalcRowCount() {
        if (singleWidth * rowcount > bodyClientWidth) {
            rowcount = parseInt(bodyClientWidth / singleWidth);
        }
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
            var item = $("<li id=\"" + category.CategoryId + "\"  hasChildren=\"" + category.HasChildren + "\">" + category.CategoryName + "<\/li>");
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
            selectedCategoryId = parseInt(obj.attr("id"));
            // 当前选中分类有子分类，则显示子分类，并清空当前已选的分类ID
            UpdateBoxes(parseInt(obj.attr("id")), classIndex);
        }else if (obj.attr("hasChildren") == "false") {
            obj.addClass("results_s2");
            // 当前选中的是最后一级分类，设置当前已选分类ID
            selectedCategoryId = parseInt(obj.attr("id"));
        } else {
            obj.addClass("results_s1");
            // 清空已选分类ID
            selectedCategoryId = 0;
            // 当前选中分类有子分类，则显示子分类，并清空当前已选的分类ID
            UpdateBoxes(parseInt(obj.attr("id")), classIndex);
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
        if (selectedCategoryId > 0) {
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


    function GotoNext() {
        if (selectedCategoryId == 0) {
            // 请先选择一个商品分类
            alert("\u8BF7\u5148\u9009\u62E9\u4E00\u4E2A\u5546\u54C1\u5206\u7C7B");
            return false;
        }

        nextButton.attr("disabled", "disabled");

        if (productId)
            window.location = "ProductEdit.aspx?CategoryId=" + selectedCategoryId + "&ProductId=" + productId;
        else
            window.location = "ProductAdd.aspx?CategoryId=" + selectedCategoryId;

        return false;   //fix FireFox submit button bug
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