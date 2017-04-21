<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsNoCategories.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsNoCategories" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2({placeholder:"请选择",width:"240px"});

            resizeImg('.borderImage', 80, 80);
        });
    </script>
    <style type="text/css">
        .autobrake
        {
            word-wrap: break-word;
            width: 280px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="未分类商品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       您可以将未分类的商品重新分类，或者将商品添加到多个分类
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
                    <asp:Literal ID="Literal2" runat="server" Text="搜索商品名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <%--<div class="newslist">
            <div class="newsicon">
                <ul>
                </ul>
            </div>
        </div>--%>
        <!--Search end-->
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ProductId" ShowExportExcel="false" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="1" BorderWidth="1px" ShowCheckAll="true"
            ShowToolBar="True">
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="borderImage">
                            <img ref='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(),"T350X350__") %>' /></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ProductName" ItemStyle-HorizontalAlign="Left"
                    HeaderText="商品名称" ControlStyle-Width="300">
                    <ItemTemplate>
                        <div class="autobrake">
                            <%# Eval("ProductName")%></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品价格" ItemStyle-HorizontalAlign="Left" SortExpression="MarketPrice">
                    <ItemTemplate>
                        市场价：<span style="color: red"><%#Eval("MarketPrice", "￥{0:N2}")%></span><br />销售价：<span style="color: green"><%#Eval("LowestSalePrice", "￥{0:N2}")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="AddedDate" ItemStyle-HorizontalAlign="Center"
                    HeaderText="发布时间">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("AddedDate")).ToString("yyyy-MM-dd HH:mm:ss")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="VistiCounts" ItemStyle-HorizontalAlign="Left"
                    HeaderText="库存数量">
                    <ItemTemplate>
                        <%#StockNum(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" SortExpression="ProductId">
                    <ItemTemplate>
                        <a href="ProductModify.aspx?pid=<%#Eval("ProductId") %>">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td align="left">
                    <asp:DropDownList CssClass="select2" ID="dropCategories" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnMove" runat="server" Text="转移主类" class="adminsubmit" OnClick="btnMove_Click" />
                    <asp:Button ID="btnDelete" OnClientClick="return confirm('你确定要放入回收站吗？要还原商品请到回收站找回！')"
                        runat="server" Text="批量删除" class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
