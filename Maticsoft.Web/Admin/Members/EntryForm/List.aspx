<%@ Page Title="<%$Resources:MsEntryForm,ptMsEntryFormList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Ms.EntryForm.List" %>

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
        <!--Title -->
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:MsEntryForm,ptMsEntryFormList%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:MsEntryForm,lblMsEntryFormList%>" />
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
                    <img src="/admin/Images/icon-1.gif" width="19" height="19" alt="" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Site,State%>" />：
                    <asp:DropDownList ID="dropState" runat="server">
                        <asp:ListItem Value="" Selected="True" Text="<%$Resources:Site,chkAllText%>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:Site,Untreated%>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:Site,Processed%>"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; <b>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：</b>
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short" />
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                      <li style="width: 1px; padding-left: 0px"></li>
                    <li id="li1" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1" runat="server" OnClick="btnBatchManage_Click"
                             Text="批量处理" class="adminsubmit"  />
                    </li>
                  <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button2" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" OnClick="btnDelete_Click"
                             Text="批量删除" class="adminsubmit"  />
                    </li>
                 
                         </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="false" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="<%$Resources:MsEntryForm,lblUsername %>"  ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Age" HeaderText="<%$Resources:Site,Age%>"   ItemStyle-HorizontalAlign="Left"  Visible="false"/>
                <asp:BoundField DataField="Email" HeaderText="<%$Resources:Site,fieldEmail%>"   ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:BoundField DataField="TelPhone" HeaderText="<%$Resources:Site,fieldTelphone%>"  ItemStyle-HorizontalAlign="Left"  Visible="false"/>
                <asp:BoundField DataField="Phone" HeaderText="<%$Resources:Site,lblCellphone%>"    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="QQ" HeaderText="QQ" SortExpression="QQ" ItemStyle-HorizontalAlign="Left"
                    Visible="false" />
                <asp:BoundField DataField="MSN" HeaderText="MSN" SortExpression="MSN" ItemStyle-HorizontalAlign="Left"
                    Visible="false" />
                <asp:BoundField DataField="HouseAddress" HeaderText="<%$Resources:msEntryForm,lblHouseAddress%>"
                    SortExpression="HouseAddress" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:BoundField DataField="CompanyAddress" HeaderText="<%$Resources:msEntryForm,lblCompanyAddress%>"
                    SortExpression="CompanyAddress" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:TemplateField HeaderText="<%$Resources:msEnterprise,lblRegionID%>" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetRegionNameByRID(Eval("RegionId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Site,fieldSex%>" ItemStyle-HorizontalAlign="Center" Visible="true">
                    <ItemTemplate>
                        <%# GetSex(Eval("Sex"))%>
                    </ItemTemplate>
                </asp:TemplateField>               
               <asp:TemplateField HeaderText="申请店铺类型" ItemStyle-HorizontalAlign="Center" Visible="true">
                    <ItemTemplate>
                        <%# GetApplyType(Eval("ApplyType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Site,State%>" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%# GetState(Eval("State"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="审核状态" ItemStyle-HorizontalAlign="Center" Visible="true">
                    <ItemTemplate>
                        <%# GetAppStatus(Eval("AppStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <br />
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <asp:Button ID="btnBatchManage" runat="server" Text="批量处理"
                        class="adminsubmit" OnClick="btnBatchManage_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
