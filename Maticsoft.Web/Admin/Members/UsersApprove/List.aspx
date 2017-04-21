<%@ Page Title="Accounts_UsersApprove" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UsersApprove.List" %>

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
                        用户实名认证管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        审核用户上传的认证信息
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
                    <asp:DropDownList ID="ddlApproveStatus" runat="server">
                        <asp:ListItem Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">审核失败</asp:ListItem>
                    </asp:DropDownList>
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
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ApproveID">
            <Columns>
                <asp:TemplateField  HeaderText="正/反面照" ItemStyle-HorizontalAlign="Center"   SortExpression="ApproveID">
                    <ItemTemplate>
                        <a href='<%#Eval("FrontView") %>' target="_blank"><img src='<%#Eval("FrontView") %>' alt="身份证正面照" title="身份证正面照" width="140" height="85"/></a> &nbsp;<a href='<%#Eval("RearView") %>' target="_blank"><img src='<%#Eval("RearView") %>' alt="身份证背面照" title="身份证背面照"  width="140" height="85"/></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="用户名"  ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="150px"/>
                <asp:BoundField DataField="TrueName" HeaderText="真实姓名" SortExpression="TrueName"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px" />
                <asp:BoundField DataField="IDCardNum" HeaderText="身份证号码" SortExpression="IDCardNum"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"/>
                
                <asp:BoundField DataField="DueDate" HeaderText="身份证过期时间" SortExpression="DueDate"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField ItemStyle-Width="80px" HeaderText="审核状态" ItemStyle-HorizontalAlign="Center"   SortExpression="Status">
                    <ItemTemplate>
                        <%#GetApproveStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ApproveUserID" HeaderText="审核人" SortExpression="ApproveUserID"
                    ItemStyle-HorizontalAlign="Center"  Visible="false" />
                <asp:BoundField DataField="CreatedDate" HeaderText="认证日期" SortExpression="CreatedDate" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ApproveDate" HeaderText="审核时间" SortExpression="ApproveDate" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center"  />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ItemStyle-Width="100px"
                    DataNavigateUrlFields="ApproveID" DataNavigateUrlFormatString="Show.aspx?id={0}"  Text="查看详细" ItemStyle-HorizontalAlign="Center"   SortExpression="ApproveID"/>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                <td>
                    <asp:Button ID="btnBatchAccess" runat="server" Text="批量审核" class="adminsubmit" OnClick="btnBatchAccess_Click" />
                    <asp:Button ID="btnBatchUnAcc" runat="server" Text="批量拒绝" class="adminsubmit" 
                        onclick="btnBatchUnAcc_Click"  />
                    <asp:Button ID="btnDelete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" Text="批量删除" class="adminsubmit" OnClick="btnDelete_Click"  />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>