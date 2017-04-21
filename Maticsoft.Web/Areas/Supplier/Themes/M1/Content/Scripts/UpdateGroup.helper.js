
function ModifySave() {
            var tName = $("#GroupName");
            if (!tName.val()) {
                ShowFailTip("分组名称不能为空");
                return;
            }
            $.ajax({
                url: ($Maticsoft.BasePath + "WeChat/UpdateGroup?timestamp={0}").format(new Date().getTime()),
                type: 'POST',
                dataType: 'json',
                timeout: 10000,
                async: false,
                //        data: { oldPassword: txtPwd.val(), newPassword: txtNewPwd.val(), confirmPassword: txtConfirmPwd.val() },
                data: $('#updateGroup').serializeArray(),
                success: function (resultData) {
                    if (resultData["Result"] == "OK") {
                        ShowSuccessTip("操作成功!");
                        setTimeout("DelayTime()", 2000);
                    } else {
                        ShowFailTip("操作失败!");
                    }
                }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                    ShowFailTip(xmlHttpRequest.responseText);
                }
            });
        }
        function DelayTime() {
            window.parent.location.reload(); 
        }