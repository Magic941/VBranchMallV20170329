<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top2.aspx.cs" Inherits="Maticsoft.Web.Admin.Top2" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>健康商城</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
<%--    <script type="text/javascript">
        var hasNewInfo = 0;
        var loadComment = function () {
            $.ajax({
                url: "CMSContent.aspx",
                type: "post", dataType: 'text', timeout: 10000, async: false,
                data: { action: "MessageInfo", cid: $("[id$='hfCurrentID']").val() },
                success: function (resultData) {
                    hasNewInfo = resultData;
                }
            });
        };
        $(function () {
            loadComment(); //先启动一次
            setInterval(loadComment, 60 * 1000);
        });

        setInterval(function () {
            if (hasNewInfo) {
                $("[id$='lblTotal']").text(hasNewInfo);
            }
            else {
                $("[id$='lblTotal']").text('0');
            }
        }, 500);
    </script> --%>
</head>
<body>
<form runat="server">
    <div class="top">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="193" valign="top">
                    <img src="images/logo.gif" alt="" />
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
                <li class="righttophelp"><a href="http://www.weiweiju.com/fb" target="mainFrame">
                    意见反馈</a></li>
                <li class="righttopabout"><a id="maticsoft" href="http://shop.maticsoft.com/" target="_blank">
                    关于</a></li>
            </ul>
        </div>
    </div>
    <div class="adminmenu">
        欢迎您，<%=username%>
        <em>|</em> <a href="Accounts/userinfo.aspx" target="mainFrame">基本信息</a> <em>|</em>
        <a href="Accounts/userpass.aspx" target="mainFrame">修改密码</a> <em>|</em> <a href="sysmanage/treefavorite.aspx?TreeType=0"
            target="mainFrame">定制菜单</a> 
            <em>|</em> <a href="/" target="_blank">网站前台</a>

    </div>
    <div class="logionout">
        <div class="adminleft">
            <ul>
                <li><a href="QuickLinks.aspx" target="leftFrame">快捷菜单</a></li>
                <li class="padd1"><a href="logout.aspx">退出</a></li>
            </ul>
        </div>
        <div class="adminright">
            <a href="main_index.aspx" target="mainFrame" class="daohanglink" style="line-height:15px;">首页</a></div>
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
    </script></form>
</body>
</html>
