<%@ Page Title="快递单模板管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ExpressTemplates.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ExpressTemplate.ExpressTemplates" %>

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
                        <asp:Literal ID="Literal3" runat="server" Text="快递单模板管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以对快递单模板进行管理" />
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
                    &nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server" Text="快递名称" />：
                    <asp:TextBox ID="txtExpressName" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Literal ID="LiteralShippingStatus" runat="server" Text="状态" />：
                    <asp:DropDownList ID="dropShippingStatus" runat="server">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="1">启用</asp:ListItem>
                        <asp:ListItem Value="0">禁用</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="False" DataKeyNames="ExpressId" OnRowCommand="gridView_RowCommand">
        <columns>
                   
        <asp:BoundField DataField="ExpressId" ItemStyle-Width="50"  HeaderText="单据编号" SortExpression="ExpressId" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ExpressName" HeaderText="单据名称" SortExpression="ExpressName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField Visible="False" DataField="XmlFile" HeaderText="XmlFile" SortExpression="XmlFile" ItemStyle-HorizontalAlign="Center"  /> 
                           <asp:TemplateField HeaderText="是否启用" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkUse" runat="server" CausesValidation="False" CommandName="Use" CommandArgument='<%# Eval("ExpressId") %>' Text='<%# GetboolText(Eval("IsUse").ToString())%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:TemplateField   HeaderText="<%$ Resources:Site, lblOperation %>"   ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkModify" 
                                    PostBackUrl='<%# "Modify.aspx?ExpressId=" + Eval("ExpressId") + "&ExpressName=" + Maticsoft.Common.Globals.UrlEncode((string)Eval("ExpressName")) + "&XmlFile=" + Eval("XmlFile")%>'
                                    runat="server" CausesValidation="False" CommandName="Modify"
                                         Text="编辑"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCopy" 
                                    PostBackUrl='<%# "Copy.aspx?ExpressName=" + Maticsoft.Common.Globals.UrlEncode((string)Eval("ExpressName")) + "&XmlFile=" + Eval("XmlFile")%>'
                                    runat="server" CausesValidation="False" CommandName="Copy"
                                         Text="添加相似单据"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除吗？"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
         <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="35px" /> 
        <sortdirectionstr>DESC</sortdirectionstr>
    </cc1:GridViewEx>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
