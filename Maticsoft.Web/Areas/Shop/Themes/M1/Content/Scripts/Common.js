
function ClosePage() {
    if (parent.RSMenu == null) {
        if (parent.top.RSMenu) {
            parent.top.RSMenu.Close();
        }
        else {
            window.close();
        }
    }
    else {
        if (parent.RSMenu.Close != null) {
            parent.RSMenu.Close();
        }
        else {
            window.close();
        }
    }
}

function Refresh() {
    document.forms(0).submit();
}

function RefreshParent() {
    if (parent.Refresh != null) {
        parent.Refresh();
    }
}

function RefreshWindow(windowName) {
    if (parent.RSMenu.Refresh != null) {
        parent.RSMenu.Refresh(windowName);
    }
}

function SetParentIframeHeight(iframeId) {
    if (iframeId == null) return;
    if (iframeId == "") return;
    if (parent.document.all(iframeId) == null) return;
    parent.document.all(iframeId).style.height = window.document.body.scrollHeight + 10
}


function ShowMessage(messageContent, x, y) {
    if (x == null) x = 0;
    if (y == null) y = 0;
    var win = document.createElement("div");
    win.id = "__ShowMesageWin";
    win.style.display = "block";
    win.className = "ShowMesage";
    var closeButton = document.createElement("button");
    closeButton.value = "确定";
    closeButton.valign = "bottom";

    closeButton.onclick = function () {
        document.body.removeChild(document.all("__ShowMesageWin"));
    }
    closeButton.className = "ShowMesageButton";
    var message = document.createElement("div");
    message.innerHTML = messageContent;
    win.appendChild(message);
    win.appendChild(document.createElement("br"));
    win.appendChild(closeButton)

    if (x > 0) win.style.left = x;
    if (y > 0) win.style.top = y;

    document.body.insertBefore(win, document.body.childNodes[0]);

}

function ShowProgress(content, offsetX, offsetY) {
    if (content == undefined || content == null) content = '正在处理，请稍候...';
    if (offsetX == undefined || offsetX == null) offsetX = 400;
    if (offsetY == undefined || offsetY == null) offsetY = 300;

    var divProgress = document.createElement('div');

    divProgress.innerHTML = content;

    divProgress.style.left = offsetX;
    divProgress.style.top = offsetY;

    divProgress.style.fontSize = 14;
    divProgress.style.background = '#c9d3f3';
    divProgress.style.padding = '0.5cm, 0.5cm, 0.5cm, 0.5cm';
    divProgress.style.zIndex = 99;
    divProgress.style.position = 'absolute';

    document.body.insertBefore(divProgress, document.body.childNodes[0]);
    //event.srcElement.parentNode.insertBefore(divProgress, document.body.childNodes[0]);
    //.srcElement.parentNode.childNodes[0]);
}

function EnterToTab() {
    if (window.event.keyCode == 13) {
        var obj = window.event.srcElement;
        if (obj.tagName.toLowerCase() == "button")
            window.event.keyCode = 20;
        else if (obj.tagName.toLowerCase() == "input"
	            && obj.type.toLowerCase() == "submit")
            window.event.keyCode = 20;
        else
            window.event.keyCode = 9;
    }

}

function ButtonDisableClick(button) {
    if (button == null) return true;
    if (button.IsClicked == true) {
        alert("您已经点击过了，系统还在处理，请稍候!");
        return false;
    }
    button.IsClicked = true;
    return true;

}

function ButtonEnableClick(button) {
    if (button == null) return true;
    button.IsClicked = false;
}

function CallbackContext(element, object) {
    this.Element = element;
    this.Object = object;
}


function RSBase() {

}

RSBase.prototype.SaveOriginalValues = function (columnNames) {
    for (var i = 0; i < columnNames.length; i++) {
        o = document.getElementById(columnNames[i]);
        if (o == null) {
            alert("元素" + columnNames[i] + "不存在.")
        }
        else
            this.SaveOriginalValue(o);
    }
}

RSBase.prototype.GetElementById = function (elementId) {
    var element = document.getElementById(elementId);
    if (element == null) {
        alert('控件编号: ' + elementId + '不存在!');

        return null;
    }

    return element;
}

RSBase.prototype.SaveOriginalValue = function (element) {
    element.OriginalValue = element.value;
    //alert(element.OriginalValue);
}

RSBase.prototype.GetOriginalValue = function (element) {
    var value = element.OriginalValue;
    if (value == "undefined") value = "";
    if (value == undefined) value = "";
    return value;
}

RSBase.prototype.HasOriginalValue = function (element) {
    var value = element.OriginalValue;
    return (value != "undefined");
}


RSBase.prototype.RestoreOriginalValue = function (element) {
    if (this.HasOriginalValue(element)) {
        element.value = this.GetOriginalValue(element);
    }
}

RSBase.prototype.CreateCallbackContext = function (element) {
    return new CallbackContext(element, this);
}

RSBase.prototype.ErrorCallback = function (error, callBackContext, response) {
    var errorString = ""; //= "Test '" + userContext + "' failed!";
    if (error == null) {
        errorString += "  Status code='" + response.get_statusCode() + "'";
    }
    else {
        errorString += error.get_message()
    }

    alert(errorString);

    if (callBackContext != null) {
        if (callBackContext.Object != null) {
            if (callBackContext.Object.CustomErrorHandle != null) {
                callBackContext.Object.CustomErrorHandle(callBackContext);
            }
        }
    }

    if (callBackContext != null) {
        if (callBackContext.Element != null) {
            callBackContext.Element.focus();

            if (callBackContext.Object.RestoreOriginalValue) {

                if (!callBackContext.Object.IsButton(callBackContext.Element)) {
                    callBackContext.Object.RestoreOriginalValue(callBackContext.Element);
                }
            }
        }
    }

}

RSBase.CustomErrorHandle = function (callbackContext) {
    // 自定义错误处理，供重载
}

RSBase.prototype.IsButton = function (element) {
    var tagName = element.tagName.toLowerCase();
    var type = element.type.toLowerCase();
    if (tagName == "button")
        return true;
    if (tagName == "input" && (type == "button" || type == "submit"))
        return true;
    else
        return false;
}

RSBase.GetCallbackContext = function (element) {
    return new CallbackContext(element, this);
}


function CheckDisabledWhenClick() {
    if (document.activeElement.disabledWhenClick != undefined) {
        var disabledWhenClickValue = document.activeElement.disabledWhenClick.toUpperCase();

        if (disabledWhenClickValue == "CLICKED") {
            alert("已经执行中，请稍候．．．");
            return false;
        }
    }
    document.activeElement.disabledWhenClick = "CLICKED";
    return true;
}

String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

String.prototype.LTrim = function () {
    return this.replace(/(^\s*)/g, "");
}

String.prototype.RTrim = function () {
    return this.replace(/(\s*$)/g, "");
}

function Cancel(isConfirm) {
    if (isConfirm) {
        if (confirm('是否关闭？')) {
            ClosePage();
        }

        return;
    }

    ClosePage();
}

// 暂停
function Pause(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;

    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}
