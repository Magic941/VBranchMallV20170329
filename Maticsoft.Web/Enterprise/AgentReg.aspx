<%@ Page Title="" Language="C#"  EnableEventValidation="false" AutoEventWireup="true"
    CodeBehind="AgentReg.aspx.cs" Inherits="Maticsoft.Web.Enterprise.AgentReg" %>

<%@ Register src="../Controls/RegionDropList.ascx" tagname="RegionDropList" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/css/base.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
     <style type="text/css">
        /* css reset */
        body{color:#000;background:#fff;font-size:12px;line-height:166.6%;text-align:center;}
        body,input,select,button{font-family:verdana}
        h1,h2,h3,select,input,button{font-size:100%}
        body,h1,h2,h3,ul,li,form,p,img{margin:0;padding:0;border:0}
        input,button,select,img{margin:0;line-height:normal}
        select{padding:1px}
        ul{list-style:none}
        select,input,button,button img,label{vertical-align:middle}
        header,footer,section,aside,nav,hgroup,figure,figcaption{display:block;margin:0;padding:0;border:none}
        a{text-decoration:none;color:#848585}
        a:hover{color:#000}
        .fontWeight{font-weight:700;}
        /* global */
        .unvisi{visibility:hidden}
        /* backgroundImage */
        .headerIntro,
        .loginIcoCurrent,
        .loginFuncNormal,
        .loginFuncMobile,
        .loginIcoNew,
        .themeText li,
        .domain,
        .whatAutologin,
        .btn,
        .dialogbox .hd .rc,
        .dialogbox .hd,
        .btn-moblogin,
        .btn-moblogin2,
        .loginFormIpt-over .loginFormTdIpt,
        .loginFormIpt-focus .loginFormTdIpt,
        .ico,
        .ext,
        .locationTestTitle,
        .verSelected,
        .servSelected,
        .locationTestTitleClose,
        #extText li,
        #extMobLogin li,
        #mobtips_arr,
        #mobtips_close{background-image:url("/images/bg_v5.png")}
        .headerLogo,
        .headerIntro,
        .headerNav,
        #headerEff,
        .footerLogo,
        .footerNav,
        .loginIcoCurrent,
        .loginIcoNew,
        .loginFormTh,
        .loginFormTdIpt,
        .domain,
        #loginFormSelect,
        #whatAutologinTip,
        #mobtips,
        #mobtips_arr,
        #mobtips_close{position:absolute}
        /* ico */
        .ico-arr{display:inline-block;width:7px;height:12px;vertical-align:baseline;background-position:-160px -112px;}
        .ico-arr-d{background-position:-160px -110px;}
        .loginFormConf a:hover .ico-arr-d,
        .ico-arr-d-focus{background-position:-176px -110px;}
        *+html .ico-arr-d{background-position:-160px -112px;}
        *+html .loginFormConf a:hover .ico-arr-d,
        *+html .ico-arr-d-focus{background-position:-176px -112px;}
        /* header */
        .header{width:800px;height:64px;position:relative;margin:0 auto;z-index:2;overflow:hidden;}
        .headerLogo{top:17px;left:0px}
        .headerIntro{height:28px;width:144px;display:block;background-position:0 -64px;top:17px;left:144px}
        .headerNav{top:21px;right:0px;width:300px;text-align:right}
        .headerNav a{margin-left:13px}
        #headerEff{}
        /* main */
        .main{height:440px;overflow:hidden;margin:0 auto;background:#fff;}
        .main-inner{width:900px;height:440px;overflow:visible;margin:0 auto;position:relative;clear:both}
        #theme{height:440px;width:900px;position:absolute;overflow:hidden;z-index:1;background-position:top right;background-repeat:no-repeat;text-align:left;top:0;left:0;}
        .themeLink{height:274px;width:430px;display:block;outline:0;}
        .themeText{margin-left:26px;}
        .themeText li{line-height:22px;-line-height:24px;height:24px;color:#858686;text-indent:12px;background-position:-756px -72px;background-repeat:no-repeat}
        .themeText li a{color:#005590;text-decoration:underline;}
        .login{width:338px;height:388px;overflow:hidden;float:right;margin-right:90px;margin-top:24px;background:#fff;border:1px solid #b7c2c9;_display:inline;text-align:left;position:relative;z-index:2;border-radius:2px;}
        .login,
        .unishadow{box-shadow:0px 1px 3px 0 rgba(0,0,0,0.2);-webkit-box-shadow:0px 1px 3px 0 rgba(0,0,0,0.2);-moz-box-shadow:0px 1px 3px 0 rgba(0,0,0,0.2);}
        .loginFunc{width:338px;height:49px;overflow:hidden;clear:both;}
        .loginFuncNormal,.loginFuncMobile{width:168px;height:49px;overflow:hidden;position:relative;line-height:46px;font-weight:700;border-right:1px #b7c2c9 solid;background-position:0 0;float:left;font-size:14px;text-align:center;+line-height:48px;background-repeat:repeat-x;color:#333;cursor:pointer;}
        .loginFuncMobile{background-position:0px 0px;width:169px;border-right:none;}
        .loginIcoCurrent{width:24px;height:24px;left:26px;top:9px;display:none;}
        .loginIcoNew{width:21px;height:10px;font-size:0;background-position:-684px 0;left:135px;top:12px;}
        .tab-1 .loginFuncNormal,
        .tab-2 .loginFuncMobile{background:none;}
        .tab-2 .loginFuncMobile .loginIcoCurrent,
        .tab-1 .loginFuncNormal .loginIcoCurrent,
        .tab-2 #extMobLogin,
        .tab-1 #extText,
        .tab-11 #extVerSelect,
        .tab-22 #extMobLogin2,
        .tab-2 #lfBtnReg2,
        .tab-1 #lfBtnReg1,
        .tab-2 .loginFormThMob{display:block;}
        .tab-2 #lfVerSelect,
        .tab-2 #extVerSelect,
        .tab-22 #extMobLogin,
        .tab-11 #extText,
        .tab-2 #extText,
        .tab-2 #lfBtnReg,
        .tab-1 #lfBtnReg2,
        .tab-22 #lfBtnMoblogin,
        .tab-2 .loginFormThAcc{display:none;}
        /* form */
        .loginForm{position:relative;height:339px;overflow:hidden;}
        .loginFormIpt{position:relative;height:33px;line-height:33px;margin-top:0px;margin-left:42px;clear:both;width:253px;border:1px solid #bac5d4;border-bottom-color:#d5dbe2;border-right-color:#d5dbe2;border-radius:2px;}
        .loginFormIpt-over{border-color:#a6b4c9;border-bottom-color:#bac5d4;border-right-color:#bac5d4;}
        .loginFormIpt-focus .loginFormTdIpt,
        .loginFormIpt-over .loginFormTdIpt{background-position:0 -160px;}
        .loginFormIpt-focus{border-color:#60a4e8;border-bottom-color:#84b4fc;border-right-color:#84b4fc;}
        .loginFormIpt-focus .placeholder{color:#b4c0d2;}
        .loginFormIptWiotTh{height:35px;border:none;margin-top:19px;width:255px;}
        .loginFormTh{width:36px;}
        .loginFormThMob{display:none;}
        .loginFormTdIpt{width:237px;padding:7px 8px 6px 8px;border:1px solid #838383;ime-mode:disabled;height:20px;top:0;left:0;line-height:20px;font-size:16px;font-weight:700;background-color:#eef3f8;border:none;font-family:verdana;line-height:17px;color:#92a4bf;}
        .loginFormTdIpt:focus{outline:0;}
        .loginFormTdIpt-focus{color:#333;font-weight:700;}
        .showPlaceholder .placeholder{visibility:visible;cursor:text;}
        .placeholder{color:#92a4bf;font-size:14px;text-indent:10px;position:absolute;left:0;top:0;visibility:hidden;background:none;}
        .domain{width:92px;height:33px;background-position:0 -112px;line-height:999em;overflow:hidden;display:block;right:0;top:0px;}
        .loginFormCheck{height:14px;line-height:14px;color:#555;margin-left:42px;margin-top:19px;clear:both;width:255px;position:relative;z-index:1;}
        .loginFormCheckInner{height:14px;width:150px;float:left;}
        .forgetPwdLine{height:18px;line-height:18px;margin-left:42px;clear:both;width:253px;text-align:right;margin-top:8px;}
        .forgetPwd{color:#848585;}
        .forgetPwd:hover{color:#333;}
        #loginFormSelect{width:182px;left:46px;top:6px;}
        .loginFormCbx{width:13px;height:13px;padding:0;overflow:hidden;margin:0;}
        .loginFormSslText{font-family:simsun}
        .whatAutologin{display:inline-block;vertical-align:top;width:14px;height:14px;background-position:-112px -112px;line-height:999em;overflow:hidden}
        #whatAutologinTip{z-index:9; width:180px; height:36px;background-color:#fffcd1; border:1px #f1d47c solid; left:0px;top:16px;text-align:left; padding:5px;line-height:18px; color:#de6907;display:none;}
        .btn{float:left;height:35px;text-align:center;cursor:pointer;border:0;padding:0;font-weight:700;font-size:14px;display:inline-block;vertical-align:baseline;line-height:35px;outline:0;}
        .btn-login{width:102px;background-position:0 -208px;color:#fff;}
        .btn-login-hover{background-position:0 -256px;}
        .btn-login-active{background-position:0 -304px}
        .btn-reg{width:102px;background-position:-112px -208px;color:#555;float:right;}
        .btn-reg:hover{color:#555}
        .btn-reg-hover{background-position:-112px -256px;color:#555}
        .btn-reg-active{background-position:-112px -304px;color:#555}
        .btn-moblogin2{width:202px;height:37px;text-align:center;font-size:14px;background-position:-396px -288px;background-color:#fff;margin-top:30px;float:none;margin-left:46px;}
        .loginFormConf{height:12px;line-height:12px;margin-left:42px;margin-top:35px;clear:both;width:255px;position:relative;color:#848585;z-index:1;}
        .loginFormVer{float:left;width:130px;}
        .loginFormService{float:right:width:120px;text-align:right;}
        .loginFormVerList{width:81px;position:absolute;padding:1px;background:#fff;border:1px solid #b7c2c9;top:-5px;top:-4px\9;left:33px;display:none;}
        .loginFormVerList li a{height:22px;line-height:22px;width:81px;overflow:hidden;color:#848585;display:block;text-indent:22px;}
        .loginFormVerList li a:hover{background-color:#eef3f8;}
        .loginFormVerList li a.verSelected{background-position:-250px -58px;background-repeat:no-repeat;color:#333;}
        /* ext */
        #extVerSelect,#extText,#extMobLogin,#extMobLogin2{display:none;}
        .ext{width:100%;border:1px solid #f1f3f5;height:62px;background-position:0 -448px;background-repeat:repeat-x;position:absolute;bottom:0;}
        #extText{margin:15px 0 0 42px;line-height:12px;}
        #extText li{background-position:-240px -123px;background-repeat:no-repeat;padding-left:7px;color:#9bacc6;margin-bottom:9px;}
        #extText li a{color:#9bacc6;}
        #extText li a:hover{color:#5d8dc8;}
        #extMobLogin{padding-left:42px;}
        #extMobLogin li{margin-bottom:9px;padding-left:7px;color:#848585;height:12px;line-height:12px;background-position:-240px -107px;background-repeat:no-repeat}
        #extMobLogin h3{color:#555;font-size:12px;margin:16px 0 11px;height:14px;line-height:14px;}
        #extVerSelect{height:66px;line-height:66px;font-size:14px;text-align:center;font-weight:700;}
        #extVerSelect a{color:#005590;text-decoration:underline;}
        .setMobLoginInfo{margin-left:46px;color:#848585;margin-top:10px;}
        /* tab-2 */
        .tab-2 .loginFormConf{margin-top:22px;}
        .tab-2 .ext{height:85px;}
        .tab-2 .loginFormIptWiotTh{margin-top:15px;}
        .tab-2 .loginFormCheck{margin-top:13px;}
        /* footer */
        .footer{height:65px;overflow:hidden;margin:0 auto;background:#f7f7f7;border-top:1px solid #fff;}
        .footer-inner{width:800px;height:63px;overflow:hidden;margin:0 auto;color:#848585;position:relative}
        .footerLogo{top:11px;left:-15px}
        .footerNav{top:25px;right:0px;}
        .footerNav a{margin-left:18px}
        .copyright{margin-left:26px}
        /* noscript */
        .noscriptTitle{line-height:32px;font-size:24px;color:#d90000;padding-top:60px;font-weight:700;background:#fff;}
        .noscriptLink{text-decoration:underline;color:#005590;font-size:14px;}
        /* mobtips */
        #mobtips{height:18px;border:1px solid #c6c6a8;top:29px;left:46px;line-height:18px;background:#ffffe1;padding-left:6px;padding-right:20px;display:none;color:#565656;zoom:1;}
        #mobtips_arr{width:9px;height:9px;background-position:-684px -72px;top:-5px;left:15px;}
        #mobtips_close{background-position:-715px -68px;top:2px;width:16px;height:14px;right:0px;}
        #mobtips em{font-style:normal;color:#328721;}
        #mobtips a{text-decoration:underline;color:#005590;}
        /* mask */
        .mask{position:absolute;left:0;top:0;width:100%;height:100%;background:#000;filter:alpha(opacity=30);-moz-opacity:0.3;opacity:0.3;z-index:998}
        /* 弹框 */
        .dialogbox{position:absolute;left:0;top:0;z-index:999;width:687px;left:50%;margin-left:-343px;top:50%;margin-top:-152px;}
        .dialogbox .hd{position:relative;padding:0 10px;height:27px;line-height:27px;color:#fff;background-repeat:repeat-x;background-position:0 -576px}
        .dialogbox .hd .rc{position:absolute;top:0;width:2px;height:27px}
        .dialogbox .hd .rc-l{left:0;background-position:-720px -36px}
        .dialogbox .hd .rc-r{right:0;background-position:-722px -36px}
        .dialogbox .hd .btn-close{position:absolute;right:5px;top:5px;width:16px;height:16px;background-position:-716px 3px;line-height:9999px;overflow:hidden;font-size:0;margin-right:0;}
        .dialogbox .bd{border:1px solid #6C92AD;border-top:none;background:#fff}
        .dialogbox iframe{display:block}
        #phoneRegFrame{width:685px;height:315px}
        /* 加密http登录弹窗 */
        .enhttp .topborder,
        .enhttp .bottomborder,
        .enhttp .ct,
        .enhttp .cldenhttp,
        .enhttp .ct .inner .httplogin{background-image:url("/images/bg_httplogin.gif");background-color:transparent;background-repeat:no-repeat;text-decoration:none;}
        .enhttp{width:420px;height:270px;position:absolute;z-index:999;overflow:hidden;top:0;left:50%;margin-left:-210px;top:50%;margin-top:-135px;}
        .enhttp .topborder{width:418px;height:2px;font-size:1px;overflow:hidden;margin:0 auto;background-position:0 -108px;}
        .enhttp .bottomborder{width:418px;height:2px;font-size:1px;overflow:hidden;margin:0 auto;background-position:0 -110px;}
        .enhttp .ct{width:418px;height:266px;background-position:0 -134px;background-color:#fff;border-left:1px solid #82aecd;border-right:1px solid #82aecd;position:relative;overflow:hidden;}
        .enhttp .ct .inner{padding-top:40px;margin:0 auto;text-align:left;}
        .enhttp .ct .inner p{font-size:14px;}
        .enhttp .ct .inner .txt-tips{color:#737373;line-height:30px;width:325px;margin-left:46px;display:inline;}
        .enhttp .ct .inner .txt-normal{line-height:30px;width:325px;margin:10px 0 0 46px;}
        .enhttp .ct .inner .httplogin{font-size:14px;height:34px;width:120px;display:block;background-position:-432px -108px;line-height:34px;text-align:center;color:#fff;font-weight:700;background-color:#3486cc;}
        .enhttp .ct .inner .txt-line{width:325px;margin-left:46px;background:#b6cad9;height:1px;overflow:hidden;font-size:1px;margin-top:24px;}
        .enhttp .ct .inner .txt-advice{line-height:60px;width:325px;color:#8d8d8d;margin-left:46px;}
        .enhttp .ct .inner .txt-advicelink{margin-left:20px;font-size:14px;}
        .enhttp .cldenhttp{height:22px;width:22px;overflow:hidden;position:absolute;right:8px;top:6px;background-position:0 -112px;text-indent:-9999px;}
        .enhttp .cldenhttp:hover{background-position:-22px -112px;}
        .enhttp .enhttpbox{position:absolute;z-index:2;left:0;}
        .enhttp .httploginframe{width:100%;height:200px;position:absolute;top:2px;z-index:1;left:0;}
        /* 测速 */
        #locationTest{position:absolute;width:255px;top:-2px;left:0px;height:88px;background:#fff;border:1px solid #b7c2c9;display:;margin-bottom:200px;height:79px;overflow:hidden;display:none;}
        .locationTestTitle{width:255px;height:26px;line-height:26px;position:relative;color:#555;text-indent:10px;background-position:0 -10px;border-bottom:1px solid #f1f3f5;}
        .locationTestTitle h4{margin:0;font-size:12px;}
        .locationTestTitleClose{height:8px;width:8px;overflow:hidden;display:block;position:absolute;right:6px;top:7px;background-position:-224px -112px}
        .locationTestTitleClose:hover{background-position:-208px -112px}
        .locationTestEach{display:inline-block;width:5em;font-family:verdana;color:#848585;}
        .locationTestList li{padding:2px;float:left;display:inline-block;}
        .locationTestList .servSelected{background-position:-248px -50px;background-repeat:no-repeat;}
        .locationTestList li a{height:38px;width:80px;display:block;line-height:16px;padding-top:10px;overflow:hidden;text-align:center;color:#000;}
        .locationTestList li a:hover{background-color:#eef3f8;}
        #selectLocation{text-align:center;}
        #locationTestCur{width:3em;}
        #selectLocationTipsDone{display:none;}
        .locationTestBest{display:none;color:green;}
        .locationChoose{text-decoration:underline;color:#005590;}
        /* tree主题 */
        #themeArea{width:240px;height:80px;position:absolute;left:90px;top:134px;}
        #themeAreaInner{width:175px;margin:80px auto 10px;padding:10px 14px;line-height:18px;background:#fff;border:1px solid #b5b5b5;box-shadow:1px 1px 5px 0 rgba(0,0,0,0.3);-webkit-box-shadow:1px 1px 5px 0 rgba(0,0,0,0.4);-moz-box-shadow:1px 1px 5px 0 rgba(0,0,0,0.4);border-radius:2px;display:none;text-indent:1em;color:#848585;}
        /* lofter主题 */
        #lofter-link{height:12px;width:184px;text-indent:-9999px;display:none;position:absolute;bottom:10px;left:14px;overflow:hidden;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <div class="header" style="margin: 15px;width: auto;">
        <h1 class="headerLogo"><a href="javascript:;" target="_blank" title="走近DMX"><img src="/logo/163logo.gif" alt="宏天手机定位" /></a></h1>
        <a class="headerIntro" href="javascript:;" target="_blank" title="走近DMX"><span class="unvisi">定位手机第一品牌</span></a>
        <nav class="headerNav">
            <a href="/Member/Register.aspx" target="_blank">用户注册</a>
            <a href="/Agent/AgentReg.aspx" target="_blank">渠道商注册</a>
            <a href="/Agent/Login.aspx" target="_blank">渠道商登录</a>
            <a href="/Help.aspx" target="_blank">帮助</a>
        </nav>
    </div>
<div class="newsadd_title">
            <ul style="padding-left: 15px">
            <li bgcolor="#FFFFFF" class="newstitle">
                渠道商注册
            </li>
        </ul>
        <ul style="padding-left: 15px">
            <li bgcolor="#FFFFFF" class="newstitlebody">
                填写下面信息，进行申请渠道商的注册
            </li>
        </ul>
        <ul><li>&nbsp;</li></ul>
<ul>
     <li style="width:30%; text-align:right">用户名: </li>
    <li style="text-align:left">
     
          <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Width="249px" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空"
                                Display="Dynamic" ControlToValidate="txtUserName" ValidationGroup="age"></asp:RequiredFieldValidator>
     
    </li>
  </ul>

  <ul>
     <li style="width:30%; text-align:right">密码: </li>
    <li style="text-align:left">
     
            <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" Width="249px" MaxLength="20"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="不能为空"
                                Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="age"></asp:RequiredFieldValidator>
     
    </li>
  </ul>

  <ul>
     <li style="width:30%; text-align:right">密码确认: </li>
    <li style="text-align:left">
     
              <asp:TextBox ID="txtPassword1" TabIndex="3" runat="server" Width="249px" MaxLength="20"
                                TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                Display="Dynamic" ControlToValidate="txtPassword1" ValidationGroup="age"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码输入不符"
                                Display="Dynamic" ControlToValidate="txtPassword1" ControlToCompare="txtPassword" ValidationGroup="age"></asp:CompareValidator>
     
    </li>
  </ul>

  <ul>
     <li style="width:30%; text-align:right">验证码: </li>
    <li style="text-align:left">
     <asp:TextBox ID="txtCode" TabIndex="4" runat="server" 
                               ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="不能为空"
                                Display="Dynamic" ControlToValidate="txtCode" ValidationGroup="age"></asp:RequiredFieldValidator>
                            <asp:Image ID="imgCode" runat="server" ImageUrl="/ValidateCode.aspx" />
     
    </li>
  </ul>
  <ul>
     <li style="width:30%; text-align:right"></li>
    <li style="text-align:left">
     <asp:Button ID="btnsubmit" runat="server" Text="注册"  CssClass="adminsubmit" 
            onclick="btnsubmit_Click" ValidationGroup="age"/>   
        &nbsp;   <input type="reset" name="button" id="button" value="重置" class="adminsubmit"/>
    </li>
  </ul>
</div>

    <div id="footer" class="footer">
        <div class="footer-inner">
            <a class="footerLogo" href="javascript:;" target="_blank"><img src="/logo/netease_logo.gif" alt="中国电信"/></a>
            <nav class="footerNav">
                <a href="javascript:;" target="_blank">关于我们</a>
                <a href="javascript:;" target="_blank">官方博客</a>
                <a href="javascript:;" target="_blank">客户服务</a>
                <a style="margin-right:26px" href="javascript:;" target="_blank">隐私政策</a>|<span class="copyright">版权所有 &copy; 2012</span>
            </nav>
        </div>
    </div>
    </form>
</body>
</html>
