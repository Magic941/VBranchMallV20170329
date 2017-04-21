/**
* maticsoft.jquery.seowordslink.js
*
* 功 能： 关键字加链接
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年10月15日 15:49:51  Rock    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
$.fn.extend({
    seowordslinks: function (isCms,isShop,isSNS,isComment) {
        var thisContent = $(this);
        $.ajax({
            url: "/Shopmanage.aspx",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            data: {
                action: "SEORelation",
                IsCMS: isCms,
                IsShop: isShop,
                IsSNS: isSNS,
                IsComment: isComment
            },
            success: function (resultData) {
                if (resultData.STATUS == "SUCCESS") {
                    var vaule = "";
                    for (var i = 0; i < resultData.DATA.length; i++) {
                        if (vaule == '')
                            vaule = resultData.DATA[i].KeyName;
                        else
                            vaule += "|" + resultData.DATA[i].KeyName;
                    }
                    var key = eval("/" + vaule + "/g");
                    $(thisContent).html($(thisContent).html().replace(key, function () {
                        var u = arguments[0];
                        for (var i = 0; i < resultData.DATA.length; i++) {
                            if (resultData.DATA[i]["KeyName"] == u) {
                                return u.link(resultData.DATA[i]["LinkURL"]);
                            }
                        }
                    }));
                }
            }
        });
    }
});