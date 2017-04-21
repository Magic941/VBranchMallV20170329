<%@ Page Title="搜索日志管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SearchLog.aspx.cs" Inherits="Maticsoft.Web.SNS.SearchWordLog.List" %>

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
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="搜索日志管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以删除搜索日志" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" >
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" style="  width:150px">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" Width=80px runat="server"></asp:TextBox>
                   </td> 
                    <td style="  width:150px">
                 <asp:Literal ID="Literal4" runat="server" Text="发布者："></asp:Literal>
                 <asp:TextBox ID="txtPoster" runat="server" Width=80px CssClass="PostPerson"></asp:TextBox>
                 </td>
                 <td style=" width:350px">
                    <asp:Literal ID="Literal5" runat="server" Text="时间："></asp:Literal>
                 <asp:TextBox ID="txtBeginTime" Width=80px runat="server"  CssClass="PostDate" ></asp:TextBox>

                   <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                 <asp:TextBox ID="txtEndTime" Width=80px runat="server"  CssClass="PostDate" ></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
                <td></td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">批量删除</asp:LinkButton>
                        <b>|</b></li>
                  
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
              
                <asp:BoundField DataField="SearchWord" HeaderText="搜索词"
                    ItemStyle-HorizontalAlign="Left" />  
                    <asp:BoundField DataField="CreatedDate" HeaderText="日期" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreatedUserId" HeaderText="创建者id"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="CreatedNickName" HeaderText="用户"
                    ItemStyle-HorizontalAlign="Left" />
           <%--     <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
        <!--Recommend start -->
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="lbtnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                </td>
            </tr>
        </table>        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <b >统计热门搜索：</b> 
                    <asp:DropDownList ID="dropTop" runat="server">
                        <asp:ListItem Text="自定义" Value="0"></asp:ListItem>
                        <asp:ListItem Text="前10条" Value="10"></asp:ListItem>
                        <asp:ListItem Text="前25条" Value="25"></asp:ListItem>
                        <asp:ListItem Text="前50条" Value="50"></asp:ListItem>
                        <asp:ListItem Text="前100条" Value="100"></asp:ListItem>
                        <asp:ListItem Text="前200条" Value="200"></asp:ListItem>
                        <asp:ListItem Text="前500条" Value="500"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtTop" runat="server" Width="50" MaxLength="6" onkeyup="value=value.replace(/[^\d]/g,'') "
                        onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                        Style="text-align: right"></asp:TextBox>
                    <asp:CheckBox ID="chkDelete" runat="server" Text="覆盖原热门搜索排行" Checked="true"  />
                    <asp:Button ID="btnPush" runat="server" Text="统计更新" class="adminsubmit" OnClientClick="return confirm('确定要统计吗？');"
                        OnClick="btnPush_Click" />
                        
                    
                </td>
            </tr>
        </table>
        <!--Recommend end -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
