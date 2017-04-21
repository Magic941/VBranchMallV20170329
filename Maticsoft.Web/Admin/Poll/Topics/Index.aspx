<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="Maticsoft.Web.Admin.Topics.Index" Title="<%$Resources:Poll,ptOptionsIndex %>" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptOptionsIndex %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Poll,lblOptionsIndex %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center">
            <table cellspacing="0" cellpadding="5" width="100%" border="0" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="5" width="100%">
                            <tr>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Poll,lblQuestionnaire %>" />
                                    【<asp:Label ID="lblFormName" runat="server" Font-Bold="true" Text=""></asp:Label>】
                                    <asp:Label ID="lblFormID" runat="server" Visible="false" Text="Label"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblTopicsTitle%>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Poll,lblTopicsType%>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:RadioButtonList ID="radbtnType" runat="server" RepeatDirection="Horizontal"
                                                    align="left">
                                                    <asp:ListItem Value="0" Selected="True" Text="<%$Resources:Poll,lblRadio%>"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="<%$Resources:Poll,lblCheck%>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$Resources:Poll,lblFeedBack%>"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                            </td>
                                            <td height="25" align="left">
                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:Site,lblAdd%>" OnClick="btnAdd_Click" class="adminsubmit_short">
                                                </asp:Button>
                                                &nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="<%$Resources:Site,btnBackText%> " OnClick="btnBack_Click" CausesValidation="False"
                                                    class="adminsubmit_short"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,FastQuery %>" />：
                    <asp:RadioButton ID="radbtnTrueName" Text="<%$Resources:Site,lblTitle %>" Checked="true" GroupName="s" runat="server" />
                    <asp:RadioButton ID="RadioButton2" Text="<%$Resources:Site,fieldFeedback_iID %>" GroupName="s" runat="server" />&nbsp;&nbsp;
                    <asp:TextBox ID="txtKey" runat="server" ToolTip="<%$Resources:Site,lblKeyword %>"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                   
                    <li id="liDel" runat="server"  style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;"><a
                        href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
               
                </ul>
            </div>
        </div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                        ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
                        Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
                        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="<%$Resources:Site,fieldFeedback_iID %>" SortExpression="ID"></asp:BoundField>
                            <asp:BoundField DataField="Title" HeaderText="<%$Resources:Site,lblTitle %>" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="Type" HeaderText="<%$Resources:Poll,lblType %>"></asp:BoundField>
                            <asp:HyperLinkField HeaderText="<%$Resources:Poll,lblEditingOptions %>" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../options/add.aspx?tid={0}"
                                Text="<%$Resources:Poll,lblEditingOptions %>"></asp:HyperLinkField>
                            <asp:TemplateField HeaderText="<%$Resources:Site,btnDeleteText %>" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>" Text="<%$Resources:Site,btnDeleteText %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" />
        <HeaderStyle Height="35px" />
                        <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </cc1:GridViewEx>
                    <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Poll,ErrorNoData%>" /></asp:Label></td></tr></table><table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
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
