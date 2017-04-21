<%@ Page Title="动态和评论管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="PostsVideo.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.PostsVideo.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
     <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
   
        $(function () {
            $(".iframe").colorbox({ iframe: true, width: "800", height: "600", overlayClose: false }); 
            $("#ctl00_ContentPlaceHolder1_gridView td:contains('已审核')").css("color", "#006400");
            $("#ctl00_ContentPlaceHolder1_gridView td:contains('未通过')").css("color", "red");
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
    <br />
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="视频管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 您可以审核删除视频" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
          <table style="width:100%;" class="borderkuang">
            <tr>
                <td style=" width:170px">
                    <asp:Literal ID="Literal2" runat="server" Text="创建人：" />
                    <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td  style=" width:70px" >
                    <asp:Literal ID="Literal10" runat="server" Text=" 审核情况：" />
                </td>
                <td   style=" width:150px">
                    <asp:DropDownList ID="rdocheck" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="2">审核未通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td   style=" width:300px">
                    <asp:Literal ID="Literal4" runat="server" Text="时间："></asp:Literal>
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate" ></asp:TextBox>
                    <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate" ></asp:TextBox>
                </td>
                <td   style=" width:100px">
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
            CellPadding="3" BorderWidth="1px" ShowCheckAll="True" DataKeyNames="PostID,Type,TargetID"
            ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="视频(点击可观看)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <a class="iframe" href="VideoPreview.aspx?id=<%#Eval("PostID")%>">
                            <%-- <asp:Image ID="Image2" runat="server" Width="100px" Height="100px" ImageAlign="Middle"
                                ImageUrl='' />--%>
                            <img src="<%#Eval("PostExUrl")%>" width="100px" height="100px" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="视频信息" ItemStyle-HorizontalAlign="Center" SortExpression="CommentCount"
                    ItemStyle-Width="360">
                    <ItemTemplate>
                          <div style="text-align: left; margin-left: 10px;">
                            <asp:Literal ID="ltlTitle" runat="server" Text="标题"></asp:Literal>：<%#Maticsoft.Common.StringPlus.SubString(Eval("Description"),30,"...")%>
                            <br /><asp:Literal ID="ltlCreatedUser" runat="server" Text="创建者"></asp:Literal>：<%# Eval("CreatedNickName")%>
                            <br /><asp:Literal ID="ltlCreatedDate" runat="server" Text="分享时间"></asp:Literal>：<%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="统计信息" ItemStyle-HorizontalAlign="Center" SortExpression="CommentCount"
                    ItemStyle-Width="240">
                    <ItemTemplate>
                        <span><a href="/Admin/SNS/Comments/List.aspx?type=4&targetid=<%#Eval("PostID")%>">评论数：
                            <%#Eval("CommentCount")%></a></span> 
                                <br /><asp:Literal  runat="server" Text="转发数"></asp:Literal>：<%# Eval("ForwardCount")%>
                                   <br /><asp:Literal ID="Literal5"  runat="server" Text="喜欢数"></asp:Literal>：<%# Eval("FavCount")%>
                                           <br /><asp:Literal ID="Literal7"  runat="server" Text="是否推荐"></asp:Literal>：<%#GetboolText(Eval("IsRecommend"))%>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkbtnDel" runat="server" CausesValidation="False" CommandArgument="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" CommandName='<%# Eval("PostID") %>' OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                </td>
                <td>
                    <asp:Button ID="btnChecked" runat="server" Text="批量审核" class="adminsubmit" OnClick="btnCheck_Click" />&nbsp;&nbsp;<label
                        visible="false" id="lblTip" style="color: Blue" runat="server">保存成功</label>
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
