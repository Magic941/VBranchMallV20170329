<%@ Page Title="<%$Resources:SiteSetting,prFriendlyLinkList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.FriendlyLink.FLinks.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SiteSetting,prFriendlyLinkList%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:SiteSetting,lblFriendlyLinkList%>" />
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
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,State%>" />£º
                    <asp:DropDownList ID="DropState" runat="server">
                        <asp:ListItem Value="" Selected="True" Text="<%$Resources:Site,All%>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$Resources:Site,Unaudited%>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$Resources:Site,btnApproveText%>"></asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,lblKeyword%>" />£º
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="add.aspx"><asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd%>" /></a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;" id="liDel" runat="server"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,btnDeleteListText%>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" ShowToolBar="True"
        AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="Ë³Ðò" SortExpression="OrderID" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Name" HeaderText="<%$Resources:Site,Name%>"  ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Image ID="imgUrl" runat="server" ImageUrl='<%#Eval("ImgUrl") %>' Width="40px"
                        Height="40px" Visible='<%# Eval("ImgUrl").ToString().Length>2 ? true : false %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LinkUrl" HeaderText="<%$Resources:SiteSetting,lblLinkUrl%>" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="LinkDesc" HeaderText="<%$Resources:SiteSetting,lblLinkDescribe%>" 
                ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:BoundField DataField="OrderID" HeaderText="<%$Resources:Site,lblOrder%>" SortExpression="OrderID" ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="ContactPerson" HeaderText="<%$Resources:SiteSetting,lblContacts%>" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Email" HeaderText="<%$Resources:Site,fieldEmail%>"  ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="TelPhone" HeaderText="<%$Resources:Site,fieldTelphone%>" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="<%$Resources:SiteSetting,lblLinkType%>" ItemStyle-HorizontalAlign="Center"  Visible="false">
                <ItemTemplate>
                    <%# fsType(Eval("TypeId")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Site,State%>"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# fsState(Eval("State"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-HorizontalAlign="Center">
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
                    class="adminsubmit"  OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                    <asp:Button ID="btnApproveList" runat="server" Text="<%$ Resources:Site, btnApproveListText %>"
                    class="adminsubmit" OnClick="btnApproveList_Click" />
                    <asp:Button ID="btnInverseApprove" runat="server" Text="<%$Resources:Site,btnNotApproveListText%>"
                    class="adminsubmit"  OnClick="btnInverseApprove_Click" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
