// Get browser type
var IE = 1;
var NS = 2;
function BrowserType()
{
	if (navigator.appName.toLowerCase().indexOf("microsoft") >= 0) {
		return IE;
	} else if (navigator.appName.toLowerCase().indexOf("netscape") >= 0) {
		return NS;
	}
}
// Create XML Http object
function CreateXMLHttp()
{
	var xmlHttp = null;
	if (BrowserType() == IE) {
		xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
	} else if (BrowserType() == NS) {
		xmlHttp = new XMLHttpRequest();
	}
	return xmlHttp;
}
//Create XML document
function CreateXmlDoc(){
	var xmldoc = null;
	if(BrowserType() == IE){
		xmldoc = new ActiveXObject("MSXML2.DOMDocument");
	}else if(BrowserType() == NS){
		xmldoc = document.implementation.createDocument("", "", null);
	}
	return xmldoc;
}
//Create XML document
function GetXmlDoc(result){
	var xmldoc = null;
	if(BrowserType() == IE){
		xmldoc = new ActiveXObject("MSXML2.DOMDocument");
				xmldoc.loadXML(result);
			}else if(BrowserType() == NS){
				xmldoc = document.implementation.createDocument("", "", null);
				var oDomP=new DOMParser();
				xmldoc=oDomP.parseFromString(result,"text/xml");
				
			}
	return xmldoc;
}
