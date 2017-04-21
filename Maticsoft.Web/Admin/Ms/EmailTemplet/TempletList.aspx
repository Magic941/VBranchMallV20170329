
<%@ Page Title="邮件模板管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="TempletList.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.EmailTemplet.TempletList" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
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
                        <asp:Literal ID="Literal1" runat="server" Text="邮件模板管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="邮件模板提供系统生成邮件所需的格式和样式，您可以结合每个模板提供的标签自行修改邮件模板" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                       <li  runat="server"  id="liadd" style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px;"><a href="AddTemplet.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="添加"/></a> <b>|</b> </li>    
                 </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="TempletId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true">
            <Columns>
              <asp:BoundField DataField="TempletId" HeaderText="模板编号" SortExpression="TempletId"
                    ControlStyle-Width="20" ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="EmailType" HeaderText="模板类型" SortExpression="EmailType"
                    ControlStyle-Width="40" ItemStyle-HorizontalAlign="center" />
                     <asp:BoundField DataField="EmailSubject" HeaderText="模板名称" SortExpression="EmailSubject"
                    ControlStyle-Width="120" ItemStyle-HorizontalAlign="center" />
                  
                         <asp:BoundField DataField="EmailDescription" HeaderText="模板描述" 
                   ItemStyle-HorizontalAlign="center" />
                    <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="40"
                    DataNavigateUrlFields="TempletId" DataNavigateUrlFormatString="UpdateTemplet.aspx?type={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="40" HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"  CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton></ItemTemplate>
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


