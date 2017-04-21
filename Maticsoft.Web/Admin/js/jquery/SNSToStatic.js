var taskCount = 0;
var taskCount_C = 0;
$(function() {
    var isStatic = $("#ctl00_ContentPlaceHolder1_txtIsStatic").val();
    if (isStatic == "false") {
        $("#tabTask").hide();
        $("#txtRemain").hide();
        $("#txtRemain_C").hide();
         $("#tabIndex").hide();
    } else {
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $("#ctl00_ContentPlaceHolder1_txtFrom").prop("readonly", true).datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            dateFormat: "yy-mm-dd",
            onClose: function(selectedDate) {
                $("#ctl00_ContentPlaceHolder1_txtTo").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#ctl00_ContentPlaceHolder1_txtTo").prop("readonly", true).datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            dateFormat: "yy-mm-dd",
            onClose: function(selectedDate) {
                $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker("option", "maxDate", selectedDate);
            }
        });
        taskCount = $("#ctl00_ContentPlaceHolder1_txtTaskCount").val();
        if (taskCount > 0) {
            $("#txtRemain").show();
        }
        taskCount_C = $("#ctl00_ContentPlaceHolder1_txtTaskCount_C").val();
        if (taskCount_C > 0) {
            $("#txtRemain_C").show();
        } //开始新的任务
        $("#btnToStatic").click(function() {
            var type = $("#txtType").val();
            DisableBtn();
            taskCount = $("#ctl00_ContentPlaceHolder1_txtTaskCount").val();
            var taskCount_C = $("#ctl00_ContentPlaceHolder1_txtTaskCount_C").val();
            if (taskCount > 0) {
                if (confirm("上次有未完成的任务,是否覆盖未完成的任务?")) {
                    RunTask(4);
                } else {
                    EnableBtn();
                }
            } else if (taskCount_C > 0) {
                if (confirm("上次有未完成的任务,是否覆盖未完成的任务?")) {
                    RunTask(5);
                } else {
                    EnableBtn();
                }
            } else {
                RunTask(type);
            }
        });
         //继续未完成的任务
        $("#btnContinue").click(function() {
            DisableBtn();
            ContinueTask(taskCount, 4);
        }) //清除未完成的任务
        $("#btnRemove").click(function() {
            RemoveTask();
        }) //继续未完成的任务
        $("#btnContinue_C").click(function() {
            DisableBtn();
            ContinueTask(taskCount_C, 5);
        }) //清除未完成的任务
        $("#btnRemove_C").click(function() {
            RemoveTask();
        })
    }
});
function doProgressbar(count, i, type) {
    $("#probar").show();
    $.ajax({
        url: ("/SNSToStaticHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        //  timeout: 10000,
        data: {
            action: "GenerateHtml",
            TaskId: i,
            Type: type
        },
        success: function(result) {
            if (i <= count) {
                $("#progressbar").progressbar({
                    value: i
                });
                $("#txtCount").text(i);
                i++;
                if (type == 4) {
                    taskCount = 0;
                }
                if (type == 5) {
                    taskCount_C = 0;
                }
                doProgressbar(count, i, type);
            } else if (taskCount > 0 || taskCount_C > 0) {
                if (confirm("还有未完成的任务,是否继续未完成的任务?")) {
                    if (type == 5) {
                           ContinueTask(taskCount, 4);
                    } else {
                        ContinueTask(taskCount_C,5);
                    }
                }
            } else {
                alert("已全部生成成功");
                EnableBtn();
                RemoveTask();
            }
        }
    });
} //执行新任务
function RunTask(type) {
    $.jBox.tip("Initializing Task List, please wait ...", 'loading');
    $.ajax({
        url: ("/SNSToStaticHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        // timeout: 10000,
        data: {
            action: "HttpToStatic",
            Callback: "true",
            From: $("#ctl00_ContentPlaceHolder1_txtFrom").val(),
            To: $("#ctl00_ContentPlaceHolder1_txtTo").val(),
            Type: type
        },
        success: function(result) {
            $.jBox.closeTip();
            if (result.STATUS == "SUCCESS") {
                if (result.DATA > 0) {
                    $("#progressbar").progressbar({
                        max: result.DATA,
                        value: 0
                    });
                    $("#txtTotalCount").text(result.DATA);
                    $("#txtCount").text(0);
                    $("#probar").show();
                    doProgressbar(result.DATA, 1, type);
                } else {
                    alert("该条件下没有需要静态生成的文件");
                    EnableBtn();
                }
            }
        }
    });
} //继续任务 断点续传功能
function ContinueTask(taskCount, type) {
    $.ajax({
        url: ("/SNSToStaticHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        //  timeout: 10000,
        data: {
            action: "ContinueTask",
            Type: type
        },
        success: function(result) {
            if (result.STATUS == "SUCCESS") {
                if (result.DATA > 0) {
                    $("#progressbar").progressbar({
                        max: taskCount,
                        value: result.DATA
                    });
                    $("#txtTotalCount").text(taskCount);
                    $("#txtCount").text(result.DATA);
                    $("#probar").show();
                    doProgressbar(taskCount, result.DATA, type);
                } else {
                    alert("已全部生成！");
                    EnableBtn();
                    $("#probar").hide();
                }
            }
        }
    });
} //清除任务
function RemoveTask() {
    $.ajax({
        url: ("/SNSToStaticHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        // timeout: 10000,
        data: {
            action: "DeleteTask", 
            Cid: $("#ctl00_ContentPlaceHolder1_dropParentID_hfSelectedNode").val()
        },
        success: function() {
            $("#probar").hide();
            $("#txtRemain").hide();
            $("#txtRemain_C").hide();
            $("#ctl00_ContentPlaceHolder1_txtTaskCount").val(0);
            $("#ctl00_ContentPlaceHolder1_txtTaskCount_C").val(0);
            EnableBtn();
        }
    });
}
function DisableBtn() {
    $("#btnToStatic").attr("disabled", "disabled");
    $("#btnContinue").attr("disabled", "disabled");
    $("#btnRemove").attr("disabled", "disabled");
    $("#btnContinue_C").attr("disabled", "disabled");
    $("#btnRemove_C").attr("disabled", "disabled");
}
function EnableBtn() {
    $("#btnToStatic").removeAttr("disabled");
    $("#btnContinue").removeAttr("disabled");
    $("#btnRemove").removeAttr("disabled");
    $("#btnContinue_C").removeAttr("disabled");
    $("#btnRemove_C").removeAttr("disabled");
}