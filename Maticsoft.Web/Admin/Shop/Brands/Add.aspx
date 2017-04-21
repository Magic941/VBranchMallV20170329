<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Brands.Add" Title="增加页" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Web" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#imgShow").hide();

            $("[id$='lnkDelete']").click(function () {
                $("[id$='hfLogoUrl']").val("");
                $("[id$='lnkDelete']").hide();
                $("#imgShow").hide();
                clickautohide(4, "品牌Logo删除成功！", 2000);
            });

            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/UploadNormalImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'width': 76,
                'height': 25,
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'All Files',
                'queueSizeLimit': 1,
                'sizeLimit': 1024*1024 *1024*1024*1024,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {

                    if (response.split('|')[0] == "1") {
                        $("[id$='hfLogoUrl']").val(response.split('|')[1]);
                        $("#logo").attr("src", response.split('|')[1].format(''));
                        $("#imgShow").show();
                        $("[id$='lnkDelete']").show();
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });
    </script>
    <!--SWF图片上传结束-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="品牌管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="管理商品所属的各个品牌，如果在上架商品时给商品指定了品牌分类，则商品可以按品牌分类浏览" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">扩展信息</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-bottom: none; border-top: none;float:left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        品牌名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtBrandName" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div id="txtBrandNameTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="品牌名称不能为空，长度限制在100个字符以内！" OkMessage="输入正确"
                                            ControlToValidate="txtBrandName" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="品牌名称不能为空，长度限制在100个字符以内！" LowerBound="1"
                                                    UpperBound="100" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        检索拼音 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtBrandSpell" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 200px">
                                            <asp:Literal ID="Literal4" runat="server" Text="请输入品牌的拼音缩写便于检索" /></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        品牌图片 ：
                                    </td>
                                    <td height="25">
                                        <asp:HiddenField ID="hfLogoUrl" runat="server" />
                                        <div id="fileQueue">
                                        </div>
                                        <input type="file" name="uploadify" id="uploadify" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 200px">
                                            <asp:Literal ID="Literal32" runat="server" Text="请选择有效的图片文件，建议将图片文件的大小限制在200KB以内。" /></label>
                                    </td>
                                </tr>
                                <tr id="imgShow">
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <img width="80" height="40" id="logo" src="" />
                                        <asp:HyperLink ID="lnkDelete" runat="server" Style="vertical-align: middle;">
                                            【<asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,btnDeleteText %>" />】</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        品牌官方地址 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCompanyUrl" Text="http://" runat="server" MaxLength="64" Columns="40"
                                            Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div id="txtCompanyUrlTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetOffUrl" runat="server" ControlToValidate="txtCompanyUrl"  OkMessage="输入正确"
                                            ContainerId="ValidatorContainer" Nullable="true" Description="品牌官方网站的网址，长度限制在100个字符以内">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="品牌官方网站的网址，长度限制在100个字符以内" UpperBound="128"
                                                    Regex="^(http)://.*" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        显示顺序 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDisplaySequence" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div id="txtDisplaySequenceTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTarget1" runat="server" ContainerId="ValidatorContainer"
                                            ControlToValidate="txtDisplaySequence" Description="设置品牌的显示顺序，只能输入大于等于1的整数" Nullable="false"
                                            FocusMessage="设置品牌的显示顺序，只能输入大于等于1的整数">
                                            <Validators>
                                                <Maticsoft:InputNumberClientValidator ErrorMessage="设置品牌的显示顺序，只能输入大于等于1的整数" />
                                                <Maticsoft:NumberRangeClientValidator ErrorMessage="设置品牌的显示顺序，只能输入大于等于1的整数" MinValue="1" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        关联商品类型 ：
                                    </td>
                                    <td height="25">
                                        <Maticsoft:ProductTypesCheckBoxList ID="chkProductTpyes" runat="server">
                                        </Maticsoft:ProductTypesCheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 200px">
                                            <asp:Literal ID="Literal1" runat="server" Text="选择与商品品牌关联的商品类型" /></label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="td_class">
                                        模版 ：
                                    </td>
                                    <td height="25">
                                        <asp:DropDownList ID="ddlTheme" runat="server">
                                            <asp:ListItem Value="0">默认模板</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none;float:left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        页面描述 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMeta_Description" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        页面关键词 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="372px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        品牌介绍 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="373px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%; border-top: none;float:left;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="height: 6px;">
                                </td>
                                <td height="6">
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                        class="adminsubmit_short" OnClientClick="return PageIsValid();" OnClick="btnSave_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [
            ['fullscreen', 'source', '|',
             'undo', 'redo', '|', 'selectall', 'cleardoc', '|',
                'link', 'unlink', '|', 'insertimage', 'insertvideo', '|']
                 ],
            initialContent: '',
            minFrameHeight: 120,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: true, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('ctl00_ContentPlaceHolder1_txtDescription'); //将编译器渲染到容器
    </script>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
