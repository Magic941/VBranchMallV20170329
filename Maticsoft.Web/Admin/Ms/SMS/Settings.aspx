
<%@ Page Title="短信接口设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="Settings.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.SMS.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .td_width {
            width: 245px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="thumbList" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        短信接口信息设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        设置发送短信的接口的相关信息。
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%; padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <div class="newsadd_title">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    短信接口信息设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr class="webprop">
                                    <td class="td_class">
                                        软件序列号：
                                    </td>
                                    <td width="360" class="td_width">
                                        <asp:TextBox ID="txtSerialNo" runat="server" Width="240" Height="21"></asp:TextBox>
                                    </td>
                                    <td rowspan="3" style="float: left">
                                            申请短信接口<a href="http://maticsoft.com/contact.aspx"
                                                style="color: blue" target="_blank">请联系</a>
                                    </td>
                                </tr>
                                <tr class="webprop">
                                    <td class="td_class" >
                                        自定义关键字：
                                    </td>
                                    <td class="td_width">
                                        <asp:TextBox ID="txtKey" runat="server" Width="240" Height="21"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="webprop">
                                    <td class="td_class">
                                        序列号密码：
                                    </td>
                                    <td style="color: Gray" class="td_width">
                                        <asp:TextBox ID="txtPassword" runat="server" Width="240" Height="21" TextMode="Password" ></asp:TextBox>
                                    </td>
                                    <td>
                                        密码框不填表示不修改密码。
                                    </td>
                                </tr>
                                <tr class="webprop">
                                    <td class="td_class">
                                        是否启用：
                                    </td>
                                    <td  class="td_width">
                                      <asp:CheckBox ID="chkOpen" runat="server"  Checked="True"/>是
                                    </td>
                                    <td>
                                           <span style="color: gray">勾选将会启动发送短信机制，不勾选将不会启用发送短信</span>
                                    </td>
                                </tr>
                                    <tr class="webprop">
                                    <td class="td_class">
                                        短信内容：
                                    </td>
                                    <td class="td_width">
                                           <asp:TextBox ID="txtSMSContent" runat="server" Width="240"  TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </td>
                                    <td>
                                           <span style="color: gray">关键字{SMSCode}表示效验码</span>
                                    </td>
                                </tr>
                                         <tr>
                                        <td class="td_class">
                                        </td>
                                        <td height="25">
                                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                                class="adminsubmit_short" OnClick="btnSave_Click"  ></asp:Button>
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    </div>
                    
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

