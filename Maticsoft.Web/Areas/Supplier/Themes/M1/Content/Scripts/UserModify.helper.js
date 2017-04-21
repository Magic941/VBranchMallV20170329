    
        $(function () {
            $("#txtCancel").click(function () {
                window.location.href = $Maticsoft.BasePath + 'Home/UserInfo';
            });
        });
        function ModifySave() {
            var txtName = $("#txtName");
            var txtTrueName = $("#txtTrueName");
            var txtEmail = $("#txtEmail");
           
            if (!txtTrueName.val()) {
                ShowFailTip("真实姓名不能为空");
                return;
            }
            $.ajax({
                url: ($Maticsoft.BasePath + "Home/UserModify?timestamp={0}").format(new Date().getTime()),
                type: 'POST',
                dataType: 'json',
                timeout: 10000,
                async: false,
                data: { txtName: txtName.val(), txtTrueName: txtTrueName.val(), txtEmail: txtEmail.val() },
                //  data: $('#btnModify').serializeArray(),
                success: function (resultData) {
                    if (resultData["Result"] == "OK") {
                        ShowSuccessTip("修改成功!");
                    } else {
                        ShowFailTip("修改失败!");
                    }
                }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                    ShowFailTip(xmlHttpRequest.responseText);
                }
            });
        }