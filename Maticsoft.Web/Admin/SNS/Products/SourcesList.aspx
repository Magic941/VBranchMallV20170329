<%@ Page Title="商品来源表" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="SourcesList.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.ProductSources.List" %>

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
                         <asp:Literal ID="Literal1" runat="server" Text="商品来源表" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除 <asp:Literal ID="Literal3" runat="server" Text="商品来源表" />
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
                    OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <li id="liAdd" runat="server" style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">添加</a> <b>|</b> </li>
                <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;">删除</a><b>|</b></li>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
        <columns>
                            
		<asp:BoundField DataField="WebSiteName" HeaderText="商品来源网站的名称" SortExpression="WebSiteName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="WebSiteUrl" HeaderText="商品来源网站的url" SortExpression="WebSiteUrl" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="WebSiteLogo" HeaderText="网站的log,在单品也链接到此" SortExpression="WebSiteLogo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="CategoryTags" HeaderText="采集时商品类别匹配的正则表达式" SortExpression="CategoryTags" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="PriceTags" HeaderText="采集时商品价格匹配的正则表达式" SortExpression="PriceTags" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ImagesTag" HeaderText="采集时图片匹配的正则表达式" SortExpression="ImagesTag" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center"  /> 
                           
                            
                            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                                Text="<%$ Resources:Site, btnDetailText %>"  ItemStyle-HorizontalAlign="Center" />
                            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                                Text="<%$ Resources:Site, btnEditText %>"  ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"   Visible="false"  ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
        <HeaderStyle Height="35px" />
        <pagerstyle height="25px" horizontalalign="Right" />
        <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
        <rowstyle height="25px" />
        <sortdirectionstr>DESC</sortdirectionstr>
    </cc1:GridViewEx>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
            </td>
        </tr>
    </table></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
