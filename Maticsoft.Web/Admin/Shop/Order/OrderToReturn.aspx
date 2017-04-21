<%@ Page Title="订单管理2" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="OrderToReturn.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrderToReturn" %>

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
    <script src="/Scripts/json2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='ddlSupplier']").select2({ placeholder: "请选择" });

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
                var Status = $(this).attr("Status");
                var LogisticStatus = $(this).attr("LogisticStatus");
                if (Status == 1 && LogisticStatus == 1) {
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

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            //日期控件
            $("[id$='txtCreatedDateStart']").prop("readonly", true).datepicker({
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });
            $("[id$='txtCreatedDateEnd']").prop("readonly", true).datepicker({
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });

            //判断退货退款状态
            $(".div_states").each(function () {
                debugger
                var status = parseInt($(this).attr("Status"));//待审核
                var logisticStatus = parseInt($(this).attr("logisticStatus"));//待发货
                var RefundStatus = parseInt($(this).attr("RefundStatus"));//待退款
                var ReturnGoodsType = parseInt($(this).attr("ReturnGoodsType")); //申请类型 --退货，退款
                var ShippingStatus = parseInt($(this).attr("ShippingStatus"));
                var ReturnGoodsType = parseInt($(this).attr("ReturnGoodsType"));
                $(this).find('.detail').show();
                if (status == 0) {
                    //待审核
                    $(this).find('.audit').show();
                }
                //待收货
                if (status == 1 && logisticStatus == 1 && RefundStatus == 0 && ReturnGoodsType ==1) {
                    $(this).find('.Logistic').show();
                }
                //待退款
                if ((status == 1 && logisticStatus == 2) || (status == 1 && logisticStatus ==0 && ReturnGoodsType == 2 && RefundStatus < 2 && ShippingStatus == 0)) {
                    $(this).find('.refund').show();
                }
                if (status == 2  && RefundStatus == 3 ) {
                    //已完成
                    $(this).find('.Logistic').hide();
                    $(this).find('.audit').hide();
                    $(this).find('.refund').hide();
                }
            });

            $("span:contains('已完成')").css("color", "green");
            $("span:contains('等待退款')").css("color", "#C27512");
            $("span:contains('取货中')").css("color", "#C27512");
            $("span:contains('审核未通过')").css("color", "red");
            $("span:contains('等待审核')").css("color", "#B23418");
            $("span:contains('已取消')").css("color", "red");
            $("span:contains('等待发货')").css("color", "#608B4E");
            $("span:contains('拒收退货')").css("color", "#E35D5D");
            $("span:contains('已发货')").css("color", "#009DE0");
            $("span:contains('已退款')").css("color", "#009DE0"); 
            $("span:contains('已收货')").css("color", "#2881BD");
            $("span:contains('退款完成')").css("color", "green");
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
                        <asp:Literal ID="Literal3" runat="server" Text="退货管理" />
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
                <td></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="LiteralReturnStatus"
                    runat="server" Text="退货退款状态" />：
                    <asp:DropDownList ID="dropReturnStatus" runat="server">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">未发货</asp:ListItem>
                        <asp:ListItem Value="2">已发货</asp:ListItem>
                        <asp:ListItem Value="3">未退款</asp:ListItem>
                        <asp:ListItem Value="4">已退款</asp:ListItem>
                        <asp:ListItem Value="5">已拒绝</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal1" runat="server" Text="订单号" />：
                    <asp:TextBox runat="server" ID="txtOrderCode"></asp:TextBox>
                    <asp:Literal ID="Literal2" runat="server" Text="退货单号" />：
                    <asp:TextBox runat="server" ID="txtReturnCode"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                    <asp:HiddenField ID="orderId" runat="server" Value="0" />
                </td>
            </tr>
        </table>
        <br />

        <%-- OnRowCommand="gridView_RowCommand"--%>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="False " DataKeyNames="OrderId" Style="float: left;">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="退款编号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <a class="iframe" href="OrderReturnDetail.aspx?returnItemId=<%#Eval("Id") %>">
                            <%# Eval("Id")%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="订单编码" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("OrderCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="退货单号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("ReturnOrderCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="商品信息" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#Eval("ProductName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="买家" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#GetUserNameById(Eval("UserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="卖家" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("Suppliername")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="交易金额" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# Eval("OrderAmounts","{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField ControlStyle-Width="50" HeaderText="退款金额" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80" Visible="False">
                    <ItemTemplate>
                        <%#Math.Round(decimal.Parse(Eval("AdjustedPrice").ToString())*(int.Parse(Eval("Quantity").ToString())),2)%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="申请时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("CreateTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField ControlStyle-Width="50" HeaderText="超时时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("TimeOut")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField ControlStyle-Width="80" HeaderText="退款状态" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <span><%#GetMainStatusStr(Convert.ToInt32(Eval("Status")),Convert.ToInt32(Eval("RefundStatus")),Convert.ToInt32(Eval("LogisticStatus")),Convert.ToInt32(Eval("ReturnGoodsType")),Convert.ToInt32(Eval("ShippingStatus"))) %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="160" HeaderText="退货详情" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <div class="div_states" Status ='<%# Eval("Status") %>' LogisticStatus="<%#Eval("LogisticStatus")%>" RefundStatus="<%#Eval("RefundStatus") %>"  ReturnGoodsType = "<%#Eval("ReturnGoodsType") %>"  ShippingStatus = "<%#Eval("ShippingStatus") %>" ReturnGoodsType ="<%#Eval("ShippingStatus") %>">
                            
                            <a class="iframe detail" style="border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap;"
                                href="OrderReturnDetail.aspx?returnItemId=<%#Eval("Id") %>">退货详情</a>

                            <a class="iframe audit" style="border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap; display: none;"
                                href="OrderReturnExamine.aspx?returnItemId=<%#Eval("Id") %>"">审核</a>

                             <%--<asp:LinkButton ID="linkComplete" runat="server"  CausesValidation="False" CommandName="Logistic"
                           Style="display: none; border: 1px #1317fc solid; padding-left: 5px;
                            padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; 
                            white-space: nowrap;"  CommandArgument='<%#Eval("Id")+","+Eval("ReturnOrderCode")%>'
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确定已经收货了吗？."
                            Text="确认收货" class="iframe Logistic"  Status='<%# Eval("Status")%>'></asp:LinkButton>--%>
                            <a class ="iframe  Logistic" style="border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap; display: none;"
                                href="OrderComplete.aspx?returnItemId=<%#Eval("Id") %>">确认收货</a>
                            <a class="iframe  refund" style="border: 1px #1317fc solid; padding-left: 5px; padding-right: 5px; margin-right: 5px; padding-top: 2px; padding-bottom: 2px; white-space: nowrap; display: none;"
                                href="OrderReturnRefund.aspx?returnItemId=<%#Eval("Id") %>">退款</a>

                        </div>

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
