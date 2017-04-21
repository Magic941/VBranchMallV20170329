$(function () {
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
    $("#ctl00_ContentPlaceHolder1_txtFrom").prop("readonly", true).datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        dateFormat: "yy-mm-dd",
        onClose: function (selectedDate) {
            $("#ctl00_ContentPlaceHolder1_txtTo").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#ctl00_ContentPlaceHolder1_txtTo").prop("readonly", true).datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        dateFormat: "yy-mm-dd",
        onClose: function (selectedDate) {
            $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker("option", "maxDate", selectedDate);
        }
    });
    var taskCount = $("#ctl00_ContentPlaceHolder1_txtTaskCount").val();
    if (taskCount > 0) {
        $("#txtRemain").show();
    } //开始新的任务
    $("#btnToStatic").click(function () {
        var type = $("#hfTaskType").val();
        DisableBtn();
        taskCount = $("#ctl00_ContentPlaceHolder1_txtTaskCount").val();
        if (taskCount > 0) {
            if (confirm("上次有未完成的任务,是否覆盖未完成的任务?")) {
                RunTask(type);
            } else {
                EnableBtn();
            }
        } else {
            RunTask(type);
        }
    }); //继续未完成的任务
    $("#btnContinue").click(function () {
        DisableBtn();
        var type = $("#hfTaskType").val();
        ContinueTask(taskCount, type);
    })
    //清除未完成的任务
    $("#btnRemove").click(function () {
        var type = $("#hfTaskType").val();
        RemoveTask(type);
    });
});
function doProgressbar(count, i,type) {
    $("#probar").show();
    $.ajax({
        url: ("/ImageReGenHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        data: {
            action: "GenerateImage",
            TaskId: i,
            TaskType: type
        },
        success: function(result) {
            if (i <= count) {
                $("#progressbar").progressbar({
                    value: i
                });
                $("#txtCount").text(i);
                i++;
                doProgressbar(count, i, type);
            } else {
                alert("已全部生成成功");
                EnableBtn();
                RemoveTask(type);
            }
        }
    });
} //执行新任务
function RunTask(type) {
    $.jBox.tip("Initializing Task List, please wait ...", 'loading');
    $.ajax({
        url: ("/ImageReGenHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        data: {
            action: "HttpToGen",
            TaskType:type,
            Callback: "true",
            From: $("#ctl00_ContentPlaceHolder1_txtFrom").val(),
            To: $("#ctl00_ContentPlaceHolder1_txtTo").val()
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
                    alert("该条件下没有需要生成的图片");
                    EnableBtn();
                }
            }
        }
    });
} //继续任务 断点续传功能
function ContinueTask(taskCount,type) {
    $.ajax({
        url: ("/ImageReGenHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        data: {
            action: "ContinueTask",
            TaskType: type
        },
        success: function(result) {
            if (result.STATUS == "SUCCESS" && result.DATA > 0) {
                $("#progressbar").progressbar({
                    max: taskCount,
                    value: result.DATA
                });
                $("#txtTotalCount").text(taskCount);
                $("#txtCount").text(result.DATA);
                $("#probar").show();
                doProgressbar(taskCount, result.DATA,type);
            }
        }
    });
} //清除任务
function RemoveTask(type) {
    $.ajax({
        url: ("/ImageReGenHandler.aspx?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        data: {
            action: "DeleteTask",
            TaskType: type
        },
        success: function() {
            $("#probar").hide();
            $("#txtRemain").hide();
            $("#ctl00_ContentPlaceHolder1_txtTaskCount").val(0);
            EnableBtn();
        }
    });
}
function DisableBtn() {
    $("#btnToStatic").attr("disabled", "disabled");
    $("#btnContinue").attr("disabled", "disabled");
    $("#btnRemove").attr("disabled", "disabled");
}
function EnableBtn() {
    $("#btnToStatic").removeAttr("disabled");
    $("#btnContinue").removeAttr("disabled");
    $("#btnRemove").removeAttr("disabled");
}