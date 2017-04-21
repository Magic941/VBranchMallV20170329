<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_New.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ShopCategories.List_New" %>
<%@ Register Src="~/Controls/copyright.ascx" TagName="copyright" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Admin/js/drop/client.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/drop/default.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/drop/print.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script src="/Admin/js/drop/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            LoadData(0);
        });

        function LoadData(CID) {
            $.ajax({
                url: "/Shopmanage.aspx",
                type: 'post',
                dataType: 'json',
                timeout: 10000,
                async: false,
                data: {
                    action: "CategoryInfo",
                    CID: CID
                },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $.each(resultData.DATA, function (i, n) {
                            var hasChildrenCss = 'sm2_liClosed';
                            var one = '<li class="{0}" pid="{3}" ci="{4}" ><dl class="sm2_s_published ui-droppable"  i="panel"><a class="sm2_expander">&nbsp;</a><dt><a class="sm2_title">{1}</a></dt><dd class="sm2_actions"><span class="sm2_move" title="排序">排序</span><span class="sm2_delete" title="删除" id="{4}">删除</span></dd><dd class="sm2_status"  style="top: 8px;"><a  style="margin-right: 300px;text-align: left;color: black;cursor: default;display: none;">{5}</a><a href="Modify.aspx?id={4}" title="编辑"  >编辑</a></dd></dl>{2}</li>'; //sm2_liOpen
                            var two = '';
                            if (!n.HasChildren) {
                                var tmp = one.format('', n.Name, '', 0, n.CategoryId, n.SeoUrl);
                                $("#sitemap").append(tmp);
                            } else {
                                $("#sitemap").append(one.format(hasChildrenCss, n.Name, CreateCTable(n.CategoryId), 0, n.CategoryId, n.SeoUrl));
                            }
                        });
                    }
                }
            });
        }
        
        function CreateCTable(CID) {
            var res = '';
            $.ajax({
                url: "/Shopmanage.aspx",
                type: 'post',
                dataType: 'json',
                timeout: 10000, async: false,
                data: {
                    action: "CategoryInfo",
                    CID: CID
                },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $.each(resultData.DATA, function (i, n) {
                            var hasChildrenCss = 'sm2_liClosed';
                            var one = '<ul><li class="{0}" pid="{3}"" ci="{4}"><dl class="sm2_s_published ui-droppable"  i="panel"><a class="sm2_expander">&nbsp;</a><dt><a class="sm2_title">{1}</a></dt><dd class="sm2_actions"> <span class="sm2_move" title="排序">排序</span><span class="sm2_delete" title="删除" id="{4}">删除</span></dd><dd class="sm2_status"  style="top: 8px;"><a  style="margin-right: 300px;text-align: left;color: black;cursor: default;display: none;">{5}</a><a href="Modify.aspx?id={4}" title="编辑" >编辑</a></dd></dl>{2}</li></ul>'; //sm2_liOpen
                            var two = '';
                            if (!n.HasChildren) {
                                res += one.format('', n.Name, '', n.ParentCategoryId, n.CategoryId, n.SeoUrl);
                            } else {
                                res += one.format(hasChildrenCss, n.Name, CreateCTable(n.CategoryId), n.ParentCategoryId, n.CategoryId, n.SeoUrl);
                            }
                        });
                    }
                }
            });
            return res;
        }
    </script>

    <script src="/Admin/js/drop/hs_draggable.js" type="text/javascript"></script>
  <%--  <link type="text/css" rel="stylesheet" href="chrome-extension://cpngackimfmofbokmjmljamhdncknpmg/style.css">
    <script type="text/javascript" charset="utf-8" src="chrome-extension://cpngackimfmofbokmjmljamhdncknpmg/page_context.js"></script>--%>
    <style type="text/css">
        .newssearch { margin:15px; height:20px}
        .newssearch ul { padding:0px; margin:0px}
        .newssearch li { float:left; height:33px; line-height:33px;padding-left: 5px;}
        .newslist { border-top-color:#d6d6d6;border-right-color:#d6d6d6;border-left-color:#d6d6d6;border-top-width:1px;border-right-width:1px;border-left-width:1px;border-top-style:solid;border-right-style:solid;border-left-style:solid;border-bottom-style:solid;border-bottom-color:#d6d6d6;border-bottom-width:1px;}
        .newsicon { width:100%; overflow:hidden;}
        .newsicon a:link { color: #333}
        .newsicon a:hover { color:#417eb7}
        .newslist ul { padding:5px; margin:0px; background:url(../../images/admintitleft.gif) repeat-x;font-size: 14px;list-style:none;}
        .newslist ul li { height: 25px; font-size:12px; float:left; width:50px; padding-left:25px; padding-top:2px}
        .newslist ul li b { color: #cdcdcd; font-size:14px; border-right:1px groove #fff}
        .newslisttitle { width:100%}
        .newslisttitle strong { font-weight:600; color: #666}
    </style>
</head>

<body >
    
    <div id="container" style="margin-left: 50px;margin-top: -50px;">
        <div id="content">
            <div id="content_left">
                <div id="content_right">
                    <div class="page">
                        <div class="page_inner">
                            
                            <div class="page_top">
                                <div class="page_left">
                                </div>
                                <div class="page_right">
                                </div>
                            </div>
                            <div class="col01">
                                <h1>产品分类管理</h1>
                                
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li   style="background: url(/images/icon8.gif) no-repeat 5px 3px; width: 50px;" id="liAdd"  runat="server"><a href="add.aspx"
                        title="添加新的网站分类">添加</a> <b>|</b> </li>
                  
                </ul>
            </div>
        </div>
                                <ul id="sitemap">
                                    
                                 

                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <uc1:copyright ID="Copyright1" runat="server" />   
</body>
</html>
