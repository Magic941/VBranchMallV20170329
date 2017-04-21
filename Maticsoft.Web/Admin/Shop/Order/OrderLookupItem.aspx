<%@ Page Title="订单可选项内容管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="OrderLookupItem.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Shop.Order.OrderLookupItem" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var value = $("#ctl00_ContentPlaceHolder1_Required_Y").attr("checked");
            if (value == "checked") {
                $("#txtUserTitle").show();
            }
            $("#ctl00_ContentPlaceHolder1_Required_Y").click(function () {
                $("#txtUserTitle").show();
            });
            $("#ctl00_ContentPlaceHolder1_Required_N").click(function () {
                $("#txtUserTitle").hide();
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtTitle" runat="server" Text="订单可选项内容管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="txtDesc" runat="server" Text="您可以对网站订单可选项内容进行添加，编辑，删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <asp:HiddenField ID="txtLookupItemId" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="选项内容名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="需要用户填写信息" />：
                            </td>
                            <td height="25">
                                <asp:RadioButton ID="Required_Y" Text="是" GroupName="IsRequired" runat="server" />
                                <asp:RadioButton ID="Required_N" Text="否" GroupName="IsRequired" runat="server" Checked="True" />
                            </td>
                        </tr>
                        <tr id="txtUserTitle" style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="用户填写信息的标题" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tTitle" TabIndex="1" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="附加金额" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tAppendMoney" runat="server" Width="250px" MaxLength="20">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="附加金额计算方式" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlCalculateMode" runat="server">
                                    <asp:ListItem Value="1">固定金额</asp:ListItem>
                                    <asp:ListItem Value="2">金额百分比</asp:ListItem>
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
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="LookupItemId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <columns>
                <asp:TemplateField HeaderText="可选内容名称" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="附加金额" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#GetMoneyInfo(Eval("AppendMoney"), Eval("CalculateMode"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户信息标题" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#Eval("InputTitle")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Maticsoft.Common.StringPlus.SubString(Eval("Remark"), 300, "...")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="40" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkModify" runat="server" CausesValidation="False" CommandName="OnUpdate"
                            Text="编辑" CommandArgument='<%#Eval("LookupItemId") %>'> </asp:LinkButton>
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
