<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsStationModes.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsStationModes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" type="text/css" rel="stylesheet" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfSelectedAccessories" runat="server" />
    <asp:HiddenField ID="hfRelatedProducts" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        商品推荐
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以设置&nbsp;商品推荐类型
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:void(0);">推荐商品</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:void(0);">国产精品</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:void(0);">进口精品</a></li>
                    <li class="normal" onclick="nTabs(this,3);"><a href="javascript:void(0);">最新商品</a></li>
                    <li class="normal" onclick="nTabs(this,4);"><a href="javascript:void(0);">分类首页推荐</a></li>
                    <li class="normal" onclick="nTabs(this,5);"><a href="javascript:void(0);">设置首页楼层显示</a></li>
                    <li class="normal" onclick="nTabs(this,6);"><a href="javascript:void(0);">设置分享商品</a></li>
                    <li class="normal" onclick="nTabs(this,7);"><a href="javascript:void(0);">主题馆1</a></li>
                    <li class="normal" onclick="nTabs(this,8);"><a href="javascript:void(0);">主题馆2</a></li>
                    <li class="normal" onclick="nTabs(this,9);"><a href="javascript:void(0);">主题馆3</a></li>
                    <li class="normal" onclick="nTabs(this,10);"><a href="javascript:void(0);">主题馆4</a></li>
                    <li class="normal" onclick="nTabs(this,11);"><a href="javascript:void(0);">微商城首页活动</a></li>
                    <li class="normal" onclick="nTabs(this,12);"><a href="javascript:void(0);">手机活动</a></li>

                </ul>
            </div>
        </div>
        <div class="TabContent formitem">
            <div id="myTab1_Content0" tabindex="0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="contetRelatedProduct" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=0">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" tabindex="1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td1" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=1">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td2" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=2">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content3" tabindex="3" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td3" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=3">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content4" tabindex="4" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td4" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=4">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content5" tabindex="5" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td5" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProductsFloor.aspx?type=5">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content6" tabindex="6" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td6" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=6">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="myTab1_Content7" tabindex="7" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td7" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=7">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content8" tabindex="8" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td8" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=8">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content9" tabindex="8" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td9" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=9">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content10" tabindex="8" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td10" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=10">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content11" tabindex="8" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td11" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=11">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

              <div id="myTab1_Content12" tabindex="8" class="none4">
               <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td12" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectCommendProducts.aspx?type=12">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>



        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>