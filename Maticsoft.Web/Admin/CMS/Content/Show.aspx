<%@ Page Language="C#" Title="<%$Resources:CMS,ContentptShow%>" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.CMS.Content.Show" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
                <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ContentptShow %>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,ContentlblShow %>"/>
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
                           <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ContentlblContentID %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblContentID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                           <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblTitle %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="td_classshow">
                           <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblSubtitled %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="td_classshow"style="vertical-align:top">
                           <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblIntro %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:TextBox ID="txtSummary" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow" style="vertical-align:top">
                           <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,Content %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td_classshow">
                           <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblThumbnails %>"/>£º
                        </td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                        </td>
                        <td height="40" width="*" align="left">
                            <asp:Image ID="imgUrl" runat="server" Width="120" Height="120"  style="margin-top:-25px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                           <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:CMS,ContentfieldCreatedDate %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="td_classshow">
                           <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMS,ContentlblCreater %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <tr style="display:none">
                        <td class="td_classshow">
                           <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMS,ContentlblLaster %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblLastEditUserID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:CMS,ContentlblLastDate %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblLastEditDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:CMS,ContentlblLinkUrl %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblLinkUrl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,State %>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblState" runat="server"></asp:Label>
                                <div style="margin-top: -20px; margin-left: 70px;">
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:CMS,ContentlblViews %>"/>£º <asp:Label ID="lblPvCount" runat="server"></asp:Label></div>
                                <div style="margin-top: -18px; margin-left: 190px;">
                                     <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Site,lblOrder %>"/>£º  <asp:Label ID="lblOrders" runat="server"></asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal17" runat="server" Text="<%$Resources:CMS,ContentlblClassType%>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblClassID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                           <asp:Literal ID="Literal18" runat="server" Text="<%$Resources:CMS,ContentKeywordList%>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblKeywords" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal19" runat="server" Text="<%$Resources:CMS,ContentlblContentProperty%>"/>£º
                        </td>
                        <td  height="25">
                            <asp:CheckBox ID="chkIsCom" Text="<%$Resources:CMS,ContentlblRecommend%>" runat="server"  Enabled="false" />
                            <div style="margin-top: -22px; margin-left: 100px;">
                                    <asp:CheckBox ID="chkIsHot" Text="<%$Resources:CMS,ContentlblHotSpot%>" runat="server"  Enabled="false" /></div>
                            <div style="margin-top: -22px; margin-left: 190px;">
                                    <asp:CheckBox ID="chkIsColor" Text="<%$Resources:CMS,ContentlblEyeCatching%>" runat="server"  Enabled="false" /></div>
                                <div style="margin-top: -22px; margin-left: 290px;">
                                    <asp:CheckBox ID="chkIsTop" Text="<%$Resources:CMS,ContentlblTop%>" runat="server"  Enabled="false" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal20" runat="server" Text="<%$Resources:CMS,ContentlblAppendix%>"/>£º
                        </td>
                        <td  height="25">
                           <%-- <asp:Label ID="lblAttachment" runat="server"></asp:Label>--%>
                            <asp:HyperLink ID="lnkAttachment" runat="server">¡¾<asp:Literal ID="Literal21" runat="server" Text="<%$Resources:CMS,ContentlblDownload%>"/>¡¿</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_classshow">
                            <asp:Literal ID="Literal22" runat="server" Text="<%$Resources:Site,remark%>"/>£º
                        </td>
                        <td  height="25">
                            <asp:Label ID="lblRemary" runat="server"></asp:Label>
                        </td>
                    </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                 <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnBackText%>" class="adminsubmit_short" OnClick="btnCancle_Click"  OnClientClick="javascript:parent.$.colorbox.close();"></asp:Button>
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
