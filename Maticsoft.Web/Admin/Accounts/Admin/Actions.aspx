<%@ Page Title="<%$ Resources:Site, ptActions %>" Language="C#" MasterPageFile="~/Admin/BasicAddSearch.Master"
    AutoEventWireup="true" CodeBehind="Actions.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.Actions" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("[id$='divAdd']").length <= 0) {
                $("#HFISSHOW").parent().parent().parent().parent().hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblActionManage %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_TitleButton"
    runat="server">
    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage,lblActionExplain%>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_ADD" runat="server">
<input  type="hidden" id="HFISSHOW"/>
    <div class="newslistabout"  id="divAdd" runat="server">
        <b>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblAdd%>" />:</b>
        <asp:TextBox ID="txtDescription" runat="server" class="inputtext" Width="300px"></asp:TextBox>&nbsp;&nbsp;
        对应权限:<asp:DropDownList ID="DropListCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropListCategory_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="DropListPermissions" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
            class="adminsubmit_short" OnClick="btnSave_Click" />
        <asp:Label ID="lblToolTip" runat="server" Text=""></asp:Label></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_Search" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
        <tr>
            <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                <asp:TextBox ID="txtKeywords" runat="server" class="inputtext"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                    class="adminsubmit_short" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_Gridview" runat="server">
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblAdd %>" /></a> <b>|</b> </li>--%>
                <%--<li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" /></a><b>|</b></li>--%>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowCancelingEdit="gridView_RowCancelingEdit"
        OnRowDeleting="gridView_RowDeleting" OnRowEditing="gridView_RowEditing" OnRowUpdating="gridView_RowUpdating"
        UnExportedColumnNames="Modify" Width="100%" PageSize="10" DataKeyNames="ActionID"
        ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="0"
        ShowCheckAll="true" BorderWidth="1px">
        <Columns>
            <asp:BoundField DataField="ActionID" HeaderText="<%$ Resources:SysManage, fieldActionID %>"
                ReadOnly="true" SortExpression="ActionID" ControlStyle-Width="40" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="<%$ Resources:SysManage, fieldDescription %>" SortExpression="Description" ItemStyle-HorizontalAlign="Left">
                <EditItemTemplate>
                    <asp:TextBox ID="TBDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PermissionID" HeaderText="<%$ Resources:SysManage, fieldPermission %>"
                ReadOnly="true" SortExpression="PermissionID" ControlStyle-Width="40" ItemStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="<%$ Resources:Site, lblOperation %>" ShowHeader="False"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="<%$ Resources:Site, btnUpdateText %>"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="<%$ Resources:Site, btnCancleText  %>"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="<%$ Resources:Site, btnEditText  %>"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                        CommandArgument='<%# Eval("ActionID")%>' Text="<%$ Resources:Site, btnDeleteText%>"></asp:LinkButton>
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
                <asp:DropDownList ID="DropListCategory2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropListCategory2_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DropListPermissions2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropListPermissions2_Changed">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
