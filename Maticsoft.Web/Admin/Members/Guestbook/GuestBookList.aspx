<%@ Page Title="留言表" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="GuestBookList.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Guestbook.GuestBookList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
        <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
        <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }

        $(function () {
            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var type = $.getUrlParam("type");
            if (!type) {
                type = 0;
            }
            if (status == null) {
                status = 0;
            }
            $("a:[href='GuestBookList.aspx?type=0']").parents("li").addClass(SelectedCss);
            $("a:[href='GuestBookList.aspx?type=" + type + "']").parents("li").removeClass(NotSelectedCss);
            $("a:[href='GuestBookList.aspx?type=" + type + "']").parents("li").addClass(SelectedCss);
            $("td:contains('未解决')").css("color", "red");
            $("td:contains('已解决')").css("color", "#006400");
            $(".iframe").colorbox({ iframe: true, width: "600px", height: "500px", overlayClose: false });
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="留言管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对留言进行管理" />
                    </td>
                </tr>
            </table>
        </div>  
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                   <td height="35"  style=" ">
                   &nbsp;&nbsp; <asp:Literal ID="Literal5" runat="server" Text="内容关键词：" />
                    <asp:TextBox ID="txtKeyword" runat="server"  Width="100px"></asp:TextBox>
                     &nbsp;&nbsp;<asp:Literal ID="Literal6" runat="server" Text="地区："></asp:Literal>
                    <asp:TextBox ID="txtRegion" runat="server" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Literal ID="Literal11" runat="server" Text="邮箱：" />
                    <asp:TextBox ID="txtEmail" runat="server"  Width="100px"></asp:TextBox>
                       &nbsp;&nbsp;<asp:Literal ID="Literal9" runat="server" Text="时间："></asp:Literal>
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate"  Width="100px" ></asp:TextBox>
                    <asp:Literal ID="Literal10" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate"  Width="100px" ></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                    </td>
                    
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                   
                     <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" OnClick="btnDelete_Click"
                             Text="批量删除" class="adminsubmit"  />
                    </li>
              
                </ul>
            </div>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                  
                      <li class="normal"><a href="GuestBookList.aspx?type=0">
                        <asp:Literal ID="Literal8" runat="server" Text="未解决"></asp:Literal></a></li>
                       <li class="normal"><a href="GuestBookList.aspx?type=1" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="已解决"></asp:Literal></a></li>
                      
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="False" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="CreateNickName" HeaderText="留言者" SortExpression="CreateNickName"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80"/>
                    <asp:BoundField DataField="CreatorEmail" HeaderText="邮箱" SortExpression="CreatorEmail"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80"/>
                    
                <asp:BoundField DataField="CreatorCompany" HeaderText="所在公司" SortExpression="CreatorCompany"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"/>
                     <asp:BoundField DataField="CreatorRegion" HeaderText="所在地区" SortExpression="CreatorRegion"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80"/>
            
                 <asp:BoundField DataField="Title" HeaderText="留言标题" SortExpression="Title"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120" />

                <asp:BoundField DataField="Description" HeaderText="留言内容" SortExpression="Description"
                    ItemStyle-HorizontalAlign="Left" />
                    
                <asp:BoundField DataField="CreatedDate" HeaderText="时间" SortExpression="CreatedDate"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" />
                <asp:BoundField DataField="CreatorUserIP" HeaderText="IP"
                 SortExpression="CreatorUserIP" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" Visible="False"/>
                 
                 
                   <asp:TemplateField HeaderText="回复" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <a class="iframe" href="ReplyGuestBook.aspx?id=<%#Eval("ID")%>">
                           回复
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status").ToString())%></ItemTemplate>
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
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

