<%@ Page Title="<%$ Resources:SysManage,ptWebSiteConfig%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="SEOConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Setting.SEOConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       社区SEO优化设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        设置网站页面的SEO信息，让搜索引擎快速收录，提升网站流量。
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">社区首页</a></li>
                     <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">博客</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:;">商品</a></li>
                    <li class="normal" onclick="nTabs(this,3);"><a href="javascript:;">图片</a></li>
                    <li class="normal" onclick="nTabs(this,4);"><a href="javascript:;">小组</a></li>
                    <li class="normal" onclick="nTabs(this,5);"><a href="javascript:;">专辑</a></li>
                    <li class="normal" onclick="nTabs(this,6);"><a href="javascript:;">达人</a></li>
                       <li class="normal" onclick="nTabs(this,7);"><a href="javascript:;">视频</a></li>
                </ul>
            </div>
        </div>
        <div id="codediv" style="display: none; top: 707px; background: url('/admin/images/mdly.png') no-repeat scroll 0 0 transparent; height: 100px; line-height: 32px; margin-top: -16px; overflow: hidden; padding: 10px 25px; position: absolute; left: 605px; width: 250px;">
            <p>
                可用代码，点击插入， 更多功能即将加入。
            </p>
            <ul id="seocodes" style="width: 100%">
                <li style="float: left; margin-right: 5px;">
                <a onclick="insertcode('subject');return false;" href="javascript:;">{subject}</a> <span class="pipe">|</span> <a onclick="insertcode('forum');return false;" href="javascript:;">{forum}</a>
            </li>
            </ul>
        </div>
        <script type="text/javascript">
            var codediv = $('#codediv').get(0);
            var codetypes = new Array(), codenames = new Array();
            //基础
            codetypes['base'] = 'hostname';
            codenames['base_hostname'] = '站点名称';

            //博客详细
            codetypes['blog'] = 'hostname,cname,cid';
            codenames['blog_hostname'] = '站点名称';
            codenames['blog_cid'] = '博文编号';
            codenames['blog_cname'] = '博文标题';

            //视频详细
            codetypes['video'] = 'hostname,cname';
            codenames['video_hostname'] = '站点名称';
            codenames['video_cname'] = '视频标题';
           

            //商品列表
            codetypes['product'] = 'hostname,cname';
            codenames['product_hostname'] = '站点名称';
            codenames['product_cname'] = '商品分类名称';
            //codenames['product_cdes'] = '商品分类描述';

            //商品详细
            codetypes['productdetail'] = 'hostname,cname,ctag';
            codenames['productdetail_hostname'] = '站点名称';
            codenames['productdetail_cname'] = '商品名称';
            codenames['productdetail_ctag'] = '商品标签';

            //图片
            codetypes['photodetail'] = 'hostname,cname';
            codenames['photodetail_hostname'] = '站点名称';
            codenames['photodetail_cname'] = '图片分享内容';

            //小组列表
            codetypes['grouplist'] = 'hostname,cname,ctag,cdes';
            codenames['grouplist_hostname'] = '站点名称';
            codenames['grouplist_cname'] = '小组名称';
            codenames['grouplist_ctag'] = '小组标签';
            codenames['grouplist_cdes'] = '小组描述';

            //小组主题
            codetypes['groupdetail'] = 'hostname,cname,ctname,ctag,cdes';
            codenames['groupdetail_hostname'] = '站点名称';
            codenames['groupdetail_cname'] = '小组名称';
            codenames['groupdetail_ctname'] = '帖子标题';
            codenames['groupdetail_ctag'] = '帖子标签';
            codenames['groupdetail_cdes'] = '帖子内容';

            //专辑列表
            codetypes['ablumlist'] = 'hostname,cname';
            codenames['ablumlist_hostname'] = '站点名称';
            codenames['ablumlist_cname'] = '专辑分类名称';
            //codenames['ablumlist_cdes'] = '专辑分类描述';

            //专辑详细
            codetypes['ablumdetail'] = 'hostname,ctname,cname,cdes';
            codenames['ablumdetail_hostname'] = '站点名称';
            codenames['ablumdetail_ctname'] = '创建者昵称';
            codenames['ablumdetail_cname'] = '专辑名称';
            codenames['ablumdetail_cdes'] = '专辑描述';
            
            $(function () {
                //$('.TabContent').unbind('mouseover').bind('mouseover', function () { codediv.style.display = 'none'; });
            });
            function getcodetext(obj, ctype) {
                var top_offset = obj.offsetTop;
                var codecontent = '';
                var targetid = obj.id;
                while ((obj = obj.offsetParent).tagName != 'BODY') {
                    top_offset += obj.offsetTop;
                }
                if (!codetypes[ctype]) {
                    return true;
                }
                types = codetypes[ctype].split(',');
                for (var i = 0; i < types.length; i++) {
                    if (codecontent != '') {
                        codecontent += '&nbsp;&nbsp;';
                    }
                    codecontent += '<li style="float: left;margin-right: 5px;"><a onclick="insertContent(\'' + targetid + '\', \'{' + types[i] + '}\');return false;" href="javascript:;" title="' + codenames[ctype + '_' + types[i]] + '">{' + types[i] + '}</a></li>';
                }
                $('#seocodes').get(0).innerHTML = codecontent;
                codediv.style.top = top_offset + 'px';
                codediv.style.display = '';
                _attachEvent($('#myTab1').get(0), 'mouseover', function () { codediv.style.display = 'none'; });
            }
            function _attachEvent(obj, evt, func, eventobj) {
                eventobj = !eventobj ? obj : eventobj;
                if (obj.addEventListener) {
                    obj.addEventListener(evt, func, false);
                } else if (eventobj.attachEvent) {
                    obj.attachEvent('on' + evt, func);
                }
            } function isUndefined(variable) {
                return typeof variable == 'undefined' ? true : false;
            }
            function insertContent(target, text) {
                var obj = $("#" + target).get(0);
                selection = document.selection;
                checkFocus(target);
                if (!isUndefined(obj.selectionStart)) {
                    var opn = obj.selectionStart + 0;
                    obj.value = obj.value.substr(0, obj.selectionStart) + text + obj.value.substr(obj.selectionEnd);
                } else if (selection && selection.createRange) {
                    var sel = selection.createRange();
                    sel.text = text;
                    sel.moveStart('character', -strlen(text));
                } else {
                    obj.value += text;
                }
            }
            function checkFocus(target) {
                var obj = $("#" + target).get(0);
                if (!obj.hasfocus) {
                    obj.focus();
                }
            }
        </script>
        <div class="TabContent" >
            <%-- 首页Tab --%>
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                           <span style="color: gray;padding-left: 18px;display:none;"  >如不设置将会使用<a href="/Admin/SysManage/WebSiteConfig.aspx" style="color: blue">网站设置</a>的信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal10" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            <span style="color: gray;padding-left: 18px;display:none;">如不设置将会使用<a href="/Admin/SysManage/WebSiteConfig.aspx" style="color: blue">网站设置</a>的信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal11" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                             <span style="color: gray;padding-left: 18px;display:none;">如不设置将会使用<a href="/Admin/SysManage/WebSiteConfig.aspx" style="color: blue">网站设置</a>的信息</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
         <div class="TabContent">
            <%-- 博客Tab --%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                           博客列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogListTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal23" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogListKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal217" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogListDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div> 
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                           博客详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal40" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogDetailTitle" onfocus="getcodetext(this, 'blog');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal41" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogDetailKeywords" onfocus="getcodetext(this, 'blog');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal42" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtBlogDetailDes" runat="server" onfocus="getcodetext(this, 'blog');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 商品Tab --%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            商品列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductListTitle" runat="server" onfocus="getcodetext(this, 'product');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal20" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductListKeywords" runat="server" onfocus="getcodetext(this, 'product');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal21" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductListDes" runat="server" onfocus="getcodetext(this, 'product');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            商品详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductDetailTitle" onfocus="getcodetext(this, 'productdetail');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal2" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductDetailKeywords" onfocus="getcodetext(this, 'productdetail');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal3" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductDetailDes" runat="server" onfocus="getcodetext(this, 'productdetail');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 图片Tab --%>
            <div id="myTab1_Content3" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            图片列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoListTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal26" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoListDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal27" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoListKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            图片详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoDetailTitle" runat="server" onfocus="getcodetext(this, 'photodetail');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal5" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoDetailKeywords" runat="server" onfocus="getcodetext(this, 'photodetail');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal6" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtPhotoDetailDes" runat="server" onfocus="getcodetext(this, 'photodetail');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%--小组 Tab--%>
            <div id="myTab1_Content4" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                             <div class="newsadd_title">
                             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            小组频道首页
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal8" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal12" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                            <div class="newsadd_title">
                             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            小组首页(帖子列表)
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupListTitle" runat="server" onfocus="getcodetext(this, 'grouplist');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal29" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupListKeywords" runat="server" onfocus="getcodetext(this, 'grouplist');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal30" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupListDes" runat="server" onfocus="getcodetext(this, 'grouplist');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                            <div class="newsadd_title">
                             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            小组帖子详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupDetailTitle" runat="server" onfocus="getcodetext(this, 'groupdetail');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal32" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupDetailKeywords" runat="server" onfocus="getcodetext(this, 'groupdetail');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal33" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtGroupDetailDes" runat="server" onfocus="getcodetext(this, 'groupdetail');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 专辑TAB --%>
            <div id="myTab1_Content5" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            专辑频道首页
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal14" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal15" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                            <div class="newsadd_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            专辑列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal34" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumListTitle" runat="server" onfocus="getcodetext(this, 'ablumlist');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal35" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumListKeywords" runat="server" onfocus="getcodetext(this, 'ablumlist');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal36" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumListDes" runat="server" onfocus="getcodetext(this, 'ablumlist');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                            <div class="newsadd_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            专辑频道详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal37" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumDetailTitle" runat="server" onfocus="getcodetext(this, 'ablumdetail');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal38" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumDetailKeywords" runat="server" onfocus="getcodetext(this, 'ablumdetail');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal39" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAblumDetailDes" runat="server" onfocus="getcodetext(this, 'ablumdetail');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 达人TAB --%>
            <div id="myTab1_Content6" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStarTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal17" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStarKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal18" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStarDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <%-- 视频TAB --%>
            <div id="myTab1_Content7" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none;padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                           视频列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoListTitle" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal43" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoListKeywords" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal44" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoListDes" runat="server" onfocus="getcodetext(this, 'base');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div> 
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                           视频详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal45" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoDetailTitle" onfocus="getcodetext(this, 'video');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal46" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoDetailKeywords" onfocus="getcodetext(this, 'video');" runat="server" Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal47" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtVideoDetailDes" runat="server" onfocus="getcodetext(this, 'video');" Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <table style="width: 100%; border-top: none; float: left;padding-top: 20px;padding-bottom: 20px" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
