<%@ Page Title="积分限制管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="PointsLimit.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Points.PointsLimits" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="积分限制管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="设置每种积分规则使用的限制条件。" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li id="liadd" runat="server" style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px;"><a href="AddLimit.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                        <b>|</b> </li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="LimitID" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="false">
            <Columns>
                <asp:BoundField DataField="LimitID" HeaderText="ID" SortExpression="LimitID"
                    ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Name" HeaderText="限制名称" SortExpression="Name" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Cycle" HeaderText="周期频率" SortExpression="Cycle" ItemStyle-HorizontalAlign="center" />
<%--                <asp:BoundField DataField="CycleUnit" HeaderText="单位" SortExpression="CycleUnit"
                    ItemStyle-HorizontalAlign="center" />--%>

                    <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <%#GetUnitName(Eval("CycleUnit"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MaxTimes" HeaderText="次数限制" SortExpression="MaxTimes"
                    ItemStyle-HorizontalAlign="center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="LimitID" DataNavigateUrlFormatString="UpdateLimit.aspx?limitId={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"   Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
