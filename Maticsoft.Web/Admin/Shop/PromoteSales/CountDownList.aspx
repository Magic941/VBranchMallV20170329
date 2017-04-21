<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="CountDownList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.PromoteSales.CountDownList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
          $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtDate']").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd"
            });
            $(".iframe").colorbox({ iframe: true, width: "680", height: "488", overlayClose: false });
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
                        <asp:Literal ID="Literal1" runat="server" Text="限时抢购活动商品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对限时抢购活动商品进行添加，编辑，删除，上架和下架操作
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
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal3" runat="server" Text="状态" />：
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text=" 全部" Value=""></asp:ListItem>
                        <asp:ListItem Text=" 上架" Value="1"></asp:ListItem>
                        <asp:ListItem Text=" 下架" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;结束时间：
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server" Text="活动说明" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li id="liAdd" runat="server" style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px;">
                        <a href="AddCountDown.aspx" >
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                    </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="CountDownId" >
            <Columns>
                <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="360">
                    <ItemTemplate>
                        <a href="/Product/ProSaleDetail/<%# Eval("CountDownId") %>" target="_blank">
                            <%# GetProductName(Eval("ProductId"))%>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="抢购价格" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" SortExpression="Price">
                    <ItemTemplate>
                        <%#Eval("Price", "￥{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"  SortExpression="EndDate">
                    <ItemTemplate>
                        <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="活动说明" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# GetStatusName(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                       <%# GetProductStatus(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <a href='Update.aspx?id=<%# Eval("CountDownId")%>' class="iframe">编辑</a>
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
                        class="adminsubmit" OnClick="btnDelete_Click" />
                        <asp:Button ID="Button1" runat="server" Text="批量上架"
                        class="adminsubmit" OnClick="btnOn_Click" />
                        <asp:Button ID="Button2" runat="server" Text="批量下架"
                        class="adminsubmit" OnClick="btnOff_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
