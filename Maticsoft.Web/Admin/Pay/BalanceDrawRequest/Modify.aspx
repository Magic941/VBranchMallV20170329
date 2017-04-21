<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.Pay.BalanceDrawRequest.Modify" Title="修改页" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {   
             if ($('[id$="hidColse"]').val() == "close") {
                 setTimeout(function () {
                     javascript: parent.$.colorbox.close();
                 }, 1000);
             }
         });
  
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="hidColse" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="提现信息编辑" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="提现信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="3" cellPadding="3" width="100%" border="0">
	<tr>
	<td class="td_class">
		请求时间
	：</td>
	<td height="25"  >
	     <asp:Label id="lblRequestTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		金额
	：</td>
	<td height="25"  >
	     <asp:Label id="lblAmount" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		申请人
	：</td>
	<td height="25"  >
	    <asp:Label id="lblUserID" runat="server"></asp:Label>
	</td></tr>
    <tr>
	<td class="td_class">
		帐号
	：</td>
	<td height="25" >
	     <asp:Label id="lblBankCard" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		帐号类型
	：</td>
	<td height="25"  >
        <asp:RadioButtonList ID="radioCardType" runat="server" Enabled="false" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1">银行帐号</asp:ListItem>
            <asp:ListItem Value="2">支付宝帐号</asp:ListItem>
        </asp:RadioButtonList>
	</td></tr>
	<tr>
	<td class="td_class">
		开户姓名
	：</td>
	<td height="25" >
	     <asp:Label id="lblTrueName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td_class">
		银行名称
	：</td>
	<td height="25"  >
	     <asp:Label id="lblBankName" runat="server"></asp:Label>
	</td></tr>	
	<tr>
	<td class="td_class">
		状态
	：</td>
	<td height="25"  >
	     <asp:DropDownList ID="dropStatus" runat="server" Enabled="false">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">未审核</asp:ListItem>
                    <asp:ListItem Value="2">审核失败</asp:ListItem>
                    <asp:ListItem Value="3">审核通过</asp:ListItem>
                </asp:DropDownList>
	</td></tr>
	<tr>
	<td class="td_class">
		备注
	：</td>
	<td height="25">
		<asp:TextBox id="txtRemark" runat="server" Width="200px" Height="51px" 
            TextMode="MultiLine"  ></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
