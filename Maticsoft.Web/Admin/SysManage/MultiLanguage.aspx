<%@ Page Title="<%$ Resources:SysManage, ptMultiLanguage %>" Language="C#" MasterPageFile="~/Admin/BasicAddSearch.Master"
    AutoEventWireup="true" CodeBehind="MultiLanguage.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.MultiLanguage" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/admin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, ptMultiLanguage%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_TitleButton"
    runat="server">
    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage, lblMultiLanguage%>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_ADD" runat="server">
    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, lblAddMultiLang%>" />:
    <asp:DropDownList ID="dropLanguage" runat="server">
    </asp:DropDownList>
    字段:<asp:TextBox ID="txtMultiLang_cField" runat="server"></asp:TextBox>&nbsp;
    外键值:<asp:TextBox ID="txtMultiLang_iPKValue" runat="server"></asp:TextBox>&nbsp;<asp:RangeValidator
        ID="RangeValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMultiLang_iPKValue" MaximumValue="999999999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
    语言值:<asp:TextBox ID="txtMultiLang_cValue" runat="server"></asp:TextBox>&nbsp;
    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
        class="adminsubmit_short" OnClick="btnSave_Click" />
    <asp:Label ID="lbltip1" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_Search" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
        <tr>
            <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                    OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_Gridview" runat="server">
<br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, lblAdd %>"/></a> <b>|</b> </li>--%>
               <%--<li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"/></a><b>|</b></li>--%>
                             
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="False" AllowSorting="True" ShowToolBar="True"
        AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowCancelingEdit="gridView_RowCancelingEdit"
        OnRowEditing="gridView_RowEditing" OnRowUpdating="gridView_RowUpdating" UnExportedColumnNames="Modify"
        OnRowDeleting="gridView_RowDeleting" Width="100%" PageSize="10" DataKeyNames="MultiLang_iID"
        ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="0"
        BorderWidth="1px">
        <Columns>
            <asp:BoundField DataField="MultiLang_cField" HeaderText="<%$ Resources:SysManage, fieldMultiLang_cField%>"
                ReadOnly="true" SortExpression="MultiLang_cField" />
            <asp:BoundField DataField="MultiLang_iPKValue" HeaderText="<%$ Resources:SysManage, fieldMultiLang_iPKValue%>"
                ReadOnly="true" SortExpression="MultiLang_iPKValue" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MultiLang_cLang" HeaderText="<%$ Resources:SysManage, fieldMultiLang_cLang%>"
                ReadOnly="true" SortExpression="MultiLang_cLang" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="<%$ Resources:SysManage, fieldMultiLang_cValue %>">
                <EditItemTemplate>
                    <asp:TextBox ID="TBDescription" runat="server" Text='<%# Bind("MultiLang_cValue") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBDescription" runat="server" Text='<%# Bind("MultiLang_cValue") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:Site, lblOperation %>" ShowHeader="False"
                ItemStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="<%$ Resources:Site, btnUpdateText %>"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="<%$ Resources:Site, btnCancleText %>"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="<%$ Resources:Site, btnEditText %>"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="<%$ Resources:Site, btnDeleteText  %>"></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
