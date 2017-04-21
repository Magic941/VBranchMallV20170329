<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddGift.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Gift.AddGift"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<%@ Register src="~/Controls/GiftCategoryDropList.ascx" tagname="GiftCategoryDropList" tagprefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
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
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script src="/admin/js/jquery/GiftImage.helper.js" type="text/javascript"></script>
    <style type="text/css">
        .table
        {
            width: 450px;
            font-size: inherit;
            border-collapse: collapse;
            border-spacing: 0;
            border-color: gray;
        }
        .th
        {
            background-color: #EDEDED;
            border: 1px solid #D7D7D7;
            font-weight: 400;
            height: 25px;
            text-align: center;
            vertical-align: middle;
            font-style: normal;
            margin: 0;
            padding: 0;
            display: table-cell;
        }
        .td
        {
            border: 1px solid #D7D7D7;
            padding: 3px 5px;
            vertical-align: middle;
            height: 25px;
            min-width: 60px;
            margin: 0;
        }
        .tile
        {
            padding-left: 20px;
            max-width: 100px;
            text-align: left;
            border: 1px solid #D7D7D7;
            padding: 3px 5px;
            vertical-align: middle;
            height: 25px;
            min-width: 60px;
            margin: 0;
        }
        .edit
        {
            padding: 2px;
            margin-left: 5px;
            width: 72px;
            display: inline;
            border: 1px solid #A7A6AA;
            height: auto;
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfGiftImages" runat="server" />
    <asp:HiddenField ID="hfProductImagesThumbSize" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="添加礼品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="设置礼品的基本信息，礼品描述，搜索优化" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:void(0);">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:void(0);">礼品描述</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:void(0);">搜索优化</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent formitem">
            <div id="myTab1_Content0"  tabindex="0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formTR">
                                <tr>
                                    <td class="td_class">
                                        礼品分类 ：
                                    </td>
                                    <td height="25">
                                        <Maticsoft:GiftCategoryDropList ID="DropParentId" runat="server" IsNull="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>礼品名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtProductName" runat="server" Width="372px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <div id="txtProductNameTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="礼品名称不能为空，长度限制在100个字符以内！"
                                            ControlToValidate="txtProductName" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="礼品名称不能为空，长度限制在100个字符以内！" LowerBound="1"
                                                    UpperBound="100" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        计量单位 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtUnit" runat="server" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        礼品重量 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                        克
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>库存数量 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStock" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        成本价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCostPrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        市场价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMarketPrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        销售价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSalePrice" runat="server" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        对话所需积分 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtPoints" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="5"
                                            Text="0">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        是否上架 ：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="rblUpselling" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="true" Text="是" Selected="True">
                                            </asp:ListItem>
                                            <asp:ListItem Value="false" Text="否">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        商品图片 ：
                                    </td>
                                    <td height="25">
                                        <ul class="product_upload_img_ul" style="display: block">
                                            <li>
                                                <div class="ImgUpload ">
                                                    <asp:HiddenField ID="hfImage" runat="server"  />
                                                      <asp:HiddenField ID="thfImage" runat="server"  />
                                                    <span id="a1" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px;
                                                            height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload0" />
                                                        </span>
                                                </div>
                                            </li>
                                            <%--                    <li>
                                                <div class="ImgUpload">
                                                    <asp:hiddenfield id="hfImage1" runat="server" />
                                                    <span id="Span1" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload1" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:hiddenfield id="hfImage2" runat="server" />
                                                    <span id="Span3" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload2" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:hiddenfield id="hfImage3" runat="server" />
                                                    <span id="Span5" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload3" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:hiddenfield id="hfImage4" runat="server" />
                                                    <span id="Span7" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload4" />
                                                    </span>
                                                </div>
                                            </li>--%>
                                        </ul>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <label class="msgNormal">
                                            <asp:literal id="Literal32" runat="server" text="请选择有效的图片文件，第一张图片为产品主图，建议将图片文件的大小限制在200KB以内。" /></label>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" tabindex="1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td height="25">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 10px">
                                    <td class="td_class" style="vertical-align: top;">
                                        礼品简介 ：
                                    </td>
                                    <td height="25">
                                        <div>
                                            <asp:TextBox ID="txtShortDescription" Style="float: left;" runat="server" TextMode="MultiLine"
                                                Height="80px" Width="494px">
                                            </asp:TextBox>
                                            <div id="progressbar1" class="progress" style="float: left;">
                                            </div>
                                        </div>
                                        (字数限制为300个)
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="vertical-align: top;">
                                        商品描述 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="500px" TextMode="MultiLine">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">
                            详细页标题 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Title" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            详细页描述 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Description" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                            详细页关键词 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="newslistabout">
        <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
            class="border">
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
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short"></asp:Button>
                                 <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({
            //实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css',
            toolbars: [

            ['fullscreen', 'source', '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                  'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|',
                 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', '|', 'link', 'unlink']
                 ],
            initialContent: '', autoHeightEnabled: false,
            minFrameHeight: 200,
            pasteplain: false,
            wordCount: false,
            elementPathEnabled: false,
            autoClearinitialContent: true, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render($('[id$=txtDescription]').get(0)); //将编译器渲染到容器
    </script>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
