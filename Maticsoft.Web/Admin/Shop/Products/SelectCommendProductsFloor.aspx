<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SelectCommendProductsFloor.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SelectCommendProductsFloor" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ProductStationFloorMode.helper.js" type="text/javascript"></script>
    <script src="/admin/js/mask.js"></script>
    <style>
        .mask {
            margin: 0;
            padding: 0;
            border: solid 5px red;
            width: 100%;
            height: 100%;
            background: #333;
            opacity: 0.6;
            filter: alpha(opacity=60);
            z-index: 100;
            position: fixed;
            top: 0;
            left: 0;
            display: block;
        }

        .borderImage {
            width: 81px;
            height: 81px;
            border: 1px solid #CCC;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            resizeImg('.borderImage', 80, 80);
        });
        function p_del() { var msg = "您真的确定要删除吗？/n/n请确认！"; if (confirm(msg) == true) { return true; } else { return false; } }
    </script>
    <style type="text/css">
        .borderImage {
            width: 81px;
            height: 81px;
            border: 1px solid #CCC;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background: white; width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px"></h3>
        </div>
        <div class="Goodsgifts">
            <div class="left">
                <h1>
                    <asp:Literal ID="litDesc" runat="server"></asp:Literal></h1>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                    <ul>
                        <li>
                            <asp:Literal runat="server" ID="LitProductCategories" Text="分类" />：
                            <abbr class="formselect">
                                <asp:DropDownList ID="drpProductCategory" runat="server">
                                </asp:DropDownList>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="Literal1" Text="商家" />：
                            <abbr class="formselect">
                                <asp:DropDownList ID="ddlSupplier" runat="server">
                                </asp:DropDownList>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="LitProductName" Text="商品名称" />：
                            <asp:TextBox ID="txtProductName" runat="server" />
                        </li>
                        <li style="display: none;">
                            <asp:Literal runat="server" ID="litProductNum" Text="货号" />：
                            <asp:TextBox ID="txtProductNum" runat="server" />
                        </li>
                        <li>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="查询" CssClass="adminsubmit_short" />
                        </li>
                    </ul>
                     <ul>
                        <li>
                           <asp:Literal runat="server" ID="Literal13" Text="商品分类" />:
                           <abbr class="formselect">
                                <asp:DropDownList CssClass="select2" name="ddlGoodType"  ID="ddlGoodType" runat="server">
                                </asp:DropDownList>
                            </abbr>
                        </li>
                    </ul>
                </asp:Panel>
                <div class="content">
                    <div class="youhuiproductlist searchproductslist">
                        <asp:HiddenField ID="hfCurrentAllData" runat="server" />
                        <asp:DataList runat="server" ID="dlstSearchProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table" OnItemDataBound="dlstSearchProducts_ItemDataBound">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                                    <tr>
                                        <td width="14%" rowspan="3" class="img">
                                            <div class="borderImage">
                                                <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">
                                                    <img width="80px" height="80px" id="ThumbnailUrl40" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T158X158_") %>" /></a>
                                            </div>
                                        </td>
                                        <td height="27" colspan="5" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                                        <%--  <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %></td>--%>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server" id="lbtnAdd" class="submit_add">添加</span></a></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div style="display: none;">
                            <asp:Button runat="server" ID="btnAddSearch" CssClass="adminsubmit" Text="全部添加" />
                        </div>
                        <div class="pagination">
                            <webdiyer:AspNetPager runat="server" ID="anpSearchProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                                OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                                LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                                PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <h1>已添加的商品</h1>
                <ul>
                    <li id="liDelAll" runat="server">
                        <asp:Button runat="server" ID="btnClear"  OnClientClick="return p_del();" CssClass="adminsubmit" Text="清空列表"
                            OnClick="btnClear_Click" />
                    </li>
                    <li>
                        <asp:Literal runat="server" ID="Floor" Text="楼层:" />：
                            <abbr class="formselect">
                                <asp:DropDownList ID="FloorList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FloorList_SelectedIndexChanged">
                                    <asp:ListItem Value="0">选择楼层查看</asp:ListItem>
                                    <asp:ListItem Value="1">Floor1</asp:ListItem>
                                     <asp:ListItem Value="12">Floor1热销</asp:ListItem>
                                    <asp:ListItem Value="2">Floor2</asp:ListItem>
                                    <asp:ListItem Value="21">Floor2热销</asp:ListItem>
                                    <asp:ListItem Value="3">Floor3</asp:ListItem>
                                    <asp:ListItem Value="31">Floor3热销</asp:ListItem>
                                    <asp:ListItem Value="4">Floor4</asp:ListItem>
                                     <asp:ListItem Value="41">Floor4热销</asp:ListItem>
                                    <asp:ListItem Value="5">Floor5</asp:ListItem>
                                    <asp:ListItem Value="51">Floor5热销</asp:ListItem>
                                    <asp:ListItem Value="60">新品Tab1</asp:ListItem>
                                   
                                    <asp:ListItem Value="22">首页滚动精品热卖</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </abbr>
                    </li>
                </ul>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server" />
                        <asp:DataList runat="server" ID="dlstAddedProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table" OnItemDataBound="dlstAddedProducts_ItemDataBound">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                                    <tr>
                                        <td width="14%" rowspan="3" class="img">

                                            <div class="borderImage">
                                                <img id="Img1" width="80px" height="80px" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T158X158_") %>" />
                                            </div>
                                        </td>
                                        <td height="27" colspan="5" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                                        <%--            <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %></td>--%>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server" id="lbtnDel"><span class="submit_del" skuid="<%# Eval("ProductId") %>">删除</span></span></a></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            &nbsp;
                        </div>
                        <div class="pagination">
                            <webdiyer:AspNetPager runat="server" ID="anpAddedProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                                OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                                LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                                PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMode" class="dataarea mainwidth td_top_ccc" style="background: white; width:460px; position:absolute;top:200px;left:400px;z-index:200; display:none">
            <div class="toptitle">
                <h1 class="title_height">请选择该推荐商品显示的楼层和顺序</h1>
            </div>
            <div class="results">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formTR">
                    <tr>
                        <td class="td_class">
                            <em>*</em>楼层 ：
                        </td>
                        <td height="25">
                            <select id="floor" style="width:372px">
                                <option value="1">Floor1</option>
                                <option value="11">Floor1热销</option>
                                <option value="2">Floor2</option>
                                <option value="21">Floor2热销</option>
                                <option value="3">Floor3</option>
                                <option value="31">Floor3热销</option>
                                <option value="4">Floor4</option>
                                <option value="41">Floor4热销</option>
                                <option value="5">Floor5</option>
                                <option value="51">Floor5热销</option>
                                <option value="60">新品Tab1</option>
                                <option value="22">首页滚动精品热卖</option>                             
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <em>*</em>排序 ：
                        </td>
                        <td height="25">
                            <input id="Sort" style="width:372px" type="text" />
                           <%-- <select id="Sort" style="width:372px">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                            </select>--%>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="bntto">
            <input type="button" id="btnOK" value="确定" class="adminsubmit_short" />
                <input type="button" id="btnCancel" value="取消" class="adminsubmit_short" />
        </div>
        </div>
        <input type="hidden" id="type" value="<%=SelectType %>"
    </div>
     <script>
         function Btn_ChageSequence(ProductId, value, StationId) {
             $.ajax({
                 url: "/ShopManage.aspx",
                 type: 'POST', dataType: 'json', timeout: 10000,
                 data: { Action: "UpdatestationModeSequence", Callback: "true", ProductId: ProductId, Sequence: value, StationId: StationId },
                 async: false,
                 success: function (resultData) {

                     if (resultData.DATA == "Approve") {
                         alert("修改成功!");
                     }
                     else {
                         alert("修改失败!");
                         return false;
                     }
                 }
             });
         }

</script>
</asp:Content>