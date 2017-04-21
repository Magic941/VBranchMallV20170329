<%@ Page Title="编辑商品" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="ProductModify.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductModify" EnableEventValidation="false" %>

<%@ Register TagPrefix="Maticsoft" Src="~/Controls/AjaxRegion.ascx" TagName="AjaxRegion" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/admin/js/jquery/SelectProductCategory.helper.js" type="text/javascript"></script>
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

    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script src="/admin/js/jquery/ProductModify.helper.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ProductImage.helper.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/regionjs.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.dynatextarea.js" type="text/javascript"></script>
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <link href="/admin/css/productstyle.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/chose/chosen.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/chose/chosen.jquery.js" type="text/javascript"></script>
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
    <!--Select2 Start-->
    <link href="/Scripts/select2/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/select2/select2.min.js" type="text/javascript"></script>
    <!--Select2 End-->
    <script type="text/javascript">
        var categoryArray = new Array();
        $(document).ready(function () {
            $(".select2").select2({ width: "200px" });

            $.ajaxPrefilter(function (options) { options.global = true; });

            $("#ctl00_ContentPlaceHolder1_ddlSelectPackage").chosen();
            var isOpenSku = $("#ctl00_ContentPlaceHolder1_hfIsOpenSku").val();
            var isOpenRelated = $("#ctl00_ContentPlaceHolder1_hfIsOpenRelated").val();
            if (isOpenSku == "True") {
                $("#specificationTab").show();
            }

            if (isOpenRelated == "True") {
                $("#tabRelated").show();
            }
            if ($("[id$=hfIsOpenSEO]").val() == "True") {
                $("#tabSEO").show();
            }

            $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" }, function () {
                $('#cboxClose').hide();
            });


            var currentClass;
            $("#category ul li").hover(function () {
                currentClass = $(this).attr('class');
                $(this).removeClass("rowBKcolor");
                $(this).addClass("mover");
            }, function () {
                $(this).removeClass("mover");
                if (currentClass) {
                    $(this).addClass(currentClass);
                }
            });
            $("#category ul li img").bind('click', function () {
                var cateId = $(this).attr('id');
                $(this).parent().remove();
                if ($("#category ul li ").length == 0) {
                    $("#category").hide();
                }
                // 删除隐藏域中的分类ID
                categoryArray = $("[id$='Hidden_SelectValue']").val().split(',');
                var delIndex = -1;
                for (var i = 0; i <= categoryArray.length - 1; i++) {
                    if (categoryArray[i] == cateId) {
                        categoryArray.remove(i);
                        delIndex = i;
                    }
                }
                var categoryNameArray = $("[id$='Hidden_SelectName']").val().split(',');
                categoryNameArray.remove(delIndex);
                $("[id$='Hidden_SelectName']").val(categoryNameArray.join(','));
                $("[id$='Hidden_SelectValue']").val(categoryArray.join(','));
            });
        });

        function closefrm(vid) {
            alert('商品保存成功');
            parent.closeWindown();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HidSaleStatus" runat="server" />
    <asp:HiddenField ID="hfCategoryId" runat="server" />
    <asp:HiddenField ID="hfCurrentProductType" runat="server" />
    <asp:HiddenField ID="hfCurrentProductBrand" runat="server" />
    <asp:HiddenField ID="hfCurrentAttributes" runat="server" />
    <asp:HiddenField ID="hfCurrentBaseProductSKUs" runat="server" />
    <asp:HiddenField ID="hfCurrentProductSKUs" runat="server" />
    <asp:HiddenField ID="hfProductImages" runat="server" />
    <asp:HiddenField ID="hfProductImagesThumbSize" runat="server" />
    <asp:HiddenField ID="hfProductAccessories" runat="server" />
    <asp:HiddenField ID="hfRelatedProducts" runat="server" />
    <asp:HiddenField ID="HiddenField_RelatedProductInfo" runat="server" />
    <input type="hidden" id="hidden_IsFirstLoad" value="1" />
    <input type="hidden" id="Hidden_TempSKUInfo" value='' />
    <asp:HiddenField ID="hfHasSku" runat="server" />

    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="编辑商品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">您可以<asp:Literal ID="Literal3" runat="server" Text="设置商品的基本信息，详细介绍，规格，搜索优化，相关商品" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfIsOpenSku" runat="server" Value="True" />
        <%--  <asp:HiddenField ID="hfIsOpenFit" runat="server" Value="True"/>--%>
        <asp:HiddenField ID="hfIsOpenRelated" runat="server" Value="True" />
        <asp:HiddenField ID="hfIsOpenSEO" runat="server" Value="True" />
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:void(0);">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:void(0);">详细介绍</a></li>
                    <li class="normal" onclick="nTabs(this,2);" id="specificationTab" style="display: none"><a href="javascript:void(0);">规格</a></li>
                    <li class="normal" onclick="nTabs(this,3);" id="tabSEO" style="display: none"><a href="javascript:void(0);">搜索优化</a></li>
                    <li class="normal" onclick="nTabs(this,4);" id="tabRelated" style="display: none"><a href="javascript:void(0);">相关商品</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent formitem">
            <div id="myTab1_Content0" tabindex="0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formTR">
                                <tr>
                                    <td class="td_class">
                                        <em>*</em> 商品分类 ：
                                    </td>
                                    <td height="25">
                                        <span style="color: royalblue; font-size: 11pt; font-weight: bold;" id="litCategoryName">
                                            <asp:Literal runat="server" ID="LitPName"></asp:Literal></span>
                                        [<a style="font-size: 9pt;" class='iframe'>设置分类</a>]
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>商品名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtProductName" runat="server" Width="372px">
                                        </asp:TextBox>
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
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <div id="txtProductNameTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="商品名称不能为空，长度限制在100个字符以内！" ControlToValidate="txtProductName" ContainerId="ValidatorContainer">
                                            <Validators>
                                                <Maticsoft:InputStringClientValidator ErrorMessage="商品名称不能为空，长度限制在100个字符以内！" LowerBound="1" UpperBound="100" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
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
                                                        <input type="text" style="width: 200px" id="txtProductTypeSearch" onkeyup="javascript:SelectProductTypeKeyPress();" />
                                                        <font color="red">回车自动搜索类型</font></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <select id="SelectProductType" size="10" style="width: 200px">
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
                                        <asp:TextBox ID="txtProductSKU" runat="server" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>销售价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSalePrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">
                                        <em>*</em>商品库存 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtStock" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="6" Text="0">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>已售数量：
                                    </td>
                                    <td height="25">
                                         <asp:TextBox ID="txtSaleCounts" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="6" Text="0">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <em>*</em>显示顺序 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDisplaySequence" runat="server" Width="200px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class"></td>
                                    <td height="25">
                                        <div id="txtDisplaySequenceTip" runat="server">
                                        </div>
                                        <Maticsoft:ValidateTarget ID="ValidateTarget1" runat="server" ContainerId="ValidatorContainer" ControlToValidate="txtDisplaySequence" Description="设置商品的显示顺序，只能输入大于等于1的整数" Nullable="false" FocusMessage="设置商品的显示顺序，只能输入大于等于1的整数">
                                            <Validators>
                                                <Maticsoft:InputNumberClientValidator ErrorMessage="设置商品的显示顺序，只能输入大于等于1的整数" />
                                                <Maticsoft:NumberRangeClientValidator ErrorMessage="设置商品的显示顺序，只能输入大于等于1的整数" MinValue="1" />
                                            </Validators>
                                        </Maticsoft:ValidateTarget>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <h2></h2>
                                    </td>
                                </tr>
                                <!--展示动态属性-->
                                <tr class="AttributesTR" style="display: none;">
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
                                        <asp:TextBox ID="txtUnit" runat="server" Width="200px" MaxLength="20">
                                        </asp:TextBox>
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
                                        <asp:TextBox ID="txtMarketPrice" runat="server" Width="200px" MaxLength="20" Text="0">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">成本价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCostPrice" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">成本价 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCostPrice2" runat="server" CssClass="OnlyFloat" Width="200px"
                                            MaxLength="20">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">警戒库存 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtAlertStock" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="5">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">商品重量 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="20">
                                        </asp:TextBox>
                                        克
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">运费 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtFreight" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="20" Text="0"> </asp:TextBox>
                                        元
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">可得积分 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtPoints" runat="server" CssClass="OnlyNum" Width="200px" MaxLength="5" Text="0">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="haveSku">
                                    <td class="td_class">是否上架 ：
                                    </td>
                                    <td height="25">
                                        <asp:RadioButtonList ID="rblUpselling" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="是">
                                            </asp:ListItem>
                                            <asp:ListItem Value="0" Text="否">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
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
                                                    <span id="a1" class="cancel" style="display: none; z-index: 999999"></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload0" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage1" runat="server" />
                                                    <span id="Span1" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload1" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage2" runat="server" />
                                                    <span id="Span3" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload2" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage3" runat="server" />
                                                    <span id="Span5" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
                                                        <input type="file" class="file_upload" id="file_upload3" />
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="ImgUpload">
                                                    <asp:HiddenField ID="hfImage4" runat="server" />
                                                    <span id="Span7" class="cancel" style="display: none; z-index: 999999"><a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px; height: 128px; overflow: hidden;">
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
                                    <asp:Repeater runat="server" ID="rptSelectCategory"
                                        OnItemDataBound="rptSelectCategory_ItemDataBound">
                                        <ItemTemplate>
                                            <li class="">
                                                <img src="http://img.baidu.com/hi/img/del.gif" class="cat-0" id="<%#Eval("CategoryId") %>_<%#Eval("CategoryPath") %>"><span><asp:Literal runat="server" ID="litCateName_1"></asp:Literal></span></li>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <li class="rowBKcolor">
                                                <img src="http://img.baidu.com/hi/img/del.gif" class="cat-1" id="<%#Eval("CategoryId") %>_<%#Eval("CategoryPath") %>"><span><asp:Literal runat="server" ID="litCateName_2"></asp:Literal></span></li>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
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
                            <asp:HiddenField runat="server" ID="Hidden_SelectValue" Value="" />
                            <asp:HiddenField runat="server" ID="Hidden_SelectName" Value="" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myTab1_Content1" tabindex="1" class="none4">
                <table style="width: 100%; border-bottom: none; padding-top: 10px; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">

                                <tr style="margin-bottom: 10px; display: none;">
                                    <td class="td_class" style="vertical-align: top;">商品简介 ：
                                    </td>
                                    <td height="25">
                                        <div>
                                            <asp:TextBox ID="txtShortDescription" Style="float: left;" runat="server" TextMode="MultiLine" Height="80px" Width="594px">
                                            </asp:TextBox>
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
                                        <asp:TextBox ID="txtDescription" runat="server" Width="600px" TextMode="MultiLine">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
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
                                    <td height="25">
                                        <input id="btnOpenSKUs" type="button" class="adminsubmit_short" value="开启规格" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="SKUsTR" style="display: none;">
                        <td id="contetSKUs" colspan="2"></td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content3" tabindex="3" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left; padding-top: 10px;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">URL规则 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtUrlRule" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面标题 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Title" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面描述 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Description" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">页面关键词 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">图片Alt信息 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtSeoImageAlt" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">图片Title信息 ：
                        </td>
                        <td height="25">
                            <asp:TextBox ID="txtSeoImageTitle" runat="server" Width="372px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="myTab1_Content4" tabindex="4" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
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
                            <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/Products/SelectRelatedProducts.aspx?pid=<%=ProductId %>" id="RelatedProductIfram"></iframe>
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

        <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1" class="border">
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
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="<%$ Resources:Site, btnSaveText %>" class="adminsubmit_short" OnClientClick="return SubForm();"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({
            //实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css',
            toolbars: [
             ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                    'bold', 'italic', '|', 'forecolor', 'backcolor', '|',
                    'superscript', 'subscript', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|', 'removeformat', 'formatmatch', 'autotypeset', '|', 'pasteplain', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                    'imagecenter', '|', 'insertimage', 'insertvideo', 'map', 'horizontal', '|',
                    'link', 'unlink', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols']
            ],
            initialContent: '',
            autoHeightEnabled: false,
            minFrameHeight: 200,
            pasteplain: false,
            wordCount: false,
            elementPathEnabled: false,
            autoClearinitialContent: false,
            imagePath: "",
            imageManagerPath: "/"
        });
        //将编译器渲染到容器
        if ($.browser.msie) {
            //针对万恶的IE特殊处理
            $(document).ready(function () { editor.render($('[id$=txtDescription]').get(0)); });
        } else {
            editor.render($('[id$=txtDescription]').get(0));
        }
    </script>
</asp:Content>
