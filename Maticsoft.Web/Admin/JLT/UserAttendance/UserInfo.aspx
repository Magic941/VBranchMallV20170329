<%@ Page Title="<%$Resources:MsEnterprise,ptEnterpriseList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits=" Maticsoft.Web.Admin.JLT.UserAttendance.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Literal ID="Literal4" runat="server" Text="用户名" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound"  UnExportedColumnNames="Modify" Width="100%"
            PageSize="15" DataKeyNames="UserID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="False">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="用户ID" SortExpression="UserID"
                                ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName"
                                ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Phone" HeaderText="电话" SortExpression="Phone"
                                ItemStyle-HorizontalAlign="Center" />
                    
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"
                                ItemStyle-HorizontalAlign="Center" />
                 <asp:TemplateField HeaderText="所在单位" ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                            <%#GetCompany(Eval("DepartmentID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                             <a class="iframe" href="javascript:;" onclick="parent.openMap(this);" ref="/admin/JLT/UserAttendance/CollectAttendance.aspx?username=<%# Eval("UserName")%>">查看考勤</a>
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
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>