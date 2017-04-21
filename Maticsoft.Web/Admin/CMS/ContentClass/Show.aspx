<%@ Page Title="<%$Resources:CMS,CCptShow %>" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.CMS.ContentClass.Show" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,CCptShow %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,CClblShow %>" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,CClassID %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblClassID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,CClassType %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblClassTypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMS,CCShowType %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblClassModel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMS,ContentlblClassName %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblClassName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:CMS,CCFieldImageUrl %>" />£º
                            </td>
                            <td height="40" width="*" align="left">
                                <asp:Image ID="imgUrl" runat="server" Width="40px" Height="40px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:CMS,CCTheParent %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblParentId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:CMS,CCFieldDescription %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMS,ContentKeywordList %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblKeywords" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMS,CCTemplateName %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblPageModelName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,State %>" />£º
                            </td>
                            <td height="25">
                                <div style="margin-top: -5px; margin-left: 0px;">
                                    <asp:Label ID="lblState" runat="server"></asp:Label></div>
                                <div style="margin-top: -18px; margin-left: 60px;">
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:CMS,CCOrder %>" />£º<asp:Label
                                        ID="lblOrders" runat="server"></asp:Label></div>
                                <div style="margin-top: -18px; margin-left: 140px;">
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:CMS,CColumnIndex %>" />£º<asp:Label
                                        ID="lblClassIndex" runat="server"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkAllowSubclass" runat="server" Text="<%$Resources:CMS,CCAddSubclass %>"
                                    Enabled="false" />
                                <div style="margin-top: -22px; margin-left: 120px;">
                                    <asp:CheckBox ID="chkAllowAddContent" runat="server" Text="<%$Resources:CMS,CCAddContent %>"
                                        Enabled="false" /></div>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                <asp:Literal ID="Literal17" runat="server" Text="<%$Resources:CMS,ContentfieldCreatedDate %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_classshow">
                                <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:CMS,ContentlblCreater %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,remark %>" />£º
                            </td>
                            <td height="25">
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnCloseText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click" OnClientClick="javascript:parent.$.colorbox.close();">
                                </asp:Button>
                            </td>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
