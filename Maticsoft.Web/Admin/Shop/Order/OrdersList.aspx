<%@ Page Title="订单管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="OrdersList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrdersList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
      <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("[id$='ddlSupplier']").select2({ placeholder: "请选择", width: "240px" });

            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var type = $.getUrlParam("type");
            if (type != null) {
                $("a:[href='OrdersList.aspx?type=" + type + "']").parents("li").removeClass(NotSelectedCss);
                $("a:[href='OrdersList.aspx?type=" + type + "']").parents("li").addClass(SelectedCss);
            } else {
                $("a:[href='OrdersList.aspx']").parents("li").removeClass(NotSelectedCss);
                $("a:[href='OrdersList.aspx']").parents("li").addClass(SelectedCss);
            }
            $(".ShipedAction").each(function () {
                var ShippingStatus = $(this).attr("ShippingStatus");
                var OrderStatus = $(this).attr("OrderStatus");
                if (ShippingStatus <= 1 && OrderStatus > 0) {
                    $(this).show();
                }
            });

            $(".ShipAction").each(function () {
                var ShippingStatus = $(this).attr("ShippingStatus");
                var OrderStatus = $(this).attr("OrderStatus");

                if (ShippingStatus < 1 && OrderStatus > 0) {
                    $(this).show();
                }
            });
            $(".iframe").colorbox({ iframe: true, width: "840", height: "700", overlayClose: false });
            $(".iframeShiped").colorbox({ iframe: true, width: "900", height: "800", overlayClose: false });

            $(".ordertype").each(function () {
                var tab = $(this).attr("value");
                var value = $(this).children().val();
                if (value == "True") {
                    $("#tab" + tab).show();
                }
            });
            $(".CompleteAction").each(function () {
                var OrderStatus = $(this).attr("OrderStatus");
                if (OrderStatus != 2 && OrderStatus != -1) {
                    $(this).show();
                }
            });
            $(".CancelAction").each(function () {
                var OrderStatus = $(this).attr("OrderStatus");
                if (OrderStatus != 2 && OrderStatus != -1) {
                    $(this).show();
                }
            });

            $(".PayAction").each(function () {
                var OrderStatus = $(this).attr("OrderStatus");
                if (OrderStatus == 0) {
                    $(this).show();
                }
            });
        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }

        function ExportConfirm() {
            if (confirm('您确认要导出么？')) {
                $.jBox.tip("正在导出订单数据，请稍候...", 'loading', { timeout: 3000 });
                return true;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="订单管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以根据订单状态查询订单，配送货等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    &nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server" Text="订单号" />：
                    <asp:TextBox ID="txtOrderCode" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralShipName" runat="server" Text="收货人" />：
                    <asp:TextBox ID="txtShipName" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralBuyerName" runat="server" Text="用户名" />：
                    <asp:TextBox ID="txtBuyerName" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="Literal6" runat="server" Text="订单ID" />：
                    <asp:TextBox ID="txtOrderID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Literal ID="LiteralPaymentStatus"
                        runat="server" Text="付款状态" />：
                    <asp:DropDownList ID="dropPaymentStatus" runat="server">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="0">未支付</asp:ListItem>
                        <asp:ListItem Value="2">已支付</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralShippingStatus" runat="server" Text="发货状态" />：
                    <asp:DropDownList ID="dropShippingStatus" runat="server">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="0">未发货</asp:ListItem>
                        <asp:ListItem Value="2">已发货</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralSupplier" runat="server" Text="商家" />：
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal10" runat="server" Text="收货地址" />：
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralCreatedDate" runat="server" Text="下单日期" />：
                    <asp:TextBox ID="txtCreatedDateStart" Onclick="WdatePicker()" runat="server"></asp:TextBox> -
                    <asp:TextBox ID="txtCreatedDateEnd" Onclick="WdatePicker()" runat="server"></asp:TextBox>


                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>

                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <span class="ordertype" value="Paying">
            <asp:HiddenField ID="hfPaying" runat="server" Value="True" />
        </span><span class="ordertype" value="PreHandle">
            <asp:HiddenField ID="hfPreHandle" runat="server" Value="True" />
        </span><span class="ordertype" value="Cancel">
            <asp:HiddenField ID="hfCancel" runat="server" Value="True" />
        </span><span class="ordertype" value="Locking">
            <asp:HiddenField ID="hfLocking" runat="server" Value="True" />
        </span><span class="ordertype" value="PreConfirm">
            <asp:HiddenField ID="hfPreConfirm" runat="server" Value="True" />
        </span><span class="ordertype" value="Handling">
            <asp:HiddenField ID="hfHandling" runat="server" Value="True" />
        </span><span class="ordertype" value="Shipping">
            <asp:HiddenField ID="hfShipping" runat="server" Value="True" />
        </span><span class="ordertype" value="Shiped">
            <asp:HiddenField ID="hfShiped" runat="server" Value="True" />
        </span><span class="ordertype" value="Success">
            <asp:HiddenField ID="hfSuccess" runat="server" Value="True" />
        </span>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="OrdersList.aspx?type=0" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="近三个月订单"></asp:Literal></a></li>
                    <li class="normal" id="tabPaying" style="display: none"><a href="OrdersList.aspx?type=1">
                        <asp:Literal ID="Literal8" runat="server" Text="等待付款"></asp:Literal></a></li>
                    <li class="normal" id="tabPreHandle" style="display: none"><a href="OrdersList.aspx?type=2">
                        <asp:Literal ID="Literal9" runat="server" Text="等待处理"></asp:Literal></a></li>
                    <li class="normal" id="tabCancel" style="display: none"><a href="OrdersList.aspx?type=3">
                        <asp:Literal ID="Literal12" runat="server" Text="取消订单"></asp:Literal></a></li>
                    <li runat="server" visible="false" class="normal" id="tabLocking" style="display: none"><a href="OrdersList.aspx?type=4">
                        <asp:Literal ID="Literal13" runat="server" Text="订单锁定"></asp:Literal></a></li>
                    <li class="normal" id="tabPreConfirm" style="display: none"><a href="OrdersList.aspx?type=5">
                        <asp:Literal ID="Literal14" runat="server" Text="等待付款确认"></asp:Literal></a></li>
                    <li class="normal" id="tabHandling" style="display: none"><a href="OrdersList.aspx?type=6">
                        <asp:Literal ID="Literal15" runat="server" Text="正在处理"></asp:Literal></a></li>
                    <li class="normal" id="tabShipping" style="display: none"><a href="OrdersList.aspx?type=7">
                        <asp:Literal ID="Literal16" runat="server" Text="配货中"></asp:Literal></a></li>
                    <li class="normal" id="tabShiped" style="display: none"><a href="OrdersList.aspx?type=8">
                        <asp:Literal ID="Literal17" runat="server" Text="已发货"></asp:Literal></a></li>
                    <li class="normal" id="tabSuccess" style="display: none"><a href="OrdersList.aspx?type=9">
                        <asp:Literal ID="Literal5" runat="server" Text="已完成"></asp:Literal></a></li>
                    <li class="normal" id="tabHistory" style="display: none"><a href="OrdersList.aspx?type=-1">
                        <asp:Literal ID="Literal18" runat="server" Text="历史订单"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="False " DataKeyNames="OrderId" Style="float: left;">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <a class="iframe" href="OrderShow.aspx?orderId=<%#Eval("OrderId") %>&type=<%#Type%>">
                            <%# Eval("OrderCode")%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField  ControlStyle-Width="50" HeaderText="子订单号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100"> 
                    <ItemTemplate>
                        <%#GetOrderChild(Eval("OrderCode"))%>
                    </ItemTemplate>
                </asp:TemplateField>--%>


                <%--      <asp:TemplateField ControlStyle-Width="50" HeaderText="订单来源" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  Eval("ReferURL")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="下单时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreatedDate")).ToString("yyyy-MM-dd HH:mm:ss")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="订单总额" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("OrderTotal", "{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="应付总额" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("Amount", "{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="用户名" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("BuyerName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="收货人" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("ShipName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="订单状态" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120" Visible="False">
                    <ItemTemplate>
                        <%#GetOrderStatus(Eval("OrderStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="发货状态" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# GetShippingStatus(Eval("ShippingStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="配送方式" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("ShippingModeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="支付方式" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("PaymentTypeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="订单状态" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%#GetOrderType(Eval("PaymentGateway"), Eval("OrderStatus"), Eval("PaymentStatus"), Eval("ShippingStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="支付流水号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("PaymentNumber")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="支付时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("PayDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="160" HeaderText="订单操作" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%--   <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>--%>
                        <a class="iframe" style="border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;"
                            href="OrderShow.aspx?orderId=<%#Eval("OrderId") %>&type=<%#Type%>">查看</a>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Pay"
                            Style="display: none; border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px;"
                            CommandArgument='<%#Eval("OrderId")+","+Eval("OrderCode")%>'
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确定要将订单设置为已支付吗？请在支付网站方确认用户已支付."
                            Text="支付" class="PayAction" OrderStatus='<%# Eval("OrderStatus")%>'></asp:LinkButton>
                        <a class="iframe ShipAction" shippingstatus="<%# Eval("ShippingStatus")%>" orderstatus='<%# Eval("OrderStatus")%>'
                            href="OrderItemInfo.aspx?orderId=<%#Eval("OrderId") %>" style="display: none; border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;">配货</a> 
                        <a class="iframeShiped ShipedAction" shippingstatus="<%# Eval("ShippingStatus")%>" orderstatus='<%# Eval("OrderStatus")%>'
                           href="OrderShip.aspx?orderId=<%#Eval("OrderId") %>" style="display: none; border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;">发货</a>
                        <asp:LinkButton ID="linkReturn" Visible="False" runat="server" CausesValidation="False"
                            CommandName="Return" CommandArgument='<%#Eval("OrderId") %>' OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="您确定要退货吗？" Text="退货"></asp:LinkButton>
                        <asp:LinkButton ID="linkCancel" Style="display: none; border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;"
                            OrderStatus='<%# Eval("OrderStatus")%>' class="CancelAction"
                            runat="server" CausesValidation="False" CommandName="CancelOrder" CommandArgument='<%#Eval("OrderId")+","+Eval("OrderCode")%>'
                            Text="取消" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确定要取消吗？"></asp:LinkButton>
                        <asp:LinkButton ID="linkComplete" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="您确定要完成吗？" Style="display: none; border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;"
                            OrderStatus='<%# Eval("OrderStatus")%>' class="CompleteAction"
                            runat="server" CausesValidation="False" CommandName="Success" CommandArgument='<%#Eval("OrderId")+","+Eval("OrderCode")%>'
                            Text="完成"></asp:LinkButton>
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

        <%--<asp:Button ID="btnExport" style="display:none;" runat="server" Text="一键导出" OnClientClick="return ExportConfirm();" OnClick="btnExport_Click"></asp:Button>
        &nbsp;&nbsp;--%>
        <asp:Button ID="btnExportOrderDetail" runat="server" Text="导出订单明细" class="adminsubmit" OnClientClick="return ExportConfirm();" OnClick="btnExportOrderDetail_Click"  />
        <asp:Button ID="btnExportProductDetail" runat="server"  Text="导出商品明细" class="adminsubmit" OnClientClick="return ExportConfirm();" OnClick="btnExportProductDetail_Click"/>
        <asp:Button ID="Button1" runat="server" Text="一键导出" OnClientClick="return ExportConfirm();"
                        OnClick="btnExportNew_Click" class="adminsubmit"></asp:Button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
