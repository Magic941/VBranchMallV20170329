<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.ProductAccessories.Add" Title="增加页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%--<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>--%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <style type="text/css">
        .borderImage{width: 121px;height: 121px;border: 1px solid #CCC;text-align: center;}
    </style>
    <script type="text/javascript">
        $(function () {
            resizeImg('.borderImage', 120, 120);
            $('[id$="txtMaxQuantity"]').OnlyNum();
            $('[id$="txtMinQuantity"]').OnlyNum();
            $('[id$="txtStock"]').OnlyNum();
            $('[id$="txtDiscountAmount"]').OnlyFloat();

            $('[name="radioskuid"]').click(function () {
                $('[id$="hfsku"]').val($(this).val());
            });

            if ($('[id$="hfsku"]').val() != '') {//页面回发时恢复该值
                $('[name="radioskuid"]').each(function() {
                    if ($(this).val() == $('[id$="hfsku"]').val()) {
                        $(this).attr("checked", "checked");
                    }
                });
            } else {//默认选中第一个
                $('[name="radioskuid"]').eq(0).attr("checked", "checked");
                $('[id$="hfsku"]').val($('[name="radioskuid"]').eq(0).val()); 
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="增加配件组合" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="增加配件组合" /> 请先选择该商品的一个SKU
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td class="td_class">
		<asp:Literal ID="literName" runat="server" Text="组合名称" />：</td>
	<td height="25">
		<asp:TextBox id="txtName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr style="display:none;">
	<td class="td_class">
		最大购买量
	：</td>
	<td height="25">
		<asp:TextBox id="txtMaxQuantity" runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr  style="display:none;">
	<td class="td_class">
		最小购买量
	：</td>
	<td height="25">
		<asp:TextBox id="txtMinQuantity"  runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr style="display:none;">
	<td class="td_class" >
		优惠类型
	：</td>
	<td height="25">
        <asp:RadioButtonList ID="RadioDiscountType" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1" Selected="Selected">优惠金额</asp:ListItem>
            <asp:ListItem Value="2">优惠折扣</asp:ListItem>
        </asp:RadioButtonList>
	</td></tr>
	<tr  visible="false" runat="server" id="trDiscountAmount">
	<td class="td_class">
		优惠价           
	：</td>
	<td height="25">
		<asp:TextBox id="txtDiscountAmount" runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr visible="false"  runat="server" id="trStock">
	<td class="td_class">
		库存  
	：</td>
	<td height="25">
		<asp:TextBox id="txtStock" runat="server" Width="200px">0</asp:TextBox>
	   
	</td></tr>
    <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                        class="adminsubmit_short"  OnClick="btnSave_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        class="adminsubmit_short" OnClick="btnCancle_Click" OnClientClick=" javascript: parent.$.colorbox.close();"></asp:Button>
                                </td>
                            </tr>
</table>

                </td>
            </tr>
        </table></div>
        <div class="Goodsgifts">
               <div class="left" style="margin-left: 15px;width:98%;">
                <h1>
                   选择商品</h1>
                <ul>
 
                </ul>
                 <asp:HiddenField runat="server" ID="hfsku" />
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server"/>
                        <asp:DataList runat="server" id="dlstAddedProducts"
                        OnItemDataBound="dlstAddedProducts_OnItemDataBound" OnItemCommand="dlstAddedProducts_ItemCommand"
                         width="96%" datakeyfield="SkuId" repeatlayout="Table">
                            <itemtemplate>
                     <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("SKUId") %>">
                        <tr>
                            <td > 
                             <span  runat="server"  ><input type="radio" value="<%# Eval("SKU") %>"  name="radioskuid"/></span>     
                            </td>
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
        </tr><tr>
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
                            &nbsp;</div><div class="pagination">
                    <webdiyer:AspNetPager runat="server" ID="anpAddedProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager_PageChanged" PageSize="5" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                    </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       
     
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
