<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Consultations.Show"
    Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="会员咨询信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        查看会员详细咨询信息
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
                                咨询ID ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblConsultationId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="display:none;">
                                咨询人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                咨询人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                咨询产品 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblProductId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                咨询人邮箱 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                咨询内容 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblConsultationText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                咨询时间 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                回复时间 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblReplyDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">

                                <asp:CheckBox ID="chkIsReply" runat="server" Text="是否回复"  Enabled="false"/>
                                <asp:CheckBox ID="chkIsStatus" runat="server" Text="是否审核"  Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                回复内容 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblReplyText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                回复人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblReplyUserId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="display:none;">
                            <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
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