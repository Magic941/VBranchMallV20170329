<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shippers.Modify" Title="修改页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Region" Src="~/Controls/Region.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="编辑发货人信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="编辑发货人信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="3" cellPadding="3" width="100%" border="0">
	 
 
	<tr>
	<td class="td_class"  >
		发货人标签
	：</td>
	<td  height="25" >
		<asp:TextBox id="txtShipperTag" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td  class="td_class" >
		发货人名称
	：</td>
	<td  height="25">
		<asp:TextBox id="txtShipperName" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
	</td></tr>
	 <tr>
                                    <td class="td_class">
                                        所在地 ：
                                    </td>
                                    <td height="25">
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
                                    <uc1:Region ID="RegionID" runat="server"  VisibleAll ="true"  VisibleAllText="--请选择--" />
                                    </contenttemplate>
                    </asp:UpdatePanel>
                                    </td>
                                </tr>
	<tr>
	<td  class="td_class" >
		地址
	：</td>
	<td height="25">
		<asp:TextBox id="txtAddress" MaxLength="300" runat="server" Width="400px"></asp:TextBox>
	</td></tr>
	<tr>
	<td  class="td_class">
		手机
	：</td>
	<td height="25">
		<asp:TextBox id="txtCellPhone" MaxLength="20" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td  class="td_class"  >
		电话
	：</td>
	<td height="25">
		<asp:TextBox id="txtTelPhone" MaxLength="20"  runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td  class="td_class"  >
		邮编
	：</td>
	<td height="25">
		<asp:TextBox id="txtZipcode" MaxLength="6"  runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td  class="td_class"  >
		备注
	：</td>
	<td height="25">
		<asp:TextBox id="txtRemark"  MaxLength="300"   Height="200px" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
	</td></tr>
     <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                        class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                </td>
            </tr>
</table>

                </td>
            </tr>
        </table></div>
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
   <script type="text/javascript">
       $(function () {
           $("[id$=txtCellPhone]").OnlyNum();
           $("[id$=txtZipcode]").OnlyNum();
       });
   </script>
</asp:Content>
 
