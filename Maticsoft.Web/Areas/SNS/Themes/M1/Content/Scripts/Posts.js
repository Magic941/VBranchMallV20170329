/////上传图片方法的开始
//    var uploadfunction = function (type,control) {
//        var button = control, interval;
//        var fileType = "all", fileNum = "more";
//        new AjaxUpload(button, {
//            action: '/Ajax_Handle/SNSUploadPhoto.ashx',
//              data: {
//                'Type': type
//            },
//            name: 'myfile',
//            onSubmit: function (file, ext) {
//               $.jBox.tip("正在上传...", 'loading');
//                if (fileType == "pic") {
//                    if (ext && /^(jpg|png|jpeg|gif)$/.test(ext)) {
//                        this.setData({
//                            'info': '文件类型为图片'
//                        });
//                    } else {
//                        $('<li></li>').appendTo('#example .files').text('非图片类型文件，请重传');
//                        return false;
//                    }
//                }
//                button.val('上传中');
//                if (fileNum == 'one')
//                    this.disable();
//                interval = window.setInterval(function () {
//                    var text = button.val();
//                    if (text.length < 14) {
//                        button.val(text + '.');
//                    } else {
//                        button.val('上传中');
//                    }
//                }, 200);
//            },
//            onComplete: function (file, response) {
//           // alert(response);
//                if (response == "-1") {
//                    ShowFailTip("上传的图片不能大于500k！");
//                    window.clearInterval(interval);
//                    button.val('点击上传');
//                    return;
//                }
//                 $.jBox.tip('上传成功', 'success');
//                ImageUrl=response;
//                button.val('重新上传');
//                window.clearInterval(interval);
//            }
//        });
//    }
/////上传图片的方法的结束*@