<%@ Page Title="SN码管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="CodeList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.CodeList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtStartDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtEndDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtStartDate").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").val($(this).val());
                }
            });
        })
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="奖品明细查询" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行查看奖品明细" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td>
                </td>
                <td>
                    使用状态
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="" Text="请选择"> </asp:ListItem>
                        <asp:ListItem Value="0" Text="未领取"> </asp:ListItem>
                        <asp:ListItem Value="1" Text="已领取"> </asp:ListItem>
                        <asp:ListItem Value="2" Text="已兑换"> </asp:ListItem>
                    </asp:DropDownList>
                    开始时间：<asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;结束时间：<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server">
                        <asp:Literal ID="Literal3" runat="server" Text="兑换卡号" />：</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="CodeName" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true">
            <Columns>
                <asp:TemplateField HeaderText="兑换码卡号" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("CodeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="活动名称" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ActivityName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="奖品名称" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("AwardName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="微信用户" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center"
                    Visible="False">
                    <ItemTemplate>
                        <%#Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="活动时间" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("StartDate","{0:yyyy-MM-dd}")%>
                        至
                        <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatusName(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="中奖者手机" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Phone")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="兑奖时间" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UsedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Remark")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
        <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClick="btnDelete_Click" CssClass="adminsubmit" />
        <asp:DropDownList ID="ddlAction" runat="server" OnSelectedIndexChanged="ddlAction_Changed"
            AutoPostBack="True">
            <asp:ListItem Value="" Text="设置为....."> </asp:ListItem>
            <asp:ListItem Value="0" Text="未领取"> </asp:ListItem>
            <asp:ListItem Value="1" Text="已领取"> </asp:ListItem>
            <asp:ListItem Value="2" Text="已兑换"> </asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
