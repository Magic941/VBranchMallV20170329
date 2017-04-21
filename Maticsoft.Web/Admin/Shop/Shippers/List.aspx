<%@ Page Title="Shop_Shippers" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shippers.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="发货人信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除 <asp:Literal ID="Literal3" runat="server" Text="发货人信息" />
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
                <asp:Literal ID="Literal2" runat="server" Text="发货人名称" />：
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
                <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">添加</a> <b>|</b> </li>
                <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;" onclick="GetDel();">删除</a><b>|</b></li>
                <li style="background: url(/admin/images/list.gif) no-repeat"><a href="list.aspx">浏览</a><b>|</b></li>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ShipperId" OnRowCommand="gridView_RowCommand">
        <columns>                  
		<asp:BoundField DataField="IsDefault" HeaderText="是否默认" Visible="false" SortExpression="IsDefault" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ShipperTag" HeaderText="发货人标签" SortExpression="ShipperTag" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ShipperName" HeaderText="发货人名称" SortExpression="ShipperName" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="CellPhone" HeaderText="手机" SortExpression="CellPhone" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Zipcode" HeaderText="邮编" SortExpression="Zipcode" ItemStyle-HorizontalAlign="Center"  /> 
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, lblOperation %>"    ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <a href="Modify.aspx?id=<%# Eval("ShipperId")%>" >编辑</a>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                                    <asp:LinkButton ID="linkIsDefault" runat="server" CommandArgument='<%#Eval("ShipperId")+","+((bool)Eval("IsDefault")?false:true)%>'  CommandName="IsDefault"  > 
                                         <span><%#(bool)Eval("IsDefault") ? "取消默认" : "设置默认"%></span>
                                    </asp:LinkButton>
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
                <asp:Button ID="btnDelete" runat="server" OnClientClick="return  confirm('您确认要删除么？');" Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click" />
            </td>
        </tr>
    </table></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        function GetDel() {
            $('[id$=btnDelete]').click();
        }
    </script>
</asp:Content>
