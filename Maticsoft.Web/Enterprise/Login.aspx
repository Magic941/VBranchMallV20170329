<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Maticsoft.Web.Enterprise.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统登录</title>
    <script language="javascript" type="text/javascript">
        ; if (parent.length) { parent.window.location = "Login.aspx"; }
        function ChangeCode() {
            var date = new Date();
            var myImg = document.getElementById("ImageCheck");
            var GUID = document.getElementById("lblGUID");
            if (GUID != null) {
                if (GUID.innerHTML != "" && GUID.innerHTML != null) {
                    myImg.src = "../ValidateCode.aspx?GUID=" + GUID.innerHTML + "&flag=" + date.getMilliseconds()

                }
            }
            return false;
        }

        var selects = document.getElementsByTagName('select');

        var isIE = (document.all && window.ActiveXObject && !window.opera) ? true : false;

        function $$(id) {
            return document.getElementById(id);
        }

        function stopBubbling(ev) {
            ev.stopPropagation();
        }

        function rSelects() {
            for (i = 0; i < selects.length; i++) {
                selects[i].style.display = 'none';
                select_tag = document.createElement('div');
                select_tag.id = 'select_' + selects[i].name;
                select_tag.className = 'select_box';
                selects[i].parentNode.insertBefore(select_tag, selects[i]);

                select_info = document.createElement('div');
                select_info.id = 'select_info_' + selects[i].name;
                select_info.className = 'tag_select';
                select_info.style.cursor = 'pointer';
                select_tag.appendChild(select_info);

                select_ul = document.createElement('ul');
                select_ul.id = 'options_' + selects[i].name;
                select_ul.className = 'tag_options';
                select_ul.style.position = 'absolute';
                select_ul.style.display = 'none';
                select_ul.style.zIndex = '999';
                select_tag.appendChild(select_ul);

                rOptions(i, selects[i].name);

                mouseSelects(selects[i].name);

                if (isIE) {
                    selects[i].onclick = new Function("clickLabels3('" + selects[i].name + "');window.event.cancelBubble = true;");
                }
                else if (!isIE) {
                    selects[i].onclick = new Function("clickLabels3('" + selects[i].name + "')");
                    selects[i].addEventListener("click", stopBubbling, false);
                }
            }
        }


        function rOptions(i, name) {
            var options = selects[i].getElementsByTagName('option');
            var options_ul = 'options_' + name;
            for (n = 0; n < selects[i].options.length; n++) {
                option_li = document.createElement('li');
                option_li.style.cursor = 'pointer';
                option_li.className = 'open';
                $$(options_ul).appendChild(option_li);

                option_text = document.createTextNode(selects[i].options[n].text);
                option_li.appendChild(option_text);

                option_selected = selects[i].options[n].selected;

                if (option_selected) {
                    option_li.className = 'open_selected';
                    option_li.id = 'selected_' + name;
                    $$('select_info_' + name).appendChild(document.createTextNode(option_li.innerHTML));
                }

                option_li.onmouseover = function () { this.className = 'open_hover'; }
                option_li.onmouseout = function () {
                    if (this.id == 'selected_' + name) {
                        this.className = 'open_selected';
                    } else {
                        this.className = 'open';
                    }
                };

                option_li.onclick = new Function("clickOptions(" + i + "," + n + ",'" + selects[i].name + "')");
            }
        }

        function mouseSelects(name) {
            var sincn = 'select_info_' + name;

            $$(sincn).onmouseover = function () { if (this.className == 'tag_select') this.className = 'tag_select_hover'; }
            $$(sincn).onmouseout = function () { if (this.className == 'tag_select_hover') this.className = 'tag_select'; }

            if (isIE) {
                $$(sincn).onclick = new Function("clickSelects('" + name + "');window.event.cancelBubble = true;");
            }
            else if (!isIE) {
                $$(sincn).onclick = new Function("clickSelects('" + name + "');");
                $$('select_info_' + name).addEventListener("click", stopBubbling, false);
            }

        }

        function clickSelects(name) {
            var sincn = 'select_info_' + name;
            var sinul = 'options_' + name;

            for (i = 0; i < selects.length; i++) {
                if (selects[i].name == name) {
                    if ($$(sincn).className == 'tag_select_hover') {
                        $$(sincn).className = 'tag_select_open';
                        $$(sinul).style.display = '';
                    }
                    else if ($$(sincn).className == 'tag_select_open') {
                        $$(sincn).className = 'tag_select_hover';
                        $$(sinul).style.display = 'none';
                    }
                }
                else {
                    $$('select_info_' + selects[i].name).className = 'tag_select';
                    $$('options_' + selects[i].name).style.display = 'none';
                }
            }

        }

        function clickOptions(i, n, name) {
            var li = $$('options_' + name).getElementsByTagName('li');

            $$('selected_' + name).className = 'open';
            $$('selected_' + name).id = '';
            li[n].id = 'selected_' + name;
            li[n].className = 'open_hover';
            $$('select_' + name).removeChild($$('select_info_' + name));

            select_info = document.createElement('div');
            select_info.id = 'select_info_' + name;
            select_info.className = 'tag_select';
            select_info.style.cursor = 'pointer';
            $$('options_' + name).parentNode.insertBefore(select_info, $$('options_' + name));

            mouseSelects(name);

            $$('select_info_' + name).appendChild(document.createTextNode(li[n].innerHTML));
            $$('options_' + name).style.display = 'none';
            $$('select_info_' + name).className = 'tag_select';
            selects[i].options[n].selected = 'selected';

        }

        window.onload = function (e) {
            bodyclick = document.getElementsByTagName('body').item(0);
            rSelects();
            bodyclick.onclick = function () {
                for (i = 0; i < selects.length; i++) {
                    $$('select_info_' + selects[i].name).className = 'tag_select';
                    $$('options_' + selects[i].name).style.display = 'none';
                }
            };
        };
    </script>
    <link href="/css/base_login.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="/Scripts/Common.js" charset="gb2312">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="login">
        <table width="100%" height="370" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="49%" height="312" align="center" valign="bottom">
                    <br />
                    <table width="375" border="0" cellspacing="0" cellpadding="0" class="bianju">
                        <tr>
                            <td colspan="2" align="center">
                                <img src="/images/logo_login.gif" width="323" height="74" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td width="240" height="25" align="right" style="padding-right: 10px; color: #666">
                                选择登陆版本
                            </td>
                            <td width="135">
                                <div class="newsadd_title">
                                    <ul>
                                        <li id="uboxstyle">
                                            <select name="select">
                                                <option value="请选择">请选择</option>
                                                <option value="国家新闻">English</option>
                                                <option value="国家新闻">中文</option>
                                            </select>
                                        </li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="51%" valign="bottom" style="padding-top: 25px">
                    <table width="90%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="21%" height="35" align="right" style="color: #666">
                                用户名：
                            </td>
                            <td width="79%">
                                <asp:TextBox ID="txtUsername" Text="" runat="server" CssClass="txt_bg" TabIndex="1">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="right" style="color: #666">
                                密&nbsp;&nbsp;码：
                            </td>
                            <td height="30">
                                <asp:TextBox ID="txtPass" TextMode="Password" runat="server" Text="" TabIndex="2" CssClass="txt_bg">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="right" style="color: #666">
                                验证码：
                            </td>
                            <td height="30">
                                <input class="txt_bg2" id="CheckCode" tabindex="3" maxlength="22" name="user" runat="server" />
                                <asp:Image ID="ImageCheck" runat="server" ImageUrl="../ValidateCode.aspx" ToolTip="验证码"></asp:Image>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 78px">
                                <asp:Label ID="lblMsg" runat="server" BackColor="Transparent" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblGUID" runat="server" Style="display: none"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="35">
                                &nbsp;
                            </td>
                            <td height="30" align="left">
                                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="/images/login.gif" CssClass="login_img" OnClick="btnLogin_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
