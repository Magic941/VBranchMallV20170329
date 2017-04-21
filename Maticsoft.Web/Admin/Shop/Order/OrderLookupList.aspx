<%@ Page Title="订单可选项管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="OrderLookupList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Order.OrderLookupList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="订单可选项管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对网站订单可选项进行添加，编辑，删除，设置订单可选项内容等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <asp:HiddenField ID="txtLookupListId" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="选择名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="选择方式" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlType" runat="server">
                                    <asp:ListItem Value="1">下拉列表</asp:ListItem>
                                    <asp:ListItem Value="2">单选按钮</asp:ListItem>
                                    <asp:ListItem Value="3">复选框</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="备注" />：
                            </td>
                            <td height="50">
                                <asp:TextBox ID="tDesc" runat="server" Width="250px" TextMode="MultiLine" Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" class="adminsubmit_short">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%--<div class="newslist">
            <div class="newsicon">
                <ul>
                    <li></li>
                </ul>
            </div>
        </div>--%>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="LookupListId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <columns>
                    <asp:TemplateField HeaderText="选项名称" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="选择方式" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#GetModeName(Eval("SelectMode"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Maticsoft.Common.StringPlus.SubString(Eval("Description"), 300, "...")%>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField ControlStyle-Width="40" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <a href="OrderLookupItem.aspx?id=<%#Eval("LookupListId") %>">设置可选项内容</a> &nbsp;&nbsp;
                           <asp:LinkButton ID="linkModify" runat="server" CausesValidation="False" CommandName="OnUpdate"
                            Text="编辑" CommandArgument='<%#Eval("LookupListId") %>'> </asp:LinkButton>
                             &nbsp;&nbsp;
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
            <footerstyle height="25px" horizontalalign="Right" />
            <headerstyle height="35px" />
            <pagerstyle height="25px" horizontalalign="Right" />
            <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
            <rowstyle height="25px" />
            <sortdirectionstr>DESC</sortdirectionstr>
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
