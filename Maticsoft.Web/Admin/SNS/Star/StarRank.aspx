<%@ Page Title="达人排行表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="StarRank.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.StarRank.List" %>

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
                        <asp:Literal ID="Literal1" runat="server" Text="达人排行管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对达人排行信息进行审核、删除等处理
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
                            <td style="text-align: right; width:100px;">
             达人类型： 
               </td>
               <td>
                 <asp:DropDownList ID="ddStarType" runat="server">
                 </asp:DropDownList>
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                     OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
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
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="Sequence" HeaderText="排行" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="30px"  SortExpression="Sequence"/>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" ImageAlign="Middle"
                            ImageUrl='<%#Eval("UserGravatar")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NickName" HeaderText="达人姓名" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />
                <asp:TemplateField HeaderText="达人类型" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <%#GetStarType(Eval("TypeID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="推荐到首页" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <%#GetboolText(Eval("IsRecommend"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>    
             
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" DataNavigateUrlFields="ID"
                    DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-HorizontalAlign="Left"
                    Visible="false" />
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 500px; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
                <td>
                    <asp:Button ID="btnUpdateState" runat="server" Text="批量审核通过" class="adminsubmit"
                        OnClick="btnUpdateState_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>