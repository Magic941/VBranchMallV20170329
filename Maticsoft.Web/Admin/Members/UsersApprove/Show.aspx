<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UsersApprove.Show"
    Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        用户实名认证详细信息
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        查看用户实名认证详细信息
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display:none;">
                            <td class="td_class">
                                认证ID ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblApproveID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                认证用户：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                真实姓名 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTrueName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                身份证号码 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblIDCardNum" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                身份证正面照 ：
                            </td>
                            <td height="25">
                             <asp:Image ID="ImageFrontView" runat="server" Width="80" Height="60" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                身份证背面照 ：
                            </td>
                            <td height="25">
                                <asp:Image ID="ImageRearView" runat="server"  Width="80" Height="60" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                身份证过期时间 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblDueDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                审核状态：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                审核人：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblApproveUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                用户类型 0：会员 1 ：商户 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                提交认证日期 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                审核时间 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblApproveDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
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