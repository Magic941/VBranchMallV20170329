<%@ Page Title="图片标签管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="PhotoTags.aspx.cs" Inherits="Maticsoft.Web.SNS.PhotoTags.List" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="图片标签管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以新增、修改、删除图片标签" />
                    </td>
                </tr>
            </table>
            <!--Title end -->
            <!--Add  -->
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            id="tableAdd" runat="server" style="margin-bottom: 15px;">
            <tr>
                <td width="30px" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                </td>
                <td style="margin-left: 0px; width: 229px;">
                    名称:
                    <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
                </td>
                <td style="margin-left: 0px; width: 110px; display: none">
                    推荐到分类标签页:
                </td>
                <td style="margin-left: 0px; width: 143px; display: none">
                    <asp:RadioButtonList ID="radlIsRecommand" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">推荐</asp:ListItem>
                        <asp:ListItem Value="1">不推荐</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="margin-left: 0px; width: 54px;">
                    状态:
                </td>
                <td style="margin-left: 0px; width: 163px;">
                    <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">可用</asp:ListItem>
                        <asp:ListItem Value="0">不可用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="margin-left: 0px;">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                </td>
            </tr>
        </table>
   
        <!--Add end -->
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
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">批量删除</asp:LinkButton>
                        <b>|</b></li>
                  
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="TagID" OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="TagID" HeaderText="编号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TagName" HeaderText="名称" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="推荐到分类标签页" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRecommandindex" runat="server" CausesValidation="False" CommandName="RecommandIndex"
                            CommandArgument='<%#Eval("TagID")+","+Eval("IsRecommand")%>' Style="color: #0063dc;"><span><%# GetIsRecommand(Eval("IsRecommand"))%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnStatus" runat="server" CausesValidation="False" CommandName="Status"
                            CommandArgument='<%#Eval("TagID")+","+Eval("Status")%>' Style="color: #0063dc;"><span><%#GetStatus(Eval("Status"))%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"
                    SortExpression="CreatedDate" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="TagID" DataNavigateUrlFormatString="PhotoTags.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
                <td style="width: 100%;">
                    <asp:DropDownList ID="dropIsRecommand" runat="server" Visible="false">
                        <asp:ListItem Value="-1" Selected="True" Text="--设置为--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="推荐"></asp:ListItem>
                        <asp:ListItem Value="1" Text="不推荐"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnRecommand" runat="server" Visible="false" Text="<%$ Resources:Site, btnBatchText %>"
                        class="adminsubmit" OnClick="btnRecommand_Click" />
                  
                    <asp:DropDownList ID="dropStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropStatus_Changed">
                        <asp:ListItem Value="-1" Selected="True" Text="设置为..."></asp:ListItem>
                        <asp:ListItem Value="1" Text="可用"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不可用"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="lbtnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>