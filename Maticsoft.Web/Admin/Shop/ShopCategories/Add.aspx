<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ShopCategories.Add" Title="增加页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/tab.js" type="text/javascript"> </script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"  type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"  type="text/javascript"></script>
    <script src="/admin/js/jquery/SelectCategoryAuto.helper.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/WebLogo.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'queueID': 'fileQueue',
                'width': 76,
                'height': 25,
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function() {
                },
                'onSelect': function(e, queueID, fileObj) {
                },
                'onComplete': function(event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("#imgUrl").attr("src", response.split('|')[1].format(''));
                        $("[id$='HiddenField_ICOPath']").val(response.split('|')[1]);
                    } else {
                        ShowFailTip("图片上传失败！", 2000);
                    }
                },
                'onError': function(event, ID, fileObj, errorObj) {
                    ShowFailTip('上传文件发生错误, 状态码: [' + errorObj.info + ']', 2000);
                }
            });
            $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" });
            resizeImg('.TabContent', 120, 120);
        });
    </script>
    <style type="text/css">
        /*890px*/
        .results_pos { position: relative; overflow: hidden; background: red; float: left; width: 450px; background: #e5f0ff; border: 1px solid #c7deff; border-left: 0; height: 298px; }
        .results_ol { position: absolute; display: block; overflow: hidden; clear: both; left: 0px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="添加商品分类" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="为不同类型的商品创建不同的分类，方便您管理也方便顾客浏览 " />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick=" nTabs(this, 0); "><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick=" nTabs(this, 1); "><a href="javascript:;">搜索优化</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table cellpadding="2" cellspacing="1" class="TabMainborder">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        选择上级分类 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtCategoryText" runat="server" Width="315px" ></asp:TextBox>
                                        <a class='iframe' href="javascript:;">选择分类</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        分类名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtName" runat="server" Width="371px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="Hidden_SelectName"/>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td class="td_class">
                                        货号前缀 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtSKUPrefix" runat="server" Width="371px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        URL重写名称 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtRewriteName" runat="server" Width="371px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        类别图片 ：
                                    </td>
                                    <td height="25">
                                            <asp:HiddenField ID="HiddenField_ICOPath" runat="server" />
                                            <div id="fileQueue">
                                            </div>
                                            <input type="file" name="uploadify" id="uploadify" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        分类描述 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="700px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                    </td>
                                    <td height="25">
                                        <asp:CheckBox ID="chkIsAdd" runat="server" Text="连续添加" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
               <div style="margin-left: 550px;position:absolute;margin-top: 50px;"  id="pic_load" ><img id="imgUrl"  ref="/Content/themes/base/Shop/images/none.png" width="120" height="120" /></div>
            </div>
            <div id="myTab1_Content1" class="none4">
                <table cellpadding="2" cellspacing="1" class="TabMainborder">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="td_class">
                                        页面标题 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMeta_Title" runat="server" Width="371px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        Meta描述标签 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMeta_Description" runat="server" Width="371px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        Meta关键字标签 ：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="txtMeta_Keywords" runat="server" Width="371px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table cellpadding="2" cellspacing="1" class="TabFotterBorder">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style='display: none;width: 700px;'>

    <div class="dataarea mainwidth td_top_ccc" style="background: white;" id='divModal'>
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h1 class="title_height">
                选择分类</h1>
        </div>
        <div class="search_results">
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
            <input type="button" name="button2" id="btnNext" value="确定选择" class="adminsubmit" />
            <input type="hidden" value="true" id="Hidden_isCate" />
              <asp:HiddenField ID="Hidden_SelectValue" runat="server" />
        </div>
    </div>
    </div>
    <br />
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

                ['fullscreen', 'source', '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                  'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|',
                 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', '|', 'link', 'unlink']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            pasteplain: false
            , wordCount: false
            , elementPathEnabled: false
            , autoClearinitialContent: true, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('<%=txtDescription.ClientID %>'); //将编译器渲染到容器
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>