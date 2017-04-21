/*
 注意： 
 此文件只允许用于添加对应的跨域方法，如若有其他非跨域方法请在其他文件中进行维护
*/
/*
Bug4762 对用户的评论进行投票 跨域使用
*/
if (typeof (m18) == "undefined"
|| m18 == null) {
    m18 = {};
}
maticsoftment = {

    /* 评论投票结果
    0 成功 1 失败（不能给自己投票） 2失败（已经投过票） 3 未登录  -1 要投票的评论编号或者投票人ContactId不能为空
    */
    ResultStatus: 3

    // 跨域访问地址
    , CrossDoaminUrl: "http://comm.maticsoft.com/Service/CrossDomain.ashx"

    // 是否出错
    , IsError: false

    , SetUseful: function(CommentId, CommentContactId, CallbackFunName) {
        $.ajax({
            url: maticsoftment.CrossDoaminUrl,
            data: "Method=SetFavoriteComment&CommentId=" + CommentId + "&CommentContactId=" + CommentContactId,
            dataType: "jsonp",
            jsonp: "jsoncallback",
            success: function(data) {
                if (data != null) {
                    maticsoftment.ResultStatus = data.Status;
                    CallbackFunName();
                }
            },
            error: function(result) {
                maticsoftment.IsError = true;
            }
        });
    }

}