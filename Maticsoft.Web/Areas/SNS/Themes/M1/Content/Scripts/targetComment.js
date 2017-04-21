$(function () {
    $(".creat").die("click").live("click", function (e) {
        e.preventDefault();
        var TargetId = $(this).parents(".pcomentinput").find("#targetid").val();
        var Type = $(this).parents(".pcomentinput").find("#targetid").attr("imagetype");
        var Des = $(this).parents(".pcomentinput").find(".poster_textarea").val();
        if (Des.length == 0) {
            $.jBox.tip('内容不能为空', 'error');
            return;
        }
        if (ContainsDisWords(Des)) {
            $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
            return;
        }
        var obj = $(this);
        $.ajax({
            url: $Maticsoft.BasePath + "Profile/AjaxAddProductComment",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { Type: Type, TargetId: TargetId, Des: Des },
            success: function (resultData) {
                $(resultData).insertAfter(obj.parents(".search_iss").find(".pcomentinput"));
                obj.parents(".pcomentinput").find(".poster_textarea").val("");
                obj.parents(".pcomentinput").hide();
                obj.parents(".i_w_y").find(".commentcount").text(parseInt(obj.parents(".i_w_y").find(".commentcount").text()) + 1);
                //同步到微博
//                var Option = {
//                    Type: Type,
//                    TargetID: TargetId,
//                    ShareDes: Des,
//                    ImageUrl: obj.parents(".i_w_y").find(".emerge p img").attr("src")
//                };
//                InfoSync.InfoSending(Option);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                return 0;
            }
        });
    })

    $(".search_com_b").die("click").live("click", function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            var object = $(this).parent().next().find(".pcomentinput");
            if (object.length == 0) {
                $("#commentTelepate").find("#targetid").val($(this).find(".TargetId").val());
                $("#commentTelepate").find("#targetid").attr("imagetype",$(this).find(".TargetId").attr("imagetype"));
                $("#commentTelepate").find("img").attr("src", "/Upload/User/Gravatar/" + $("#currentUserId").val() + ".jpg")
                $(this).parent().next().prepend($("#commentTelepate").html());
            }
            else {
                object.slideToggle();
            }
        }
    })

})