<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SelectRelatedProducts.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SelectRelatedProducts" %>
<%--<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            resizeImg('.borderImage', 120, 120);
            var hfSelectRelatedData = $('[id$=HiddenField_SelectRelatedData]');
            var SelectRelatedDataArray = hfSelectRelatedData.val().split(',');
            for (var i = 0; i <= SelectRelatedDataArray.length - 1; i++) {
                var relatedData = SelectRelatedDataArray[i].split('_');
                if (relatedData[1] == 0) {
                    $("#singleRela_" + relatedData[0]).attr('checked', 'checked');
                } else {
                    $("#doubleRela_" + relatedData[0]).attr('checked', 'checked');
                }
            }
        });
    </script>
    <style type="text/css">
        .borderImage{width: 121px;height: 121px;border: 1px solid #CCC;text-align: center;}
    </style>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div  style="background: white;width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px">
                选择相关商品</h3>
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
                        <li style="display: none;">
                            <asp:Literal runat="server" ID="litProductNum" Text="货号" />：
                            <asp:textbox id="txtProductNum" runat="server"/>
                        </li>
                        <li>
                            <asp:button id="btnSearch" OnClick="btnSearch_Click" runat="server" text="查询" cssclass="adminsubmit_short" />
                        </li>
                    </ul>

                </asp:panel>
                <div class="content">
                    <div class="youhuiproductlist searchproductslist">
                    <asp:HiddenField ID="hfCurrentAllData" runat="server"/>
                        <asp:DataList runat="server" ID="dlstSearchProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table">
                            <ItemTemplate>
                    <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>" id="<%# Eval("ProductId") %>">
                         <tr>
                            <td width="14%" rowspan="3" class="img">
                                <div class="borderImage">
                                  <img id="ThumbnailUrl40" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T150X150_") %>"  width="120px" height="120px"/></div>
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='#' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                        <%--<td width="19%" valign="top"> 库存：0<%# Eval("Stock") %></td>--%>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="12%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="17%" valign="top" class="tdmanage"><a href="javascript:void(0);"><span class="submit_add">添加</span></a></td>
                      </tr>
                   </table>
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            <asp:button runat="server" id="btnAddSearch" cssclass="adminsubmit" text="全部添加"  style="display:none;"/>
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
                <h1>
                    已添加的商品</h1>
                <ul>
                    <li>
                        <asp:button runat="server" id="btnClear" cssclass="adminsubmit" text="清空列表" 
                            onclick="btnClear_Click"  /> 
                    </li>
                </ul>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server"/>
                        <asp:HiddenField ID="HiddenField_SelectRelatedData" runat="server"/>
                        <asp:DataList runat="server" id="dlstAddedProducts"  width="96%" datakeyfield="ProductId" repeatlayout="Table">
                            <itemtemplate>
                     <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>" id="<%# Eval("ProductId") %>">
                        <tr>
                            <td width="14%" rowspan="3" class="img">
                                <div class="borderImage">
                                  <img id="Img1" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T150X150_") %>"  width="120px" height="120px"/></div>
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='#' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                        <%--<td width="19%" valign="top"> 库存：0<%# Eval("Stock") %></td>--%>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="12%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="17%" valign="top">
                            <div class="relatedInfo"><input type="radio" name="relationproducts<%# Eval("ProductId") %>" value="0" id="singleRela_<%# Eval("ProductId") %>" checked="checked"  i="<%# Eval("ProductId") %>" onclick="relatedChanged(this);"/><label for="singleRela_<%# Eval("ProductId") %>"  i="<%# Eval("ProductId") %>"  value="0" onclick="relatedChanged(this);">单向关联</label><input type="radio" name="relationproducts<%# Eval("ProductId") %>" value="1" id="doubleRela_<%# Eval("ProductId") %>"  i="<%# Eval("ProductId") %>"  onclick="relatedChanged(this);"/><label for="doubleRela_<%# Eval("ProductId") %>" i="<%# Eval("ProductId") %>"  value="1" onclick="relatedChanged(this);">双向关联</label></div><a href="javascript:void(0);"><span class="submit_del">删除</span></a></td>
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
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            &nbsp;</div>
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
        <div class="bntto">
            <%--<input type="button" id="btnOK" value="确定" class="adminsubmit_short" />--%>
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceCheckright" runat="server">
    <script src="/admin/js/jquery/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ProductAccessorie.helper.js" type="text/javascript"></script>
</asp:content>
