<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Supplier.SupplierStore.Modify" Title="编辑商家" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission" TagPrefix="uc2" %>
<%@ Register Src="/Controls/Region.ascx" TagName="Region" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.colorpicker.js"></script>
    <script type="text/javascript">
        $(function () {

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);

            $("[id$='txtEstablishedDate']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd" });
            $("[id$=txtRegisteredCapital],[id$=txtCellPhone],[id$=txtMobileCount],[id$=txtQQ],[id$=txtIndexProdTop]").OnlyNum();
            $("[id$=txtBalance]").OnlyFloat();
        });
    </script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
         <!--图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var upload = function ($Id, type) {
                $Id.uploadify({
                    'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                    'script': '/SupplierUploadLogo.aspx',
                    'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                    'buttonImg': '/admin/images/uploadfile.jpg',
                    'folder': 'UploadFile',
                    'queueID': 'fileQueue',
                    'auto': true,
                    'multi': true,
                    'width': 76,
                    'height': 25,
                    'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                    'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                    'queueSizeLimit': 1,
                    'sizeLimit': 1024 * 1024 * 10,
                    'onInit': function () {
                    },

                    'onSelect': function (e, queueID, fileObj) {
                    },
                    'onComplete': function (event, queueId, fileObj, response, data) {
                        var responsejson = $.parseJSON(response);
                        if (responsejson.success) {
                            switch (type) {
                                case "ShopStoreIndex":
                                    $("[id$='hfLogoUrl']").val(responsejson.data.format(''));
                                    $("[id$='imagelogourl']").attr('src', responsejson.data.format(''));
                                    break;
                                case "ShopSearch":
                                    $("[id$='hfLogoUrlSearch']").val(responsejson.data.format(''));
                                    $("[id$='imagelogourlSearch']").attr('src', responsejson.data.format(''));
                                    break;
                                case "Square":
                                    $("[id$='hfLogoUrlSquare']").val(responsejson.data.format(''));
                                    $("[id$='imagelogourlSquare']").attr('src', responsejson.data.format(''));
                                    break;
                                case "Sale":
                                    $("[id$='hfLogoUrlSale']").val(responsejson.data.format(''));
                                    $("[id$='imagelogourlSale']").attr('src', responsejson.data.format(''));
                                    break;
                                case "Background":
                                    $("[id$='hfLogoUrlBackground']").val(responsejson.data.format(''));
                                    $("[id$='imagelogourlBackground']").attr('src', responsejson.data.format(''));
                                    break;
                                default:
                                    break;
                            }
                            ShowSuccessTip("上传成功");
                        } else {
                            ShowFailTip("图片上传失败！");
                        }
                    }
                });
            };
            upload($("#uploadify"), "ShopStoreIndex");
            upload($("#uploadifySearch"), "ShopSearch");
            upload($("#uploadifySquare"), "Square");
            upload($("#uploadifySale"), "Sale");
            upload($("#uploadifyBackground"), "Background");

            $("#Background").colorpicker({
                fillcolor: true,
                success: function (o, color) {
                    $(o).css("color", color);
                }
            });
        });
    </script>
    <!--图片上传结束-->
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
                        <asp:Literal ID="Literal2" runat="server" Text="编辑店铺" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以编辑店铺信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td>
                     <fieldset class="border_radius border" style="width: 850px;margin: 20px;padding-bottom: 10px;" >
                        <legend>基本设置</legend>
              
                  <table style="width: 100%;" cellpadding="2" cellspacing="0"  border="0">
                         <tr>
                <td class="td_class">
                    店铺名称 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtShopName" runat="server" Width="350px" MaxLength="100"></asp:TextBox><span style=" color:Red;"> <asp:Literal ID="lterShopClose"  Visible="false" runat="server" Text="您的店铺已关闭" /></span>
                </td>
            </tr>
 
             <tr  runat="server" id="trLogo1" visible="false"  >
                                     <td class="td_class">
                                         Logo1 ：
                                     </td>
                                     <td height="25">
                                          <asp:HiddenField ID="hfLogoUrl" runat="server" />
                                            <input type="file" name="uploadify" id="uploadify" /> &nbsp;&nbsp; 建议上传图片的尺寸为 1200*68(px)  (用于商城店铺首页)
                                     </td>
              </tr>
              <tr runat="server"  id="trLogo1Image" visible="false" >
                                     <td class="td_class">
                                     </td>
                                     <td height="25"> 
                                          <img ID="imagelogourl" src="<%=Maticsoft.BLL.Shop.Supplier.SupplierInfo.SupperLogo(SupplierId,"T980X68") %>" width="490px" Height="34px"  onerror="this.onerror='';this.src='/Content/themes/base/images/404/404_980X68.jpg'"/>
                                  
                                     </td>
                                 </tr> 

                                 <tr runat="server"   id="trLogo2"  >
                                <td class="td_class">
                                    <asp:Literal ID="ltlLogo2" runat="server" Text="Logo2" /> ：
                                </td>
                                <td height="25">
                                    <asp:HiddenField ID="hfLogoUrlSquare" runat="server" />                              
                                    <input type="file" name="uploadify" id="uploadifySquare"   />  &nbsp;&nbsp; 建议上传图片为正方形  <asp:Literal ID="ltlSquare" runat="server" Text="" />          
                                </td>
                            </tr>
                             <tr runat="server" id="trLogo2Image"   >
                                <td class="td_class">
                                </td>
                                <td  >
                                     <img id="imagelogourlSquare" src="<%=Maticsoft.BLL.Shop.Supplier.SupplierInfo.SupperLogo(SupplierId,"T200X200") %>"  width="100" height="100" onerror="this.onerror='';this.src='/Content/themes/base/images/404/404_200X200.jpg'"/>
                                  
                                </td>
                            </tr>
                             <tr runat="server"  id="trLogo3" visible="false" >
                                <td class="td_class">
                                    <asp:Literal ID="ltlLogo3" runat="server" Text="Logo3" /> ：
                                </td>
                                <td height="25">
                                     <asp:HiddenField ID="hfLogoUrlSearch" runat="server" />  
                                    <input type="file" name="uploadify" id="uploadifySearch" />&nbsp;&nbsp; 建议上传图片的尺寸为 180*60(px)   (用于商城搜索店铺)
                                </td>
                            </tr>

                             <tr  runat="server" id="trLogo3Image" visible="false">
                                      <td class="td_class">
                                      </td>
                                      <td height="25">
                                          
                                         <img id="imagelogourlSearch"  width="180" height="60"  src="<%=Maticsoft.BLL.Shop.Supplier.SupplierInfo.SupperLogo(SupplierId,"T180X60") %>"  onerror="this.onerror='';this.src='/Content/themes/base/images/404/404_180X60.jpg'" />
                                      </td>
                                  </tr>

                      <tr runat="server"  id="trLogo4" visible="false" >
                                <td class="td_class">
                                    <asp:Literal ID="ltlLogo4" runat="server" Text="Logo4" /> ：
                                </td>
                                <td height="25">
                                     <asp:HiddenField ID="hfLogoUrlSale" runat="server" />  
                                    <input type="file" name="uploadify" id="uploadifySale" />&nbsp;&nbsp; 建议上传图片的尺寸为 580*280(px)   (用于商城促销展示)
                                </td>
                            </tr>

                             <tr  runat="server" id="trLogo4Image" visible="false">
                                      <td class="td_class">
                                      </td>
                                      <td height="25">
                                          
                                         <img id="imagelogourlSale"  width="276" height="133"  src="<%=Maticsoft.BLL.Shop.Supplier.SupplierInfo.SupperLogo(SupplierId,"T200X400") %>"  onerror="this.onerror='';this.src='/Content/themes/base/images/404/404_180X60.jpg'" />

                                      </td>
                                  </tr>
            <tr>
                <td class="td_class" valign="top">
                    店铺状态 ：
                </td>
                <td height="25">
                   <asp:RadioButtonList ID="radioStatus" runat="server"  RepeatDirection="Horizontal"  Enabled="True" >
            <asp:ListItem  Value="0">未审核</asp:ListItem>
            <asp:ListItem Value="1">已审核</asp:ListItem>
              <asp:ListItem  Value="-1">未开店</asp:ListItem>
            <asp:ListItem Value="2">已关闭</asp:ListItem>
        </asp:RadioButtonList>
                </td>
            </tr>
                  </table>
                  </fieldset>
                    </td>
            </tr>
            <tr id="trShop" runat="server" visible="false">
                <td>
                     <fieldset class="border_radius border" style="width: 850px;margin: 20px;padding-bottom: 10px;" >
                        <legend>商城店铺设置</legend>
                            <table style="width: 100%;" cellpadding="2" cellspacing="0" border="0">
                              <tr>
                <td class="td_class" valign="top">
                    店铺自定义内容区 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtIndexContent" runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="td_class" valign="top">
                    首页显示商品数量 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtIndexProdTop" runat="server" Width="50px" ></asp:TextBox>  &nbsp;&nbsp;
                    <span>
                        <asp:Button ID="btnClose" runat="server" Text="关闭店铺" class="adminsubmit_short" OnClick="btnClose_Click" Visible="False"></asp:Button>
                    </span>
                </td>
            </tr>
            
            <tr >
                <td class="td_class">
                    客服QQ ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtQQ" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>

               <tr>
                                <td class="td_class" valign="top">
                                    店铺背景类型 ：
                                </td>
                                <td height="25">
                                    <asp:RadioButtonList ID="rdlBGType" runat="server" RepeatColumns="2">
                                        <asp:ListItem Selected="True" Value="True">背景色</asp:ListItem>
                                        <asp:ListItem Value="False">背景图</asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" valign="top">
                                    店铺背景色 ：
                                </td>
                                <td height="25">
                                    <asp:TextBox ID="Background" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>


                                <tr id="tr1" visible="false" >
                                <td class="td_class">
                                    店铺背景图 ：
                                </td>
                                <td height="25">
                                     <asp:HiddenField ID="hfLogoUrlBackground" runat="server" />  
                                    <input type="file" name="uploadify" id="uploadifyBackground" />&nbsp;&nbsp;建议上传无缝贴图，尺寸以较小为宜   (用于商铺店面背景)
                                </td>
                            </tr>

                             <tr id="tr2" visible="false">
                                      <td class="td_class">
                                      </td>
                                      <td height="25">
                                         <img id="imagelogourlBackground"  src="<%=Maticsoft.BLL.Shop.Supplier.SupplierInfo.SupperLogo(SupplierId,"T200X600") %>"  onerror="this.onerror='';this.src='/Content/themes/base/images/404/404_180X60.jpg'" />
                                      </td>
                                  </tr>
                            </table>

                          </fieldset>

                    </td>
            </tr>
            <tr id="trMShop" runat="server" visible="false">
                <td>
                     <fieldset class="border_radius border" style="width: 850px;margin: 20px;padding-bottom: 10px;" >
                        <legend>微商城店铺设置</legend>
                            <table style="width: 100%;" cellpadding="2" cellspacing="0" border="0">
                                 <tr  >
                <td class="td_class">
                    客服电话 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtServicePhone" runat="server" Width="100px" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
             <tr>
                                <td class="td_class" valign="top">
                                    首页显示商品数量 ：
                                </td>
                                <td height="25">
                                     <asp:TextBox ID="txtMobileCount" runat="server" Width="50px" MaxLength="2" >10</asp:TextBox>
                                </td>
                            </tr>  
                           </table>
                          </fieldset>

                    </td>
            </tr> 
 
            <tr>
                <td class="td_class" style="text-align: center;">
                     <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>" class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                </td>
                <td height="25">
                   
                    </td>
            </tr>
        </table>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

  ['fullscreen',
                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
                'insertimage', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        if ($('[id$="txtIndexContent"]').length>=1) {
            editor.render('<%=this.txtIndexContent.ClientID %>'); //将编译器渲染到容器
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
