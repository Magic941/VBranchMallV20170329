<%@ Page Title="SNS_GradeConfig" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserLog.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.MembershipManage.UserLog" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });

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
                        会员 日志信息管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以查看会员 <span style="color: Blue">【<asp:Literal ID="txtName" runat="server"></asp:Literal>】
                        </span>的操作日志信息。
                        <span style=" float:right; padding-right:18px"><a href="/Admin/Members/MembershipManage/List.aspx">返回</a></span>
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <%--<table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="昵称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="冻结"></asp:ListItem>
                        <asp:ListItem Value="1" Text="激活"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal5" runat="server" Text="注册时间时间："></asp:Literal>
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate"></asp:TextBox>
                    <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>--%>
        <!--Search end-->
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
          OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting"
            UnExportedColumnNames="Modify" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="编号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="40" SortExpression="ID" />
                <asp:TemplateField ItemStyle-Width="120" HeaderText="操作时间" SortExpression="OPTime">
                    <ItemTemplate>
                        <%#Eval("OPTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作地址" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Url")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("OPInfo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作IP" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UserIP")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UserType")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                    <asp:Button ID="btnDelete" runat="server" Text="批量删除" class="adminsubmit" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
