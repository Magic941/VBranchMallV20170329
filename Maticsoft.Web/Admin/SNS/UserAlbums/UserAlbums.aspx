<%@ Page Title="用户专辑管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="UserAlbums.aspx.cs" Inherits="Maticsoft.Web.SNS.UserAlbums.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("span:contains('首页推荐')").css("color", "black");
            $("span:contains('频道推荐')").css("color", "black");
            $("span:contains('取消首页推荐')").css("color", "#006400");
            $("span:contains('取消频道推荐')").css("color", "#006400");
        })
   
    </script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });

        })
    
  
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
        .adminsubmit_short {
            background-image: url(/Admin/Images/adminsubmit7.gif)
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
                        <asp:Literal ID="Literal1" runat="server" Text="用户专辑管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以删除推荐用户专辑" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <!--Search end-->
        <!--Add  -->
        <table style="width: 100%;" class="borderkuang" bgcolor="#FFFFFF">
            <tr>
                <td width="10px" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td style="width: 60px">
                    <asp:Literal ID="Literal5" runat="server" Text="专辑名称" />：
                </td>
                <td style="width: 300px">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="100px"></asp:TextBox>
                    <asp:Literal ID="Literal2" runat="server" Text="创建人：" />
                    <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 70px">
                    状态：
                </td>
                <td style="width: 200px">
                    <asp:DropDownList ID="rdoisable" runat="server" RepeatDirection="Horizontal" Width="100px">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">可用</asp:ListItem>
                        <asp:ListItem Value="0">不可用</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 200px">
                    <asp:DropDownList ID="ddlTypeList" runat="server" RepeatDirection="Horizontal" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 60px">
                    <asp:Literal ID="Literal4" runat="server" Text="时间："></asp:Literal>
                </td>
                <td style="width: 300px">
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate"></asp:TextBox>
                    --------
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate"></asp:TextBox>
                </td>
                <td>
                    是否推荐：
                </td>
                <td>
                    <asp:DropDownList ID="rdorecommand" runat="server" RepeatDirection="Horizontal" Width="100px">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">推荐到首页</asp:ListItem>
                        <asp:ListItem Value="2">推荐到频道页</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 100px">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short" Width="100px"></asp:Button>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist" style="display: none;">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px; display: none;">
                        <asp:LinkButton ID="ltbnDelete" runat="server" OnClick="ltbnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">批量删除</asp:LinkButton>
                        <b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="false" DataKeyNames="AlbumID">
            <Columns>
                <asp:BoundField DataField="AlbumID" HeaderText="编号" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="封面" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <img src="<%# Eval("CoverPhotoUrl")%>" width="60px" height="60px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专辑名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href='<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS)%>Album/Details?AlbumID=<%#Eval("AlbumID")%>'
                            target="_blank">
                            <%# Eval("AlbumName") %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建人" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href='<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS)%>User/Posts/<%#Eval("CreatedUserID")%>'
                            target="_blank">
                            <%#Eval("CreatedNickName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="专辑分类" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# GetCategoryName(Eval("AlbumID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PhotoCount" HeaderText="宝贝数" SortExpression="PhotoCount"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" />
                <asp:BoundField DataField="PVCount" HeaderText="访问量" SortExpression="PVCount" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="60" />
                <asp:BoundField DataField="FavouriteCount" HeaderText="收藏数" SortExpression="FavouriteCount"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" />
                <asp:TemplateField ItemStyle-Width="60" HeaderText="评论数" SortExpression="CommentsCount"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="/Admin/SNS/Comments/List.aspx?targetid=<%#Eval("AlbumID")%>&type=3">评论(<%#Eval("CommentsCount")%>)</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ChannelSequence" HeaderText="首页顺序" SortExpression="ChannelSequence"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" />
                <asp:BoundField DataField="Sequence" HeaderText="顺序" SortExpression="Sequence" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="是否被管理员推荐" SortExpression="IsRecommend" ItemStyle-HorizontalAlign="Center"
                    Visible="false">
                    <ItemTemplate>
                        <%# GetboolText(Eval("IsRecommend"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="隐私" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetPrivacy(Eval("Privacy"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LastUpdatedDate" HeaderText="最后的更新时间" SortExpression="LastUpdatedDate"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:TemplateField ControlStyle-Width="80" HeaderText="专辑推荐" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRecommandindex" runat="server" CausesValidation="False" CommandName="RecommandIndex"
                            CommandArgument='<%#Eval("AlbumID")+","+Eval("IsRecommend")%>' Style="color: #0063dc;"><span ><%#Eval("IsRecommend").ToString() == "1" ? "取消首页推荐" : "首页推荐"%></span></asp:LinkButton>
                        <asp:LinkButton ID="lbtnRecommandAblum" runat="server" CausesValidation="False" CommandName="RecommandAblum"
                            CommandArgument='<%#Eval("AlbumID")+","+Eval("IsRecommend")%>' Style="color: #0063dc;"><span ><%#Eval("IsRecommend").ToString() == "2" ? "取消频道推荐" : "频道推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="创建日期" ItemStyle-HorizontalAlign="Center"
                    SortExpression="CreatedDate" ItemStyle-Width="130" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;
            display: none;">
            <tr>
                <td style="width: 100%">
                    是否推荐：
                    <asp:DropDownList ID="dropIsRecommand" runat="server">
                        <asp:ListItem Value="-1" Selected="True" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="推荐"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不推荐"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnRecommand" runat="server" Text="<%$ Resources:Site, btnBatchText %>"
                        class="adminsubmit" OnClick="btnRecommand_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
