<%@ Page Title="缩略图尺寸列表" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="ThumSizeList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Ms.ThumbnailSize.ThumSizeList" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="缩略图尺寸管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对网站缩略图尺寸进行添加，编辑，删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 100%; margin-bottom: 15px;" cellpadding="2" cellspacing="1"
            class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="系统模块" />：
                            </td>
                            <td height="25">
                                <cc1:ConfigAreaList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_Change">
                                </cc1:ConfigAreaList>
                                模板名称：
                                <asp:DropDownList ID="ddlTheme" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="缩略图标识符" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="宽度" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tWidth" TabIndex="2" runat="server" Width="100px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" Type="Integer" ControlToValidate="tWidth"
                                    MinimumValue="1" MaximumValue="10000" runat="server" ErrorMessage="数字在1-10000之间"></asp:RangeValidator>
                                高度：
                                <asp:TextBox ID="tHeight" TabIndex="3" runat="server" Width="100px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="tHeight"
                                    MinimumValue="1" MaximumValue="10000" runat="server" ErrorMessage="数字在1-10000之间"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal51" runat="server" Text="裁剪模式" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlThumMode" runat="server">
                                    <asp:ListItem Value="0">Auto自动缩放</asp:ListItem>
                                    <asp:ListItem Value="1">指定高宽裁减（不变形）</asp:ListItem>
                                    <asp:ListItem Value="2">指定高，宽按比例</asp:ListItem>
                                    <asp:ListItem Value="3">指定高宽缩放（可能变形）</asp:ListItem>
                                    <asp:ListItem Value="4">指定宽，高按比例</asp:ListItem>
                                    <%--   <asp:ListItem Value="3">Tao</asp:ListItem>
                                    <asp:ListItem Value="4">COM</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="是否加水印" />：
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkWatermark" runat="server" />是
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="对应云存储尺寸名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tCloudSizeName" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="描述" />：
                            </td>
                            <td height="50">
                                <asp:TextBox ID="tDesc" runat="server" Width="250px" TextMode="MultiLine" Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tradd" runat="server">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
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
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 15px;">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/admin/images/list.gif" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal91" runat="server" Text="类型" />：
                    <cc1:ConfigAreaList ID="ddAreaType" runat="server" VisibleAll="True" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlType_Change2">
                    </cc1:ConfigAreaList>
                    模板名称：
                    <asp:DropDownList ID="ddlTheme2" runat="server">
                        <asp:ListItem Value=""> 全部</asp:ListItem>
                    </asp:DropDownList>
                    <%--         <asp:Literal ID="Literal9" runat="server" Text="关键字" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>--%>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="ThumName"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true">
            <Columns>
                <asp:BoundField DataField="ThumName" HeaderText="缩略图标识符" ControlStyle-Width="40"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="ThumWidth" HeaderText="缩略图宽度" SortExpression="ThumWidth"
                    ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="ThumHeight" HeaderText="缩略图高度" SortExpression="ThumHeight"
                    ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Theme" HeaderText="模板名称" SortExpression="Theme" ControlStyle-Width="40"
                    ItemStyle-HorizontalAlign="center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="系统模块" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetTypeName(Eval("Type"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="裁剪方式" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetModeName(Eval("ThumMode"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="描述" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Maticsoft.Common.StringPlus.SubString(Eval("Remark"),100,"...")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CloudSizeName" HeaderText="云存储尺寸名称" SortExpression="CloudSizeName"
                    ControlStyle-Width="40" ItemStyle-HorizontalAlign="Left" />
                <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="40" DataNavigateUrlFields="ThumName"
                    DataNavigateUrlFormatString="UpdateThumSize.aspx?Name={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="40" HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
