<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SelectAccessorieNew.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductAccessories.SelectAccessorieNew" %>
<%--<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            resizeImg('.borderImage', 120, 120);
        });
    </script>
    <style type="text/css">
        .borderImage{width: 121px;height: 121px;border: 1px solid #CCC;text-align: center;}
    </style>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div  style="background: white;width: 100%">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height"   style="margin-bottom: 5px">
                 <asp:Literal runat="server" ID="littitle" Text="" />
                </h3>
        </div>
        <div class="Goodsgifts">
            <div class="left">
                <h1>
                    需要添加的商品</h1>
                <asp:panel id="Panel1" runat="server" defaultbutton="btnSearch">
                    <ul>
                        <li>
                            <asp:Literal runat="server" ID="LitProductCategories" Text="分类" />：
                            <abbr class="formselect">
                                <asp:dropdownlist ID="drpProductCategory" runat="server">
                                </asp:dropdownlist>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="LitProductName" Text="商品名称" />：
                            <asp:textbox id="txtProductName" runat="server"/>
                        </li>
                        <li>
                            <asp:button id="btnSearch" OnClick="btnSearch_Click" runat="server" text="查询" cssclass="adminsubmit_short" />
                        </li>
                    </ul>
                </asp:panel>
                <div class="content">
                    <div class="youhuiproductlist searchproductslist">
                    <asp:HiddenField ID="hfCurrentAllData" runat="server"/>
                        <asp:DataList runat="server" id="dlstSearchProducts" width="96%"
                         OnItemDataBound="dlstSearch_OnItemDataBound"  OnItemCommand="dlstSearchProducts_ItemCommand"
                         datakeyfield="SkuId" repeatlayout="Table">
                            <itemtemplate>
                    <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("SKUId") %>">
                         <tr>
                            <td width="14%" rowspan="3" class="img">
                                <div class="borderImage">
                                  <img id="Img2" ref="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ProductThumbnailUrl").ToString(), "T128X130_") %>" /></div>
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='#' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">销售价：<%# Eval("SalePrice","{0:n2}")%></span></td>
                        <td width="19%" valign="top"> 库存：<%# Eval("Stock") %></td>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="15%" valign="top">
                            <asp:LinkButton ID="lbtnAdd" runat="server" Style="color: #0063dc;" CommandName="add"
                                                CommandArgument='<%#Eval("SKU")%>' >添加</asp:LinkButton>
                           
                        </td>
                      </tr>
                      <tr>
                          <td colspan="5">
                              <asp:Repeater ID="rptSKUItems" runat="server">
                                  <ItemTemplate>
                                      <div id="SKUValueStr" class="specdiv"><%# Eval("ValueStr")%></div>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </td>
                      </tr>
                   </table>
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            <asp:button runat="server" id="btnAddSearch" cssclass="adminsubmit" text="全部添加" style="display:none;" />
                        </div>
                        <div class="pagination">
                    <webdiyer:AspNetPager runat="server" ID="anpSearchProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPageranpSearchProducts_PageChanged" PageSize="3" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                    </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right">
                <h1>
                    已添加的商品</h1>
            <asp:panel id="Panel2" runat="server" defaultbutton="btnSearch">
                    <ul>
                        <li>
                            <asp:Literal runat="server" ID="Literal1" Text="&nbsp;&nbsp;" /> 
                            
                        </li>
                        
                    </ul>
                </asp:panel>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server"/>
                        <asp:DataList runat="server" id="dlstAddedProducts"
                        OnItemCommand="dlstAddedProducts_ItemCommand"
                         width="96%" datakeyfield="SkuId" repeatlayout="Table" OnItemDataBound="dlstAddedProducts_ItemDataBound" >
                            <itemtemplate>
                     <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("SKUId") %>">
                        <tr>
                            <td width="14%" rowspan="3" class="img">
                                <div class="borderImage">
                                  <img id="Img1" ref="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ProductThumbnailUrl").ToString(), "T128X130_") %>" /></div>
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='javascript:;' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">销售价：<%# Eval("SalePrice","{0:n2}")%></span></td>
                        <td width="19%" valign="top"> 库存：<%# Eval("Stock") %></td>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="15%" valign="top">
                          <span id="spanMain" runat="server" visible="false">主商品</span>
                          <asp:LinkButton ID="lbtnDel" runat="server" Style="color: #0063dc;" CommandName="delete" Visible="true"
                                                CommandArgument='<%#Eval("SKU") %>'  >  <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton>
                                                      
                                                   </td>
                      </tr>
                      <tr>
                          <td colspan="5">
                              <asp:Repeater ID="rptSKUItems" runat="server">
                                  <ItemTemplate>
                                      <div id="Div1" class="specdiv"><%# Eval("ValueStr")%></div>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </td>
                      </tr>
                     </table>
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            &nbsp;</div>
                        <div class="pagination">
                    <webdiyer:AspNetPager runat="server" ID="anpAddedProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPagerAddedProducts_PageChanged" PageSize="3" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                    </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bntto">
        </div>
    </div>
</asp:content>
 
    