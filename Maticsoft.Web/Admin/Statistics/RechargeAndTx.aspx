<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="RechargeAndTx.aspx.cs" Inherits="Maticsoft.Web.Admin.Statistics.RechargeAndTx"
    Title="充值提现统计" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(function () {
                $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
                $("[id$=BeginTime]").prop("readonly", true).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    onClose: function (selectedDate) {
                        $("[id$=EndTime]").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("[id$=EndTime]").prop("readonly", true).datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    dateFormat: "yy-mm-dd",
                    onClose: function (selectedDate) {
                        $("[id$=BeginTime]").datepicker("option", "maxDate", selectedDate);
                        $("[id$=EndTime]").val($(this).val());
                    }
                });
            });
        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="LiteralRT" runat="server" Text="充值提现统计" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="LiteralIn" runat="server" Text="您可以查看充值提现的统计信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="5" border="0" style="border-collapse: collapse;
                        margin-left: 2%; margin-top: 10px; margin-bottom: 10px">
                        <tr>
                            <td class="td_class" style="font-weight: bold;">
                                开始时间:
                            </td>
                            <td style="font-weight: bold;">
                                <asp:TextBox ID="BeginTime" runat="server" CssClass="PostDate"></asp:TextBox>
                            </td>
                            <td class="td_class" style="font-weight: bold;width: 60px;">
                                结束时间:
                            </td>
                            <td style="font-weight: bold;">
                                <asp:TextBox ID="EndTime" runat="server" CssClass="PostDate"> </asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnCount" runat="server" Text="统计" OnClick="btnCount_Click" class="adminsubmit">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="text-align: center; font-weight: bold;padding-left: 23%;" colspan="2">
                                次数
                            </td>
                            <td style="text-align: right; font-weight: bold;padding-right: 12%;" colspan="2">
                                总金额
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="width: 150px; text-align: center; font-weight: bold;">
                                充值统计
                            </td>
                            <td height="25" style="text-align: center">
                                <asp:Label ID="lblToalCount1" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right;" colspan="2">
                                &emsp;<asp:Label id="lblToalCount2" style="margin-right: 70px;" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="width: 150px; text-align: center; font-weight: bold;">
                                提现统计
                            </td>
                            <td height="25" style="text-align: center">
                                <asp:Label ID="lblToalAmount1" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right" colspan="2">
                                &emsp;<asp:Label id="lblToalAmount2" style="margin-right: 70px;" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <%--<table  cellspacing="0" cellpadding="5" width="280px" border="0"  style="border-collapse: collapse;margin-left: 5%;margin-top: 10px;margin-bottom: 10px">
                        <tr>
                            <td colspan="2">时间</td>
                            <td><asp:TextBox ID="BeginTime" runat="server" CssClass="PostDate" ></asp:TextBox></td>
                    <td><asp:Literal ID="Literal67" runat="server" Text="--"></asp:Literal></td>
                     <td><asp:TextBox ID="EndTime" runat="server" CssClass="PostDate" > </asp:TextBox></td>
                     <td><asp:Button ID="btnCount" runat="server" Text="统计" OnClick="btnCount_Click"
                         class="adminsubmit"></asp:Button></td>
                   
                        </tr>
                        <tr>
                              <td class="td_class" style="width: auto">&emsp;</td>
                            <td style="text-align: center;font-weight: bold; " colspan="2">次数</td>
                            <td style="text-align: right;font-weight: bold;" colspan="2">总金额</td>
                        </tr>
                        <tr>
                            <td  style="text-align: left;font-weight: bold; " colspan="4">
                                充值统计
                            </td>
                            <td height="25" style="text-align: right">
                                <asp:Label  ID="" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right">
                                &emsp;<asp:Label  ID="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="text-align: left;font-weight: bold; " colspan="4">
                                提现统计
                            </td>
                            <td height="25" style="text-align: center">
                                <asp:Label  ID="" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right">
                                &emsp;<asp:Label  ID="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>--%>
                </td>
            </tr>
        </table>
        <table style="width: 100%; display: none;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnReEdit" runat="server" Text="重新统计" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
