<%@ Page Title="代理商列表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.AgentInfo.List" %>

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
                        代理商管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除、搜索代理商信息
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
                    <asp:Literal ID="Literal2" runat="server" Text="输入代理商名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist" id="liAdd" runat="server">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="Add.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        OnBind="BindData" OnPageIndexChanging="GridViewEx1_PageIndexChanging" OnRowDataBound="GridViewEx1_RowDataBound" 
        OnRowDeleting="GridViewEx1_RowDeleting" UnExportedColumnNames="Modify" Width="100%" PageSize="10" 
        DataKeyNames="AgentId" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1" 
        CellPadding="3" BorderWidth="1px" ShowCheckAll="False">
            <Columns>
                <asp:TemplateField HeaderText="LOGO" SortExpression="LOGO" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Image ID="imgLOGO" runat="server" Width="30" Height="30" ImageUrl='<%# Eval("LOGO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="UserName" HeaderText="所属用户" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="分类" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetEnteClassName(Eval("CategoryId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性质" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetCompanyType(Eval("CompanyType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ArtiPerson" HeaderText="<%$Resources:MsEnterprise,lblArtiPerson%>" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Introduction" HeaderText="<%$Resources:MsEnterprise,lblIntroduction%>" SortExpression="Introduction" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="RegisteredCapital" HeaderText="<%$Resources:MsEnterprise,lblRegisteredCapital%>" SortExpression="RegisteredCapital" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="TelPhone" HeaderText="<%$Resources:Site,fieldTelphone%>" SortExpression="TelPhone" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="CellPhone" HeaderText="<%$Resources:Site,lblCellphone%>" SortExpression="CellPhone" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="ContactMail" HeaderText="<%$Resources:MsEnterprise,lblContactMail%>" SortExpression="ContactMail" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="RegionId" HeaderText="<%$Resources:MsEnterprise,lblRegionID%>" SortExpression="RegionId" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Address" HeaderText="<%$Resources:Site,address%>" SortExpression="Address" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Remark" HeaderText="<%$Resources:Site,remark%>" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Contact" HeaderText="<%$Resources:MsEnterprise,lblContact%>" SortExpression="Contact" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:BoundField DataField="EstablishedCity" HeaderText="<%$Resources:MsEnterprise,lblEstablishedCity%>" SortExpression="EstablishedCity" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Fax" HeaderText="<%$Resources:MsEnterprise,lblFax%>" SortExpression="Fax" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="PostCode" HeaderText="<%$Resources:MsEnterprise,lblPostCode%>" SortExpression="PostCode" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="HomePage" HeaderText="<%$Resources:MsEnterprise,lblHomePage%>" SortExpression="HomePage" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Rank" HeaderText="<%$Resources:MsEnterprise,lblEnteRank%>" SortExpression="Rank" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="BusinessLicense" HeaderText="<%$Resources:MsEnterprise,lblLicense%>" SortExpression="BusinessLicense" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="TaxNumber" HeaderText="<%$Resources:MsEnterprise,lblTaxRegistration%>" SortExpression="TaxNumber" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="AccountBank" HeaderText="<%$Resources:MsEnterprise,lblAccountBank%>" SortExpression="AccountBank" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="AccountInfo" HeaderText="<%$Resources:MsEnterprise,lblAccountInfo%>" SortExpression="AccountInfo" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="ServicePhone" HeaderText="<%$Resources:MsEnterprise,lblServicePhone%>" SortExpression="ServicePhone" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="QQ" HeaderText="<%$Resources:MsEnterprise,lblQQ%>" SortExpression="QQ" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="MSN" HeaderText="<%$Resources:MsEnterprise,lblMSN%>" SortExpression="MSN" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="<%$Resources:MsEnterprise,lblEstablishedDate%>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DateTimeFormat(Eval("EstablishedDate"), "yyyy-MM-dd", true)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:MsEnterprise,fieldCreatedDate%>" SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField DataField="CreatedUserId" HeaderText="<%$Resources:MsEnterprise,fieldCreatedUserID%>" SortExpression="CreatedUserId" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="UpdatedDate" HeaderText="<%$Resources:MsEnterprise,fieldUpdatedDate%>" SortExpression="UpdatedDate" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="UpdatedUserId" HeaderText="<%$Resources:MsEnterprise,fieldUpdatedUserID%>" SortExpression="UpdatedUserId" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="AgentId" HeaderText="<%$Resources:MsEnterprise,fieldAgentID%>" SortExpression="AgentId" ItemStyle-HorizontalAlign="Center" Visible="false" />
                
                <asp:TemplateField HeaderText="<%$Resources:MsEnterprise,lblStatus%>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" SortExpression="">
                    <ItemTemplate>  
<span id="lbtnModify"  runat="server" >  <a href="Modify.aspx?id=<%# Eval("AgentId")%>">
                            <asp:Literal ID="ltlEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"></asp:Literal></a> 
                            &nbsp; </span>
                            <a href="Show.aspx?id=<%# Eval("AgentId")%>">
                            <asp:Literal ID="ltlDetail" runat="server" Text="<%$ Resources:Site, btnDetailText %>"></asp:Literal></a> &nbsp;
                        <asp:LinkButton ID="linkDel" OnClientClick="return confirm('删除代理商及其代理商的用户信息,'+$(this).attr('ConfirmText'))" ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" CausesValidation="False" CommandName="Delete" Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                        
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
                    <asp:Button ID="btnDelete" Visible="False" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" class="adminsubmit" OnClick="btnDelete_Click" OnClientClick="return confirm('<%$ Resources.MsEnterprise,TooltipDeleteUser%>')" />
                    <asp:Button ID="btnApproveList" Visible="False" runat="server" Text="<%$ Resources:Site, btnApproveListText %>" class="adminsubmit" OnClick="btnApproveList_Click" />
                    <asp:Button ID="btnInverseApprove" runat="server" Text="<%$Resources:Site,btnNotApproveList%>" class="adminsubmit" Visible="False" OnClick="btnInverseApprove_Click" />
                    <asp:Button ID="btnUpdateState" runat="server" Text="<%$Resources:Site,FreezeList%>" class="adminsubmit" Visible="False" OnClick="btnUpdateState_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
