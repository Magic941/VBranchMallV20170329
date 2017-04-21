<%@ Page Title="小组表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Group.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Groups.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var status = $.getUrlParam("status");
            if (status == null) {
                status = -1;
            }
            $("a:[href='Group.aspx?status=" + status + "']").parents("li").removeClass(NotSelectedCss);
            $("a:[href='Group.aspx?status=" + status + "']").parents("li").addClass(SelectedCss);
            $("td:contains('已审核')").css("color", "#006400");
            $("td:contains('未通过')").css("color", "red");
            $("span:contains('推荐')").css("color", "black");
            $("span:contains('取消推荐')").css("color", "#006400");
        })
    </script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });

        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
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
        <div class="newslist_title" >
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="小组管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以删除、审核小组" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <!--Add  -->
        <table style="width: 100%;" class="borderkuang" bgcolor="#FFFFFF">
            <tr>
                <td style="text-align: right; width:100px;">
                    <asp:Literal ID="Literal5" runat="server" Text="专辑名称：" />
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
                                 
                    <asp:DropDownList ID="rdorecommand" runat="server" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">推荐首页</asp:ListItem>
                        <asp:ListItem Value="2">推荐精选</asp:ListItem>
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
                    <li class="normal"><a href="Group.aspx?status=-1" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="全部"></asp:Literal></a></li>
                    <li class="normal"><a href="Group.aspx?status=0">
                        <asp:Literal ID="Literal8" runat="server" Text="未审核"></asp:Literal></a></li>
                    <li class="normal"><a href="Group.aspx?status=1">
                        <asp:Literal ID="Literal9" runat="server" Text="已审核"></asp:Literal></a></li>
                    <li class="normal"><a href="Group.aspx?status=2">
                        <asp:Literal ID="Literal11" runat="server" Text="未通过"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="GroupID">
            <Columns>
                 <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="64px">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Groups/GroupTopics.aspx?GroupID=<%#Eval("GroupId")%>">
                        <img src="<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("GroupLogoThumb").ToString(), "T80x80_") %>" style="height:64px;width:64px;border-width:0px;">
                            </a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="小组名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Groups/GroupTopics.aspx?GroupID=<%#Eval("GroupId")%>">
                            <%#Eval("GroupName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GroupDescription" HeaderText="小组的简介" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField ItemStyle-Width="100" HeaderText="创始人" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <span>
                            <%#Eval("CreatedNickName")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100" HeaderText="人数" SortExpression="GroupUserCount"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <span item="/Admin/SNS/Groups/GroupUsers.aspx?GroupID=<%#Eval("GroupID")%>">成员(<%#Eval("GroupUserCount")%>)</span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100" HeaderText="话题" SortExpression="TopicCount"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Groups/GroupTopics.aspx?GroupID=<%#Eval("GroupID")%>">话题(<%#Eval("TopicCount")%>)</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="80" HeaderText="回复" SortExpression="TopicReplyCount" Visible="false"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Groups/GroupTopicReply.aspx?GroupID=<%#Eval("GroupID")%>">回复(<%#Eval("TopicReplyCount")%>)</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="56" HeaderText="推荐首页" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRecommandHome" runat="server" CommandArgument='<%#Eval("GroupID")+","+Eval("IsRecommand")%>'
                            Style="color: #0063dc;" CommandName="RecommendHome">
                                                    <span><%#Eval("IsRecommand").ToString() == "1" ? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="56" HeaderText="推荐精选" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRecommandPro" runat="server" CommandArgument='<%#Eval("GroupID")+","+Eval("IsRecommand")%>'
                            Style="color: #0063dc;" CommandName="RecommendPro">
                                                    <span><%#Eval("IsRecommand").ToString() == "2" ? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="创建日期" SortExpression="CreatedDate"
                    ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%;">
            <tr>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" />&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnChecked" runat="server" Text="批量审核" class="adminsubmit" OnClick="btnCheck_Click" />&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnCheckedUnpass" runat="server" Text="批量拒绝" class="adminsubmit"
                        OnClick="btnCheckedUnPass_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
