

<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
     CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Members.Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看会员详细信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以查看会员的详细信息
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
                                头像 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Image ID="imageGra" runat="server" Width="142" Height="142" />
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
                        <tr>
                            <td class="td_class">
                                真实姓名 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTrueName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                性别 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblSex" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系电话 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系地址 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                邮箱 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                账户状态 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblActivity" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                积分 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPoints" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                粉丝数 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblFans" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                专辑数 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblAblums" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                关注数 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblFellows" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                               喜欢数 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblFav" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                                分享商品数 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblProducts" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                注册日期 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblCreTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                最后登录日期 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblLoginDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnBackText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
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
