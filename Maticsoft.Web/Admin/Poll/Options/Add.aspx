<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Options.Add" Title="<%$Resources:Poll,ptOptionsAdd %>" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptOptionsAdd %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Poll,lblOptionsAdd %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center">
            <table cellspacing="0" cellpadding="5" width="100%" border="0" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td height="22" align="left" class="style1">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    【<asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>】<asp:Literal ID="Literal3"
                                        runat="server" Text="<%$Resources:Poll,lblOptionsQuestions %>" /><asp:Label ID="lblTopicID"
                                            runat="server" Visible="false" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblOptionsName %>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td class="td_class">
                                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Poll,lblIsChecked%>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:CheckBox ID="chkisChecked" runat="server" />
                                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Poll,lblDefaultCheched%>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                            </td>
                                            <td height="25" align="left">
                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:Site,btnOKText%>" OnClick="btnAdd_Click"
                                                    class="adminsubmit_short"></asp:Button>&nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="<%$Resources:Site,btnBackText%>" OnClick="btnBack_Click"
                                                    class="adminsubmit_short" CausesValidation="False"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px;display:none;"><a href="add.aspx">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblAdd%>" /></a>
                        <b>|</b> </li>
                    <li id="liDel" runat="server" style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;"><a
                        href="javascript:;" onclick="GetDeleteM()">
                        <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,btnDeleteListText%>" /></a><b>|</b></li>
          
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="ID"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="<%$Resources:Site,fieldFeedback_iID%>"
                    SortExpression="ID"></asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="<%$Resources:Poll,lblOptionsName%>"
                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                <asp:BoundField DataField="isChecked" HeaderText="<%$Resources:Poll,lblIsChecked%>">
                </asp:BoundField>
                <asp:BoundField DataField="SubmitNum" HeaderText="<%$Resources:Poll,NumberOfVotes%>">
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$Resources:Site,btnDeleteText%>" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
        </cc1:GridViewEx>
        <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red" Text="<%$Resources:Poll,ErrorNoData%>"></asp:Label>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td align="left">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
