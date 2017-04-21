<%@ Page Title="<%$ Resources:Sysmanage,ptptMailSettings%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="MailConfiglist.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.MailConfiglist" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Sysmanage,ptptMailSettings%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblConfiguringWebsiteEmails%>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <%--  <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site,lblAdd%>"/></a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site,btnDeleteListText%>"/></a><b>|</b></li>--%>
                 
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:BoundField DataField="Mailaddress" HeaderText="<%$ Resources:SysManage,fieldMailaddress %>" SortExpression="Mailaddress"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Username" HeaderText="<%$ Resources:Site,fieldUserName%>" SortExpression="Username" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="SMTPServer" HeaderText="<%$ Resources:Sysmanage,lblSMTPServer%>" SortExpression="SMTPServer" />
                <asp:BoundField DataField="SMTPPort" HeaderText="<%$ Resources:Sysmanage,lblSMTPServerPort%>" SortExpression="SMTPPort" />
                <%--<asp:BoundField DataField="POPServer" HeaderText="<%$ Resources:Sysmanage,lblPOPServer%>POP服务器" SortExpression="POPServer" />
            <asp:BoundField DataField="POPPort" HeaderText="<%$ Resources:Sysmanage,lblPOPServerPort%>POP服务器端口" SortExpression="POPPort" />--%>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="35"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                         Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
                <td style="width: 1px;">
                </td>
                <td>
                    <%--<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                            onmouseout="this.className='inputbutton'" OnClick="btnDelete_Click"/>   --%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
