<%@ Page Title="<%$ Resources:CMSVideo, ptVideoAlbumList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.CMS.VideoAlbum.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Add
            $(".Add").colorbox({ iframe: true, width: "750", height: "690", overlayClose: false });
            //Modify
            $(".Modify").colorbox({ iframe: true, width: "750", height: "690", overlayClose: false });
            //Show
            $(".Show").colorbox({ iframe: true, width: "750", height: "690", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <!--Title -->
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlList" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumList %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoAlbumListTip %>"></asp:Literal>
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
                <td width="2%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/admin/Images/icon-1.gif" width="19" height="19" alt="" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlState" runat="server" Text="<%$ Resources:CMSVideo, State %>"></asp:Literal>
                        ：
                    <asp:DropDownList ID="dropState" runat="server" Width="100px">
                        <asp:ListItem Value="" Selected="True" Text="<%$ Resources:Site, PleaseSelect %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, Normal %>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$ Resources:CMSVideo, NotAudit %>"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px" runat="server"  id="liAdd"><a class="Add" href="add.aspx">
                        <asp:Literal ID="ltlAdd" runat="server" Text="<%$ Resources:Site, ltlAdd %>"></asp:Literal></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;"  id="liDel" runat="server">
                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteListText %>"></asp:LinkButton>
                        <b>|</b></li>
                 
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AlbumID"
            RowHeight="130">
            <Columns>
                <asp:BoundField DataField="AlbumID" HeaderText="<%$ Resources:CMSVideo, ID %>" SortExpression="AlbumID"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, Album %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="CoverVideo" ItemStyle-Width="120" ItemStyle-Height="120">
                    <ItemTemplate>
                        <a href="/Admin/CMS/Video/list.aspx?AlbumID=<%# Eval("AlbumID") %>">
                            <img src="/UploadFolder/<%# Eval("CoverVideo") %>" alt="" width="120" height="120"
                                style="border: none" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" SortExpression=""
                    ItemStyle-Width="200px">
                    <ItemTemplate>
                        <div style="text-align: left; margin-left: 10px;">
                           <asp:Literal ID="ltlName" runat="server" Text="<%$ Resources:CMSVideo, Name %>"></asp:Literal>：<%# SubString(Eval("AlbumName"), "...", 30)%><br /><asp:Literal ID="ltlDescription" runat="server" Text="<%$ Resources:CMSVideo, Description %>"></asp:Literal>
                            ：<%# SubString(Eval("Description"),"...",20)%><br /><asp:Literal ID="ltlCreatedDate" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedDate %>"></asp:Literal>
                            ：<%# Eval("CreatedDate") %><br /><asp:Literal ID="ltlCreatedUser" runat="server" Text="<%$ Resources:CMSVideo, ltlCreatedUser %>"></asp:Literal>
                            ：<%# Eval("CreatedUserName")%><br /><asp:Literal ID="ltlPvCount" runat="server" Text="<%$ Resources:CMSVideo, ltlPvCount %>"></asp:Literal>：<%# Eval("PvCount")%></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, State %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="State">
                    <ItemTemplate>
                        <%# GetVideoAlbumState(Eval("State"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, ltlPrivacy %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="Privacy">
                    <ItemTemplate>
                        <%# GetVideoAlbumPrivacy(Eval("Privacy"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="<%$ Resources:Site,btnDetailText %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="">
                    <ItemTemplate>
                        <a class="Show" href="Show.aspx?id=<%# Eval("AlbumID")%>">
                            <asp:Literal ID="ltlDetail" runat="server" Text="<%$ Resources:Site, btnDetailText %>"></asp:Literal></a>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Site,btnEditText %>" ItemStyle-HorizontalAlign="Center"
                    SortExpression="">
                    <ItemTemplate>
                    <a class="Modify" href="Modify.aspx?id=<%# Eval("AlbumID")%>">
                            <asp:Literal ID="ltlEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"></asp:Literal></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center"  >
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
                        <asp:ListItem Value="2" Text="<%$ Resources:CMSVideo, Normal %>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$ Resources:CMSVideo, PendingReview %>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$ Resources:CMSVideo, NotAudit %>"></asp:ListItem>
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
