<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.ProductAccessories.Modify" Title="修改页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="Shop_ProductAccessories编辑" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="Shop_ProductAccessories" />
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
		<asp:Literal ID="literName" runat="server" Text="组合名称" />：</td>
	<td height="25">
		<asp:TextBox id="txtName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr style="display:none;">
	<td class="td_class">
		最大购买量
	：</td>
	<td height="25">
		<asp:TextBox id="txtMaxQuantity" runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr  style="display:none;">
	<td class="td_class">
		最小购买量
	：</td>
	<td height="25">
		<asp:TextBox id="txtMinQuantity"  runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr style="display:none;">
	<td class="td_class" >
		优惠类型
	：</td>
	<td height="25">
        <asp:RadioButtonList ID="RadioDiscountType" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1" Selected="Selected">优惠金额</asp:ListItem>
            <asp:ListItem Value="2">优惠折扣</asp:ListItem>
        </asp:RadioButtonList>
	</td></tr>
	<tr visible="false"  runat="server" id="trDiscountAmount" >
	<td class="td_class">
		优惠价      
	：</td>
	<td height="25">
		<asp:TextBox id="txtDiscountAmount" runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
	<tr visible="false"   runat="server" id="trStock">
	<td class="td_class">
		库存  
	：</td>
	<td height="25">
		<asp:TextBox id="txtStock" runat="server" Width="200px">0</asp:TextBox>
	</td></tr>
    <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                        class="adminsubmit_short"  OnClick="btnSave_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        class="adminsubmit_short" OnClick="btnCancle_Click" OnClientClick=" javascript: parent.$.colorbox.close();"></asp:Button>
                                </td>
                            </tr>
</table>

                </td>
            </tr>
        </table>
    </div>
    <div  >
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr  >
                        <td id="Td4" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                          <iframe width="95%" height="649px" frameborder="0" src="SelectAccessorieNew.aspx?id=<%=AccessoriesId %>&pid=<%=ProductId %>&acctype=<%=Type %>">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
