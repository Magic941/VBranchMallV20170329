<%@ Page Title="小组话题管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="GroupTopics.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.GroupTopics.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
            <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
         
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });

        });

        $(function () {
            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var status = $.getUrlParam("status");
            if (status == null) {
                status = -1;
            }
            $("a:[href^='GroupTopics.aspx?status=" + status + "']").parents("li").removeClass(NotSelectedCss);
            $("a:[href^='GroupTopics.aspx?status=" + status + "']").parents("li").addClass(SelectedCss);
            $("td:contains('已审核')").css("color", "#006400");
            $("td:contains('未通过')").css("color", "red");
            $("span:text").filter(function () {
                return this.value.match(/[^推荐]/);
            }).css("color", "#006400");

            ;
            $("span:contains('推荐')").css("color", "black");
            $("span:contains('取消推荐')").css("color", "#006400");
        });
        function GetDeleteM() {
            $("#ctl00_ContentPlaceHolder1_btnDelete").click();
        }
    </script>
    <style type="text/css">
        .PostPerson
        {
            width: 100px;
        }
        .PostDate
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlTitle" runat="server" Text="小组话题管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以推荐和删除话题" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
               <table style="width: 100%;" class="borderkuang" bgcolor="#FFFFFF">
            <tr>
                <td style="text-align: right; width:100px;">
                <asp:Literal ID="Literal5" runat="server" Text="关键字" />：
                </td>
                <td style="text-align: left; width:300px;">
                   <asp:TextBox ID="txtKeyword" runat="server" Width="100px"></asp:TextBox>
                
                   <asp:Literal ID="Literal2" runat="server" Text="创建人：" />
                
                <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
               </td>
                <td style="text-align: right; width:100px;">
                是否推荐： 
               </td>
               <td>
                                 
                 <asp:DropDownList ID="ddtRecommand" runat="server">
                    <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="0">不推荐</asp:ListItem>
                        <asp:ListItem Value="1">推荐到首页</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Literal ID="Literal10" runat="server" Text="时间：" />
                </td>
                <td  >
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate" ></asp:TextBox>
                    <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate" ></asp:TextBox>                
                   </td>
               <td style="text-align: right; width:100px;">
               
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
               <td>
                
               </td>
            </tr>
        </table>

        <br />
        <%--      <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>--%>
        <!--Search end-->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                  
                </ul>
            </div>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="GroupTopics.aspx?status=-1&groupid=<%=GroupID %>" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="全部"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopics.aspx?status=0&groupid=<%=GroupID %>">
                        <asp:Literal ID="Literal8" runat="server" Text="未审核"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopics.aspx?status=1&groupid=<%=GroupID %>">
                        <asp:Literal ID="Literal9" runat="server" Text="已审核"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopics.aspx?status=2&groupid=<%=GroupID %>">
                        <asp:Literal ID="Literal11" runat="server" Text="未通过"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="TopicID">
            <Columns>
                <asp:TemplateField HeaderText="标题" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a target="_blank" href="<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS)%>Group/TopicReply?id=<%#Eval("TopicID")%>">
                            <%#Eval("Title")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="80px" HeaderText="发表者" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <span>
                            <%#Eval("CreatedNickName")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GroupName" HeaderText="小组" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100" />
                <asp:TemplateField ItemStyle-Width="100" HeaderText="推荐到频道" ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRecommand" runat="server" CommandArgument='<%#Eval("TopicID")+","+Eval("IsRecomend")%>'
                            Style="color: #0063dc;" CommandName="RecommendChannal">
                     <span><%#Convert.ToInt32(Eval("IsRecomend")) == 1 ? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100" HeaderText="推荐到首页" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminRecommand" runat="server" CommandArgument='<%#Eval("TopicID")+","+Eval("IsAdminRecommend")%>'
                            Style="color: #0063dc;" CommandName="RecommendIndex">
                         <span><%#(bool)Eval("IsAdminRecommend") == true ? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="60px" HeaderText="回复" SortExpression="ReplyCount"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Groups/GroupTopicReply.aspx?TopicID=<%#Eval("TopicID")%>">回帖（<%#Eval("ReplyCount")%>）</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsAdminRecommend" HeaderText="推荐到频道首页" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" />
                <asp:TemplateField ItemStyle-Width="100" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%;">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_Changed">
                        <asp:ListItem Value="" Text="设置为..."></asp:ListItem>
                          <asp:ListItem Value="1" Text="批量审核"></asp:ListItem>
                            <asp:ListItem Value="2" Text="批量拒绝"></asp:ListItem>
                              <asp:ListItem Value="3" Text="批量禁言"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
                <td>
                    <asp:Button ID="BtnBack" runat="server" Text="返回小组列表" class="adminsubmit" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
