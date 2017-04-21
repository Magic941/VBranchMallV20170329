<%@ Page Title="SNS_GradeConfig" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.ActivityUser.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet"type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
         <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
           
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });

        })
    
  
    </script>
<script type="text/javascript" >
    $(function () {
        $("span:contains('已冻结')").css("color", "red");
        $("span:contains('已激活')").css("color", "#006400");
        $("span:contains('未认证')").css("color", "red");
        $("span:contains('已认证')").css("color", "#006400");
    })
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        会员信息管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以查看会员的认证、申请达人等信息。
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
                    <asp:Literal ID="Literal2" runat="server" Text="昵称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:DropDownList ID="dropUnActive" runat="server">
                        <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3个月"></asp:ListItem>
                        <asp:ListItem Value="6" Text="6个月"></asp:ListItem>  
                        <asp:ListItem Value="9" Text="6个月"></asp:ListItem>
                    </asp:DropDownList>未活跃的用户
                    &nbsp;&nbsp;&nbsp;&nbsp;

                     <asp:DropDownList ID="dropActive" runat="server">
                        <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3个月"></asp:ListItem>
                        <asp:ListItem Value="6" Text="6个月"></asp:ListItem>  
                        <asp:ListItem Value="9" Text="6个月"></asp:ListItem>
                    </asp:DropDownList>活跃的用户
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--                    <asp:Literal ID="Literal5" runat="server" Text="注册时间时间："></asp:Literal>
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate"></asp:TextBox>
                    <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate"></asp:TextBox>--%>
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
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"  OnRowCommand="gridView_RowCommand" 
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="UserID">
            <Columns>
               <asp:BoundField DataField="UserID" HeaderText="用户编号"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20" SortExpression="UserID"/>
                <asp:BoundField DataField="UserName" HeaderText="用户名"    ItemStyle-HorizontalAlign="Left"   ItemStyle-Width="100"/>
                <asp:BoundField DataField="NickName" HeaderText="昵称"   ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="100"/>
                <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#Eval("TrueName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField DataField="AblumsCount" HeaderText="专辑数"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="AblumsCount"/>
                       <asp:BoundField DataField="FansCount" HeaderText="粉丝数"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="FansCount"/> 
                       <asp:BoundField DataField="FellowCount" HeaderText="关注数"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="FellowCount"/>
                       <asp:BoundField DataField="ProductsCount" HeaderText="分享商品数"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="ProductsCount"/>
                       <asp:BoundField DataField="FavouritesCount" HeaderText="喜欢数"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="FavouritesCount"/>
                       <asp:BoundField DataField="Points" HeaderText="积分"    ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="60" SortExpression="Points"/>
                  <asp:BoundField DataField="User_dateCreate" HeaderText="注册时间"   ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100" SortExpression="User_dateCreate"/>
                  <asp:BoundField DataField="LastLoginTime" HeaderText="最后登录时间"   ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100" SortExpression="LastLoginTime"/>
                    <asp:BoundField DataField="LastAccessTime" HeaderText="最后访问时间"   ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100" SortExpression="LastAccessTime"/>
                <asp:TemplateField HeaderText="用户状态" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnID" runat="server" CausesValidation="False" CommandName="Status" CommandArgument='<%#Eval("UserID")+","+Eval("Activity")%>' Style="color: #0063dc;" ><span ><%#(bool)Eval("Activity") ? "已激活" : "已冻结"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnActivity" runat="server" Text="批量激活"
                        class="adminsubmit" OnClick="btnActivity_Click" />
                </td>

                 <td>
                    <asp:Button ID="btnUnActivity" runat="server" Text="批量冻结"
                        class="adminsubmit" OnClick="btnUnActivity_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>