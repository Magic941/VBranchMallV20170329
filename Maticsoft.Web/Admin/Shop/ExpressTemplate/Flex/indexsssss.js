//保存数据
function getData(){
	var dd = document.forms[0].emsname;
	var s = dd.value;
	if(s == null || s == "")
	{
	    alert("请填写单据名称");
	    return;
	}
	
	var flexApp = FABridge.example.root();	
	flexApp.addData(s);
	
//	var ii = document.forms[3].id;
//	ii.name = 2;
}
﻿//删除打印项数据
function delData(){
	var flexApp = FABridge.example.root();	
	flexApp.dele();
}
//设置字体
function fontfamily()
{
	var dd = document.forms[0].ffamily;
	var s = dd.options[dd.selectedIndex].value;
    var flexApp = FABridge.example.root();
	flexApp.setfamily( s );
}
//设置字体大小
function fontsize()
{
	var dd = document.forms[0].fsize;
	var s = dd.options[dd.selectedIndex].value;
    var flexApp = FABridge.example.root();
	flexApp.setsize( s );
}
//设置文字对齐方式
function updateBottom()
{
	var dd = document.forms[0].bottomDropDown;
	var s = dd.options[dd.selectedIndex].value;
    var flexApp = FABridge.example.root();   
	flexApp.setlabel(s);
	flexApp.setalign(s);	
}
//设置文字样式
function showstyle()
{
	var dd = document.forms[0].style;
	var s = dd.options[dd.selectedIndex].value;
    var flexApp = FABridge.example.root();
	flexApp.setstyle( s );
}
//添加打印项
function addbtn(t){
	
	var dd = document.forms[0].item;
	var s = dd.options[dd.selectedIndex].value +"_"+ t;
	
	var flexApp = FABridge.example.root();

	if(dd.options[dd.selectedIndex].value != "自定义内容"){

		flexApp.add( s ,false);
	}else{

		flexApp.add( s ,true);
	}
}
//删除背景图片
function imagebtn(){

	var flexApp = FABridge.example.root();
	flexApp.deleteimage();
}
//设置单据大小
function setfsize(){
    if(document.forms[0].swidth.value < 2000 && document.forms[5].sheight.value < 2000)
    {
	    var aa = document.forms[0].swidth;
	    var dd = aa.value;
	    var dwidth =aa.value/25.4*96;
    	
	    var bb = document.forms[5].sheight;
	    var dh = bb.value;
	    var dheight=bb.value/25.4*96;
    		
	    flexApp.width =dwidth;
	    flexApp.height =dheight;
    	
	    var flexA = FABridge.example.root();
	    flexA.setordsize(dd , dh);
	}else{
	    alert("单据尺寸宽、高分别不能超过2000mm");
	}
}
//显示隐藏栏
function clickbtn()
{

	var dd = document.forms[0].btnclick;
	var flexApp = FABridge.example.root();
	flexApp.showbrowse();
}
