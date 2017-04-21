
function quoted(str) 
{
     return (str != null)?'"'+str+'"':'""';
}	
   var keyStr = "ABCDEFGHIJKLMNOP" +
                "QRSTUVWXYZabcdef" +
                "ghijklmnopqrstuv" +
                "wxyz0123456789+/" +
                "=";

   function encode64(input) {
      input = escape(input);
      var output = "";
      var chr1, chr2, chr3 = "";
      var enc1, enc2, enc3, enc4 = "";
      var i = 0;

      do {
         chr1 = input.charCodeAt(i++);
         chr2 = input.charCodeAt(i++);
         chr3 = input.charCodeAt(i++);

         enc1 = chr1 >> 2;
         enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
         enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
         enc4 = chr3 & 63;

         if (isNaN(chr2)) {
            enc3 = enc4 = 64;
         } else if (isNaN(chr3)) {
            enc4 = 64;
         }

         output = output + 
            keyStr.charAt(enc1) + 
            keyStr.charAt(enc2) + 
            keyStr.charAt(enc3) + 
            keyStr.charAt(enc4);
         chr1 = chr2 = chr3 = "";
         enc1 = enc2 = enc3 = enc4 = "";
      } while (i < input.length);

      return output;
   }

   function decode64(input) {
      var output = "";
      var chr1, chr2, chr3 = "";
      var enc1, enc2, enc3, enc4 = "";
      var i = 0;

      // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
      var base64test = /[^A-Za-z0-9\+\/\=]/g;
      if (base64test.exec(input)) {
         alert("There were invalid base64 characters in the input text.\n" +
               "Valid base64 characters are A-Z, a-z, 0-9, '+', '/', and '='\n" +
               "Expect errors in decoding.");
      }
      input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

      do {
         enc1 = keyStr.indexOf(input.charAt(i++));
         enc2 = keyStr.indexOf(input.charAt(i++));
         enc3 = keyStr.indexOf(input.charAt(i++));
         enc4 = keyStr.indexOf(input.charAt(i++));

         chr1 = (enc1 << 2) | (enc2 >> 4);
         chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
         chr3 = ((enc3 & 3) << 6) | enc4;

         output = output + String.fromCharCode(chr1);

         if (enc3 != 64) {
            output = output + String.fromCharCode(chr2);
         }
         if (enc4 != 64) {
            output = output + String.fromCharCode(chr3);
         }

         chr1 = chr2 = chr3 = "";
         enc1 = enc2 = enc3 = enc4 = "";

      } while (i < input.length);

      return unescape(output);
}
function SetWinHeight(obj)
{
var win=obj;
if (document.getElementById)
{
    if (win && !window.opera)
    {
     if (win.contentDocument && win.contentDocument.body.offsetHeight) 

      win.height = win.contentDocument.body.offsetHeight; 
     else if(win.Document && win.Document.body.scrollHeight)
      win.height = win.Document.body.scrollHeight;
    }
}
}

MaticSoft = {}; MaticSoft.SomeApp = {};
var scripts = document.getElementsByTagName("script");
var param= eval(scripts[scripts.length - 1].innerHTML);
//"c=1&t=0&w=100&h=100"
var spparam = param.split('&');
var orgUrl = spparam[0];
param = param.replace(orgUrl+"&", '');
//maticsoft_ad_width = parseFloat(spparam[2].split('=')[1]) + 2; //iframe增加2像素宽度
//maticsoft_ad_height = parseFloat(spparam[3].split('=')[1]) + 2;//iframe增加2像素高度

var url= encode64(window.location);
maticsoft_ad_url = orgUrl+ "/Showad/MCFShow.aspx?" + param;

function SetCwinHeight(thisFrame) {
    var iframeid = thisFrame;
    if (document.getElementById) {
        if (iframeid && !window.opera) {
            if (iframeid.contentDocument && iframeid.contentDocument.body.offsetHeight) {
                iframeid.height = iframeid.contentDocument.body.offsetHeight;
                iframeid.width = iframeid.contentDocument.body.offsetWidth;
            } else if (iframeid.Document && iframeid.Document.body.scrollHeight) {
                iframeid.height = iframeid.Document.body.scrollHeight;
                iframeid.width = iframeid.Document.body.scrollWidth;
            }
        }
    }
}
//var tar = "<iframe style='width:auto;'  id='iframyee'  allowTransparency='true' onload=\"this.height=this.document.body.scrollHeight;\"   frameborder=0 bordercolor='#000000' hspace='0' scrolling=\"auto\" src='" + maticsoft_ad_url + "'></iframe>";
//var tar = "<iframe   id='iframyee'  allowTransparency='true' onload=\"SetCwinHeight(this) \"   frameborder=0 bordercolor='#000000' hspace='0' scrolling=\"auto\" src='" + maticsoft_ad_url + "' width='684px' height='263px'></iframe>";
(function() {
    document.write("<iframe   id='iframyee'  allowTransparency='true'  onload=\"SetCwinHeight(this) \"  frameborder=0 bordercolor='#000000' hspace='0' scrolling=\"no\" src='" + maticsoft_ad_url + "' width='0px' height='0px'></iframe>");
})();

