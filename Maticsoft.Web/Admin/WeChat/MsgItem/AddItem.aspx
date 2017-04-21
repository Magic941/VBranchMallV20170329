<%@ Page Title="系统默认消息管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddItem.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.MsgItem.AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script type="text/javascript">
        $("#uploadify").uploadify({
            'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
            'script': '/WeChatImg.aspx',
            'cancelImg': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',   //指定取消上传的图片，默认‘cancel.png’ 
            'wmode': 'transparent',
            'hideButton': true,
            'auto': true,               //选定文件后是否自动上传，默认false 
            'multi': false,               //是否允许同时上传多文件，默认false 
            'fileDesc': '图片文件', //出现在上传对话框中的文件类型描述 
            'fileExt': '*.jpg;*.bmp;*.png;*.gif',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc 
            'sizeLimit': 2097152,          //控制上传文件的大小，单位byte 
            'onComplete': function (event, queueID, fileObj, response) {
                var responseJSON = $.parseJSON(response);
                if (responseJSON.success) {
                    $(event.target).parent().css("background", backgtoundImgBase.format(responseJSON.data.format('T_')));
                    $("[id$='ImagePath']").val(responseJSON.data);
                }
            },
            'onError': function (event, ID, fileObj, errorObj) {
                alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信系统默认消息管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="设置微信默认回复信息的内容。" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
            cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr class="news_s">
                            <td class="td_class">
                                标题：
                            </td>
                            <td height="25" style="width: 320px">
                                <asp:TextBox ID="txtTitle" runat="server" Width="350"></asp:TextBox>
                            </td>
                            <td height="25" style="padding-left: 5px;" rowspan="3">
                                <ul class="product_upload_img_ul" style="display: block">
                                    <li>
                                        <div class="ImgUpload ">
                                            <asp:HiddenField ID="ImagePath" runat="server" />
                                            <span id="Span2" class="cancel" style="display: none; z-index: 999999"><a class="DelImage"
                                                href="javascript:void(0);">删除</a></span> <span class="file_uploadUploader" style="width: 127px;
                                                    height: 128px; overflow: hidden; background-image: url(/admin/images/AddImage.gif)">
                                                    <input type="file" class="file_upload" id="uploadify" />
                                                </span>
                                        </div>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        
                        <tr class="news_s">
                            <td class="td_class">
                                描述：
                            </td>
                            <td height="25" colspan="3">
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="346" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="news_s" style="display: none">
                            <td class="td_class">
                                链接地址：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlUrl" runat="server" Width="245px">
                                    <asp:ListItem Value="0">自定义网址</asp:ListItem>
                                    <asp:ListItem Value="1">微信商城</asp:ListItem>
                                    <asp:ListItem Value="2">微官网</asp:ListItem>
                                    <asp:ListItem Value="3">我的账户</asp:ListItem>
                                    <asp:ListItem Value="4">我的订单</asp:ListItem>
                                    <asp:ListItem Value="5">商品分类</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="ddCategory" runat="server" Width="245px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="txt_s">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="Button1" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
