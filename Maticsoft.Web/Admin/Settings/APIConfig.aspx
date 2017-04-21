<%@ Page Title="接口设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="APIConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.APIConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        开放接口设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        设置第三方的开放接口信息，实现更多的扩展功能。
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%; padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <div class="newsadd_title" style="display: none;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    三方登录设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        三方登录ID：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDengluAPPID" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的APIID&nbsp;&nbsp;<a href="http://open.denglu.cc/application/applicationNew?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        三方登录Key：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDengluAPIKEY" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的APIKEY&nbsp;&nbsp; <a href="http://open.denglu.cc/application/applicationNew?s=maticsoft"
                                            target="_blank" style="color: Blue">点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        说明：
                                    </td>
                                    <td style="color: Gray">
                                        回调地址： denglu.cc上申请的Key时，需要填写回调地址，回调地址请填写<span style="color: Green"> http://您的域名/Account/Redirect
                                        </span>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td style="color: Gray">
                                        转发地址：有些第三方平台需要填写转发地址才能使用，转发地址请填写 <span style="color: Green">http://您的域名/Account/Receiver
                                        </span>绑定地址可填写与回调地址一样。
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divTaoke" style="<%=TaokeStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    淘宝客接口
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        淘宝客AppKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAppKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的淘宝AppKey&nbsp;&nbsp;<a href="http://my.open.taobao.com/common/createApp.htm?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        淘宝客Appsecret：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAppsecret" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的淘宝AppSecret
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        淘宝客ApiUrl：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtApiUrl" runat="server" Width="400" Height="21"></asp:TextBox>
                                        非沙箱测试，则默认保持不变。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        是否开启：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkIsOpenR" runat="server" />
                                        商品链接采用隐藏淘宝客链接方式（注：开启隐藏链接后，淘点金代码将不再起作用。）
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divShopTao" style="<%=ShopTaoStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    淘宝卖家接口
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        淘宝卖家AppKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShopAppKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的淘宝卖家AppKey&nbsp;&nbsp;<a href="http://my.open.taobao.com/common/createApp.htm?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        淘宝卖家Appsecret：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShopAppsecret" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的淘宝AppSecret
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        淘宝卖家ApiUrl：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtShopApiUrl" runat="server" Width="400" Height="21"></asp:TextBox>
                                        非沙箱测试，则默认保持不变。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        淘宝卖家Callback：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtTaoCallback" runat="server" Width="400" Height="21" ReadOnly="True"></asp:TextBox>
                                        淘宝卖家应用回调URL，请将此值复制到淘宝应用“回调URL”中。
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divTaoCode" style="<%=TaoCodeStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    淘点金推广代码
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtTaoCode" runat="server" TextMode="MultiLine" Rows="5" Width="400"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top;">
                                        淘点金推广是一个傻瓜式的淘宝客推广工具，站长只需要在网站上部署点金推广代码，就可以帮助站长将网站上的各种普通淘宝链接转换成可推广的淘宝客链接获得增值收益。
                                        <br />
                                        <a href="http://www.alimama.com/?s=maticsoft" target="_blank" style="color: Blue">点击申请</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divSina" style="<%=SinaStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    新浪微博设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        新浪AppKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSinaAppKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的新浪AppKey&nbsp;&nbsp;<a href="http://open.weibo.com/development?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        新浪AppSercet：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSinaAppSercet" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的新浪AppSercet
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        新浪CallBack：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSinaCallBack" runat="server" Width="400" Height="21" Text=""></asp:TextBox>
                                        请将此值填复制到新浪开发者中心的回调地址文本框中。
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divQQ" style="<%=QQStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    腾讯QQ设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        QQAPPID：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQQAPPID" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的QQAPPID&nbsp;&nbsp;<a href="http://connect.qq.com/intro/login?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        QQAPPKEY：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQQAPPKEY" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的QQAPPKEY
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="newsadd_title" id="divTencent" style="<%=TencentStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    腾讯微博登录设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        TencentAppId：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTencentAppId" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的TencentAppId&nbsp;&nbsp;<a href="http://dev.t.qq.com/developer?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        TencentSercet：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTencentSercet" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的TencentSercet
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                    

                    <div class="newsadd_title" id="divBaidu" style="<%=BaiduStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    百度云推送设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        ApiKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBaiDuPushApiKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的ApiKey&nbsp;&nbsp;<a href="http://developer.baidu.com/cloud/push?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        SecretKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBaiDuPushSecretKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                        请输入自己申请的SecretKey
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="newsadd_title" id="divVideo" style="<%=VideoStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    视频接口设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        优酷视频API：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtYouKuAPI" runat="server" Width="400" Height="21"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="newsadd_title" id="div1" style="<%=ExpressStr%>">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    物流接口
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        物流APIKey：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpressKey" runat="server" Width="400" Height="21"></asp:TextBox>
                                              请输入自己申请的物流APIKey&nbsp;&nbsp;<a href="http://www.kuaidi100.com/openapi/applyapi.shtml?s=maticsoft"
                                            target="_blank" style="color: Blue"> 点击申请</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                        class="border">
                        <tr>
                            <td class="tdbg">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="td_class">
                                        </td>
                                        <td height="25">
                                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                                class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                            <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>"
                                                class="adminsubmit_short" OnClick="btnReset_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
