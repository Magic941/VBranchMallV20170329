<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" CodeBehind="WeiBoTaskList.aspx.cs"
    Inherits="Maticsoft.Web.Admin.Ms.WeiBo.WeiBoTaskList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
   <script type="text/javascript">
       $(function () {
           $(".imageUrl").each(function () {
               var src = $(this).attr("src");
               if (src != "") {
                   $(this).show();
               }
           }); 
       });
     
   </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfImage" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfWeiboCount" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微博任务管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="查看设置的微博群发定时任务。" />
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
                <%--    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：--%>
                    &nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="True" DataKeyNames="WeiBoTaskId" Style="float: left;" ShowGridLine="true"
            ShowHeaderStyle="true">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="微博消息" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Maticsoft.Common.StringPlus.SubString(Eval("WeiboMsg"), 100, "...")%>
                             <img src="<%#Eval("ImageUrl")%>" alt="" width="80" height="80" class="imageUrl" style="display: none" />
                    </ItemTemplate>
                </asp:TemplateField>
          
                <asp:TemplateField ControlStyle-Width="50" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("CreateDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="发布时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("PublishDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="80" HeaderText="删除" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton>
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
