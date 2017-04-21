<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="ModifySpec.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.ModifySpec"
    Title="商品类型管理" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript">
        var midValue;
        $(document).ready(function () {
            midValue = $.getUrlParam("m");
            $("[id$='linDelete']").click(function () {
                $.ajax({
                    url: "/Shopmanage.aspx",
                    type: 'post',
                    dataType: 'text',
                    timeout: 10000,
                    data: {
                        action: "DeleteImage",
                        ValueId: $.getUrlParam("v")
                    },
                    success: function (resultData) {
                        if (resultData == "SUCCESS") {
                            $("[id$='hfFileUrl']").val("");
                            $("[id$='linDelete']").hide();
                            alert("图片删除成功！");
                            return false;
                        } else {
                            alert(resultData);
                            alert("图片删除失败");
                            return false;
                        }
                    },
                    error: function () {
                        alert("执行失败");
                        return false;
                    }
                });
            });
        });
    </script>
       <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSContent.aspx?action=uploadico',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.png',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    var jsonData = eval("(" + response.split('|')[1] + ")");
                    switch (jsonData.Status) {
                        case "OK":
                            $("[id$='hfFileUrl']").val(jsonData.SavePath); //imgInfo
                            $("[id$='imgInfo']").attr("src", jsonData.SavePath);

                            break;
                        case "Failed":
                            alert(jsonData.ErrorMessage);
                            break;
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
                       编辑【<asp:Literal ID="Literal2" runat="server" Text="" />】规格值
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="添加供客户可选的规格,如服装类型商品的颜色、尺码。 " />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                      
                        <tr id="attributeText" runat="server">
                            <td class="td_class">
                                规格值名 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAttributeValue" runat="server" Width="372px" onkeydown="javascript:this.value=this.value.replace('，',',')"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr id="TextMsg"  runat="server">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal" style="width: 200px">
                                    <asp:Literal ID="Literal1" runat="server" Text="规格值1到15个字符！" /></label>
                            </td>
                        </tr>
                        <tr id="UseAttributeImage" runat="server">
                            <td class="td_class">图片地址：
                            </td>
                            <td height="25">
                                <asp:HiddenField ID="hfFileUrl" runat="server" />
                                <div id="fileQueue">
                                </div>
                                <input type="file" name="uploadify" id="uploadify" /><br />
                            </td>
                        </tr>
                        <tr id="imgShow" runat="server">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Image ID="imgInfo" runat="server" Width="23" Height="20" />
                                <asp:LinkButton ID="linDelete" runat="server">【删除】</asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="imgTitle" runat="server">
                            <td class="td_class">
                                图片描述 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtImgTitle" runat="server" Width="372px" onkeydown="javascript:this.value=this.value.replace('，',',')"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="imgMsg" runat="server">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal" style="width: 200px">
                                    <asp:Literal ID="Literal4" runat="server" Text="请将描述控制在1到20个字符之间！" /></label>
                            </td>
                        </tr>


                        <tr>
                            <td style="height: 15px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="保存" title="保存当前属性值" class="adminsubmit_short"  OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short"  OnClientClick="javascript:parent.$.colorbox.close();" title="返回列表页"></asp:Button>
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
</asp:Content>
