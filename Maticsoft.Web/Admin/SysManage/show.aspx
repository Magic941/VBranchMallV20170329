<%@ Page Title="<%$ Resources:SysManage, ptMenuShow%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="show.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, ptMenuShow%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage, lblMenuShow%>" />
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
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Site, fieldFeedback_iID%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblID" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, fieldNodeName%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblName" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage, fieldParent%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblTarget" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, lblOrder%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblOrderid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                               <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:SysManage, fieldUrl%>"/>：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblUrl" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                       <tr style="display:none">
                            <td class="td_classshow">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:Site,lblIcon%>"/>：
                            </td>
                            <td height="22" align="left">
                                <asp:Image ID="Image1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:SysManage, fieldNodeType%>" />:
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblTreeType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:SysManage, fieldNodeEnable%>" />:
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblEnable" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:SysManage, fieldPermission%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblPermission" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:SysManage, fieldDescription%>" />：
                            </td>
                            <td height="22" align="left">
                                <asp:Label ID="lblDescription" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25"> 

                            <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site,btnBackText%>"
                                    onclick="btnCancle_Click" class="adminsubmit_short">
                                </asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
