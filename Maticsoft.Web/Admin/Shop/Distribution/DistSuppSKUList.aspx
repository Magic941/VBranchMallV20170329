<%@ Page Title="分销商商品管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="DistSuppSKUList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Distribution.DistSuppSKUList" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="分销商商品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对分销商的商品进行管理操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal3" runat="server" Text="分销商名称" />：
                    <asp:DropDownList ID="ddlSupplier" runat="server">

                    </asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"   class="adminsubmit_short">
                        </asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px">
                    <a href="ProductSKUList.aspx" >
                        <asp:Literal ID="Literal5" runat="server" Text="获取商品" />
                    </a> 
                    </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" Width="100%" OnRowDeleting="gridView_RowDeleting"
            PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="SKU"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <Columns>
                
                <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetProductName(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品SKU" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetSKUStr(Eval("SKU"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="库存" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Stock")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="警戒库存" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("AlertStock")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkDel" runat="server" CommandArgument='<%#Eval("SKU")%>'
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
