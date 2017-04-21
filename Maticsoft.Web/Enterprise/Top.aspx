<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="Maticsoft.Web.Enterprise.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="/admin/css/admin.css" rel="stylesheet" type="text/css">
    <script src="/Admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
//        parent.showFrameDialog($("#maticsoft"), "http://www.maticsoft.com/");
    </script>
</head>
<body>
    <div class="top">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="193" valign="top">
                    <img src="/images/logo.gif" />
                </td>
                <td>
                    <div class="nav">
                        <ul class="TabBarLevel1" id="TabPage1">
                            <%=strMenu %>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
        <div class="righttop">
            <ul>
                <li class="righttophelp">使用帮助</li>
                <li class="righttopabout">关于</li>
            </ul>
        </div>
    </div>
    <div class="adminmenu">
        欢迎您，<%=username%>
        <%--<em>|</em> <a href="javascript:;">账号管理</a>--%>
        <em>|</em> <a href="Recharge.aspx" target="mainFrame">充值</a>
        <%--<em>|</em> <a href="javascript:;">编辑</a> <em>|</em> 

<a href="javascript:;" class="hover_url"><font color="#3186c8" style="text-decoration:underline;">
    <asp:Literal ID="litMesNum" runat="server"></asp:Literal></font></a> <span class="hover_url1">条信息</span>--%></div>
    <div class="logionout">
        <div class="adminleft">
            <ul>
                <li><a href="/Enterprise/Left/left.aspx" target="leftFrame">个人资料</a></li>
                <li class="padd1"><a href="Logout.aspx">退出</a></li>
            </ul>
        </div>
        <div class="adminright">
            <a href="/Enterprise/Main.aspx" target="mainFrame" class="sel">返回首页</a></div>
    </div>
    <script language="JavaScript" type="text/javascript">
        //Switch Tab Effect
        function switchTab(tabpage, tabid) {
            //                var oItem = document.getElementById(tabpage);
            //                for (var i = 0; i < oItem.children.length; i++) {
            //                    var x = oItem.children(i);
            //                    x.className = "";
            //                    var y = x.getElementsByTagName('a');
            //                   
            //                }
            //                document.getElementById(tabid).className = "Selected";
            //                var dvs = document.getElementById("cnt").getElementsByTagName("div");
            //                for (var i = 0; i < dvs.length; i++) {
            //                    if (dvs[i].id == ('d' + tabid)) dvs[i].style.display = 'block';
            //                    else dvs[i].style.display = 'none';
            //                }
            $("#TabPage1 li").removeClass('Selected');
            $("#TabPage1").find("#" + tabid).addClass('Selected');
        }
    </script>
</body>
</html>