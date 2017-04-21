<%@ Page Title="<%$ Resources:Site, ptUserAdmin %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.UserAdmin" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Site, ptUserAdmin %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Sysmanage, lblUserinfoOp %>" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:DropDownList ID="DropUserType" runat="server" class="dropSelect">
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />:</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox><asp:Button ID="btnSearch"
                                runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"
                                class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="adduser.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                        <b>|</b> </li>
                  </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="UserID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Site, fieldUserName %>" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%# Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField ControlStyle-Width="50" DataNavigateUrlFields="UserID" HeaderText="<%$ Resources:Site,fieldSetRoles%>"
                    DataNavigateUrlFormatString="RoleAssignment.aspx?UserID={0}" Text="<%$ Resources:Site,fieldSetRoles%>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Phone" HeaderText="<%$ Resources:Site, fieldTelphone %>"  ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Email" HeaderText="<%$ Resources:Site, fieldEmail %>"  ItemStyle-HorizontalAlign="Left" />  
                <asp:TemplateField HeaderText="<%$ Resources:Site, fieldUserType %>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                            <%# GetTypeName(Eval("UserType"))%>
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField HeaderText="<%$ Resources:SysManage,fieldEmployeeID %>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                            <%# (Eval("EmployeeID") != null && Eval("EmployeeID").ToString()!="-1") ? Eval("EmployeeID") :""%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:SysManage,fieldActivity%>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# GetboolText(Eval("Activity").ToString())%></ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="UserUpdate.aspx?userid={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton></ItemTemplate>
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