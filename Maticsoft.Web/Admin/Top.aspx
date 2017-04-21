<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="Maticsoft.Web.Admin.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="/admin/css/admin.css" rel="stylesheet" type="text/css">
    <script src="/Admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
</head>
<body >
        
<div class="top">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="193" valign="top"><img src="images/logo.gif" /></td>
    <td> <div class="nav">
    <ul class="TabBarLevel1" id="TabPage1">
    <%--<li id="Tab1" class="Selected"><a href="main_index.html" target="mainFrame" onclick="javascript:switchTab('TabPage1','Tab1');">栏目管理</a></li>
        <li id="Tab2"><a href="left_news.html" target="leftFrame" onclick="javascript:switchTab('TabPage1','Tab2');">内容管理</a></li>
        <li id="Tab3"><a href="left_pro.html" target="leftFrame" onclick="javascript:switchTab('TabPage1','Tab3');">商品管理</a></li>--%>
        
        <%=strMenu %>

        </ul>
    </div>
    </td>
  </tr>
</table>
    <div class="righttop">
<ul>
<li class="righttophelp"><a href="http://www.maticsoft.com/contact.aspx" target="_blank">购买咨询</a></li>
<li class="righttopabout"><a href="http://www.maticsoft.com/" target="_blank">关于</a></li>
</ul>
</div>    
</div>

<div class="adminmenu">欢迎您，<%=username%> <em>|</em> <a href="javascript:;">账号管理</a> <em>|</em> <a href="javascript:;">充值</a> <em>|</em> <a href="javascript:;">编辑</a> <em>|</em> 

<a href="javascript:;" class="hover_url"><font color="#3186c8" style="text-decoration:underline;">0</font></a> <span class="hover_url1">条信息</span></div>

<div class="logionout">
<div class="adminleft">
<ul>
<li><a href="javascript:;">系统设置</a></li>
<li class="padd1"><a href="logout.aspx">退出</a></li>
</ul>
</div>
<div class="adminright"><a href="javascript:;" class="daohanglink">系统管理平台</a> ><a href="javascript:;" class="sel">首页</a></div>
</div>
 <script language="JavaScript" type="text/javascript">
     //Switch Tab Effect
     function switchTab(tabpage, tabid) {
//         var oItem = document.getElementById(tabpage);
//         for (var i = 0; i < oItem.children.length; i++) {
//             var x = oItem.children(i);
//             x.className = "";
////             var y = x.getElementsByTagName('a');
//         }
         $("#TabPage1 li").removeClass('Selected');
         $("#TabPage1").find("#" + tabid).addClass('Selected');

//         document.getElementById(tabid).className = "Selected";
//         var dvs = document.getElementById("cnt").getElementsByTagName("div");
//         for (var i = 0; i < dvs.length; i++) {
//             if (dvs[i].id == ('d' + tabid)) dvs[i].style.display = 'block';
//             else dvs[i].style.display = 'none';
//         }
     }
        </script>

    
    </body>
</html>
