<%@ Page Title="微信用户统计管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="UserCount.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.User.UserCount" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/Highcharts/highcharts.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtFrom").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtTo").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtTo").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtTo").val($(this).val());
                }
            });

            var categories = $("[id$='hfCategory']").val().split(',');
            var dayCount = [];
            var totalCount = [];

            var datavalue = $("[id$='hfDayCount']").val().split(',');
            for (var i = 0; i < datavalue.length; i++) {
                var item = parseInt(datavalue[i]);
                dayCount.push(item);
            }

            var totalData = $("[id$='hfTotalCount']").val().split(',');
            for (var i = 0; i < totalData.length; i++) {
                var item = parseInt(totalData[i]);
                totalCount.push(item);
            }

            $('#dayCount').highcharts({
                chart: {
                    type: 'line'
                },
                title: {
                    text: '微信用户统计'
                },
                subtitle: {
                    text: '新增关注人数'
                },
                xAxis: {
                    categories: categories
                },
                yAxis: {
                    title: {
                        text: '人数'
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' + this.x + '： ' + this.y + '人';
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: '新增关注人数',
                    data: dayCount
                }]
            });

            $('#totalCount').highcharts({
                chart: {
                    type: 'line'
                },
                title: {
                    text: '微信用户统计'
                },
                subtitle: {
                    text: '累积关注人数'
                },
                xAxis: {
                    categories: categories
                },
                yAxis: {
                    title: {
                        text: '人数'
                    }
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' + this.x + '： ' + this.y + '人';
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    }
                },
                series: [{
                    name: '累积关注人数',
                    data: totalCount
                }]
            });

            $("#dayCount").find("text").last().hide();
            $("#totalCount").find("text").last().hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信用户统计管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以查微信用户的统计信息" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfCategory" runat="server" />
            <asp:HiddenField ID="hfDayCount" runat="server" />
            <asp:HiddenField ID="hfTotalCount" runat="server" />
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="280px">
                    关注时间：
                    <asp:TextBox ID="txtFrom" runat="server" Width="90"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtTo" runat="server" Width="90"></asp:TextBox>
                    <asp:Button ID="btnReStatistic" runat="server" Text="统计" class="adminsubmit" OnClick="btnReStatistic_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            height="50px">
            <tr>
                <td style="width: 1px;">
                </td>
                <td style="width: 120px;">
                    今日新增关注人数：
                    <asp:Literal ID="lblDayCount" runat="server" Text="--"></asp:Literal>
                </td>
                <td>
                    累计关注人数：
                    <asp:Literal ID="lblTotalCount" runat="server" Text="--"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="1" width="100%" class="borderkuang tabChart"
            runat="server">
            <tr>
                <td>
                    <div id="dayCount">
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="1" width="100%" class="borderkuang"
            runat="server">
            <tr>
                <td>
                    <div id="totalCount">
                    </div>
                </td>
            </tr>
        </table>
         <br />
        <table border="0" cellpadding="0" cellspacing="1" width="100%" class="borderkuang"
            runat="server" id="tabGrid">
            <tr>
                <td width="800px">
                    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                        ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                        OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
                        PageSize="31" ShowExportExcel="true" ShowExportWord="false" ExcelFileName="FileName1"
                        CellPadding="3" BorderWidth="1px" ShowCheckAll="False">
                        <Columns>
                            <asp:TemplateField HeaderText="时间" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%#Eval("Date")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DayCount" HeaderText="新增关注人数" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="取消关注人数" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%#GetCancelCount(Eval("Date"))%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="累积关注人数 " ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%#GetTotalCount(Eval("Date"))%></ItemTemplate>
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
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
