<%@ Page Title="热搜此" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SearchTop.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.SearchWordTop.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".Rank").each(function () {
                var value = $(this).text();
                if (value == 1) {
                    $(this).css("color", "red").css("font-size", 17).css("font-weight", "bolder");
                }
                if (value == 2) {
                    $(this).css("color", "blue").css("font-size", 17).css("font-weight", "bolder");
                }
                if (value == 3) {
                    $(this).css("font-size", 17).css("color", "#70F814").css("font-size", 17).css("font-weight", "bolder");
                }
            });
       });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
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
                        <asp:Literal ID="Literal1" runat="server" Text="热搜词排行" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以查看、删除
                        <asp:Literal ID="Literal3" runat="server" Text="热搜词" />
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
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
         
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Rank" HeaderText="排行" SortExpression="Rank" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-CssClass="Rank" />
                <asp:BoundField DataField="HotWord" HeaderText="热词" SortExpression="HotWord" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TimeUnit" HeaderText="0: 天(DateStart " SortExpression="TimeUnit"
                    Visible="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DateStart" HeaderText="开始日期" SortExpression="DateStart"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="DateEnd" HeaderText="截止日期" SortExpression="DateEnd" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="SearchCount" HeaderText="搜索数量" SortExpression="SearchCount"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreatedDate" HeaderText="创建日期" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" Visible="false" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="<%$ Resources:Site, btnDetailText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" Visible="false" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>