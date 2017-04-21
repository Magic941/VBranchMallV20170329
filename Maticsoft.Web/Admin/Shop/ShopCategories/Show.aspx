<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ShopCategories.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="ShopCategories信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="ShopCategories信息" />接查看
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td class="td_class">
		CategoryId
	：</td>
	<td height="25" >
		<asp:Label id="lblCategoryId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Name
	：</td>
	<td height="25" >
		<asp:Label id="lblName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		DisplaySequence
	：</td>
	<td height="25" >
		<asp:Label id="lblDisplaySequence" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Meta_Description
	：</td>
	<td height="25" >
		<asp:Label id="lblMeta_Description" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Meta_Keywords
	：</td>
	<td height="25" >
		<asp:Label id="lblMeta_Keywords" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Description
	：</td>
	<td height="25" >
		<asp:Label id="lblDescription" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		ParentCategoryId
	：</td>
	<td height="25" >
		<asp:Label id="lblParentCategoryId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Depth
	：</td>
	<td height="25" >
		<asp:Label id="lblDepth" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Path
	：</td>
	<td height="25" >
		<asp:Label id="lblPath" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		RewriteName
	：</td>
	<td height="25" >
		<asp:Label id="lblRewriteName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		SKUPrefix
	：</td>
	<td height="25" >
		<asp:Label id="lblSKUPrefix" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		AssociatedProductType
	：</td>
	<td height="25" >
		<asp:Label id="lblAssociatedProductType" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Notes1
	：</td>
	<td height="25" >
		<asp:Label id="lblNotes1" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Notes2
	：</td>
	<td height="25" >
		<asp:Label id="lblNotes2" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Notes3
	：</td>
	<td height="25" >
		<asp:Label id="lblNotes3" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Notes4
	：</td>
	<td height="25" >
		<asp:Label id="lblNotes4" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Notes5
	：</td>
	<td height="25" >
		<asp:Label id="lblNotes5" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Theme
	：</td>
	<td height="25" >
		<asp:Label id="lblTheme" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		HasChildren
	：</td>
	<td height="25" >
		<asp:Label id="lblHasChildren" runat="server"></asp:Label>
	</td></tr>
	<tr><td class="td_class"></td>
			<td height="25"><asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>" class="adminsubmit" OnClick="btnCancle_Click"></asp:Button></td></tr>
</table>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
