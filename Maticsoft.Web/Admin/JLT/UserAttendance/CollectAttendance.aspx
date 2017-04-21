<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" 
CodeBehind="CollectAttendance.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.CollectAttendance" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($.getUrlParam('username')) {
                $('.usermode').hide();
            }
            //绑定日期控件
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();
            var day = today.getDate();
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtStartDate'],[id$='txtEndDate']").prop("readonly", true).datepicker({
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title usermode">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看考勤" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以查看考勤信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang usermode">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="用户名" />：
                    <asp:TextBox ID="txtUserName" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Literal ID="Literal9" runat="server" Text="考勤日期" />：
                    <asp:TextBox ID="txtStartDate" runat="server" class="admininput_1" ></asp:TextBox>
                    ---
                    <asp:TextBox ID="txtEndDate" runat="server" class="admininput_1" ></asp:TextBox>
                    <asp:Literal ID="Literal11" runat="server" Text="批复状态" />：
                    <asp:DropDownList ID="DropRevStatus" runat="server" class="dropSelect">
                    </asp:DropDownList>
                    &nbsp
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/list.gif) no-repeat"><a href="javascript:;" onclick="location.reload();">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%" PageSize="100" DataKeyNames="UserID" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="5" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="180px">
                    <ItemTemplate>
                        <%# Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%# GetTrueName(Eval("UserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考勤日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <%# Eval("AttendanceDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考勤时间" ItemStyle-HorizontalAlign="Left"  >
                    <ItemTemplate>
                        <%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <a href="Map.aspx?id=<%# Eval("UserID")%>&date=<%# Eval("AttendanceDate", "{0:yyyy-MM-dd}")%>" >考勤地图</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" CssClass="DataRow" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
