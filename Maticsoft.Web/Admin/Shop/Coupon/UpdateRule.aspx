<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="UpdateRule.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.UpdateRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var cate = $("#ctl00_ContentPlaceHolder1_hfCategory").val();
            var sup = $("#ctl00_ContentPlaceHolder1_hfSupplier").val();
            if (cate.toLowerCase() == "true") {
                $("#txtCategory").show();
            }
            if (sup.toLowerCase() == "true") {
                $("#txtSupplier").show();
            }
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
                    $(".SendCount").hide();
                    $(".Point").show();
                } else {
                    $(".SendCount").show();
                    $(".Point").hide();
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
                        <asp:Literal ID="Literal1" runat="server" Text="优惠券活动管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行设置优惠券条件操作" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfCategory" runat="server" />
        <asp:HiddenField ID="hfSupplier" runat="server" />

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="名称" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="lblName" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="优惠券前缀" />：
                            </td>
                            <td height="25">
                            <asp:Literal ID="lblPreName" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="优惠券卡号长度" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="lblCpLength" runat="server" Text="" />
                                   <span style="color: red;padding-left: 8px">（不包括优惠券前缀）</span>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="有效时间" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="100" MaxLength="30" ></asp:TextBox>——
                                 <asp:TextBox ID="txtEndDate" runat="server" Width="100" MaxLength="30" ></asp:TextBox>
                                <asp:CheckBox ID="chkNoDate" runat="server" />无限期
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="最低消费金额" />：
                            </td>
                            <td height="25">
                                  <asp:Literal ID="lblLimitPrice" runat="server" Text="" />
                                   &nbsp;&nbsp;优惠券面值：
                                   <asp:Literal ID="lblPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr class="SendCount">
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="生成数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSendCount" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                                 <span style="color: red;padding-left: 8px">必须为数字</span>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="生成"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
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
