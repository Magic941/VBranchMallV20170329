<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" MasterPageFile="~/Admin/Basic.Master"
    Inherits="Maticsoft.Web.Admin.Ms.WaterMarks.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/colorselect/iColorPicker.js" type="text/javascript"></script>
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
            var isAddWater = $("#ctl00_ContentPlaceHolder1_txtIsAddWater").val();
            if (isAddWater == "false") {
                $("#txtAddWater").hide();
            } else {
                $("#txtAddWater").show();
            };

            if ($("[id$='hfLogoUrl']").val().length <= 0) {
                $("#imgShow").hide();
            } else {
                $("#imgShow").show();
            }
            $("[id$='lnkDelete']").click(function () {
                $("[id$='hfLogoUrl']").val("");
                $("[id$='lnkDelete']").hide();
                $("#imgShow").hide();
                clickautohide(4, "删除成功！", 2000);
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
                'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024,
                'onInit': function () {
                },
                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {

                    if (response.split('|')[0] == "1") {
                        //                        alert(response);
                        //                        alert(response.split('|')[1].format(''));
                        $("[id$='hfLogoUrl']").val(response.split('|')[1]);
                        $("[id$='logo']").attr("src", response.split('|')[1].format(''));
                        $("[id$='hfLogoUrl']").val(response.split('|')[1].format(''));
                        $("#imgShow").show();
                        $("[id$='lnkDelete']").show();
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });
        $(function () {
            $("#slider-range-max").slider({
                range: "max",
                min: 1,
                max: 100,
                value: $('input[id$="txtTransparent"]').val(),
                slide: function (event, ui) {
                    $('input[id$="txtTransparent"]').val(ui.value);
                }
            });
            $('input[id$="txtTransparent"]').val($("#slider-range-max").slider("value"));
            DataInit();
            $("#ctl00_ContentPlaceHolder1_ddlType").change(function () {
                DataInit();
            });
        });
        
        function DataInit() {
            if ($("#ctl00_ContentPlaceHolder1_ddlType").val() == "0") {
                wordshow();
            } else {
                photoshow();
            }
        }

        function wordshow() {
            $(".wordAbout").show();
            $(".photoAbout").hide();
        }

        function photoshow() {
            $(".wordAbout").hide();
            $(".photoAbout").show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:HiddenField ID="txtIsAddWater" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="水印设置" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以选择水印的类型、水印的位置、水印的颜色等" />
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 150px;text-align: right;">
                    是否启用水印：
                </td>
                <td>
                    <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="radlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="true">是</asp:ListItem>
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            id="txtAddWater">
            <tr>
                 <td class="td_class">
                    产品图片水印设置：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:DropDownList runat="server" Width="150px" ID="ddlType">
                        <asp:ListItem Value="0" Selected="True">文字水印</asp:ListItem>
                        <asp:ListItem Value="1">图片水印</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="wordAbout">
                   <td class="td_class">
                    水印文字：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:TextBox ID="txtWaterWords" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr class="wordAbout">
                   <td class="td_class">
                    水印字体：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:DropDownList ID="ddlFont" runat="server" Width="150px">
                        <asp:ListItem Value="SimSun">宋体</asp:ListItem>
                        <asp:ListItem Value="KaiTi">楷体</asp:ListItem>
                        <asp:ListItem Value="Microsoft YaHei">微软雅黑</asp:ListItem>
                        <asp:ListItem Value="SimHe">黑体</asp:ListItem>
                        <asp:ListItem Value="SimHe">黑体</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="wordAbout">
                   <td class="td_class">
                    水印文字大小：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:TextBox ID="txtWordsSize" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr class="wordAbout">
                  <td class="td_class">
                    水印文字颜色：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:TextBox ID="txtColor" runat="server" CssClass="iColorPicker" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <%--     
            <tr class="photoAbout">
                <td  style=" width:20%">
                    水印图片：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Image ID="waterPhoto" runat="server"  /><br/>

                                               <asp:FileUpload ID="uploadPhoto" runat="server" Width="235px" CssClass="CS" />
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="uploadPhoto" runat="server" ErrorMessage="请选择正确的JPG格式" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG)$"></asp:RegularExpressionValidator>
  
                </td>
            </tr>--%>
            <tr class="photoAbout">
                 <td class="td_class">
                    图片 ：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:HiddenField ID="hfLogoUrl" runat="server" />
                    <div id="fileQueue">
                    </div>
                    <input type="file" name="uploadify" id="uploadify" /><br />
                </td>
            </tr>
            <tr class="photoAbout">
                <td class="td_class">
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <label class="msgNormal" style="width: 200px">
                        <asp:Literal ID="Literal32" runat="server" Text="请选择有效的图片文件，建议将图片文件的大小限制在200KB以内。" /></label>
                </td>
            </tr>
            <tr id="imgShow" class="photoAbout">
                  <td class="td_class">
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <img width="80" height="40" id="logo" runat="server" src="" />
                    <asp:HyperLink ID="lnkDelete" runat="server" Style="vertical-align: middle;">
                        【<asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,btnDeleteText %>" />】</asp:HyperLink>
                </td>
            </tr>
            <tr>
                  <td class="td_class">
                    水印位置：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:DropDownList ID="ddlPosition" runat="server" Width="150px">
                        <asp:ListItem Value="WM_CENTER">居中 </asp:ListItem>
                        <asp:ListItem Value="WM_TOP_LEFT">左上角</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_LEFT">左下角</asp:ListItem>
                        <asp:ListItem Value="WM_TOP_RIGHT">右上角</asp:ListItem>
                        <asp:ListItem Value="WM_BOTTOM_RIGHT">右下角</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style=" display:none">
                  <td class="td_class">
                    水印对象：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:CheckBox ID="chkThum" runat="server" Text="缩略图水印" />
                    <asp:CheckBox ID="chkHD" runat="server" Text="清晰图水印" />
                </td>
            </tr>
            <tr>
                  <td class="td_class">
                    水印透明度：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <div style="float: left">
                        <asp:TextBox ID="txtTransparent" runat="server" Width="40px"></asp:TextBox></div>
                    <div id="slider-range-max" style="width: 200px; float: left; margin-left: 10px; margin-top: 7px">
                    </div>
                </td>
            </tr>
            <tr>
                  <td class="td_class">
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                </td>
            </tr>
            <tr>
                  <td class="td_class">
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="adminsubmit_short" OnClick="btnSave_Click" />
                    <asp:Button ID="ReSet" runat="server" Text="重置产品水印" CssClass="adminsubmit" OnClick="ReSet_Click"  Visible="False"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
