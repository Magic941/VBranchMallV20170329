<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Ms.Enterprise.Modify" Title="编辑企业" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register Src="/Admin/../Controls/Region.ascx" TagName="Region" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
         <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);

            $("[id$='txtEstablishedDate']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd" });
            $("[id$=txtRegisteredCapital],[id$=txtCellPhone],[id$=txtQQ]").OnlyNum();
            $("[id$=txtBalance]").OnlyFloat();
        });
    </script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="编辑企业" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以编辑企业信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
           
                        <tr style="display: none;">
                            <td class="td_class">
                                用户名 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUserName" runat="server" Width="350px" MaxLength="25" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                企业名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>  <tr>
                        <td class="td_class">
                            成立时间 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtEstablishedDate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                        </td>
            </tr>
            <tr>
                <td class="td_class">
                    注册资本 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtRegisteredCapital" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    账户余额 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtBalance" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    电话 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtTelPhone" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    手机 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtCellPhone" runat="server" Width="350px" MaxLength="11"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    联系邮箱 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtContactMail" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    省/市/县：
                </td>
                <td height="25">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
                                    <uc1:Region ID="RegionID" runat="server"  VisibleAll ="true"  VisibleAllText="--请选择--" />
                                    </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    注册地 ：
                </td>
                <td height="25">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <contenttemplate>
                                    <uc1:Region ID="RegionEstablishedCity" runat="server"  VisibleAll ="true"  VisibleAllText="--请选择--" />
                                    </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    详细地址 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtAddress" runat="server" Width="350px" MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    联系人 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtContact" runat="server" Width="350px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    传真 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtFax" runat="server" Width="350px" MaxLength="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    邮编 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtPostCode" runat="server" Width="350px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    主页 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtHomePage" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    法人 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtArtiPerson" runat="server" Width="350px" MaxLength="25"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    企业等级 ：
                </td>
                <td height="25">
                    <asp:DropDownList ID="dropEnteRank" runat="server" Width="205px">
                        <asp:ListItem Value="0">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">一星级</asp:ListItem>
                        <asp:ListItem Value="2">二星级</asp:ListItem>
                        <asp:ListItem Value="3">三星级</asp:ListItem>
                        <asp:ListItem Value="4">四星级</asp:ListItem>
                        <asp:ListItem Value="3">五星级</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    企业分类 ：
                </td>
                <td height="25">
                    <asp:DropDownList ID="dropEnteClassID" runat="server" Width="205px">
                        <asp:ListItem Value="0">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">合资</asp:ListItem>
                        <asp:ListItem Value="2">独资</asp:ListItem>
                        <asp:ListItem Value="3">国有</asp:ListItem>
                        <asp:ListItem Value="4">私营</asp:ListItem>
                        <asp:ListItem Value="5">全民所有制</asp:ListItem>
                        <asp:ListItem Value="6">集体所有制</asp:ListItem>
                        <asp:ListItem Value="7">股份制</asp:ListItem>
                        <asp:ListItem Value="8">有限责任制</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    公司性质：
                </td>
                <td height="25">
                    <asp:DropDownList ID="dropCompanyType" runat="server" Width="205px">
                        <asp:ListItem Value="0">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">个体工商</asp:ListItem>
                        <asp:ListItem Value="2">私营独资企业</asp:ListItem>
                        <asp:ListItem Value="3">国营企业</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    营业执照 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtBusinessLicense" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    税务登记 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtTaxNumber" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    开户银行 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtAccountBank" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    账号信息 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtAccountInfo" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    客服电话 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtServicePhone" runat="server" Width="350px" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    QQ ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtQQ" runat="server" Width="350px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    MSN ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtMSN" runat="server" Width="350px" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    状态
                </td>
                <td height="25">
                    <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                        <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                        <asp:ListItem Value="2" Text="冻结"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    创建日期 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtCreatedDate" runat="server" Width="70px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    创建用户 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtCreatedUserID" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    更新日期 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtUpdatedDate" runat="server" Width="70px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    更新用户 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtUpdatedUserID" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    标志 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtLOGO" runat="server" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="td_class">
                    代理商ID ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtAgentID" runat="server" Width="350px" Text="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_class" valign="top">
                    企业介绍 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtIntroduction" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td height="10px">
                </td>
            </tr>
            <tr>
                <td class="td_class" valign="top">
                    备注 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtRemark" runat="server" Width="500px" TextMode="MultiLine" Rows="3"
                        Height="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td height="10px">
                </td>
            </tr>
            <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                        class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                             'superscript', 'subscript', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|', 'removeformat', 'formatmatch', 'autotypeset', '|', 'pasteplain', '|', 'customstyle',
                'paragraph', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', 'insertvideo', 'map', 'horizontal', '|',
                'link', 'unlink', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('<%=this.txtIntroduction.ClientID %>'); //将编译器渲染到容器
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
