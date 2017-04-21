function textCounter(fieldId, countfieId, maxlimit) {
    var fieldEle = fieldId;
    var countfieldEle = $("#" + countfieId + "");
    if (fieldEle == null || countfieldEle == null) {
        return false;
    }
    if (fieldEle.value.gblen() > maxlimit) // too long... trim it
    //fieldEle.value = fieldEle.value.substring(0, maxlimit);
    {
        ShowFailTip("<img src=\"/Content/face/em1.gif\" />亲，您输的太多了...");
        fieldEle.value = fieldEle.value.gbtrim(maxlimit, '');
    }
    else {
        countfieldEle.text(maxlimit - fieldEle.value.gblen());
    }

}
String.prototype.gblen = function () {
    var len = 0;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len += 1;
        }
        else {
            len++;
        }
    }
    return len;
}
String.prototype.gbtrim = function (len, s) {
    var str = '';
    var sp = s || '';
    var len2 = 0;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len2 += 1;
        }
        else {
            len2++;
        }
    }
    if (len2 <= len) {
        return this;
    }
    len2 = 0;
    len = (len > sp.length) ? len - sp.length : len;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) == 94) {
            len2 += 1;
        }
        else {
            len2++;
        }
        if (len2 > len) {
            str += sp;
            break;
        }
        str += this.charAt(i);
    }
    return str;
}