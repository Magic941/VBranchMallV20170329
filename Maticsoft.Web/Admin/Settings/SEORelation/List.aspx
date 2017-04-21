<%@ Page Title="Ms_SEORelation" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.SEORelation.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

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
                        SEO关联设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        在指定范围内出现的相关文字自动加上链接
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="add.aspx">
                        添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat"  id="liDel" runat="server"><a href="javascript:;"  onclick="GetDeleteM()">删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="gridView_RowEditing"
            OnRowUpdating="gridView_RowUpdating" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="RelationID" ShowGridLine="true" ShowHeaderStyle="true">
            <Columns>
                <asp:BoundField DataField="KeyName" HeaderText="链接文字" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="LinkURL" HeaderText="链接地址" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField SortExpression="IsCMS" ItemStyle-HorizontalAlign="Center" HeaderText="CMS">
                    <ItemTemplate>
                        <asp:CheckBox ID="IsCMS" runat="server" Checked='<%# Eval("IsCMS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="IsShop" ItemStyle-HorizontalAlign="Center" HeaderText="Shop">
                    <ItemTemplate>
                        <asp:CheckBox ID="IsShop" runat="server" Checked='<%# Eval("IsShop") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="IsSNS" ItemStyle-HorizontalAlign="Center" HeaderText="SNS" >
                    <ItemTemplate>
                        <asp:CheckBox ID="IsSNS" runat="server" Checked='<%# Eval("IsSNS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="IsComment" ItemStyle-HorizontalAlign="Center"
                    HeaderText="评论">
                    <ItemTemplate>
                        <asp:CheckBox ID="IsComment" runat="server" Checked='<%# Eval("IsComment") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="IsActive" ItemStyle-HorizontalAlign="Center"
                    HeaderText="是否有效">
                    <ItemTemplate>
                        <asp:CheckBox ID="IsActive" runat="server" Checked='<%# Eval("IsActive") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="添加时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" Visible="false"/>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="RelationID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="<%$ Resources:Site, btnDetailText %>" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="RelationID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, lblOperation %>"
                    ItemStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                            Text="<%$ Resources:Site, btnUpdateText %>"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="<%$ Resources:Site, btnCancleText %>"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnModify" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="<%$ Resources:Site, btnEditText %>"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText  %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>