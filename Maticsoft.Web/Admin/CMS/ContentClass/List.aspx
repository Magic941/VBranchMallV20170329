<%@ Page Title="<%$Resources:CMS,CCptAlist %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.CMS.ContentClass.List" %>

<%--<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/Admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        } //btnApprove
        function BatchApprove() {
            $("[id$='btnApprove']").click();
        }
        function BatchUnApprove() {
            $("[id$='btnInverseApprove']").click();
        }
        function BatchUpdateStat() {
            $("[id$='btnUpdateState']").click();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "800", height: "600", overlayClose: false });
        });
        $(function () {
            $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                if (index != 0) {
                    var optionTag = $(this).html();
                    if (optionTag.indexOf("parentid=\"0\"") < 0) {
                        //$(domEle).hide();
                        $(domEle).show();
                        $(".productcag1 span img").attr("src", "/admin/images/jia.gif");
                    }
                }
            })
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,CCptAlist %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,CClblAlist %>" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/admin/images/list.gif" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,State %>" />：
                    <asp:DropDownList ID="ddlState" runat="server">
                        <asp:ListItem Value="" Selected="True" Text="<%$Resources:Site,PleaseSelect %>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:Site,btnApproveText %>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:CMS,ContentdrpDraft %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$Resources:Site,Unaudited %>"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,CCSearchMethod %>" />：
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Value="0" Selected="True" Text="<%$Resources:Site,PleaseSelect %>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:CMS,ContentlblClassName %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$Resources:CMS,CCFieldDescription %>"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
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
                        <a href="add.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                 
                    <li id="openAll" style="background: url(/admin/images/jia.gif) no-repeat; width: 60px;">
                        <a style="cursor: pointer;">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMS,CCExpandAll%>" /></a><b>|</b></li>
                    <li id="closeAll" style="background: url(/admin/images/jian.gif) no-repeat; width: 60px;">
                        <a style="cursor: pointer;">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMS,CCollapseAll %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeader="true"
            CssClass="GridViewStyle" RowStyle-CssClass="grdrow" DataKeyNames="ClassID" ShowFooter="false"
            SelectedRowStyle-BackColor="#FBFBF4" OnRowDataBound="gridView_RowDataBound" OnRowCommand="gridView_RowCommand"
            OnRowDeleting="gridView_RowDeleting" CellPadding="3" BorderWidth="1px" BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Site,Select %>" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="ckSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,CClassification %>">
                    <ItemTemplate>
                        <span id="spShowImage" runat="server" parentid='<%# Eval("ParentID") %>'>
                            <img src="/admin/images/jian.gif" width="24" height="24" alt="" />
                        </span>
                        <asp:Label ID="lblContentClassName" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,CClassType %>" SortExpression="ClassTypeID">
                    <ItemTemplate>
                        <%# Eval("ClassTypeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,CContentModel %>" SortExpression="ClassModel">
                    <ItemTemplate>
                        <%# GetContentModel(Eval("ClassModel"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Site,State %>" SortExpression="State"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetState(Eval("State"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ClassIndex" HeaderText="<%$Resources:CMS,CColumnIndex %>"
                    SortExpression="ClassIndex" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="<%$Resources:CMS,CCAddSubclass %>" SortExpression="AllowAddContent"
                    Visible="false">
                    <ItemTemplate>
                        <%# GetboolText(Eval("AllowSubclass").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,CCAddContent %>" SortExpression="AllowAddContent"
                    Visible="false">
                    <ItemTemplate>
                        <%# GetboolText(Eval("AllowAddContent").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PageModelName" HeaderText="<%$Resources:CMS,CCTemplateName%>"
                    SortExpression="PageModelName" Visible="false" />
                <asp:BoundField DataField="Description" HeaderText="<%$Resources:CMS,CCFieldDescription%>"
                    SortExpression="Description" Visible="false" />
                <asp:BoundField DataField="Keywords" HeaderText="<%$Resources:CMS,ContentKeywordList%>"
                    SortExpression="Keywords" Visible="false" />
                <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:CMS,ContentfieldCreatedDate%>"
                    SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Remark" HeaderText="<%$Resources:Site,remark%>" SortExpression="Remark"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="<%$Resources:Site,Sort%>" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall"
                            Width="16" Height="16" />
                        <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise"
                            Width="16" Height="16" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="40" HeaderText="<%$Resources:Site,btnDetailText%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class='iframe' href="Show.aspx?id=<%#Eval("ClassID") %>">
                            <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:CMS,ContentfieldContentID %>" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50" 
                    DataNavigateUrlFields="ClassID" DataNavigateUrlFormatString="Modify.aspx?id={0}"  
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
              <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <RowStyle Height="25px" />
        </asp:GridView>
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
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                    <asp:Button ID="btnApprove" runat="server" Text="<%$ Resources:Site, btnApproveListText %>"
                        class="adminsubmit" OnClick="btnApprove_Click" />
                    <asp:Button ID="btnInverseApprove" runat="server" Text="<%$Resources:Site,btnNotApproveList %>"
                        class="adminsubmit" OnClick="btnInverseApprove_Click" />
                    <asp:Button ID="btnUpdateState" runat="server" Text="<%$Resources:CMS,ContentdropDraftList %>"
                        class="adminsubmit" OnClick="btnUpdateState_Click" />
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //全部隐藏
            $("#closeAll").bind("click", function () {
                $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                    if (index != 0) {
                        var optionTag = $(this).html();
                        if (optionTag.indexOf("parentid=\"0\"") < 0) {
                            $(domEle).hide();
                            $(".productcag1 span img").attr("src", "/admin/images/jia.gif");
                        }
                    }
                })
            });
            //全部展开
            $("#openAll").bind("click", function () {
                $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                    if (index != 0) {
                        $(domEle).show();
                        $(".productcag1 span img").attr("src", "/admin/images/jian.gif");
                    }
                })
            });
            $(".productcag1 span img").each(function (index, imgObj) {
                $(imgObj).click(function () {
                    if ($(imgObj).attr("src") == "/admin/images/jian.gif") {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof (optionHTML) != "string") { break; }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.hide();
                                currentTrNode = currentTrNode.next();
                            }
                            else { break; }
                        }
                        //把img src设加可开打状态
                        $(imgObj).attr("src", "/admin/images/jia.gif");
                    }
                    else {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof (optionHTML) != "string") { break; }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.show();
                                currentTrNode = currentTrNode.next();
                            }
                            else { break; }
                        }
                        $(imgObj).attr("src", "/admin/images/jian.gif");
                    }
                })
            })
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>