/*
**方法的入口
*/
$(document).ready(function () {
    $(".recommend").bind("click",
    function () {
        Recommend.Action();
        //alert("123");
        Recommend.showTip();
        return false;
    });
});

/*
**推荐
*/
var Recommend = {
    //程序的入口方法
    Action: function () {
        var url = location.href; //取当前页面的URL
        var title = $("#styleName").text(); // 取出商品的标题
        Recommend.copy_clip(url); // 将链接复制到剪贴板
    },
    //复制到剪贴板的方法
    copy_clip: function (copy) {
        if (window.clipboardData) {
            window.clipboardData.clearData();
            window.clipboardData.setData("Text", copy);
        } else if (navigator.userAgent.indexOf("Opera") != -1) {
            window.location = txt;
        } else if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');
            } catch (e) {
                alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
            }
            var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
            if (!clip) return;
            var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
            if (!trans) return;
            trans.addDataFlavor('text/unicode');
            var str = new Object();
            var len = new Object();
            var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
            var copytext = copy;
            str.data = copytext;
            trans.setTransferData("text/unicode", str, copytext.length * 2);
            var clipid = Components.interfaces.nsIClipboard;
            if (!clip) return false;
            clip.setData(trans, null, clipid.kGlobalClipboard);
        }
        //alert("已复制" + copy)
        return false;
    },
    //提示信息方法
    showTip: function () {
        //alert("链接已复制到剪贴板了！");
        var title = $("#styleName").text(); // 取出商品的标题
        var tip = $("<div id=\"Recommend_pop\" style=\"display:none;\" class=\"detail-pop\"><div class=\"title\">推荐给朋友</div><p>" + title + "<br/> 已经复制<br/>您可以按Ctrl+V粘贴到QQ、MSN或邮件中发送给好友了</p><div class=\"close-favlist\"><a href=\"#\" name=\"fClose\">关闭</a></div></div>");
        $("body").append(tip);
        $("#Recommend_pop").show();
        $("#Recommend_pop").find("a[name='fClose']").bind("click",
        function () {
            $("#Recommend_pop").remove();
            return false;
        });

        setTimeout(function () {
            $("#Recommend_pop").fadeOut("slow",
            function () {
                $(this).remove();
            });
        },
        4000);
    }
};