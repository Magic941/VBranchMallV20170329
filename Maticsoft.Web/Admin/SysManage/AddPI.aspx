<%@ Page Title="<%$Resources:SysManage,ptAdjustmentMenuOrder%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddPI.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.AddPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="doing" runat="server" style="z-index: 13000; left: 0px; width: 100%; cursor: wait;
                    position: absolute; top: 0px; height: 100%">
                    <table bgcolor="LightGrey" style="filter: Alpha(Opacity=70);" width="100%" height="100%">
                        <tr align="center" valign="middle">
                            <td>
                                <img src="/images/busy.gif" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%--    <table class="user_border" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0" id="table1">
        <tr>
            <td valign="top">
                <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                    <tr>
                        <td align="left">
                            <span style="font-size: 12pt; font-weight: bold; color: #3666AA">
                                <img src="/admin/images/icon.gif" align="absmiddle" style="border-width: 0px;" />
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptAdjustmentMenuOrder%>" /></span>
                        </td>
                        <td align="middle">
                            <table align="left" id="table3">
                                <tr valign="top" align="left">
                                    <td width="80">
                                        <a href="TreeFavorite.aspx">
                                            <img title="" src="/admin/images/view.gif" border="0" alt="" /></a>
                                    </td>
                                    <td width="80">                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>
        <div class="newslistabout">
            <div class="newslist_title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitle">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,lblCustomFavoritesMenu %>"/>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitlebody">
                           <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SysManage,lblCustomShortcutMenu%>"/>
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="tdbg">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="td_class">
                                           <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:SysManage,lblMenuList%>"/>：
                                        </td>
                                        <td height="25" width="170">
                                            <asp:ListBox ID="listboxSysManage" runat="server" Height="241px" Width="157px"></asp:ListBox>
                                        </td>
                                        <td height="25" align="left" valign="middle">
                                            <asp:Button ID="btnUP" runat="server" Text="<%$ Resources:Site, btnUpText %>" OnClick="btnUP_Click"
                                                class="adminsubmit"></asp:Button><br />
                                            <asp:Button ID="btnDown" runat="server" Text="<%$ Resources:Site, btnDownText %>"
                                                OnClick="btnDown_Click" class="adminsubmit"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_class">
                                        </td>
                                        <td height="45" >
                                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                                OnClick="btnSave_Click" class="adminsubmit"></asp:Button>
                                            <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                                OnClick="btnCancle_Click" class="adminsubmit"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
