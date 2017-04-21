
<%@ Page Language="C#" Title="订单发货" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
   CodeBehind="OrderItemInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrderItemInfo" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<%@ Register TagPrefix="uc1" TagName="Region" Src="~/Controls/Region.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <!--jQueryUploadify Start-->
    <!--jQueryUploadify End-->
    <style type="text/css">
        .td_class
        {
            width: 80px;
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
            padding-bottom: 4px;
            padding-top: 4px;
        }
        .td_content
        {
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfSuccess" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="lblTitle" runat="server" Text="正在进行订单发货操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">商品清单</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">其它信息</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <%--基本信息--%>
            <div id="myTab1_Content0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td style="vertical-align: top;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal2" runat="server" Text=" 订单号" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblOrderCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal23" runat="server" Text=" 下单日期" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal21" runat="server" Text=" 配送方式" />：
                                    </td>
                                    <td height="25" class="td_content">
                                          <asp:Literal ID="lblShipTypeName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal1" runat="server" Text=" 实际运费" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Literal ID="lblFreightActual" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                                ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                                OnRowDataBound="gridView_RowDataBound" Width="100%" PageSize="10" ShowExportExcel="False"
                                ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
                                ShowCheckAll="False" DataKeyNames="ItemId" Style="float: left;">
                                <Columns>
                                    <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                           <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">
                                            <%#Eval("Name")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="货号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%#Eval("SKU")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="购买数量" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%# Eval("Quantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="商品单价" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <span class="txtprice">
                                                <%# Maticsoft.Common.Globals.SafeDecimal(Eval("AdjustedPrice").ToString(),0).ToString("F")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--         <asp:TemplateField ControlStyle-Width="50" HeaderText="当前库存" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <span class="txtstock">
                                                <%#GetStock(Eval("SKU"), Eval("ProductId"))%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="发货数量" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50" Visible="False">
                                        <ItemTemplate>
                                            <%--  <asp:TextBox ID="TextBox1" runat="server" Text=' <%#Eval("ShipmentQuantity")%>'></asp:TextBox>--%>
                                            <%#Eval("ShipmentQuantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle Height="25px" HorizontalAlign="Right" />
                                <HeaderStyle Height="35px" />
                                <PagerStyle Height="25px" HorizontalAlign="Right" />
                                <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
                                <RowStyle Height="25px" />
                                <SortDirectionStr>DESC</SortDirectionStr>
                            </cc1:GridViewEx>
                        </td>
                    </tr>
                </table>
            </div>
            <%--     收货人信息--%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td style="vertical-align: top;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                                style="padding-top: 8px">
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">购买人信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal18" runat="server" Text=" 购买人" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblBuyerName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal22" runat="server" Text="手机号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblBuyerCellPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal24" runat="server" Text="邮箱地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblBuyerEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="newslistabout">
        <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
            class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="height: 6px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="25" style="text-align: center">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="确认配货" class="adminsubmit" ></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
