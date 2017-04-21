<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Ms.EntryForm.Show" Title="<%$Resources:MsEntryForm,ptMsEntryShow%>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:MsEntryForm,ptMsEntryShow%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:MsEntryForm,lblMsEntryShow%>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display: none">
                            <td class="td_classshow">
                               <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,fieldFeedback_iID%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:MsEntryForm,lblUsername%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                              <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,Age%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblAge" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,fieldEmail%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                              <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,fieldTelphone%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblTelPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblCellphone%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                QQ ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblQQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_classshow">
                                MSN ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblMSN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:MsEntryForm,lblHouseAddress%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblHouseAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:MsEntryForm,lblCompanyAddress%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblCompanyAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:MsEnterprise,lblRegionID%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblRegionId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,fieldSex%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblSex" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Site,Content%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,remark%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblremark" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal16" runat="server" Text="申请店铺类型" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblApplyType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:Site,State%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <br />
                                <asp:Button ID="btnEdit" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnEditText %>" 
                                    class="adminsubmit_short" onclick="btnEdit_Click"
                                    />
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnBackText %>" 
                                    class="adminsubmit_short" onclick="btnCancle_Click"
                                    />
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
