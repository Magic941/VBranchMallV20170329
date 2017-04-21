<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shippers.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="发货人信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="发货人信息" />接查看
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="3" cellPadding="3 width="100%" border="0">
	<tr>
	 <td  class="td_class" >
		发货人ID
	：</td>
	<td height="25" >
		<asp:Label id="lblShipperId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		是否默认
	：</td>
	<td height="25" >
		<asp:Label id="lblIsDefault" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		发货人标签
	：</td>
	<td height="25" >
		<asp:Label id="lblShipperTag" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		发货人名称
	：</td>
	<td height="25" >
		<asp:Label id="lblShipperName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		区域
	：</td>
	<td height="25" >
		<asp:Label id="lblRegionId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td >
		地址
	：</td>
	<td height="25" >
		<asp:Label id="lblAddress" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td >
		手机
	：</td>
	<td height="25" >
		<asp:Label id="lblCellPhone" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		电话
	：</td>
	<td height="25" >
		<asp:Label id="lblTelPhone" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		邮编
	：</td>
	<td height="25" >
		<asp:Label id="lblZipcode" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td  class="td_class">
		备注
	：</td>
	<td height="25" >
		<asp:Label id="lblRemark" runat="server"></asp:Label>
	</td></tr>
</table>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
