<%@ Page Title="移动设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Config.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Setting.Config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .PostPerson
        {
            width: 100px;
        }
        .PostDate
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信设置 " />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="设置微信的相关功能配置" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end-->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px; height: 160px">
            <tr>
                <td width="180" bgcolor="#FFFFFF" class="newstitlebody" colspan="2">
                    <span class="newstitle">公众号接口设置</span>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    接口URL：
                </td>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" Text="" Width="400" Height="21" ReadOnly="True" />
                    <span style="color: grey">请将此值填写到微信公众平台接口配置URL文本框中 </span>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    接口Token：
                </td>
                <td>
                    <asp:TextBox ID="txtToken" runat="server" Text="" Width="400" Height="21" />
                    <span style="color: grey">请将此值填写到微信公众平台接口配置Token文本框中 </span>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    原始ID：
                </td>
                <td>
                    <asp:TextBox ID="txtWeChatOriginalId" runat="server" Width="400" Height="21"></asp:TextBox>
                    请输入自己的微信帐号的原始ID&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    AppId：
                </td>
                <td>
                    <asp:TextBox ID="txtWeChatAppId" runat="server" Width="400" Height="21"></asp:TextBox>
                 （该信息只有服务号下或订阅号认证后才需填写）&nbsp;&nbsp;<a href="https://mp.weixin.qq.com/cgi-bin/loginpage?t=wxm2-login&lang=zh_CN?s=maticsoft"
                        target="_blank" style="color: Blue"> 点击申请</a>
                      
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    AppSercet：
                </td>
                <td>
                    <asp:TextBox ID="txtWeChatAppSercet" runat="server" Width="400" Height="21"></asp:TextBox>
                    （该信息只有服务号下或订阅号认证后才需填写）
                </td>
            </tr>
            <tr>
                <td width="100" height="30">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnSaveOpenId_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px;" >
            <tr>
                <td width="180" bgcolor="#FFFFFF" class="newstitlebody" colspan="2">
                    <span class="newstitle">公众号功能设置</span>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                  
                </td>
                <td>
                    <asp:CheckBox ID="chkApprove" runat="server"  AutoPostBack="true" OnCheckedChanged="Check_Changed"/>
                    是否认证，并具有高级接口&nbsp;&nbsp;
                    <%-- <asp:CheckBox ID="chkHideToolbar" runat="server" Checked="true" />
                   隐藏网页底部导航栏&nbsp;&nbsp;--%>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    高级功能：
                </td>
                <td>
                    <asp:CheckBox ID="chkWeChatLogin" runat="server"  Enabled="false" />
                    开启微信自动登录&nbsp;&nbsp;
                     <asp:CheckBox ID="chkWeChatTransfer" runat="server"  Enabled="false" />
                    开启微信多客服&nbsp;&nbsp;
                    <%-- <asp:CheckBox ID="chkHideToolbar" runat="server" Checked="true" />
                   隐藏网页底部导航栏&nbsp;&nbsp;--%>
                </td>
            </tr>
            <tr>
              <td class="td_class">
                    相关功能：
                </td>
                <td>
                  <asp:CheckBox ID="chkHideOptionMenu" runat="server" Checked="true" />
                    隐藏网页右上角按钮&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    任务消息推送：
                </td>
                <td>
                    <asp:RadioButtonList ID="rblTaskMsg" runat="server" RepeatDirection="Horizontal"
                        align="left">
                        <asp:ListItem Value="0" Text="关闭" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="开启任务消息推送"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>
                 <tr>
                <td width="100" height="30">
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnSaveOther_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $('.OnlyNum').OnlyNum();
    </script>
    <script type="text/javascript">
        $('textarea').autosize();
        $.dynatextarea($('[id$=txtContent]'), 984, $('#progressbar1'));
        $(".OnlyNum").OnlyNum();

        if ($('[id$=imgResult]').attr('src')) {
            $('#Result').show();
        }
    </script>
    <script type="text/javascript">
        $('.qrImg:eq(0)').attr('src', '/Upload/QR/website.png?r=' + Math.random());
        $('.qrImg:eq(1)').attr('src', '/Upload/QR/android.png?r=' + Math.random());
        function qrImgErr(sender) {
            $(sender).hide().parent().hide();
            if ($('.qrImg:visible').length == 0) {
                // $('#QRIMG').hide();
            }
        }
    </script>
</asp:Content>
