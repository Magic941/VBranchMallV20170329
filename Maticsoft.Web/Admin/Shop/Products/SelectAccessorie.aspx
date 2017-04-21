<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SelectAccessorie.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SelectAccessorie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dataarea mainwidth td_top_ccc" style="background: white;">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h1 class="title_height">
                选择配件商品</h1>
        </div>
        <div class="search_results">
            <label>
                SKU：
            </label>
            <asp:TextBox ID="txtSKU" runat="server">
            </asp:TextBox>
            &nbsp;
            <label>
                商品分类：
            </label>
            <asp:DropDownList ID="drpProductCategory" runat="server">
                <asp:ListItem Text="请选择" Selected="True" Value="">
                </asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <label>
                产品名称：
            </label>
            <asp:TextBox ID="txtProductName" runat="server">
            </asp:TextBox>
            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="搜索" CssClass="adminsubmit_short" />
        </div>
        <div class="results">
            <div class="results_main" style="height: 306px">
                <asp:HiddenField ID="hfCurrentAllData" runat="server" />
                <asp:HiddenField ID="hfCurrentDataCount" runat="server" />
                <asp:GridView ID="gridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gridView_PageIndexChanging" BackColor="White" CssClass="GridViewStyle"
                    RowStyle-CssClass="grdrow" ShowHeader="False" OnRowDataBound="gridView_RowDataBound"
                    Width="100%" PageSize="10" HeaderStyle-CssClass="GridViewHeaderStyle" SelectedRowStyle-BackColor="#FBFBF4"
                    CellPadding="3" BorderWidth="1px" DataKeyNames="key">
                    <Columns>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <input id="chkSkuId" type="checkbox" value='<%# Eval("key") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                &nbsp;<asp:Label runat="server" Text='<%# Eval("value") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle Height="25px"/>
                    <EmptyDataTemplate>
                        没有搜索到商品, 请换个条件试试.
                    </EmptyDataTemplate>
                    <%--<PagerSettings Mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"></PagerSettings>--%>
                    <PagerStyle Height="25px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                    <PagerTemplate>
                        当前第:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        页/共:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        页
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                            Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                            Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                        <%--转到第
<asp:textbox id="txtNewPageIndex" runat="server" width="20px" text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页--%>
                        <%--<asp:linkbutton id="btnGo" runat="server" causesvalidation="False" commandargument="-1" commandname="Page" text="GO" /> --%>
                    </PagerTemplate>
                </asp:GridView>
            </div>
        </div>
        <div class="results_img">
        </div>
        <div class="results_bottom">
            <span class="spanE">您当前选择了<span id="selectedNum"></span>条记录</span>， 点此取消选择。 <span
                id="SelectAll">点此选择全部的<asp:Label ID="Count" runat="server"></asp:Label>
                条记录</span> <span class="spanE">您当前选择了全部的<span id="SpselectNuman1"></span>条记录，点此取消选选择部记录</span>
        </div>
        <div class="bntto">
            <input type="button" id="btnOK" value="确定" class="adminsubmit_short" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>