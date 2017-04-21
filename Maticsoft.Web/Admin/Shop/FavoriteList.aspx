<%@ Page Title="收藏管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="FavoriteList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.FavoriteList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtTitle" runat="server" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound"  Width="100%"
            PageSize="10" DataKeyNames="FavoriteId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="商品名称" SortExpression="Type"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <a href="javascript:;"> <%# GetTargetName(Convert.ToInt32(Eval("TargetId")), Convert.ToInt16(Eval("TypeId")))%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="用户" SortExpression="Type"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetUserName(Convert.ToInt32(Eval("UserId")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Tags" HeaderText="收藏标签" SortExpression="Description"
                    ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Remark" HeaderText="收藏备注" SortExpression="Description"
                    ItemStyle-HorizontalAlign="center" />
                <asp:TemplateField ControlStyle-Width="120" HeaderText="收藏时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreateDate"))%>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
