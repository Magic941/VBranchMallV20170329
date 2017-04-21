<%@ Page Title="<%$ Resources:CMSVideo, ptVideoList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.CMS.Video.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="Maticsoft.Controls" Namespace="Maticsoft.Controls" TagPrefix="cc2" %>
<%@ Register Src="/Admin/../Controls/VideoClassDropList.ascx" TagName="VideoClassDropList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".localvideopreview").colorbox();
            $(".onlinevideopreview").colorbox({ iframe: true, innerWidth: 550, innerHeight: 380 });
        });
    </script>
    <style type="text/css">
        .bgcms div{
            display: initial;
            margin-left: -8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <!--Title -->
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlList" runat="server" Text="<%$ Resources:CMSVideo, ptVideoList %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoListTip %>"></asp:Literal>
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
                <td width="1%"  height="30" bgcolor="#FFFFFF" class="newstitlebody"  >
                    <img src="/admin/Images/icon-1.gif" width="19" height="19" alt="" />
                </td>
                   <td colspan="2" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="ltlAlbum" runat="server" Text="<%$ Resources:CMSVideo, ltlAlbum %>" />：
                          <asp:DropDownList ID="dropAlbum" runat="server">
                    </asp:DropDownList>
                     <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>" />：
                            <asp:DropDownList ID="dropState" runat="server">
                        <asp:ListItem Value="" Selected="True" Text="<%$ Resources:Site, PleaseSelect %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                        <asp:ListItem Value="3" Text="<%$ Resources:CMSVideo, NotYetReleased %>"></asp:ListItem>
                        <asp:ListItem Value="4" Text="<%$ Resources:CMSVideo, Screen %>"></asp:ListItem>
                        <asp:ListItem Value="5" Text="<%$ Resources:CMSVideo, Publish %>"></asp:ListItem>
                    </asp:DropDownList>
                   </td>
            </tr>
            <tr>
                <td width="1%"  height="30"></td>
                <td height="35" style="width:40px;" bgcolor="#FFFFFF" ><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CMSVideo, ltlCategory %>" />：
                </td><td class="bgcms"><uc1:VideoClassDropList ID="VideoClassDropList1" runat="server" />
                       <asp:Literal ID="ltlSearch" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                           <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                             <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short" /></td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px" runat="server" id="liAdd"><a href="add.aspx">
                        <asp:Literal ID="ltlAdd" runat="server" Text="<%$ Resources:Site, ltlAdd %>"></asp:Literal></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" OnClick="lbtnDelete_Click"></asp:LinkButton>
                        <b>|</b></li>
               
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="VideoID"
            RowHeight="130">
            <Columns>
                <asp:BoundField DataField="VideoID" HeaderText="<%$ Resources:CMSVideo, ID %>" SortExpression="VideoID"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, Video %>" ItemStyle-HorizontalAlign="Left"
                   ItemStyle-Width="120px"  ItemStyle-Height="120px">
                    <ItemTemplate>
                        <%# OutHtmlCodeByVideoID("localvideopreview", "onlinevideopreview", Eval("VideoID"),"Upload"+ Eval("ImageUrl").ToString(), 120, 120)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <div style="text-align: left; margin-left: 10px;">
                            <asp:Literal ID="ltlTitle" runat="server" Text="<%$ Resources:CMSVideo, ltlTitle %>"></asp:Literal>：<%# SubString(Eval("Title"), "...", 20)%><br /><asp:Literal ID="ltlCreatedUser" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedUser %>"></asp:Literal>：<%# Eval("CreatedUserName")%><br /><asp:Literal ID="ltlCreatedDate" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedDate %>"></asp:Literal>：<%# Eval("CreatedDate") %><br /><asp:Literal ID="ltlType" runat="server" Text="<%$ Resources:CMSVideo, ltlType %>"></asp:Literal>：<%# GetUrlType(Eval("UrlType"))%><br />
                           <%-- <asp:Literal ID="ltlDuration" runat="server" Text="<%$ Resources:CMSVideo, ltlDuration %>"></asp:Literal>：<%# SecondToDateTime(Eval("TotalTime"))%>--%>
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, StatisticalInfo %>" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="125px">
                    <ItemTemplate>
                        <div style="text-align: left; margin-left: 10px;">
                            <asp:Literal ID="ltlTotalComment" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalComment %>"></asp:Literal>：<%# Eval("TotalComment")%><br /><asp:Literal ID="ltlTotalFav" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalFav %>"></asp:Literal>：<%# Eval("TotalFav")%><br /><asp:Literal ID="ltlReference" runat="server" Text="<%$ Resources:CMSVideo, ltlReference %>"></asp:Literal>：<%# Eval("Reference")%><br /><asp:Literal ID="ltlPvCount" runat="server" Text="<%$ Resources:CMSVideo, ltlPvCount %>"></asp:Literal>：<%# Eval("PvCount")%><br /><asp:Literal ID="ltlTotalUp" runat="server" Text="<%$ Resources:CMSVideo, ltlTotalUp %>"></asp:Literal>：<%# Eval("TotalUp")%><br /><asp:Literal ID="ltlIsRecommend" runat="server" Text="<%$ Resources:CMSVideo, IsRecommend %>"></asp:Literal>：
                            <%#GetboolText(Eval("IsRecomend")) %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:CMSVideo, Description %>"
                    SortExpression="Description" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, AlbumAndCategory %>" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="125px">
                    <ItemTemplate>
                        <div style="text-align: left; margin-left: 10px;">
                            <asp:Literal ID="ltlAlbum" runat="server" Text="<%$ Resources:CMSVideo, ltlAlbum %>"></asp:Literal>：<%# Eval("AlbumName")%><br /><asp:Literal ID="ltlCategory" runat="server" Text="<%$ Resources:CMSVideo, ltlCategory %>"></asp:Literal>：
                            <%# Eval("VideoClassName")%>
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="<%$ Resources:CMSVideo, State %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="State">
                    <ItemTemplate>
                        <%# GetVideoState(Eval("State"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="80px" HeaderText="<%$ Resources:CMSVideo, Operation %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="" ControlStyle-Width="50">
                    <ItemTemplate>
                        <a href="Show.aspx?id=<%# Eval("VideoID")%>">
                            <asp:Literal ID="ltlDetail" runat="server" Text="<%$ Resources:Site, btnDetailText %>"></asp:Literal></a>
                        &nbsp; <a href="Modify.aspx?id=<%# Eval("VideoID")%>">
                            <asp:Literal ID="ltlEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"></asp:Literal></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="80px" ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
        <br />
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                    <asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem Value="-1" Selected="True" Text="<%$ Resources:Site, PleaseSelect %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                        <asp:ListItem Value="3" Text="<%$ Resources:CMSVideo, NotYetReleased %>"></asp:ListItem>
                        <asp:ListItem Value="4" Text="<%$ Resources:CMSVideo, Screen %>"></asp:ListItem>
                        <asp:ListItem Value="5" Text="<%$ Resources:CMSVideo, Publish %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBatch" runat="server" Text="<%$ Resources:Site, btnBatchText %>"
                        class="adminsubmit" OnClick="btnBatch_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>