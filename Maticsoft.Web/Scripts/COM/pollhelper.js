/**
* pollhelper.js
*
* 功 能： 提交投票结果
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年11月26日 17:04:25  Rock    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

function SelectOption() {
    var selectVal = "";
    $("input").each(function () {
        if ($(this).attr('checked')) {
            selectVal += $(this).attr('name') + '_' + $(this).attr('id') + ',';
        }
    });
    $("#MaticsoftHfPoll").val(selectVal);
    if (!$("#MaticsoftHfPoll").val()) {
        alert('对不起，请填写完全部投票结果！');
        return false;
    }
    $.ajax({
        url: '/COM/Poll/SubmitPoll',
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: {
            UID: -1,
            Option: $("#MaticsoftHfPoll").val(),
            FID: $("#MaticsoftHfPoll_FID").val()
        },
        success: function (resultData) {
            if (resultData.STATUS == "800") {
                alert("投票成功，感谢您的参与！");
            } else if (resultData.STATUS == "805") {
                alert(resultData.DATA);
            } else {
                alert("系统忙，请稍后再试！");
            }
        }
    });
}