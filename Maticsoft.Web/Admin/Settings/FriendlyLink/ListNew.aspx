<%@ Page Title="<%$Resources:SiteSetting,prFriendlyLinkList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ListNew.aspx.cs" Inherits="Maticsoft.Web.FriendlyLink.FLinks.ListNew" %>

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
                       <asp:Literal ID="Literal4" runat="server" Text="管理网站的所有友情链接，您可以添加、修改或删除友情链接" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SiteSetting,lblIsDisplay%>" />：
                    <asp:DropDownList ID="DropIsDisplay" runat="server">
                        <asp:ListItem Value="" Selected="True" Text="<%$Resources:Site,All%>"></asp:ListItem>
                        <asp:ListItem Value="true" Text="<%$Resources:SiteSetting,lblYes%>"></asp:ListItem>
                        <asp:ListItem Value="false" Text="<%$Resources:SiteSetting,lblNo%>"></asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="addnew.aspx"><asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd%>" /></a> <b>|</b> </li>
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
            <asp:BoundField DataField="OrderID" HeaderText="顺序" SortExpression="OrderID" ItemStyle-HorizontalAlign="Center"  Visible="false"/>
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
            <asp:TemplateField HeaderText="<%$Resources:SiteSetting,lblIsDisplay%>"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# fsIsDisplay(Eval("IsDisplay"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ShowNew.aspx?id={0}" Text="<%$ Resources:Site, btnDetailText %>"
                ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50"
                DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ModifyNew.aspx?id={0}" Text="<%$ Resources:Site, btnEditText %>"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-HorizontalAlign="Center">
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
                   OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit"  OnClick="btnDelete_Click"  />
                    <asp:Button ID="btnIsDisplay" runat="server" Text="<%$ Resources:Site, btnIsDisplayList %>"
                    class="adminsubmit" OnClick="btnIsDisplay_Click" />
                    <asp:Button ID="btnIsNotDisplay" runat="server" Text="<%$ Resources:Site,btnIsNotDisplayList %>"
                    class="adminsubmit"  OnClick="btnIsNotDisplay_Click" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
