<%@ Page Title="<%$ Resources:SysManage,ptWebSiteConfig%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="SEOConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Setting.SEOConfig" %>

<%@ Register Src="~/Controls/CategoriesDropList.ascx" TagName="CategoriesDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
      <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {
//            $("[id$='ddProduct']").select2();
//            $("[id$='ddCategory']").select2();
//            $("[id$='ddNewsCate']").select2();
//            $("[id$='ddCateImage']").select2();

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
                      商城SEO优化设置
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
                    <li class="active" onclick="nTabs(this,0);" id="tab0"><a href="javascript:;">商城首页</a></li>
                    <li style="display:none;" class="normal" onclick="nTabs(this,1);" id="tab1"><a href="javascript:;">基础页面</a></li>
                    <li class="normal" onclick="nTabs(this,2);" id="tab2"><a href="javascript:;">产品类别</a></li>
                    <li class="normal" onclick="nTabs(this,3);" id="tab3"><a href="javascript:;">产品详细</a></li>
                    <li style="display:none;" class="normal" onclick="nTabs(this,4);" id="tab4"><a href="javascript:;">产品图片</a></li>
                    <li  class="normal" style="display:none;"  onclick="nTabs(this,5);" id="tab5"><a href="javascript:;">CMS管理</a></li>
                </ul>
            </div>
        </div>
        <div id="codediv" style="display: none; top: 707px; background: url('/admin/images/mdly.png') no-repeat scroll 0 0 transparent;
            height: 100px; line-height: 32px; margin-top: -16px; overflow: hidden; padding: 10px 25px;
            position: absolute; left: 605px; width: 250px;">
            <p>
                可用代码，点击插入， 更多功能即将加入。
            </p>
            <ul id="seocodes" style="width: 100%">
                <li style="float: left; margin-right: 5px;"><a onclick="insertcode('subject');return false;"
                    href="javascript:;">{subject}</a> <span class="pipe">|</span> <a onclick="insertcode('forum');return false;"
                        href="javascript:;">{forum}</a> </li>
            </ul>
        </div>
        <script language="javascript">
            var codediv = $('#codediv').get(0);
            var codetypes = new Array(), codenames = new Array();
            //基础
            codetypes['base'] = 'hostname';
            codenames['base_hostname'] = '站点名称';

            //产品类别
            codetypes['category'] = 'hostname,cname,cid';
            codenames['category_hostname'] = '站点名称';
            codenames['category_cname'] = '产品类别名称';
            codenames['category_cid'] = '产品类别Id';
            //codenames['product_cdes'] = '商品分类描述';

            //产品详细
            codetypes['productdetail'] = 'hostname,cname,cid,catelistname,brands';
            codenames['productdetail_hostname'] = '站点名称';
            codenames['productdetail_cname'] = '产品名称';
            codenames['productdetail_cid'] = '产品ID';
            codenames['productdetail_catelistname'] = '分类名称集合';  
            codenames['productdetail_brands'] = '产品品牌';
            
            //新闻详细页
//            codetypes['cms'] = 'hostname,cname,cid';
//            codenames['cms_hostname'] = '站点名称';
//            codenames['cms_cname'] = '文章标题';
//           // codenames['cms_catepath'] = '文章栏目路径名称';
//            codenames['cms_cid'] = '文章ID';
      
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
        <div class="TabContent"   >
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
        <div class="TabContent" style="display:none;">
            <%-- 基础页面Tab --%>
            <div id="myTab1_Content1" class="none4">
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
                                                <asp:TextBox ID="txtContactTitle" onfocus="getcodetext(this, 'base');" runat="server"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal3" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtContactDes" runat="server" onfocus="getcodetext(this, 'base');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal2" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtContactKeywords" onfocus="getcodetext(this, 'base');"
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
        <div class="TabContent">
            <%--  产品类别Tab --%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            产品类别
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
                                                <asp:TextBox ID="txtCategoryTitle" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal26" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategoryDes" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal27" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategoryKeywords" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal12" runat="server" Text="URL 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategoryUrl" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title" style="display:none;">
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
                                                <asp:Literal ID="Literal34" runat="server" Text="产品类别" />：
                                            </td>
                                            <td height="25">
                                            <asp:DropDownList ID="ddCategory" runat="server" Width="381px" OnSelectedIndexChanged="ddCategory_IndexChange" AutoPostBack="true">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategorySelfTitle" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal23" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategorySelfDes" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal24" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategorySelfKeywords" runat="server" onfocus="getcodetext(this, 'category');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal13" runat="server" Text="URL 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCategorySelfUrl" runat="server" onfocus="getcodetext(this, 'category');"
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
        <div class="TabContent">
            <%--     产品详细 Tab--%>
            <div id="myTab1_Content3" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            产品详细
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
                                                <asp:TextBox ID="txtProductTitle" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal6" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductDes" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal5" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductKeywords" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal14" runat="server" Text="URL 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductUrl" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title" style="display:none;">
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
                                                <asp:Literal ID="Literal32" runat="server" Text="产品类别" />：
                                            </td>
                                            <td height="25">
                                              <asp:DropDownList ID="ddProduct" runat="server" Width="381px"  OnSelectedIndexChanged="ddProduct_IndexChange"  AutoPostBack="true">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:SysManage,lblPageTitle%>" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductSelfTitle" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal30" runat="server" Text="页面描述" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductSelfDes" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal29" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductSelfKeywords" runat="server" onfocus="getcodetext(this, 'productdetail');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal15" runat="server" Text="URL 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductSelfUrl" runat="server" onfocus="getcodetext(this, 'productdetail');"
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
        <div class="TabContent" style="display:none;">
            <%--  产品图片TAB --%>
            <div id="myTab1_Content4" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                            产品图片
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
                                                <asp:TextBox ID="txtProductImageAlt" runat="server" onfocus="getcodetext(this, 'productimage');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal33" runat="server" Text="链接 title" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductImageTitle" runat="server" onfocus="getcodetext(this, 'productimage');"
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
                                                <asp:TextBox ID="txtProductSelfImageAlt" runat="server" onfocus="getcodetext(this, 'productimage');"
                                                    Width="400" Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal8" runat="server" Text="链接 title" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtProductSelfImageTitle" runat="server" onfocus="getcodetext(this, 'productimage');"
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
        <div class="TabContent" >
            <%--新闻页面TAB --%>
            <div id="myTab1_Content5" class="none4" >
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <div class="newsadd_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                    <tr>
                                        <td bgcolor="#FFFFFF" class="newstitle">
                                          CMS管理
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
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal38" runat="server" Text="Url 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSUrl" runat="server" onfocus="getcodetext(this, 'cms');"
                                                    Width="400" Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="newsadd_title"  style="display:none;">
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
                                                <asp:TextBox ID="txtCMSSelfDec" runat="server" onfocus="getcodetext(this, 'cms');" Width="400"
                                                    Height="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal36" runat="server" Text="页面关键字" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfKeyword" runat="server" onfocus="getcodetext(this, 'cms');" Width="400"
                                                    Height="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal39" runat="server" Text="Url 地址" />：
                                            </td>
                                            <td height="25">
                                                <asp:TextBox ID="txtCMSSelfUrl" runat="server" onfocus="getcodetext(this, 'cms');"
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
