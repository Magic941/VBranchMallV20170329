if (typeof (window.m18) == "undefined" || window.m18 == null) {
    window.m18 = {};
}

window.m18.IPAssistant = {

    // Cookie名称
    CookieName: 'M18_User_Area'

    //Coolie有效期 单位:天
    ,
    Expires: 365

    // 作用域
    ,
    Domain: '.maticsoft'

    // 调用前 请赋值（为当前发布站点 eg:http://tools.maticsoft.com/）
    ,
    ServerPath: ''

    ,
    AreaInfos: {

        // 地区名称
        AreaName: ''

        // 邮编
        ,
        Postcode: ''

        /*  获取IP操作类型
        *   -1服务器异常获取失败返回默认“上海市”
        *   0 服务端获取IP对应的CityName失败  
        *   1 服务端成功获取IP对应的CityName
        *   2 客户端设置过Cookie
        */
        ,
        SelectionType: 2
    }

    ,
    GetAreaInfoByRequest: function (CallbackFunName) {
        if (m18.IPAssistant.ServerPath.length <= 0) {
            alert("调用前请先初始化ServerPath为当前发布站点如：http://www.maticsoft");
            return;
        }
        var data = $.cookie(m18.IPAssistant.CookieName);
        if (data == null) {
            /*
            本地Cookie不存在 则从服务端获取
            */
            $.ajax({
                url: m18.IPAssistant.ServerPath + "/IPAssistant.aspx",
                data: "Method=GetAreaInfosByRequest",
                dataType: "jsonp",
                jsonp: "jsoncallback",
                success: function (data) {
                    if (data != null) {
                        m18.IPAssistant.AreaInfos.AreaName = data.AreaName;
                        m18.IPAssistant.AreaInfos.Postcode = data.Postcode;
                        m18.IPAssistant.AreaInfos.SelectionType = data.SelectionType;
                        var cookieValue = data.AreaName + "|" + data.Postcode + "|" + data.SelectionType;
                        $.cookie(m18.IPAssistant.CookieName, cookieValue, {
                            expires: m18.IPAssistant.Expires,
                            domain: m18.IPAssistant.Domain
                        }); // 存储 cookie
                        m18.IPAssistant.SaveIPAssistantObject(data, CallbackFunName);

                    }
                },
                error: function (result) {
                    var data = [];
                    m18.IPAssistant.AreaInfos.AreaName = data["AreaName"] = "上海市";
                    m18.IPAssistant.AreaInfos.Postcode = data["Postcode"] = "";
                    m18.IPAssistant.AreaInfos.SelectionType = data["SelectionType"] = -1;
                    m18.IPAssistant.SaveIPAssistantObject(data, CallbackFunName);
                }
            });
        } else {
            var cookieObj = data.split('|');
            m18.IPAssistant.AreaInfos.AreaName = cookieObj[0];
            m18.IPAssistant.AreaInfos.Postcode = cookieObj[1];
            if (cookieObj.length > 2) {
                m18.IPAssistant.AreaInfos.SelectionType = cookieObj[2];
            } else {
                m18.IPAssistant.AreaInfos.SelectionType = 1;
            }
            CallbackFunName();
        }
    }

    /* 保存Cookie
    */
    ,
    SaveIPAssistantObject: function (ResultData, CallbackFunName) {
        var cookieValue = ResultData.AreaName + "|" + ResultData.Postcode + "|" + ResultData.SelectionType;
        $.cookie(m18.IPAssistant.CookieName, cookieValue, {
            expires: m18.IPAssistant.Expires,
            domain: m18.IPAssistant.Domain
        }); // 存储 cookie
        CallbackFunName();
    }

    ,
    SetInfos: function (AreaName, Postcode) {
        var rest = false;
        if (AreaName.length == 0 && Postcode.length == 0) return false;
        var data = $.cookie(m18.IPAssistant.CookieName);
        if (data != null) {
            var cookieObj = data.split('|');
            if (AreaName != 'undefined') cookieObj[0] = AreaName;
            if (Postcode != 'undefined') cookieObj[1] = Postcode;
            //修改操作对象
            m18.IPAssistant.AreaInfos.AreaName = cookieObj[0];
            m18.IPAssistant.AreaInfos.Postcode = cookieObj[1];
            m18.IPAssistant.AreaInfos.SelectionType = 2;
            var cookieValue = cookieObj[0] + "|" + cookieObj[1] + "|" + m18.IPAssistant.AreaInfos.SelectionType;
            $.cookie(m18.IPAssistant.CookieName, cookieValue, {
                expires: m18.IPAssistant.Expires,
                domain: m18.IPAssistant.Domain
            });
            rest = true;
        }
        return rest;
    }

    ,
    SetAreaNameInfo: function (AreaName) {
        if (AreaName.length == 0) return false;
        return m18.IPAssistant.SetInfos(AreaName, 'undefined');
    }

    ,
    SetPostcodeInfo: function (Postcode) {
        if (Postcode.length == 0) return false;
        return m18.IPAssistant.SetInfos('undefined', Postcode);
    }

}

/* Cookie 操作
*/
jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie 
        options = options || {};
        if (value === null) {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        var path = options.path ? '; path=' + options.path : '';
        var domain = options.domain ? '; domain=' + options.domain : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};