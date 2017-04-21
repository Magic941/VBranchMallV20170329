<%@ Page Title="Shop_ProductAccessories" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.ProductAccessories.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function() {
            $('id$="btnReturn"').click(function () {
                
            });
        });
        
        function GetDel() {
            $('[id$="btnDelete"]').click();
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
                         <asp:Literal ID="Literal1" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除 <asp:Literal ID="Literal3" runat="server" Text="" />
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
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                    OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                    <asp:Button ID="btnReturn" runat="server" Text="返回"
                    OnClick="btnReturn_Click" class="adminsubmit"></asp:Button>
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx?pid=<%=ProductId %>&acctype=<%=Type %>">添加</a> <b>|</b> </li>
                <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;" onclick="GetDel();">删除</a><b>|</b></li>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server"   AllowSorting="True"
        AutoGenerateColumns="False" OnBind="BindData"  ShowToolBar="false"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%"   ShowExportExcel="false" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="0" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AccessoriesId">
        <columns>                
		<asp:BoundField DataField="Name" HeaderText="组合名称" SortExpression="Name" ItemStyle-HorizontalAlign="Center"  />
         <asp:BoundField DataField="DiscountType" HeaderText="DiscountType" SortExpression="Name" Visible="false"  ItemStyle-HorizontalAlign="Center"  />
		<asp:BoundField DataField="DiscountAmount" HeaderText="优惠额度 " SortExpression="DiscountAmount" ItemStyle-HorizontalAlign="Center"   Visible="false"  /> 
		<asp:BoundField DataField="Stock" HeaderText="库存" SortExpression="Stock" ItemStyle-HorizontalAlign="Center"  Visible="false"  /> 
                          <%--  <asp:TemplateField ControlStyle-Width="50" HeaderText="编辑改组商品"   ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                   <a href="SelectAccessorieNew.aspx?id=<%# Eval("AccessoriesId") %>&pid=<%=ProductId %>&acctype=<%=Type %>">编辑</a>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnEditText %>"   ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                   <a href="Modify.aspx?id=<%# Eval("AccessoriesId") %>&pid=<%=ProductId %>&acctype=<%=Type %>">编辑</a>
                                </ItemTemplate>
                            </asp:TemplateField>
 
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"   ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbtnDel" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
         <HeaderStyle Height="35px" />
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
