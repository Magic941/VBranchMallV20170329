<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Treelist.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.Treelist" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, ptMenuManage%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage, lbltreeList%>" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, lblParentMenu %>" />：
                    <asp:DropDownList ID="listTarget" runat="server" Width="200px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 状态：<asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="True" Text="启用">
                        </asp:ListItem>
                        <asp:ListItem Value="False" Text="未启用">
                        </asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword %>" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
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
                        <a href="add.aspx?TreeType=<%= TreeType%>">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd %>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="15" DataKeyNames="NodeID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true"
            OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="NodeID" HeaderText="<%$ Resources:SysManage, fieldNodeID %>"
                    SortExpression="NodeID" ControlStyle-Width="50px" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OrderID" HeaderText="<%$ Resources:Site, lblOrder %>"
                    SortExpression="OrderID" ControlStyle-Width="40" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:SysManage, fieldText %>" DataTextField="TreeText"
                    DataNavigateUrlFields="NodeID,TreeType" DataNavigateUrlFormatString="show.aspx?id={0}&TreeType={1}"
                    Text="TreeText" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Url" HeaderText="<%$ Resources:SysManage, fieldUrl %>"
                    SortExpression="Url" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="<%$ Resources:SysManage,fieldEnabled%>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnActivity" runat="server" CommandArgument='<%#Eval("NodeID")%>'
                            Style="color: #0063dc;" CommandName="Enabled">
                                                   <%#(bool)Eval("Enabled") ? " <span style='color:green;'>启用</span>" : "<span  style='color:red;'>未启用<span>"%></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="comment" HeaderText="<%$ Resources:SysManage, fieldcomment %>"
                    SortExpression="comment" ItemStyle-HorizontalAlign="Left" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="NodeID,TreeType" DataNavigateUrlFormatString="Modify.aspx?id={0}&TreeType={1}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                    &nbsp;&nbsp;
                  
                    <asp:DropDownList ID="listTarget2" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="listTarget2_Changed">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
