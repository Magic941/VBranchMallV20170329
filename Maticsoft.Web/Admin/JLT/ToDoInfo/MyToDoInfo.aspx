<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="MyToDoInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.ToDoInfo.MyToDoInfo" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
<script src="../../js/tab.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        //绑定日期控件
        var today = new Date();
        var year = today.getFullYear();
        var month = today.getMonth();
        var day = today.getDate();
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $("[id$='txtDate']").prop("readonly", true).datepicker({
            numberOfMonths: 1, //显示月份数量
            onClose: function () {
                $(this).css("color", "#000");
            }
        }).focus(function () { $(this).val(''); });
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        var SelectedCss = "active";
        var NotSelectedCss = "normal";
        var status = $.getUrlParam("status");
        if (status != null) {
            $("a:[href='MyToDoInfo.aspx?status=" + status + "&type=1']").parents("li").removeClass(NotSelectedCss);
            $("a:[href='MyToDoInfo.aspx?status=" + status + "&type=1']").parents("li").addClass(SelectedCss);
        } else {
            $("a:[href='MyToDoInfo.aspx?status=0&type=1']").parents("li").removeClass(NotSelectedCss);
            $("a:[href='MyToDoInfo.aspx?status=0&type=1']").parents("li").addClass(SelectedCss);
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="待办管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以添加，修改，删除待办" />
                    </td>
                </tr>
            </table>
    </div>     
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Literal ID="Literal7" runat="server" Text="创建人" />：
                    <asp:TextBox ID="txtCreatedUserName" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Literal ID="Literal9" runat="server" Text="创建日期" />：
                    <asp:TextBox ID="txtDate" runat="server" class="admininput_1" ></asp:TextBox>
                    <asp:Label ID="Label1" runat="server"> 
                    <asp:Literal ID="Literal3" runat="server" Text="待办标题" />:</asp:Label><asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>&nbsp <asp:Button ID="btnSearch" runat="server" 
                                Text="<%$ Resources:Site, btnSearchText %>" 
                                OnClick="btnSearch_Click"
                                class="adminsubmit_short">
                    </asp:Button>
                </td>

            </tr>
    </table>
    <br />
    <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="MyToDoInfo.aspx?status=0&type=1" style="padding-top: 5px;">
                        <asp:Literal ID="Literal5" runat="server" Text="未办"></asp:Literal></a></li>
                        <li class="normal"><a href="MyToDoInfo.aspx?status=1&type=1">
                        <asp:Literal ID="Literal11" runat="server" Text="已办"></asp:Literal></a></li>
                        <li class="normal"><a href="MyToDoInfo.aspx?status=2&type=1">
                        <asp:Literal ID="Literal12" runat="server" Text="未通过"></asp:Literal></a></li>
                        <li class="normal"><a href="MyToDoInfo.aspx?status=3&type=1">
                        <asp:Literal ID="Literal13" runat="server" Text="已通过"></asp:Literal></a></li></ul>
            </div>
    </div>
      <div class="newslist">
        <div class="newsicon">
            <ul>
                    <li style="background: url(/admin/images/list.gif) no-repeat"><a href="MyToDoInfo.aspx?type=1&status=0">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a><b>|</b></li> <%--<li style="background: url(/admin/images/reload.png) no-repeat"><a href="#">
                    <asp:Literal ID="Literal7" runat="server" Text="返回" /></a><b>|</b></li>--%></ul></div></div><cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" 
        OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" 
        ShowCheckAll="true">
            <Columns>
                <asp:TemplateField HeaderText="待办标题" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%# Eval("Title")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="发送类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetInfoToType( Eval("ToType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetStatus( Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建人" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%#GetUserName( Eval("CreatedUserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批复人" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%#GetUserName(Eval("ReviewedUserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建日期" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <a href="Show.aspx?id=<%#Eval("ID") %>&type=1&status=<%#Eval("Status") %>">详细</a></ItemTemplate></asp:TemplateField><asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>">
                        </asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr></cc1:gridviewex></div>
            <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr runat="server" id="appTr">
                <td style="width: 1px;">
                </td>
                <td>
                    &nbsp;&nbsp; <asp:Literal ID="ltlStatus" runat="server" Text="状态："></asp:Literal><asp:DropDownList ID="drop_Status" runat="server">
                        <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem><asp:ListItem Value="0" Text="未办"></asp:ListItem><asp:ListItem Value="1" Text="已办"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp; <asp:Literal ID="Literal8" runat="server" Text="内容" />： <asp:TextBox ID="txtReviewedContent" runat="server" class="admininput_1"></asp:TextBox>&nbsp;&nbsp; <asp:Button ID="btnBatch" runat="server" Text="批复处理" class="adminsubmit"
                        OnClick="btnBatch_Click" />
                 </td>
            </tr>
        </table>
   </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
