<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ReservationService.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Reservation.ReservationService" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        用户预约服务管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对用户预约服务进行管理等操作
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a href="AddService.aspx">添加</a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowSorting="True" ShowToolBar="True"
            AutoGenerateColumns="False" OnBind="BindData" OnRowDeleting="gridView_RowDeleting"
            UnExportedColumnNames="Modify" Width="100%" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false"
            DataKeyNames="ServiceId">
            <Columns>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="服务号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ServiceId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="服务名称" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="服务类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetServiceType(Eval("ServiceId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="预约限定规则" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetRuleType(Eval("RuleType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="总订单限制" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("MaxCount")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="开始时间/结束时间" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("StartDate", "{0:yyyy/MM/dd HH:mm:ss}")%>-<%#Eval("EndDate","{0:yyyy/MM/dd HH:mm:ss}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="是否需要支付" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetPay(Eval("IsPay"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="UpdateService.aspx?id=<%#Eval("ServiceId") %>" itemstyle-horizontalalign="Center">
                            编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确认要删除吗"
                            Text="删除"></asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
