<%@ Page Title="<%$Resources:CMSPhoto,ptClassList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.PhotoClass.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $(".addiframe").colorbox({ iframe: true, width: "575", height: "330", overlayclose: false });
            $(".iframe").colorbox({ iframe: true, width: "575", height: "330", overlayclose: false });
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptClassList %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblClassList %>" />
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
                    <asp:Literal ID="Literal8" runat="server" Text="类别名称" />：
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
                <ul class="list">
                    <li >
                        <input id="Checkbox1" type="checkbox" onclick='$(":checkbox").attr("checked", $(this).attr("checked")=="checked");' /><asp:Literal
                            ID="Literal5" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" runat="server" id="liAdd">
                        <a class="addiframe" href="add.aspx">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></asp:LinkButton><b>|</b></li>
                    
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ClassID">
            <Columns>
                <asp:BoundField DataField="ClassName" HeaderText="<%$Resources:Site,fieldUserDescription %>"
                    SortExpression="ClassName" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="<%$Resources:CMSPhoto,lblParentsClass %>" ItemStyle-Width="25%"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetParentName(Eval("ParentId")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Sequence" HeaderText="<%$Resources:Site,lblOrder %>" SortExpression="Sequence"
                    ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class="iframe" href="Modify.aspx?id=<%#Eval("ClassID") %>">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, btnEditText %>" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton></ItemTemplate>
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
