<%@ Page Title="<%$Resources:AddPaymentMode,ptEditPaymentMode %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="EditPaymentMode.aspx.cs" Inherits="Maticsoft.Web.Admin.EditPaymentMode" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../js/validate/pagevalidator.css" type="text/css" />
    <link rel="stylesheet" href="../css/Maticsoftv5.css" type="text/css" />
    <script type="text/javascript" src="../js/validate/pagevalidator.js"></script>
    <script type="text/javascript" src="../js/Maticsoftv5.js"></script>
    <!-- Ben ADD ueditor START -->
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <!-- Ben ADD ueditor END -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:AddPaymentMode,ptEditPaymentMode %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:EditPaymentMode, IDS_PageDesc %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
<%--            <div class="PageTitleArea">
                <div class="PageTitle">
                    <asp:Literal ID="lblPageTitle" runat="server" Text="<%$ Resources:EditPaymentMode, IDS_PageTitle %>"></asp:Literal>
                </div>
                <div style="width: 80%;">
                    <asp:Literal ID="lblPageDesc" runat="server" Text="<%$ Resources:EditPaymentMode, IDS_PageDesc %>"></asp:Literal>
                </div>
            </div>--%>
            <Maticsoft:StatusMessage ID="statusMessage" runat="server" Visible="False" />
            <!-- 支付方式名称-->
            <table style="width: 100%;padding: 20px;" cellspacing="5" cellpadding="1" class="border">
                <tr>
                    <td class="td_class">
                        <asp:Label ID="lblName" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblName %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <asp:TextBox ID="txtName" runat="server" Columns="40" MaxLength="200">
                        </asp:TextBox>
                    </td>
                    <td class="rightTD">
                        <div id="txtNameTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="<%$ Resources:PaymentModeView, IDS_Message_Name_Description %>"
                            ControlToValidate="txtName" ContainerId="ValidatorContainer">
                            <Validators>
                                <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_Message_Name_Description %>"
                                    LowerBound="1" UpperBound="50" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <tr>
                    <td class="td_class">
                        <asp:Label ID="lblGateway" runat="server" Text="<%$ Resources:PaymentModeView,IDS_FormField_lblGateway %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <Maticsoft:PayInterfaceDropDownList ID="dropPayInterface" runat="server" AllowNull="true"
                            NullToDisplay="<%$ Resources:PaymentModeView, IDS_FormField_nullToDisplay %>"
                            AutoPostBack="true">
                        </Maticsoft:PayInterfaceDropDownList>
                        <br />
                        <asp:HyperLink ID="hlinkImage" runat="server" Target="_blank">
                            <asp:Label ID="lblimage" runat="server"></asp:Label>
                        </asp:HyperLink>
                    </td>
                    <td class="rightTD">
                        <div id="dropPayInterfaceTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="ValidateTargetPayInterface" runat="server" ContainerId="ValidatorContainer"
                            Description="<%$ Resources:PaymentModeView, IDS_Message_PayInterface_Description %>"
                            ControlToValidate="dropPayInterface">
                            <Validators>
                                <Maticsoft:DropDownListClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_Message_PayInterface_Description %>" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <tr id="tblrMerchantCode" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblMerchantCode" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblMerchantCode %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <asp:TextBox ID="txtMerchantCode" runat="server" Columns="40" MaxLength="200">
                        </asp:TextBox>
                    </td>
                    <td class="rightTD">
                        <div id="txtMerchantCodeTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="ValidateTargetMerchantCode" runat="server" Description="<%$ Resources:PaymentModeView, IDS_Message_MerchantCode_Description %>"
                            ControlToValidate="txtMerchantCode" ContainerId="ValidatorContainer">
                            <Validators>
                                <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_Message_MerchantCode_Description %>"
                                    LowerBound="1" UpperBound="300" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <!-- 显示隐藏容器 邮件地址 -->
                <tr id="tblrImage" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblEmailAddress" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblEmailAddress %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtEmailAddress" runat="server" Columns="40" MaxLength="200">
                            </asp:TextBox>
                        </label>
                    </td>
                    <td class="rightTD">
                        <div id="txtEmailAddressTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="validatetxtEmailAddress" ContainerId="ValidatorContainer"
                            runat="server" ControlToValidate="txtEmailAddress" Description="<%$Resources:AddPaymentMode,TooltipEmialAddressLength %>"
                            Nullable="false">
                            <Validators>
                                <Maticsoft:InputStringClientValidator ErrorMessage="<%$ Resources:EmailSettings, IDS_EmailAddress_Error%>"
                                    LowerBound="1" UpperBound="200" Regex="([a-zA-Z\\.0-9_-])+@([a-zA-Z0-9_-])+((\\.[a-zA-Z0-9_-]{2,3}){1,2})" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <!-- 显示隐藏容器 商户密钥 -->
                <tr id="tblrSecretKey" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblSecretKey" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblSecretKey %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtSecretKey" runat="server" Columns="40" MaxLength="200">
                            </asp:TextBox>
                        </label>
                    </td>
                    <td class="rightTD">
                        <div class="msgNormal">
                            <asp:Literal ID="lblHelpSecret" runat="server" Text="<%$ Resources:PaymentModeView, IDS_PaymnetMode_SecondKey_Description %>"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <!-- 显示隐藏容器 第二密钥 -->
                <tr id="tblrSecondKey" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblSecondKey" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblSecondKey %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtSecondKey" runat="server" Columns="40" MaxLength="200">
                            </asp:TextBox>
                        </label>
                    </td>
                    <td class="rightTD">
                    </td>
                </tr>
                <!-- 显示隐藏容器 商户密码 -->
                <tr id="tblrPassword" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblPassWord" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblPassWord %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtPassword" runat="server" Columns="40" MaxLength="200" TextMode="Password">
                            </asp:TextBox>
                        </label>
                    </td>
                    <td class="rightTD">
                        <div class="msgNormal">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_PassWord_Description %>"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <!-- 显示隐藏容器 合作伙伴 -->
                <tr id="tblrPartner" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblPartner" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblPartner %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtPartner" runat="server" Columns="40" MaxLength="200">
                            </asp:TextBox>
                        </label>
                    </td>
                    <td class="rightTD">
                    </td>
                </tr>
                <tr id="tblrCurrencys" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblCurrencys" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblCurrencys %>"></asp:Label>
                    </td>
                    <td class="rightTD" style="width: 37%">
                        <label>
                            <asp:CheckBoxList ID="chkCurrencysList" runat="server" RepeatDirection="Horizontal"
                                RepeatColumns="4">
                            </asp:CheckBoxList>
                        </label>
                    </td>
                    <td class="rightTD">
                        <div class="msgNormal">
                            <asp:Literal ID="lblCurrencyHelp" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblCurrencyHelp %>"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr id="tblCharge" runat="server">
                    <td class="td_class">
                        <asp:Label ID="lblCharge" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblCharge %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <asp:TextBox ID="txtCharge" runat="server" Text="0">
                            </asp:TextBox>
                            <asp:CheckBox ID="chkIsPercent" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_chkIsPercent %>" />
                        </label>
                    </td>
                    <td class="rightTD">
                        <div id="txtChargeTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="ValidateTargetCharge" runat="server" ContainerId="ValidatorContainer"
                            Nullable="true" ControlToValidate="txtCharge" Description="<%$ Resources:PaymentModeView, IDS_Message_Charge_Description %>">
                            <Validators>
                                <Maticsoft:InputMoneyClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_Message_Charge_Description %>" />
                                <Maticsoft:NumberRangeClientValidator ErrorMessage="<%$Resources:AddPaymentMode,ErrorHandlingCharge %>" MinValue="0" MaxValue="99999999" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <tr>
                    <td class="td_class">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:AddPaymentMode,lblOnlineRecharge %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <label>
                            <Maticsoft:YesNoRadioButtonList ID="radAllowRecharge" runat="server">
                            </Maticsoft:YesNoRadioButtonList>
                        </label>
                    </td>
                    <td class="rightTD">
                        <div class="msgNormal">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:AddPaymentMode,lblAllowRecharge %>" />
                        </div>
                    </td>
                </tr>
               <tr>
                                <td class="td_class">
                                    <asp:Label ID="Label2" runat="server" Text="接口类型"></asp:Label>
                                </td>
                                <td class="rightTD">
                                    <asp:CheckBox ID="chkWeb" runat="server" Text="电脑版" />
                                    <asp:CheckBox ID="chkWap" runat="server" Text="手机版" />
                                </td>
                                <td class="rightTD">
                               
                                </td>
                            </tr>

                <tr>
                    <td class="td_class">
                        <asp:Label ID="lblDisplaySequence" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblDisplaySequence %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <asp:TextBox ID="txtDisplaySequence" runat="server" Text="1" Columns="5">
                        </asp:TextBox>
                    </td>
                    <td class="rightTD">
                        <div id="txtDisplaySequenceTip" runat="server">
                        </div>
                        <Maticsoft:ValidateTarget ID="ValidateTargetSequence" runat="server" ContainerId="ValidatorContainer"
                            ControlToValidate="txtDisplaySequence" Description="<%$ Resources:PaymentModeView, IDS_Message_DisplaySequence_Description %>">
                            <Validators>
                                <Maticsoft:InputNumberClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_Message_DisplaySequence_Description %>" />
                                <Maticsoft:NumberRangeClientValidator ErrorMessage="<%$ Resources:PaymentModeView, IDS_ErrorMessage_DisplaySequence %>"
                                    MinValue="1" MaxValue="65535" />
                            </Validators>
                        </Maticsoft:ValidateTarget>
                    </td>
                </tr>
                <tr>
                    <td class="td_class">
                        <asp:Label ID="lblDescription" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblDescription %>"></asp:Label>
                    </td>
                    <td class="rightTD" colspan="2">
                        <textarea id="fcContent" enableviewstate="true" cols="100" rows="8" style="width: 80%;
                            height: 200px; " runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="td_class">
                        &nbsp;
                    </td>
                    <td class="rightTD" colspan="2">
                         <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:EditPaymentMode, IDS_Button_Update %>"
                                        OnClientClick="return PageIsValid()" CssClass="adminsubmit_short" />
                                        <asp:Button ID="btnBack" runat="server" 
                                        Text="<%$ Resources:EditPaymentMode, IDS_FromField_lblReturn %>" 
                                        CssClass="adminsubmit_short" onclick="btnBack_Click" />
                    </td>
                </tr>
            </table>
<%--            <table style="width: 100%" cellspacing="0" cellpadding="0" class="formTableList">
                <tr>
                    <td class="td_class">
                        <asp:Label ID="lblDescription" runat="server" Text="<%$ Resources:PaymentModeView, IDS_FormField_lblDescription %>"></asp:Label>
                    </td>
                    <td class="rightTD">
                        <textarea id="fcContent" enableviewstate="true" cols="100" rows="8" style="width: 700px;
                            height: 200px; visibility: hidden;" runat="server"></textarea>
                    </td>
                    <td class="rightTD">
                        &nbsp;
                    </td>
                </tr>
            </table>--%>
        </div>
    </div>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
            <script type="text/javascript">
                var editor = new baidu.editor.ui.Editor({//实例化编辑器
                    iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', '|', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                'justifyleft', 'justifycenter', 'justifyright', '|', 'removeformat', '|', 'pasteplain', '|', 'link', 'unlink', '|', 'insertimage']
                 ],
                    initialContent: '', autoHeightEnabled: false,
                    minFrameHeight: 120,
                    pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
                });
                //editor.render('ctl00_ContentPlaceHolder1_fcContent'); //将编译s器渲染到容器
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
