<%@ Page Title="用户文章管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="UserBlog.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Posts.UserBlog" %>
<%@ Import Namespace="Maticsoft.Web" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>     
      <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet"type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
            <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
<script type="text/javascript" >
    $(function() {
        $("td:contains('已审核')").css("color", "#006400");
        $("td:contains('未通过')").css("color", "red");
        $("span:contains('推荐')").css("color", "black");
        $("span:contains('取消推荐')").css("color", "#006400");
    });
    $(function () {
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
        $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
    });

    function GetDeleteM() {
        $("[id$='btnDelete']").click();
    }
    </script>
<STYLE type="text/css">
.PostPerson{ width:100px} 
.PostDate{ width:100px}
</STYLE>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="用户文章管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 您可以对用户文章进行删除，审核和推荐处理" />
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
                <td width="1%" height="30" >
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                    <td  style=" width:200px">
                 <asp:Literal ID="Literal4" runat="server" Text="发布者："></asp:Literal>
                 <asp:TextBox ID="txtPoster" runat="server" CssClass="PostPerson"></asp:TextBox>

                  </td>
                  <td  style=" width:300px">
                 <asp:Literal ID="Literal5" runat="server" Text="时间："></asp:Literal>
                 <asp:TextBox ID="txtBeginTime" runat="server"  CssClass="PostDate" ></asp:TextBox>

                   <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                 <asp:TextBox ID="txtEndTime" runat="server"  CssClass="PostDate" ></asp:TextBox>
                 </td>
                 <td>
                  关键字：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
                <td></td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist" style="display: none">
            <div class="newsicon">
                <ul>
               
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
           
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" ExcelFileName="FileName1"  
            CellPadding="3" BorderWidth="1px" ShowCheckAll="True" DataKeyNames="BlogID"
            ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <Columns>
                                <asp:TemplateField HeaderText="标题" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <p style="white-space: normal; max-width:700px">                            
                           <a href='<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.SNS)%>Blog/BlogDetail?id=<%#Eval("BlogID")%>' target="_blank"> <%#Eval("Title")%></a>
                        </p>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="用户" 
                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80" />                          

                      <asp:BoundField DataField="PvCount" HeaderText="<%$Resources:CMS,ContentfieldPvCount%>"
                    SortExpression="PvCount" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="40" />             
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100" HeaderText="推荐到首页" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminRecommand" runat="server" CommandArgument='<%#Eval("BlogID")+","+Eval("Recomend")%>'
                            Style="color: #0063dc;" CommandName="RecommendIndex">
                         <span><%#(int)Eval("Recomend") == 1? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="发布时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150"/>             
                          
                <asp:TemplateField ItemStyle-Width="40px"  HeaderText="<%$ Resources:Site, btnDeleteText %>" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkbtnDel" runat="server" CausesValidation="False" CommandArgument="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" CommandName='<%# Eval("BlogID") %>'  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"> </asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td  width="80">
                    <asp:Button ID="btnIndexRec" runat="server" Text="批量推荐"
                        class="adminsubmit" OnClick="btnIndexRec_Click"  />
                </td>
                  <td>
              <asp:Button ID="btnChecked" runat="server" Text="批量审核"  class="adminsubmit" OnClick="btnCheck_Click" />&nbsp;&nbsp;
                
            </td>
            
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

