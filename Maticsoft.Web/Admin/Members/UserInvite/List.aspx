<%@ Page Title="邀请" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserInvite.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {
             $("span:contains('否')").css("color", "#d71345");
             $("span:contains('是')").css("color", "#006400");

             $(".iframe").colorbox({ iframe: true, width: "800", height: "560", overlayClose: false });
         });
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
                         <asp:Literal ID="Literal1" runat="server" Text="邀请" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以删除 <asp:Literal ID="Literal3" runat="server" Text="邀请" />
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
                <img src="../../Images/icon-1.gif" width="19" height="19" />
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <asp:Literal ID="Literal2" runat="server" Text="邀请用户昵称" />：
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
                <ul class="list">
                   
  <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liRevert" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button2"  OnClientClick="return confirm('你确定要删除吗？')" runat="server"  Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click"   />
                    </li>
                    
                </ul>
            </div>
        </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound"   UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="InviteId">
        <columns>                            
		<asp:BoundField DataField="UserId" HeaderText="被邀请用户" SortExpression="UserId" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="UserNick" HeaderText="被邀请用户昵称" SortExpression="UserNick" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="InviteUserId" HeaderText="邀请用户ID" SortExpression="InviteUserId" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="InviteNick" HeaderText="邀请用户昵称" SortExpression="InviteNick" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:TemplateField ItemStyle-Width="140" HeaderText="是否已返利" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%# (bool)Eval("IsRebate")?"是":"否"%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="140" HeaderText="是否是新用户" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%# (bool)Eval("IsNew") ? "是" : "否"%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="140" HeaderText="创建时间" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%# Convert.ToDateTime(Eval("CreatedDate")).ToString("yyyy-MM-dd HH:mm:ss")%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="RebateDesc" HeaderText="返利情况"  ItemStyle-HorizontalAlign="Center"  />    
                            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50" DataNavigateUrlFields="InviteId" DataNavigateUrlFormatString="Show.aspx?id={0}"
                                Text="<%$ Resources:Site, btnDetailText %>"  ItemStyle-HorizontalAlign="Center" />
                        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
        <headerstyle height="35px" />
        <pagerstyle height="25px" horizontalalign="Right" />
        <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
        <rowstyle height="25px" />
        <sortdirectionstr>DESC</sortdirectionstr>
    </cc1:GridViewEx>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
