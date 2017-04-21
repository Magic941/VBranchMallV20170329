<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/BasicNoFoot.Master" CodeBehind="SelectExportProduct.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SelectExportProduct" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div  style="background: white;width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px">
                </h3>
        </div>
        <div class="Goodsgifts">
            <div class="left">
                <h1>
                <asp:Literal ID="litDesc" runat="server"></asp:Literal></h1>
                <asp:panel id="Panel1" runat="server" defaultbutton="btnSearch">
                    <ul>
                        <li>
                            <abbr class="formselect">
                                <asp:dropdownlist ID="drpProductCategory" runat="server">
                                </asp:dropdownlist>
                            </abbr>
                        </li>
                        <li>
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
                        <asp:DataList runat="server" ID="dlstSearchProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table">
                            <ItemTemplate>
                    <table width="100%" border="0" cellspacing="1" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                         <tr>
                            <td width="14%" rowspan="3" class="img">
                                  <img id="ThumbnailUrl40" ref="<%# Eval("ThumbnailUrl1") %>" />
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='#' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">市场价：<%# Eval("MarketPrice", "{0:n2}")%></span></td>
                        <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %>--%></td>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="15%" valign="top"><a href="javascript:void(0);"><span class="submit_add">添加</span></a></td>
                      </tr>
                   </table>
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            <asp:button runat="server" id="btnAddSearch" cssclass="adminsubmit" text="全部添加" />
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
                            onclick="btnClear_Click" /> 
                    </li>
                </ul>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server"/>
                        <asp:DataList runat="server" id="dlstAddedProducts"  width="96%" datakeyfield="ProductId" repeatlayout="Table">
                            <itemtemplate>
                     <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                        <tr>
                            <td width="14%" rowspan="3" class="img">
                                  <img id="Img1" ref="<%# Eval("ThumbnailUrl1") %>"  />
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='#' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td width="29%" height="28" valign="top"><span class="colorC">市场价：<%# Eval("MarketPrice", "{0:n2}")%></span></td>
                        <td width="19%" valign="top"> 库存：0<%--<%# Eval("Stock") %>--%></td>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="15%" valign="top"><a href="javascript:void(0);"><span class="submit_del">删除</span></a></td>
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
    <script src="/admin/js/jquery/ProductStationMode.helper.js" type="text/javascript"></script>
</asp:content>
