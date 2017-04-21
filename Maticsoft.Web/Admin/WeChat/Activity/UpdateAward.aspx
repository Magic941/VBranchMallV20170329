
<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
  CodeBehind="UpdateAward.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.UpdateAward" %>
<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
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
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信活动礼品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行编辑微信活动礼品操作" />
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
                                <asp:Literal ID="Literal5" runat="server" Text="奖品类型" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                       <tr runat="server" id="trGiftName">
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="奖品名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtGiftName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                               <tr runat="server" id="trCoupon">
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="优惠券活动" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlCoupon" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                          <tr runat="server" id="trCount">
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="奖品数量" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="txtCount" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="奖品描述" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAwardDesc" runat="server" Width="320px"  TextMode="MultiLine" Rows="5"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="保存"  OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
