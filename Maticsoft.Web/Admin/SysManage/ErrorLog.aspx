<%@ Page Title="<%$ Resources:SysManage, ptErrorlog%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.ErrorLog" %>
<%@ register assembly="Maticsoft.Web" namespace="Maticsoft.Web.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            //绑定日期控件
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();
            var day = today.getDate();
            $("[id$='txtDate']").prop("readonly", true).datepicker({
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
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, ptErrorlog%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, lblConsultErrorlog%>" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
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
                    <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblAdd %>"/>添加</a> <b>|</b> </li>--%>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel" runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" 
                            Text="<%$ Resources:Site, btnDeleteListText %>" onclick="lbtnDelete_Click"></asp:LinkButton> 
                        <b>|</b></li>
                
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="<%$Resources:Sysmanage,fieldID%>" SortExpression="ID"
                    ControlStyle-Width="40" />
                <asp:BoundField DataField="OPTime" HeaderText="<%$Resources:Sysmanage,fieldOPTime%>"
                    SortExpression="OPTime" ControlStyle-Width="40" />
                <asp:BoundField DataField="Loginfo" HeaderText="<%$Resources:Sysmanage,fieldLoginfo%>"
                    SortExpression="Loginfo" />
                             <asp:BoundField DataField="StackTrace" HeaderText="堆栈信息" Visible="False"/>
                    
                <%--<asp:HyperLinkField HeaderText="Loginfo" DataTextField="Loginfo" DataNavigateUrlFields="ID"
                                DataNavigateUrlFormatString="show.aspx?id={0}" Text="Loginfo" ItemStyle-HorizontalAlign="Left" />    --%>
                <asp:BoundField DataField="Url" HeaderText="<%$Resources:Sysmanage,fieldUrl%>" SortExpression="Url"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                         OnClientClick="return confirm($(this).attr('ConfirmText'))" ConfirmText="<%$ Resources:Site,btnIfRemove%>" Text="Delete"></asp:LinkButton>
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
                <td height="10px;"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText%>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" />
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Sysmanage, lblDeleteBeforeOneDay%>"
                        class="adminsubmit" OnClick="btnDeleteAll_Click" />
                    <asp:TextBox ID="txtDate" runat="server" Width="100px"  ></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
