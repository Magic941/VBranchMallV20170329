<%@ Page Title="标签管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Tags.List" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="商品标签管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以新增、修改、删除商品标签" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat; width: 50px;" id="liAdd"  runat="server"><a href="add.aspx"
                        title="添加新的标签">添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" OnClick="lbtnDelete_Click"></asp:LinkButton>
                        <b>|</b></li>
                  
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="TagID" ShowGridLine="true"
            ShowHeaderStyle="true">
            <Columns>
                <asp:BoundField DataField="TagName" HeaderText="名称" SortExpression="TagName" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="标签类型" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetCateName(Eval("TagCategoryId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="推荐到分类标签页"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# IsRecommand(Eval("IsRecommand"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="TagID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="<%$ Resources:Site, btnDetailText %>" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="TagID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="TagID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                <td style="width: 100%">
                    <asp:DropDownList ID="dropIsRecommand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropIsRecommand_Changed">
                        <asp:ListItem Value="-1" Selected="True" Text="设置为..."></asp:ListItem>
                        <asp:ListItem Value="true" Text="推荐"></asp:ListItem>
                        <asp:ListItem Value="false" Text="不推荐"></asp:ListItem>
                    </asp:DropDownList>
     
                    &nbsp;
                    <asp:DropDownList ID="dropStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropStatus_Changed"> 
                        <asp:ListItem Value="-1" Selected="True" Text="--设置为--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不可用"></asp:ListItem>
                        <asp:ListItem Value="1" Text="可用"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="lbtnDelete_Click"  />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
