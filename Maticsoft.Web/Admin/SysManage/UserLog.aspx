<%@ Page Title="<%$ Resources:SysManage, ptUserLog%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserLog.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.UserLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ register assembly="Maticsoft.Web" namespace="Maticsoft.Web.Controls" tagprefix="cc1" %>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet"type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
            <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
        $(document).ready(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtDate']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
            //绑定日期控件
            //            var today = new Date();
            //            var year = today.getFullYear();
            //            var month = today.getMonth();
            //            var day = today.getDate();
//            $("[id$='txtDate']").prop("readonly", true).datepicker({
//                numberOfMonths: 1, //显示月份数量
//                onClose: function () {
//                    $(this).css("color", "#000");
//                }
//            }).focus(function () { $(this).val(''); });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, ptUserLog%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, lblConsultOperatelog%>" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblAdd %>" /></a> <b>|</b> </li>--%>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" /></a><b>|</b></li>
               
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true"
            ShowToolBar="True" >
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="<%$ Resources:Site, fieldFeedback_iID%>"
                    SortExpression="ID" ControlStyle-Width="40" />
                <asp:BoundField DataField="OPTime" HeaderText="<%$ Resources:SysManage, fieldOPTime%>"
                    ControlStyle-Width="40" />
                <asp:BoundField DataField="Url" HeaderText="<%$ Resources:SysManage, fieldPageUrl%>"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="OPInfo" HeaderText="<%$ Resources:SysManage, fieldOPInfo%>"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:Site, fieldUserName%>" />
                <asp:BoundField DataField="UserIP" HeaderText="<%$ Resources:Site, lblUserIP%>" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnModifyText %>" ControlStyle-Width="35"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="Modify"
                    Visible="false" />
                <asp:TemplateField ControlStyle-Width="35" HeaderText="<%$ Resources:Site, btnDeleteText%>"
                    Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$ Resources:Site, TooltipDelConfirm%>"
                            Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText%>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:SysManage, lblDeleteBeforeOneDay%>"
                        class="adminsubmit" OnClick="btnDeleteAll_Click" />
                    <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>