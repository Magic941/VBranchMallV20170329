<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="AddWeiXinGroupBuy.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.PromoteSales.AddWeiXinGroupBuy" %>

<%@ Register TagPrefix="Maticsoft" TagName="AjaxRegion" Src="~/Controls/AjaxRegion.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/regionjs.js" type="text/javascript"></script>
    <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <!--SWF图片上传开始-->
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
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
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });
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
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加团购商品操作" />
                    </td>
                </tr>
            </table>
        </div>

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr id="txtCategory">
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="商品分类" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlCateList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCateList_Changed" Width="160px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlCateList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCateList2_Changed" Width="160px" Visible="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="商品" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlProduct" CssClass="select2" runat="server" Width="420px">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="开始时间" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="160" MaxLength="30" Onclick="WdatePicker()">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="结束时间" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEndDate" runat="server" Onclick="WdatePicker()" Width="160" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="团购价格" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPrice" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
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
                                <asp:Literal ID="Literal8" runat="server" Text="总限制购买数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtMaxCount" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="txtMaxCount"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal17" runat="server" Text="账号限购数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPromotionLimitQu" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator5" Display="Dynamic" Type="Integer" ControlToValidate="txtPromotionLimitQu"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <%--最低起订量 默认为1--%>
                         <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal19" runat="server" Text="最低起订量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtLeastbuyNum" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator6" Display="Dynamic" Type="Integer" ControlToValidate="txtLeastbuyNum"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="违约金" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtFinePrice" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="团购满足数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtGroupCount" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" Type="Integer" ControlToValidate="txtGroupCount"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="团购基数" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtGroupBase" runat="server" Width="80px" MaxLength="30" Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator4" Display="Dynamic" Type="Integer" ControlToValidate="txtGroupBase"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="显示顺序" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSequence" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" Display="Dynamic" Type="Integer" ControlToValidate="txtSequence"
                                    MinimumValue="0" MaximumValue="1000000" runat="server" ErrorMessage="请填写数字"></asp:RangeValidator>
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
                        <tr id="Tr1">
                            <td class="td_class">
                                <asp:Literal ID="Literal18" runat="server" Text="微信商品分类" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="dllGoodsTypeID0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dllGoodsTypeID0_SelectedIndexChanged" Width="160px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="dllGoodsTypeID1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dllGoodsTypeID1_SelectedIndexChanged" Width="160px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="dllGoodsTypeID2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dllGoodsTypeID2_SelectedIndexChanged" Width="160px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="dllGoodsTypeID3" runat="server" Width="160px" Visible="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">活动分类：
                            </td>
                            <td height="25">
                                <asp:DropDownList runat="server" ID="DdlGoodsActiveType">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">楼层：
                            </td>
                            <td height="25">
                                <asp:DropDownList runat="server" ID="ddlIsIndex">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:Literal ID="Literal16" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkPromotionType" Text="是否是精品商城商品" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal20" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkIsIndexP" Text="是否是首页推荐" runat="server" Checked="false" />
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
                                <asp:Button ID="btnSave" runat="server" Text="确定"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".select2").select2({ placeholder: "请选择", width: '440px' });
        });
    </script>
</asp:Content>



