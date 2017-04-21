<%@ Page Title="<%$Resources:MsEnterprise,ptEnterpriseList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Enterprise.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script language="javascript" src="/Scripts/CheckBox.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        企业管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除、搜索企业信息
                    </td>
                </tr>
            </table>
        </div>
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="输入企业名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist" id="liAdd" runat="server">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" ><a href="Add.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="GridViewEx1_PageIndexChanging"
            OnRowDataBound="GridViewEx1_RowDataBound" OnRowDeleting="GridViewEx1_RowDeleting"
            UnExportedColumnNames="Modify" Width="100%" PageSize="10" DataKeyNames="EnterpriseID"
            ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="False">
            <Columns>
                <asp:TemplateField HeaderText="LOGO" SortExpression="LOGO" ItemStyle-HorizontalAlign="Center"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Image ID="imgLOGO" runat="server" Width="30" Height="30" ImageUrl='<%# Eval("LOGO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="分类" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetEnteClassName(Eval("EnteClassID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性质" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetCompanyType(Eval("CompanyType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ArtiPerson" HeaderText="<%$Resources:MsEnterprise,lblArtiPerson%>"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="<%$Resources:MsEnterprise,lblStatus%>"   ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:MsEnterprise,lblEstablishedDate%>"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DateTimeFormat(Eval("EstablishedDate"), "yyyy-MM-dd", true)%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" SortExpression="">
                    <ItemTemplate>
                        <a href="Modify.aspx?id=<%# Eval("EnterpriseID")%>">
                            <asp:Literal ID="ltlEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"></asp:Literal></a>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="地图管理" ItemStyle-HorizontalAlign="Center" SortExpression="">
                    <ItemTemplate>
                       <a href="MapManage.aspx?DepartmentId=<%# Eval("EnterpriseID")%>">
                            <asp:Literal ID="Literal1" runat="server" Text="地图管理"></asp:Literal></a>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" SortExpression="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('删除企业及其企业的用户信息,'+$(this).attr('ConfirmText'))" ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="详细" ItemStyle-HorizontalAlign="Center" SortExpression="">
                    <ItemTemplate>
                       <a href="Show.aspx?id=<%# Eval("EnterpriseID")%>">
                            <asp:Literal ID="ltlDetail" runat="server" Text="<%$ Resources:Site, btnDetailText %>"></asp:Literal></a>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                    <asp:Button ID="btnDelete" Visible="False" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick="return confirm('<%$ Resources.MsEnterprise,TooltipDeleteUser%>')"/>
                    <asp:Button ID="btnApproveList" Visible="False" runat="server" Text="<%$ Resources:Site, btnApproveListText %>"
                        class="adminsubmit" OnClick="btnApproveList_Click" />
                    <asp:Button ID="btnInverseApprove" runat="server" Text="<%$Resources:Site,btnNotApproveList%>"
                        class="adminsubmit" Visible="False" OnClick="btnInverseApprove_Click" />
                    <asp:Button ID="btnUpdateState" runat="server" Text="<%$Resources:Site,FreezeList%>"
                        class="adminsubmit" Visible="False" OnClick="btnUpdateState_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
