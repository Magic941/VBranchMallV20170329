<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Categories.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="专辑类型信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="专辑类型信息" />接查看
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		CategoryId
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCategoryId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		描述
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblDescription" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		父级id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblParentID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		路径
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPath" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		深度
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblDepth" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		顺序
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSequence" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否有子级
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblHasChildren" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否是菜单
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblIsMenu" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		类型
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblType" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		菜单是否显示
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblMenuIsShow" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		菜单显示的顺序
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblMenuSequence" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		创建者用户名
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCreatedUserID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		日期
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCreatedDate" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblStatus" runat="server"></asp:Label>
	</td></tr>
</table>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
