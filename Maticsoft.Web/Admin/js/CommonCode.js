if (jQuery) {
    jQuery(function() {
        jQuery(':button, :submit').addClass("inputbutton").hover(function() {
            jQuery(this).addClass('inputbutton_hover');
        }, function() {
            jQuery(this).removeClass('inputbutton_hover');
        });
        SwitchTab("#prompt-note", 0, "on");//Tab式提示信息
    });
}
String.prototype.endWith = function(oString)
{   
    var reg = new RegExp(oString + "$");
    return reg.test(this);
}

function batchconfirm(prompt, nocheckprompt)
{
    var prompt = (arguments.length > 0) ? arguments[0] : "确定要进行此批量操作？";
    var nocheckprompt = (arguments.length > 1) ? arguments[1] : "请选择所要操作的记录！";
    var haschecked = false;
    for (var i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == "checkbox" && o.name.endWith("CheckBoxButton") && o.checked == true) 
        { 
            haschecked = true;
            break;
        } 
    } 
    if (!haschecked)
    {
        alert(nocheckprompt);
        return false;
    }
    else
    {
        if (!confirm(prompt))
        {
            return false;
        }
    }
}

function JumpToLeft(url)
{
    parent.frames["left"].location = url;
}

function ReloadLeft()
{
    parent.frames["left"].location.reload();
}

function Redirect(url)
{
    window.location = url;
}

function isSecurity(v) {
    if (v.length < 6) { iss.reset(v.length); return; }
    var lv = -1;
    var p1 = (v.search(/[a-zA-Z]/) != -1) ? 1 : 0;
    var p2 = (v.search(/[0-9]/) != -1) ? 1 : 0;
    var p3 = (v.search(/[^A-Za-z0-9_]/) != -1) ? 1 : 0;
    var lv = p1 + p2 + p3;
    switch (lv) {
        case 1:
            iss.level0();
            break;
        case 2:
            iss.level1();
            break;
        case 3:
            iss.level2();
            break;
        default:
            iss.reset(v.length);
    }
}
var iss = {
    width: ["60", "80", "100", "10"],
    reset: function(len) {
    document.getElementById("BarIndicator_TxtUserPassword").style.width = len * iss.width[3] + 'px';
    },
    level0: function() {
    document.getElementById("BarIndicator_TxtUserPassword").style.width = iss.width[0] + 'px';
    },
    level1: function() {
    document.getElementById("BarIndicator_TxtUserPassword").style.width = iss.width[1] + 'px';
    },
    level2: function() {
    document.getElementById("BarIndicator_TxtUserPassword").style.width = iss.width[2] + 'px';
    }
}
function Bardisplaynone() {
    document.getElementById("BarIndicator_TxtUserPassword").style.display = "none";
    document.getElementById("BarBorder_TxtUserPassword").style.display = "none";
}
function Bardisplayshow() {
    document.getElementById("BarIndicator_TxtUserPassword").style.display = "inline";
    document.getElementById("BarBorder_TxtUserPassword").style.display = "inline";
}

//通用切换
//containerId - 容器选择符(class, id, tag)
//defaultIndex - 默认选中的标签索引，从0开始
//titOnClassName -标签选中时的样式
//tagName - 可选参数，自定义标题标签，默认为li （dl>dt>ul>li）
function SwitchTab(containerId, defaultIndex, titOnClassName, tagName) {
    if (!jQuery) return;
    var st;
    var tagName = (tagName == '' || tagName == null || tagName == undefined) ? 'a' : tagName;
    var defaultIndex = (defaultIndex == '' || defaultIndex == null || defaultIndex == undefined) ? 0 : defaultIndex;
    var titOnClassName = (titOnClassName == '' || titOnClassName == null || titOnClassName == undefined) ? 'on' : titOnClassName;
    var obj = jQuery(containerId);

    //根据defaultIndex初始化
    obj.find("dd").hide();
    obj.find("dt " + tagName + ":eq(" + defaultIndex + ")").addClass(titOnClassName);
    obj.find("dd:eq(" + defaultIndex + ")").fadeIn({ queue: false, duration: 500 });

    //处理交互事件
    obj.find("dt " + tagName).each(function(i, ele) {
        jQuery(ele).click(function() {
            st = setTimeout(function() {
                ShowSTCon(obj, i, titOnClassName, tagName);
                st = null;
            }, 100);
        }, function() {
            if (st != null) clearTimeout(st);
        });
    });
}
function ShowSTCon(obj, i, titOnClassName, tagName) {
    obj.find("dt ." + titOnClassName).removeClass(titOnClassName);
    obj.find("dd").hide();
    obj.find("dt " + tagName + ":eq(" + i + ")").addClass(titOnClassName);
    obj.find("dd:eq(" + i + ")").fadeIn({ queue: false, duration: 500 }); //show();
}