<%@ Page Title="分销商商品分配管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="ProductSKUList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Distribution.ProductSKUList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnGetStock").click(function () {
                ShowServerBusyTip("正在分配库存，请稍候！");
                var list = [];
                $(".txtStock").each(function () {
                    var sku = $(this).attr("sku");
                    var stock = $(this).val();
                    var json = { SKU: sku, Stock: stock };
                    list.push(json);
                });
                var supplierId = $("[id$='ddlSupplier']").val();
                if (supplierId == 0) {
                    ShowFipTip("请选择分销商！");
                    return;
                }
                $.ajax({
                    url: ("ProductSKUList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'post',
                    dataType: 'json',
                    data: { Action: "GetSuppStock", Callback: "true", List: JSON.stringify(list), SupplierId: supplierId },
                    success: function (result) {
                        if (result.STATUS == "SUCCESS") {
                            ShowSuccessTip("分销商分配库存成功！");
                            setTimeout(function () {
                                window.location.reload();
                            }, 2000);
                        }
                        else {
                            ShowFailTip("服务器繁忙，请稍候再试！");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        ShowFailTip("服务器繁忙，请稍候再试！");
                    }
                });
            })
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="分销商商品分配管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对商品进行分销商分配管理操作" />
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
                    <div style="float: left">
                        <Maticsoft:CategoriesDropList ID="ddlCateList" runat="server" IsNull="true" />
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" Width="100%" OnRowDeleting="gridView_RowDeleting"
            PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="SkuId" Style="float: left;"
            ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
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
                <asp:TemplateField HeaderText="获取数量" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <input type="text" class="txtStock" value="0" sku='<%#Eval("SKU")%>' />
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
            <tr>
                <td style="width: 5px;">
                </td>
                <td align="left">
                    <asp:Literal ID="Literal3" runat="server" Text="分销商名称" />：
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                    <input id="btnGetStock" type="button" value="获取" class="adminsubmit_short" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
