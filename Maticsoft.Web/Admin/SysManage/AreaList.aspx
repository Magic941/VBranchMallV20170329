<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master"
    CodeBehind="AreaList.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.AreaList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "480", height: "380", overlayClose: false });
            $("span:contains('启用')").css("color", "#006400");
            $("span:contains('不启用')").css("color", "red");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="系统路由区域管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对系统路由区域添加、删除管理" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="区域名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkStatus" runat="server" Checked="True" />
                                启用
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
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
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li id="Li1" runat="server"></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="False" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            OnRowCommand="gridView_RowCommand"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AreaName">
            <Columns>
                <asp:TemplateField HeaderText="区域名称" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# Eval("AreaName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                               <asp:LinkButton ID="lbtnID" runat="server" CausesValidation="False" CommandName="Status"
                            CommandArgument='<%#Eval("AreaName")+","+Eval("Status")%>' Style="color: #0063dc;">
                            <span ><%#Maticsoft.Common.Globals.SafeInt(Eval("Status"),0)== 1 ? "启用" : "不启用"%></span>
                            </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClick="btnDelete_Click" CssClass="adminsubmit" />
        <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAction_Changed">
            <asp:ListItem Value="" Text="请选择..."></asp:ListItem>
            <asp:ListItem Value="1" Text="批量启用"></asp:ListItem>
            <asp:ListItem Value="2" Text="批量禁用"></asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
