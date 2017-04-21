
function ltrim(s){
	return s.replace( /^\s*/, "");
}



//去右空格;
function rtrim(s){
	return s.replace( /\s*$/, "");
}

//左右空格;
function trim(s){
	return rtrim(ltrim(s));
}

//判断是否为空开始
function isnotnull(str){
if (str.length==""){
  return(false);
}
else{
  return(true);
}
}
function alertMsg(ParamMsg,toElementFocus){
	alert(ParamMsg);
	toElementFocus.focus();
}
function isEqual(m,n){
	if (m==n){
		return true;	
	}
	else{
		return false;
	}
	
}

//判断是否为空结束


//限制输入字符的位数开始
//m是用户输入，n是要限制的位数
function issmall(m,n){
if ((m<n) && (m>0)){
  return(true);
 }
else{
	return(false);
}
}

//限制输入字符的位数结束
//判断是否为电话号码开始
function istel(telstr)
{
   var reg=/[^0-9-]/g;
   var reg1=/^[^0-1]/g;
   if (telstr!="") {
   if (reg1.test(telstr)){
   return(false);
   }
   else
   {
   return(true);
  }
  }
   if (telstr!="") {
   if (reg.test(telstr)){
   return(false);
   }
   else
   {
   return(true);
   }
   }
}




function isConSameChar(strInput)
{
    if(strInput!="")
    {
        var charInput=strInput.substr(0,1);
        var lenInput=strInput.length;
        var str = "/^(["+charInput+"]"+"{"+lenInput+"})+$/";
        var reg=eval(str);
        return reg.test(strInput); 
    } 
    else
    {
        return true;
    }
}

function isConSpecChar(strInput)
{
    var flag=false;
    var list=new Array("123456","654321");
    for(i=0;i<list.length;i++)
    {
        if(strInput==list[i])
        {
            flag=true;break;
        }
    }
    return flag;
}



//判断是否为电话号码结束
//function setCookie(CookieName,CookieVal){ 
//     var Then=new Date();
//     Then.setTime(Then.getTime()+30*24*60*60*1000);//保存一个月
//     document.cookie=""+CookieName+"="+escape(CookieVal)+";expires="+Then.toGMTString();
//}
//
//function getCookie(CookieName111,desName){
//	 var cookieString=new String(document.cookie);
//     var cookieHeader=CookieName111+"="
//     var beginPosition=cookieString.indexOf(cookieHeader)
//     if (beginPosition!=-1){
//		   document.all(""+desName+"").value = unescape(cookieString.substring(beginPosition + cookieHeader.length,cookieString.indexOf(";",beginPosition))) 		   
//     }
//     else
//	 {
//           document.all(""+desName+"").value = "示例:0105166XXXX 或手机:1369129XXXX";
//     }
//	// window.alert(cookieString);
//}



function $(obj)
{
    if (typeof(obj) == "string")
        return document.getElementById(obj);
    else
        return obj;
}


function trimStr(str)
{
    if( str == undefined || str == null ) return '';
    
    return str.replace(/(^\s*)|(\s*$)/g,'');
}

function isEmptyStr(obj)
{
    return ''==trimStr(obj);
}

function validate()
{
    var username=$("txtUsername");
    var password=$("txtPass");
    var checkcode=$("CheckCode");
    if(isEmptyStr(username.value))
    {
        alert("Please enter a username！");
        username.focus();
        return false;
    }
    if(isEmptyStr(password.value))
    {
        alert("Please enter a password！");
        password.focus();
        return false;
    }
    
    if(isEmptyStr(checkcode.value))
    {
        alert("Please enter a Securitycode！");
        checkcode.focus();
        return false;
    }
    return true;
}



function focusNext(nextName,evt,num,t,lastName) {
evt = (evt) ? evt : event;
var charCode = (evt.charCode) ? evt.charCode :
((evt.which) ? evt.which : evt.keyCode);
if (charCode == 13 || charCode == 3) {
var nextobj=document.getElementById(nextName);
var lastobj=document.getElementById(lastName);

if(num==1 && isEmptyStr(t.value))
{
    alert("Please enter a username！");
    t.focus();
    return false;
}


if(num==2 && isEmptyStr(t.value))
{
    alert("Please enter a password！");
    t.focus();
    return false;
}

if(lastobj!=null && isEmptyStr(lastobj.value))
{
    alert("Please enter a username！");
    lastobj.focus();
    return false;
}

nextobj.focus();
return false;
}
return true;
}

