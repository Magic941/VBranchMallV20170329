<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="Maticsoft.Web.Admin.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: url(images/left_main.gif)">
    <div class="left_main_top">
    </div>
    <div class="left_main">
        <div id="foldmenu2" class="foldmenu" style="float: right;">
            
            
            <%=strMenuTree%>  
            

        </div>
        <script type="text/javascript">
            window.onload = function () {
                myMenu2 = new FOLDMenu("foldmenu2");
                myMenu2.init();
            };
        </script>
        <script type="text/javascript">
            function FOLDMenu(id, onlyone) {
                if (!document.getElementById || !document.getElementsByTagName) { return false; }
                this.menu = document.getElementById(id);
                this.submenu = this.menu.getElementsByTagName("ul");
                this.speed = 3;
                this.time = 10;
                this.onlyone = onlyone == true ? onlyone : false;
                this.links = this.menu.getElementsByTagName("a");
            }
            FOLDMenu.prototype.init = function () {
                var mainInstance = this;
                for (var i = 0; i < this.submenu.length; i++) {
                    this.submenu[i].getElementsByTagName("span")[0].onclick = function () {
                        mainInstance.toogleMenu(this.parentNode);
                    };
                }
                for (var i = 0; i < this.links.length; i++) {
                    this.links[i].onclick = function () {
                        this.className = "current";
                        mainInstance.removeCurrent(this);
                    }
                }
            }
            FOLDMenu.prototype.removeCurrent = function (link) {
                for (var i = 0; i < this.links.length; i++) {
                    if (this.links[i] != link) {
                        this.links[i].className = " ";
                    }
                }
            }
            FOLDMenu.prototype.toogleMenu = function (submenu) {
                if (submenu.className == "open") {
                    this.closeMenu(submenu);
                } else {
                    this.openMenu(submenu);
                }
            }
            FOLDMenu.prototype.openMenu = function (submenu) {
                var fullHeight = submenu.getElementsByTagName("span")[0].offsetHeight;
                var links = submenu.getElementsByTagName("a");
                for (var i = 0; i < links.length; i++) {
                    fullHeight += links[i].offsetHeight;
                }
                var moveBy = Math.round(this.speed * links.length);
                var mainInstance = this;
                var intId = setInterval(function () {
                    var curHeight = submenu.offsetHeight;
                    var newHeight = curHeight + moveBy;
                    if (newHeight < fullHeight) {
                        submenu.style.height = newHeight + "px";
                    } else {
                        clearInterval(intId);
                        submenu.style.height = "";
                        submenu.className = "open";
                    }
                }, this.time);
                this.collapseOthers(submenu);
            }
            FOLDMenu.prototype.closeMenu = function (submenu) {
                var minHeight = submenu.getElementsByTagName("span")[0].offsetHeight;
                var moveBy = Math.round(this.speed * submenu.getElementsByTagName("a").length);
                var mainInstance = this;
                var intId = setInterval(function () {
                    var curHeight = submenu.offsetHeight;
                    var newHeight = curHeight - moveBy;
                    if (newHeight > minHeight) {
                        submenu.style.height = newHeight + "px";
                    } else {
                        clearInterval(intId);
                        submenu.style.height = "";
                        submenu.className = "";
                    }
                }, this.time);
            }
            FOLDMenu.prototype.collapseOthers = function (submenu) {
                if (this.onlyone) {
                    for (var i = 0; i < this.submenu.length; i++) {
                        if (this.submenu[i] != submenu) {
                            this.closeMenu(this.submenu[i]);
                        }
                    }
                }
            }
        </script>
    </div>
    <div>
    </div>
</body>
</html>
