<%@ Page Title="<%$Resources:AddPaymentMode,ptPaymentModes %>"  Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="PaymentModes.aspx.cs" Inherits="Maticsoft.Web.Admin.TaoPayment.PaymentModes" %>
  <%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/Maticsoftv5.css" type="text/css" />
    <script type="text/javascript" src="../js/Maticsoftv5.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <Maticsoft:StatusMessage ID="statusMessage" runat="server" Visible="False" />
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:AddPaymentMode,ptPaymentModes %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:AddPaymentMode,lblPaymentModes %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="AddPaymentMode.aspx">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                </ul>
            </div>
        </div>
            <cc1:GridViewEx ID="grdPaymentMode" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="False" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="grdPaymentMode_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ModeId">
                    <Columns>
               <asp:TemplateField HeaderText="<%$ Resources:PaymentModes, IDS_Header_Name %>" ItemStyle-Width="25%">
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="merchantCode" HeaderText="<%$ Resources:PaymentModes, IDS_Header_merchantCode %>">
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:PaymentModes, IDS_Header_Gateway %>">
                    <ItemTemplate>
                        <asp:Label ID="lblGatawayType" runat="server" Text='<%# Eval("Gateway") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <Maticsoft:SortImageColumn HeaderText="<%$ Resources:PaymentModes,IDS_Header_DisplaySequence %>" />
                <asp:TemplateField HeaderText="编辑">
                    <ItemStyle Width="100px" CssClass="GridViewTyle" />
                    <ItemTemplate>
                        <a style="color: #1317FC;" href='EditPaymentMode.aspx?modeId=<%# Eval("ModeId") %>'>
                            <asp:Literal ID="lblManagerText" runat="server" Text="<%$ Resources:Resources, IDS_Button_Edit %>"></asp:Literal></a>
                        &nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle Width="100px" CssClass="GridViewTyle" />
                    <ItemTemplate>
                        <Maticsoft:DeleteImageLinkButton ID="DeleteImageLinkButton1" Style="color: #1317FC;" runat="server"
                            Text="<%$ Resources:Resources, IDS_Button_Delete %>" CommandName="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
              <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
