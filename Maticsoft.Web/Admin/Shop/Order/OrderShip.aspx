<%@ Page Language="C#" Title="订单发货" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="OrderShip.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrderShip" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<%@ Register TagPrefix="uc1" TagName="Region" Src="~/Controls/Region.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        $(function () {
            $('[id$="PrintData_btnPrint"]').click(function () {
                var shipperid = $('[id$="dropShippers"]').val();
                var orderId = $('[id$="hidorderid"]').val();
                var xmlFile = $('[id$="dropExpressTemp"]').val();
                if (parseInt(shipperid) <= 0) {
                    ShowFailTip("请选择发货人信息！");
                    return false;
                }
                if ($.trim(orderId) == '') {
                    ShowFailTip("请刷新页面重试！");
                    return false;
                }
                if ($.trim(xmlFile) == '' || $.trim(xmlFile) == '0') {
                    ShowFailTip("请选择模版！");
                    return false;
                }
                window.open("/Admin/Shop/ExpressTemplate/print.html?ShipperId=" + shipperid + "&OrderId=" + orderId + "&XmlFile=" + xmlFile);
            });
        });
        function SubForm() {
            var success = true;
            var shipType = $('[id$=ddlShipType]').val();
            if (shipType == "0") {
                ShowFailTip("请选择配送方式！");
                return false;
            }
            var orderNumber = $('[id$=txtShipOrderNumber]').val();
            if (orderNumber == "") {
                if (!confirm('您还未输入物流单号, 确认发货吗?')) {
                    return false;
                }
            }
            return success;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfSuccess" runat="server" />
    <input type="hidden" value="<%=OrderId %>" id="hidorderid"/>
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
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">其它信息</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:;">打印快递单</a></li>
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
                                        <asp:HiddenField ID="hfWeight" runat="server" />
                                        <asp:DropDownList ID="ddlShipType" runat="server" OnSelectedIndexChanged="ShipType_Changed"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0" Text="请选择配送方式"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal1" runat="server" Text=" 实际运费" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtFreightActual" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal123" runat="server" Text=" 调整后运费" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtFreightAdjusted" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal6" runat="server" Text=" 物流单号" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipOrderNumber" runat="server" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal5" runat="server" Text=" 备注" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" Width="280px"></asp:TextBox>
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
                                        <span style="font-size: 16px; padding-left: 20px">收货人信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal3" runat="server" Text="收货人" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipName" runat="server" Width="320px"></asp:TextBox>
                                        <%--   <asp:Label ID="lblShipName" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal4" runat="server" Text="收货人地区" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <%--      <asp:Label ID="lblShipRegion" runat="server"></asp:Label>--%>
                                        <%--         <Maticsoft:RegionDropList ID="RegionList" runat="server" IsNull="true" />--%>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <uc1:Region ID="RegionList" runat="server" VisibleAll="true" VisibleAllText="--请选择--" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal7" runat="server" Text="详细地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipAddress" runat="server" Width="320px"></asp:TextBox>
                                        <%--    <asp:Label ID="lblShipAddress" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal9" runat="server" Text="电话号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipTelPhone" runat="server" Width="320px"></asp:TextBox>
                                        <%--  <asp:Label ID="lblShipTelPhone" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal10" runat="server" Text="手机号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipCellPhone" runat="server" Width="320px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal14" runat="server" Text="邮政编码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblShipZipCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal16" runat="server" Text="邮箱地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblShipEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
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
                                <tr>
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
            
            <%--打印快递单--%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td style="vertical-align: top;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                                style="padding-top: 8px">
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">收货人信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal8" runat="server" Text="收货人" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelShipName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal11" runat="server" Text="收货人地区" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <uc1:Region ID="regionLists" runat="server" VisibleAll="true" CityEnabled="false" ProvinceEnabled="false" AreaEnabled="false" VisibleAllText="--请选择--" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal12" runat="server" Text="详细地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                         <asp:Label ID="labelShipAddress" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal13" runat="server" Text="电话号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelShipTelPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal15" runat="server" Text="手机号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                       <asp:Label ID="labelCellPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal17" runat="server" Text="邮政编码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelShipZipCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal19" runat="server" Text="邮箱地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelShipEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    <td style="vertical-align: top;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                              <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                                style="padding-top: 8px">
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">发货人信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="width: 90px; background-color: #E2E8EB">
                                        <asp:Literal ID="Literal20" runat="server" Text="发货标签选择" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:DropDownList ID="dropShippers" runat="server" OnSelectedIndexChanged="dropShippers_Changed" AutoPostBack="True" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal25" runat="server" Text="发货人姓名" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lalelShipperName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal28" runat="server" Text="发货地" />：
                                    </td>
                                    <td height="25" class="td_content">
                                         <uc1:Region ID="regionListShip" runat="server" VisibleAll="true" CityEnabled="false" ProvinceEnabled="false" AreaEnabled="false" VisibleAllText="--请选择--" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal26" runat="server" Text="邮编" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelZipcode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                   <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal27" runat="server" Text="手机" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="labelTelPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>  
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                           
                        </td> 
                    </tr>
                     <tr>
                    <td style="vertical-align: top;">
                        
                                              <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                                style="padding-top: 8px">
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">选择快递单模版</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal29" runat="server" Text="选择模版" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:DropDownList ID="dropExpressTemp" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="25"  style="text-align: center"  >
                                       <input type="button" id="PrintData_btnPrint"   value="立即打印快递单"  class="adminsubmit6" />
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
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="确认发货" class="adminsubmit"
                                    OnClientClick="return SubForm();"></asp:Button>
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
