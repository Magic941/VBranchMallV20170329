<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="ModifyPoints.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.MembershipManage.ModifyPoints"
    Title="积分充值" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="积分充值" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对会员的积分进行充值
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr style="display: none;">
                            <td class="td_class">
                                ID ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                用户名 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblUserName" Text="Admin" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                昵称 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblNickName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                真实姓名 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTrueName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                联系电话 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                邮箱 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                当前积分 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPoints" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                余额 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                充值积分 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtPoints" runat="server" CssClass="OnlyNumber"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnRecharge" runat="server" CausesValidation="false" Text="充值"
                                    class="adminsubmit_short" OnClick="btnPoints_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript" language="javascript">
        $(".OnlyNum").OnlyNum();
    </script>
</asp:Content>
