<%@ Page Title="微信导航菜单管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="CommandList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Command.CommandList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".iframe").colorbox({ iframe: true, width: "800", height: "524", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信导航菜单管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="微信导航菜单管理" />
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
                    <asp:Literal ID="Literal3" runat="server" Text="状态" />：
                    <asp:DropDownList ID="dropStatus" runat="server" Width="200px">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不可用"></asp:ListItem>
                        <asp:ListItem Value="1" Text="可用"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server" Text="关键字" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a class="various" href='AddCommand.aspx'>
                            <asp:Literal ID="Literal8" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="CommandId">
            <Columns>
                <asp:TemplateField HeaderText="导航菜单名称" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="导航菜单描述" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Remark")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="系统指令功能" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# GetAction(Eval("ActionId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查询目标" ItemStyle-HorizontalAlign="center" ItemStyle-Width="160">
                    <ItemTemplate>
                        <%# GetTarget(Eval("TargetId"), Eval("ActionId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="解析类型" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# GetParseType(Eval("ParseType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="解析长度" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("ParseLength")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="字符串" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("ParseChar")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <a href='Update.aspx?id=<%# Eval("CommandId")%>' class="iframe">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
               <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClick="btnDelete_Click" CssClass="adminsubmit" OnClientClick="return confirm('你确认要删除么？')"/>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
