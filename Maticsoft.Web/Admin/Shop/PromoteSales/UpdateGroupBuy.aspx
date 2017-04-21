<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="UpdateGroupBuy.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.PromoteSales.UpdateGroupBuy" %>

<%@ Register TagPrefix="Maticsoft" TagName="AjaxRegion" Src="~/Controls/AjaxRegion.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <%--<script src="/Admin/js/adddate.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/UploadNormalImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'width': 76,
                'height': 25,
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
                    if (response.split('|')[0] == "1") {
                        $("[id$='imgAd']").attr("src", response.split('|')[1].format(''));
                        $("[id$='hfFileUrl']").val(response.split('|')[1]);
                        $("[id$='HiddenField_ISModifyImage']").val("True");
                        alert("图片上传成功");
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });

        function closefrm(vid) {
            alert('商品保存成功');
            parent.closeWindown();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HiddenField_ISModifyImage" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="团购活动管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行编辑团购商品活动操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="商品名称" />：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="所在地" />：
                            </td>
                            <td height="25">
                                <Maticsoft:AjaxRegion runat="server" ID="ajaxRegion" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="开始时间" />：

                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtStartDate" runat="server"  Onclick="WdatePicker()" Width="150" MaxLength="30"></asp:TextBox>
                                <%--<input type="text" id="" onclick="SelectDate(this,'yyyy-MM-dd hh:mm:ss')" />--%>
                                &nbsp;&nbsp;
                                <asp:Literal ID="Literal3" runat="server" Text="结束时间" />：
                                <asp:TextBox ID="txtEndDate" runat="server"  Onclick="WdatePicker()" Width="150" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="团购价格" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPrice" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                                &nbsp;&nbsp;<asp:Literal ID="Literal8" runat="server" Text="限购总数量" />：
                                <asp:TextBox ID="txtMaxCount" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="txtMaxCount"
                                    MinimumValue="0" MaximumValue="1000000" runat="server"  ForeColor="Red" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal17" runat="server" Text="账号限购数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPromotionLimitQu" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                                   <asp:RangeValidator ID="RangeValidator5" Display="Dynamic" Type="Integer" ControlToValidate="txtPromotionLimitQu"
                                    MinimumValue="0" MaximumValue="1000000" runat="server"  ForeColor="Red" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="最少购买数" />：
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtLeastbuyNum" MaxLength="30"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" Display="Dynamic" Type="Integer" ControlToValidate="txtLeastbuyNum"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ForeColor="Red" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                         </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="违约金" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtFinePrice" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                                &nbsp;&nbsp;<asp:Literal ID="Literal11" runat="server" Text="满足数量" />：
                                <asp:TextBox ID="txtGroupCount" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" Type="Integer" ControlToValidate="txtGroupCount"
                                    MinimumValue="0" MaximumValue="1000000" runat="server"  ForeColor="Red" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                    <asp:Literal ID="Literal14" runat="server" Text="显示顺序" />：
                                </td>
                                <td height="25">
                                    <asp:TextBox ID="txtSequence" runat="server" Width="100" MaxLength="30"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Literal ID="Literal16" runat="server" Text="团购基数" />：
                                    <asp:TextBox ID="txtGroupBase" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">商品图片 ：
                            </td>
                            <td height="25">
                                <ul class="product_upload_img_ul" style="display: block">
                                    <li>
                                        <asp:HiddenField ID="hfFileUrl" runat="server" />
                                        <div id="fileQueue">
                                        </div>
                                        <input type="file" name="uploadify" id="uploadify" /><br />
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkStatus" Text="上架" runat="server" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="" />
                            </td>
                            <td height="25">
                                  <asp:CheckBox ID="chkPromotionType" Text="是否是好礼大派送商品" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="活动说明" />
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDesc" runat="server" Width="420px" TextMode="MultiLine" Rows="3"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class"></td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="确定" OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
