<%@ Page Title="专辑类型管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="TypeList.aspx.cs" Inherits="Maticsoft.Web.SNS.AlbumType.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" >
    $(function () {
        $("span:contains('设为可用')").css("color", "black");
        $("span:contains('取消可用')").css("color", "#006400");
        $("span:contains('设为菜单')").css("color", "black");
        $("span:contains('取消菜单')").css("color", "#006400");
        $("span:contains('显示')").css("color", "black");
        $("span:contains('取消显示')").css("color", "#006400");
    });

    function GetDeleteM() {
        $("[id$='btnDelete']").click();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="专辑类型管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 您可以新增、修改、删除专辑类型" />
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" runat="server"  ID="AddLi" >
                        <asp:HyperLink ID="hlkadd" runat="server" NavigateUrl="Typeadd.aspx" >添加</asp:HyperLink>
                     <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                   
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"  OnRowCommand="gridView_RowCommand"  
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="MenuSequence" HeaderText="顺序" SortExpression="MenuSequence"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TypeName" HeaderText="名称"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AlbumsCount" HeaderText="专辑数" SortExpression="AlbumsCount"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="是否菜单"  ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnIsMenu" runat="server" CausesValidation="False" CommandName="IsMenu" CommandArgument='<%#Eval("ID")+","+Eval("IsMenu")%>' Style="color: #0063dc;" ><span ><%#(bool)Eval("IsMenu") ? "取消菜单" : "设为菜单"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否频道页显示"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnMenuIsShow" runat="server" CausesValidation="False" CommandName="MenuIsShow" CommandArgument='<%#Eval("ID")+","+Eval("MenuIsShow")%>' Style="color: #0063dc;" ><span ><%#(bool)Eval("MenuIsShow") ? "取消显示" : "显示"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnID" runat="server" CausesValidation="False" CommandName="ID" CommandArgument='<%#Eval("ID")+","+Eval("Status")%>' Style="color: #0063dc;" ><span ><%#Eval("Status").ToString() == "0" ? "设为可用" : "取消可用"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="备注"  ItemStyle-HorizontalAlign="Left"  />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                    ItemStyle-HorizontalAlign="Center"  Visible="false"/>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="TypeModify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"  ItemStyle-HorizontalAlign="Center">
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 200px; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
