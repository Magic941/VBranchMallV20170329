if (typeof HTMLElement != 'undefined') HTMLElement.prototype.contains = function (o) {
    //扩展非IE浏览器下的contains方法
    if (this == o) return true;
    while (o = o.parentNode) if (o == this) return true;
    return false;
}

var RequestMemberInfoUri = 'http://myshishang.maticsoft.com/memberbehavior/getmemberinfo.ashx';
var RequestDoFollowUri = 'http://myshishang.maticsoft.com/memberbehavior/memberfollowinsert.ashx';

var memberInfo = null;
var mouseOutTimer = false;

var MemberCard = {

    // 获得元素的绝对坐标
    GetAbsolutePoint: function (element) {
        var result = {
            x: element.offsetLeft,
            y: element.offsetTop
        };
        element = element.offsetParent;
        while (element) {
            result.x += element.offsetLeft;
            result.y += element.offsetTop;
            element = element.offsetParent;
        }
        return result;
    },

    // 创建MemberInfo实例
    GetMemberInfo: function (sender, contactId) {
        $.getJSON(RequestMemberInfoUri + "?contactid=" + contactId + "&jsoncallback=?",
        function (data) {
            if (data != null && data.IndexpageUrl.length > 0) {
                $('#divmembercard').remove();
                memberInfo = new MemberInfo(data);

                // 构造Card显示的层得HTML
                var renderHtml = '';
                var sexVal = 0;
                var point = MemberCard.GetAbsolutePoint(sender);
                var popClass = 'detailbox2';
                var popLeft = point.x - 4;
                var popTop = point.y + 54;
                if (point.x > 650) {
                    popClass = 'detailbox3';
                    popLeft = point.x - 216;
                }

                renderHtml += '<div id="divmembercard" onmouseover="clearTimeout(mouseOutTimer)" onmouseout="MemberCard.Hide(event)" ';
                renderHtml += 'style="filter:Alpha(opacity=95); -moz-opacity: 0.95;opacity: 0.95; position:absolute;z-index:100;" class="' + popClass + ' upbox clearfix none">';
                renderHtml += '<div id="divsubmembercard" class="fl tc mr10 hz">';
                renderHtml += '<a target="_blank" href="' + memberInfo.IndexpageUrl + '" class="avatar_75 mb5">';
                renderHtml += '<img src="' + memberInfo.AvatarUrl + '"></a>';
                var sexNick = memberInfo.Sex == 1 ? '他' : '她';
                if (memberInfo.IsFollowd == '0') {
                    renderHtml += "<div contactid='" + contactId + "' onclick='javascript:MemberCard.DoFollow(this);'>";
                    renderHtml += '<a class="btn-hl" href="#11"><span>+ 关注' + sexNick + '</span></a></div>';
                } else {
                    if (memberInfo.IsEachFollowed == '1') {
                        renderHtml += '<a class="btn-hascare">互相关注</a>';
                    } else {
                        renderHtml += '<a class="lower">已关注</a>';
                    }
                }
                renderHtml += '</div><div><div class="mb5">';
                renderHtml += '<p class="fb f16 mb5">' + memberInfo.NickName + '</p>';
                renderHtml += '<img src="' + memberInfo.GrandImgUrl + '">';
                //renderHtml += '</div><div class="quiet mb5">' + memberInfo.MemberDesc + '</div>';隐藏来自于……
                renderHtml += '</div><div class="quiet mb5">&nbsp;</div>';
                renderHtml += '<div class="mb10"><span class="lower">粉丝 </span>';
                renderHtml += '<a class="a2">' + memberInfo.FansCount + '</a>';
                renderHtml += '<span class="lower ml5 mr5">|</span><span class="lower">关注 </span>';
                renderHtml += '<a class="a2">' + memberInfo.FollowCount + '</a>';
                renderHtml += '</div> <div class="tr">';
                renderHtml += '<a target="_blank" href="' + memberInfo.IndexpageUrl + '" class="a2">访问' + sexNick + '的MY时尚<span class="simsun">></span></a>';
                renderHtml += '</div></div></div>';

                $('body').append(renderHtml);
                $('#divmembercard').css({
                    left: popLeft + 'px',
                    top: popTop + 'px'
                });
                $('#divmembercard').show(100);
            }
        });
    },

    // 显示会员信息名片
    Show: function (sender, contactId) {
        if (mouseOutTimer) {
            clearTimeout(mouseOutTimer);
        }
        t = setTimeout(function () {
            MemberCard.GetMemberInfo(sender, contactId);
        },
        450);
    },

    // 执行隐藏
    Hide: function (e) {
        var dv = document.getElementById('divmembercard');
        if (e === true) {
            //计时器直接隐藏
            $('#divmembercard').hide();
        } else {
            var refObj = e.toElement || e.relatedTarget;
            try {
                if (!dv.contains(refObj)) {
                    //不是div的子控件则隐藏
                    $('#divmembercard').hide();
                }
            } catch (e) { };
        }
    },

    // 隐藏弹出层
    PopHide: function () {
        clearTimeout(t);
        mouseOutTimer = setTimeout(function () {
            MemberCard.Hide(true);
        },
        450);
    },

    // 关注
    DoFollow: function (sender) {
        var contactId = $(sender).attr('contactid');
        $.getJSON(RequestDoFollowUri + '?contactid=' + contactId + '&jsoncallback=?',
        function (data) {
            if (null != data.result && data.result.length > 0) {
                if (data.result == '-2') {
                    // 用户没有登录或登录超时弹出登录窗口
                    $("body").AjaxLogin({
                        success: function () {
                            MemberCard.DoFollow(sender);
                        }
                    });
                    return;
                } else if (data.result == '-4') {
                    alert('抱歉，关注失败。不能关注自己哦。');
                    return;
                }
                //else if (data.result == '1') {
                //  $('<a class="lower" href="javascript:void(0);">已关注</a>').insertAfter($(sender));
                //  $(sender).hide();
                //} else if (data.result == '11') {
                //   $('<a class="btn-hascare" href="javascript:void(0);">互相关注</a>').insertAfter($(sender));
                //    $(sender).hide();
                //}
                //关注成功后更新关注分组
                $('#divmembercard').hide();
                Member.UpdateFollowGroup(contactId);
            }
        });
    }
}

// 定义一个MemberInfo类
function MemberInfo(data) {
    this.Hot = false;
    this.IndexpageUrl = data.IndexpageUrl;
    this.AvatarUrl = data.AvatarUrl;
    this.IsFollowd = data.IsFollowd;
    this.IsEachFollowed = data.IsEachFollowed;
    this.NickName = data.NickName;
    this.GrandImgUrl = data.GrandImgUrl;
    this.MemberDesc = data.MemberDesc;
    this.FansCount = data.FansCount;
    this.FollowCount = data.FollowCount;
    this.Sex = data.Sex;
}