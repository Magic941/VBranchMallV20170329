<%@ Page Title="<%$Resources:Poll,ptUsersList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Poll.Users.List" %>

<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptUsersList%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Poll,lblUsersList%>" />
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
                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,FastQuery%>" />：
                    <asp:RadioButton ID="radbtnTrueName" Text="<%$Resources:Site,fieldFeedback_cUserName%>"
                        Checked="true" GroupName="s" runat="server" />
                    <asp:RadioButton ID="radbtnUserID" Text="<%$Resources:Site,fieldFeedback_iID%>" GroupName="s"
                        runat="server" />&nbsp;&nbsp;
                    <asp:TextBox ID="txtKey" runat="server" ToolTip="<%$Resources:Site,lblKeyword%>"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                     
                    <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" OnClick="btnDelete_Click"
                             Text="批量删除" class="adminsubmit"  />
                    </li>
                
            
                   
                 
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="UserID">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="<%$Resources:Site,fieldUserID%>" SortExpression="UserID">
                </asp:BoundField>
                <asp:BoundField DataField="TrueName" HeaderText="<%$Resources:Site,fieldFeedback_cUserName%>" />
                <asp:BoundField DataField="Sex" HeaderText="<%$Resources:Site,fieldSex%>"></asp:BoundField>
                <asp:BoundField DataField="Age" HeaderText="<%$Resources:Site,Age%>"></asp:BoundField>
                <asp:BoundField DataField="Phone" HeaderText="<%$Resources:Poll,PhoneOrEmial%>" ItemStyle-HorizontalAlign="Center">
                </asp:BoundField>
                <asp:HyperLinkField HeaderText="<%$Resources:Poll,ptOptionsShow%>" DataNavigateUrlFields="UserID"
                    DataNavigateUrlFormatString="show.aspx?uid={0}" Text="<%$Resources:Poll,ptOptionsShow%>">
                </asp:HyperLinkField>
                <asp:TemplateField HeaderText="<%$Resources:Site,btnDeleteText%>" ShowHeader="False"
                    Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
          <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </cc1:GridViewEx>
        <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red" Text="<%$Resources:Poll,ErrorNoData %>"></asp:Label>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td align="left">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
