<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductAdd.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductAdd"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="Maticsoft" Src="~/Controls/AjaxRegion.ascx" TagName="AjaxRegion" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
	<script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>--%>
    <script src="/admin/js/jquery/SelectProductCategory.helper.js" type="text/javascript"></script>
    <%-- <link href="/admin/js/jBox/Skins/jbox.css" rel="stylesheet" type="text/css" />
	<script src="/admin/js/jBox/maticsoft.jquery.jBox.js" type="text/javascript"></script>
	<script src="/admin/js/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>--%>
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
    <script src="/admin/js/jquery/ProductAdd.helper.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ProductImage.helper.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/regionjs.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.dynatextarea.js" type="text/javascript"></script>
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <link href="/admin/css/productstyle.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/chose/chosen.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/chose/chosen.jquery.js" type="text/javascript"></script>
    <!--Select2 Start-->
    <link href="/Scripts/select2/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/select2/select2.min.js" type="text/javascript"></script>
    <!--Select2 End-->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2({ width: "200px" });

            $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" }, function () {
                $('#cboxClose').hide();
            });
            $("a.packageWindow").colorbox({ width: "auto", height: "auto", inline: true, href: "#divPackage" });
            $("#ctl00_ContentPlaceHolder1_ddlSelectPackage").chosen();
            var isOpenSku = $("#ctl00_ContentPlaceHolder1_hfIsOpenSku").val();
            // var isOpenFit = $("#ctl00_ContentPlaceHolder1_hfIsOpenFit").val();
            var isOpenRelated = $("#ctl00_ContentPlaceHolder1_hfIsOpenRelated").val();
            if (isOpenSku == "True") {   //隐藏【规格】
                $("#tabSku").show();
            }
            //            if (isOpenFit == "True") {
            //                $("#tabFit").show();
            //            }
            if (isOpenRelated == "True") {
                $("#tabRelated").show();
            }
            if ($("[id$=hfIsOpenSEO]").val() == "True") {
                $("#tabSEO").show();
            }



        });

       
    </script>
    <style type="text/css">
        #AttributeContent {
            list-style-type: none;
        }

            #AttributeContent li {
                line-height: 30px;
                vertical-align: middle;
            }

                #AttributeContent li input {
                    margin-right: 5px;
                }

            #AttributeContent li {
                float: left;
                width: 150px;
                display: block;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfCurrentProductType" runat="server" />
    <asp:HiddenField ID="hfCurrentProductBrand" runat="server" />
    <asp:HiddenField ID="hfCurrentAttributes" runat="server" />
    <asp:HiddenField ID="hfCurrentBaseProductSKUs" runat="server" />
    <asp:HiddenField ID="hfCurrentProductSKUs" runat="server" />
    <asp:HiddenField ID="hfProductImages" runat="server" />
    <asp:HiddenField ID="hfProductImagesThumbSize" runat="server" />
    <asp:HiddenField ID="hfProductAccessories" runat="server" />
    <%--<asp:HiddenField ID="hfSelectedAccessories" runat="server" />--%>
    <asp:HiddenField ID="hfRelatedProducts" runat="server" />
    <asp:HiddenField ID="HiddenField_RelatedProductInfo" runat="server" />
    <input type="hidden" id="Hidden_TempSKUInfo" value='' />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="添加新商品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">您可以<asp:Literal ID="Literal3" runat="server" Text="设置商品的基本信息，详细介绍，规格，搜索优化，相关商品" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfIsOpenSku" runat="server" Value="True" />
        <%-- <asp:HiddenField ID="hfIsOpenFit" runat="server" Value="True"/>--%>
        <asp:HiddenField ID="hfIsOpenRelated" runat="server" Value="True" />
        <asp:HiddenField ID="hfIsOpenSEO" runat="server" Value="True" />
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:void(0);">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:void(0);">详细介绍</a></li>
                    <li class="normal" onclick="nTabs(this,2);" id="tabSku" style="display: none"><a href="javascript:void(0);">规格</a></li>
                    <li class="normal" onclick="nTabs(this,3);" id="tabSEO" style="display: none"><a href="javascript:void(0);">搜索优化</a></li>
                    <%--<li class="normal" onclick="nTabs(this,4);" id="tabFit"  style="display: none"><a href="javascript:void(0);">配件</a></li>--%>
                    <li class="normal" onclick="nTabs(this,4);" id="tabRelated" style="display: none"><a href="javascript:void(0);">相关商品</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent formitem">
            <div id="myTab1_Content0" tabindex="0">
                <table class="TabMainborder" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formTR">
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品分类 ：
                                    </td>
                                    <td height="25">
                                        <span style="color: royalblue; font-size: 11pt; font-weight: bold;" id="litCategoryName"></span>[<a style="font-size: 9pt;" class='iframe'>设置分类</a>]
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtProductName" runat="server" Width="372px"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <div id="txtProductNameTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="商品名称不能为空，长度限制在100个字符以内！"
                                            ControlToValidate="txtProductName" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="商品名称不能为空，长度限制在100个字符以内！" LowerBound="1"
                                                    UpperBound="100" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>副标题 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSubhead" runat="server" Width="372px"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品类型 ：
                                    </td>
                                    <td height="25">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input type="text" style="width: 200px" id="txtProductTypeSearch"  onkeyup="javascript:SelectProductTypeKeyPress();" />
                                                        <font color="red">输入自动搜索类型</font></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <select id="SelectProductType" size="10" style="width:200px">
                                                            <option selected='selected' value="">请选择</option>
                                                        </select></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>品牌 ：
                                            <select id="SelectProductBrand">
                                                <option selected='selected' value="">请选择</option>
                                            </select></span>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 200px">
                                            <asp:Literal ID="Literal1" runat="server" Text="选择此商品的商品类型" /></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品编码 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtProductSKU" runat="server" Width="200px" MaxLength="20"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>销售价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSalePrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品库存 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStock" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="6" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td class="td_class">
                                        <em>*</em>显示顺序 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDisplaySequence" runat="server" Width="200px"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <div id="txtDisplaySequenceTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTarget1" runat="server" ContainerId="ValidatorContainer"
                                            ControlToValidate="txtDisplaySequence" Description="设置商品的显示顺序，只能输入大于等于1的整数" Nullable="false"
                                            FocusMessage="设置商品的显示顺序，只能输入大于等于1的整数">
                                            <Validators>
                                                <Maticsoft:InputNumberClientValidator ErrorMessage="设置商品的显示顺序，只能输入大于等于1的整数" />
                                                <Maticsoft:NumberRangeClientValidator ErrorMessage="设置商品的显示顺序，只能输入大于等于1的整数" MinValue="1" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="td_class">指定包装 ：
                                    </td>
                                    <td height="25" id="PackageShow">
                                        <a class="packageWindow" href="javascript:void(0)">点击选择</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h2></h2>
                                    </td>
                                </tr>
                                <tr class="AttributesTR">
                                    <td id="ContetAttributesEx" colspan="2"></td>
                                </tr>

                                <tr>
                                    <td class="td_class">商家 ：
                                    </td>
                                    <td height="25">
                                        <asp:DropDownList ID="drpSupplier" CssClass="select2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">计量单位 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtUnit" runat="server" Width="200px" MaxLength="20"> </asp:TextBox>(个/件)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">所在地 ：
                                    </td>
                                    <td height="25">
                                        <Maticsoft:AjaxRegion runat="server" ID="ajaxRegion" />
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <label class="msgNormal" style="width: 372px">
                                            <asp:Literal ID="Literal4" runat="server" Text="长度不能超过20个字符" /></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">市场价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMarketPrice" runat="server" Width="200px" MaxLength="20" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">成本价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCostPrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">内部成本 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCostPrice2" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">警戒库存 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAlertStock" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="6" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">商品重量 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="20" Text="0"> </asp:TextBox>
                                        克
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">可得积分 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtPoints" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="5" Text="0"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="td_class">推荐类型：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox runat="server" ID="chbRec" Text="推荐商品" />
                                        <asp:CheckBox runat="server" ID="chbHot" Text="热卖商品" />
                                        <asp:CheckBox runat="server" ID="chbNew" Text="最新商品" />
                                        <asp:CheckBox runat="server" ID="chbLowPrice" Text="特价商品" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">是否上架 ：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="rblUpselling" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="是" Selected="True">
                                            </asp:ListItem>
                                            <asp:ListItem Value="0" Text="否">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">同步到微博 ：
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkSina" Text="新浪微博" runat="server" Checked="False" />
                                        <asp:CheckBox ID="chkQQ" Text="腾讯微博" runat="server" Checked="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">商品图片 ：
                                    </td>
                                    <td height="25">
                                        <ul class="product_upload_img_ul" style="display: block">
                                            <li>
                                                <div class="ImgUpload ">
                                                    <asp:HiddenField ID="hfImage0" runat="server" />
                                                    <span id="a1" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload0" />
                                                        </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage1" runat="server" />
                                                    <span id="Span1" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload1" />
                                                        </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage2" runat="server" />
                                                    <span id="Span3" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload2" />
                                                        </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage3" runat="server" />
                                                    <span id="Span5" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload3" />
                                                        </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage4" runat="server" />
                                                    <span id="Span7" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                        href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                            <input type="file" class="file_upload" id="file_upload4" />
                                                        </span>
                                                </div>
                                            </li>
                                            <li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <label class="msgNormal">
                                            <asp:Literal ID="Literal32" runat="server" Text="请选择有效的图片文件，第一张图片为产品主图，建议将图片文件的大小限制在200KB以内。" /></label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <input type="hidden" value="" id="Hidden_SelectValueCompare" />
                <!--当关闭分类时用于比较分类是否发生改变-->
                <div style='display: none; width: 700px;'>
                    <div class="dataarea mainwidth td_top_ccc" style="background: white;" id='divModal'>
                        <div class="advanceSearchArea clearfix">
                            <!--预留显示高级搜索项区域-->
                        </div>
                        <div class="toptitle">
                            <h1 class="title_height">选择分类</h1>
                        </div>
                        <div class="search_results">
                            <div id="category" style="display: block; margin-bottom: 10px;">
                                <h2>
                                    <span>删除</span>已添加分类</h2>
                                <ul id="selectCategory">
                                </ul>
                            </div>
                        </div>
                        <div class="results">
                            <div class="results_main" style="overflow: hidden;">
                                <div class="results_left">
                                    <label>
                                        <input type="button" name="button2" id="button2" value="" class="search_left" />
                                    </label>
                                </div>
                                <div class="results_pos">
                                    <ol class="results_ol">
                                    </ol>
                                </div>
                                <div class="results_right">
                                    <label>
                                        <input type="button" name="button2" id="button2" value="" class="search_right" />
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="results_img">
                        </div>
                        <div class="results_bottom">
                            <span class="spanE">您当前选择的是：</span> <span id="fullName"></span>
                        </div>
                        <div class="bntto">
                            <input type="button" name="button2" id="btnAdd" value="继续添加" class="adminsubmit" />
                            <input type="button" name="button2" id="btnNext" value="确定" class="adminsubmit" />
                            <input type="hidden" value="true" id="Hidden_isCate" />
                            <%--<input type="hidden" value="" id="Hidden_SelectValue" />--%>
                            <asp:HiddenField runat="server" ID="Hidden_SelectValue" Value="" />

                            <input type="hidden" value="" id="Hidden_SelectName" />

                        </div>

                        <script type="text/javascript">
                            $(function () {
                                $('#btnNext').click(function () {
                                    //alert('1');
                                    //发送 请求  改变顺序的值
                                    $.ajax({
                                        url: "/ProductHandler.aspx",
                                        type: 'post',
                                        dataType: 'json',
                                        async: false,
                                        timeout: 10000,
                                        data: { Action: "MaxSequence", CategoryPath: $("[id$='Hidden_SelectValue']").val() },
                                        success: function (resultData) {
                                            switch (resultData.STATUS) {
                                                case "SUCCESS":
                                                    //success
                                                    $("[id$='txtDisplaySequence']").val(resultData.DATA); //改变顺序的值
                                                    break;
                                                default:
                                                    alert("服务器繁忙请稍后再试！");
                                                    break;
                                            }
                                        },
                                        error: function (xmlHttpRequest, textStatus, errorThrown) {
                                            alert(xmlHttpRequest.responseText);
                                        }
                                    });

                                });
                            });
                        </script>
                    </div>
                </div>
                <div style='display: none; width: 700px;'>
                    <div class="dataarea mainwidth td_top_ccc" style="background: white;" id='divPackage'>
                        <div class="advanceSearchArea clearfix">
                            <!--预留显示高级搜索项区域-->
                        </div>
                        <div class="toptitle">
                            <h1 class="title_height">选择具体包装</h1>
                        </div>
                        <%--<div class="search_results">
							<div id="categoryEx" style="display: block;margin-bottom: 10px;">
								<h2>
									<span>删除</span>已添加分类</h2>
								<ul id="selectCategoryEx">
								</ul>
							</div>
						</div>--%>
                        <div class="results">
                            <div style="width: 500px">
                                <asp:DropDownList ID="ddlSelectPackageCategory" runat="server">
                                </asp:DropDownList>
                                <input type="text" id="txt4Keyword" />
                            </div>
                            <div id="packageList" style="width: 500px; margin-top: 15px">
                            </div>
                        </div>
                        <div class="results_img">
                        </div>
                        <asp:HiddenField runat="server" ID="selectPackageText" />
                        <div class="m_cj" id="packageDiv">
                            <div class="right_text">
                                已选择的包装：
                            </div>
                            <ul class="cen_yy" id="allpackage">
                                <div class="clear">
                                </div>
                            </ul>
                        </div>
                        <%--    <span id="allpackage">  </span>--%>
                        <div class="bntto">
                            <input type="button" name="button2" id="btnPackage" value="确定" class="adminsubmit" />
                            <input type="hidden" value="true" id="Hidden_isCate" />
                            <%--<input type="hidden" value="" id="Hidden_SelectValue" />--%>
                            <asp:HiddenField runat="server" ID="Hidden_SelectPackage" Value="" />
                            <input type="hidden" value="" id="Hidden_SelectName1" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myTab1_Content1" tabindex="1" class="none4">
                <table style="width: 100%; border-bottom: none; padding-top: 10px; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">

                                <tr style="margin-bottom: 10px; display: none;">
                                    <td class="td_class" style="vertical-align: top;">商品简介 ：
                                    </td>
                                    <td height="25">
                                        <div>
                                            <asp:TextBox ID="txtShortDescription" Style="float: left;" runat="server" TextMode="MultiLine"
                                                Height="80px" Width="594px"> </asp:TextBox>
                                            <div id="progressbar1" class="progress" style="float: left;">
                                            </div>
                                        </div>
                                        (字数限制为300个)
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td height="25"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="vertical-align: top; width: 97px;">商品介绍 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="700px" TextMode="MultiLine"> </asp:TextBox>
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
                    <tr id="trbtnOpenSKUs">
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td style="height: 10px;"></td>
                                    <td height="10"></td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <%-- <input id="btnCloseSkus" type="button" class="adminsubmit_short" value="关闭规格" />--%>
                                    </td>
                                    <td>
                                        <input id="btnOpenSKUs" type="button" class="adminsubmit_short" value="开启规格" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="SKUsTR">
                        <td id="contetSKUs" colspan="2"></td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content3" tabindex="3" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left; padding-top: 10px;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">URL规则 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtUrlRule" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面标题 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Title" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面描述 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Description" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面关键词 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">图片Alt信息 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtSeoImageAlt" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">图片Title信息 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtSeoImageTitle" runat="server" Width="372px"> </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content4" tabindex="4" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr id="AddRelatedProductTR">
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td style="height: 6px;"></td>
                                    <td height="6"></td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <input id="btnAddRelatedProducts" type="button" class="adminsubmit_short" value="添加" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="RelatedProductTR">
                        <td id="contetRelatedProduct" colspan="2" style="display: none;">
                            <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                                cellspacing="1" class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="" id="RelatedProductIfram"></iframe>
                                    </td>
                                </tr>
                            </table>
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
                            <td style="height: 6px;"></td>
                            <td height="6"></td>
                        </tr>
                        <tr>
                            <td class="td_class"></td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClientClick="return SubForm();"></asp:Button>
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
            //            toolbars: [

            //               ['fullscreen',
            //                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
            //                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
            //                'insertimage', 'imagenone', 'imageleft', 'imageright',
            //                'imagecenter', '|', 'link', 'unlink', '|']
            //                 ],

            toolbars: [

                ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                    'bold', 'italic', '|', 'forecolor', 'backcolor', '|',
                    'superscript', 'subscript', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|', 'removeformat', 'formatmatch', 'autotypeset', '|', 'pasteplain', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                    'imagecenter', '|', 'insertimage', 'insertvideo', 'map', 'horizontal', '|',
                    'link', 'unlink', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols']
            ],
            initialContent: '', autoHeightEnabled: false,
            minFrameHeight: 200,
            pasteplain: false,
            wordCount: false,
            elementPathEnabled: false,
            autoClearinitialContent: true, imagePath: "", imageManagerPath: "/"
        });
        editor.render($('[id$=txtDescription]').get(0)); //将编译器渲染到容器
    </script>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
