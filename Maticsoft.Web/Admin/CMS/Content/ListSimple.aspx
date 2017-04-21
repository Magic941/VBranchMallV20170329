
<%@ Page Title="<%$Resources:CMS,ContentptList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ListSimple.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Content.ListSimple" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var type = $.getUrlParam("type");
            if (type != null) {
                $("a:[href='ListSimple.aspx?type=" + type + "']").parents("li").removeClass(NotSelectedCss);
                $("a:[href='ListSimple.aspx?type=" + type + "']").parents("li").addClass(SelectedCss);
            } else {
                $("a:[href='ListSimple.aspx']").parents("li").removeClass(NotSelectedCss);
                $("a:[href='ListSimple.aspx']").parents("li").addClass(SelectedCss);
            }
            $(".iframe").colorbox({ iframe: true, width: "800", height: "600", overlayClose: false });
        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='from']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
            $("[id$='to']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ContentptList %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,ContentlblList %>" />
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
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody" >
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:DropDownList ID="dropParentID" runat="server" Width="200px"  >
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a href="addSimple.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd%>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="ListSimple.aspx?type=0" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,HaveAudited %>"></asp:Literal></a></li>
                    <li class="normal"><a href="ListSimple.aspx?type=1">
                        <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,Unaudited %>"></asp:Literal></a></li>
                    <li class="normal"><a href="ListSimple.aspx?type=2">
                        <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:CMS,ContentdrpDraft %>"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ContentID"
            Style="float: left;" OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,lblTitle%>"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("Title") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:CMS,ContentlblClassName%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  Eval("ClassName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:CMS,ContentlblFileUrl%>"
                    ItemStyle-HorizontalAlign="Left" Visible="false">
                    <ItemTemplate>
                        <a href="<%# Eval("ImageUrl") %>" target="_blank">
                            <img src="<%# Eval("ImageUrl") %>" style="border: none" width="40px" height="40px"
                                alt="" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,State%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetContentState(Eval("State")) %>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="50" HeaderText="设置推荐" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminRecommand" runat="server" CommandArgument='<%#Eval("ContentID")+","+Eval("IsRecomend")%>'
                            Style="color: #0063dc;" CommandName="SetRec">
                         <span><%#(bool)Eval("IsRecomend") ? "取消推荐" : "推荐"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="50" HeaderText="设置热门" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminSetHot" runat="server" CommandArgument='<%#Eval("ContentID")+","+Eval("IsHot")%>'
                            Style="color: #0063dc;" CommandName="SetHot">
                         <span><%#(bool)Eval("IsHot")? "取消热门" : "热门"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="50" HeaderText="设置醒目" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminColor" runat="server" CommandArgument='<%#Eval("ContentID")+","+Eval("IsColor")%>'
                            Style="color: #0063dc;" CommandName="SetColor">
                         <span><%#(bool)Eval("IsColor") ? "取消醒目" : "醒目"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="50" HeaderText="设置置顶" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdminTop" runat="server" CommandArgument='<%#Eval("ContentID")+","+Eval("IsTop")%>'
                            Style="color: #0063dc;" CommandName="SetTop">
                         <span><%#(bool)Eval("IsTop")? "取消置顶" : "置顶"%></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PvCount" HeaderText="<%$Resources:CMS,ContentfieldPvCount%>"
                    SortExpression="PvCount" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="40" />
                <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:CMS,ContentfieldCreatedDate%>"
                    SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="100" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:CMS,ContentfieldContentID%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class="iframe" href="Show.aspx?id=<%#Eval("ContentID") %>&classid=<%#Eval("ClassID") %>">
                            <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMS,ContentfieldContentID%>" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="ContentID,ClassID" DataNavigateUrlFormatString="UpdateSimple.aspx?id={0}&classid={1}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,btnDeleteText%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick="return confirm('你确认要删除么？')" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Type_Changed">
                        <asp:ListItem Value="0" Selected="True" Text="<%$Resources:CMS,ContentdrpSelect%>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:CMS,ContentdropAudit%>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$Resources:CMS,ContentdropNotApprove%>"></asp:ListItem>
                        <asp:ListItem Value="3" Text="<%$Resources:CMS,ContentdropDraftList%>"></asp:ListItem>
                        <asp:ListItem Value="4" Text="设为推荐文章"></asp:ListItem>
                        <asp:ListItem Value="5" Text="设为热门文章"></asp:ListItem>
                        <asp:ListItem Value="6" Text="设为醒目文章"></asp:ListItem>
                        <asp:ListItem Value="7" Text="设为置顶文章"></asp:ListItem>
                    </asp:DropDownList>
                  
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

