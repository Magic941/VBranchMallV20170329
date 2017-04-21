
<%@ Page Language="C#" Title="商品列表" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true"CodeBehind="SetProductList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.SetProductList" %>


<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
                        <asp:Literal ID="Literal1" runat="server" Text="活动商品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以移除，查询活动商品操作。
                     
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
                       <asp:Literal ID="Literal3" runat="server" Text="活动规则" />：
                    <asp:DropDownList ID="ddlRule" runat="server">
                    </asp:DropDownList>
                    <asp:Literal ID="Literal2" runat="server" Text="商品名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Literal ID="Literal4" runat="server" Text="商品编号" />：
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AMId,ProductId">
            <Columns>
                <asp:TemplateField HeaderText="活动名称" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label21" runat="server" Text='<%# GetAMName(Eval("AMId")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField DataField="ProductId" HeaderText="商品编号" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                      <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">  <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductName") %>' /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LowestSalePrice" ItemStyle-ForeColor="Red" HeaderText="商品售价" ItemStyle-HorizontalAlign="Center" />
                
                <%--<asp:TemplateField HeaderText="优惠方式" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label22" runat="server" Text='<%# GetAMType(Eval("AMType")) %>' />
                    </ItemTemplate>
                </asp:TemplateField> --%>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="35px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

