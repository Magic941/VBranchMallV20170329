<%@ Page Title="<%$ Resources:Site, ptRoleAssignment%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="EnterpriseRoleAssignment.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Enterprise.EnterpriseRoleAssignment" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:MsEnterprise,lblEnterpriseRole %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:MsEnterprise,lblEnterpriseRole %>" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Search -->
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td style="width: 80px" align="right" class="tdbg">
                    <b>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,lblKeyword%>" /></b>
                </td>
                <td class="tdbg">
                    <asp:TextBox ID="txtKeyword" runat="server">
                    </asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dropState" runat="server" Height="22px">
                        <asp:ListItem Value="-1" Text="<%$Resources:Site,PleaseSelect%>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:Site,Unaudited%>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:Site,Normal%>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$Resources:Site,Freeze%>"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="adminsubmit_short" Text="<%$Resources:Site,btnQueryText%>"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
                <td class="tdbg">
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                 
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
            ShowToolBar="True" OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px"
            DataKeyNames="EnterpriseID" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false"
            PageSize="10" RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated"
            CheckColumnVAlign="Middle" ShowCheckAll="False">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="<%$Resources:MsEnterprise,lblEnterpriseName %>"
                    SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TelPhone" HeaderText="<%$Resources:Site,fieldTelphone %>"
                    SortExpression="TelPhone" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CellPhone" HeaderText="<%$Resources:Site,lblCellphone %>"
                    SortExpression="CellPhone" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ContactMail" HeaderText="<%$Resources:MsEnterprise,lblContactMail %>"
                    SortExpression="ContactMail" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contact" HeaderText="<%$Resources:MsEnterprise,lblContact %>"
                    SortExpression="Contact" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UserName" HeaderText="<%$Resources:Site,fieldUserName %>"
                    SortExpression="UserName" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:MsEnterprise,lblStatus%>">
                    <ItemTemplate>
                        <asp:Label ID="Status" runat="server" Text='<%#GetStatus(Eval("Status")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:MsEnterprise,lblEstablishedDate %>"
                    SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$Resources:MsEnterprise,lblSetRole %>" ControlStyle-Width="60"
                    DataNavigateUrlFields="EnterpriseID" DataNavigateUrlFormatString="~/Admin/Accounts/Admin/UserRoleAssignment.aspx?DepartmentId={0}&UserType=EE"
                    Text="<%$Resources:Site,lblSet%>" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,btnDeleteText %>"
                    Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$Resources:Site,btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
               <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
