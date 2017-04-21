<%@ Page Title="APP信息推送" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="BaiDuPush.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Push.BaiDuPush" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="APP信息推送" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以发送信息到手机APP端" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title">
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table2">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    ApiKey：
                </td>
                <td>
                    <asp:TextBox ID="txtApiKey" runat="server" Text="未设置" Enabled="False" Width="400" Height="21"/>
                    <a style="color: Blue" href="/admin/Settings/APIConfig.aspx">点此修改</a>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    SecretKey：
                </td>
                <td>
                    <asp:TextBox ID="txtSecretKey" runat="server" Text="未设置" Enabled="False" Width="400" Height="21"/>
                </td>
            </tr>
        </table>
        
        </div>
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    设备范围：
                </td>
                <td>
                    <input type="checkbox" checked="checked" disabled="disabled" id="chkDrive" /><label for="chkDrive">Andorid</label>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    推送范围：
                </td>
                <td>
                    <input type="radio" checked="checked" id="rdoScope" /><label for="rdoScope">所有人</label>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    发送时间：
                </td>
                <td>
                    <input type="radio" checked="checked" id="rdoTime" /><label for="rdoTime">即时发送</label>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    推送内容：
                </td>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" Width="450" Height="21" MaxLength="40"></asp:TextBox>
                    (40个字符)
                </td>
            </tr>
        </table>
        <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSend" runat="server" Text="发送" class="adminsubmit_short" OnClick="btnSend_Click"></asp:Button>
                                <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" class="adminsubmit_short" OnClientClick="window.location.reload();return false;"></asp:Button>
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
