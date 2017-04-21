        $(function () {
            //添加关键字
            $("#btnAddValue").die("click").live("click", function () {
                var value = $("#txtValue").val();
                var ruleId = $("[id$='hfRuleId']").val();
                if (value == "") {
                    ShowFailTip("请填写关键字！");
                    return;
                }
                $.ajax({
                    url: ($Maticsoft.BasePath + "WeChat/AddValue?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Callback: "true", Value: value, RuleId: ruleId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#AllMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)'   valueId='" + resultData.DATA +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + resultData.DATA +
                                "'>删除</a></span> </span>");
                            $("#txtValue").val("");

                            if ($('[id$="hidDel"]').val() == "hidden") {
                                $(".del").css('display', 'none');
                                $(".delMsg").css('display', 'none');
                            }
                        }
                        if (resultData.STATUS == "Exist") {
                            ShowFailTip("该关键字已存在！");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });

            if ($('[id$="hidDel"]').val() == "hidden") {
                $(".del").css('display', 'none');
                $(".delMsg").css('display', 'none');
            }

            // 删除关键字
            $(".del").die("click").live("click", function () {
                var valueId = $(this).attr("valueId");
                var self = $(this);
                $.ajax({
                    url: ($Maticsoft.BasePath + "WeChat/DeleteValue?timestamp={0}").format(new Date().getTime()),
//                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Callback: "true", ValueId: valueId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.parent().parent().hide();
                            ShowSuccessTip("删除关键字成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //移动到模糊匹配
            $("#AllMatchTags").find(".span1").die("click").live("click", function () {
                var self = $(this);
                var valueId = self.attr("valueId");
                var value = self.find("a").text();
                $.ajax({
                    //                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    url: ($Maticsoft.BasePath + "WeChat/UpdateType?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Callback: "true", ValueId: valueId, MatchType: 0 },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            self.parent().hide();
                            $("#NoMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)'  title='点击设为全匹配'  valueId='" + valueId +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + valueId +
                                "'>删除</a></span> </span>");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //移动到全匹配
            $("#NoMatchTags").find(".span1").die("click").live("click", function () {
                var self = $(this);
                var valueId = self.attr("valueId");
                var value = self.find("a").text();
                $.ajax({
                    //                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    url: ($Maticsoft.BasePath + "WeChat/UpdateType?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Callback: "true", ValueId: valueId, MatchType: 1 },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            self.parent().hide();
                            $("#AllMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)' title='点击设为模糊匹配'  valueId='" + valueId +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + valueId +
                                "'>删除</a></span> </span>");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //添加回复
            $("#btnAddMsg").die("click").live("click", function () {
                var msg = $("#txtPostMsg").val();
                var ruleId = $("[id$='hfRuleId']").val();

                if (msg == "") {
                    ShowFailTip("请填写回复内容！");
                    return;
                }
                if (msg.length >= 1024) {
                    ShowFailTip("您输入的文字太长！");
                    return;
                }

                $.ajax({
                    //                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    url: ($Maticsoft.BasePath + "WeChat/AddMsg?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Callback: "true", Msg: msg, RuleId: ruleId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#txtPostMsg").val("");
                            var length = $("#msgBox").find("ul>li").length;
                            if (length == 0) {
                                $("#msgBox").find("ul").append("<li><div class='userPic'>▶</div><div class='content'> <div class='msgInfo'>" + msg +
                                    "</div><div class='times'> <a class='delMsg' href='javascript:;' msgId='" + resultData.DATA + "'>删除</a></div></div> </li>");
                            } else {
                                $("#msgBox").find("ul>li").eq(0).before("<li><div class='userPic'>▶</div><div class='content'> <div class='msgInfo'>" + msg +
                                "</div><div class='times'> <a class='delMsg' href='javascript:;' msgId='" + resultData.DATA + "'>删除</a></div></div> </li>");
                            }
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });

            //删除回复
            $(".delMsg").die("click").live("click", function () {
                var msgId = $(this).attr("msgId");
                var self = $(this);
                $.ajax({
//                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    url: ($Maticsoft.BasePath + "WeChat/DeleteMsg?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: {  Callback: "true", MsgId: msgId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.parent().parent().parent().hide();
                            ShowSuccessTip("删除回复成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
        })