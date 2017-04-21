<%@ Page Title="Shop_RechargeCards" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.RechargeCards.List" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="充值卡" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、删除
                        <asp:Literal ID="Literal3" runat="server" Text="充值卡" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">
                        添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="#" onclick="btnDelete_Click">
                        删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="0" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Number" HeaderText="卡号" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Password" HeaderText="密码" SortExpression="Password" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField SortExpression="Amount" ItemStyle-HorizontalAlign="Center"
                    HeaderText="金额">
                    <ItemTemplate>
                          <%#Eval("Amount", "￥{0:N2}")%>
                     </ItemTemplate>
                </asp:TemplateField>
                
                             <asp:TemplateField SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center"
                    HeaderText="生成时间">
                    <ItemTemplate>
                        <%#Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UsedDate" HeaderText="使用时间" SortExpression="UsedDate"
                    ItemStyle-HorizontalAlign="Center" />
                       <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                    HeaderText="状态">
                    <ItemTemplate>
                          <%#Eval("Status").ToString()=="1"?"已使用":"未使用"%>
                     </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
