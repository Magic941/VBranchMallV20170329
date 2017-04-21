<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ShowNew.aspx.cs" Inherits="Maticsoft.Web.FriendlyLink.FLinks.ShowNew" Title="<%$Resources:SiteSetting,ptFriendlyLinkShow %>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        if ($("[id$='lblTypeID']").val() == "Í¼Æ¬Á´½Ó") {
            $("#ImgUrl").show();
            $("#ImgWidth").show();
            $("#ImgHeight").show();
        }
        if ($("[id$='lblTypeID']").val() == "ÎÄÕÂÁ´½Ó") {
            $("#ImgUrl").hide();
            $("#ImgWidth").hide();
            $("#ImgHeight").hide();
        }
    });
</script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SiteSetting,ptFriendlyLinkShow %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SiteSetting,lblFriendlyLinkShow %>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:SiteSetting,lblFriendlyLinkID %>" />£º
                            </td>
                            <td>
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:SiteSetting,lblLinkName %>" />£º
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="ImgUrl">
                            <td class="td_classshow">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:SiteSetting,lblImgUrl %>" />£º
                            </td>
                            <td>
                                <asp:Image ID="imgImgUrl" runat="server" Width="40px" Height="40px" />
                            </td>
                        </tr>
                        <tr id="ImgWidth">
                            <td class="td_class">
                                <asp:Literal ID="Litera13" runat="server" Text="<%$Resources:SiteSetting,lblImgWidth %>" />£º
                            </td>
                            <td>
                                <asp:Label ID="lblImgWidth" runat="server" ></asp:Label>
                            </td>
                        </tr>

                        <tr id="ImgHeight">
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:SiteSetting,lblImgHeight %>" />£º
                            </td>
                            <td>
                                <asp:Label ID="lblImgHeight" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:SiteSetting,lblLinkUrl %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblLinkUrl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:SiteSetting,lblLinkDescribe %>" />£º
                            </td>
                            <td>
                                <asp:Label ID="lblLinkDesc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:SiteSetting,lblIsDisplay %>" />£º
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:Label ID="lblIsDisplay" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,lblOrder %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:SiteSetting,lblContacts %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,fieldEmail %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,fieldTelphone %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTelPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:SiteSetting,lblLinkPattern %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnBackText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
