<%@ Page Title="<%$ Resources:SysManage,ptWebSiteConfig%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true"  CodeBehind="SEOConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Setting.SEOConfig" %>


<%@ Register Src="~/Controls/CategoriesDropList.ascx" TagName="CategoriesDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
      <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='ddNewsCate']").select2();
            $("[id$='ddCateImage']").select2();

            var index = $("#ctl00_ContentPlaceHolder1_TabIndex").val();
            var obj = $("#tab" + index)[0];
            nTabs(obj, index);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:HiddenField ID="TabIndex" runat="server" Value="0" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       文章SEO优化设置
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
                    <li class="active" onclick="nTabs(this,0);" id="tab0"><a href="javascript:;">首页</a></li>
                    <li class="normal" onclick="nTabs(this,1);" id="tab1"><a href="javascript:;">文章</a></li>
                 <%--   <li class="normal" onclick="nTabs(this,2);" id="tab2"><a href="javascript:;">CMS文章</a></li>
                      <li class="normal" onclick="nTabs(this,3);" id="tab3"><a href="javascript:;">CMS图片</a></li>--%>
                </ul>
            </div>
        </div>
        <div id="codediv" style="display: none; top: 707px; background: url('/admin/images/mdly.png') no-repeat scroll 0 0 transparent;
            height: 100px; line-height: 32px; margin-top: -16px; overflow: hidden; padding: 10px 25px;
            position: absolute; left: 605px; width: 360px;">
            <p>
                可用代码，点击插入， 更多功能即将加入。
            </p>
            <ul id="seocodes" style="width: 100%">
                <li style="float: left; margin-right: 5px;"><a onclick="insertcode('subject');return false;"
                    href="javascript:;">{subject}</a> <span class="pipe">|</span> <a onclick="insertcode('forum');return false;"
                        href="javascript:;">{forum}</a> </li>
            </ul>
        </div>
        <script type="text/javascript">
            var codediv = $('#codediv').get(0);
            var codetypes = new Array(), codenames = new Array();
            //基础
            codetypes['base'] = 'hostname';
            codenames['base_hostname'] = '站点名称';

            //文章列表
            codetypes['category'] = 'hostname,cname,cid';
            codenames['category_hostname'] = '站点名称';
            codenames['category_cname'] = '文章栏目名称';
            codenames['category_cid'] = '文章栏目编号';

            //新闻详细页
            codetypes['cms'] = 'ctname,cname,cateid,cid';
            codenames['cms_ctname'] = '栏目名称';
            codenames['cms_cname'] = '文章标题';
            codenames['cms_cateid'] = '栏目编号';
//              codenames['cms_namepath'] = '栏目路径名称';
              codenames['cms_cid'] = '文章编号';
//              codenames['cms_catedir'] = '自定义路径名称';
//              codenames['cms_ctname_p'] = '栏目拼音名称';
//              codenames['cms_cname_p'] = '文章拼音标题';
//              codenames['cms_namepath_p'] = '栏目路径拼音名称';
      
            //CMS图片
//            codetypes['cmsimage'] = 'hostname,cname';
//            codenames['cmsimage_hostname'] = '站点名称';
//            codenames['cmsimage_cname'] = '文章标题';
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
        <div class="TabContent">
            <%-- 首页Tab --%>
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeTitle" runat="server" onfocus="getcodetext(this, 'base');"
                                            Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal10" runat="server" Text="页面关键字" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeKeywords" runat="server" onfocus="getcodetext(this, 'base');"
                                            Width="400" Height="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal11" runat="server" Text="页面描述" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtHomeDes" runat="server" onfocus="getcodetext(this, 'base');"
                                            Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="TabContent">
            <%--新闻页面TAB --%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                         <div class="newsadd_title " >
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                         文章列表
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                    <tr style="display:none;">
                                            <td class="td_class">
                                                <asp:Literal ID="Literal40" runat="server" Text="新闻分类" />：
                                            </td>
                                            <td height="25">
                                                <asp:DropDownList ID="ddNewsCate" runat="server" Width="381px" OnSelectedIndexChanged="ddNewsCate_IndexChange" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal35" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfTitle" runat="server" onfocus="getcodetext(this, 'cms');" Width="400"
                                                    Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal37" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfDes" runat="server" onfocus="getcodetext(this, 'cms');" Width="400"
                                                    Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal36" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfKeywords" runat="server" onfocus="getcodetext(this, 'cms');" Width="400"
                                                    Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                               <tr style="display: none">
                                            <td class="td_class">
                                                <asp:Literal ID="Literal39" runat="server" Text="Url 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfUrl" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400"  Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                        文章详细
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSTitle" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal18" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSDes" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal17" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSKeywords" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="td_class">
                                                <asp:Literal ID="Literal38" runat="server" Text="Url 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSUrl" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400" Height="30" ></asp:TextBox>
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

        <div class="TabContent" style=" display:none">
            <%-- 基础页面Tab --%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            关于我们
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
                                                <asp:TextBox ID="txtAboutTitle" runat="server" onfocus="getcodetext(this, 'base');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal21" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtAboutDes" runat="server" onfocus="getcodetext(this, 'base');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal20" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtAboutKeywords" runat="server" onfocus="getcodetext(this, 'base');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            联系我们
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
                                                <asp:TextBox ID="txtContactTitle" onfocus="getcodetext(this, 'productdetail');" runat="server"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal3" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtContactDes" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal2" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtContactKeywords" onfocus="getcodetext(this, 'productdetail');"
                                                    runat="server" Width="400" Height="30"></asp:TextBox>
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
        
        
        <div class="TabContent" style="  display:none">
            <%--  产品图片TAB --%>
            <div id="myTab1_Content3" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px; "
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                           CMS图片
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal31" runat="server" Text="alt 文字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSImageAlt" runat="server" onfocus="getcodetext(this, 'cmsimage');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal33" runat="server" Text="链接 title" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSImageTitle" runat="server" onfocus="getcodetext(this, 'cmsimage');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            自定义优化
                                        </td>
                                    </tr>
                                </table>
                                <div class="member_info_show">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                    <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal41" runat="server" Text="产品类别" />：
                                            </td>
                                            <td height="25">
                                              <asp:DropDownList ID="ddCateImage" runat="server" Width="381px"  OnSelectedIndexChanged="ddCateImage_IndexChange"  AutoPostBack="true">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal7" runat="server" Text="alt 文字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfImageAlt" runat="server" onfocus="getcodetext(this, 'cmsimage');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal8" runat="server" Text="链接 title" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfImageTitle" runat="server" onfocus="getcodetext(this, 'cmsimage');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
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
        
        <table style="width: 100%; border-top: none; float: left; padding-top: 20px; padding-bottom: 20px"
            cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click">
                                </asp:Button>
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
