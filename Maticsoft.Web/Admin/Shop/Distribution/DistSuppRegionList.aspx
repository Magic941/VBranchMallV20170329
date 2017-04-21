<%@ Page Title="分销商地区管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="DistSuppRegionList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Distribution.DistSuppRegionList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "580", height: "360", overlayClose: false });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="分销商地区管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对分销商进行地区分配管理操作" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px">
                    <a href="AddDistSuppRegion.aspx" class="iframe">
                        <asp:Literal ID="Literal5" runat="server" Text="添加" />
                        </a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound"   Width="100%" OnRowDeleting="gridView_RowDeleting"
            PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="SupplierId,RegionId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="分销商名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetSuppName(Eval("SupplierId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分销地区" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetRegionName(Eval("RegionId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkDel" runat="server" CommandArgument='<%#Eval("SupplierId")+","+Eval("RegionId")%>'
                            CommandName="Delete" ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>">
                        </asp:LinkButton>
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
