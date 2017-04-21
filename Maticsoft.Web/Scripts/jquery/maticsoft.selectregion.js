/*
* File:        maticsoft.selectnode.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2013 Maticsoft. All Rights Reserved.
*/
;
(function() {
    var currenScript = $('[src$="maticsoft.selectregion.js"]').last(); //锁定脚本引用
    var currentBaseSelectGuid = $.Guid.New(); //生成闭包GUID 唯一标识
    //给隐藏域设置GUID属性 以便闭包内脚本跟踪
   var currhfSelectedNode = $('[id$=hfSelectedNode]').last().attr('HFID', currentBaseSelectGuid);
    var currentIsNull,currentHandleName;
    if (currenScript.length == 1 && currenScript.attr('isnull') && currenScript.attr('handle')) {
        currentIsNull = currenScript.attr('isnull'); //是否有请选择
        currentHandleName = currenScript.attr('handle'); //获取Handler
    } else if(currhfSelectedNode.length == 1 && currhfSelectedNode.attr('isnull') && currhfSelectedNode.attr('handle')) {
        currentIsNull = currhfSelectedNode.attr('isnull'); //是否有请选择
        currentHandleName = currhfSelectedNode.attr('handle'); //获取Handler
    } else {
        //Default
        currentIsNull = 'true';
        currentHandleName = '/RegionHandle.aspx';
    }
    $(document).ready(function() {
        var hfSelectedVal = $('[HFID=' + currentBaseSelectGuid + ']').val();
        if (hfSelectedVal && hfSelectedVal != "0") {
            //逆向加载Select
            InitLastSelect();
        } else {
                //正向加载Select
                InitFirstSelect();
        }
    });

    //逆向初期化第一个下拉列表

    function InitLastSelect() {
        var isSelectNodeNull = (currentIsNull == 'true');

        //如果隐藏域有值 回传或需初次加载 并选中 ---重点

        if (!$('[HFID=' + currentBaseSelectGuid + ']').val()) return; //当下拉无值时终止
        $.ajax({
            url: currentHandleName,
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { Action: "GetParentNode", NodeId: $('[HFID=' + currentBaseSelectGuid + ']').val() },
            success: function(resultData) {
                switch (resultData.STATUS) {
                case "OK":
                    //循环添加第一级select
                    for (var n = 0; n < resultData.DATA.length; n++) {
                        var baseSelect;
                        //是否默认加载“请选择”
                        if (isSelectNodeNull) {
                            baseSelect = $("<select id='" + $.Guid.New() + "'><option value=''>请选择</option></select>");
                        } else {
                            baseSelect = $("<select id='" + $.Guid.New() + "'></select>");
                        }

                        //循环添加子集
                        for (var j = 0; j < resultData.DATA[n].length; j++) {
                            //获取项的文本，值
                            var value, name, index = 0;
                            for (var key in resultData.DATA[n][j]) {
                                //resultData.DATA[n]返回值第一个为 项的值，第二个为显示文本
                                if (index == 1) {
                                    value = resultData.DATA[n][j][key];
                                }
                                if (index == 3) {
                                    name = resultData.DATA[n][j][key];
                                }
                                index++;
                            }
                            var k = n;
                            if (n < (resultData.DATA.length - 1)) {
                                k = n + 1;
                            }
                            //是否选中
                            if (value == resultData.PARENT[k] || value == currhfSelectedNode.val() ) {
                                $("<option value='" + value + "' selected='selected'>" + name + "</option>").appendTo(baseSelect);
                            } else {
                                $("<option value='" + value + "'>" + name + "</option>").appendTo(baseSelect);
                            }
                        }
                        $('[HFID=' + currentBaseSelectGuid + ']').parent().append(baseSelect);
                        //选中值改变，正向加载子集
                        baseSelect.change(function() {
                            LoadChildNodeData(this);
                        });
                    }
                    break;
                default:
                    break;
                }
            },
            error: function(xmlHttpRequest, textStatus, errorThrown) {
                alert(xmlHttpRequest.responseText);
            }
        });
    }

    //正向初期化第一个下拉列表

    function InitFirstSelect() {
        //第一个下拉列表
        var baseSelect = $("<select id='" + currentBaseSelectGuid + "'><option value=''>请选择</option></select>");
        $.ajax({
            url: currentHandleName,
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            data: { Action: "GetDepthNode" },
            async: false,
            success: function(resultData) {
                switch (resultData.STATUS) {
                case "OK":
                    $(resultData.DATA).each(function() {
                        baseSelect.append("<option value='" + this.RegionId + "'>" + this.RegionName + "</option>");
                    });
                    break;
                default:
                    break;
                }
            },
            error: function(xmlHttpRequest, textStatus, errorThrown) {
                alert(xmlHttpRequest.responseText);
            }
        });

        //将动态生成的Select加载到隐藏域内
        $('[HFID=' + currentBaseSelectGuid + ']').parent().append(baseSelect);
        baseSelect.change(function() {
            LoadChildNodeData(this);
        });
    }

    //加载子节点数据 追加到父容器内部 递归方法 已完成

    function LoadChildNodeData(send) {
        if ($(send).val() || !$(send).prev('select').val() ) {
            $('[HFID=' + currentBaseSelectGuid + ']').val($(send).val()?$(send).val():'');
        } else {
            $('[HFID=' + currentBaseSelectGuid + ']').val($(send).prev('select').val());
        }
        //将选择的省市县数据填入详细地址栏 edit by wangzhongyu 2014/7/7
        $.ajax({
            async: false,
            url: '/RegionHandle.aspx',
            data: { Action: "GetAreaString", NodeId: $(send).val() ? $(send).val() : '' },
            type: 'post',
            success: function (data) {
                if (data) {
                    $("#Address").val(data);
                }
            }
        });
        $(send).nextAll('select').remove();
        if (!$(send).val()) return; //当下拉无值时终止递归
        $.ajax({
            url: currentHandleName,
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            data: { Action: "GetChildNode", ParentId: $(send).val() },
            async: false,
            success: function(resultData) {
                switch (resultData.STATUS) {
                case "OK":
                    var baseSelect;
                    var isSelectNodeNull = (currentIsNull == 'true');
                        //是否有默认 [请选择]
                    if (isSelectNodeNull) {
                        baseSelect = $("<select id='" + $.Guid.New() + "'><option value=''>请选择</option></select>");
                    } else {
                        baseSelect = $("<select id='" + $.Guid.New() + "'></select>");
                    }

                    $(resultData.DATA).each(function() {
                        var value, name, index = 0;
                        for (var key in this) {
                            if (index == 0) {
                                value = this[key];
                            }
                            if (index == 1) {
                                name = this[key];
                            }
                            index++;
                        }
                        baseSelect.append("<option value='" + value + "'>" + name + "</option>");
                    });

                    $('[HFID=' + currentBaseSelectGuid + ']').parent().append(baseSelect);
                    baseSelect.change(function() {
                        LoadChildNodeData(this);
                    });
                        //是否立即加载子节点数据
                    if (!isSelectNodeNull) {
                        LoadChildNodeData(baseSelect);
                    }
                    break;
                default:
                    break;
                }
            },
            error: function(xmlHttpRequest, textStatus, errorThrown) {
                alert(xmlHttpRequest.responseText);
            }
        });
    }

}());