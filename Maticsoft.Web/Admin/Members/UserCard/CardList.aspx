
<%@ Page Title="用户会员卡" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="CardList.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserCard.CardList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        会员卡管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                     您可以对用户会员卡进行 删除等操作
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
                   
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="20" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="CardCode">
            <Columns>
                <asp:BoundField DataField="CardCode" HeaderText="卡号"    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="120" HeaderText="用户名" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetUserName(Eval("UserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="值" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetCardValue(Eval("CardValue"), Eval("Type"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="会员卡状态" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField ControlStyle-Width="120" HeaderText="会员卡类型" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetType(Eval("Type"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="创建时间" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CreatedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
           
             <%--   <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="CardCode" DataNavigateUrlFormatString="UpdateCard.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"   Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;display:none;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"   class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
