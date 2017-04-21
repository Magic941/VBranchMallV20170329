<%@ Page Title="充值管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Pay.RechargeRequest.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
          $(function () {
              $("span:contains('无效')").css("color", "red");
              $("span:contains('有效')").css("color", "green");
 
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
                         <asp:Literal ID="Literal1" runat="server" Text="充值记录" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以删除 <asp:Literal ID="Literal3" runat="server" Text="充值记录" />
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
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, User_UserName%>" />：
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
                  <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button2" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" OnClick="btnDelete_Click"
                             Text="批量删除" class="adminsubmit"  />
                    </li>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound"   UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="0" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="RechargeId">
        <columns>
         <asp:TemplateField ItemStyle-Width="140" HeaderText="充值时间" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%# Convert.ToDateTime(Eval("TradeDate")).ToString("yyyy-MM-dd HH:mm:ss")%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="80" HeaderText="充值金额" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span>
                       ￥<%# Convert.ToDecimal(Eval("RechargeBlance")).ToString("F")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField   HeaderText="用户" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span>
                        <%#Eval("UserName")%></span> 
                </ItemTemplate>
            </asp:TemplateField>                    
        <asp:TemplateField ItemStyle-Width="60" HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                          <span ><%#(int)Eval("Status")==1?"有效":"无效"%> </span>                   
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="60" HeaderText="交易类型" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                          <span ><%#GetTradetype((int)Eval("Tradetype"))%> </span>                   
                </ItemTemplate>
            </asp:TemplateField>
		<asp:BoundField DataField="Name" HeaderText="支付网关名称" SortExpression="Name" ItemStyle-HorizontalAlign="Center"  /> 
                           
                        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
        <headerstyle height="35px" />
        <pagerstyle height="25px" horizontalalign="Right" />
        <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
        <rowstyle height="35px" />
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
