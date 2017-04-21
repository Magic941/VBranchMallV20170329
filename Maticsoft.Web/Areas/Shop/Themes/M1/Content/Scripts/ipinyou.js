var adownerid = "";
var adpid = "194335";
window.onerror = function () {
    return true
};
var toprefer = "ZJSTAT";
var parentlocation = "";
var parentrefer = "ZJSTAT";
var selflocation = window.location;
var selfrefer = document.referrer;
var realrefer = "";
var reallocation = "";
var hourvisitnum = 1;
var realvisitnum = 1;
var nowdate = new Date();
var clientcolor = "";
if (navigator.appName == "Netscape") {
    clientcolor = screen.pixelDepth;
} else {
    clientcolor = screen.colorDepth;
}

parentlocation = window.parent.location;

realrefer = selfrefer;
if (parentrefer !== "ZJSTAT") {
    realrefer = parentrefer;
}
if (toprefer !== "ZJSTAT") {
    realrefer = toprefer;
}
reallocation = parentlocation;
try {
    lainframe
} catch (e) {
    reallocation = selflocation;
}
document.writeln('<div style="visibility: hidden;" ><img style="width:0px;height:0px" src="http://www.ipinyou.com/collect_m18.jsp?collectCodeType=4&hourVisitNum=' + hourvisitnum + '&totalVisitNum=' + realvisitnum + '&zone=' + (0 - nowdate.getTimezoneOffset() / 60) + '&screenColor=' + clientcolor + '&screen=' + screen.width + ',' + screen.height + '&referUrl=' + escape(realrefer) + '&url=' + escape(reallocation) + '&adpid=' + adpid + '&adownerid=' + adownerid + '"/></div>');