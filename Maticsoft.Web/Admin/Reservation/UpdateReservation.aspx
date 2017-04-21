<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="UpdateReservation.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Reservation.UpdateReservation" %>

<%@ Register TagPrefix="Maticsoft" Src="~/Controls/AjaxRegion.ascx" TagName="AjaxRegion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtContactPhone]").OnlyNum();
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$=txtEndDate]").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                minDate: new Date(),
                onClose: function (selectedDate) {
                    $("[id$=txtEndDate]").val($(this).val());
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
                        <asp:Literal ID="Literal1" runat="server" Text="修改用户预约" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行修改用户预约操作" />
                    </td>
                </tr>
            </table>
        </div>
       <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                预约商家 ：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="drpSupplier" CssClass="select2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                服务类型 ：
                            </td>
                            <td height="25">
                                <select id="ddlType" name="Rank" cssclass="select2" style="width: 205px" runat="server">
                                    <option value="-1">--请选择--</option>
                                    <option value="1">KTV</option>
                                    <option value="2">酒店</option>
                                    <option value="3">看房</option>
                                </select>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                预约日期 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                服务名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                预约内容 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContent" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                                当前状态 ：
                            </td>
                            <td height="25">
                                <select id="ddlStatus" name="Rank" cssclass="select2" style="width: 205px" runat="server">
                                    <option value="-2">系统锁定</option>
                                    <option value="-1">用户锁定</option>
                                    <option value="0">未处理</option>
                                    <option value="1">取消</option>
                                    <option value="2">活动</option>
                                    <option value="3">已处理</option>
                                    <option value="4">已过期</option>

                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                所在地 ：
                            </td>
                            <td height="25">
                               <%-- <Maticsoft:AjaxRegion runat="server" ID="ajaxRegion" />--%>
                                <script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
                                    <asp:HiddenField ID="hfSelectedNode" runat="server" />
                                    <script src="/Scripts/jquery/maticsoft.selectregion.js" handle="/RegionHandle.aspx"
                                        isnull="true" type="text/javascript"></script>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                详细地址 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系人 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContactName" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtContactName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系电话 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContactPhone" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtContactPhone"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                电子邮件 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                备注 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit_short" 
                                    onclick="btnSave_Click">
                                </asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="取消" class="adminsubmit_short"  CausesValidation=false>
                                </asp:Button>
                               
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
</asp:Content>
