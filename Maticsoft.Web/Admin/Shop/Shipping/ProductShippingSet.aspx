<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductShippingSet.aspx.cs" MasterPageFile="~/Admin/Basic.Master" Inherits="Maticsoft.Web.Admin.Shop.Shipping.ProductShippingSet" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/jquery.scrollTo-min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ProductFreightFreeSet.helper.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
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
    <input type="hidden" value="<%=CurrentUser.UserID %>" id="UserId" />
    <div style="background: white; width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
           <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="单品免邮设置" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以对单品免邮规则进行添加，删除等操作" />
                    </td>
                </tr>
            </table>
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
                        <li>
                            <asp:Literal runat="server" ID="LitProductCode" Text="商品编号" />：
                            <asp:TextBox ID="txtProductCode" runat="server" />
                        </li>
                        <li style="display: none;">
                            <asp:Literal runat="server" ID="litProductNum" Text="货号" />：
                            <asp:TextBox ID="txtProductNum" runat="server" />
                        </li>
                        <li>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="查询" CssClass="adminsubmit_short" />
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
                                                    <img width="80px" height="80px" id="ThumbnailUrl40" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T150X150_") %>" /></a>
                                            </div>
                                        </td>
                                        <td height="27" colspan="4" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC"></span></td>
                                        <%--  <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %></td>--%>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server"  id="lbtnAdd" class="submit_add">添加</span></a></td>
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
                                                <img id="Img1" width="80px" height="80px" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T150X150_") %>" />
                                            </div>
                                        </td>
                                        <td height="27" colspan="4" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC">免邮数量：<%# Eval("Quantity")%></span><br /><span class="colorC">有效日期：<%# Convert.ToDateTime( Eval("StartDate")).ToString("yyyy/MM/dd")  %>- <%# Convert.ToDateTime( Eval("EndDate")).ToString("yyyy/MM/dd")  %></span><br />
                                            <span class="colorC">设置日期：<%# Convert.ToDateTime( Eval("createdate")).ToString("yyyy/MM/dd")%></span></td>
                                        <%--            <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %></td>--%>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server" id="lbtnDel"><span class="submit_del" skuid="<%# Eval("ProductId") %>">删除</span></span></a></td>
                                    </tr>
                                    <%--<tr>
                          <td colspan="5">
                              <asp:Repeater ID="rptSKUItems" runat="server">
                                  <ItemTemplate>
                                      <div id="Div1" class="specdiv"><%# Eval("ValueStr")%></div>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </td>
                      </tr>--%>
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
                <h1 class="title_height">请选择该免邮商品的有效期和数量</h1>
            </div>
            <div class="results">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formTR">
                    <tr>
                        <td class="td_class">
                            <em>*</em>数量 ：
                        </td>
                        <td height="25">
                            <input type="text" id="txt_quantity"  style="width:100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            <em>*</em>有效期 ：
                        </td>
                        <td height="25" style="width:500px">
                            <input type="text" id="txt_start" style="width:150px; float:left" /><span style="float:left">&nbsp;&nbsp;-&nbsp;&nbsp;</span><input type="text" id="txt_end" style="width:150px;float:left" />
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
    
</asp:Content>