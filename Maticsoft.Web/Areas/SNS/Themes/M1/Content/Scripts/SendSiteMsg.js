$(function () {
    $(".SendMsg").click(function (e) {
        e.preventDefault();
        if (!CheckUserState()) {
            return;
        }
        var NickName = $(this).attr("nickname");
        var html = "<div style='margin-left:40px;margin-top:10px;font-size: 16px;font-weight: bold;display:none'>发给：<input type='text' style='height: 20px;width: 220px;' id='nickName' name='nickName' value='" + NickName + "' /></div>"
         html+= "<div style='margin-left:40px;margin-top:10px;font-size: 16px;font-weight: bold;'>标题：<input type='text' style='height: 20px;width: 220px;' id='title' name='Title' /><span style='display:none;color:red;font-size: 16px;' name='tipname' id='tipname'>请输入账号</span></div>"
        html += "<div style='margin-left:40px;margin-top:20px;font-size: 16px;font-weight: bold;'>内容：<input type='text' style='height: 50px;width: 220px;' id='content' name='Content' /><span style='display:none;color:red;font-size: 16px;' name='tippwd' name='tippwd'>请输入密码</span></div>";
        html += "<div class='sm_submit_box' style='margin-left:80px;margin-top:20px;font-size: 16px;font-weight: bold;'><a href='#' class='sm_submit' id='sendmsgbtn'>发送</a></div>"

        html += " <input type='hidden' name='NickName' value='" + NickName + "' />"
        $.jBox(html, { title: "发私信", submit: submitSendMsg, width: 400, top: 300, height: 250, buttons: { '发送': true} });
        $(".jbox-button").hide();

    });
})
$("#sendmsgbtn").die("click").live("click", function () { $(".jbox-button").click(); });
var submitSendMsg = function (v, h, f) {
    if (f.Title == '') {
        return false;
    }
    if (f.Content == '') {
        return false;
    }
    $.ajax({
        type: "POST",
        dataType: "text",
        dataType: 'json',
        url: $Maticsoft.BasePath + "UserCenter/SendMsg",
        data: { NickName:f.NickName,Title:f.Title,Content:f.Content},
        success: function (data) {
            if (data.STATUS == "SUCC") {
                $.jBox.tip('发送成功', 'success');
            }
            else {
                $.jBox.tip('出现异常', 'error');
            }
        }
    });
    
    





}