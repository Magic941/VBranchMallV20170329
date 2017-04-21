<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.CMS.VideoClass.Show" Title="<%$ Resources:CMSVideo, ptVideoClassShow %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlShow" runat="server" Text="<%$ Resources:CMSVideo, ptVideoClassShow %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoClassShowTip %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlName" runat="server" Text="<%$ Resources:CMSVideo, Name %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblVideoClassName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlParentClass" runat="server" Text="<%$ Resources:CMSVideo, ParentClass %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblParentID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlSequence" runat="server" Text="<%$ Resources:CMSVideo, ltlSequence %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblSequence" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlPath" runat="server" Text="<%$ Resources:CMSVideo, ltlPath %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPath" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="ltlDepth" runat="server" Text="<%$ Resources:CMSVideo, ltlDepth %>"></asp:Literal>
                                ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblDepth" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</asp:Content>
