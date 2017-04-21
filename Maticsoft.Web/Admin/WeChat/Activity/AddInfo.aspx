<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.AddInfo" %>

<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
       <link href="/Admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            var backgtoundImgBase = "url('{0}') no-repeat center center";
            $("[name$='rblLimitType']").click(function () {
                var value = $(this).val();
                if (value == 1) {
                    $(".claDay").show();
                    $(".claAll").hide();
                }
                else {
                    $(".claDay").hide();
                    $(".claAll").show();
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtStartDate").prop("readonly", true).datepicker({
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtEndDate").prop("readonly", true).datepicker({
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtStartDate").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").val($(this).val());
                }
            });
            $("#uploadify").uploadify({
                'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
                'script': '/WeChatImg.aspx',
                'cancelImg': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',   //指定取消上传的图片，默认‘cancel.png’ 
                'wmode': 'transparent',
                'hideButton': true,
                'width': 128,
                'height': 127,
                'auto': true,               //选定文件后是否自动上传，默认false 
                'multi': false,               //是否允许同时上传多文件，默认false 
                'fileDesc': '图片文件', //出现在上传对话框中的文件类型描述 
                'fileExt': '*.jpg;*.bmp;*.png;*.gif',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc 
                'sizeLimit': 2097152,          //控制上传文件的大小，单位byte 
                'onComplete': function (event, queueID, fileObj, response) {
                    var responseJSON = $.parseJSON(response);
                    if (responseJSON.success) {
                        $(event.target).parent().css("background", backgtoundImgBase.format(responseJSON.data.format('T_')));
                        $("[id$='hfPath']").val(responseJSON.data);
                    }
                },
                'onError': function (event, ID, fileObj, errorObj) {
                    alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信活动管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加微信活动操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td  >
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="活动名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="活动前缀" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPreName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="有效时间" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="100" MaxLength="30"></asp:TextBox>—
                                <asp:TextBox ID="txtEndDate" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                                <asp:CheckBox ID="chkNoDate" runat="server" />无限期
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="抽奖规则" />：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rblLimitType" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="0" Text="按活动"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="按天数" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class='claAll' style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text=" 每人总共参与次数" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUserTotal" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class='claDay'>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="每人每天参与次数" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEachCount" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <span style="color: Gray; padding-left: 8px">每个人 每天参与次数 推荐只设置1次!</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="活动每天限制次数" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDayCount" runat="server" Width="80px" MaxLength="30" Text="0"></asp:TextBox>
                                <span style="color: Gray; padding-left: 8px">默认 0 为不限制 </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="中奖概率" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtProbability" runat="server" Width="80px" MaxLength="30"></asp:TextBox>%
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="奖项类型" />：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="0" Text="自定义" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="商城优惠券"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="活动图片" />：
                            </td>
                            <td height="128" >
                                <ul class="product_upload_img_ul" style="display: block">
                                    <li style=" margin-left:0px">
                                        <div class="ImgUpload ">
                                       <asp:HiddenField ID="hfPath" runat="server" />
                                            <span id="Span1" class="cancel" style="display: none; z-index: 999999; height: 128px">
                                                <a class="DelImage" href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader"
                                                    style="width: 127px; height: 128px; overflow: hidden; background-image: url(/admin/images/AddImage.gif)">
                                                    <input type="file" id="uploadify" />
                                                </span>
                                        </div>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="活动简介" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSummary" runat="server" Width="320px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="密码" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlPwd" runat="server" Width="80">
                                    <asp:ListItem Value="6" Text="6位" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7位"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8位"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkIsPwd" Text="需要密码" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="添加" OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
