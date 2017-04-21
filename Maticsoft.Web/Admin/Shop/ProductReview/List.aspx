<%@ Page Title="商品评论表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductReview.List" %>
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
                        <asp:Literal ID="Literal1" runat="server" Text="商品评论" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对商品的评论信息进行管理
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
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">审核失败</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />

        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ReviewId">
            <Columns>
               <%-- <asp:BoundField DataField="ReviewId" HeaderText="编号" SortExpression="ReviewId"
                    ItemStyle-HorizontalAlign="Center" />--%>
                    <asp:TemplateField  HeaderText="评论内容" ItemStyle-HorizontalAlign="Left"
                    SortExpression="ReviewId">
                    <ItemTemplate>
                        <%#Maticsoft.Common.Globals.HtmlDecode(SubString(Eval("ReviewText"),"...",20)) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="评论商品" ItemStyle-HorizontalAlign="Left"
                    SortExpression="ProductId">
                    <ItemTemplate>
                       <%# GetProudctName(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField DataField="UserName" HeaderText="评论人" SortExpression="UserName"  ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="UserEmail" HeaderText="评论人邮箱" SortExpression="UserEmail"
                    ItemStyle-HorizontalAlign="Center"  Visible="false"/>
                <asp:BoundField DataField="CreatedDate" HeaderText="评论时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ParentId" HeaderText="ParentId" SortExpression="ParentId"
                    ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:TemplateField  HeaderText="评论状态" ItemStyle-HorizontalAlign="Center"
                    SortExpression="State">
                    <ItemTemplate>
                        <%#GetCommentStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"  SortExpression="ReviewId">
                    <ItemTemplate>
                        <a href="Show.aspx?id=<%#Eval("ReviewId") %>">详细信息</a>
                        &nbsp;&nbsp;  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Delete"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                    <asp:DropDownList ID="ddlAction" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlAction_Changed">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">审核失败</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnDelete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>