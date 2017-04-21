<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="Maticsoft.Web.Forms.Index" Title="<%$Resources:Poll,ptFormsIndex %>" %>

<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
        $(function () {
            $("[id$='txtFormID']").OnlyNum();
        })
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptFormsIndex %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="管理网站的在线投票调查，您可以新建投票或查看原有投票数据。这里允许创建多个投票调查，但只能设定一个在前台显示。" />
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
                                <td height="22">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Poll,lblFormsName %>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                    ErrorMessage="*" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblFormsExplain %>" />：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                                    ErrorMessage="*" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Text="是否有效" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                            </td>
                                            <td height="25" align="left">
                                                <asp:Button ID="btnAdd" runat="server" Text="<%$Resources:Site,lblAdd%>" OnClick="btnAdd_Click"
                                                    class="adminsubmit_short" ValidationGroup="A"></asp:Button>
                                                <div>
                                                </div>
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
        <div align="center" style="margin-top: 15px;">
            <table cellspacing="0" cellpadding="5" width="100%" border="0" class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td height="22">
                                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
  
                                        <tr>
                                            <td class="td_class">
                                                启用的问卷编号：
                                            </td>
                                            <td height="25" width="*" align="left">
                                                <asp:TextBox ID="txtFormID" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_class">
                                            </td>
                                            <td height="25" align="left">
                                                <asp:Button ID="btnSaveFormID" runat="server" Text="<%$Resources:Site,btnSaveText%>" OnClick="btnSaveFormID_Click"
                                                    class="adminsubmit_short" ></asp:Button>
                                                <div>
                                                </div>
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
        <div align="center">
            <br />
            <div class="newslist">
                <div class="newsicon">
                    <ul>
                        <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="add.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                            <b>|</b> </li>
                        <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;"  id="liDel" runat="server"><a
                            href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                     
                    </ul>
                </div>
            </div>
            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
                Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
                CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="FormID">
                <Columns>
                    <asp:BoundField DataField="FormID" HeaderText="<%$Resources:Site,fieldFeedback_iID %>"
                        SortExpression="FormID" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="<%$Resources:Site,Name %>" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#IsActive(Eval("IsActive"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="<%$Resources:Site,Describe %>"
                        ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:HyperLinkField HeaderText="<%$Resources:Poll,EditTopic %>" DataNavigateUrlFields="FormID"
                        DataNavigateUrlFormatString="../Topics/index.aspx?fid={0}" Text="<%$Resources:Poll,EditTopic %>" ItemStyle-HorizontalAlign="Center">
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="编辑" DataNavigateUrlFields="FormID"
                        DataNavigateUrlFormatString="Modify.aspx?fid={0}" Text="编辑" ItemStyle-HorizontalAlign="Center">
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="<%$Resources:Poll,ViewResults %>" DataNavigateUrlFields="FormID"
                        DataNavigateUrlFormatString="../Options/showcount.aspx?fid={0}" Text="<%$Resources:Poll,ViewResults %>" ItemStyle-HorizontalAlign="Center">
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="<%$Resources:Site,btnDeleteText %>" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                OnClientClick="return confirm($(this).attr('ComfirmText'))" ComfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                                Text="<%$Resources:Site,btnDeleteText %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                    <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
                <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </cc1:GridViewEx>
            <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red">
                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Poll,ErrorNoData%>" /></asp:Label><table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
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
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center">
            <%--<br />
            <table cellspacing="0" cellpadding="5" width="700" border="0">
                <tr>
                    <td align="left">
                        <img src="/Admin/Images/msg.gif" />
                        <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Poll,TooltipUrl%>" />：
                        http://stock.gupk.com/poll1.aspx?fid=<asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Poll,lblFormsID%>" />
                    </td>
                </tr>
            </table>--%>
            <br />
        </div>
    </div>
    <%--    <uc1:copyright ID="Copyright1" runat="server" />
    <uc2:checkright ID="Checkright1" runat="server" />--%>
</asp:Content>