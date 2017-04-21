<%@ Page Title="配送方式管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="ShippingType.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Shipping.ShippingType" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="配送方式管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对网站配送方式进行添加，编辑，删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                       <li id="liAdd" runat="server" style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px; width: auto;">
                        <a href="AddShippingType.aspx" >添加</a> <b>|</b> </li>
                   
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="15" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="ModeId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" >
            <Columns>
                <asp:TemplateField HeaderText="配送方式名称" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="首重" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("Weight")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="缺省价" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80">
                    <ItemTemplate>
                        ￥<%# Maticsoft.Common.Globals.SafeDecimal(Eval("Price").ToString(),0).ToString("F")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加重" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("AddWeight")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加价" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="80">
                    <ItemTemplate>
                             ￥<%# Maticsoft.Common.Globals.SafeDecimal(Eval("AddPrice").ToString(), 0).ToString("F")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="物流公司" ItemStyle-HorizontalAlign="Center"  Visible="false">
                    <ItemTemplate>
                        <%#Eval("ExpressCompanyName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="40" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <a style="display: none;" href="ShowShippingType.aspx?id=<%#Eval("ModeId") %>">查看</a> &nbsp;&nbsp;
                      <span id="lbtnModify"  runat="server" >  <a href="UpdateShipType.aspx?id=<%#Eval("ModeId") %>">编辑</a> &nbsp;&nbsp;</span>
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton>
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
