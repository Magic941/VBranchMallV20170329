<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="Shop_ProductTypes信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="Shop_ProductTypes信息" />接查看
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
		TypeId
	：</td>
	<td height="25" >
		<asp:Label id="lblTypeId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		TypeName
	：</td>
	<td height="25" >
		<asp:Label id="lblTypeName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		Remark
	：</td>
	<td height="25" >
		<asp:Label id="lblRemark" runat="server"></asp:Label>
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
