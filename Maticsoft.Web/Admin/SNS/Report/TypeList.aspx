<%@ Page Title="举报类型管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="TypeList.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.ReportType.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //Add
            $(".Add,.Modify").colorbox({ iframe: true, width: "450px", height: "360px", overlayClose: false });
            $("span:contains('设为可用')").css("color", "black");
            $("span:contains('取消可用')").css("color", "#006400");
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="举报类型管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以新增、修改、删除举报类型" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody"  style="float:left">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    </td>
                    <td  style="float:left">
                    <asp:DropDownList ID="rdoable" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="0">不可用</asp:ListItem>
                        <asp:ListItem Value="1">可用</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="float:left">
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" runat="server"  id="LiAdd"><a href="TypeAdd.aspx" 
                        class="Add">添加</a> <b>|</b> </li>
                  
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="50" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"   OnRowCommand="gridView_RowCommand" 
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID" ShowGridLine="true"
            ShowHeaderStyle="true">
            <Columns>
                <asp:BoundField DataField="TypeName" HeaderText="名称" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnID" runat="server" CausesValidation="False" CommandName="Status" CommandArgument='<%#Eval("ID")+","+Eval("Status")%>' Style="color: #0063dc;" ><span ><%#Eval("Status").ToString() == "0" ? "设为可用" : "取消可用"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink CssClass="Modify" ID="lkEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"
                            NavigateUrl='<%#Eval("ID", "TypeModify.aspx?id={0}")%>' ForeColor="blue"></asp:HyperLink>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="lbtnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Visible="False"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
