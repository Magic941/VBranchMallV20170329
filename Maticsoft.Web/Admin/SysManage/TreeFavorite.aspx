<%@ Page Title="<%$ Resources:SysManage, ptTreeFavorite%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="TreeFavorite.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.TreeFavorite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        *
        {
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, lblCustomFavoritesMenu%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage, lblCustomShortcutMenu%>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg" align="center">
                    <asp:DataList ID="listMenus" Width="550px" runat="server" RepeatColumns="2" ItemStyle-VerticalAlign="Top"
                        RepeatDirection="Horizontal" ItemStyle-HorizontalAlign="Left" OnItemDataBound="listMenus_ItemDataBound"
                        DataKeyField="NodeID">
                        <ItemTemplate>
                            <img alt="" src="/admin/images/menuimg/thumbnails.gif" />
                            <b>
                                <%# DataBinder.Eval(Container, "DataItem.TreeText")%>
                            </b>
                            <br />
                            <asp:TreeView runat="server" ID="treeMenu" ShowLines="true" ShowCheckBoxes="Leaf"
                                ShowExpandCollapse="true">
                            </asp:TreeView>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="tdbg">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Site, btnEditText%>"
                        class="adminsubmit_short" OnClick="btnEdit_Click" Visible=false />
                    <asp:Label ID="lblTooltip" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
