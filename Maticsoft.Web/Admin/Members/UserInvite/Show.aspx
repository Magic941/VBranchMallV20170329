<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserInvite.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="3" cellpadding="3" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="邀请信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="邀请信息" />接查看
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
		邀请ID
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblInviteId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		被邀请者ID
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblUserId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		被邀请用户昵称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblUserNick" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		邀请用户ID
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblInviteUserId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		邀请用户昵称
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblInviteNick" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否已返利
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblIsRebate" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否是新用户
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblIsNew" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		创建时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCreatedDate" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		备注：奖励情况
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRemark" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		返利情况
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRebateDesc" runat="server"></asp:Label>
	</td></tr>
      <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                   
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        class="adminsubmit_short" OnClick="btnCancle_Click" OnClientClick=" javascript: parent.$.colorbox.close();"></asp:Button>
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
