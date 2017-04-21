<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductSales.aspx.cs" Inherits="Maticsoft.Web.Admin.Statistics.ProductSales" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$=txtStartDate]").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("[id$=txtEndDate]").datepicker("option", "minDate", selectedDate);
                }
            });
            $("[id$=txtEndDate]").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("[id$=txtStartDate]").datepicker("option", "maxDate", selectedDate);
                    $("[id$=txtEndDate]").val($(this).val());
                }
            });
        })
    </script>
    <script src="/FusionCharts/FusionCharts.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="litTitle" runat="server" Text="商品销量排行" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="litDec" runat="server" Text="您可以查看商品销量排行走势图信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="width: 610px; float: left; height: 38px; line-height: 38px;margin-left: 10px;">
                                开始时间：<asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;结束时间：<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                <asp:RadioButtonList ID="rdoMode" runat="server" RepeatDirection="Horizontal" Style="float: right;
                                    height: 38px; line-height: 38px;">
                                    <asp:ListItem Text="天" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="月" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="年" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="float: left; height: 38px; line-height: 38px;">
                                &emsp;&emsp;<asp:Button ID="btnReStatistic" runat="server" Text="统计" class="adminsubmit"
                                    OnClick="btnReStatistic_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Literal ID="litChart" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gridView_PageIndexChanging"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnRowDataBound="gridView_RowDataBound"
            UnExportedColumnNames="Modify" Width="900" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowGridLine="true" ShowHeaderStyle="true">
            <Columns>
                <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        
                        <%#Eval("ProductName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销售量(份)" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ToalQuantity")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <asp:Button ID="Button1" runat="server" Text="一键导出" OnClientClick="return ExportConfirm();"
            OnClick="btnExportNew_Click" class="adminsubmit"></asp:Button>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
