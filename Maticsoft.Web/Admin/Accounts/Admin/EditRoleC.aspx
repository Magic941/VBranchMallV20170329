<%@ Page Title="<%$ Resources:Site, ptEditRole %>" Language="C#" MasterPageFile="~/Admin/BasicAddSearch.Master"
    AutoEventWireup="true" CodeBehind="EditRoleC.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.Admin.EditRoleC" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Title" runat="server">
    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, ptEditRole%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_TitleButton"
    runat="server">
    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, lblRoleRedactOperate%>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_ADD" runat="server">
    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, lblNewRoleName %>" />:
    <asp:TextBox ID="TxtNewname" runat="server" Width="120px"></asp:TextBox>&nbsp;
    <asp:Button ID="BtnUpName" runat="server" Text="<%$ Resources:Site, btnUpdateText %>"
        OnClick="BtnUpName_Click" class="adminsubmit_short" />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="RemoveRoleButton" runat="server" Text="<%$ Resources:Site, btnDeleteText %>"
        class="adminsubmit_short" OnClick="RemoveRoleButton_Click"></asp:Button>
    <asp:Label ID="lblTiptool" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnBackText %>"
        OnClick="btnBach_ServerClick" class="adminsubmit_short"></asp:Button>
    <asp:Label ID="lblRoleID" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_Search" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
        <tr>
            <td width="0%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                <%--<img src="/Admin/Images/icon-1.gif" width="19" height="19" />--%>
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <table cellpadding="5" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="center" height="25" colspan="2">
                            <b>
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Site, lblRole%>" />:
                                <asp:Label ID="RoleLabel" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="Top" width="250px">
                            <b>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblCategory%>" />
                            </b>:
                            <asp:ListBox ID="CategoryDownList" runat="server" AutoPostBack="True" Height="200px"
                                Width="150px" OnSelectedIndexChanged="CategoryDownList_SelectedIndexChanged">
                            </asp:ListBox>
                        </td>
                        <td valign="Top" align="left">
                            <asp:CheckBox ID="chkAll" runat="server" Text="<%$ Resources:Site, chkAllText%>" OnCheckedChanged="chkAll_CheckedChanged"
                                AutoPostBack="true" /><br />
                            <asp:CheckBoxList ID="chkPermissions" runat="server" RepeatColumns="5" CellPadding="3"
                                OnDataBound="chkPermissions_DataBinding" align="left">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_class">
                        </td>
                        <td height="25">
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                            <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnBackText %>"
                                OnClientClick="javascript:history.go(-1);return false;" class="adminsubmit_short">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder_Gridview" runat="server">
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd %>"/></a> <b>|</b> </li>--%>
               <%--<li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Site, btnDeleteListText%>"/></a><b>|</b></li>--%>
                                  
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" DataKeyNames="UserID" ShowExportExcel="False" ShowExportWord="False"
        ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true">
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:Site, fieldUserName %>" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                        <%# Eval("UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TrueName" HeaderText="<%$ Resources:Site, fieldTrueName %>"
                SortExpression="TrueName" ControlStyle-Width="40" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Phone" HeaderText="<%$ Resources:Site, fieldTelphone %>"
                SortExpression="Phone" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField DataField="Email" HeaderText="<%$ Resources:Site, fieldEmail %>"
                SortExpression="Email" ItemStyle-HorizontalAlign="Left" />
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
                <asp:Button ID="btnRemove" runat="server" Text="<%$ Resources:Site, btnRemoveText %>" OnClientClick="return confirm('你确认要移除么？')" class="adminsubmit" OnClick="btnRemove_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
