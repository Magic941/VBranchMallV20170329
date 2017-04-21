<%@ Page Title="<%$ Resources:Site, ptUserAdmin %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserTypeAdmin.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.UserTypeAdmin" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "500", height: "340", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, ptUserTypeAdmin%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Sysmanage, lblUserTypeManageOperate%>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a  class='iframe'  href="AddUserType.aspx">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                        <b>|</b> </li>
                    <%-- <li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, btnDeleteListText%>"/></a><b>|</b></li>--%>
                   
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDeleting="gridView_RowDeleting" OnRowDataBound="gridView_OnRowDataBound"
            UnExportedColumnNames="Modify" Width="100%" PageSize="10" DataKeyNames="UserType"
            ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="0"
            BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="UserType" HeaderText="<%$ Resources:Site, fieldUserType %>"
                    SortExpression="UserType" ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Site, fieldUserDescription %>"
                    SortExpression="Description" ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <a  class='iframe'  href="UserTypeUpdate.aspx?UserType=<%#Eval("UserType") %>">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, btnEditText%>" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick="return confirm($(this).attr('ConfirmText'))" ConfirmText="您确认要删除吗？"
                            Text="删除"></asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
