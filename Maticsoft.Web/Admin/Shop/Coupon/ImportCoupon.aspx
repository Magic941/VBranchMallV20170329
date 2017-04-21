<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="ImportCoupon.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.ImportCoupon" %>

<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtStartDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtEndDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtStartDate").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtEndDate").val($(this).val());
                }
            });
            $("[id$='chkExchange']").click(function () {
                var isCheck = $(this).attr("checked");
                if (isCheck == "checked") {
                    $(".Point").show();
                } else {
                    $(".Point").hide();
                }
            });

            $("[id$='chkDate']").click(function () {
                var isCheck = $(this).attr("checked");
                if (isCheck == "checked") {
                    $(".date").show();
                } else {
                    $(".date").hide();
                }
            });

            $("[id$='chkLimitPrice']").click(function () {
                var isCheck = $(this).attr("checked");
                if (isCheck == "checked") {
                    $(".limitPrice").show();
                } else {
                    $(".limitPrice").hide();
                }
            });

            $("[id$='chkPrice']").click(function () {
                var isCheck = $(this).attr("checked");
                if (isCheck == "checked") {
                    $(".price").show();
                } else {
                    $(".price").hide();
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
                        <asp:Literal ID="Literal1" runat="server" Text="导入优惠券数据" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="通过导入线下优惠券数据，实现线上线下的业务融合。" />
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
                                <asp:Literal ID="Literal5" runat="server" Text="名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <a href="/Upload/Template/ImportCoupon.xls" style=" color:Red; font-weight:bold">优惠券Excel模版下载 </a>
                            </td>
                        </tr>
                        <tr>
                           <td class="td_class">
                    Excel数据文件：
                </td>
                <td height="25">
                    <asp:FileUpload ID="uploadExcel" runat="server" CssClass="uploadExcel" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="uploadExcel"
                        runat="server" ErrorMessage="请选择正确的格式" ValidationExpression="^.+(xls|xlsx)$"></asp:RegularExpressionValidator>
                </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="提示" />：
                            </td>
                            <td height="25" style="color: Red">
                                如果开启了下面的相关设置，会更新优惠券导入的优惠券对应的值。
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkDate" Text="启用有效期设置" runat="server" />
                            </td>
                        </tr>
                        <tr class="date" style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="有效时间" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="100" MaxLength="30"></asp:TextBox>——
                                <asp:TextBox ID="txtEndDate" runat="server" Width="100" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkLimitPrice" Text="启用最低消费金额设置" runat="server" />
                            </td>
                        </tr>
                        <tr class="limitPrice" style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="最低消费金额" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtLimitPrice" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkPrice" Text="启用优惠券面值设置" runat="server" />
                            </td>
                        </tr>
                        <tr class="price" style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="优惠券面值" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPrice" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="" />
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkExchange" Text="启用积分兑换" runat="server" />
                            </td>
                        </tr>
                        <tr class="Point" style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal17" runat="server" Text="兑换所需积分" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPoint" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                                <span style="color: red; padding-left: 8px">必须为数字</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="导入" OnClick="btnSave_Click" class="adminsubmit_short">
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
