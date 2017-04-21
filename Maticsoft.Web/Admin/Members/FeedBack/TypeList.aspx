<%@ Page Title="<%$ Resources:SysManage,ptFeedbackList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="TypeList.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.FeedBack.TypeList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <script type="text/javascript">
     
    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="反馈类型管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对反馈类型进行添加，修改和删除操作" />
                    </td>
                </tr>
            </table>
        </div>  

           <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
          <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                  <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"  runat="server" id="LiAdd"  ><a href="AddType.aspx">
                        添加</a> <b>|</b> </li>
                        <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click">批量删除</asp:LinkButton><b>|</b>
                    </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3" OnRowDeleting="gridView_RowDeleting"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="TypeId">
            <Columns>
                       <asp:TemplateField HeaderText="类型名称" ItemStyle-HorizontalAlign="center"  SortExpression="TypeName">
                    <ItemTemplate>
                        <%# Eval("TypeName")%></ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="类型描述" ItemStyle-HorizontalAlign="center"  SortExpression="Description">
                    <ItemTemplate>
                        <%# Maticsoft.Common.StringPlus.SubString(Eval("Description"),30,"...")%></ItemTemplate>
                </asp:TemplateField>
                     
            <asp:TemplateField HeaderText="编辑" ItemStyle-Width="15%"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate> <a href="UpdateType.aspx?id=<%#Eval("TypeId") %>" style="color: Blue">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除" ItemStyle-Width="15%"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Blue"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                  <%--  <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceCheckright" runat="server">
</asp:content>
